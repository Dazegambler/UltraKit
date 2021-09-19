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

    public class UKContentSpawnable : UKContent
    {
        public Type type;
        public GameObject Prefab;
    }
}
