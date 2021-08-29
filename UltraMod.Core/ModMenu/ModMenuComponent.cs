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

        Rect guirect = new Rect(100 / (Screen.width / 1920), 100 / (Screen.height / 1080), 155, 55), wind;

        void OnGUI()
        {
            GUI.skin = skin;
            GUI.Window(0,wind,AddonsMenu,"");
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
                            if (GUI.Button(new Rect(5, 5, 140, 50), "SHOW ADDONS"))
                            {
                                active = !active;
                            }
                            for(int i = 0; i < list.Count; i++)
                            {
                                GUI.Button(new Rect(5,55+(35*i),140,30),list.ElementAt(i).Data.ModName);
                                wind = new Rect(100 / (Screen.width / 1920), 100 / (Screen.height / 1080),155,35+(30*i));
                            }
                            break;
                        default:
                            wind = guirect;
                            if(GUI.Button(new Rect(5,5,140,50), "SHOW ADDONS"))
                            {
                                active = !active;
                            }
                            break;
                    }
                    break;
            }
        } 

        void DrawButton()
        {
        }

        void DrawModlist()
        {

        }
    }
}
