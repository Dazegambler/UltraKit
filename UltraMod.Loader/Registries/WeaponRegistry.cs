using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraMod.Loader.WeaponRegistry
{
    public class UltraModWeapon
    {
        public string Name;
        public string Description;
        public List<GameObject> Variants;
    }

    public static class WeaponRegistry
    {
        public static List<UltraModWeapon> registeredWeapons;
        
        public static void Initialize()
        {

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

        static void Prefix(GunSetter __instance)
        {
            // check if equipped
            modSlots.Clear();
            
            foreach(var weap in WeaponRegistry.registeredWeapons)
            {
                modSlots.Add(weap.Variants);
            }

            foreach (var slot in modSlots)
            {
                __instance.gunc.slots.Add(slot);
                foreach(var obj in slot)
                {
                    __instance.gunc.allWeapons.Add(obj);
                }
            }
        }
    }

}
