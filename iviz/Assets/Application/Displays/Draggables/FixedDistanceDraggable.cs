#nullable enable

using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class FixedDistanceDraggable : ScreenDraggable
    {
        [SerializeField] Collider? rayCollider;

        Collider RayCollider => rayCollider.AssertNotNull(nameof(rayCollider));  
        public float? Damping { get; set; }
        float distance;
        
        protected override void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = Transform;
            Transform mTarget = TargetTransform;

            if (needsStart)
            {
                if (!RayCollider.bounds.IntersectRay(pointerRay, out distance))
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
                var intersectionWorld = pointerRay.origin + distance * pointerRay.direction;
                var intersectionLocal = mTransform.InverseTransformPoint(intersectionWorld);

                var deltaPosition = intersectionLocal - startIntersection;
                if (Damping is { } damping)
                {
                    deltaPosition *= damping;
                }

                Vector3 deltaPositionWorld = mTransform.TransformVector(deltaPosition);
                mTarget.position += deltaPositionWorld;
                RaiseMoved();
            }            
        }
    }
}