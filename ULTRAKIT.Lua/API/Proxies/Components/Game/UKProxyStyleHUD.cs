using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    public class UKProxyStyleHUD : UKProxyComponentAbstract<StyleHUD>
    {
        [MoonSharpHidden]
        public UKProxyStyleHUD(StyleHUD target) : base(target)
        {
        }

        public void AddPoints(int points, string pointName) => target.AddPoints(points, pointName);
        public void RemovePoints(int points) => target.RemovePoints(points);

        // I don't know if these should be exposed...
        public void DescendRank() => target.DescendRank();
        public void ComboStart() => target.ComboStart();
        public void ComboOver() => target.ComboOver();
    }
}
