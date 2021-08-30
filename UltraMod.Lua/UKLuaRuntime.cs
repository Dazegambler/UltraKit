using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Data;
using UltraMod.Data.Components;
using UltraMod.Data.ScriptableObjects.Registry;
using UltraMod.Lua.API;
using UltraMod.Lua.API.Proxies;
using UltraMod.Lua.API.Proxies.Core;
using UltraMod.Lua.Attributes;
using UltraMod.Lua.Components;
using UnityEngine;

namespace UltraMod.Lua
{
    public static class MoonsharpScriptExtensions{

        public static void FuzzyCall(this Script script, string name, params DynValue[] args)
        {
            //TODO: proper logger, with mod and script name!!!
            try
            {
                if (script.Globals.Get(name).IsNotNil())
                {
                    script.Call(script.Globals.Get(name), args);
                }
            } catch(ScriptRuntimeException e)
            {
                Debug.LogError($"(UltraMod Lua) - {e.DecoratedMessage}");
            } catch(Exception e)
            {
                Debug.LogError($"(UltraMod Lua) - {e.Message} ({e.TargetSite})");
            }
        }

        public static void FuzzyCall(this Table table, string name, params DynValue[] args)
        {
            if (table.Get(name).IsNotNil())
            {
                table.OwnerScript.Call(table.Get(name), args);
            }
        }
    }

    public static class UKLuaRuntime
    {
        public static Dictionary<TextAsset, UKAddonData> addonDict = new Dictionary<TextAsset, UKAddonData>(); 
        static Dictionary<string, System.Type> staticDict = new Dictionary<string, System.Type>();

        public static void Initialize()
        {
            ScriptHelper.RegisterSimpleAction();

            foreach(var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var att = type.GetCustomAttribute(typeof(UKLuaStaticAttribute)) as UKLuaStaticAttribute;
                if (att != null)
                {
                    UserData.RegisterType(type);
                    staticDict.Add(att.luaName, type);
                }
            }

            //TODO: proxy these
            RegisterValueType<Vector3>();

            //TODO: AUTOMATE WITH A PROXY ATTRIBUTE
            //https://stackoverflow.com/questions/293905/reflection-getting-the-generic-arguments-from-a-system-type-instance
            RegisterProxy<UKLuaGameObject, GameObject>();
            RegisterProxy<UKLuaTransform, Transform>();
            RegisterProxy<UKScriptRuntimeProxy, UKScriptRuntime>();
        }

        //TEMP
        public static void RegisterValueType<T>()
        {
            UserData.RegisterType<Vector3>();
            staticDict.Add(typeof(T).Name, typeof(T));
        }

        // holy shit
        public static void RegisterProxy<P, T>()
            where T : class
            where P : UKLuaProxy<T>
        {
            UserData.RegisterProxyType<P, T>((o) => (P)Activator.CreateInstance(typeof(P), o));
        }

        public static void Register(UKAddonData data, GameObject go)
        {
            foreach(var c in go.GetComponentsInChildren<UKScript>())
            {
                Register(data, c);
            }
        }

        public static void Register(UKAddonData data, UKScript ukScript)
        {
            if (!addonDict.ContainsKey(ukScript.sourceCode))
            {
                addonDict.Add(ukScript.sourceCode, data);
            }
            var runtime = ukScript.gameObject.AddComponent<UKScriptRuntime>();
        }

        public static void InitializeScript(Script script)
        {
            foreach (var pair in staticDict)
            {
                script.Globals[pair.Key] = pair.Value;
            }

            script.Globals["log"] = (Action<string>)Debug.Log;
            
        }

        
    }
}
