using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Lua.Attributes;
using UnityEngine;

namespace UltraMod.Lua.API.Core
{


    [MoonSharpUserData]
    public class UKLineTraceResult
    {
        public GameObject gameObject => this.transform.gameObject; 
        public Transform transform;
        public Vector3 point;
    }

    //TODO: nested system, Physics.HitType
    //TODO: game userdatas such as Enemy, Gib, Item
    [UKLuaStatic("HitType")]
    public enum UKHitType{
        World,
        Object,
        None
    }

    [UKLuaStatic("LayerMask")]
    public static class UKLayerMask
    {
        public static int EnemyTrigger = NameToLayer("EnemyTrigger");
        public static int Projectile = NameToLayer("Projectile");
        public static int Limb = NameToLayer("Limb");

        public static int NameToLayer(string name) => LayerMask.NameToLayer(name);
    }


    [UKLuaStatic("Physics")]
    public static class UKLuaPhysics
    {
        public static UKLineTraceResult Linecast(Vector3 start, Vector3 end, params int[] layers)
        {
            int mask = 0;
            foreach(var layer in layers)
            {
                mask |= (1 << layer);
            }



            RaycastHit info;
            bool hit = Physics.Linecast(start, end, out info, mask);

            UKLineTraceResult res = null;
            if (hit)
            {
                res = new UKLineTraceResult();
                res.transform = info.transform;
                res.point = info.point;
            }

            return res;
        }
    }
}
