using System.Collections.Generic;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using UnityEngine;

namespace ULTRAKIT.Loader
{
    public class PersistentsInstantiator
    {
        public static void InstantiatePersistents(IEnumerable<UKContentPersistent> list)
        {
            foreach (var persistent in list)
            {
                foreach (var prefab in persistent.Prefabs)
                {
                    Object.DontDestroyOnLoad(Object.Instantiate(prefab));
                }
            }
        }
    }
}