using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Lua.Attributes;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Game
{
    [UKLuaStatic("Player")]
    public static class UKLuaPlayer
    {
        public static Rigidbody rigidbody => MonoSingleton<NewMovement>.Instance.rb;
        public static GameObject camera => MonoSingleton<CameraController>.Instance.gameObject;

        public static bool canSwitchWeapon
        {
            get => MonoSingleton<GunControl>.Instance.enabled;

            set => MonoSingleton<GunControl>.Instance.enabled = value;
        }
    }
}
