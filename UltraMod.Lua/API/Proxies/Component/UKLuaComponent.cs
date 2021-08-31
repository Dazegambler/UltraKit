using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Lua.Attributes;
using UnityEngine;

namespace UltraMod.Lua.API.Proxies.Component
{

    //TODO: autogenerate static as table on script construction, with dictionary matching static values to type i guess
    //TODO: guess that means its time for another attribute
    [UKLuaStatic("ComponentType")]
    public enum ComponentType
    {
        Rigidbody,
        LineRenderer,
        Enemy,
        AudioSource,
        Projectile
    }

    public class UKLuaComponent<T> : UKLuaProxy<T>
        where T : UnityEngine.Component
    {
        public GameObject gameObject => target.gameObject;
        public Transform transform => target.transform;
        
        public UKLuaComponent(T target) : base(target)
        {
        }
    }
}
