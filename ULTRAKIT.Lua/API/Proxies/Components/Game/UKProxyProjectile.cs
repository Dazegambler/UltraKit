using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    public class UKProxyProjectile : UKProxyComponentAbstract<Projectile>
    {
        [MoonSharpHidden]
        public UKProxyProjectile(Projectile target) : base(target)
        {
        }

        public GameObject explosionEffect
        {
            get => target.explosionEffect;
            set => target.explosionEffect = value;
        }

        public float damage
        {
            get => target.damage;
            set => target.damage = value;
        }
        
        public float speed
        {
            get => target.speed;
            set => target.speed = value;
        }

        public float speedRandomizer
        {
            get => target.speedRandomizer;
            set => target.speedRandomizer = value;
        }

        public float turningSpeedMultiplier
        {
            get => target.turningSpeedMultiplier;
            set => target.turningSpeedMultiplier = value;
        }

        public bool undeflectable
        {
            get => target.undeflectable;
            set => target.undeflectable = value;
        }

        public bool decorative
        {
            get => target.decorative;
            set => target.decorative = value;
        }

        public bool canHitCoin
        {
            get => target.canHitCoin;
            set => target.canHitCoin = value;
        }

        public Transform targetTransform
        {
            get => target.target;
            set => target.target = value;
        }



        public bool friendly
        {
            get => target.friendly;
            set => target.friendly = value;
        }
        
        public string bulletType
        {
            get => target.bulletType;
            set => target.bulletType = value;
        }

        public string weaponType
        {
            get => target.weaponType;
            set => target.weaponType = value;
        }

        public bool explosive
        {
            get => target.explosive;
            set => target.explosive = value;
        }

        public bool bigExplosion
        {
            get => target.bigExplosion;
            set => target.bigExplosion = value;
        }

        public bool playerBullet
        {
            get => target.playerBullet;
            set => target.playerBullet = value;
        }

        public string homingType
        {
            get => target.homingType.ToString();
            set => target.homingType = setHomingType(value);
        }

        public Rigidbody rigidbody => target.GetComponent<Rigidbody>();

        public HomingType setHomingType(string type)
        {
            if (type == "Gradual")
            {
                return HomingType.Gradual;
            }
            if (type == "HorizontalOnly")
            {
                return HomingType.HorizontalOnly;
            }
            if (type == "Instant")
            {
                return HomingType.Instant;
            }
            if (type == "Loose")
            {
                return HomingType.Loose;
            }
            if (type == "None")
            {
                return HomingType.None;
            }
            return HomingType.None;
        }
    }
}
