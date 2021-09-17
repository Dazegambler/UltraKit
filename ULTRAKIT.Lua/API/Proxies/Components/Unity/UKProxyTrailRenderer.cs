using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    class UKProxyTrailRenderer : UKProxyComponentAbstract<TrailRenderer>
    {       
        public UKProxyTrailRenderer(TrailRenderer target) : base(target)
        {
        }

        //COMPONENTS
        public bool emitting
        {
            get => target.emitting;
            set => target.emitting = value;
        }

        //METHODS
    }
}
