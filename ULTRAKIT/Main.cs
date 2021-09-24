using System;
using System.Net;
using BepInEx;
using Humanizer;
using ULTRAKIT.Core;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using ULTRAKIT.Loader;
using ULTRAKIT.Lua.API;
using ULTRAKIT.Lua.API.Statics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = ULTRAKIT.Lua.Debug;

namespace ULTRAKIT
{
    [BepInPlugin("ULTRAKIT", "ULTRAKIT", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public void Start()
        {
            UpdateChecker.PrintStatus();
            CoreContent.Initialize();
            UKLuaAPI.Initialize();
            AddonLoader.Initialize(AddonLoader.BundlePath);
        }

        public void Update()
        {
            // Addon refresh
            if (Keyboard.current.f8Key.wasPressedThisFrame)
            {
                // JetBrains Rider is suggesting me to turn this into a LINQ expression.
                // You better pray to God that I don't.
                foreach (var ukContents in AddonLoader.registry.Values)
                foreach (var content in ukContents)
                    if (content is UKContentPersistent persistent)
                        Destroy(persistent.Prefab);

                foreach (var addon in AddonLoader.registry.Keys)
                {
                    AddonLoader.registry[addon].Clear();
                    addon.Bundle.Unload(true);
                }

                UKStaticRegistry.addonData.Clear();
                UKStaticRegistry.sharedData.Clear();

                AddonLoader.registry.Clear();

                CoreContent.Initialize();
                AddonLoader.LoadAllAddons(AddonLoader.BundlePath);

                RefreshGuns();
            }
        }

        private static void RefreshGuns()
        {
            // This is expected to throw an error since there's not always a GunSetter present (eg. in menus)
            try
            {
                var gs = MonoSingleton<GunSetter>.Instance;
                var storedSlot = gs.gunc.currentSlot;
                var storedVariant = gs.gunc.currentVariation;
                gs.ResetWeapons();
                gs.gunc.currentSlot = storedSlot;
                gs.gunc.currentVariation = storedVariant;
                gs.gunc.YesWeapon();
            }
            catch
            {
            }
        }

        private void OnApplicationQuit()
        {
            // Ensures that the mod can be uninstalled without issue
            PlayerPrefs.SetInt("CurSlo", 1);
        }
    }
}