using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                    l.Add(addon.Bundle);
                }

                return l;
            }


        }

        public static void Initialize(string FilePath)
        {
            // FilePath = addon folder (has folders for each addon inside)
            // Loop over all folders at FilePath
            // Call LoadAddon on every folder
            Debug.LogWarning("LOADING ADDONS...");
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
                    addons.Add(LoadAddon(file));
                } catch(Exception e)
                {
                    Debug.LogWarning(e.Message);
                }
            }
            Debug.LogWarning("...FINISHED LOADING ADDONS");

            harmony.PatchAll(Assembly.GetExecutingAssembly());
            addons.ForEach(RegisterContent);
        }

        public static void RegisterContent(Addon a)
        {
            //REMOVE
            SpawnableRegistry.registeredObjects.Add(a, new List<SpawnableObject>());

            foreach (var weapon in a.LoadedWeapons)
            {
                WeaponRegistry.Register(weapon);
            }

            foreach (var spawnable in a.LoadedSpawnables)
            {
                SpawnableRegistry.Register(a,spawnable);
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

            a.Data = a.Bundle.LoadAsset<UltraModData>("ModData");

            a.LoadedWeapons = a.Bundle.LoadAllAssets<UKContentWeapon>().ToList();
            

            a.LoadedSpawnables = a.Bundle.LoadAllAssets<UKContentSpawnable>().ToList();
            
            return a;
        }
        
    }
}
