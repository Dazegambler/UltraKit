using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies
{
    public class UKProxyGameObject : UKProxy<GameObject>
    {
        public UKProxyGameObject(GameObject target) : base(target)
        {
        }
    }
}
