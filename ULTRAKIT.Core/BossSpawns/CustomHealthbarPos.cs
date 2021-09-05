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
            barObj.transform.position = playerCam.WorldToScreenPoint(newPos);

            barObj.SetActive(Vector3.Dot(diff, playerCam.transform.forward) > 0.05);
        }
    }
}
