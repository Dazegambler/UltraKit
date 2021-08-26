using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UltraMod
{
    [BepInPlugin("ULTRA.MOD", "ULTRAMOD", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        //AssetBundle
        static AssetBundle UIBundle = Plugin.LoadAssetBundle(Properties.Resource1.ultramod);
        GUISkin skin = UIBundle.LoadAsset<GUISkin>("UIUltraMod");
        //General
        string FilePath = "AssetBundles";
        string[] files;
        List<AssetBundle> Bundles = new List<AssetBundle>(); AssetBundle[] _Bundles;
        List<GameObject>
            _props = new List<GameObject>(),
            _items = new List<GameObject>(),
            _enemies = new List<GameObject>(); 
        GameObject[] list, props, items, enemies;
        AssetBundle active;
        GameObject trgt;
        bool toggle;
        Transform TractorObj;LineRenderer L;
        GameObject Cc;
        int mod;
        string _mod;
        //UI
        float sWidth = Screen.width,ratio;
        //Vector2 scrollprops, scrollitems, scrollenemies;
        //Configs
        ConfigEntry<KeyCode> Toggle, Spawn,SetTractorTrgt,ResetTractor;
        public void Start()
        {
            switch (Directory.Exists(FilePath))
            {
                case false:
                    Debug.LogWarning("Asset Bundle Directory Not Found...Creating Directory");
                    Directory.CreateDirectory(FilePath);
                    break;
                case true:
                    Debug.LogWarning("LOADING ASSETBUNDLES");
                    files = Directory.GetFiles(FilePath, "*.*", SearchOption.TopDirectoryOnly);
                    foreach (string file in files)
                    {
                        Debug.LogWarning($"Loading {file}");
                        Bundles.Add(AssetBundle.LoadFromFile(file));
                    }
                    Debug.LogWarning("FINISHED LOADING ASSETBUNDLES");
                    break;
            }
            Toggle = Config.Bind("Binds","Toggle",KeyCode.Backspace);
            Spawn = Config.Bind("Binds","Spawn",KeyCode.I);
            SetTractorTrgt = Config.Bind("Binds","TractorObj Set",KeyCode.Greater);
            ResetTractor = Config.Bind("Binds","TractorObj Reset",KeyCode.Less);
        }
        public void Update()
        {
            switch(SceneManager.GetActiveScene().name)
            {
                case "Intro":break;
                case "Main Menu":break;
                default:
                    if (Cc != GameObject.Find("Main Camera")) Cc = GameObject.Find("Main Camera");
                    ratio = sWidth / 1920;
                    _Bundles = Bundles.ToArray();
                    if (Time.timeSinceLevelLoad < .2f) TractorObj = null;
                    switch (Input.GetKeyDown(Toggle.Value))
                    {
                        case true:
                            toggle = !toggle;
                            break;
                    }
                    switch (Input.GetKeyDown(Spawn.Value))
                    {
                        case true:
                            Inst();
                            break;
                    }
                    switch (Input.GetKeyDown(SetTractorTrgt.Value))
                    {
                        case true:
                            TractorSet();
                            break;
                    }
                    switch (Input.GetKeyDown(ResetTractor.Value))
                    {
                        case true:
                            TractorObj = null;
                            break;
                    }
                    switch (TractorObj)
                    {
                        case null:
                            break;
                        default:
                            TractorBeam();
                            break;
                    }
                    switch (mod)
                    {
                        case 0:
                            _mod = "Rotation";
                            break;
                        case 1:
                            _mod = "Translation";
                            break;
                        case 2:
                            _mod = "Scale";
                            break;
                    }
                    break;
            }
        }
        public void OnGUI()
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Intro":break;
                case "Main Menu":break;
                default:
                    if (GUI.skin != skin)
                    {
                        GUI.skin = skin;
                    }
                    switch (toggle)
                    {
                        case true:
                            GUI.Window(0, new Rect(ratio+0 + (Screen.width / 5), ratio+0 + (Screen.height / 10),450, Screen.height / 1.5f), TabBundles, "");
                            break;
                    }
                    switch (TractorObj)
                    {
                        case null:
                            break;
                        default:
                            Text(_mod,new Rect(Screen.width-200,Screen.height-50,200,50),25,"White");
                            break;
                    }
                    break;
            }
        }
        private void Text(string txt, Rect Pos, int fntsize, string col)
        {
            //Generates Funky text with custom colors
            GUI.Label(new Rect(Pos.x + 1f, Pos.y - 1f, Pos.width, Pos.height), $"<size={fntsize}><color=black>{txt}</color></size>");
            GUI.Label(Pos, $"<size={fntsize}><color={col}>{txt}</color></size>");
        }
        public void TabBundles(int id)
        {
            switch (id)
            {
                case 0:
                    GUI.Label(new Rect(5, 0, 140, 30), $"<i>BUNDLES:</i>{_Bundles.Length}");
                    for (int i = 0; i < _Bundles.Length; i++)
                    {
                        switch (GUI.Button(new Rect(5, 35 + (35 * i), 140, 30), _Bundles[i].name))
                        {
                            case true:
                                active = _Bundles[i];
                                break;
                        }
                    }
                    switch (active)
                    {
                        case null:
                            break;
                        default:
                            if (list != active.LoadAllAssets<GameObject>()) ListObjects();
                            GUI.Label(new Rect(155, 0, 140, 30), $"<i>MAPOBJECTS:{props.Length}</i>");
                            for (int p = 0; p < props.Length; p++)
                            {
                                switch (GUI.Button(new Rect(155, 35 + (35 * p), 140, 30), props[p].name))
                                {
                                    case true:
                                        trgt = props[p];
                                        break;
                                }
                            }
                            GUI.Label(new Rect(300, 0, 140, 30), $"<i>PROPS:{items.Length}</i>");
                            for (int i = 0; i < items.Length; i++)
                            {
                                switch (GUI.Button(new Rect(300, 35 + (35 * i), 140, 30), items[i].name))
                                {
                                    case true:
                                        trgt = items[i];
                                        break;
                                }
                            }
                            GUI.Label(new Rect(445, 0, 140, 30), $"<i>ENEMIES:{enemies.Length}</i>");
                            for (int e = 0; e < enemies.Length; e++)
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
        void TractorSet()
        {
            GameObject Cc = GameObject.Find("Main Camera");
            RaycastHit beam;
            switch (Physics.Raycast(Cc.transform.position, Cc.transform.forward, out beam, 200f))
            {
                case true:
                    TractorObj = beam.transform;
                    break;
            }
        }
        void TractorBeam()
        {
            switch (L)
            {
                case null:
                    switch(GameObject.Find("Player").TryGetComponent<LineRenderer>(out L))
                    {
                        case true:
                            L = GameObject.Find("Player").GetComponent<LineRenderer>();
                            break;
                        case false:
                            L = GameObject.Find("Player").AddComponent<LineRenderer>();
                            break;
                    }
                    break;
                default:
                    switch (GameObject.Find("Player").TryGetComponent<LineRenderer>(out L))
                    {
                        case true:
                            L = GameObject.Find("Player").GetComponent<LineRenderer>();
                            break;
                        case false:
                            L = GameObject.Find("Player").AddComponent<LineRenderer>();
                            break;
                    }
                    if (L.startWidth != .65f) L.startWidth = .65f;
                    if (L.endWidth != 0) L.endWidth = 0;
                    L.useWorldSpace = true;
                    switch (TractorObj)
                    {
                        case null:
                            break;
                        default:
                            L.startColor = Color.green;
                            L.endColor = Color.white;
                            L.SetPosition(1,new Vector3(TractorObj.transform.position.x,TractorObj.transform.position.y+5,TractorObj.transform.position.z));
                            L.SetPosition(0,new Vector3(TractorObj.transform.position.x,TractorObj.transform.position.y+15,TractorObj.transform.position.z));
                            switch (Input.GetKeyDown(KeyCode.Insert))
                            {
                                case true:
                                    if (mod != 0) mod--;
                                    break;
                            }
                            switch (Input.GetKeyDown(KeyCode.PageUp))
                            {
                                case true:
                                    if (mod != 2) mod++;
                                    break;
                            }
                            switch (Input.GetKey(KeyCode.Keypad4))
                            {
                                case true:
                                    switch (mod)
                                    {
                                        case 0:
                                            TractorObj.transform.rotation = new Quaternion(TractorObj.transform.rotation.x, TractorObj.transform.rotation.y-.02f, TractorObj.transform.rotation.z, TractorObj.transform.rotation.w);
                                            break;
                                        case 1:
                                            TractorObj.transform.position = new Vector3(TractorObj.transform.position.x-.2f, TractorObj.transform.position.y, TractorObj.transform.position.z);
                                            break;
                                        case 2:
                                            TractorObj.transform.localScale = new Vector3(TractorObj.transform.localScale.x -.2f, TractorObj.transform.localScale.y, TractorObj.transform.localScale.z);
                                            break;
                                    }
                                    break;
                            }//NUMPAD LEFT
                            switch (Input.GetKey(KeyCode.Keypad6))
                            {
                                case true:
                                    switch (mod)
                                    {
                                        case 0:
                                            TractorObj.transform.rotation = new Quaternion(TractorObj.transform.rotation.x, TractorObj.transform.rotation.y + .02f, TractorObj.transform.rotation.z, TractorObj.transform.rotation.w);
                                            break;
                                        case 1:
                                            TractorObj.transform.position = new Vector3(TractorObj.transform.position.x + .2f, TractorObj.transform.position.y, TractorObj.transform.position.z);
                                            break;
                                        case 2:
                                            TractorObj.transform.localScale = new Vector3(TractorObj.transform.localScale.x + .1f, TractorObj.transform.localScale.y, TractorObj.transform.localScale.z);
                                            break;
                                    }
                                    break;
                            }//NUMPAD RIGHT
                            switch (Input.GetKey(KeyCode.Keypad8))
                            {
                                case true:
                                    switch (mod)
                                    {
                                        case 0:
                                            TractorObj.transform.rotation = new Quaternion(TractorObj.transform.rotation.x+.02f, TractorObj.transform.rotation.y, TractorObj.transform.rotation.z, TractorObj.transform.rotation.w);
                                            break;
                                        case 1:
                                            TractorObj.transform.position = new Vector3(TractorObj.transform.position.x, TractorObj.transform.position.y+.2f, TractorObj.transform.position.z);
                                            break;
                                        case 2:
                                            TractorObj.transform.localScale = new Vector3(TractorObj.transform.localScale.x, TractorObj.transform.localScale.y+.1f, TractorObj.transform.localScale.z);
                                            break;
                                    }
                                    break;
                            }//NUMPAD UP
                            switch (Input.GetKey(KeyCode.Keypad2))
                            {
                                case true:
                                    switch (mod)
                                    {
                                        case 0:
                                            TractorObj.transform.rotation = new Quaternion(TractorObj.transform.rotation.x-.02f, TractorObj.transform.rotation.y, TractorObj.transform.rotation.z, TractorObj.transform.rotation.w);
                                            break;
                                        case 1:
                                            TractorObj.transform.position = new Vector3(TractorObj.transform.position.x, TractorObj.transform.position.y -.2f, TractorObj.transform.position.z);
                                            break;
                                        case 2:
                                            TractorObj.transform.localScale = new Vector3(TractorObj.transform.localScale.x, TractorObj.transform.localScale.y-.1f, TractorObj.transform.localScale.z);
                                            break;
                                    }
                                    break;
                            }//NUMPAD DOWN
                            switch (Input.GetKey(KeyCode.Keypad7))
                            {
                                case true:
                                    switch (mod)
                                    {
                                        case 0:
                                            TractorObj.transform.rotation = new Quaternion(TractorObj.transform.rotation.x, TractorObj.transform.rotation.y, TractorObj.transform.rotation.z-.02f, TractorObj.transform.rotation.w);
                                            break;
                                        case 1:
                                            TractorObj.transform.position = new Vector3(TractorObj.transform.position.x, TractorObj.transform.position.y, TractorObj.transform.position.z-.2f);
                                            break;
                                        case 2:
                                            TractorObj.transform.localScale = new Vector3(TractorObj.transform.localScale.x, TractorObj.transform.localScale.y, TractorObj.transform.localScale.z-.1f);
                                            break;
                                    }
                                    break;
                            }//NUMPAD TOP LEFT
                            switch (Input.GetKey(KeyCode.Keypad9))
                            {
                                case true:
                                    switch (mod)
                                    {
                                        case 0:
                                            TractorObj.transform.rotation = new Quaternion(TractorObj.transform.rotation.x, TractorObj.transform.rotation.y, TractorObj.transform.rotation.z+.02f, TractorObj.transform.rotation.w);
                                            break;
                                        case 1:
                                            TractorObj.transform.position = new Vector3(TractorObj.transform.position.x, TractorObj.transform.position.y, TractorObj.transform.position.z+.2f);
                                            break;
                                        case 2:
                                            TractorObj.transform.localScale = new Vector3(TractorObj.transform.localScale.x, TractorObj.transform.localScale.y, TractorObj.transform.localScale.z+.1f);
                                            break;
                                    }
                                    break;
                            }//NUMPAD TOP RIGHT
                            switch (Input.GetKey(KeyCode.Keypad5))
                            {
                                case true:
                                    switch (TractorObj)
                                    {
                                        case null:
                                            break;
                                        default:
                                            Destroy(TractorObj.gameObject);
                                            TractorObj = null;
                                            break;
                                    }
                                    break;
                            }//NUMPAD CENTER
                            switch (Input.GetKeyDown(KeyCode.Keypad0))
                            {
                                case true:
                                    switch (TractorObj)
                                    {
                                        case null:
                                            break;
                                        default:
                                            RaycastHit _Spawnpos;
                                            switch (Physics.Raycast(Cc.transform.position, Cc.transform.forward, out _Spawnpos, 200f))
                                            {
                                                case true:
                                                    Instantiate(TractorObj.gameObject, new Vector3(_Spawnpos.point.x, _Spawnpos.point.y + 1.5f, _Spawnpos.point.z), Quaternion.identity);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }
        void Inst()
        {
            RaycastHit _Spawnpos;
            switch (Physics.Raycast(Cc.transform.position, Cc.transform.forward, out _Spawnpos, 200f))
            {
                case true:
                    Instantiate(trgt,new Vector3(_Spawnpos.point.x,_Spawnpos.point.y+1.5f,_Spawnpos.point.z),Quaternion.identity);
                    break;
            }
        }
        void ListObjects()
        {
            list = active.LoadAllAssets<GameObject>();
            _props = new List<GameObject>();
            _items = new List<GameObject>();
            _enemies = new List<GameObject>();
            for (int i = 0; i < list.Length; i++)
            {
                switch (list[i].layer)
                {
                    case 24://MAP
                        //Debug.LogWarning($"ADDING {list[i].name} TO PROPS");
                        if(list[i].tag != "Floor")list[i].tag = "Floor";
                        _props.Add(list[i]);
                        break;
                    case 22://PROP
                        //Debug.LogWarning($"ADDING {list[i].name} TO ITEMS");
                        _items.Add(list[i]);
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
                        _enemies.Add(list[i]);
                        break;
                    default:
                        //Debug.LogWarning($"COULD NOT FIND TAG FOR {list[i].name} IN BUNDLE {active.name}...PLEASE CONTACT BUNDLE MAKER ABOUT SAID BUNDLE");
                        break;
                }
            }
            props = _props.ToArray();
            items = _items.ToArray();
            enemies = _enemies.ToArray();
        }
        static AssetBundle LoadAssetBundle(byte[] Bytes)
        {
            if (Bytes == null) throw new ArgumentNullException(nameof(Bytes));
            var bundle = AssetBundle.LoadFromMemory(Bytes);
            return bundle;
        }
    }
}
