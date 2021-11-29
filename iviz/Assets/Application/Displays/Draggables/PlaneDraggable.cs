#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class PlaneDraggable : XRScreenDraggable
    {
        [SerializeField] Vector3 normal = Vector3.forward;

        public Vector3 Normal
        {
            set => normal = value;
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

                mTarget.position += Damping is { } damping
                    ? damping * deltaPositionWorld
                    : deltaPositionWorld;
                RaiseMoved();
            }
        }
    }
}