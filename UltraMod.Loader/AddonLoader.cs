using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Data;
using UnityEngine;

namespace UltraMod.Loader
{
    public static class AddonLoader
    {
        public static List<Addon> addons;

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

        }

        public static Addon LoadAddon(string FilePath)
        {
            //Filepath = individual addon folder (has assetbundles & lua scripts inside)

            UltraModData Data = null;
            var a = new Addon();

            a.path = FilePath;

            a.bundles = new List<AssetBundle>();
            a.bundles.Add(AssetBundle.LoadFromFile(FilePath));

            Data = FindData(Data,a.bundles);

            a.Name = Data.ModName;
            a.Description = Data.ModDesc;
            a.Author = Data.Author;

            a.loadedContent = new List<UltraModItem>();
            foreach (AssetBundle bundle in a.bundles)
            {
                List<UltraModItem> content = new List<UltraModItem>(bundle.LoadAllAssets<UltraModItem>());
                foreach(UltraModItem item in content)
                {
                    a.loadedContent.Add(item);
                }
            }
            //TODO
            // Load all asset bundles in folder
            // Fill all fields of 'a' variable
            // May need scriptableobject for mod name and description in assetbundle, UltraModData?



            return new Addon();
        }
        public static UltraModData FindData(UltraModData Data, List<AssetBundle> Bundles)
        {
            switch(Data)
            {
                case null:
                foreach (AssetBundle bundle in Bundles)
                {
                        try
                        {
                            Data = bundle.LoadAsset<UltraModData>("ModData");
                        }
                        catch (NullReferenceException)
                        {
                        }
                }
                break;
            }
            return new UltraModData();
        }
    }
}
