using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Core.BossSpawns
{
    static class V2Utils
    {
        static GameObject 
            GoreZone,
            Escape,
            nail;

        static bool 
            Greed;


        static void DifCheck(bool Greed)
        {
            V2 v2 = BossSpawnsInjector.PrefabFind("V2").GetComponent<V2>();
            Machine mch = v2.gameObject.GetComponent<Machine>();
            List<GameObject> weps = new List<GameObject>(v2.weapons);

            if(nail == null){
                foreach (Transform obj in v2.gameObject.GetComponentsInChildren<Transform>(true)){
                    if (obj.name == "Nailgun"){
                        nail = obj.gameObject;
                    }
                }
            }
            else{
                if(Greed == true){
                    v2.secondEncounter = true;

                    mch.health = 80f;

                    nail.SetActive(true);

                    if (weps.Contains(nail) == false){
                        weps.Add(nail);
                        v2.weapons = weps.ToArray();
                    }
                }
                else{
                    v2.secondEncounter = false;

                    mch.health = 40f;

                    nail.SetActive(false);

                    if(weps.Contains(nail) == true){
                        weps.Remove(nail);
                        v2.weapons = weps.ToArray();
                    }
                }
            }
        }
        static void DeathFix()
        {
            if(GoreZone != null && Escape != null){
                foreach(Transform obj in GoreZone.transform)
                {
                    if(obj.name == "V2(Clone)"){
                        //obj.gameObject.GetComponent<V2>().escapeTarget = Escape;
                    }
                }
            }
            else{
                //Escape
                GoreZone = GameObject.Find("DebugGoreZone");
            }
        } 
    }
}
