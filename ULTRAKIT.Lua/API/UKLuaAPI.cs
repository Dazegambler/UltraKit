using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Reflection;
using ULTRAKIT.Lua.API.Abstract;
using ULTRAKIT.Lua.API.Proxies;
using ULTRAKIT.Lua.Attributes;
using ULTRAKIT.Lua.Components;
using UnityEngine;

namespace ULTRAKIT.Lua.API
{
    public static class UKLuaAPI
    {
        public static Dictionary<UKStatic, string> luaStatics = new Dictionary<UKStatic, string>();
        public static Action<UKScriptRuntime> constructMethods, destructMethods, updateMethods;

        ///<summary> 
        /// Fills in constructMethods and deconstructMethods to be called when a script is destroyed or created
        ///</summary>
        public static void Initialize()
        {
            // Register all types with MoonsharpUserData attribute
            UserData.RegisterAssembly();

            // Register all types extending UKStatic
            var staticsToInitialize = AttributeHelper.GetDerivedTypes(typeof(UKStatic));
            foreach (var staticType in staticsToInitialize)
            {
                var inst = (UKStatic)Activator.CreateInstance(staticType);
                Debug.Log($"IS {inst.name} NULL: {inst == null}");
                luaStatics.Add(inst, inst.name);
            }

            foreach (var obj in luaStatics.Keys)
            {
                UserData.RegisterType(obj.GetType());
            }

            // Register all methods with UKScriptConstructor attribute
            foreach (var method in AttributeHelper.GetMethodsWith<UKScriptConstructor>().Keys)
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

            var regMethod = typeof(UKLuaAPI).GetMethod(nameof(RegisterProxyType), BindingFlags.Public | BindingFlags.Static);
            foreach (var type in AttributeHelper.GetDerivedTypes(typeof(UKProxy<>)))
            {
                var targetType = type.BaseType.GetGenericArguments()[0];
                Debug.Log(targetType.Name);

                var regMethodGen = regMethod.MakeGenericMethod(type, targetType);

                Debug.Log(regMethodGen.IsGenericMethodDefinition);
                regMethodGen.Invoke(null, new object[] { });
            }
        }

        /// <summary>
        /// A wrapper function to allow calls to UserData.RegisterProxy with runtime types. Additionally adds all fake fields of the proxy to the typedef
        /// </summary>
        /// <typeparam name="TProxy">The proxy type to be registered</typeparam>
        /// <typeparam name="TTarget">The type that the proxy is targeting</typeparam>
        public static void RegisterProxyType<TProxy, TTarget>()
            where TProxy : UKProxy<TTarget>
            where TTarget : class
        {
            UserData.RegisterProxyType<TProxy, TTarget>((target) => (TProxy)Activator.CreateInstance(typeof(TProxy), target));
        }

        ///<summary>
        /// Accepts a UKScriptRuntime and calls constructMethods on it, allowing all the API modules to access the scripts on construction for whatever reason 
        ///</summary>
        public static void ConstructScript(UKScriptRuntime c)
        {
            // Globals
            c.runtime.Options.DebugPrint = (Action<string>)Debug.Log;
            c.runtime.Globals["gameObject"] = c.gameObject;

            // Statics
            foreach (var pair in luaStatics)
            {
                c.runtime.Globals[pair.Value] = pair.Key;
            }

            constructMethods?.Invoke(c);
        }

        ///<summary>
        /// Accepts a UKScriptRuntime and calls deconstructMethods on it, allowing all the API modules to access the scripts on destroy for whatever reason 
        ///</summary>
        public static void DestructScript(UKScriptRuntime c)
        {
            destructMethods?.Invoke(c);
        }

        ///<summary>
        /// Accepts a UKScriptRuntime and calls updateMethods on it, allowing all the API modules to access the scripts on update for whatever reason 
        ///</summary>
        public static void UpdateScript(UKScriptRuntime c)
        {
            updateMethods?.Invoke(c);
        }
    }
}
