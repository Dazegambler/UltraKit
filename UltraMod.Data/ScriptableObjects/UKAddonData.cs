using UnityEngine;

namespace ULTRAKIT.Data
{
    [CreateAssetMenu(fileName = "ModData", menuName = "ULTRAKIT/ModData")]
    public class UKAddonData : ScriptableObject
    {
        public string ModName;
        public string Author;

        [TextArea]
        public string ModDesc;
    }
}