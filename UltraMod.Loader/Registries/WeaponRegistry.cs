using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Data;
using UnityEngine;

namespace UltraMod.Loader.Registries
{
    public class UltraModWeapon
    {
        public string Name;
        public string Description;
        public List<GameObject> Variants;
    }

    public static class WeaponRegistry
    {
        public static List<UltraModWeapon> registeredWeapons = new List<UltraModWeapon>();
        
        public static void Initialize()
        {
            AddonLoader.harmony.PatchAll(typeof(GunSetterPatch));
            AddonLoader.harmony.PatchAll(typeof(GunControlPatch));
        }

        public static void Register(UltraModItem item)
        {
            var w = new UltraModWeapon();
            w.Name = item.Name;
            w.Description = item.Desc;
            w.Variants = new List<GameObject>();

            foreach(Transform variant in item.Prefab.transform)
            {
                w.Variants.Add(variant.gameObject);
            }

            registeredWeapons.Add(w);
        }


    }

    [HarmonyPatch(typeof(GunSetter), "ResetWeapons")]
    class GunSetterPatch
    {
        static List<List<GameObject>> modSlots = new List<List<GameObject>>();

        static void Postfix(GunSetter __instance)
        {
            modSlots.Clear();
            
            foreach(var weap in WeaponRegistry.registeredWeapons)
            {

                // check if equipped
                var slot = new List<GameObject>();
                foreach(var variant in weap.Variants)
                {
                    var go = GameObject.Instantiate(variant, __instance.transform);
                    go.SetActive(false);
                    slot.Add(go);

                    var field = typeof(GunControl).GetField("weaponFreshnesses", BindingFlags.NonPublic | BindingFlags.Instance);
                    var freshnessList = field.GetValue(__instance.gunc) as List<float>;
                    freshnessList.Add(10);

                    field.SetValue(__instance.gunc, freshnessList);

                    __instance.gunc.slots.Add(slot);
                    foreach (var obj in slot)
                    {
                        obj.SetActive(false);
                        __instance.gunc.allWeapons.Add(obj);
                    }
                }

                Debug.Log(slot.Count);

                modSlots.Add(slot);
            }
        }
    }
    
    [HarmonyPatch(typeof(GunControl), "Start")]
    class GunControlPatch
    {
        static void Prefix(GunControl __instance)
        {
            // Important to avoid semi-permanently breaking weapon script lol
            if (PlayerPrefs.GetInt("CurSlo", 1) > __instance.slots.Count)
            {
                PlayerPrefs.SetInt("CurSlo", 1);
            }
        }
    }

}
