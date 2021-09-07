using System.Collections.Generic;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using UnityEngine;

namespace ULTRAKIT.Data.ScriptableObjects.Registry
{
    public enum Type
    {
        Enemy,
        Spawnable,
        Throwable,
        Explosive,
    }

    [CreateAssetMenu(fileName = "UKContentSpawnable", menuName = "ULTRAKIT/UKContentSpawnable")]
    public class UKContentSpawnable : UKContent
    {
        public Type type;
        public GameObject Prefab;
    }
}
