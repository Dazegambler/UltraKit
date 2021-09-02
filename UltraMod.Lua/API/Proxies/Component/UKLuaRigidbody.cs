using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Component
{
    public class UKLuaRigidbody : UKLuaComponent<Rigidbody>
    {
        public Vector3 position
        {
            get => target.position;
            set => target.position = value;
        }
        public Vector3 velocity
        {
            get => target.velocity;
            set => target.velocity = value;
        }


        public bool isKinematic
        {
            get => target.isKinematic;
            set => target.isKinematic = value;
        }

        public UKLuaRigidbody(Rigidbody target) : base(target)
        {
        }
    }
}
