#nullable enable

using Iviz.Core;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class RotationDraggable : ScreenDraggable
    {
        [SerializeField] Vector3 normal;
        public float? Damping { get; set; }
        public bool DoesRotationReset { get; set; }
        
        protected override void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = Transform;
            Transform mTarget = TargetTransform;

            var normalRay = new Ray(mTransform.position, mTransform.TransformDirection(normal));

            UnityUtils.PlaneIntersection(normalRay, pointerRay, out Vector3 intersection, out float cameraDistance);
            if (cameraDistance < 0)
            {
                return;
            }

            Vector3 localIntersection = mTransform.InverseTransformPoint(intersection);
            if (needsStart)
            {
                needsStart = false;
                startIntersection = localIntersection;
            }
            else
            {
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
            }

            if (DoesRotationReset)
            {
                startIntersection = localIntersection;
            }
        }
    }
}