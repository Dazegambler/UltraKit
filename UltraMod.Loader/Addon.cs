using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Data;
using UnityEngine;

namespace UltraMod.Loader
{
    public class Addon
    {
        public UltraModData Data;

        public string Path;
        public AssetBundle Bundle;
        public List<UKContentSpawnable> LoadedSpawnables;
        public List<UKContentWeapon> LoadedWeapons;
    }
}
