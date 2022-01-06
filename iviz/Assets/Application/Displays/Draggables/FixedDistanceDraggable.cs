#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class FixedDistanceDraggable : XRScreenDraggable
    {
        Vector3 lastControllerPosition;
        float distance;

        public float? ForwardScale { get; set; }

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
                var intersectionWorld = InitializeReferencePoint(pointerRay);
                distance = Vector3.Distance(pointerRay.origin, intersectionWorld);
                lastControllerPosition = pointerRay.origin;
            }
            else
            {
                if (interactorTransform != null
                    && ForwardScale is { } forwardScale)
                {
                    float deltaDistance = Vector3.Dot(interactorTransform.forward,
                        lastControllerPosition - pointerRay.origin);
                    distance = Math.Max(0.1f, distance - forwardScale * deltaDistance);
                }


                var intersectionWorld = pointerRay.origin + distance * pointerRay.direction;
                var referencePointWorld = mTransform.TransformPoint(referencePointLocal);
                var deltaPositionWorld = intersectionWorld - referencePointWorld;

                mTarget.position += DampingPerFrame is { } damping
                    ? damping * deltaPositionWorld
                    : deltaPositionWorld;

                lastControllerPosition = pointerRay.origin;
                RaiseMoved();
            }
        }
    }
}