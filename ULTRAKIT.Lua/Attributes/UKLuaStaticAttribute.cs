using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRAKIT.Lua.Attributes
{
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
