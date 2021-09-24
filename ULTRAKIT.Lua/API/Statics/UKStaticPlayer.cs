using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoonSharp.Interpreter;
using ULTRAKIT.Lua.API.Abstract;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Statics
{
    // Sorry for all the null checks
    // But the Lua...
    // It needs to know.
    public class UKStaticPlayer : UKStatic
    {
        public override string name => "Player";

        public override bool Equals(object obj)
        {
            var playerExists = MonoSingleton<NewMovement>.Instance != null;
            if (obj is bool b) return playerExists == b;
            if (obj is null) return !playerExists;
            return base.Equals(obj);
        }

        public override int GetHashCode() => base.GetHashCode();

        public bool exists => MonoSingleton<NewMovement>.Instance != null;
        public Transform head => MonoSingleton<CameraController>.Instance?.transform;
        public Transform body => MonoSingleton<NewMovement>.Instance?.transform;
        public Rigidbody rigidbody => MonoSingleton<NewMovement>.Instance?.rb;
        public GameObject crosshair => MonoSingleton<StatsManager>.Instance?.crosshair;
        public CameraController camera => MonoSingleton<CameraController>.Instance;
        public NewMovement movement => MonoSingleton<NewMovement>.Instance;
        public StyleHUD styleHUD => MonoSingleton<StyleHUD>.Instance;
        public ScanningStuff scanningStuff => MonoSingleton<ScanningStuff>.Instance;

        public bool canSwitchWeapon
        {
            get => MonoSingleton<GunControl>.Instance != null && MonoSingleton<GunControl>.Instance.enabled;
            set
            {
                if (MonoSingleton<GunControl>.Instance != null) 
                    MonoSingleton<GunControl>.Instance.enabled = value;
            }
        }

        public void CameraShake(float amount) => MonoSingleton<CameraController>.Instance?.CameraShake(amount);
    }
}
