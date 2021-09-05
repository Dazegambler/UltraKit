using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRAKIT.Lua.API.Proxies
{
    public class UKProxy<T>
    {
        T target;
        public UKProxy(T target)
        {
            this.target = target;
        }
    }

    public class UKComponentProxy<T> : UKProxy<T>
    {
        public UKComponentProxy(T target) : base(target)
        {
        }
    }
}
