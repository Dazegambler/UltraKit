using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Lua.API.Abstract;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Statics
{
    public class UKStaticPlayer : UKStatic
    {
        public override string name => "Player";

        public Transform head => MonoSingleton<CameraController>.Instance.transform;
        public Transform body => MonoSingleton<NewMovement>.Instance.transform;
        public Rigidbody rigidbody => MonoSingleton<NewMovement>.Instance.rb;
        public CameraController camera => MonoSingleton<CameraController>.Instance;
        public StyleHUD styleHUD => MonoSingleton<StyleHUD>.Instance;

        public bool canSwitchWeapon
        {
            get => MonoSingleton<GunControl>.Instance.enabled;
            set => MonoSingleton<GunControl>.Instance.enabled = value;
        }
        
        public void CameraShake(float amount) => MonoSingleton<CameraController>.Instance.CameraShake(amount);
    }
}
