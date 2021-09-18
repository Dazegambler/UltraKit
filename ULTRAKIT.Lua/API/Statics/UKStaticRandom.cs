using ULTRAKIT.Lua.API.Abstract;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Statics
{
    public class UKStaticRandom : UKStatic
    {
        public override string name => "Random";

        public float Range(float min, float max) => Random.Range(min, max);

        public int RangeInt(int min, int max) => Random.Range(min, max);
    }
}
