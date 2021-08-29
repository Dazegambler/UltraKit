using System.Collections.Generic;
using UltraMod.Data.ScriptableObjects.Registry;
using UnityEngine;

namespace UltraMod.Data.ScriptableObjects.Registry
{
    public enum Type
    {
        Enemy,
        Spawnable,
    }

    [CreateAssetMenu(fileName = "UKContentWeapon", menuName = "UltraMod/UKContentSpawnable")]
    public class UKContentSpawnable : UKContent
    {
        public Type type;
        public GameObject Prefab;
    }
}
