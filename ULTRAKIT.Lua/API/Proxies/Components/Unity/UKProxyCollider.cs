using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Lua.API.Statics;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components.Unity
{
    public class UKProxyCollider : UKProxyComponentAbstract<Collider>
    {
        // attachedArticulationBody

        [MoonSharpHidden]
        public UKProxyCollider(Collider target) : base(target)
        {
        }

        #region Properties
        public Rigidbody attachedRigidbody => target.attachedRigidbody;
        public Bounds bounds => target.bounds;
        public float contactOffset
        {
            get => target.contactOffset;
            set => target.contactOffset = value;
        }

        public bool enabled
        {
            get => target.enabled;
            set => target.enabled = value;
        }
        public bool isTrigger
        {
            get => target.isTrigger;
            set => target.isTrigger = value;
        }
        // material, sharedMaterial
        #endregion

        #region Instance Methods
        public Vector3 ClosestPoint(Vector3 point) => target.ClosestPoint(point);
        public Vector3 ClosestPointOnBounds(Vector3 point) => target.ClosestPointOnBounds(point);
        //TODO: raycast
        #endregion
    }
}
