using BepInEx;
using System.IO;
using ULTRAKIT.Core;
using ULTRAKIT.Loader;
using ULTRAKIT.Lua.API;
using UnityEngine;

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
