#nullable enable

using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class LineDraggable : ScreenDraggable
    {
        [SerializeField] Vector3 line = new Vector3(0, 0, 1);

        protected override void OnPointerMove(in Ray cameraRay)
        {
            Transform mTransform = transform;
            Transform mTarget = TargetTransform;
            
            var axisRay = new Ray(mTransform.position, mTransform.TransformDirection(line));

            UnityUtils.ClosestPointBetweenLines(axisRay, cameraRay, out float axisDistance, out float cameraDistance);

            if (cameraDistance < 0)
            {
                return;
            }

            float clampedDistance = Mathf.Max(Mathf.Min(axisDistance, 0.5f), -0.5f);
            Vector3 deltaPosition = clampedDistance * axisRay.direction;
            if (needsStart)
            {
                startIntersection = deltaPosition;
                needsStart = false;
            }
            else
            {
                mTarget.position += deltaPosition - startIntersection;
                RaiseMoved();
            }
        }
    }
}