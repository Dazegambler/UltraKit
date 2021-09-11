using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    public class UKProxyLineRenderer : UKProxyComponentAbstract<LineRenderer>
    {
        public UKProxyLineRenderer(LineRenderer target) : base(target)
        {
        }
        //COMPONENTS
        public LineAlignment alignment => target.alignment;
        public Gradient colorGradient => target.colorGradient;
        public Color endColor => target.endColor;
        public float endWidth => target.endWidth;
        public bool generateLightingData => target.generateLightingData;
        public bool loop => target.loop;
        public int numCapVertices => target.numCapVertices;
        public int numCornerVertices => target.numCornerVertices;
        public int positionCount => target.positionCount;
        public float shadowBias => target.shadowBias;
        public Color startColor => target.startColor;
        public float startWidth => target.startWidth;
        public LineTextureMode textureMode => target.textureMode;
        public bool useWorldSpace => target.useWorldSpace;
        public AnimationCurve widthCurve => target.widthCurve;
        public float widthMultiplier => target.widthMultiplier;

        //METHODS
        public void BakeMesh(Mesh mesh, bool useTransform) => target.BakeMesh(mesh, useTransform);
        public void BakeMesh(Mesh mesh, Camera camera, bool useTransform) => target.BakeMesh(mesh, camera, useTransform);
        public Vector3 GetPosition(int index) => target.GetPosition(index);
        public int GetPositions(out Vector3[] positions) => target.GetPositions(positions);
        public void SetPosition(int index, Vector3 position) => target.SetPosition(index,position);
        public void SetPositions(Vector3[] positions) => target.SetPositions(positions);
        public void Simplify(float tolerance) => target.Simplify(tolerance);
    }
}
