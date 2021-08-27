using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraMod.Loader
{
    public class Addon
    {
        public string Name;
        public string Description;
        public string Author;
        public string path;

        public List<AssetBundle> bundles;
        public List<UltraModItem> loadedContent;
    }
}
