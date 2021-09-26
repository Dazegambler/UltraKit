using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data.Components;
using UnityEngine;

namespace ULTRAKIT.Data.CSharp_scripts
{
    class BasicWeapon : UKScript
    {
        CSharpAssetDatabase
            Database;

        GameObject
            Cc;

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

        [Header("LUA(Optional)")]
        public bool
            UseLua;

        public UKScript
            Script;

        [Space]
        public float
            DelayAlt,
            Delay;

        public int
            Amount_to_shoot,
            Amount_to_shootAlt;

        public GameObject
            Muzzle;

        public float
            ProjBoostAlt,
            ProjBoost;

        GameObject
            Projectile;

        GameObject
            ProjectileAlt;

        public string
            ProjectileName;

        public string
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
                Invoke("Shoot",0);
                for (int i = 1; i < Amount_to_shoot; i++)
                {
                    Invoke("Shoot", (Delay/3)*i);
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Invoke("AltShoot",0);
                for (int i = 1; i < Amount_to_shootAlt; i++)
                {
                    Invoke("AltShoot",(DelayAlt / 3) * i);
                }
            }
        }
        void Shoot()
        {
            switch (Type)
            {
                case WepType.Hitscan:
                    var proj = Database.Create(ProjectileName, Muzzle.transform.position, Cc.transform.rotation);
                    //proj.transform.forward = Muzzle.transform.forward;
                    break;
                case WepType.Projectile:
                    var a = Database.Create(ProjectileName, Muzzle.transform.position, Cc.transform.rotation);
                    // Make this use muzzle.transform.forward too? I won't change it because I haven't tested it 
                    a.transform.forward = Cc.transform.forward;
                    a.GetComponent<Rigidbody>().AddForce(a.transform.forward * ProjBoost, ForceMode.Impulse);
                    break;
            }
        }
        void AltShoot()
        {
            switch (TypeAlt)
            {
                case WepTypeAlt.Hitscan:
                    var proj = Database.Create(ProjectileNameAlt, Muzzle.transform.position, Cc.transform.rotation);
                    break;
                case WepTypeAlt.Projectile:
                    var a = Database.Create(ProjectileNameAlt, Muzzle.transform.position, Cc.transform.rotation);
                    a.transform.forward = Cc.transform.forward;
                    a.GetComponent<Rigidbody>().AddForce(a.transform.forward*ProjBoostAlt,ForceMode.Impulse);
                    break;
            }
        }
    }
}
