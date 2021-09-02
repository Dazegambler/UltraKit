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
            
            CoreContent.Initialize();
            UKLuaAPI.Initialize();
            AddonLoader.Initialize(BundlePath);
        }

        public void Update()
        {

        }
        
        
        
    }
}
