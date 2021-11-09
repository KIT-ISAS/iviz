using System;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Displays
{
    public sealed class DraggableRotation : ScreenDraggable
    {
        [SerializeField] Vector3 normal;

        Vector3 startIntersection;

        public bool DoesRotationReset { get; set; }
        
        static (Vector3, float) PlaneIntersection(in Ray ray, in Ray other)
        {
            float t = Vector3.Dot(other.origin - ray.origin, ray.direction) /
                      Vector3.Dot(-other.direction, ray.direction);
            Vector3 p = other.origin + t * other.direction;

            return (p, t);
        }

        protected override void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = transform;
            Transform mParent = mTransform.parent.CheckedNull() ?? mTransform;
            Transform mTarget = TargetTransform;

            if (mParent == null)
            {
                Debug.LogWarning("The object with the DraggableRotation must have a parent!");
                return;
            }

            var normalRay = new Ray(mTransform.position, mParent.TransformDirection(normal));

            (Vector3 intersection, float cameraDistance) = PlaneIntersection(normalRay, pointerRay);
            if (cameraDistance < 0)
            {
                return;
            }

            Vector3 localIntersection = mParent.InverseTransformPoint(intersection);
            if (needsStart)
            {
                needsStart = false;
                startIntersection = localIntersection;
            }
            else
            {
                Matrix4x4 m = Matrix4x4.identity;
                m.SetColumn(0, startIntersection.Normalized());
                m.SetColumn(1, localIntersection.Normalized());
                m.SetColumn(2, normal);

                float angle = Mathf.Asin(m.determinant) * Mathf.Rad2Deg;

                Quaternion q = Quaternion.AngleAxis(angle, mTarget.InverseTransformDirection(normalRay.direction));
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