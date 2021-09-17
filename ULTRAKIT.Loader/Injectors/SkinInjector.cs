using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data.ScriptableObjects.Registry;

namespace ULTRAKIT.Loader.Injectors
{
    public class SkinInjector
    {

        /// <summary>
        /// Called when weapons are reset; should replace vanilla weapon material with skin material
        /// </summary>
        /// <param name="setter">Reference to GunSetter instance</param>
        /// <param name="skin">Reference to skin content</param>
        public static void InjectSkin(GunSetter setter, UKContentSkin skin)
        {
            
        }
    }

    [HarmonyPatch(typeof(GunSetter))]
    public static class GunSetterSkinPatches
    {

        [HarmonyPatch(nameof(GunSetter.ResetWeapons))]
        [HarmonyPrefix]
        public static void ResetWeaponPrefix(GunSetter __instance)
        {
            foreach(var item in AddonLoader.GetAll<UKContentSkin>())
            {
                SkinInjector.InjectSkin(__instance, item);
            }
        }
    }
}
