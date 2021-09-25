using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Data.CSharp_scripts
{
    class BasicWeapon : MonoBehaviour
    {
        CSharpAssetDatabase
            Database;

        GameObject
            Player;

        #region Projectile
        public enum WepType
        {
            Hitscan,
            Projectile,
        }

        public WepType
            Type = WepType.Hitscan;

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
            Player = GameObject.Find("Player");
            Database = new CSharpAssetDatabase();
            Projectile = Database.Create(ProjectileName);
            ProjectileAlt = Database.Create(ProjectileNameAlt);
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                for (int i = 0; i < Amount_to_shoot; i++)
                {
                    Invoke("Shoot", Delay);
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                for (int i = 0; i < Amount_to_shootAlt; i++)
                {
                    Invoke("AltShoot",DelayAlt);
                }
            }
        }
        void Shoot()
        {
            switch (Type)
            {
                case WepType.Hitscan:
                    Instantiate(Projectile, Muzzle.transform.position, Player.transform.rotation);
                    break;
                case WepType.Projectile:
                    var a = Instantiate(Projectile, Muzzle.transform.position, Player.transform.rotation);
                    a.transform.forward = Player.transform.forward;
                    a.GetComponent<Rigidbody>().AddForce(a.transform.forward * ProjBoost, ForceMode.Impulse);
                    break;
            }
        }
        void AltShoot()
        {
            switch (Type)
            {
                case WepType.Hitscan:
                    Instantiate(ProjectileAlt, Muzzle.transform.position, Player.transform.rotation);
                    break;
                case WepType.Projectile:
                    var a = Instantiate(ProjectileAlt, Muzzle.transform.position, Player.transform.rotation);
                    a.transform.forward = Player.transform.forward;
                    a.GetComponent<Rigidbody>().AddForce(a.transform.forward*ProjBoostAlt,ForceMode.Impulse);
                    break;
            }
        }
    }
}
