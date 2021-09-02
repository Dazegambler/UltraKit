using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltraMod.Data.ScriptableObjects.Registry
{
    [CreateAssetMenu(fileName = "UKContentWeapon",menuName = "UltraMod/UKContentWeapon")]
    public class UKContentWeapon : UKContent
    {
        public GameObject[] Variants;
    }
}
