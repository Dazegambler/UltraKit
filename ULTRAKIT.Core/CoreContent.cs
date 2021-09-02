using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Core.ModMenu;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using ULTRAKIT.Loader.Registries;
using UnityEngine;

namespace ULTRAKIT.Core
{
    public static class CoreContent
    {
        

        public static void Initialize()
        {
            ModMenuInjector.Initialize();
            Loader.AddonLoader.addons.Add(CreateBossAddon());
        }
        public static Loader.Addon CreateBossAddon()
        {
            Loader.Addon b = new Loader.Addon();

            b.Data = new Data.UKAddonData();
            b.Data.ModName = "Vanilla Bosses/Enemies";
            b.Data.Author = "UltraKit";
            b.Data.ModDesc = "Contains Enemies that cannot be spawned by the spawner arm";

            b.Path = "Internal";

            b.enabled = false;

            return b;
        }
    }
}
