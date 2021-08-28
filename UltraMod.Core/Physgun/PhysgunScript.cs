using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace UltraMod.Core.Physgun
{
    public class PhysgunScript : MonoBehaviour
    {
        public LayerMask livingMask = 1 << LayerMask.NameToLayer("EnemyTrigger") | 1 << LayerMask.NameToLayer("Projectile") | 1 << LayerMask.NameToLayer("Item");
        public LayerMask limbMask = 1 << LayerMask.NameToLayer("Limb");
        public LineRenderer lr;

        InputManager inman;
        Transform head;

        Rigidbody target;
        Vector3 targOff;
        float targetDist;

        void Start()
        {
            inman = MonoSingleton<InputManager>.Instance;
            lr.enabled = false;
            lr.startWidth = 0.05f;
            head = Camera.main.transform;
        }

        void Update()
        {
            var beamStartOffset = new Vector3(0.25f, -0.25f, 0);
            lr.SetPosition(0, head.position + (head.transform.right * beamStartOffset.x) + (head.transform.up * beamStartOffset.y));

            if (target)
            {
                HoldTarget();
            }
            else
            {
                NoTarget();
            }

            
        }

        void NoTarget()
        {
            
            if (inman.InputSource.Fire1.IsPressed)
            {
                lr.enabled = true;

                lr.SetPosition(1, head.position + head.forward * 1000);

                RaycastHit hit;
                if (Physics.Raycast(head.position + head.forward, head.forward, out hit, Mathf.Infinity, livingMask))
                {
                    SetTarget(hit.transform.GetComponentInParent<Rigidbody>(), hit);
                    lr.SetPosition(1, hit.point);
                } 
                else if (Physics.Raycast(head.position + head.forward, head.forward, out hit, Mathf.Infinity, limbMask))
                {
                    if (hit.transform.GetComponent<EnemyIdentifierIdentifier>()?.eid?.dead ?? true)
                    {
                        SetTarget(hit.transform.GetComponentInParent<Rigidbody>(), hit);
                    }
                    

                    lr.SetPosition(1, hit.point);
                } 
                else if(Physics.Raycast(head.position + head.forward, head.forward, out hit, Mathf.Infinity)){
                    lr.SetPosition(1, hit.point);
                }

            }
            else
            {
                lr.enabled = false;
            }
        }

        void SetTarget(Rigidbody targ, RaycastHit hit)
        {
            if(targ == null)
            {
                return;
            }

            MonoSingleton<GunControl>.Instance.enabled = false;
            target = targ;
            targOff = target.transform.position - hit.point;
            target.GetComponentInChildren<GroundCheckEnemy>()?.ForceOff();
            targetDist = Vector3.Distance(head.position, target.position);
        }

        void HoldTarget()
        {
            var newPos = (head.position + head.forward * targetDist) + targOff;
            var lastPos = target.position;
            target.position = Vector3.Lerp(lastPos, newPos, 15f * Time.deltaTime);
            target.velocity = (target.position - lastPos) / Time.deltaTime;

            lr.SetPosition(1, target.position - targOff);

            if (inman.InputSource.Fire2.IsPressed)
            {
                target.transform.Rotate(head.transform.up, Mouse.current.delta.x.ReadValue(), Space.World);
                target.transform.Rotate(head.transform.right, Mouse.current.delta.y.ReadValue(), Space.World);
                MonoSingleton<CameraController>.Instance.enabled = false;
            }

            if (inman.InputSource.Fire2.WasCanceledThisFrame)
            {
                MonoSingleton<CameraController>.Instance.enabled = true;
            }

            targetDist += Mouse.current.scroll.y.ReadValue() * 3.5f * Time.deltaTime;
            targetDist = Mathf.Max(targetDist, 4);

            if (!inman.InputSource.Fire1.IsPressed)
            {
                lr.enabled = false;

                target.GetComponentInChildren<GroundCheckEnemy>()?.StopForceOff();
                target = null;
                MonoSingleton<GunControl>.Instance.enabled = true;
                MonoSingleton<CameraController>.Instance.enabled = true;
            }
        }
    }
}
