#nullable enable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.Core
{
    public static class BoundsUtils
    {
        [return: NotNullIfNotNull("resource")]
        public static Transform? GetTransform(this IDisplay? resource)
        {
            return ((MonoBehaviour?)resource)?.transform;
        }

        static Vector3[]? cubePoints;

        static Vector3[] CubePoints => cubePoints ??= new[]
        {
            Vector3.right + Vector3.up + Vector3.forward,
            Vector3.right + Vector3.up - Vector3.forward,
            Vector3.right - Vector3.up + Vector3.forward,
            Vector3.right - Vector3.up - Vector3.forward,
            -Vector3.right + Vector3.up + Vector3.forward,
            -Vector3.right + Vector3.up - Vector3.forward,
            -Vector3.right - Vector3.up + Vector3.forward,
            -Vector3.right - Vector3.up - Vector3.forward,
        };

        public static Bounds TransformBound(this in Bounds bounds, in Pose pose, in Vector3 scale)
        {
            if (bounds.IsInvalid())
            {
                ThrowHelper.ThrowArgument("Bounds contain invalid values", nameof(bounds));
            }

            if (pose == Pose.identity)
            {
                return scale == Vector3.one
                    ? bounds
                    : new Bounds(scale.Mult(bounds.center), scale.Mult(bounds.size));
            }

            if (pose.rotation == Quaternion.identity)
            {
                return scale == Vector3.one
                    ? new Bounds(bounds.center + pose.position, bounds.size)
                    : new Bounds(scale.Mult(bounds.center) + pose.position, scale.Mult(bounds.size));
            }

            var positionMin = float.MaxValue * Vector3.one;
            var positionMax = float.MinValue * Vector3.one;
            var boundsCenter = bounds.center;
            var boundsExtents = bounds.extents;

            if (scale == Vector3.one)
            {
                foreach (var point in CubePoints)
                {
                    var position = pose.rotation * point.Mult(boundsExtents);
                    positionMin = Vector3.Min(positionMin, position);
                    positionMax = Vector3.Max(positionMax, position);
                }

                return new Bounds(
                    pose.position + pose.rotation * boundsCenter + (positionMax + positionMin) / 2,
                    positionMax - positionMin);
            }

            foreach (var point in CubePoints)
            {
                var localPoint = boundsCenter + point.Mult(boundsExtents);
                var position = pose.rotation * localPoint.Mult(scale);
                positionMin = Vector3.Min(positionMin, position);
                positionMax = Vector3.Max(positionMax, position);
            }

            return new Bounds(pose.position + (positionMax + positionMin) / 2, positionMax - positionMin);
        }

        public static Bounds TransformBound(this in Bounds bounds, Transform transform)
        {
            return TransformBound(bounds, transform.AsLocalPose(), transform.localScale);
        }

        public static Bounds? CombineBounds(this IEnumerable<Bounds?> enumOfBounds)
        {
            ThrowHelper.ThrowIfNull(enumOfBounds, nameof(enumOfBounds));

            Bounds? result = null;
            using var it = enumOfBounds.GetEnumerator();
            while (it.MoveNext())
            {
                Bounds? bounds = it.Current;
                if (bounds == null)
                {
                    continue;
                }

                if (result == null)
                {
                    result = bounds;
                }
                else
                {
                    if (bounds.Value.IsInvalid())
                    {
                        ThrowHelper.ThrowArgument("Bounds contain invalid values", nameof(bounds));
                    }

                    result.Value.Encapsulate(bounds.Value);
                }
            }

            return result;
        }

        public static Bounds? TransformBoundsUntil(Bounds? bounds, Transform transform, Transform endTransform)
        {
            return bounds is { } notNullBounds
                ? TransformBoundsUntil(notNullBounds, transform, endTransform)
                : null;
        }

        static Bounds TransformBoundsUntil(Bounds bounds, Transform transform, Transform endTransform)
        {
            if (bounds.IsInvalid())
            {
                ThrowHelper.ThrowArgument("Bounds contain invalid values", nameof(bounds));
            }

            while (transform != endTransform)
            {
                bounds = bounds.TransformBound(transform);
                transform = transform.parent;
            }

            return bounds;
        }

        static Plane[]? planeCache;
        static Plane[] PlaneCache => planeCache ??= new Plane[6];

        public static bool IsVisibleFromMainCamera(this in Bounds bounds)
        {
            if (bounds.IsInvalid())
            {
                ThrowHelper.ThrowArgument( "Bounds contain invalid values", nameof(bounds));
            }

            GeometryUtility.CalculateFrustumPlanes(Settings.MainCamera, PlaneCache);
            return GeometryUtility.TestPlanesAABB(PlaneCache, bounds);
        }

        public static bool TryIntersectRay(this Collider collider, Ray pointerRay, out Vector3 intersection,
            out Vector3 normal)
        {
            if (collider.Raycast(pointerRay, out var hitInfo, 100))
            {
                intersection = hitInfo.point;
                normal = hitInfo.normal;
                return true;
            }

            intersection = Vector3.zero;
            normal = Vector3.zero;
            return false;
        }
    }
}