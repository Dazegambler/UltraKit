using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    class UKProxyParticleSystem : UKProxyComponentAbstract<ParticleSystem>
    {
        private ParticleSystem.EmissionModule emissionModule;
       
        public UKProxyParticleSystem(ParticleSystem target) : base(target)
        {
            emissionModule = target.emission;
        }

        //COMPONENTS
        public float time
        {
            get => target.time;
        }
        public bool isPlaying
        {
            get => target.isPlaying;
        }
        public bool isEmitting
        {
            get => target.isEmitting;
        }
        public bool isPaused
        {
            get => target.isPaused;
        }
        public ParticleSystem.EmissionModule emission
		{
            get => target.emission;
		}

        //METHODS
        public void Play() => target.Play();
        public void Pause() => target.Pause();
        public void Stop() => target.Stop();
        public void Emit(int count) => target.Emit(count);
        public void Clear() => target.Clear();

        public void Clear(bool withChildren = true) => target.Clear(withChildren);
        public void Simulate(float t) => target.Simulate(t);
        public void Simulate(float t, bool withChildren = true, bool restart = true) => target.Simulate(t, withChildren, restart);
        public void Simulate(float t, bool withChildren = true, bool restart = true, bool fixedTimeStep = true) => target.Simulate(t, withChildren, restart, fixedTimeStep);
        public bool IsAlive() => target.IsAlive();
        public void TriggerSubEmitter(int subEmitterIndex) => target.TriggerSubEmitter(subEmitterIndex);
    }
}
