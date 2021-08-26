using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UltraModItem",menuName = "MenuItem/MenuItem")]
public class UltraModItem : ScriptableObject
{
    public GameObject Prefab;
    public int type;
    public string Name, Desc;
}
