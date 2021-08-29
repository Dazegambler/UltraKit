using UnityEngine;
using System.Collections.Generic;
using System;

namespace UltraMod.Data
{
    [Serializable]
    public struct UKAddonAsset
    {
        public string Name;
        public UnityEngine.Object asset;
    }

    [CreateAssetMenu(fileName = "New Asset Database", menuName = "UltraMod/Asset Database")]
    public class UKAddonAssetDB : ScriptableObject
    {
        List<UKAddonAsset> Assets;
    }
}
