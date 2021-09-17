using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    class UKProxyLineRenderer : UKProxyComponentAbstract<LineRenderer>
    {
        public UKProxyLineRenderer(LineRenderer target) : base(target)
        {
        }
        //COMPONENTS
        //public LineAlignment alignment => target.alignment;
        //public Gradient colorGradient => target.colorGradient;

        public Color endColor
        {
            get => target.endColor;
            set => target.endColor = value;
        }
        public float endWidth
        {
            get => target.endWidth;
            set => target.endWidth = value;
        }
        public bool generateLightingData
        {
            get => target.generateLightingData;
            set => target.generateLightingData = value;
        }
        public bool loop {
            get => target.loop;
            set => target.loop = value;
        }
        public int numCapVertices { 
            get => target.numCapVertices;
            set => target.numCapVertices = value;
        }
        public int numCornerVertices
        {
            get => target.numCornerVertices;
            set => target.numCornerVertices = value;
        }
        public int positionCount
        {
            get => target.positionCount;
            set => target.positionCount = value;
        }
        public float shadowBias
        {
            get => target.shadowBias;
            set => target.shadowBias = value;
        }
        public Color startColor
        {
            get => target.startColor;
            set => target.startColor = value;
        }
        public float startWidth
        {
            get => target.startWidth;
            set => target.startWidth = value;
        }
        //public LineTextureMode textureMode => target.textureMode;
        public bool useWorldSpace
        {
            get => target.useWorldSpace;
            set => target.useWorldSpace = value;
        }
        //public AnimationCurve widthCurve => target.widthCurve;
        public float widthMultiplier {
            get => target.widthMultiplier;
            set => target.widthMultiplier = value;
        }

        //METHODS
        public void BakeMesh(Mesh mesh, bool useTransform) => target.BakeMesh(mesh, useTransform);
        public void BakeMesh(Mesh mesh, Camera camera, bool useTransform) => target.BakeMesh(mesh, camera, useTransform);
        public Vector3 GetPosition(int index) => target.GetPosition(index);
        public int GetPositions(Vector3[] positions)
        {
            var res = target.GetPositions(positions);
            return res;
        }
        public void SetPosition(int index, Vector3 position) => target.SetPosition(index,position);
        public void SetPositions(Vector3[] positions) => target.SetPositions(positions);
        public void Simplify(float tolerance) => target.Simplify(tolerance);
    }
}
