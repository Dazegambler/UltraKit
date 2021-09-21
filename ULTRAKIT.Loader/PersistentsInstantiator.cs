using System.Collections.Generic;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using ULTRAKIT.Lua.Components;
using UnityEngine;

namespace ULTRAKIT.Loader
{
    public class PersistentsInstantiator
    {
        public static void InstantiatePersistents(Addon a)
        {
            foreach (var persistent in a.Bundle.LoadAllAssets<UKContentPersistent>())
            {
                var go = Object.Instantiate(persistent.Prefab);
                Object.DontDestroyOnLoad(go);
                UKScriptRuntime.Create(a.Data, go, true);
            }
        }
    }
}