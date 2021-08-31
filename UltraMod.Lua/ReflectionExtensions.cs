using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UltraMod.Lua
{
    public static class ReflectionExtensions
    {
        public static void SetPrivate<T>(this T obj, string name, object value)
        {
            typeof(T).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(obj, value);
        }

        public static object GetPrivate<T>(this T obj, string name)
        {
            return typeof(T).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj);
        }
    }
}
