using System.Collections.Generic;
using UnityEngine;

namespace UltraMod.Data
{
    public enum Type
    {
        Enemy,
        Spawnable,
    }

    [CreateAssetMenu(fileName = "UKContentWeapon", menuName = "UltraMod/UKContentSpawnable")]
    class UKContentSpawnable : ScriptableObject
    {
        public Type type;
        public string Name;

        public Sprite Icon;

        public GameObject Prefab;
    }
}
