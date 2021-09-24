using HarmonyLib;
using System.Linq;
using UnityEngine;

namespace ULTRAKIT.Loader.Injectors
{
    [HarmonyPatch(typeof(AssistController))]
    public static class AssistControllerPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void SetInfoPost(AssistController __instance)
        {
            if (AddonLoader.registry.Keys.Any((a) => a.Enabled))
            {
                __instance.cheatsEnabled = true;
            }
        }
    }
}
