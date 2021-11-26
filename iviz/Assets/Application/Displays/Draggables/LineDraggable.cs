#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class LineDraggable : XRScreenDraggable
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

            if (ReferencePointLocal is not {} referencePointLocal)
            {
                if (!RayCollider.TryIntersectRay(pointerRay, out var intersectionWorld))
                {
                    return; // shouldn't happen
                }

                ReferencePointLocal = mTransform.InverseTransformPoint(intersectionWorld);
            }
            else
            {
                var axisRay = new Ray(mTransform.TransformPoint(referencePointLocal), mTransform.TransformDirection(line));

                UnityUtils.ClosestPointBetweenLines(axisRay, pointerRay, out float axisDistance,
                    out float cameraDistance);

                if (cameraDistance < 0)
                {
                    return;
                }

                var deltaPositionWorld = axisDistance * axisRay.direction;

                mTarget.position += Damping is { } damping
                    ? damping * deltaPositionWorld
                    : deltaPositionWorld;
                RaiseMoved();
            }
        }
    }
}