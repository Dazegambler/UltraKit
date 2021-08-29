using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraMod.Core.ModMenu
{
    [HarmonyPatch(typeof(OptionsManager)]
    public static class OptionsManagerPatch
    {
        [HarmonyPatch(typeof("Awake"))]
        [HarmonyPrefix]
        public static void AwakePrefix(OptionsManager __instance)
        {
            __instance.optionsMenu.AddComponent<ModMenuComponent>();
        }
    }
}
