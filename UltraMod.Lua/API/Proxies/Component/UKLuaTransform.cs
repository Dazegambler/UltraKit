using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Lua.Attributes;
using UnityEngine;

namespace UltraMod.Lua.API.Proxies.Component
{
    [UKLuaStatic("Transform")]
    class UKLuaTransform : UKLuaComponent<Transform>
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


        public Vector3 localScale
        {
            get => target.localScale;
            set => target.localScale = value;
        }

        public Vector3 forward
        {
            get => target.forward;
            set => target.forward = value;
        }
        public Vector3 up
        {
            get => target.up;
            set => target.up = value;
        }

        public Transform Find(string n) => target.Find(n);
    }
}
