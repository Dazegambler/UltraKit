using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraMod.Lua.API.Math
{
    [MoonSharpUserData]
    public class UKLuaVector3
    {
        [MoonSharpHidden]
        public Vector3 target;

        [MoonSharpHidden]
        public UKLuaVector3(ref Vector3 target)
        {
            this.target = target;
        }

        public float x
        {
            get => target.x;
            set => target.x = value;
        }
        public float y
        {
            get => target.y;
            set => target.y = value;
        }
        public float z
        {
            get => target.z;
            set => target.z = value;
        }
    }
}
