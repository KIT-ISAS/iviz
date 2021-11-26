#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class FixedDistanceDraggable : XRScreenDraggable
    {
        [SerializeField] Collider? rayCollider;
        Vector3 lastControllerPosition;
        float distance;

        Collider RayCollider => rayCollider.AssertNotNull(nameof(rayCollider));

        public float? Damping { get; set; } = 0.2f;

        public float? ForwardScale { get; set; }

        protected override void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = Transform;
            Transform mTarget = TargetTransform;

            if (ReferencePointLocal is not {} referencePointLocal)
            {
                if (!RayCollider.TryIntersectRay(pointerRay, out var intersectionWorld))
                {
                    return; // shouldn't happen
                }

                distance = Vector3.Distance(pointerRay.origin, intersectionWorld);
                lastControllerPosition = pointerRay.origin;
                ReferencePointLocal = mTransform.InverseTransformPoint(intersectionWorld);
            }
            else
            {
                if (interactorTransform != null
                    && ForwardScale is { } forwardScale)
                {
                    float deltaDistance = Vector3.Dot(interactorTransform.forward,
                        lastControllerPosition - pointerRay.origin);
                    distance = Mathf.Max(0.1f,  distance - forwardScale * deltaDistance);
                }


                var intersectionWorld = pointerRay.origin + distance * pointerRay.direction;
                var referencePointWorld = mTransform.TransformPoint(referencePointLocal);
                var deltaPositionWorld = intersectionWorld - referencePointWorld;

                mTarget.position += Damping is { } damping 
                    ? damping * deltaPositionWorld 
                    : deltaPositionWorld; 

                lastControllerPosition = pointerRay.origin;
                RaiseMoved();
            }
        }
    }
}