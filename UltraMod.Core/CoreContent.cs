using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Core.ModMenu;
using UltraMod.Data.ScriptableObjects.Registry;
using UltraMod.Loader.Registries;
using UnityEngine;

namespace UltraMod.Core
{
    public static class CoreContent
    {
        

        public static void Initialize()
        {
            ModMenuInjector.Initialize();
        }
    }
}
