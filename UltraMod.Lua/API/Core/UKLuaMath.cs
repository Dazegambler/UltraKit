using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Lua.Attributes;
using UnityEngine;

namespace UltraMod.Lua.API.Core
{
    [UKLuaStatic("Math")]
    public static class UKLuaMath
    {
        public static float Pi => Mathf.PI;

        public static float Random(float min, float max) => UnityEngine.Random.Range(min, max);
        public static float Lerp(float x, float y, float t) => Mathf.Lerp(x, y, t);

        public static float Min(float x, float y) => Mathf.Min(x, y);
        public static float Max(float x, float y) => Mathf.Max(x, y);

        public static float Sin(float x) => Mathf.Sin(x);
    }
}
