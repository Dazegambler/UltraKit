using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ULTRAKIT.Data;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using Debug = ULTRAKIT.Lua.Debug;
using UnityEngine;

namespace ULTRAKIT.Loader
{
    public static class AddonExtensions
    {
        public static List<T> GetAll<T>(this Addon a)
            where T : UKContent
        {
            var res = AddonLoader.registry[a].Where(k => k is T).ToList();
            return res.Cast<T>().ToList();
        }
    }
    public static class AddonLoader
    {
        public static readonly string BundlePath = Directory.GetCurrentDirectory() + "/AssetBundles";
        
        public static List<Addon> addons => registry.Keys.ToList();
        public static Dictionary<Addon, List<UKContent>> registry = new Dictionary<Addon, List<UKContent>>();
        
        public static List<T> GetAll<T>()
            where T : UKContent
        {
            var res = new List<T>();

            foreach (var val in registry.Values)
            {
                res.AddRange(val.Where(k => k is T).ToList().Cast<T>());
            }

            return res;
        }


        public static Harmony harmony = new Harmony("ULTRAKIT.Loader");

        //TEMP TO MAKE SPAWNMENU WORK DELETE AFTER INTEGRATION INTO SPAWNER ARM

        public static void Initialize(string filePath)
        {
            // FilePath = addon folder (has folders for each addon inside)
            // Loop over all folders at FilePath
            // Call LoadAddon on every folder
            Debug.LogWarning("LOADING ADDONS...");
            LoadAllAddons(filePath);
            Debug.LogWarning("...FINISHED LOADING ADDONS");

            harmony.PatchAll();
        }

        public static void LoadAllAddons(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                Debug.LogWarning($"Addons directory not found... creating directory at {filePath}");
                Directory.CreateDirectory(filePath);
            }

            var files = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                Debug.LogWarning($"LOADING ADDON:{file}");

                try
                {
                    LoadAddon(file);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }

        public static Addon LoadAddon(string filePath)
        {
            // filePath = individual addon folder (has assetbundles & lua scripts inside)
            // Load all asset bundles in folder
            // Fill all fields of 'a' variable
            var a = new Addon();
            a.Path = filePath;
            a.Bundle = AssetBundle.LoadFromFile(filePath);
            a.Data = a.Bundle.LoadAllAssets<UKAddonData>()[0];
            
            // if (a.Data.GenerateDataFolder) CreateAddonDataDirectory(a);
            // Debug.Log(a.Data.GenerateDataFolder);

            registry.Add(a, new List<UKContent>());
            registry[a].AddRange(a.Bundle.LoadAllAssets<UKContentWeapon>());
            registry[a].AddRange(a.Bundle.LoadAllAssets<UKContentSpawnable>());

            // Injection of persistent GameObjects into the scene
            PersistentsInstantiator.InstantiatePersistents(a);
            var persistentAddons = a.Bundle.LoadAllAssets<UKContentPersistent>();
            registry[a].AddRange(persistentAddons);

            return a;
        }

        // work in progress
        // private static void CreateAddonDataDirectory(Addon addon)
        // {
        //     // creates folder named the addon's name into the folder
        //     var dataFolder = Lua.API.Statics.UKStaticFileLoader.AddonDataFolder;
        //     if (Directory.Exists(dataFolder)) Directory.CreateDirectory(dataFolder);
        //     var path = $@"{dataFolder}/{addon.Data.ModName}";
        //     if (Directory.Exists(path)) return;
        //     
        //     Debug.Log($@"Created new addon data directory at {path}");
        //     Directory.CreateDirectory(path);
        // }
    }
}
