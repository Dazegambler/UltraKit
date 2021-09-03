using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Core.ModMenu;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using ULTRAKIT.Loader.Registries;
using UnityEngine;

namespace ULTRAKIT.Core
{
    public static class CoreContent
    {
        public static void Initialize()
        {
            ModMenuInjector.Initialize();
            CreateBossAddon();
        }
        public static void CreateBossAddon()
        {
            Loader.Addon b = new Loader.Addon();

            b.Data = new Data.UKAddonData();
            b.Data.ModName = "Vanilla Bosses/Enemies";
            b.Data.Author = "UltraKit";
            b.Data.ModDesc = "Contains Enemies that cannot be spawned by the spawner arm";

            b.Bundle = ModMenu.ModMenuInjector.UIBundle;///SINCE EVERYTHING WILL BE REGISTERED OUTSIDE OF THE ADDON I'LL JUST USE THE UI BUNDLE

            b.Path = "Internal";

            b.enabled = true;

            Loader.AddonLoader.registry.Add(b, new List<UKContent>());
            Loader.AddonLoader.registry[b].AddRange(Enemies());
            Loader.AddonLoader.registry[b].AddRange(new List<UKContentWeapon>());
        }
        public static List<UKContentSpawnable> Enemies()
        {
            List<UKContentSpawnable> a = new List<UKContentSpawnable>();
            string[] List ={
                "MinosPrime",
                "V2",
                "Gabriel",
                "DroneFlesh",
                "FleshPrison",
                "MinosBoss",
                "Wicked",
                "DroneSkull Variant"
            };

            foreach (string _List in List) a.Add(EnemySpawnable(_List));

            return a;
        }
        public static  UKContentSpawnable EnemySpawnable(string Enemy)
        {

            UKContentSpawnable a = new UKContentSpawnable();

            a.Name = Enemy;
            a.type = Data.ScriptableObjects.Registry.Type.Enemy;

            a.Prefab = PrefabFind(Enemy);

            return a;
        }
        public static GameObject PrefabFind(string name)
        {
            //Find set Object in the prefabs
            GameObject[] Pool = Resources.FindObjectsOfTypeAll<GameObject>();
            GameObject a = new GameObject();
            foreach (GameObject obj in Pool)
            {
                if (obj.gameObject.name == name)
                {
                    if (obj.activeSelf != true) obj.SetActive(true);
                    a = obj;
                }
            }
            return a;
        }
    }
}
