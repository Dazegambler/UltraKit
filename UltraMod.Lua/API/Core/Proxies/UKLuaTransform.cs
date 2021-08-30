using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Lua.Attributes;
using UnityEngine;

namespace UltraMod.Lua.API.Proxies.Core
{
    [UKLuaStatic("Transform")]
    class UKLuaTransform : UKLuaProxy<Transform>
    {
        public UKLuaTransform(Transform target) : base(target)
        {
        }

        public Vector3 position
        {
            get => target.position;
            set => target.position = value;
        }
        public Vector3 localPosition
        {
            get => target.localPosition;
            set => target.localPosition = value;
        }

        public Quaternion rotation
        {
            get => target.rotation;
            set => target.rotation = value;
        }
        public Quaternion localRotation
        {
            get => target.localRotation;
            set => target.localRotation = value;
        }


        public Vector3 forward
        {
            get => target.forward;
            set => target.forward = value;
        }

        public GameObject gameObject => target.gameObject;

        public Transform Find(string n) => target.Find(n);
    }
}
