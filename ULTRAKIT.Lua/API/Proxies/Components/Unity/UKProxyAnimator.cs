using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components.Unity
{
    class UKProxyAnimator : UKProxy<Animator>
    {
        public UKProxyAnimator(Animator target) : base(target)
        {
        }

        #region Properties
        public Vector3 angularVelocity => target.angularVelocity;

        public bool applyRootMotion
        {
            get => target.applyRootMotion;
            set => target.applyRootMotion = value;
        }
        // avatar

        public Vector3 bodyPosition
        {
            get => target.bodyPosition;
            set => target.bodyPosition = value;
        }

        public Quaternion bodyRotation
        {
            get => target.bodyRotation;
            set => target.bodyRotation = value;
        }
        // cullingMode
        public Vector3 deltaPostion => target.deltaPosition;
        public Quaternion deltaRotation => target.deltaRotation;

        public float feetPivotActive
        {
            get => target.feetPivotActive;
            set => target.feetPivotActive = value;
        }

        public bool fireEvents
        {
            get => target.fireEvents;
            set => target.fireEvents = value;
        }

        public float gravityWeight => target.gravityWeight;
        public bool hasBoundPlayables => target.hasBoundPlayables;
        public bool hasRootMotion => target.hasRootMotion;
        public bool hasTransformHierarchy => target.hasTransformHierarchy;
        public float humanScale => target.humanScale;
        public bool isHuman => target.isHuman;
        public bool isInitialized => target.isInitialized;
        public bool isMatchingTarget => target.isMatchingTarget;
        public bool isOptimizable => target.isOptimizable;

        public bool keepAnimatorControllerStateOnDisable
        {
            get => target.keepAnimatorControllerStateOnDisable;
            set => target.keepAnimatorControllerStateOnDisable = value;
        }
        public int layerCount => target.layerCount;

        public bool layersAffectMassCenter => target.layersAffectMassCenter;
        //leftFeetBottomHeight

        public int parameterCount => target.parameterCount;
        // parameters

        public Vector3 pivotPosition => target.pivotPosition;
        public float pivotWeight => target.pivotWeight;
        // playableGraph

        public float playbackTime
        {
            get => target.playbackTime;
            set => target.playbackTime = value;
        }
        //recorderMode, recorderStartTime, recorderStopTime, rightFeetBottomHeight

        public Vector3 rootPosition
        {
            get => target.rootPosition;
            set => target.rootPosition = value;
        }
        public Quaternion rootRotation
        {
            get => target.rootRotation;
            set => target.rootRotation = value;
        }
        //runtimeAnimatorController
        public float speed
        {
            get => target.speed;
            set => target.speed = value;
        }
        public bool stabilizeFeet
        {
            get => target.stabilizeFeet;
            set => target.stabilizeFeet = value;
        }

        public Vector3 targetPosition => target.targetPosition;

        public Quaternion targetRotation => target.targetRotation;
        //updateMode
        public Vector3 velocity => target.velocity;
        #endregion

        #region Instance Methods

        #endregion


        // ApplyBuiltinRootMotion, CrossFade, CrossFadeInFixedTime, GetAnimatorTransitionInfo, GetBehaviour, GetBehaviours, GetBoneTransform
        public bool GetBool(string name) => target.GetBool(name);
        public void SetBool(string name, bool value) => target.SetBool(name, value);

        public float GetFloat(string name) => target.GetFloat(name);
        public void SetFloat(string name, float value) => target.SetFloat(name, value);

        public int GetInteger(string name) => target.GetInteger(name);
        public void SetInteger(string name, int value) => target.SetInteger(name, value);

        public void SetTrigger(string name) => target.SetTrigger(name);
        public void ResetTrigger(string name) => target.ResetTrigger(name);

        public void StartPlayback() => target.StartPlayback();
        public void StopPlayback() => target.StopPlayback();
        // GetCurrentAnimatorClipInfo,m GetCurrentAnimatorClipinfoCount, GetCurrentAnimatorStateInfo
    }
}
