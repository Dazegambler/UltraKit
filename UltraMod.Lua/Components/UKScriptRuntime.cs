using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Data.Components;
using UltraMod.Lua.API.Proxies;
using UnityEngine;

namespace UltraMod.Lua.Components
{
    public class UKScriptRuntimeProxy : UKLuaProxy<UKScriptRuntime>
    {
        [MoonSharpHidden]
        public UKScriptRuntime Target => target;

        public UKScriptRuntimeProxy(UKScriptRuntime target) : base(target)
        {
        }
    }

    //TODO: properly sort runtime scripts into Addons
    public class UKScriptRuntime : MonoBehaviour
    {
        public UKScript original;
        public Script script;

        void Awake()
        {
            original = GetComponent<UKScript>();

            script = new Script();
            UKLuaRuntime.InitializeScript(script);

            script.Globals["_source"] = this;
            script.Globals["transform"] = transform;
            script.Globals["gameObject"] = gameObject;

            try
            {
                script.DoString(original.sourceCode.text);
            } catch(ScriptRuntimeException e)
            {
                //TODO: seriously, reorganize so you stop putting these everywhere
                //TODO: this is the kinda stuff the runtime class is for!! leave the loading to the addonloader (duh)
                //TODO: and the fuzzy calls there too dumbass
                Debug.LogError($"(UltraMod Lua) - {e.DecoratedMessage}");
            }
        }

        void OnEnable()
        {
            script.FuzzyCall("OnEnable");
        }

        void Update()
        {
            script.FuzzyCall("Update", DynValue.FromObject(script, Time.deltaTime));
        }

        void OnDisable()
        {
            script.FuzzyCall("OnDisable");
        }

    }
}
