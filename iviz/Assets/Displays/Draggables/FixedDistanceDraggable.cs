#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class FixedDistanceDraggable : XRScreenDraggable
    {
        [SerializeField] float forwardScale = -1;
        
        Vector3 lastControllerPosition;
        float distance;
        bool isNearInteraction;

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
                isNearInteraction = distance < Settings.DragHandler.XRDraggableNearDistance;
                lastControllerPosition = pointerRay.origin;
            }
            else
            {
                if (interactorTransform != null
                    && ForwardScale is { } validatedForwardScale)
                {
                    float deltaDistance = Vector3.Dot(interactorTransform.forward,
                        lastControllerPosition - pointerRay.origin);
                    distance = Math.Max(0.1f, distance - validatedForwardScale * deltaDistance);
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