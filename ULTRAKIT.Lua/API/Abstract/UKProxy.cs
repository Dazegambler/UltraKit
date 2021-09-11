using MoonSharp.Interpreter;

namespace ULTRAKIT.Lua.API.Proxies
{
    public abstract class UKProxy<T>
    {
        protected T target;

        public UKProxy(T target)
        {
            this.target = target;
        }

        [MoonSharpUserDataMetamethod("__eq")]
        public static bool MoonSharpEquals(UKProxy<T> o, object v)
        {
            return (o.target as object) == v;
        }
    }
}
