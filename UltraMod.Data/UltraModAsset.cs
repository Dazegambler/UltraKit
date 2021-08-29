using UnityEngine;
using System.Collections.Generic;

namespace UltraMod.Data
{
    public struct UltraAsset
    {
        public string Name;
        public UnityEngine.Object asset;
    }
    class UltraModAsset : ScriptableObject
    {
        List<UltraAsset> Assets;
    }
}
