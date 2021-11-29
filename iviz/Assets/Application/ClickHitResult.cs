using System;
using Iviz.Core;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Iviz.App
{
    public sealed class ClickHitResult
    {
        public GameObject GameObject { get; }
        public Vector3 Position { get; }
        public Vector3 Normal { get; }

        ClickHitResult(GameObject gameObject, in Vector3 position, in Vector3 normal)
        {
            GameObject = gameObject.CheckedNull() ?? throw new ArgumentNullException(nameof(gameObject));
            Position = position;
            Normal = normal;
        }

        public ClickHitResult(in RaycastHit r) : this(r.collider.gameObject, r.point, r.normal)
        {
        }

        public ClickHitResult(in ARRaycastHit r) : this(r.trackable.gameObject, r.pose.position,
            ((ARPlane)r.trackable).normal)
        {
        }

        public Pose CreatePose()
        {
            var side = Mathf.Approximately(Vector3.forward.Cross(Normal).MagnitudeSq(), 0)
                ? Vector3.right
                : Vector3.forward;

            var forward = side.Cross(Normal);

            return new Pose(Position, Quaternion.LookRotation(forward, Normal));
        }

        public void Deconstruct(out GameObject gameObject, out Vector3 position, out Vector3 normal) =>
            (gameObject, position, normal) = (GameObject, Position, Normal);
    }
}