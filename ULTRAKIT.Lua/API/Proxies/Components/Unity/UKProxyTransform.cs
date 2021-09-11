using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components.Unity
{
    class UKProxyTransform : UKProxyComponentAbstract<Transform>
    {
        public UKProxyTransform(Transform target) : base(target)
        {
        }

        public int childCount => target.childCount;

        public Vector3 eulerAngles
        {
            get => target.eulerAngles;
            set => target.eulerAngles = value;
        }
        public Vector3 forward
        {
            get => target.forward;
            set => target.forward = value;
        }
        public bool hasChanged
        {
            get => target.hasChanged;
            set => target.hasChanged = value;
        }
        // hierarchyCapacity, hierarchyCount

        public Vector3 localEulerAngles
        {
            get => target.localEulerAngles;
            set => target.localEulerAngles = value;
        }
        public Vector3 localPosition
        {
            get => target.localPosition;
            set => target.localPosition = value;
        }
        public Vector3 localScale
        {
            get => target.localScale;
            set => target.localScale = value;
        }
        //localToWorldMatrix
        public Vector3 lossyScale => target.lossyScale;
        public Transform parent
        {
            get => target.parent;
            set => target.parent = value;
        }
        public Vector3 position
        {
            get => target.position;
            set => target.position = value;
        }
        public Vector3 right
        {
            get => target.right;
            set => target.right = value;
        }
        public Transform root => target.root;
        public Quaternion rotation
        {
            get => target.rotation;
            set => target.rotation = value;
        }
        public Vector3 up
        {
            get => target.up;
            set => target.up = value;
        }
        //worldToLocalMatrix

        public void DetachChildren() => target.DetachChildren();
        public Transform Find(string name) => target.Find(name);
        public Transform GetChild(int index) => target.GetChild(index);
        public int GetSiblingIndex() => target.GetSiblingIndex();
        public Vector3 InverseTrasnformDirection(Vector3 direction) => target.InverseTransformDirection(direction);
        public Vector3 InverseTransformPoint(Vector3 point) => target.InverseTransformPoint(point);
        public Vector3 InverseTransformVector(Vector3 vector) => target.InverseTransformVector(vector);
        public bool IsChildOf(Transform parent) => target.IsChildOf(parent);
        public void LookAt(Transform lookTarget) => target.LookAt(lookTarget);
        public void LookAt(Transform lookTarget, Vector3 up) => target.LookAt(lookTarget, up);
        public void LookAt(Vector3 lookTarget) => target.LookAt(lookTarget);
        public void LookAt(Vector3 lookTarget, Vector3 up) => target.LookAt(lookTarget, up);
        public void Rotate(Vector3 eulers) => target.Rotate(eulers);
        public void RotateAround(Vector3 axis, float angle) => target.Rotate(axis, angle);
        //SetAsFirstSibling, SetAsLastSibling
        public void SetSiblingIndex(int index) => target.SetSiblingIndex(index);
        public Vector3 TransformDirection(Vector3 direction) => target.TransformDirection(direction);
        public Vector3 TransformPoint(Vector3 point) => target.TransformPoint(point);
        public Vector3 TransformVector(Vector3 vector) => target.TransformVector(vector);
        public void Translate(Vector3 translation) => target.Translate(translation);
    }
}
