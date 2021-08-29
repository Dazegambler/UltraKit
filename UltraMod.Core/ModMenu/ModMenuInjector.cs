using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Loader;
using UnityEngine;

namespace UltraMod.Core.ModMenu
{
    public static class ModMenuInjector
    {
        public static AssetBundle UIBundle = LoadAssetBundle(Properties.Resource1.ultramod);

        public static void Initialize()
        {
            AddonLoader.harmony.PatchAll(typeof(OptionsMenuToManagerPatch));
        }

        static AssetBundle LoadAssetBundle(byte[] Bytes)
        {
            if (Bytes == null) throw new ArgumentNullException(nameof(Bytes));
            var bundle = AssetBundle.LoadFromMemory(Bytes);
            return bundle;
        }
    }
}
