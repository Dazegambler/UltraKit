using System;
using ULTRAKIT.Core.BossSpawns;
using ULTRAKIT.Core.ModMenu;
using ULTRAKIT.Loader;
using UnityEngine;

namespace ULTRAKIT.Core
{
    public static class CoreContent
    {
        //TODO: rename?
        public static AssetBundle UIBundle;

        public static void Initialize()
        {
            UIBundle = LoadAssetBundle(Properties.Resource1.ULTRAKIT);
            AddonLoader.harmony.PatchAll();

            ModMenuInjector.Initialize();
            BossSpawnsInjector.Initialize();
        }

        static AssetBundle LoadAssetBundle(byte[] Bytes)
        {
            if (Bytes == null) throw new ArgumentNullException(nameof(Bytes));
            var bundle = AssetBundle.LoadFromMemory(Bytes);
            return bundle;
        }
    }
}
