﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Data
{
    public static class AssetDatabase
    {
        public static Dictionary<string, GameObject> assets = new Dictionary<string, GameObject>();

        public static string[] _assets = {
            "RevolverBeam",
            "RevolverBeamSuper",
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
            "HarpoonMalicious"
        };

        public static void Initialiaze()
        {
            foreach(string Name in _assets){
                assets.Add(Name,AssetFind(Name));
            }
        }
        private static GameObject AssetFind(string name)
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
