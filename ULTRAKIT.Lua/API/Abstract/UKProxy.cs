using MoonSharp.Interpreter;
using System.Collections.Generic;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies
{
    public abstract class UKProxy<T>
        where T : class
    {
        protected T target;

        public UKProxy(T target)
        {
            this.target = target;
        }
    }
}
