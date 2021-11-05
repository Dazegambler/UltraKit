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
            if(enemy == null)
            {
                this.enabled = false;
                barObj.SetActive(false);
                return;
            }

            var newPos = enemy.transform.position + offset;
            var diff = (newPos - playerCam.transform.position).normalized;
            var screenPos = playerCam.WorldToViewportPoint(newPos);

            var isOnScreen = Vector3.Dot(diff, playerCam.transform.forward) > 0f;

            barObj.SetActive(isOnScreen);
            barObj.transform.position = new Vector3(screenPos.x * Screen.width, screenPos.y * Screen.height);
        }
    }
}
