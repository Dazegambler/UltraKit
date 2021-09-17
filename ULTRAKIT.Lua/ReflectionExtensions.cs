using System.Reflection;

namespace ULTRAKIT.Lua
{
    public static class ReflectionExtensions
    {
        public static void SetPrivate(this object obj, string name, object value)
        {
            obj.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(obj, value);
        }

        public static T GetPrivate<T>(this object obj, string name)
        {
            return (T)obj.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj);
        }
    }
}
