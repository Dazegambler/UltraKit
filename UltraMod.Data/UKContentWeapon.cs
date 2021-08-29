using System.Collections.Generic;
using UnityEngine;

namespace UltraMod.Data
{
    [CreateAssetMenu(fileName = "UKContentWeapon",menuName = "UltraMod/UKContentWeapon")]
    class UKContentWeapon : ScriptableObject
    {
        public string Name;

        public Sprite Icon;

        public List<GameObject> Variants;
    }
}
