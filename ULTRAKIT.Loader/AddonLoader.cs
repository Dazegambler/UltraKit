using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ULTRAKIT.Data;
using ULTRAKIT.Data.ScriptableObjects.Registry;
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
        public static List<Addon> addons
        {
            get
            {
                return registry.Keys.ToList();
            }
        }

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


        public static void Initialize(string FilePath)
        {
            // FilePath = addon folder (has folders for each addon inside)
            // Loop over all folders at FilePath
            // Call LoadAddon on every folder
            Debug.LogWarning("LOADING ADDONS...");
            LoadAllAddons(FilePath);
            Debug.LogWarning("...FINISHED LOADING ADDONS");



            harmony.PatchAll();
        }

        public static void LoadAllAddons(string FilePath)
        {
            if (!Directory.Exists(FilePath))
            {
                Debug.LogWarning($"Addons Directory Not Found...Creating Directory at {FilePath}");
                Directory.CreateDirectory(FilePath);
            }

            var files = Directory.GetFiles(FilePath, "*.*", SearchOption.AllDirectories);
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

        public static Addon LoadAddon(string FilePath)
        {
            //Filepath = individual addon folder (has assetbundles & lua scripts inside)
            // Load all asset bundles in folder
            // Fill all fields of 'a' variable
            var a = new Addon();
            a.Path = FilePath;
            a.Bundle = AssetBundle.LoadFromFile(FilePath);
            a.Data = a.Bundle.LoadAllAssets<UKAddonData>()[0];

            registry.Add(a, new List<UKContent>());
            registry[a].AddRange(a.Bundle.LoadAllAssets<UKContentWeapon>());
            registry[a].AddRange(a.Bundle.LoadAllAssets<UKContentSpawnable>());

            return a;
        }

    }
}
