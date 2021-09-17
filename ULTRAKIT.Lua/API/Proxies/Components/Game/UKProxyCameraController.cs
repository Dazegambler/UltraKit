using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    public class UKProxyCameraController : UKProxyComponentAbstract<CameraController>
    {
        [MoonSharpHidden]
        public UKProxyCameraController(CameraController target) : base(target)
        {
        }

        public void CameraShake(float amount) => target.CameraShake(amount);
        public void SlowDown(float amount) => target.SlowDown(amount);
        public void Zoom(float amount) => target.Zoom(amount);
        public void StopZoom() => target.StopZoom();
        public void StopShake() => target.StopShake();
        public void ResetCamera(float degrees) => target.ResetCamera(degrees, true);
        public void TrueStop(float length) => target.TrueStop(length);
    }
}
