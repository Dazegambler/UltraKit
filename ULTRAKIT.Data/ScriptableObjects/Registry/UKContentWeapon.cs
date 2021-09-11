using UnityEngine;

namespace ULTRAKIT.Data.ScriptableObjects.Registry
{
    [CreateAssetMenu(fileName = "UKContentWeapon", menuName = "ULTRAKIT/UKContentWeapon")]
    public class UKContentWeapon : UKContent
    {
        public GameObject[] Variants;
    }
}
