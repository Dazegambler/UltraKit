using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data;
using ULTRAKIT.Data.Components;
using ULTRAKIT.Lua.API;
using ULTRAKIT.Lua.API.Proxies;
using UnityEngine;

namespace ULTRAKIT.Lua.Components
{
    public class UKScriptRuntime : MonoBehaviour
    {
        public UKScript data;
        public Script runtime;

        public static void Create(UKAddonData addon, UKScript orig)
        {
            var r = orig.gameObject.AddComponent<UKScriptRuntime>();
        }

        public static void Create(UKAddonData data, GameObject go)
        {
            foreach(var script in go.GetComponentsInChildren<UKScript>())
            {
                Create(data, script);
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
                Debug.LogError($"(ULTRAKIT Lua) {data.sourceCode.name} -  {e.DecoratedMessage}");
            }
        }

        public void FuzzyCall(DynValue d, params object[] luap)
        {
            if (d.Function != null)
            {
                runtime.Call(d, luap);
            }
            else
            {
                //TODO: proper logging
            }
        }

        void Awake()
        {
            data = GetComponent<UKScript>();
            runtime = new Script(CoreModules.Preset_SoftSandbox);

            

            UKLuaAPI.ConstructScript(this);
            runtime.DoString(data.sourceCode.text);
        }

        void OnDestroy()
        {
            UKLuaAPI.DestructScript(this);
        }

        //Script Callbacks
        void OnEnable()
        {
            FuzzyCall(runtime.Globals, "OnEnable");
        }

        void Update()
        {
            if (!MonoSingleton<OptionsManager>.Instance.paused)
            {
                FuzzyCall(runtime.Globals, "Update", Time.deltaTime);

                //TODO: automate API update calls using attribute
                //UKLuaInput.Update(this);
            }
        }

        void OnDisable()
        {
            FuzzyCall(runtime.Globals, "OnDisable");
        }
    }
}
