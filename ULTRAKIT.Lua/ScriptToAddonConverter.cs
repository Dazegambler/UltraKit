using System.Linq;
using MoonSharp.Interpreter;
using ULTRAKIT.Data;
using ULTRAKIT.Lua.Components;
using UnityEngine;

namespace ULTRAKIT.Lua
{
    // I don't know where to place this, so I made a class for it!
    // But feel free to move it around...
    public class ScriptToAddonConverter
    {
        public static UKAddonData GetAddonFromScript(Script script)
        {
            Debug.Log("Getting a call!");
            Debug.Log("In the inventory we have:");
            foreach (var instance in UKScriptRuntime.Instances)
            {
                Debug.Log(instance.addon == null);
            }
            return UKScriptRuntime.Instances.First(t => t.runtime == script).addon;
        } 
    }
}