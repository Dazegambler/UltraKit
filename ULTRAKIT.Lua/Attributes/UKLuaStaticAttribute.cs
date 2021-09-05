using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRAKIT.Lua.Attributes
{
    /// <summary>
    /// Used for objects that are treated as statics in lua, accessed globally with their name (without an instance).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public class UKLuaStaticAttribute : Attribute
    {
        public string luaName;

        public UKLuaStaticAttribute(string luaName)
        {
            this.luaName = luaName;
        }
    }
}
