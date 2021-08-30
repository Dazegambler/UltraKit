using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Lua.Attributes;
using UltraMod.Lua.Components;
using UnityEngine;

namespace UltraMod.Lua.API.Proxies.Core
{
    [UKLuaStatic("GameObject")]
    public class UKLuaGameObject : UKLuaProxy<GameObject>
    {
        public UKLuaGameObject(GameObject target) : base(target)
        {
        }

        public string name => target.name;
        public Transform transform => target.transform;
        

        public void SetActive(bool b) => target.SetActive(b);

        public static GameObject Instantiate(Script s, GameObject g)
        {
            UKScriptRuntime rs = s.Globals.Get("_source").ToObject<UKScriptRuntime>();

            var newG = GameObject.Instantiate(g);
            UKLuaRuntime.Register(UKLuaRuntime.addonDict[rs.original.sourceCode], newG);
            return newG;
        }
        public static GameObject Instantiate(Script s, GameObject g, Transform parent)
        {
            var newG = Instantiate(s, g);
            newG.transform.parent = parent;
            return newG;
        }

    }
}
