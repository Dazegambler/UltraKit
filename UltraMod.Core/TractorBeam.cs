using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UltraMod.Core
{
    public class TractorBeam : MonoBehaviour
    {
        Transform TractorObj; 
        LineRenderer L;
        int mod;
        string _mod;

        public void OnGUI()
        {
            switch (TractorObj)
            {
                case null:
                    break;
                default:
                    GUIUtil.Text(_mod, new Rect(Screen.width - 200, Screen.height - 50, 200, 50), 25, "White");
                    break;
            }
        }

        public void Update()
        {
            if (SceneManager.GetActiveScene().name != "Intro" && SceneManager.GetActiveScene().name != "Main Menu")
            {
                
                if (Time.timeSinceLevelLoad < .2f)
                {
                    TractorObj = null;
                }

                switch (Input.GetKeyDown(Bindings.SetTractorTrgt.Value))
                {
                    case true:
                        Debug.Log("WORKING");
                        TractorSet();
                        break;
                }
                switch (Input.GetKeyDown(Bindings.ResetTractor.Value))
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
                        DoBeam();
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
            }
        }

        void TractorSet()
        {
            GameObject gameObject = GameObject.Find("Main Camera");
            RaycastHit beam;
            switch (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out beam, 200f))
            {
                case true:
                    TractorObj = beam.transform;
                    break;
            }
        }
        void DoBeam()
        {
            switch (L)
            {
                case null:
                    switch (GameObject.Find("Player").TryGetComponent<LineRenderer>(out L))
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
                            L.SetPosition(1, new Vector3(TractorObj.transform.position.x, TractorObj.transform.position.y + 5, TractorObj.transform.position.z));
                            L.SetPosition(0, new Vector3(TractorObj.transform.position.x, TractorObj.transform.position.y + 15, TractorObj.transform.position.z));
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
                                            TractorObj.transform.rotation = new Quaternion(TractorObj.transform.rotation.x, TractorObj.transform.rotation.y - .02f, TractorObj.transform.rotation.z, TractorObj.transform.rotation.w);
                                            break;
                                        case 1:
                                            TractorObj.transform.position = new Vector3(TractorObj.transform.position.x - .2f, TractorObj.transform.position.y, TractorObj.transform.position.z);
                                            break;
                                        case 2:
                                            TractorObj.transform.localScale = new Vector3(TractorObj.transform.localScale.x - .2f, TractorObj.transform.localScale.y, TractorObj.transform.localScale.z);
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
                                            TractorObj.transform.rotation = new Quaternion(TractorObj.transform.rotation.x + .02f, TractorObj.transform.rotation.y, TractorObj.transform.rotation.z, TractorObj.transform.rotation.w);
                                            break;
                                        case 1:
                                            TractorObj.transform.position = new Vector3(TractorObj.transform.position.x, TractorObj.transform.position.y + .2f, TractorObj.transform.position.z);
                                            break;
                                        case 2:
                                            TractorObj.transform.localScale = new Vector3(TractorObj.transform.localScale.x, TractorObj.transform.localScale.y + .1f, TractorObj.transform.localScale.z);
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
                                            TractorObj.transform.rotation = new Quaternion(TractorObj.transform.rotation.x - .02f, TractorObj.transform.rotation.y, TractorObj.transform.rotation.z, TractorObj.transform.rotation.w);
                                            break;
                                        case 1:
                                            TractorObj.transform.position = new Vector3(TractorObj.transform.position.x, TractorObj.transform.position.y - .2f, TractorObj.transform.position.z);
                                            break;
                                        case 2:
                                            TractorObj.transform.localScale = new Vector3(TractorObj.transform.localScale.x, TractorObj.transform.localScale.y - .1f, TractorObj.transform.localScale.z);
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
                                            TractorObj.transform.rotation = new Quaternion(TractorObj.transform.rotation.x, TractorObj.transform.rotation.y, TractorObj.transform.rotation.z - .02f, TractorObj.transform.rotation.w);
                                            break;
                                        case 1:
                                            TractorObj.transform.position = new Vector3(TractorObj.transform.position.x, TractorObj.transform.position.y, TractorObj.transform.position.z - .2f);
                                            break;
                                        case 2:
                                            TractorObj.transform.localScale = new Vector3(TractorObj.transform.localScale.x, TractorObj.transform.localScale.y, TractorObj.transform.localScale.z - .1f);
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
                                            TractorObj.transform.rotation = new Quaternion(TractorObj.transform.rotation.x, TractorObj.transform.rotation.y, TractorObj.transform.rotation.z + .02f, TractorObj.transform.rotation.w);
                                            break;
                                        case 1:
                                            TractorObj.transform.position = new Vector3(TractorObj.transform.position.x, TractorObj.transform.position.y, TractorObj.transform.position.z + .2f);
                                            break;
                                        case 2:
                                            TractorObj.transform.localScale = new Vector3(TractorObj.transform.localScale.x, TractorObj.transform.localScale.y, TractorObj.transform.localScale.z + .1f);
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
                                            switch (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out _Spawnpos, 200f))
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

    }
}
