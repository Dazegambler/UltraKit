using System.Collections.Generic;
using System;
using UnityEngine;
using ULTRAKIT.Lua.API.Abstract;
using MoonSharp.Interpreter;
using General_Unity_Tools;

namespace ULTRAKIT.Data
{
    public class UKStaticAssetDatabase : UKStatic
    {
        public static AssetBundle Common;

        public override string name => "AssetDatabase";

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
            "PhysicalShockwave",
            "PhysicalShockwaveHarmless",
            "PhysicalShockwavePlayer",
        };


        [MoonSharpHidden]
        public UKStaticAssetDatabase()
        {
            foreach (string Name in assetNames)
            {
                try
                {
                    assetDict.Add(Name, AssetFind(Name));
                }
                catch
                {
                    assetDict.Add(Name, AssetFindInBundle(Name));
                }
            }
            Common = AssetBundle.LoadFromFile($@"{Application.productName}_Data\StreamingAssets\common");
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

        private GameObject AssetFindInBundle(string name)
        {
            return Common.LoadAsset<GameObject>(name);
        }
    }
}
