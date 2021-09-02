using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRAKIT.Lua.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UKLuaProxyAttribute : Attribute
    {
        public Type target;
        public Func<object, object> CreateProxy;

        public UKLuaProxyAttribute(Type target, Func<object, object> CreateProxy)
        {
            this.target = target;
            this.CreateProxy = CreateProxy;
        }
    }
}
