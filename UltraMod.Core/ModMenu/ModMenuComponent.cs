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
        // These two are automatically filled in; the button to access the mod list should only show when optionsMenu is active
        public GameObject pauseMenu, optionsMenu;

        // Use this to decide whether to show the mod list or not
        public bool active;

        void OnGUI()
        {
            if (active)
            {
                DrawModlist();
            }
            else
            {
                DrawButton();
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
