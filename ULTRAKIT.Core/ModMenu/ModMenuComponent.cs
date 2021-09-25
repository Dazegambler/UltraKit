using UnityEngine;
using System.Collections;
using ULTRAKIT.Loader;
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

        Loader.Addon Selected;

        Rect guirect = new Rect(20 / (Screen.width / 1920), 40 / (Screen.height / 1080), addonMenuWidth, 60);
        Rect wind;

        Rect _scroll = Rect.zero;
        Vector2 Scroll = Vector2.zero;

        void OnGUI()
        {
            if (pauseMenu.activeInHierarchy)
            {
                var list = ULTRAKIT.Loader.AddonLoader.addons;
                guirect.height = 60 + 35 * list.Count();
                GUI.skin = CoreContent.UIBundle.LoadAsset<GUISkin>("UIUltraMod");
                GUI.Window(0, guirect, AddonsMenu, "");

                if (Selected != null && AddonLoader.registry.ContainsKey(Selected))
                {
                    GUI.Label(new Rect(addonMenuWidth + 25, 100, 1000, 30), $"{Selected.Data.ModName}");
                    GUI.Box(new Rect(addonMenuWidth+25,130,addonMenuWidth+10,110),"");
                    if (GUI.Button(new Rect(addonMenuWidth + 30, 135, addonMenuWidth, 30), "SPAWN INFO"))
                    {
                        GameObject.Instantiate(Info(Selected), GameObject.Find("Player").transform.position,Quaternion.identity);
                    }
                    if (GUI.Button(new Rect(addonMenuWidth + 30, 170, addonMenuWidth, 30), "ENABLE/DISABLE"))
                    {
                        Selected.Enabled = !Selected.Enabled;
                    }
                    if (GUI.Button(new Rect(addonMenuWidth + 30, 205, addonMenuWidth, 30), "CLOSE"))
                    {
                        Selected = null;
                    }
                }

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

        private GameObject Info(Addon _addon)
        {
            GameObject a = CoreContent.UIBundle.LoadAsset<GameObject>("InfoTab");
            string info = $"{_addon.Data.ModName}\nBy {_addon.Data.Author}\n{_addon.Data.ModDesc}";

            a.layer = 22;

            if (a.TryGetComponent<ItemIdentifier>(out var b) == false)
            {
                a.AddComponent<ItemIdentifier>();
            }
            var item = a.GetComponent<ItemIdentifier>();
            item.itemType = ItemType.Readable;
            item.putDownPosition = Vector3.zero;
            item.putDownRotation = Vector3.zero;
            item.noHoldingAnimation = true;
            item.putDownScale = new Vector3(a.transform.localScale.x, a.transform.localScale.y, a.transform.localScale.z);
            item.pickUpSound = new GameObject();

            if(a.TryGetComponent<Readable>(out var c) == false)
            {
                a.AddComponent<Readable>();
            }
            var read = a.GetComponent<Readable>();
            read.SetPrivate("content",info);
            read.SetPrivate("instantScan",true);

            if(a.TryGetComponent<CheckForScroller>(out var d) == false)
            {
                a.AddComponent<CheckForScroller>();
            }
            var scroller = a.GetComponent<CheckForScroller>();
            scroller.checkOnCollision = true;

            return a;
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
                                Selected = list.ElementAt(i);
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
                            if(list.ElementAt(i).Enabled == false)
                            {
                                if (GUI.Button(new Rect(5, 60 + (35 * (i)), addonMenuWidth - 10, 30), $"<color=grey>{list.ElementAt(i)?.Data?.ModName ?? "Unnamed Mod "}</color>"))
                                {
                                    Selected = list.ElementAt(i);
                                }
                            }
                            else
                            {
                                if (GUI.Button(new Rect(5, 60 + (35 * (i)), addonMenuWidth - 10, 30), list.ElementAt(i)?.Data?.ModName ?? "Unnamed Mod "))
                                {
                                    Selected = list.ElementAt(i);
                                }
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
