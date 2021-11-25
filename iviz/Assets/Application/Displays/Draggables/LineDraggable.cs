#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class LineDraggable : ScreenDraggable
    {
        [SerializeField] Vector3 line = new Vector3(0, 0, 1);
        [SerializeField] Collider? rayCollider;

        public Collider RayCollider
        {
            get => rayCollider.AssertNotNull(nameof(rayCollider));
            set => rayCollider = value != null ? value : throw new ArgumentNullException(nameof(value));
        }

        public float? Damping { get; set; } = 0.2f;

        protected override void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = transform;
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
                var axisRay = new Ray(mTransform.TransformPoint(startIntersection), mTransform.TransformDirection(line));

                UnityUtils.ClosestPointBetweenLines(axisRay, pointerRay, out float axisDistance,
                    out float cameraDistance);

                if (cameraDistance < 0)
                {
                    return;
                }

                if (Damping is { } damping)
                {
                    axisDistance *= damping;
                }

                Vector3 deltaPosition = axisDistance * axisRay.direction;
                //mTarget.position += deltaPosition - startIntersection;
                mTarget.position += deltaPosition;
                RaiseMoved();
            }
        }
    }
}