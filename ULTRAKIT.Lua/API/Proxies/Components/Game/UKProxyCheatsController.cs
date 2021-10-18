using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    public class UKProxyCheatsController : UKProxyComponentAbstract<CheatsController>
    {
        [MoonSharpHidden]
        public UKProxyCheatsController(CheatsController target) : base(target)
        {
        }
        public bool BlindEnemies = CheatsController.BlindEnemies;
        public bool IgnoreArenaTriggers = CheatsController.IgnoreArenaTriggers;
        public bool NoCooldown = CheatsController.NoCooldown;
    }
}
