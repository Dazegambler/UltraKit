using System;
using System.Collections.Generic;
using UnityEngine;

namespace ULTRAKIT.Data.ScriptableObjects.Registry
{
    [CreateAssetMenu(fileName = "UKContentWeapon",menuName = "ULTRAKIT/UKContentWeapon")]
    public class UKContentWeapon : UKContent
    {
        public GameObject[] Variants;
    }
}
