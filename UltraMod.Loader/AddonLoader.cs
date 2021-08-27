using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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






            // OLD CODE
            /*Debug.Log(typeof(UltraModItem).Name);

            if (!Directory.Exists(FilePath))
            {
                Debug.LogWarning("Asset Bundle Directory Not Found...Creating Directory at " + FilePath);
                Directory.CreateDirectory(FilePath);
            }

            Debug.LogWarning("LOADING ASSETBUNDLES");
            var files = Directory.GetFiles(FilePath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                Debug.LogWarning($"Loading {file}");
                assetBundles.Add(AssetBundle.LoadFromFile(file));
            }
            Debug.LogWarning("FINISHED LOADING ASSETBUNDLES");*/
            
        }

        public static Addon LoadAddon(string FilePath)
        {
            //Filepath = individual addon folder (has assetbundles & lua scripts inside)

            var a = new Addon();
            //TODO
            // Load all asset bundles in folder
            // Fill all fields of 'a' variable
            // May need scriptableobject for mod name and description in assetbundle, UltraModData?



            return new Addon();
        }
    }
}
