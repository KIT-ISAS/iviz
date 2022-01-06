#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class LineDraggable : XRScreenDraggable
    {
        [SerializeField] Vector3 line = new Vector3(0, 0, 1);

        public Vector3 Line
        {
            set => line = value;
        }

        public override Quaternion BaseOrientation
        {
            set => line = value.Forward();
        }
        
        protected override void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = transform;
            Transform mTarget = TargetTransform;

            if (ReferencePointLocal is not {} referencePointLocal)
            {
                InitializeReferencePoint(pointerRay);
            }
            else
            {
                var axisRay = new Ray(mTransform.TransformPoint(referencePointLocal), mTransform.TransformDirection(line));

                UnityUtils.ClosestPointBetweenLines(axisRay, pointerRay, out float axisDistance,
                    out float cameraDistance);

                if (cameraDistance < 0)
                {
                    return;
                }

                var deltaPositionWorld = axisDistance * axisRay.direction;

                mTarget.position += DampingPerFrame is { } damping
                    ? damping * deltaPositionWorld
                    : deltaPositionWorld;
                RaiseMoved();
            }
        }
    }
}