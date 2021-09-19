using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Proxies.Components
{
    class UKProxyRigidbody : UKProxyComponentAbstract<Rigidbody>
    {
        public UKProxyRigidbody(Rigidbody target) : base(target)
        {
        }

        #region Properties
        public float angularDrag
        {
            get => target.angularDrag;
            set => target.angularDrag = value;
        }

        public Vector3 angularVelocity
        {
            get => target.angularVelocity;
            set => target.angularVelocity = value;
        }
        public Vector3 centerOfMass
        {
            get => target.centerOfMass;
            set => target.centerOfMass = value;
        }
        //public CollisionDetectionMode collisionDetectionMode
        //{
        //    get => target.collisionDetectionMode;
        //    set => target.collisionDetectionMode = value;
        //}

        //public RigidbodyConstraints constraints
        //{
        //    get => target.constraints;
        //    set => target.constraints = value;
        //}

        public bool detectCollisions
        {
            get => target.detectCollisions;
            set => target.detectCollisions = value;
        }
        public float drag
        {
            get => target.drag;
            set => target.drag = value;
        }
        public bool freezeRotation
        {
            get => target.freezeRotation;
            set => target.freezeRotation = value;
        }

        public Vector3 inertiaTensor
        {
            get => target.inertiaTensor;
            set => target.inertiaTensor = value;
        }
        public Quaternion inertiaTensorRotation
        {
            get => target.inertiaTensorRotation;
            set => target.inertiaTensorRotation = value;
        }
        //public RigidbodyInterpolation interpolation
        //{
        //    get => target.interpolation;
        //    set => target.interpolation = value;
        //}

        public bool isKinematic
        {
            get => target.isKinematic;
            set => target.isKinematic = value;
        }

        public float mass
        {
            get => target.mass;
            set => target.mass = value;
        }

        public float maxAngularVelocity
        {
            get => target.maxAngularVelocity;
            set => target.maxAngularVelocity = value;
        }
        public float maxDepenetrationVelocity
        {
            get => target.maxDepenetrationVelocity;
            set => target.maxDepenetrationVelocity = value;
        }
        public Vector3 position
        {
            get => target.position;
            set => target.position = value;
        }
        public Quaternion rotation
        {
            get => target.rotation;
            set => target.rotation = value;
        }
        public float sleepThreshold
        {
            get => target.sleepThreshold;
            set => target.sleepThreshold = value;
        }
        public int solverIterations
        {
            get => target.solverIterations;
            set => target.solverIterations = value;
        }
        public int solverVelocityIterations
        {
            get => target.solverVelocityIterations;
            set => target.solverVelocityIterations = value;
        }
        public bool useGravity
        {
            get => target.useGravity;
            set => target.useGravity = value;
        }
        public Vector3 velocity
        {
            get => target.velocity;
            set => target.velocity = value;
        }
        public Vector3 worldCenterOfMass => target.worldCenterOfMass;
        #endregion

        #region Public Methods
        public void AddExplosionForce(float force, Vector3 explosionPosition, float explosionRadius) => target.AddExplosionForce(force, explosionPosition, explosionRadius);
        public void AddExplosionForce(float force, Vector3 explosionPosition, float explosionRadius, float upwardsModifier) => target.AddExplosionForce(force, explosionPosition, explosionRadius, upwardsModifier);
        public void AddForce(Vector3 force) => target.AddForce(force);
        //public void AddForce(Vector3 force, ForceMode mode) => target.AddForce(force, mode);
        public void AddForceAtPosition(Vector3 force, Vector3 position) => target.AddForceAtPosition(force, position);
        //public void AddForceAtPosition(Vector3 force, Vector3 position, ForceMode mode) => target.AddForceAtPosition(force, position, mode);
        public void AddRelativeForce(Vector3 force) => target.AddRelativeForce(force);
        public void AddRelativeTorque(Vector3 torque) => target.AddRelativeTorque(torque);
        public void AddTorque(Vector3 torque) => target.AddTorque(torque);
        public Vector3 ClosestPointOnBounds(Vector3 position) => target.ClosestPointOnBounds(position);
        public Vector3 GetPointVelocity(Vector3 point) => target.GetPointVelocity(point);
        public Vector3 GetRelativePointVelocity(Vector3 point) => target.GetPointVelocity(point);
        public bool IsSleeping() => target.IsSleeping();
        public void MovePosition(Vector3 position) => target.MovePosition(position);
        public void MoveRotation(Quaternion rotation) => target.MoveRotation(rotation);
        public void ResetCenterOfMass() => target.ResetCenterOfMass();
        public void ResetInertiaTensor() => target.ResetInertiaTensor();
        public void SetDensity(float density) => target.SetDensity(density);
        public void Sleep() => target.Sleep();
        //TODO: SweepTest, SweepTestAll
        public void WakeUp() => target.WakeUp();
        #endregion
    }
}
