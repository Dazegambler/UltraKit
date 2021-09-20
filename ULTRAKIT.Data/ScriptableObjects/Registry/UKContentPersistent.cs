using System;
using System.Collections.Generic;
using UnityEngine;

namespace ULTRAKIT.Data.ScriptableObjects.Registry
{
    public class UKContentPersistent : UKContent
    {
        // TODO: These two should be made into a serialized dictionary if possible
        public GameObject[] Prefabs;
        // The addon should be able to decide if it wants to be destroyed every time
        // Or injected into each scene
        //public bool[] DontDestroyOnLoad;
    }
}
