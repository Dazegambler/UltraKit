using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using UnityEngine;

namespace ULTRAKIT.Core.BossSpawns
{
    public static class BossSpawnsInjector
    {
        static string[] List ={
            "MinosPrime",
            "V2",
            "Gabriel",
            "DroneFlesh",
            "Flesh Prison",
            "MinosBoss",
            "Wicked",
            "DroneSkull Variant"
        };

        public static void Initialize()
        {
            Loader.Addon b = new Loader.Addon();

            b.Data = new Data.UKAddonData();
            b.Data.ModName = "Vanilla Bosses/Enemies";
            b.Data.Author = "UltraKit";
            b.Data.ModDesc = "Contains Enemies that cannot be spawned by the spawner arm";

            b.Bundle = CoreContent.UIBundle;///SINCE EVERYTHING WILL BE REGISTERED OUTSIDE OF THE ADDON I'LL JUST USE THE UI BUNDLE

            b.Path = "Internal";

            b.enabled = true;

            Loader.AddonLoader.registry.Add(b, new List<UKContent>());
            Loader.AddonLoader.registry[b].AddRange(Enemies());
        }

        public static List<UKContentSpawnable> Enemies()
        {
            List<UKContentSpawnable> a = new List<UKContentSpawnable>();
            

            foreach (string item in List) a.Add(EnemySpawnable(item));

            return a;
        }
        public static UKContentSpawnable EnemySpawnable(string Enemy)
        {

            UKContentSpawnable a = ScriptableObject.CreateInstance<UKContentSpawnable>();

            a.Name = Enemy;
            a.type = Data.ScriptableObjects.Registry.Type.Enemy;

            a.Prefab = PrefabFind(Enemy);
            a.Icon = CoreContent.UIBundle.LoadAsset<Sprite>($"{Enemy}");

            return a;
        }
        public static GameObject PrefabFind(string name)
        {
            //Find set Object in the prefabs
            GameObject[] Pool = Resources.FindObjectsOfTypeAll<GameObject>();
            GameObject a = null;
            foreach (GameObject obj in Pool)
            {
                if (obj.gameObject.name == name)
                {
                    if (obj.gameObject.tag == "Enemy" || name == "Wicked")
                    {
                        if (obj.activeSelf != true) obj.SetActive(true);
                        a = obj;

                        // Fix lighting
                        var smrs = a.GetComponentsInChildren<SkinnedMeshRenderer>(true);
                        foreach (var item in smrs)
                        {
                            item.gameObject.layer = LayerMask.NameToLayer("Outdoors");
                        }
                    }
                }
            }


            return a;
        }
    }
}
