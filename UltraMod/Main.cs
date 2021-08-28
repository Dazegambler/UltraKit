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

namespace UltraMod
{
    [BepInPlugin("ULTRA.MOD", "ULTRAMOD", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        static AssetBundle UIBundle = Plugin.LoadAssetBundle(Properties.Resource1.ultramod);
        public static string BundlePath = Directory.GetCurrentDirectory() + "/AssetBundles";

        public void Start()
        {
            
            Bindings.Initialize(Config);
            CoreContent.Initialize();
            AddonLoader.Initialize(BundlePath);

            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if(mode != LoadSceneMode.Additive && scene.name != "Intro" && scene.name != "Main Menu")
                {
                    if(CameraController.Instance != null)
                    {
                        //CameraController.Instance.gameObject.AddComponent<UltraModSpawnMenu>();
                        //CameraController.Instance.gameObject.GetComponent<UltraModSpawnMenu>().skin = UIBundle.LoadAsset<GUISkin>("UIUltraMod");
                    }
                }
            };
        }

        public void Update()
        {
            
        }
        
        
        static AssetBundle LoadAssetBundle(byte[] Bytes)
        {
            if (Bytes == null) throw new ArgumentNullException(nameof(Bytes));
            var bundle = AssetBundle.LoadFromMemory(Bytes);
            return bundle;
        }
    }
}
