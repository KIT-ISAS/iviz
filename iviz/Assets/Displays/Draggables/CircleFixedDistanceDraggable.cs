#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class CircleFixedDistanceDraggable : XRScreenDraggable
    {
        [SerializeField] float forwardScale = -1;
        
        Vector3 lastControllerPosition;
        float distance;
        bool isNearInteraction;

        public float Radius { get; set; } = 1;
        public float? ForwardScale
        {
            get => forwardScale <= 0 || isNearInteraction ? null : forwardScale;
            set => forwardScale = value is { } newScale and > 0 ? newScale : -1;
        }

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
                isNearInteraction = distance < Settings.DragHandler.XRDraggableNearDistance;
            }
            else
            {
                if (interactorTransform != null && ForwardScale is { } validatedForwardScale)
                {
                    float deltaDistance = Vector3.Dot(interactorTransform.forward,
                        lastControllerPosition - pointerRay.origin);
                    distance = Math.Max(0.1f, distance - validatedForwardScale * deltaDistance);
                }

                var intersectionWorld = pointerRay.origin + distance * pointerRay.direction;
                var referencePointWorld = mTransform.TransformPoint(referencePointLocal);
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

                lastControllerPosition = pointerRay.origin;
                RaiseMoved();
            }
        }
    }
}