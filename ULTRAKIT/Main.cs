using BepInEx;
using System.IO;
using System.Linq;
using ULTRAKIT.Core;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using ULTRAKIT.Loader;
using ULTRAKIT.Lua.API;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace ULTRAKIT
{
    [BepInPlugin("ULTRAKIT", "ULTRAKIT", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {

        public void Start()
        {
            CoreContent.Initialize();
            UKLuaAPI.Initialize();
            AddonLoader.Initialize(AddonLoader.BundlePath);
        }

        public void Update()
        {
            if (Keyboard.current.f8Key.wasPressedThisFrame)
            {
                // Delete persistent prefabs
                /*foreach (var ukContents in AddonLoader.registry.Values)
                {
                    
                    // Fancy talk for "is List<UKContent> actually List<UKContentPersistent>"
                    if (ukContents.GetType().IsAssignableFrom(typeof(List<UKContentPersistent>)))
                    {
                        Debug.Log("m1ksu your thing worked!!");
                        // WAIT. Addons think that they load in at game start...
                        // And could behave badly if they get chucked into the game midway through.
                        // So I need to implement a way for the objects to ignore reloading if the game is goin...
                        // But what if the addon has an error? Or the user wants to unload?
                        // For this we have...
                        // TODO: fix this
                        ukContents.ForEach(go=>Destroy(go as UKContentPersistent));
                    }
                    
                }*/
                
                foreach(var addon in AddonLoader.registry.Keys)
                {
                    AddonLoader.registry[addon].Clear();
                    addon.Bundle.Unload(true);
                }

                Lua.API.Statics.UKStaticRegistry.addonData.Clear();
                Lua.API.Statics.UKStaticRegistry.sharedData.Clear();

                AddonLoader.registry.Clear();

                CoreContent.Initialize();
                AddonLoader.LoadAllAddons(AddonLoader.BundlePath);

                RefreshGuns();
            }
        }

        private void RefreshGuns()
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
            } catch {}
        }

        private void RefreshPersistents()
        {
            
        }


        void OnApplicationQuit()
        {
            // Ensures that the mod can be uninstalled without issue
            PlayerPrefs.SetInt("CurSlo", 1);
        }
    }
}
