using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies
{
    class UKProxyComponentAbstract<T> : UKProxyObjectAbstract<T>
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
        //TODO: GetComponentInChildren, GetComponentInParent, GetComponents, GetComponentsInChildren, GetComponentsInParent
        //SendMessage, SendMessageUpwards
        //TryGetComponent
        #endregion


    }
}
