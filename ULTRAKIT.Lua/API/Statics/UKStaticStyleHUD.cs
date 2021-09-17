using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Lua.API.Abstract;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Statics
{
    public class UKStaticStyleHUD : UKStatic
    {
        public override string name => "StyleHUD";

        private StyleHUD styleHUD = MonoSingleton<StyleHUD>.Instance;
    }
}
