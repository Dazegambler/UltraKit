using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data.Components;
using UnityEngine;

namespace ULTRAKIT.Data.CSharp_scripts
{
    class Shotgun :UKScript
    {
        CSharpAssetDatabase
            Database;

        GameObject
            Cc;
        [Header("LUA(Optional)")]
        public bool
            UseLua;

        #region Projectile
        public enum WepType
        {
            Hitscan,
            Projectile,
        }
        public enum WepTypeAlt
        {
            Hitscan,
            Projectile,
        }

        public WepType
            Type = WepType.Hitscan;
        public WepTypeAlt
            TypeAlt = WepTypeAlt.Hitscan;

        public float
            SpreadX,
            SpreadY,
            SpreadAltX,
            SpreadAltY;

        public int
            PelletCount,
            PelletCountAlt;

        public List<GameObject>
            BarrelsAlt,
            Barrels;

        public float
            ProjBoostAlt,
            ProjBoost;

        GameObject
            Projectile;

        GameObject
            ProjectileAlt;

        public string
            ProjectileName,
            ProjectileNameAlt;

        #endregion

        void Awake()
        {
            Cc = GameObject.Find("Main Camera");
            Database = new CSharpAssetDatabase();
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Invoke("Shoot", 0);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Invoke("AltShoot", 0);
            }
        }
        void Shoot()
        {
            switch (Type)
            {
                case WepType.Hitscan:
                    foreach(var Barrel in Barrels)
                    {
                        for (int i = 0; i < PelletCount; i++)
                        {
                            var proj = Database.Create(ProjectileName, Barrel.transform.position, new Quaternion(
                                Cc.transform.rotation.x + UnityEngine.Random.Range(-SpreadX, SpreadX),
                                Cc.transform.rotation.y + UnityEngine.Random.Range(-SpreadY, SpreadY),
                                Cc.transform.rotation.z,
                                Cc.transform.rotation.w
                                ));
                        }
                    }
                    break;
                case WepType.Projectile:
                    foreach (var Barrel in BarrelsAlt)
                    {
                        for (int i = 0; i < PelletCount; i++)
                        {
                            var a = Database.Create(ProjectileName, Barrel.transform.position, new Quaternion(
                                Cc.transform.rotation.x + UnityEngine.Random.Range(-SpreadX, SpreadX),
                                Cc.transform.rotation.y + UnityEngine.Random.Range(-SpreadY, SpreadY),
                                Cc.transform.rotation.z,
                                Cc.transform.rotation.w
                                ));
                            a.transform.forward = Cc.transform.forward;
                            a.GetComponent<Rigidbody>().AddForce(a.transform.forward * ProjBoost, ForceMode.Impulse);
                        }
                    }
                    // Make this use muzzle.transform.forward too? I won't change it because I haven't tested it 
                    break;
            }
        }
        void AltShoot()
        {
            switch (TypeAlt)
            {
                case WepTypeAlt.Hitscan:
                    foreach (var Barrel in Barrels)
                    {
                        for (int i = 0; i < PelletCountAlt; i++)
                        {
                            var proj = Database.Create(ProjectileNameAlt, Barrel.transform.position, new Quaternion(
                                Cc.transform.rotation.x + UnityEngine.Random.Range(-SpreadAltX, SpreadAltX),
                                Cc.transform.rotation.y + UnityEngine.Random.Range(-SpreadAltY, SpreadAltY),
                                Cc.transform.rotation.z,
                                Cc.transform.rotation.w
                                ));
                        }
                    }
                    break;
                case WepTypeAlt.Projectile:
                    foreach (var Barrel in BarrelsAlt)
                    {
                        for (int i = 0; i < PelletCountAlt; i++)
                        {
                            var a = Database.Create(ProjectileNameAlt, Barrel.transform.position, new Quaternion(
                                Cc.transform.rotation.x + UnityEngine.Random.Range(-SpreadAltX, SpreadAltX),
                                Cc.transform.rotation.y + UnityEngine.Random.Range(-SpreadAltX, SpreadAltY),
                                Cc.transform.rotation.z,
                                Cc.transform.rotation.w
                                ));
                            a.transform.forward = Cc.transform.forward;
                            a.GetComponent<Rigidbody>().AddForce(a.transform.forward * ProjBoost, ForceMode.Impulse);
                        }
                    }
                    // Make this use muzzle.transform.forward too? I won't change it because I haven't tested it 
                    break;
            }
        }
    }
}
