#nullable enable

using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class PlaneDraggable : XRScreenDraggable
    {
        [SerializeField] Vector3 normal = Vector3.forward;

        public float? Damping { get; set; }
        
        public Vector3 Normal
        {
            get => normal;
            set => normal = value;
        }

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
                startIntersection = localIntersection;
                needsStart = false;
            }
            else
            {
                var deltaPosition = localIntersection - startIntersection;
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