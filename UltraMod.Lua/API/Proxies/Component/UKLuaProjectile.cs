using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraMod.Lua.API.Proxies.Component
{
    [MoonSharpUserData]
    class UKLuaProjectile : UKLuaComponent<Projectile>
    {
        public UKLuaProjectile(Projectile target) : base(target)
        {
        }

        public Rigidbody rigidbody => target.GetComponent<Rigidbody>();

        public bool friendlyFire
        {
            get => target.friendly;
            set => target.friendly = value;
        }

        public float speed
        {
            get => target.speed;
            set => target.speed = value;
        }

    }
}
