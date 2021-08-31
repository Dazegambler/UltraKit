using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using UltraMod.Core;
using UltraMod.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UltraMod.Lua;
using UnityEngine.InputSystem;
using UltraMod.Loader.Registries;
using UltraMod.Lua.API;

namespace UltraMod
{
    [BepInPlugin("ULTRA.MOD", "ULTRAMOD", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        
        public static string BundlePath = Directory.GetCurrentDirectory() + "/AssetBundles";

        public void Start()
        {
            
            Bindings.Initialize(Config);
            CoreContent.Initialize();
            UKLuaAPI.Initialize();
            AddonLoader.Initialize(BundlePath);

            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if(mode != LoadSceneMode.Additive && scene.name != "Intro" && scene.name != "Main Menu")
                {
                    if(CameraController.Instance != null)
                    {
                    }
                }
            };
        }

        public void Update()
        {
            //TODO: better.
            if (Keyboard.current.f8Key.wasPressedThisFrame)
            {
                foreach(var addon in AddonLoader.addons)
                {
                    addon.Bundle?.Unload(true);
                }
                AddonLoader.registry.Clear();
 
                foreach (var slot in GunSetterPatch.modSlots)
                {
                    MonoSingleton<GunControl>.Instance.slots.Remove(slot);
                }
                GunSetterPatch.modSlots.Clear();

                UKLuaInput.bindings.Clear();
            }   
        }
        
        
        
    }
}
