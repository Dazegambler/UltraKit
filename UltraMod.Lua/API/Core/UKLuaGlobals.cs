using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Lua.Attributes;
using ULTRAKIT.Lua.Components;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Core
{
    public static class UKLuaGlobals
    {
        static Vector3 Vector3Create(float x = 0, float y = 0, float z = 0) => new Vector3(x, y, z);

        [UKScriptConstructor]
        static void ConstructScript(UKScriptRuntime script)
        {
            //references
            script.runtime.Globals["transform"] = script.transform;
            script.runtime.Globals["gameObject"] = script.gameObject;

            //statics
            script.runtime.Globals["Vector3"] = typeof(Vector3);
            script.runtime.Globals["CreateVector3"] = (Func<float, float, float, Vector3>) Vector3Create;

            //log method
            script.runtime.Globals["log"] = (Action<string>)Debug.Log;
        }
    }
}
