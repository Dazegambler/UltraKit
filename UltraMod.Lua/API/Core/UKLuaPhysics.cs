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

        public static int NameToLayer(string name) => LayerMask.NameToLayer(name);
    }


    [UKLuaStatic("Physics")]
    public static class UKLuaPhysics
    {
        public static UKLineTraceResult Linecast(Vector3 start, Vector3 end, int layer)
        {
            RaycastHit info;
            bool hit = Physics.Linecast(start, end, out info, 1 << layer, QueryTriggerInteraction.Ignore);

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
