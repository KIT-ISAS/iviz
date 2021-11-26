#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class PlaneDraggable : XRScreenDraggable
    {
        [SerializeField] Vector3 normal = Vector3.forward;
        [SerializeField] Collider? rayCollider;

        public Collider RayCollider
        {
            get => rayCollider.AssertNotNull(nameof(rayCollider));
            set => rayCollider = value != null ? value : throw new ArgumentNullException(nameof(value));
        }

        public float? Damping { get; set; } = 0.2f;

        public Vector3 Normal
        {
            get => normal;
            set => normal = value;
        }

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

                var referencePointWorld = mTransform.TransformPoint(referencePointLocal);
                var deltaPositionWorld = intersectionWorld - referencePointWorld;
                /*
                var localIntersection = mTransform.InverseTransformPoint(intersection);
                var deltaPosition = localIntersection - referencePointLocal;
                var deltaPositionWorld = mTransform.TransformVector(deltaPosition);
                */

                mTarget.position += Damping is { } damping
                    ? damping * deltaPositionWorld
                    : deltaPositionWorld;
                RaiseMoved();
            }
        }
    }
}