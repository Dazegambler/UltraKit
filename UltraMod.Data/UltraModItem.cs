using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltraMod.Data
{
    public enum ContentType {
        Spawnable,
        Enemy,
        Weapon
    }

    [CreateAssetMenu(fileName = "UltraModItem", menuName = "Menu/MenuItem")]
    public class UltraModItem : ScriptableObject
    {
        public ContentType type;

        public GameObject Prefab;

        public Texture2D Icon;
        public string Name;
        [TextArea]
        public string Desc;
    }
}