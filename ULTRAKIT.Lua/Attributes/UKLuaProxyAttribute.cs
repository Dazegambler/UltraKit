using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRAKIT.Lua.Attributes
{
    /// <summary>
    /// Used for objects that represent a c# object in lua, but with limited access.
    /// </summary>
    [Obsolete]
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

    /// <summary>
    /// Binds a field to the value of the target in a proxy
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class UKProxyTargetMatcher : Attribute
    {
    }
}
