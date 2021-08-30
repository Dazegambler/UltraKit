using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraMod.Lua.API.Math
{
    public class UKLuaVector3
    {
        Vector3 target;

        public UKLuaVector3(ref Vector3 target)
        {
            this.target = target;
        }


    }
}
