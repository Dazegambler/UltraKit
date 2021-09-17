using ULTRAKIT.Lua.API.Abstract;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Statics
{
    public class UKStaticTime : UKStatic
    {
        public override string name => "Time";

        public float captureDeltaTime
		{
            get => Time.captureDeltaTime;
            set => Time.captureDeltaTime = value;
		}
        public int captureFramerate
        {
            get => Time.captureFramerate;
            set => Time.captureFramerate = value;
        }
        public float deltaTime
        {
            get => Time.deltaTime;
        }
        public float fixedTime
        {
            get => Time.fixedTime;
        }
        public float fixedUnscaledDeltaTime
        {
            get => Time.fixedUnscaledDeltaTime;
        }
        public float frameCount
        {
            get => Time.frameCount;
        }
        public bool inFixedTimeStep
        {
            get => Time.inFixedTimeStep;
        }
        public float maximumDeltaTime
        {
            get => Time.maximumDeltaTime;
            set => Time.maximumDeltaTime = value;
        }
        public float maximumParticleDeltaTime
        {
            get => Time.maximumParticleDeltaTime;
            set => Time.maximumParticleDeltaTime = value;

        }
        public float realtimeSinceStartup
        {
            get => Time.realtimeSinceStartup;
        }
        public float smoothDeltaTime
        {
            get => Time.smoothDeltaTime;
        }
        public float time
        {
            get => Time.time;
        }
        public float timeScale
        {
            get => Time.timeScale;
            set => Time.timeScale = value;
        }
        public float timeSinceLevelLoad
        {
            get => Time.timeSinceLevelLoad;
        }
        public float unscaledDeltaTime
        {
            get => Time.unscaledDeltaTime;
        }
        public float unscaledTime
        {
            get => Time.unscaledTime;
        }
    }
}
