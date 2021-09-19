using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    class UKProxyAudioSource : UKProxyComponentAbstract<AudioSource>
    {
        public UKProxyAudioSource(AudioSource target) : base(target)
        {
        }

        public bool bypassEffects
        {
            get => target.bypassEffects;
            set => target.bypassEffects = value;
        }
        public bool bypassListenerEffects
        {
            get => target.bypassListenerEffects;
            set => target.bypassListenerEffects = value;
        }
        public bool bypassReverbZones
        {
            get => target.bypassReverbZones;
            set => target.bypassReverbZones = value;
        }
        //public AudioClip clip
        //{
        //    get => target.clip;
        //    set => target.clip = value;
        //}
        public float dopplerLevel
        {
            get => target.dopplerLevel;
            set => target.dopplerLevel = value;
        }
        //gamepadSpeakerOutputType

        public bool ignoreListenerPause
        {
            get => target.ignoreListenerPause;
            set => target.ignoreListenerPause = value;
        }
        public bool ignoreListenerVolume
        {
            get => target.ignoreListenerVolume;
            set => target.ignoreListenerVolume = value;
        }
        public bool isPlaying => target.isPlaying;
        public bool isVirtual => target.isVirtual;

        public bool loop
        {
            get => target.loop;
            set => target.loop = value;
        }
        public float maxDistance
        {
            get => target.maxDistance;
            set => target.maxDistance = value;
        }
        public float minDistance
        {
            get => target.minDistance;
            set => target.minDistance = value;
        }
        public bool mute
        {
            get => target.mute;
            set => target.mute = value;
        }
        //outputAudioMixerGroup

        public float panStereo
        {
            get => target.panStereo;
            set => target.panStereo = value;
        }
        public float pitch
        {
            get => target.pitch;
            set => target.pitch = value;
        }
        public bool playOnAwake
        {
            get => target.playOnAwake;
            set => target.playOnAwake = value;
        }
        public int priority
        {
            get => target.priority;
            set => target.priority = value;
        }
        public float reverbZoneMix
        {
            get => target.reverbZoneMix;
            set => target.reverbZoneMix = value;
        }
        //public AudioRolloffMode rolloffMode
        //{
        //    get => target.rolloffMode;
        //    set => target.rolloffMode = value;
        //}

        public float spatialBlend
        {
            get => target.spatialBlend;
            set => target.spatialBlend = value;
        }
        public bool spatialize
        {
            get => target.spatialize;
            set => target.spatialize = value;
        }
        public bool spatializePostEffects
        {
            get => target.spatializePostEffects;
            set => target.spatializePostEffects = value;
        }
        public float spread
        {
            get => target.spread;
            set => target.spread = value;
        }
        public float time
        {
            get => target.time;
            set => target.time = value;
        }
        public int timeSamples
        {
            get => target.timeSamples;
            set => target.timeSamples = value;
        }
        //velocityUpdateMode

        public float volume
        {
            get => target.volume;
            set => target.volume = value;
        }

        //DisableGamepadOutput, GetAmbisonicDecoderFloat
        //public AnimationCurve GetCustomCurve(AudioSourceCurveType type) => target.GetCustomCurve(type);
        //GetOutputData, GetSpatializerFloat, GetSpectrumData
        public void Pause() => target.Pause();
        public void Play() => target.Play();
        public void PlayDelayed(float secs) => target.PlayDelayed(secs);
        //public void PlayOneShot(AudioClip clip) => target.PlayOneShot(clip);
        //PlayOnGamepad, PlayScheduled, SetAmbisonicDecoderFloat
        //public void SetCustomCurve(AudioSourceCurveType type, AnimationCurve curve) => target.SetCustomCurve(type, curve);
        //SetScheduledEndTime, SetScheduledStartTime, SetSpatializerFloat
        public void Stop() => target.Stop();
        public void UnPause() => target.UnPause();
    }
}
