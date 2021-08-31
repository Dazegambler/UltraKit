using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraMod.Lua.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UKScriptConstructor : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class UKScriptDestructor : Attribute
    {
    }
}
