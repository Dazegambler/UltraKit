using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraMod.Core
{
    //TODO: a proper keybinding system, where lua scripts can register custom bindings
    //TODO: a menu to handle the above keybinding system
    public static class Bindings
    {
        public static ConfigEntry<KeyCode> Toggle, Spawn, SetTractorTrgt, ResetTractor;

        public static void Initialize(ConfigFile config)
        {
            Toggle = config.Bind("Binds", "Toggle", KeyCode.Backspace);
            Spawn = config.Bind("Binds", "Spawn", KeyCode.I);
            SetTractorTrgt = config.Bind("Binds", "TractorObj Set", KeyCode.X);
            ResetTractor = config.Bind("Binds", "TractorObj Reset", KeyCode.Z);
        }
    }
}
