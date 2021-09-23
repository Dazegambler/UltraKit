using UnityEngine;

namespace ULTRAKIT.Data
{
    public class UKAddonData : ScriptableObject
    {
        public string ModName;
        public string Author;
        // public bool GenerateDataFolder;

        [TextArea]
        public string ModDesc;
    }
}