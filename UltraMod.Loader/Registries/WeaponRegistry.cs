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

    public static class WeaponRegistry
    {
        public static List<UKContentWeapon> registeredWeapons = new List<UKContentWeapon>();

        public static void Register(UKContentWeapon item)
        {
            registeredWeapons.Add(item);
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
