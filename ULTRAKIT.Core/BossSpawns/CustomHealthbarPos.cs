using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Core.BossSpawns
{
    public class CustomHealthbarPos : MonoBehaviour
    {
        public float size = 0.1f;
        public Vector3 offset;
        public GameObject enemy, barObj;
        public Camera playerCam;

        void OnEnable()
        {
            barObj.transform.localScale = Vector3.one * size;
            playerCam = MonoSingleton<CameraController>.Instance.GetComponent<Camera>();
        }

        void Update()
        {
            var newPos = enemy.transform.position + offset;
            var diff = (newPos - playerCam.transform.position).normalized;
            var screenPos = playerCam.WorldToViewportPoint(newPos);

            var isOnScreen = screenPos.x > 0 && screenPos.x < 1 && screenPos.y > 0 && screenPos.y < 1;
            Debug.Log(isOnScreen);

            barObj.SetActive(isOnScreen);
            barObj.transform.position = new Vector3(screenPos.x * Screen.width, screenPos.y * Screen.height);
        }
    }
}
