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
    public class UKLuaLineRenderer : UKLuaComponent<LineRenderer>
    {
        [MoonSharpHidden]
        public UKLuaLineRenderer(LineRenderer target) : base(target)
        {
        }

        public bool enabled
        {
            get => target.enabled;
            set => target.enabled = value;
        }
        public float startWidth
        {
            get => target.startWidth;
            set => target.startWidth = value;
        }

        public int positionCount
        {
            get => target.positionCount;
            set => target.positionCount = value;
        }
        public void SetPosition(Script s, int index, Vector3 pos) => target.SetPosition(index, pos);
    }
}
