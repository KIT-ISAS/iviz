#nullable enable

using System;
using Iviz.Core;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class RotationDraggable : ScreenDraggable
    {
        [SerializeField] Vector3 normal;
        [SerializeField] Collider? rayCollider;

        public Collider RayCollider
        {
            get => rayCollider.AssertNotNull(nameof(rayCollider));
            set => rayCollider = value != null ? value : throw new ArgumentNullException(nameof(value));
        }

        public float? Damping { get; set; } = 0.2f;
        public bool DoesRotationReset { get; set; }

        protected override void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = Transform;
            Transform mTarget = TargetTransform;

            if (needsStart)
            {
                if (!RayCollider.bounds.IntersectRay(pointerRay, out float distance))
                {
                    return; // shouldn't happen
                }

                var intersectionWorld = pointerRay.origin + distance * pointerRay.direction;
                var intersectionLocal = mTransform.InverseTransformPoint(intersectionWorld);

                startIntersection = intersectionLocal;
                needsStart = false;
            }
            else
            {
                var normalRay = new Ray(mTransform.TransformPoint(startIntersection), mTransform.TransformDirection(normal));

                UnityUtils.PlaneIntersection(normalRay, pointerRay, out Vector3 intersection, out float cameraDistance);
                if (cameraDistance < 0)
                {
                    return;
                }

                var localIntersection = mTransform.InverseTransformPoint(intersection);

                var m = new float3x3(
                    startIntersection.Normalized(),
                    localIntersection.Normalized(),
                    normal);
                float det = math.determinant(m);

                float angle = Mathf.Asin(det) * Mathf.Rad2Deg;
                if (Damping is { } damping)
                {
                    angle *= damping;
                }

                var q = Quaternion.AngleAxis(angle, mTarget.InverseTransformDirection(normalRay.direction));
                mTarget.rotation *= q;
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