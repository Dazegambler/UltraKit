using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using UnityEngine;

namespace ULTRAKIT.Loader
{
    public class Addon
    {
        public UKAddonData Data;
        public bool enabled;
        public string Path;
        public AssetBundle Bundle;
    }
}
