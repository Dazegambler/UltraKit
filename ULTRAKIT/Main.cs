using BepInEx;
using System.IO;
using System.Linq;
using ULTRAKIT.Core;
using ULTRAKIT.Loader;
using ULTRAKIT.Lua.API;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ULTRAKIT
{
    [BepInPlugin("ULTRAKIT", "ULTRAKIT", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {

        public static string BundlePath = Directory.GetCurrentDirectory() + "/Addons";

        public void Start()
        {
            CoreContent.Initialize();
            UKLuaAPI.Initialize();
            AddonLoader.Initialize(BundlePath);
        }

        public void Update()
        {
            if (Keyboard.current.f8Key.wasPressedThisFrame)
            {
                foreach(var addon in AddonLoader.registry.Keys)
                {
                    AddonLoader.registry[addon].Clear();
                    addon.Bundle.Unload(true);
                }

                Lua.API.Statics.UKStaticRegistry.addonData.Clear();
                Lua.API.Statics.UKStaticRegistry.sharedData.Clear();

                AddonLoader.registry.Clear();

                CoreContent.Initialize();
                AddonLoader.LoadAllAddons(BundlePath);

                // This is expected to throw an error since there's not always a gunsetter present (eg. in menus)
                try
                {
                    var gs = MonoSingleton<GunSetter>.Instance;
                    var storedSlot = gs.gunc.currentSlot;
                    var storedVariant = gs.gunc.currentVariation;
                    gs.ResetWeapons();
                    gs.gunc.currentSlot = storedSlot;
                    gs.gunc.currentVariation = storedVariant;
                    gs.gunc.YesWeapon();
                } catch {}
            }
        }


        void OnApplicationQuit()
        {
            // Ensures that the mod can be uninstalled without issue
            PlayerPrefs.SetInt("CurSlo", 1);
        }
    }
}
