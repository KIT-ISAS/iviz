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
                var deltaPosition = localIntersection - startIntersection;
                if (Damping is { } damping)
                {
                    deltaPosition *= damping;
                }

                var deltaPositionWorld = mTransform.TransformVector(deltaPosition);
                mTarget.position += deltaPositionWorld;
                RaiseMoved();
            }
        }
    }
}