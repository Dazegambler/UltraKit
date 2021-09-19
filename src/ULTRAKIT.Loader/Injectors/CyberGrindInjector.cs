using HarmonyLib;

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
