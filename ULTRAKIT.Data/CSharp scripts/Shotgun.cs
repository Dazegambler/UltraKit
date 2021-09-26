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

        [Serializable]
        public class Spread
        {
            public float
                SpreadX,
                SpreadY,
                SpreadZ,
                SpreadW,
                SpreadAltX,
                SpreadAltY,
                SpreadAltZ,
                SpreadAltW;
            public int
                PelletCount,
                PelletCountAlt;

        }
        public Spread
            Pellets;
        [Space]

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
                        for (int i = 0; i < Pellets.PelletCount; i++)
                        {
                            var proj = Database.Create(ProjectileName, Barrel.transform.position, new Quaternion(
                                Cc.transform.rotation.x + UnityEngine.Random.Range(0, Pellets.SpreadX),
                                Cc.transform.rotation.y + UnityEngine.Random.Range(0, Pellets.SpreadY),
                                Cc.transform.rotation.z + UnityEngine.Random.Range(0, Pellets.SpreadZ),
                                Cc.transform.rotation.w + UnityEngine.Random.Range(0, Pellets.SpreadW)
                                ));
                        }
                    }
                    break;
                case WepType.Projectile:
                    foreach (var Barrel in BarrelsAlt)
                    {
                        for (int i = 0; i < Pellets.PelletCount; i++)
                        {
                            var a = Database.Create(ProjectileName, Barrel.transform.position, new Quaternion(
                                Cc.transform.rotation.x + UnityEngine.Random.Range(0, Pellets.SpreadX),
                                Cc.transform.rotation.y + UnityEngine.Random.Range(0, Pellets.SpreadY),
                                Cc.transform.rotation.z + UnityEngine.Random.Range(0, Pellets.SpreadZ),
                                Cc.transform.rotation.w + UnityEngine.Random.Range(0, Pellets.SpreadW)
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
                        for (int i = 0; i < Pellets.PelletCountAlt; i++)
                        {
                            var proj = Database.Create(ProjectileName, Barrel.transform.position, new Quaternion(
                                Cc.transform.rotation.x + UnityEngine.Random.Range(0, Pellets.SpreadAltX),
                                Cc.transform.rotation.y + UnityEngine.Random.Range(0, Pellets.SpreadAltY),
                                Cc.transform.rotation.z + UnityEngine.Random.Range(0, Pellets.SpreadAltZ),
                                Cc.transform.rotation.w + UnityEngine.Random.Range(0, Pellets.SpreadAltW)
                                ));
                        }
                    }
                    break;
                case WepTypeAlt.Projectile:
                    foreach (var Barrel in BarrelsAlt)
                    {
                        for (int i = 0; i < Pellets.PelletCountAlt; i++)
                        {
                            var a = Database.Create(ProjectileName, Barrel.transform.position, new Quaternion(
                                Cc.transform.rotation.x + UnityEngine.Random.Range(0, Pellets.SpreadAltX),
                                Cc.transform.rotation.y + UnityEngine.Random.Range(0, Pellets.SpreadAltY),
                                Cc.transform.rotation.z + UnityEngine.Random.Range(0, Pellets.SpreadAltZ),
                                Cc.transform.rotation.w + UnityEngine.Random.Range(0, Pellets.SpreadAltW)
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
