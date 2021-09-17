using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Data.ScriptableObjects.Registry
{
    public enum TargetWeapon
    {
        Revolver,
        SlabRevolver,
        Shotgun,
        Nailgun,
        Railcannon
    }

    public class UKContentSkin : UKContent
    {
        public TargetWeapon targ;
        public Material newMat;
    }
}
