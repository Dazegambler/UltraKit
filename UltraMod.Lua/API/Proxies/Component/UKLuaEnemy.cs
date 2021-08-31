using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Lua.API.Proxies;
using UnityEngine;

namespace UltraMod.Lua.API
{
    [MoonSharpUserData]
    public class UKLuaEnemy : UKLuaProxy<EnemyIdentifier>
    {
        [MoonSharpHidden]
        public UKLuaEnemy(EnemyIdentifier target) : base(target)
        {
        }

        public Transform transform => target.transform;
        public Rigidbody rigidbody => target.GetComponent<Rigidbody>();
        
        public void ForceAir(Script script, bool air)
        {
            if (target.gce == null)
            {
                return;
            }

            if (air)
            {
                if (target.gce.forcedOff == 0)
                {
                    target.gce.ForceOff();
                }
            }
            else
            {
                target.gce.StopForceOff();
            }
        }
    }
}
