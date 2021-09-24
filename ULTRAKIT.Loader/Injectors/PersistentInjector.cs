using System.Collections.Generic;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using ULTRAKIT.Lua.Components;
using UnityEngine;

namespace ULTRAKIT.Loader
{
    public class PersistentInjector
    {
        public static Dictionary<Addon, List<GameObject>> persistDict = new Dictionary<Addon, List<GameObject>>();

        public static void Initialize()
        {
            foreach (var pair in AddonLoader.registry) {
                if (!persistDict.ContainsKey(pair.Key))
                {
                    persistDict.Add(pair.Key, new List<GameObject>());
                }

                foreach (var persistent in pair.Key.GetAll<UKContentPersistent>())
                {
                    var go = Object.Instantiate(persistent.Prefab);
                    Object.DontDestroyOnLoad(go);
                    UKScriptRuntime.Create(pair.Key.Data, go, true);
                    go.SetActive(true);

                    persistDict[pair.Key].Add(go);
                }
            }
        }
    }
}