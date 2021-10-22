using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    public class UKProxyCheatsController : UKProxyComponentAbstract<CheatsController>
    {
        IEnumerator SetMovement(bool value, string type)
        {
            CheatsController.Instance.GetComponentInParent<NewMovement>().StopMovement();
            yield return new WaitForEndOfFrame();
            CheatsController.Instance.GetComponentInParent<NewMovement>().activated = !value;
            yield return new WaitForEndOfFrame();
            CheatsController.Instance.GetComponentInParent<NewMovement>().enabled = !value;
            if (type == "noclip")
            {
                CheatsController.Instance.GetComponentInParent<Rigidbody>().useGravity = true;
                CheatsController.Instance.GetComponentInParent<Rigidbody>().isKinematic = value;
                if (value) { CheatsController.Instance.GetComponentInParent<Rigidbody>().Sleep(); }
                else { CheatsController.Instance.GetComponentInParent<Rigidbody>().WakeUp(); }
                CheatsController.Instance.SetPrivate("flight", false);
            }
            else if (type == "flight")
            {
                CheatsController.Instance.GetComponentInParent<Rigidbody>().isKinematic = false;
                CheatsController.Instance.GetComponentInParent<Rigidbody>().WakeUp();
                CheatsController.Instance.GetComponentInParent<Rigidbody>().useGravity = !value;
                CheatsController.Instance.SetPrivate("noclip", false);
            }
            CheatsController.Instance.SetPrivate(type, value);
            CheatsController.Instance.RenderInfo();
            yield return null;
        }

        [MoonSharpHidden]
        public UKProxyCheatsController(CheatsController target) : base(target)
        {
        }
        public bool BlindEnemies
        {
            get => CheatsController.BlindEnemies;
            set
            {
                CheatsController.BlindEnemies = value;
                CheatsController.Instance.RenderInfo();
            }
        }
        public bool IgnoreArenaTriggers
        {
            get => CheatsController.IgnoreArenaTriggers;
            set
            {
                CheatsController.IgnoreArenaTriggers = value;
                CheatsController.Instance.RenderInfo();
            }
        }
        public bool NoCooldown
        {
            get => CheatsController.NoCooldown;
            set
            {
                CheatsController.NoCooldown = value;
                CheatsController.Instance.RenderInfo();
            }
        }
        public bool Noclip
        {
            get => CheatsController.Instance.GetPrivate<bool>("noclip");
            set
            {
                if (CheatsController.Instance.GetPrivate<bool>("cheatsEnabled"))
                {
                    CheatsController.Instance.StartCoroutine(SetMovement(value, "noclip"));
                }
            }
        }
        public bool Flight
        {
            get => CheatsController.Instance.GetPrivate<bool>("flight");
            set
            {
                if (CheatsController.Instance.GetPrivate<bool>("cheatsEnabled"))
                {
                    CheatsController.Instance.StartCoroutine(SetMovement(value, "flight"));
                }
            }
        }
        public bool InfiniteJumps
        {
            get => CheatsController.Instance.GetPrivate<bool>("infiniteJumps");
            set
            {
                CheatsController.Instance.SetPrivate("infiniteJumps", value);
                CheatsController.Instance.RenderInfo();
            }
        }
        public bool spawnerEnabled = (CheatsController.Instance.arm != null);
        public bool stayEnabled = CheatsController.Instance.GetPrivate<bool>("stayEnabled");
    }
}
