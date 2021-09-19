using UnityEngine;
using System.Collections;
using System.Linq;

namespace ULTRAKIT.Core.ModMenu
{
    public class ModMenuComponent : MonoBehaviour
    {
        // Loads GuiSkin in Built-in Asset Bundle
        //GUISkin skin = CoreContent.UIBundle.LoadAsset<GUISkin>("UIUltraMod");
            // Absolutely not!! This shit breaks when you reload the mods

        // These two are automatically filled in; the button to access the mod list should only show when optionsMenu is active
        public GameObject pauseMenu, optionsMenu;

        // Use this to decide whether to show the mod list or not
        public bool active;

        const float addonMenuWidth = 240;

        Rect
            guirect = new Rect(20 / (Screen.width / 1920), 40 / (Screen.height / 1080), addonMenuWidth, 60),
            wind;
        Rect
            _scroll = Rect.zero;
        Vector2
            Scroll = Vector2.zero;

        void OnGUI()
        {
            if (pauseMenu.activeInHierarchy)
            {
                var list = ULTRAKIT.Loader.AddonLoader.addons;
                guirect.height = 60 + 35 * list.Count();
                GUI.skin = CoreContent.UIBundle.LoadAsset<GUISkin>("UIUltraMod");
                GUI.Window(0, guirect, AddonsMenu, "");

                Rect reloadNotifierRect = new Rect(
                    position: guirect.min + new Vector2(addonMenuWidth+10,0),
                    size: new Vector2(
                        x: 1000 / (Screen.width / 1920), 
                        y: 60 / (Screen.height / 1080)));

                GUI.Label(reloadNotifierRect, "Press F8 to\nreload addons\nat any time");
            }
            else
            {
                active = false;
            }
        }

        void AddonsMenu(int id)
        {
            if (id == 0)
            {
                var list = ULTRAKIT.Loader.AddonLoader.addons;

                if (GUI.Button(new Rect(5, 5, addonMenuWidth - 10, 50), $"Addons:{list.Count}"))
                {
                }

                active = true;

                if (active)
                {
                    if (list.Count > 20)
                    {
                        Scroll = GUI.BeginScrollView(new Rect(5, 60, 155, 800), Scroll, _scroll, false, false);
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (GUI.Button(new Rect(5, 60 + (35 * (i)), addonMenuWidth - 10, 30), list.ElementAt(i)?.Data?.ModName ?? "Unnamed Mod"))
                            {
                                Debug.LogWarning($"{list.ElementAt(i)?.Data?.ModName ?? "Unnamed Mod"} Selected");
                            }
                            _scroll = new Rect(5, 60, 155, 95 + (35 * i));
                            if (wind.height > 800)
                            {
                                wind.height = 800;
                            }
                            else
                            {
                                wind.height = 95 + (35 * i);
                            }
                        }
                        GUI.EndScrollView();
                    }
                    else
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            // This doesn't seem to trigger
                            if (GUI.Button(new Rect(5, 60 + (35 * (i)), addonMenuWidth - 10, 30), list.ElementAt(i)?.Data?.ModName ?? "Unnamed Mod"))
                            {
                                Debug.LogWarning($"{list.ElementAt(i)?.Data?.ModName ?? "Unnamed Mod"} Selected");
                            }
                            wind.height = 95 + (35 * i);//95 + (35 * i)
                        }
                    }
                }
                else
                {
                    wind = guirect;
                }
            }
        }
    }
}
