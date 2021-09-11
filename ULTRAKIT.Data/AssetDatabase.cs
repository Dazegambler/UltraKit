using System.Collections.Generic;
using UnityEngine;

namespace ULTRAKIT.Data
{
    public static class AssetDatabase
    {
        public static Dictionary<string, GameObject> assets = new Dictionary<string, GameObject>();

        public static string[] _assets = {
            "RevolverBeam",
            "RevolverBeamSuper",
            "MaliciousBeam",
            "ShotgunProjectile",
            "Grenade",
            "NailHeated",
            "Harpoon",
            "NailFodder",
            "Nail",
            "RevolverBeamAlt",
            "RevolverBeamSuperAlt",
            "Coin",
            "RailcannonBeamMalicious",
            "RailcannonBeam",
            "HarpoonMalicious",
            "Projectile",
            "ProjectileHoming",
            "ProjectileExplosiveHH",
            "ProjectileExplosive",
            "ShotgunProjectileEnemy",
            "ProjectileMinosPrime",
            "ProjectileSpread",
            "Explosion",
            "ExplosionPrime",
            "ExplosionBig",
            "MindflayerExplosion",
            "ExplosionMaliciousRail",
            "ExplosionSuper",
            "ExplosionSand",
            "VirtueShatterExplosionless",
            "VirtueShatterExplosionlessHuge",
            "BlackHoleExplosion",
            "ExplosionWaveEnemy",
            "ExplosionWaveSisyphus",
            "ExplosionWave",
        };

        public static void Initialiaze()
        {
            foreach (string Name in _assets)
            {
                assets.Add(Name, AssetFind(Name));
            }
        }
        private static GameObject AssetFind(string name)
        {
            //Find set Object in the prefabs
            GameObject[]
                Pool = Resources.FindObjectsOfTypeAll<GameObject>();
            GameObject
                Original = new GameObject();

            foreach (GameObject obj in Pool)
            {
                if (obj.gameObject.name == name)
                {
                    Original = obj;
                }
            }
            return Original;
        }

    }
}
