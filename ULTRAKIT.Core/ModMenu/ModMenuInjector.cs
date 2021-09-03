using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Loader;
using UnityEngine;

namespace ULTRAKIT.Core.ModMenu
{
    public static class ModMenuInjector
    {
        

        public static void Initialize()
        {
            AddonLoader.harmony.PatchAll(typeof(OptionsMenuToManagerPatch));
        }

        
    }
}
