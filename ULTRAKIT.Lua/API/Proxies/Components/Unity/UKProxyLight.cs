using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components.Unity
{
    class UKProxyLight : UKProxyComponentAbstract<Light>
    {
        public UKProxyLight(Light target) : base(target)
        {
        }

        #region Properties
        //areaSize, bakingOutput, bounceIntensity, boundingSphereOverride
        public Color color
        {
            get => target.color;
            set => target.color = value;
        }
        public float colorTemperature
        {
            get => target.colorTemperature;
            set => target.colorTemperature = value;
        }
        //commandBufferCount
        //public Texture cookie
        //{
        //    get => target.cookie;
        //    set => target.cookie = value;
        //}
        public float cookieSize
        {
            get => target.cookieSize;
            set => target.cookieSize = value;
        }
        public int cullingMask
        {
            get => target.cullingMask;
            set => target.cullingMask = value;
        }
        //flare

        public float innerSpotAngle
        {
            get => target.innerSpotAngle;
            set => target.innerSpotAngle = value;
        }
        public float intensity
        {
            get => target.intensity;
            set => target.intensity = value;
        }
        //layerShadowCullDistances, lightmapBakeType, lightShadowCasterMode

        public float range
        {
            get => target.range;
            set => target.range = value;
        }
        public int renderingLayerMask
        {
            get => target.renderingLayerMask;
            set => target.renderingLayerMask = value;
        }
        //public LightRenderMode renderMode
        //{
        //    get => target.renderMode;
        //    set => target.renderMode = value;
        //}
        //shadowAngle, shadowBias, shadowCustomResolution, shadowMatrixOverride, shadowNearPlane, shadowNormalBias, shadowRadius, shadowResolution, shadows
        public float shadowStrength
        {
            get => target.shadowStrength;
            set => target.shadowStrength = value;
        }
        //shape
        public float spotAngle
        {
            get => target.spotAngle;
            set => target.spotAngle = value;
        }
        //public LightType type
        //{
        //    get => target.type;
        //    set => target.type = value;
        //}
        //useBoundingSphereOverride
        public bool useColorTemperature
        {
            get => target.useColorTemperature;
            set => target.useColorTemperature = value;
        }
        //useShadowMatrixOverride

        #endregion

        #region Public Methods
        // AddCommandBuffer, AddCommandBufferAsync, GetCommandBuffers, RemoveAllCommandBuffers, RemoveCommandBuffer, RemoveCommandBuffers
        public void Reset() => target.Reset();
        // SetLightDirty
        #endregion
    }
}
