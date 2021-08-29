﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Data;
using UltraMod.Data.ScriptableObjects.Registry;
using UnityEngine;

namespace UltraMod.Loader
{
    public class Addon
    {
        public UKAddonData Data;
        public bool enabled;
        public string Path;
        public AssetBundle Bundle;
    }
}
