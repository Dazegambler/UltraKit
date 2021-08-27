using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltraMod.Data{
    [CreateAssetMenu(fileName = "UltraModItem", menuName = "Menu/MenuItem")]
    public class UltraModItem : ScriptableObject
    {
        public GameObject Prefab;
        public Texture2D Icon;
        public int type;
        public string Name;
        [TextArea]
        public string Desc;
    }
}