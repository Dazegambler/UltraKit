using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Lua.API.Abstract;
using ULTRAKIT.Lua.API.Proxies.Components;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Statics
{
    [MoonSharpUserData]
    public class UKHitResult
    {
        public Vector3 point;
        public Vector3 normal;
        public UKProxyEnemy enemy;
        public UKProxyProjectile projectile;
        public Transform transform;

        public UKHitResult(Vector3 point, Vector3 normal, Transform transform, UKProxyEnemy enemy, UKProxyProjectile projectile)
        {
            this.point = point;
            this.normal = normal;
            this.enemy = enemy;
            this.projectile = projectile;
            this.transform = transform;
        }

        public UKHitResult(UKHitResult other)
        {
            this.point = other.point;
            this.normal = other.normal;
            this.transform = other.transform;
            this.enemy = other.enemy;
            this.projectile = other.projectile;
        }
    }

    public class UKStaticPhysics : UKStatic
    {
        public override string name => "Physics";
        static readonly int DefaultCastMask = (1 << LayerMask.NameToLayer("EnemyTrigger")) | (1 << LayerMask.NameToLayer("Projectile"));

        public UKHitResult Raycast(Vector3 point, Vector3 dir, float maxDistance)
        {
            RaycastHit hit;
            if (Physics.Raycast(point, dir, out hit, maxDistance, DefaultCastMask))
            {
                var res = new UKHitResult(
                    hit.point, hit.normal, hit.transform,
                    new UKProxyEnemy(hit.transform.GetComponentInChildren<EnemyIdentifier>()), 
                    new UKProxyProjectile(hit.transform.GetComponentInChildren<Projectile>()));
                return res;
            } else if(Physics.Raycast(point, dir, out hit))
            {
                var res = new UKHitResult(
                    hit.point, hit.normal, hit.transform,
                    null,
                    null
                );

                return res;
            }

            return null;
        }
        public UKHitResult Raycast(Vector3 point, Vector3 dir) => Raycast(point, dir, Mathf.Infinity);

        public UKHitResult Linecast(Vector3 start, Vector3 end)
        {
            var diff = end - start;
            return Raycast(start, diff.normalized, diff.magnitude);
        }
    }
}
