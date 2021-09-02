using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Component
{
    [MoonSharpUserData]
    public class UKLuaAudioSource : UKLuaProxy<AudioSource>
    {
        public UKLuaAudioSource(AudioSource target) : base(target)
        {
        }

        public void Play() => target.Play();

        public bool enabled
        {
            get => target.enabled;
            set => target.enabled = value;
        }
        public float pitch
        {
            get => target.pitch;
            set => target.pitch = value;
        }
        public float volume
        {
            get => target.volume;
            set => target.volume = value;
        }
    }
}
