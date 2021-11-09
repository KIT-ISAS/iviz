#nullable enable

using Iviz.Core;
using UnityEngine;


namespace Iviz.Displays
{
    public sealed class DraggablePlane : XRScreenDraggable
    {
        [SerializeField] Vector3 normal = new Vector3(0, 0, 1);

        Vector3 startIntersection;

        public Vector3 Normal
        {
            get => normal;
            set => normal = value;
        }

        static (Vector3, float) PlaneIntersection(in Ray ray, in Ray other)
        {
            float t = Vector3.Dot(other.origin - ray.origin, ray.direction) /
                      Vector3.Dot(-other.direction, ray.direction);
            Vector3 p = other.origin + t * other.direction;

            return (p, t);
        }

        protected override void OnPointerMove(in Ray pointerRay)
        {
            Transform mTransform = Transform;
            Transform mParent = mTransform.parent.CheckedNull() ?? mTransform;
            Transform mTarget = TargetTransform;

            var normalRay = new Ray(mTransform.position, mParent.TransformDirection(normal));

            (Vector3 intersection, float cameraDistance) = PlaneIntersection(normalRay, pointerRay);
            if (cameraDistance < 0)
            {
                return;
            }

            Vector3 localIntersection = mParent.InverseTransformPoint(intersection);
            if (needsStart)
            {
                startIntersection = localIntersection;
                needsStart = false;
            }
            else
            {
                var deltaPosition = localIntersection - startIntersection;
                float deltaDistance = deltaPosition.Magnitude();
                if (deltaDistance > 0.5f) deltaPosition *= 0.75f / deltaDistance;

                Vector3 deltaPositionWorld = mTransform.TransformVector(deltaPosition);
                mTarget.position += deltaPositionWorld;
                RaiseMoved();
            }
        }
    }
}