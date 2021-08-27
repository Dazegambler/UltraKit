using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Data;
using UltraMod.Loader.Registries;
using UnityEngine;

namespace UltraMod.Loader
{
    public static class AddonLoader
    {
        public static List<Addon> addons = new List<Addon>();
        public static Harmony harmony = new Harmony("UltraMod.Loader");
        
        //TEMP TO MAKE SPAWNMENU WORK DELETE AFTER INTEGRATION INTO SPAWNER ARM
        public static List<AssetBundle> assetBundles {
            get
            {
                var l = new List<AssetBundle>();
                foreach(var addon in addons)
                {
                    l.AddRange(addon.Bundles);
                }

                return l;
            }


        }

        public static void Initialize(string FilePath)
        {
            // FilePath = addon folder (has folders for each addon inside)

            // Loop over all folders at FilePath
            // Call LoadAddon on every folder
            if (!Directory.Exists(FilePath))
            {
                Debug.LogWarning($"Addons Directory Not Found...Creating Directory at {FilePath}");
                Directory.CreateDirectory(FilePath);
            }

            var files = Directory.GetFiles(FilePath, "*.UKMod", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                Debug.LogWarning($"LOADING ADDON:{file}");
                addons.Add(LoadAddon(file));
            }

            WeaponRegistry.Initialize();
            addons.ForEach(RegisterContent);
        }

        public static void RegisterContent(Addon a)
        {
            foreach(var content in a.LoadedContent)
            {
                //TODO: check the type and call the correct registry
                Debug.Log("Registering weapon " + content.Name);
                WeaponRegistry.Register(content);
            }
        }

        public static Addon LoadAddon(string FilePath)
        {
            //Filepath = individual addon folder (has assetbundles & lua scripts inside)
            // Load all asset bundles in folder
            // Fill all fields of 'a' variable
            var a = new Addon();

            a.Path = FilePath;

            a.Bundles = new List<AssetBundle>();
            a.Bundles.Add(AssetBundle.LoadFromFile(FilePath));

            a.Data = FindData(a.Bundles);

            a.LoadedContent = new List<UltraModItem>();
            foreach (AssetBundle bundle in a.Bundles)
            {
                List<UltraModItem> content = new List<UltraModItem>(bundle.LoadAllAssets<UltraModItem>());
                a.LoadedContent.AddRange(content);
            }

            return a;
        }

        //is this necessary?
        public static UltraModData FindData(List<AssetBundle> Bundles)
        {
            UltraModData data = null;
            if(data == null) { 
                foreach (AssetBundle bundle in Bundles)
                {
                    try
                    {
                        data = bundle.LoadAsset<UltraModData>("ModData");
                    }
                    catch (NullReferenceException)
                    {
                    }
                }
            }

            return data;
        }
    }
}
