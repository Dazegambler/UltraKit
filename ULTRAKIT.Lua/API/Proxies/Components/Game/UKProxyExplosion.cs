using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    public class UKProxyExplosion : UKProxyComponentAbstract<Explosion>
    {
        [MoonSharpHidden]
        public UKProxyExplosion(Explosion target) : base(target)
        {
        }

        public bool enemy
        {
            get => target.enemy;
            set => target.enemy = value;
        }

        public bool harmless
        {
            get => target.harmless;
            set => target.harmless = value;
        }
        public bool lowQuality
        {
            get => target.lowQuality;
            set => target.lowQuality = value;
        }
        public float speed
        {
            get => target.speed;
            set => target.speed = value;
        }
        public float maxSize
        {
            get => target.maxSize;
            set => target.maxSize = value;
        }
        public int damage
        {
            get => target.damage;
            set => target.damage = value;
        }
        public float enemyDamageMultiplier
        {
            get => target.enemyDamageMultiplier;
            set => target.enemyDamageMultiplier = value;
        }
        public int playerDamageOverride
        {
            get => target.playerDamageOverride;
            set => target.playerDamageOverride = value;
        }
        public GameObject explosionChunk
        {
            get => target.explosionChunk;
            set => target.explosionChunk = value;
        }
        public bool ignite
        {
            get => target.ignite;
            set => target.ignite = value;
        }
        public bool friendlyFire
        {
            get => target.friendlyFire;
            set => target.friendlyFire = value;
        }
        public string hitterWeapon
        {
            get => target.hitterWeapon;
            set => target.hitterWeapon = value;
        }
        public bool halved
        {
            get => target.halved;
            set => target.halved = value;
        }
        public AffectedSubjects canHit
        {
            get => target.canHit;
            set => target.canHit = value;
        }
        public List<EnemyType> toIgnore
        {
            get => target.toIgnore;
            set => target.toIgnore = value;
        }
        public bool ultrabooster
        {
            get => target.ultrabooster;
            set => target.ultrabooster = value;
        }

        public Rigidbody rigidbody => target.GetComponent<Rigidbody>();

    }
}
