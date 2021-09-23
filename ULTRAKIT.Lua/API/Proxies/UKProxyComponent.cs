using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MoonSharp.Interpreter;

namespace ULTRAKIT.Lua.API.Proxies
{
    public abstract class UKProxyComponentAbstract<T> : UKProxyObjectAbstract<T>
        where T : Component
    {
        public UKProxyComponentAbstract(T target) : base(target)
        {
        }

        #region Properties
        public GameObject gameObject => target.gameObject;
        public string tag => target.tag;
        public Transform transform => target.transform;
        #endregion

        #region Public Methods
        // BroadcastMessage
        public bool CompareTag(string tag) => target.CompareTag(tag);
        public Component GetComponent(string typeName) => target.GetComponent(typeName);
        public Component GetComponentInParent(string typeName) => target.GetComponentsInParent<Component>()?.Where(t => t.GetType().Name == typeName)?.FirstOrDefault();
        public Component GetComponentInChildren(string typeName) => target.GetComponentsInChildren<Component>()?.Where(t => t.GetType().Name == typeName)?.FirstOrDefault();

        // Returns (Component, bool)
        public DynValue TryGetComponent(string typeName) {
            var component = target.GetComponents<Component>()?.Where(t => t.GetType().Name == typeName)?.FirstOrDefault();
            var success = component != null;

            if (component == null)
                return DynValue.NewTuple(DynValue.Nil, DynValue.NewBoolean(success));
            else
                return DynValue.NewTuple(UserData.Create(component), DynValue.NewBoolean(success));
        }
        public Component[] GetComponents(string typeName) => target.GetComponents<Component>()?.Where(t => t.GetType().Name == typeName).ToArray();
        public Component[] GetComponentsInChildren(string typeName) => target.GetComponentsInChildren<Component>()?.Where(t => t.GetType().Name == typeName).ToArray();
        public Component[] GetComponentsInParent(string typeName) => target.GetComponentsInChildren<Component>()?.Where(t => t.GetType().Name == typeName).ToArray();
        //SendMessage, SendMessageUpwards
        #endregion
    }

    public class UKProxyComponent : UKProxyComponentAbstract<Component>
    {
        public UKProxyComponent(Component target) : base(target)
        {
        }
    }
}
