using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using ULTRAKIT.Lua.API.Abstract;
using ULTRAKIT.Data;
using MoonSharp.Interpreter;

namespace ULTRAKIT.Lua.API.Statics
{
    public class UKStaticRegistry : UKStatic
    {
        public override string name => "Registry";

        public static Dictionary<string, DynValue> sharedData = new Dictionary<string, DynValue>();
        public static Dictionary<(int, string), DynValue> addonData = new Dictionary<(int, string), DynValue>();

        public void Set(Script script, string name, DynValue value)
        {
            var addon = ScriptToAddonConverter.GetAddonFromScript(script).GetInstanceID();
            
            try
            {
                addonData.Add((addon, name), value);
            }
            catch (ArgumentException) // Key already exists
            {
                addonData[(addon, name)] = value;
            }
        }

        public DynValue Get(Script script, string name)
        {
            var runtime = ScriptToAddonConverter.GetRuntimeFromScript(script);
            var addon = runtime.addon.GetInstanceID();
            try
            {
                return addonData[(addon, name)];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError("Key not found in database.", ScriptToAddonConverter.GetRuntimeFromScript(script));
                return null;
            }
        }

        public void SetGlobal(string name, DynValue value)
        {
            try
            {
                sharedData.Add(name, value);
            }
            catch (ArgumentException) // Key already exists
            {
                sharedData[name] = value;
            }
        }

        public DynValue GetGlobal(Script script, string name)
        {
            if (script is null)
            {
                throw new ArgumentNullException(nameof(script));
            }

            try
            {
                return sharedData[name];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError("Key not found in database.", ScriptToAddonConverter.GetRuntimeFromScript(script));
                return null;
            }
        }

    }
}
