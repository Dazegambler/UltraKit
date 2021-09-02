using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Data;
using UltraMod.Data.ScriptableObjects.Registry;
using UltraMod.Lua;
using UltraMod.Lua.Components;
using UnityEngine;

namespace UltraMod.Loader.Registries
{
    [HarmonyPatch(typeof(GunSetter), "ResetWeapons")]
    public class GunSetterPatch
    {
        public static List<List<GameObject>> modSlots = new List<List<GameObject>>();

        static void Postfix(GunSetter __instance)
        {
            Debug.Log("Weapons resetting");
            modSlots.Clear();
            foreach (var pair in AddonLoader.registry)
            {
                foreach (var weap in pair.Key.GetAll<UKContentWeapon>())
                {

                    // check if equipped
                    var slot = new List<GameObject>();

                    int i = 0;
                    foreach (var variant in weap.Variants)
                    {
                        var go = GameObject.Instantiate(variant, __instance.transform);
                        go.SetActive(false);

                        Debug.Log(go.name);
                        foreach (var c in go.GetComponentsInChildren<Renderer>())
                        {
                            if (c is MeshRenderer)
                            {
                                //c.material.shader = Shader.Find("psx/railgun");
                            }
                            
                            c.gameObject.layer = LayerMask.NameToLayer("AlwaysOnTop");
                        }
                        var wi = go.AddComponent<WeaponIcon>();
                        wi.weaponIcon = weap.Icon;
                        wi.glowIcon = weap.Icon;
                        wi.variationColor = i;
                        i++;

                        slot.Add(go);

                        var field = typeof(GunControl).GetField("weaponFreshnesses", BindingFlags.NonPublic | BindingFlags.Instance);
                        var freshnessList = field.GetValue(__instance.gunc) as List<float>;
                        freshnessList.Add(10);
                        field.SetValue(__instance.gunc, freshnessList);

                        __instance.gunc.allWeapons.Add(go);

                        UKScriptRuntime.Create(pair.Key.Data, go);
                    }

                    __instance.gunc.slots.Add(slot);

                    Debug.Log(slot.Count);

                    modSlots.Add(slot);
                }
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
