using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Lua.Attributes;
using ULTRAKIT.Lua.Components;
using UnityEngine;

namespace ULTRAKIT.Lua.API
{
    public static class UKLuaAPI
    {
        public static Dictionary<Type, string> luaStatics = new Dictionary<Type, string>();
        public static Action<UKScriptRuntime> constructMethods, destructMethods, updateMethods;


        ///<summary> 
        /// Fills in constructMethods and deconstructMethods to be called when a script is destroyed or created
        ///</summary>
        public static void Initialize()
        {
            // Register all types with MoonsharpUserData attribute
            UserData.RegisterAssembly();

            // Register all types with UKLuaStatic attribute
            luaStatics = AttributeHelper.GetTypesWith<UKLuaStaticAttribute>()
                .ToDictionary(pair => pair.Key, pair => pair.Value.luaName);
            
            foreach(var type in luaStatics.Keys)
            {
                UserData.RegisterType(type);
            }

            // Register all methods with UKScriptConstructor attribute
            foreach(var method in AttributeHelper.GetMethodsWith<UKScriptConstructor>().Keys)
            {
                constructMethods += Delegate.CreateDelegate(method.DeclaringType, method) as Action<UKScriptRuntime>;
            }

            // Register all methods with UKScriptUpdater attributes
            foreach (var method in AttributeHelper.GetMethodsWith<UKScriptUpdater>().Keys)
            {
                updateMethods += Delegate.CreateDelegate(method.DeclaringType, method) as Action<UKScriptRuntime>;
            }

            // Register all methods with UKScriptDestructor attributes
            foreach (var method in AttributeHelper.GetMethodsWith<UKScriptDestructor>().Keys)
            {
                destructMethods += Delegate.CreateDelegate(method.DeclaringType, method) as Action<UKScriptRuntime>;
            }
        }

        ///<summary>
        /// Accepts a UKScriptRuntime and calls constructMethods on it, allowing all the API modules to access the scripts on construction for whatever reason 
        ///</summary>
        public static void ConstructScript(UKScriptRuntime c)
        {
            // Globals
            var script = new Script(CoreModules.Preset_SoftSandbox);
            // TODO: real log method
            script.Globals["log"] = (Action<string>)Debug.Log;

            // Statics
            foreach(var pair in luaStatics)
            {
                script.Globals[pair.Value] = pair.Key;
            }

            constructMethods?.Invoke(c);
            c.runtime = script;
        }

        ///<summary>
        /// Accepts a UKScriptRuntime and calls deconstructMethods on it, allowing all the API modules to access the scripts on destroy for whatever reason 
        ///</summary>
        public static void DestructScript(UKScriptRuntime c)
        {
            destructMethods.Invoke(c);
        }

        ///<summary>
        /// Accepts a UKScriptRuntime and calls updateMethods on it, allowing all the API modules to access the scripts on update for whatever reason 
        ///</summary>
        public static void UpdateScript(UKScriptRuntime c)
        {
            updateMethods.Invoke(c);
        }
    }
}
