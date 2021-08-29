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
        public GameObject Prefab;
    }

    public static class WeaponRegistry
    {
        public static List<UltraModWeapon> registeredWeapons = new List<UltraModWeapon>();

        static void SetLayerAllChildren(Transform root, int layer)
        {
            var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                //            Debug.Log(child.name);
                child.gameObject.layer = layer;
            }
        }

        public static void Register(UltraModItem item)
        {
            var w = new UltraModWeapon();
            w.Name = item.Name;
            w.Description = item.Desc;
            w.Prefab = item.Prefab;

            w.Prefab.AddComponent<WeaponIdentifier>();
            SetLayerAllChildren(w.Prefab.transform, LayerMask.NameToLayer("AlwaysOnTop"));

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
                var newGo = GameObject.Instantiate(weap.Prefab, __instance.transform);
                foreach(Transform variant in newGo.transform)
                {
                    var field = typeof(GunControl).GetField("weaponFreshnesses", BindingFlags.NonPublic | BindingFlags.Instance);
                    var freshnessList = field.GetValue(__instance.gunc) as List<float>;
                    freshnessList.Add(10);

                    field.SetValue(__instance.gunc, freshnessList);
                    slot.Add(variant.gameObject);

                    variant.gameObject.SetActive(false);
                    __instance.gunc.allWeapons.Add(variant.gameObject);
                }

                __instance.gunc.slots.Add(slot);

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
