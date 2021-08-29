using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Data.Components;
using UltraMod.Data.ScriptableObjects.Registry;
using UltraMod.Loader;
using UnityEngine;

namespace UltraMod.Lua
{
    public static class UKLuaRuntime
    {
        public static Dictionary<TextAsset, Script> scriptDict = new Dictionary<TextAsset, Script>();

        public static void Initialize()
        {
            AddonLoader.GetAll<UKContentWeapon>();
        }

        public static void RegisterSWEP(UKScriptSWEP weap) {
            var s = new Script();
        }
    }
}
