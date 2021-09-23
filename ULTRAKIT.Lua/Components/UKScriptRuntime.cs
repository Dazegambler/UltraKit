using System.Collections;
using MoonSharp.Interpreter;
using ULTRAKIT.Data;
using ULTRAKIT.Data.Components;
using ULTRAKIT.Lua.API;
using UnityEngine;
using System.Collections.Generic;

namespace ULTRAKIT.Lua.Components
{
    public class UKScriptRuntime : MonoBehaviour
    {
        public static List<UKScriptRuntime> Instances = new List<UKScriptRuntime>();

        public UKScript data;
        public Script runtime;
        public UKAddonData addon;
        public bool callUpdateWhilePaused;
        public bool initialized;

        public static void Create(UKAddonData addon, UKScript orig, bool callUpdateWhilePaused)
        {
            var r = orig.gameObject.AddComponent<UKScriptRuntime>();
            r.addon = addon;
            r.callUpdateWhilePaused = callUpdateWhilePaused;
            r.data = r.GetComponent<UKScript>();
            r.runtime = new Script(CoreModules.Preset_SoftSandbox);

            r.initialized = true;
            r.enabled = true;
            r.Awake();
            r.OnEnable();
        }

        public static void Create(UKAddonData data, GameObject go, bool callUpdateWhilePaused = false)
        {
            foreach (var script in go.GetComponentsInChildren<UKScript>(true))
            {
                Create(data, script, callUpdateWhilePaused);
            }
        }

        public void FuzzyCall(Table t, string name, params object[] luap)
        {
            try
            {
                if (t?.Get(name)?.Function != null)
                {
                    t.Get(name).Function.Call(luap);
                }
                else
                {
                    //TODO: proper logging
                }
            }
            catch (ScriptRuntimeException e)
            {
                //TODO: propper logging
                Debug.LogError($"(ULTRAKIT Lua) RUNTIME ERROR: {data.sourceCode.name} - {e.DecoratedMessage}");
            }
        }

        public void FuzzyCall(DynValue d, params object[] luap)
        {
            if (d.Function != null)
            {
                try
                {
                    runtime.Call(d, luap);
                }
                catch (ScriptRuntimeException e)
                {
                    //TODO: propper logging
                    Debug.LogError($"(ULTRAKIT Lua) RUNTIME ERROR: {data.sourceCode.name} - {e.DecoratedMessage}");
                }
            }
            else
            {
                //TODO: proper logging
            }
        }

        void Awake()
        {
            if (initialized == false) return;

            Instances.Add(this);
            try
            {
                var func = runtime.LoadString(data.sourceCode.text);
                UKLuaAPI.ConstructScript(this);
                FuzzyCall(func);
            }
            catch (SyntaxErrorException e)
            {
                //TODO: propper logging
                Debug.LogError($"(ULTRAKIT Lua) {data.sourceCode.name} - SYNTAX ERROR: {e.DecoratedMessage}");
                this.enabled = false;
            }
        }

        void Start()
        {
            FuzzyCall(runtime.Globals, "Start");
        }

        void OnDestroy()
        {
            Instances.Remove(this);
            UKLuaAPI.DestructScript(this);
        }

        //Script Callbacks
        void OnEnable()
        {
            if (!initialized) return;
            FuzzyCall(runtime.Globals, "OnEnable");
        }

        void OnCollisionEnter(Collision other)
        {
            FuzzyCall(runtime.Globals, "OnCollisionEnter", other);
        }

        void OnCollisionStay(Collision other)
        {
            FuzzyCall(runtime.Globals, "OnCollisionStay", other);
        }

        void OnCollisionExit(Collision other)
        {
            FuzzyCall(runtime.Globals, "OnCollisionExit", other);
        }

        void OnTriggerEnter(Collider other)
        {
            FuzzyCall(runtime.Globals, "OnTriggerEnter", other);
        }

        void OnTriggerStay(Collider other)
        {
            FuzzyCall(runtime.Globals, "OnTriggerStay", other);
        }

        void OnTriggerExit(Collider other)
        {
            FuzzyCall(runtime.Globals, "OnTriggerExit", other);
        }

        void Update()
        {
            if (!initialized) return;

            UKLuaAPI.UpdateScript(this);

            if (callUpdateWhilePaused || !MonoSingleton<OptionsManager>.Instance.paused)
            {
                FuzzyCall(runtime.Globals, "Update", Time.deltaTime);
            }
        }

        void LateUpdate()
        {
            if (!initialized) return;

            if (callUpdateWhilePaused || !MonoSingleton<OptionsManager>.Instance.paused)
                FuzzyCall(runtime.Globals, "LateUpdate", Time.deltaTime);
        }

        void FixedUpdate()
        {
            if (!initialized) return;

            if (callUpdateWhilePaused || !MonoSingleton<OptionsManager>.Instance.paused)
                FuzzyCall(runtime.Globals, "FixedUpdate");
        }

        void OnDisable()
        {
            FuzzyCall(runtime.Globals, "OnDisable");
        }
    }
}