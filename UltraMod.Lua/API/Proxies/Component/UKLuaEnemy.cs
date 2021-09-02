using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Lua.API.Proxies;
using UnityEngine;

namespace ULTRAKIT.Lua.API
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

        public void InstaKill() => target.InstaKill();

        public void Damage(Script script, float dmg, Vector3 force, Vector3 point)
        {
            target.DeliverDamage(target.gameObject, force, point, dmg, false);
        }

        public bool GoLimp() {
            if(target.machine != null)
            {
                target.machine?.GoLimp();
                return true;
            }

            if (target.zombie != null)
            {
                target.zombie?.GoLimp();
            }

            return false;
        }

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
