using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Lua.API.Proxies.Component;
using UltraMod.Lua.Attributes;
using UltraMod.Lua.Components;
using UnityEngine;

namespace UltraMod.Lua.API.Proxies
{
    [UKLuaStatic("GameObject")]
    public class UKLuaGameObject : UKLuaProxy<GameObject>
    {
        //TODO: move this to the api class use attribute to register them to a table AND here
        public static readonly Dictionary<ComponentType, Type> ComponentDict = new Dictionary<ComponentType, Type>()
        {
            { ComponentType.Rigidbody, typeof(Rigidbody) },
            { ComponentType.Enemy, typeof(EnemyIdentifier) },
            { ComponentType.LineRenderer, typeof(LineRenderer) },
            { ComponentType.AudioSource, typeof(AudioSource) },
            { ComponentType.Projectile, typeof(Projectile) }
        };

        public UKLuaGameObject(GameObject target) : base(target)
        {
        }

        public string name => target.name;
        public Transform transform => target.transform;
        

        public void SetActive(bool b) => target.SetActive(b);

        public DynValue GetComponent(Script s, ComponentType type)
        {
            return DynValue.FromObject(s, target.GetComponent(ComponentDict[type]));
        }

        public DynValue GetComponentInTree(Script s, ComponentType type)
        {
            return DynValue.FromObject(s, FindComponentAllTree(ComponentDict[type]));
        }

        public DynValue GetComponentInParent(Script s, ComponentType type)
        {
            return DynValue.FromObject(s, target.GetComponentInParent(ComponentDict[type]));
        }

        public DynValue GetComponentInChildren(Script s, ComponentType type)
        {
            return DynValue.FromObject(s, target.GetComponentInChildren(ComponentDict[type]));
        }

        public DynValue AddComponent(Script s, ComponentType type)
        {
            return DynValue.FromObject(s, target.AddComponent(ComponentDict[type]));
        }

        object FindComponentAllTree(Type t)
        {
            return target.GetComponent(t) ?? target.GetComponentInChildren(t) ?? target.GetComponentInParent(t) ?? null;
        }

        public static GameObject Instantiate(Script s, GameObject g)
        {

            var newG = GameObject.Instantiate(g);
            return newG;
        }
        public static GameObject Instantiate(Script s, GameObject g, Transform parent)
        {
            var newG = Instantiate(s, g);
            newG.transform.parent = parent;
            return newG;
        }

    }
}
