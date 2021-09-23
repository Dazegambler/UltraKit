﻿using System.Collections;
using MoonSharp.Interpreter;
using ULTRAKIT.Data;
using ULTRAKIT.Data.Components;
using ULTRAKIT.Lua.API;
using UnityEngine;
using System.Collections.Generic;
using BepInEx.Logging;
using UnityEngine.SceneManagement;

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
        // public ManualLogSource logger; 

        public static void Create(UKAddonData addon, UKScript orig, bool callUpdateWhilePaused)
        {
            var r = orig.gameObject.AddComponent<UKScriptRuntime>();
            r.addon = addon;
            r.callUpdateWhilePaused = callUpdateWhilePaused;
            r.data = r.GetComponent<UKScript>();
            r.runtime = new Script(CoreModules.Preset_SoftSandbox);
            // r.logger = BepInEx.Logging.Logger.CreateLogSource(r.addon.ModName);

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
                // logger.LogError($"RUNTIME ERROR: {data.sourceCode.name} - {e.DecoratedMessage}");
                
                this.LuaError(e);
                // Debug.LogError($"(ULTRAKIT Lua) RUNTIME ERROR: {data.sourceCode.name} - {e.DecoratedMessage}");
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
                    // logger.LogError($"RUNTIME ERROR: {data.sourceCode.name} - {e.DecoratedMessage}");
                    this.LuaError(e);
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
                // logger.LogError($"RUNTIME ERROR: {data.sourceCode.name} - {e.DecoratedMessage}");
                this.LuaError(e);
                enabled = false;
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
            SceneManager.sceneLoaded += OnSceneLoaded;
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
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            FuzzyCall(runtime.Globals, "OnSceneLoaded");
            Debug.Log(scene.name);
        }

        public void Invoke(DynValue func, float delay)
        {
            StartCoroutine(Delay());
            
            IEnumerator Delay()
            {
                yield return new WaitForSeconds(delay);
                func.Function.Call();
            }
        }
    }
}