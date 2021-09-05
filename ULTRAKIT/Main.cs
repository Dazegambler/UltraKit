using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using ULTRAKIT.Core;
using ULTRAKIT.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ULTRAKIT.Lua;
using UnityEngine.InputSystem;
using ULTRAKIT.Loader.Registries;
using ULTRAKIT.Lua.API;

namespace ULTRAKIT
{
    [BepInPlugin("ULTRAKIT", "ULTRAKIT", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        
        public static string BundlePath = Directory.GetCurrentDirectory() + "/AssetBundles";

        public void Start()
        {
            
            CoreContent.Initialize();
            UKLuaAPI.Initialize();
            AddonLoader.Initialize(BundlePath);
            //Data.AssetDatabase.Initialiaze();
        }

        public void Update()
        {

        }
        
        
        void OnApplicationQuit()
        {
            // Ensures that the mod can be uninstalled without issue
            PlayerPrefs.SetInt("CurSlo", 1);
        }
    }
}
