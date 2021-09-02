using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRAKIT.Lua.API.Proxies
{
    public abstract class UKLuaProxy<T>
    {
        protected T target;

        protected UKLuaProxy(T target)
        {
            this.target = target;
        }
    }
}
