using System.Linq;
using MoonSharp.Interpreter;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies
{
    public class UKProxyGameObject : UKProxyObjectAbstract<GameObject>
    {
        public UKProxyGameObject(GameObject target) : base(target)
        {
        }

        #region Properties
        public bool activeInHierarchy => target.activeInHierarchy;
        public bool activeSelf => target.activeSelf;
        public bool isStatic => target.isStatic;
        public int layer
        {
            get => target.layer;
            set => target.layer = value;
        }

        public string tag
        {
            get => target.tag;
            set => target.tag = value;
        }
        public Transform transform => target.transform;
        #endregion

        #region Instance Methods
        //TODO: AddComponent
        public bool CompareTag(string tag) => target.CompareTag(tag);
        public void SetActive(bool value) => target.SetActive(value);

        // Copied from UKProxyComponent.cs

        public Projectile AddProjComponent() => target.AddComponent<Projectile>();
        public FloatingPointErrorPreventer AddFloatingPointErrorPreventer() => target.AddComponent<FloatingPointErrorPreventer>();

        public Component GetComponent(string typeName) => target.GetComponent(typeName);
        public Component GetComponentInParent(string typeName) => target.GetComponentsInParent<Component>()?.Where(t => t.GetType().Name == typeName)?.FirstOrDefault();
        public Component GetComponentInChildren(string typeName) => target.GetComponentsInChildren<Component>()?.Where(t => t.GetType().Name == typeName)?.FirstOrDefault();
        public Component[] GetComponents(string typeName) => target.GetComponents<Component>()?.Where(t => t.GetType().Name == typeName).ToArray();
        public Component[] GetComponentsInChildren(string typeName) => target.GetComponentsInChildren<Component>()?.Where(t => t.GetType().Name == typeName).ToArray();
        public Component[] GetComponentsInParent(string typeName) => target.GetComponentsInChildren<Component>()?.Where(t => t.GetType().Name == typeName).ToArray();
        public DynValue TryGetComponent(string typeName) {
            var component = target.GetComponents<Component>()?.Where(t => t.GetType().Name == typeName)?.FirstOrDefault();
            var success = component != null;

            if (component == null)
                return DynValue.NewTuple(DynValue.Nil, DynValue.NewBoolean(success));
            else
                return DynValue.NewTuple(UserData.Create(component), DynValue.NewBoolean(success));
        }
        #endregion
    }
}
