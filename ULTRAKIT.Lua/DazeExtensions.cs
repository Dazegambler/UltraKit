using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace General_Unity_Tools
{
    public static class DazeExtensions
    {

        public static T[] GetComponentsInArray<T>(this GameObject[] source)
        {
            List<T> a = new List<T>();

            try
            {
                foreach (var obj in source)
                {
                    a.Add(obj.GetComponent<T>());
                }
            }
            catch
            {
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

        public static T[] GetComponentsInArray<T>(this Transform[] source)
        {
            List<T> a = new List<T>();

            try
            {
                foreach (var obj in source)
                {
                    a.Add(obj.GetComponent<T>());
                }
            }
            catch
            {
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

        public static T[] GetAllComponentsInArray<T>(this GameObject[] source)
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

        public static T[] GetAllComponentsInArray<T>(this Transform[] source)
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

        public static Transform[] ListChildren(this GameObject parent)
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

        public static Transform[] ListChildren(this Transform parent)
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

        public static Transform FindInChildren(this Transform parent, string name)
        {
            Transform a = new GameObject().transform;
            foreach (Transform obj in parent.GetComponentsInChildren<Transform>())
            {
                if (obj.name == name) a = obj;
            }
            return a;
        }

        public static Transform FindInChildren(this GameObject parent, string name)
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
            Debug.LogWarning($"FOUND:{a.name}");
            return a;
        }
    }
}
