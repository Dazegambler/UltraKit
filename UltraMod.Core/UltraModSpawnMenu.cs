using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UltraMod.Core
{
    public class UltraModSpawnMenu : MonoBehaviour
    {
        public GUISkin skin;

        float ratio = Screen.width / 1920;

        List<GameObject>
            props = new List<GameObject>(),
            items = new List<GameObject>(),
            enemies = new List<GameObject>();

        List<UltraModItem>
            _props = new List<UltraModItem>(),
            _items = new List<UltraModItem>(),
            _enemies = new List<UltraModItem>();

        GameObject[] list;

        UltraModItem[] _list;

        AssetBundle active;
        GameObject trgt;

        bool toggle;

        public void Start()
        {

        }

        public void Update()
        {
            switch (Input.GetKeyDown(Bindings.Spawn.Value))
            {
                case true:
                    Inst();
                    break;
            }

            if(Input.GetKeyDown(Bindings.Toggle.Value))
            {
               toggle = !toggle;
            }
        }

        public void OnGUI()
        {
            if(SceneManager.GetActiveScene().name != "Intro" && SceneManager.GetActiveScene().name != "Main Menu") { 
                if (GUI.skin != skin)
                {
                    GUI.skin = skin;
                }
                switch (toggle)
                {
                    case true:
                        GUI.Window(0, new Rect(ratio + 0 + (Screen.width / 5), ratio + 0 + (Screen.height / 10), 590, Screen.height / 1.5f), TabBundles, "");
                        break;
                }
                
            }
        }

        public void TabBundles(int id)
        {
            switch (id)
            {
                case 0:

                    GUI.Label(new Rect(5, 0, 140, 30), $"<i>BUNDLES:</i>{AddonLoader.assetBundles.Count}");
                    for (int i = 0; i < AddonLoader.assetBundles.Count; i++)
                    {
                        
                        switch (GUI.Button(new Rect(5, 35 + (35 * i), 140, 30), AddonLoader.assetBundles[i].name))
                        {
                            case true:
                                active = AddonLoader.assetBundles[i];
                                break;
                        }
                    }
                    switch (active)
                    {
                        case null:
                            break;
                        default:
                            if (list != active.LoadAllAssets<GameObject>()) ListObjects();
                            GUI.Label(new Rect(155, 0, 140, 30), $"<i>MAPOBJECTS:{_props.Count}</i>");
                            for (int p = 0; p < _props.Count; p++)//MODIFIED
                            {
                                switch (GUI.Button(new Rect(155, 35 + (35 * p), 140, 30), _props[p].Name))
                                {
                                    case true:
                                        trgt = _props[p].Prefab;
                                        break;
                                }
                            }
                            GUI.Label(new Rect(300, 0, 140, 30), $"<i>PROPS:{items.Count}</i>");
                            for (int i = 0; i < items.Count; i++)
                            {
                                switch (GUI.Button(new Rect(300, 35 + (35 * i), 140, 30), items[i].name))
                                {
                                    case true:
                                        trgt = items[i];
                                        break;
                                }
                            }
                            GUI.Label(new Rect(445, 0, 140, 30), $"<i>ENEMIES:{enemies.Count}</i>");
                            for (int e = 0; e < enemies.Count; e++)
                            {
                                switch (GUI.Button(new Rect(445, 35 + (35 * e), 140, 30), enemies[e].name))
                                {
                                    case true:
                                        trgt = enemies[e];
                                        break;
                                }
                            }
                            break;
                    }
                    break;
            }
        }

        void ListObjects()
        {
            //list = active.LoadAllAssets<GameObject>();
            _list = active.LoadAllAssets<UltraModItem>();
            //props = new List<GameObject>();
            _props = new List<UltraModItem>();
            //items = new List<GameObject>();
            _items = new List<UltraModItem>();
            //enemies = new List<GameObject>();
            _enemies = new List<UltraModItem>();
            for(int i = 0; i < _list.Length; i++)
            {
                switch (_list[i].type)
                {
                    case 0://PROP
                        _props.Add(_list[i]);
                        break;
                }
            }
            /*for (int i = 0; i < list.Length; i++)
            {
                switch (list[i].layer)
                {
                    case 24://MAP
                        //Debug.LogWarning($"ADDING {list[i].name} TO PROPS");
                        if (list[i].tag != "Floor") list[i].tag = "Floor";
                        props.Add(list[i]);
                        break;
                    case 22://PROP
                        //Debug.LogWarning($"ADDING {list[i].name} TO ITEMS");
                        items.Add(list[i]);
                        switch (list[i].TryGetComponent<ItemIdentifier>(out ItemIdentifier iid))
                        {
                            case false:
                                list[i].AddComponent<ItemIdentifier>().itemType = ItemType.Soap;
                                break;
                        }
                        break;
                    case 12://ENEMY
                        //Debug.LogWarning($"ADDING {list[i].name} TO ENEMIES");
                        switch (list[i].TryGetComponent<EnemyIdentifier>(out EnemyIdentifier eid))
                        {
                            case false:
                                list[i].AddComponent<EnemyIdentifier>();
                                break;
                        }
                        enemies.Add(list[i]);
                        break;
                    default:
                        //Debug.LogWarning($"COULD NOT FIND TAG FOR {list[i].name} IN BUNDLE {active.name}...PLEASE CONTACT BUNDLE MAKER ABOUT SAID BUNDLE");
                        break;
                }
            }*/
        }

        void Inst()
        {
            RaycastHit _Spawnpos;
            switch (Physics.Raycast(transform.position, transform.forward, out _Spawnpos, 200f))
            {
                case true:
                    Instantiate(trgt, new Vector3(_Spawnpos.point.x, _Spawnpos.point.y + 1.5f, _Spawnpos.point.z), Quaternion.identity);
                    break;
            }
        }
    }
    public class UltraModItem : ScriptableObject
    {
        public GameObject Prefab;
        public Texture2D Icon;
        public int type;
        public string Name;
        [TextArea]
        public string Desc;
    }
}
