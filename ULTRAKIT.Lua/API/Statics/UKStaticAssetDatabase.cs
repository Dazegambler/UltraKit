using System.Collections.Generic;
using System;
using UnityEngine;
using ULTRAKIT.Lua.API.Abstract;
using MoonSharp.Interpreter;
using ULTRAKIT.Lua;
using ULTRAKIT.Lua.Extensions;

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
            "ExplosionHarmless",
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
            //Sounds
            "CoinHit",
            "HookArmHit",
            "ShopSuccess",
            "WickedHit",
            "WarningBeep",
            "HookArmThrow",
            "RailcannonFire",
            "HookArmPull",
            "NailgunNoAmmo",
            "ShopFail",
            "RevolverClick",
            "WeakHit",
            "GrenadeLauncherSound",
            "MindflayerWindup",
            "HookArmCatch",
            "CrashKill",
            "ProtectorLose",
            "ProtectorGet",
            "CoinGet",
        };


        [MoonSharpHidden]
        public UKStaticAssetDatabase()
        {
            foreach (string Name in assetNames)
            {
                assetDict.Add(Name, DazeExtensions.PrefabFind(Name) ?? Common.PrefabFind("common", Name));
            }
        }

        public GameObject Get(string name)
        {
            try
            {
                return assetDict[name];
            }
            catch (KeyNotFoundException)
            {
                UnityEngine.Debug.LogWarning($"Could Not Find Asset {name}, please check the asset list");
                return new GameObject();
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
                UnityEngine.Debug.LogWarning($"Could Not Find Asset {name}, please check the asset list");
                return new GameObject();
            }
        }

        //TODO: create asset position
    }
}
