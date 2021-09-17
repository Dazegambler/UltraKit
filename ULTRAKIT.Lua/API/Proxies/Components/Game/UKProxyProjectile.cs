using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    public class UKProxyProjectile : UKProxyComponentAbstract<Projectile>
    {
        [MoonSharpHidden]
        public UKProxyProjectile(Projectile target) : base(target)
        {
        }

        public Rigidbody rigidbody => target.GetComponent<Rigidbody>();
    }
}
