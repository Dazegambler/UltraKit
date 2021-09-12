using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    public class UKProxyProjectile : UKProxyComponentAbstract<Projectile>
    {
        public UKProxyProjectile(Projectile target) : base(target)
        {
        }
    }
}
