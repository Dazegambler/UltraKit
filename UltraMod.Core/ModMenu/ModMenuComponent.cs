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

        int t;

        void OnGUI()
        {
            var list = UltraMod.Loader.AddonLoader.addons;
            GUI.skin = skin;
            GUI.Window(0,wind,AddonsMenu,"");
            if (list.Count > 10)
            {
                GUI.Window(0, new Rect(20 / (Screen.width / 1920), 445 / (Screen.height / 1080), 155,40),TabButtons,"");
            }
        }

        void AddonsMenu(int id)
        {
            switch(id)
            {
                case 0:
                    var list = UltraMod.Loader.AddonLoader.addons;
                    switch (active)
                    {
                        case true:
                            if (GUI.Button(new Rect(5, 5, 140, 50), $"Addons:{list.Count}"))
                            {
                                active = !active;
                            }
                            for(int i = 0+t; i < list.Count; i++)
                            {
                                if (i < 10 + t)
                                {
                                    GUI.Button(new Rect(5, 60 + (35 * (i)), 140, 30), list.ElementAt(i).Data.ModName);
                                    wind = new Rect(20 / (Screen.width / 1920), 40 / (Screen.height / 1080), 155, 95 + (35 * i));
                                }
                            }
                            break;
                        default:
                            wind = guirect;
                            if(GUI.Button(new Rect(5,5,140,50), $"Addons:{list.Count}"))
                            {
                                active = !active;
                            }
                            break;
                    }
                    break;
            }
        } 
        void TabButtons(int id)
        {
            switch (id)
            {
                case 0:
                    var list = UltraMod.Loader.AddonLoader.addons;
                    if (GUI.Button(new Rect(5,5, 70, 30), "<"))
                    {
                        if (t != 0)
                        {
                            t--;
                        }
                    }
                    if (GUI.Button(new Rect(75,5, 70, 30), ">"))
                    {
                        if (t < list.Count - 1)
                        {
                            t++;
                        }
                    }
                    break;
            }
        }
    }
}
