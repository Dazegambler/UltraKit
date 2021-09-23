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
        public static UKAddonData GetAddonFromScript(Script script) => UKScriptRuntime.Instances.First(t => t.runtime == script).addon;
    }
}