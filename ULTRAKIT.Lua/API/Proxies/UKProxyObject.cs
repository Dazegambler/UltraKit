using ULTRAKIT.Lua.Attributes;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies
{
    public abstract class UKProxyObjectAbstract<T> : UKProxy<T>
        where T : Object
    {
        public UKProxyObjectAbstract(T target) : base(target)
        {
        }

        #region Properties
        public string name => target.name;
        #endregion

        #region Instance Methods
        public int GetInstanceID() => target.GetInstanceID();
        public new string ToString() => target.ToString();
        #endregion
    }
    public class UKProxyObject : UKProxyObjectAbstract<Object>
    {
        public UKProxyObject(Object target) : base(target)
        {
        }
    }
}
