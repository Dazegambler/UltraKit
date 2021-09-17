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
        public Component GetComponent(string typeName) => target.GetComponent(typeName);
        //TODO: GetComponentInChildren, GetComponentInParent, GetComponents, GetComponentsInChildren, GetComponentsInParent
        public void SetActive(bool value) => target.SetActive(value);
        #endregion
    }
}
