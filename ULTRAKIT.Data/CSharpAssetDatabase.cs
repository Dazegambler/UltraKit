using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Data
{
    public class CSharpAssetDatabase
    {
        private Dictionary<string, GameObject> assetDict = new Dictionary<string, GameObject>();
        public static readonly string[] assetNames = {
            //Projectiles
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
            "DroneMaliciousBeam",
            "ProjectileExplosiveHH",
            "ProjectileExplosive",
            "ShotgunProjectileEnemy",
            "ProjectileMinosPrime",
            "VirtueInsignia",
            "ProjectileSpread",
            "DroneFleshBeam",
            //Particles and Miscs
            "BlackHoleEnemy",
            "DroneSkullWarningBeam",
            "GunFlash",
            "Flash",
            "ImpactParticle",
            "GunFlashBig",
            "GunFlashDistant",
            "CoinFlashEnemy",
            "CoinFlash",
            "BulletSpark",
            "Fire",
            "FireSimple",
            "SparksNail",
            "SandificationEffect",
            "BreakParticleMetal",
            "AttackWindup",
            "BreakParticleMetalSmall",
            "BreakParticleBig",
            "LaserHitParticle",
            "SuperLaserHitParticle",
            "SecLaserHitParticle",
            "EnemyRevolverPrepare",
            "EnemyRevolverMuzzleSuper",
            //Explosions
            "Explosion",
            "ExplosionPrime",
            "ExplosionBig",
            "MindflayerExplosion",
            "ExplosionMaliciousRail",
            "ExplosionSuper",
            "ExplosionSand",
            "VirtueShatter",
            "VirtueShatterExplosionless",
            "VirtueShatterExplosionlessHuge",
            "BlackHoleExplosion",
            "ExplosionWaveEnemy",
            "ExplosionWaveSisyphus",
            "ExplosionWave",
        };


        public CSharpAssetDatabase()
        {
            foreach (string Name in assetNames)
            {
                assetDict.Add(Name, AssetFind(Name));
            }
        }

        public GameObject Create(string name)
        {
            try
            {
                return GameObject.Instantiate(assetDict[name]);
            }
            catch (KeyNotFoundException)
            {
                Debug.LogWarning($"Could Not Find Asset {name}, please check the asset list");
                return new GameObject();
            }
        }

        //TODO: create asset position

        private GameObject AssetFind(string name)
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
