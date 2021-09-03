using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Core.BossSpawns;
using ULTRAKIT.Core.ModMenu;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using ULTRAKIT.Loader.Registries;
using UnityEngine;

namespace ULTRAKIT.Core
{
    public static class CoreContent
    {
        //TODO: rename?
        public static AssetBundle UIBundle = LoadAssetBundle(Properties.Resource1.ULTRAKIT);

        public static void Initialize()
        {
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
