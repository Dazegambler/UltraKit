using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data.Components;
using UnityEngine;

namespace ULTRAKIT.Data.CSharp_scripts
{

    /// <summary>
    /// TODO:
    /// ANIMATIONS
    /// </summary>
    class MeleeWeaponSimple : UKScript
    {
        public float
            AnimationSpeed,
            Radius,
            Range,
            Damage;

        public GameObject
            Shield;

        GameObject
            Cc;

        RaycastHit[]
            Hits;

        List<Transform>
            HitEids = new List<Transform>();

        void Awake()
        {
            Cc = GameObject.Find("Main Camera");
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Invoke("Hit", AnimationSpeed);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Invoke("Block",AnimationSpeed);
            }
            else
            {

            }
        }
        void Hit()
        {
            Hits = Physics.SphereCastAll(Cc.transform.forward,Radius,Cc.transform.forward,Range,4096);
            foreach(RaycastHit Hit in Hits)
            {
                HitEids.Add(Hit.transform);
            }
        }
        void Block()
        {

        }
    }
}
