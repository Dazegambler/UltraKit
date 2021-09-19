using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    public class UKProxyEnemy : UKProxyComponentAbstract<EnemyIdentifier>
    {
        static Dictionary<EnemyIdentifier, List<Script>> airForcers = new Dictionary<EnemyIdentifier, List<Script>>();

        public UKProxyEnemy(EnemyIdentifier target) : base(target)
        {
        }

        public Rigidbody rigidbody => target.GetComponent<Rigidbody>();
        public bool dead => target.dead;
        public float health => target.health;
        public string enemyType => target.enemyType.ToString();
        
        public void Damage(float damage) => target.DeliverDamage(target.gameObject, Vector3.zero, target.transform.position, damage, false);
        public void Damage(float damage, Vector3 force) => target.DeliverDamage(target.gameObject, force, target.transform.position, damage, false);
        public void Damage(float damage, Vector3 force, GameObject targetObj) => target.DeliverDamage(targetObj, force, targetObj.transform.position, damage, false);
        public void Damage(float damage, Vector3 point, Vector3 force, GameObject targetObj) => target.DeliverDamage(targetObj, force, point, damage, false);
        public void Explode() => target.Explode();
        public void Splatter() => target.Splatter();
        
        public void ForceAir(Script s, bool forced)
        {
            if(target.gce == null)
            {
                return;
            }

            if (!airForcers.ContainsKey(target))
            {
                airForcers.Add(target, new List<Script>());
            }

            if(forced)
            {
                if (!airForcers[target].Contains(s))
                {
                    target.gce.ForceOff();
                    airForcers[target].Add(s);
                }
            }
            else
            {
                if (airForcers[target].Contains(s))
                {
                    target.gce.StopForceOff();
                    airForcers[target].Remove(s);
                }
            }
        }
    }
}
