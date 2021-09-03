using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data;
using ULTRAKIT.Data.Components;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using ULTRAKIT.Lua;
using ULTRAKIT.Lua.Components;
using UnityEngine;

namespace ULTRAKIT.Loader.Registries
{
    [HarmonyPatch(typeof(GunSetter), "ResetWeapons")]
    public class GunSetterPatch
    {
        public static List<List<GameObject>> modSlots = new List<List<GameObject>>();
        public static Dictionary<GameObject, bool> equippedDict = new Dictionary<GameObject, bool>();

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
                        if (!equippedDict.ContainsKey(variant))
                        {
                            equippedDict.Add(variant, true);
                        }

                        if (!equippedDict[variant])
                        {
                            continue;
                        }


                        var go = GameObject.Instantiate(variant, __instance.transform);
                        go.SetActive(false);
                        

                        foreach (var c in go.GetComponentsInChildren<Renderer>(true))
                        {
                            c.gameObject.layer = LayerMask.NameToLayer("AlwaysOnTop");
                            
                            var glow = c.gameObject.GetComponent<UKGlow>();
                            if (glow)
                            {
                                c.material.shader = Shader.Find("psx/railgun");
                                c.material.SetFloat("_EmissivePosition", 5);
                                c.material.SetFloat("_EmissiveStrength", glow.glowIntensity);
                                c.material.SetColor("_EmissiveColor", glow.glowColor);
                            }

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
    
    [HarmonyPatch(typeof(GunControl))]
    class GunControlPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static void StartPrefix(GunControl __instance)
        {
            // Important to avoid semi-permanently breaking weapon script lol
            if (PlayerPrefs.GetInt("CurSlo", 1) > __instance.slots.Count)
            {
                PlayerPrefs.SetInt("CurSlo", 1);
            }
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void UpdatePostfix(GunControl __instance)
        {
            if (MonoSingleton<InputManager>.Instance.InputSource.Slot6.WasPerformedThisFrame && __instance.slots[5]?.Count > 1)
            {
                __instance.SwitchWeapon(6);
            }

            if (MonoSingleton<InputManager>.Instance.InputSource.Slot7.WasPerformedThisFrame && __instance.slots[6]?.Count > 1)
            {
                __instance.SwitchWeapon(7);
            }

            if (MonoSingleton<InputManager>.Instance.InputSource.Slot8.WasPerformedThisFrame && __instance.slots[7]?.Count > 1)
            {
                __instance.SwitchWeapon(8);
            }
        }
    }

}
