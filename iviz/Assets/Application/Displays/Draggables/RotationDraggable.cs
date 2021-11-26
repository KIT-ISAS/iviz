#nullable enable

using System;
using Iviz.Core;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class RotationDraggable : XRScreenDraggable
    {
        [SerializeField] Vector3 normal;
        [SerializeField] Collider? rayCollider;

        Collider RayCollider => rayCollider.AssertNotNull(nameof(rayCollider));

        public float? Damping { get; set; } = 0.2f;
        public bool DoesRotationReset { get; set; }

        protected override void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = Transform;
            Transform mTarget = TargetTransform;

            if (ReferencePointLocal is not {} referencePointLocal)
            {
                if (!RayCollider.TryIntersectRay(pointerRay, out Vector3 intersectionWorld))
                {
                    return; // shouldn't happen
                }

                ReferencePointLocal = mTransform.InverseTransformPoint(intersectionWorld);
            }
            else
            {
                var normalRay = new Ray(mTransform.TransformPoint(referencePointLocal),
                    mTransform.TransformDirection(normal));

                UnityUtils.PlaneIntersection(normalRay, pointerRay, out Vector3 intersectionWorld,
                    out float cameraDistance);

                if (cameraDistance < 0)
                {
                    return;
                }

                var intersectionLocal = mTransform.InverseTransformPoint(intersectionWorld);

                var m = new float3x3(
                    referencePointLocal.Normalized(),
                    intersectionLocal.Normalized(),
                    normal);

                float det = math.determinant(m);
                float angle = Mathf.Asin(det) * Mathf.Rad2Deg;
                float dampenedAngle = Damping is { } damping
                    ? damping * angle
                    : angle;

                var deltaRotation =
                    Quaternion.AngleAxis(dampenedAngle, mTarget.InverseTransformDirection(normalRay.direction));

                mTarget.rotation *= deltaRotation;
                RaiseMoved();

                /*
                if (DoesRotationReset)
                {
                    startIntersection = localIntersection;
                }
                */
            }
        }
    }
}