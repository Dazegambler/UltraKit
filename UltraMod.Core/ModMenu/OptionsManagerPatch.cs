using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Loader;
using UnityEngine;

namespace UltraMod.Core.ModMenu
{
    [HarmonyPatch(typeof(OptionsMenuToManager))]
    public static class OptionsMenuToManagerPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPostfix(OptionsMenuToManager __instance)
        {
            var opm = __instance.GetPrivate("opm") as OptionsManager;
            var mm = opm.gameObject.AddComponent<ModMenuComponent>();
            mm.optionsMenu = __instance.optionsMenu;
            mm.pauseMenu = __instance.pauseMenu;
        }
    }
}
