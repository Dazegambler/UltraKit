using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Core.Physgun;
using UltraMod.Loader.Registries;
using UnityEngine;

namespace UltraMod.Core
{
    public static class CoreContent
    {
        public static void Initialize()
        {
            RegisterPhysgun();

        }

        public static void RegisterPhysgun()
        {
            
            var phys = new GameObject().AddComponent<PhysgunScript>();
            phys.lr = phys.gameObject.AddComponent<LineRenderer>();

            var physWeap = new UltraMod.Data.UKContentWeapon();
            physWeap.Name = "Physgun";
            physWeap.Variants = new List<GameObject>();
            physWeap.Variants.Add(phys.gameObject);

            GameObject.DontDestroyOnLoad(phys.gameObject);
            phys.gameObject.SetActive(false);

            WeaponRegistry.registeredWeapons.Add(physWeap);
        }
    }
}
