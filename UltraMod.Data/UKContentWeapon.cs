using System.Collections.Generic;
using UnityEngine;

namespace UltraMod.Data
{
    [CreateAssetMenu(fileName = "UKContentWeapon",menuName = "UltraMod/UKContentWeapon")]
    public class UKContentWeapon : ScriptableObject
    {
        public string Name;

        public Sprite Icon;

        public List<GameObject> Variants;
    }
}
