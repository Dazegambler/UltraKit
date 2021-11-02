using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Core.Extensions
{
    public static class DazeExtensions
    {

        public static T[] GetComponentsInArray<T>(this Component[] source)
        {
            List<T> a = new List<T>();

            foreach (var obj in source)
            {
                try
                {
                    a.Add(obj.GetComponent<T>());
                }
                catch
                {
                }
            }

            try
            {
                return a.ToArray();
            }
            catch
            {
                Debug.LogWarning($"DazeExtensions.GetComponentsInArray:Error Returning Array");
                return new T[0];
            }
        }
        public static T[] GetAllComponentsInArray<T>(this Component[] source)
        {
            List<T> a = new List<T>();

            foreach (var obj in source)
            {
                try
                {
                    a.Add(obj.GetComponent<T>());
                }
                catch
                {
                }
                foreach (var _obj in obj.ListChildren())
                {
                    try
                    {
                        a.Add(_obj.GetComponent<T>());
                    }
                    catch
                    {

                    }
                }

            }
            try
            {
                return a.ToArray();
            }
            catch
            {
                Debug.LogWarning($"DazeExtensions.GetComponentsInArray:Error Returning Array");
                return new T[0];
            }
        }

        public static Transform[] ListChildren(this Component parent)
        {
            Transform[] a;
            a = parent.GetComponentsInChildren<Transform>(true);
            try
            {
                return a;
            }
            catch
            {
                Debug.LogWarning($"DazeExtensions.ListChildren:COULD NOT LIST CHILDREN INSIDE {parent.name}");
                return new Transform[0];
            }
        }

        public static Transform FindInChildren(this Component parent, string name)
        {
            Transform a = new GameObject().transform;
            foreach (Transform obj in parent.GetComponentsInChildren<Transform>())
            {
                if (obj.name == name) a = obj;
            }
            return a;
        }

        public static GameObject PrefabFind(string name)
        {
            GameObject a = new GameObject();
            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (obj.name == name) a = obj;
            }
            return a;
        }

        public static GameObject PrefabFind(this AssetBundle bundle,string bundlename,string name)
        {
            if(bundle == null)
            {
                if (File.Exists($@"{Application.productName}_Data\StreamingAssets\{bundlename}"))
                {
                    var data = File.ReadAllBytes($@"{Application.productName}_Data\StreamingAssets\{bundlename}");
                    bundle = AssetBundle.LoadFromMemory(data) ?? LoadFromLoaded(bundle,"common");
                }
                else
                {
                    Debug.LogWarning($"Could not find bundle {bundlename} or StreamingAssets file");
                    return new GameObject();
                }
            }
            return bundle.LoadAsset<GameObject>(name) ?? new GameObject();
        }
        private static AssetBundle LoadFromLoaded(this AssetBundle bundle, string name)
        {
            foreach (var bndl in AssetBundle.GetAllLoadedAssetBundles())
            {
                if (bndl.name == name)
                {
                    bundle = bndl;
                }
            }
            return bundle ?? null;
        }
    }
}
