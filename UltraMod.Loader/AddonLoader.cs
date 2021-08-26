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
        public static List<AssetBundle> assetBundles = new List<AssetBundle>();

        public static void Initialize(string FilePath)
        {
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
            Debug.LogWarning("FINISHED LOADING ASSETBUNDLES");
            
        }
    }
}
