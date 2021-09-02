using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraMod.Core.ModMenu
{ 
    public class ModMenuComponent : MonoBehaviour
    {
        // Loads GuiSkin in Built-in Asset Bundle
        GUISkin skin = ModMenuInjector.UIBundle.LoadAsset<GUISkin>("UIUltraMod");

        // These two are automatically filled in; the button to access the mod list should only show when optionsMenu is active
        public GameObject pauseMenu, optionsMenu;

        // Use this to decide whether to show the mod list or not
        public bool active;

        Rect guirect = new Rect(20 / (Screen.width / 1920), 40 / (Screen.height / 1080), 155, 60), wind;
        Rect _scroll = Rect.zero;
        Vector2 Scroll = Vector2.zero;

        int t;

        void OnGUI()
        {
            if (pauseMenu.activeInHierarchy)
            {
                var list = UltraMod.Loader.AddonLoader.addons;
                GUI.skin = skin;
                GUI.Window(0, wind, AddonsMenu, "");
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
                var list = UltraMod.Loader.AddonLoader.addons;
                if (active)
                {
                    if (GUI.Button(new Rect(5, 5, 140, 50), $"Addons:{list.Count}"))
                    {
                        active = !active;
                    }
                    if (list.Count > 20)
                    {
                        Scroll = GUI.BeginScrollView(new Rect(5, 60, 155, 800), Scroll, _scroll, false, false);
                        for (int i = 0; i < list.Count; i++)
                        {
                            GUI.Button(new Rect(5, 60 + (35 * (i)), 140, 30), i.ToString());
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
                            GUI.Button(new Rect(5, 60 + (35 * (i)), 140, 30), list.ElementAt(i)?.Data?.ModName ?? "Unnamed Mod");
                            wind.height = 95 + (35 * i);
                        }
                    }
                }
                else
                {
                    wind = guirect;
                    if (GUI.Button(new Rect(5, 5, 140, 50), $"Addons:{list.Count}"))
                    {
                        active = !active;
                    }
                }
            }
        } 
    }
}
