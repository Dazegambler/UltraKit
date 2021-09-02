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
        }
    }
}
