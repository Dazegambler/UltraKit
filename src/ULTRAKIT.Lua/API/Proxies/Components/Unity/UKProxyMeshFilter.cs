using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components.Unity
{
    public class UKProxyMeshFilter : UKProxyComponentAbstract<MeshFilter>
    {
        public UKProxyMeshFilter(MeshFilter target) : base(target)
        {
        }
        public Mesh mesh
        {
            get => target.mesh;
            set => target.mesh = value;
        }
        public Mesh sharedMesh
        {
            get => target.sharedMesh;
            set => target.sharedMesh = value;
        }
    }
}
