#nullable enable

using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class CirclePlaneDraggable : XRScreenDraggable
    {
        public float Radius { get; set; } = 1;
        
        public override Quaternion BaseOrientation
        {
            set { }
        }

        protected override void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = Transform;
            Transform mTarget = TargetTransform;

            if (ReferencePointLocal is not { } referencePointLocal)
            {
                InitializeReferencePoint(pointerRay);
            }
            else
            {
                var referencePointWorld = mTransform.TransformPoint(referencePointLocal);
                var normalRay = new Ray(referencePointWorld, mTransform.rotation * Vector3.up);

                UnityUtils.PlaneIntersection(normalRay, pointerRay, 
                    out Vector3 intersectionWorld, out float cameraDistance);

                if (cameraDistance < 0)
                {
                    return;
                }

                var deltaPositionWorld = intersectionWorld - referencePointWorld;
                
                var parent = mTransform.parent;
                var currentPositionWorld = mTransform.position; 
                var newPositionWorld = currentPositionWorld + deltaPositionWorld;
                var newPositionParentLocal = parent.InverseTransformPoint(newPositionWorld);

                var circleProjectLocal = newPositionParentLocal.WithY(0).normalized * Radius;
                var circleProjectWorld = parent.TransformPoint(circleProjectLocal);

                var deltaProjectedWorld = circleProjectWorld - currentPositionWorld; 
                
                mTarget.position += DampingPerFrame is { } damping
                    ? damping * deltaProjectedWorld
                    : deltaProjectedWorld;
                RaiseMoved();
            }
        }
    }
}