using System;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Displays
{
    public sealed class DraggableTranslation : ScreenDraggable
    {
        [SerializeField] Vector3 line = new Vector3(0, 0, 1);

        Vector3 startOffset;

        static (float, float) ClosestPointDelta(in Ray ray, in Ray other)
        {
            Matrix4x4 m = Matrix4x4.identity;
            m.SetColumn(0, ray.direction);
            m.SetColumn(1, Vector3.Cross(ray.direction, other.direction));
            m.SetColumn(2, -other.direction);

            Matrix4x4 mInv = Matrix4x4.identity;
            Matrix4x4.Inverse3DAffine(m, ref mInv);
            Vector3 t = mInv * (other.origin - ray.origin);

            return (t.x, t.z);
        }

        protected override void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = transform;
            Transform mParent = mTransform.parent.CheckedNull() ?? mTransform;
            Transform mTarget = TargetTransform;
            var forwardRay = new Ray(mTransform.position, mParent.TransformDirection(line));

            (float deltaDistance, float cameraDistance) = ClosestPointDelta(forwardRay, pointerRay);

            if (cameraDistance < 0)
            {
                return;
            }

            deltaDistance = Mathf.Max(Mathf.Min(deltaDistance, 0.5f), -0.5f);
            Vector3 deltaPosition = deltaDistance * forwardRay.direction;
            if (needsStart)
            {
                startOffset = deltaPosition;
                needsStart = false;
            }
            else
            {
                mTarget.position += deltaPosition - startOffset;
                RaiseMoved();
            }
        }
    }
}