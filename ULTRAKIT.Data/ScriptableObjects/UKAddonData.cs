using UnityEngine;

namespace ULTRAKIT.Data
{
    public class UKAddonData : ScriptableObject
    {
        public string ModName;
        public string Author;

        [TextArea]
        public string ModDesc;

        public string ExportPath;
    }
}
