using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRAKIT.Loader.Injectors
{
    [HarmonyPatch(typeof(AssistController))]
    public static class AssistControllerPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void SetInfoPost(AssistController __instance)
        {
            __instance.majorEnabled = true;
        }
    }
}
