using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Data;
using UltraMod.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UltraMod.Core
{
    public class UltraModSpawnMenu : MonoBehaviour
    {
        public GUISkin skin;

        float ratio = Screen.width / 1920;

        List<UltraModItem>
            _props = new List<UltraModItem>(),
            _items = new List<UltraModItem>(),
            _weapons = new List<UltraModItem>(),
            _enemies = new List<UltraModItem>();

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

            if (Input.GetKeyDown(Bindings.Toggle.Value))
            {
                toggle = !toggle;
            }
        }

        public void OnGUI()
        {
            if (SceneManager.GetActiveScene().name != "Intro" && SceneManager.GetActiveScene().name != "Main Menu")
            {
                if (GUI.skin != skin)
                {
                    GUI.skin = skin;
                }
                switch (toggle)
                {
                    case true:
                        GUI.Window(0, new Rect(ratio + 0 + (Screen.width / 5), ratio + 0 + (Screen.height / 10), 735, Screen.height / 1.5f), TabBundles, "");
                        break;
                }

            }
        }

        public void TabBundles(int id)
        {
            //Currently Not Working and might not be needed in the future
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
                            if (_list != active.LoadAllAssets<UltraModItem>()) ListObjects();
                            GUI.Label(new Rect(155, 0, 140, 30), $"<i>MAPOBJECTS:{_props.Count}</i>");
                            for (int p = 0; p < _props.Count; p++)
                            {
                                switch (GUI.Button(new Rect(155, 35 + (35 * p), 140, 30), _props[p].Name))
                                {
                                    case true:
                                        trgt = _props[p].Prefab;
                                        break;
                                }
                            }
                            GUI.Label(new Rect(300, 0, 140, 30), $"<i>PROPS:{_items.Count}</i>");
                            for (int i = 0; i < _items.Count; i++)
                            {
                                switch (GUI.Button(new Rect(300, 35 + (35 * i), 140, 30), _items[i].Name))
                                {
                                    case true:
                                        trgt = _items[i].Prefab;
                                        break;
                                }
                            }
                            GUI.Label(new Rect(445, 0, 140, 30), $"<i>ENEMIES:{_enemies.Count}</i>");
                            for (int e = 0; e < _enemies.Count; e++)
                            {
                                switch (GUI.Button(new Rect(445, 35 + (35 * e), 140, 30), _enemies[e].Name))
                                {
                                    case true:
                                        trgt = _enemies[e].Prefab;
                                        break;
                                }
                            }
                            GUI.Label(new Rect(590, 0, 140, 30), $"<i>WEAPONS:{_weapons.Count}</i>");
                            for (int w = 0; w < _weapons.Count; w++)
                            {
                                switch (GUI.Button(new Rect(590, 35 + (35 * w), 140, 30), _weapons[w].Name))
                                {
                                    case true:
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
            _list = active.LoadAllAssets<UltraModItem>();
            _props = new List<UltraModItem>();
            _items = new List<UltraModItem>();
            _enemies = new List<UltraModItem>();
            _weapons = new List<UltraModItem>();
            for (int i = 0; i < _list.Length; i++)
            {
                switch (_list[i].type)
                {
                    case ContentType.Spawnable://PROP
                        _props.Add(_list[i]);
                        break;
                    //case 1://ITEM
                    //    _items.Add(_list[i]);
                    //    switch (_list[i].Prefab.TryGetComponent<ItemIdentifier>(out ItemIdentifier iid))
                    //    {
                    //        case false:
                    //            _list[i].Prefab.AddComponent<ItemIdentifier>().itemType = ItemType.Soap;
                    //            break;
                    //    }
                    //    break;
                    case ContentType.Weapon://WEAPONS
                        _weapons.Add(_list[i]);
                        break;
                    case ContentType.Enemy://ENEMIES
                        switch (_list[i].Prefab.TryGetComponent<EnemyIdentifier>(out EnemyIdentifier eid))
                        {
                            case false:
                                _list[i].Prefab.AddComponent<EnemyIdentifier>();
                                break;
                        }
                        _enemies.Add(_list[i]);
                        break;
                    default:
                        Debug.LogWarning($"FAILED TO FIND SUITABLE TYPE FOR {_list[i].Name} PLEASE CONTACT THE BUNDLE CREATOR");
                        break;
                }
            }
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
}
