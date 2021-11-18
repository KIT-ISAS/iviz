#nullable enable

using System;
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

        static readonly Vector3[] CubePoints =
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

        static Bounds TransformBound(this in Bounds bounds, in Pose pose, Vector3 scale)
        {
            if (pose == Pose.identity)
            {
                return scale == Vector3.one
                    ? bounds
                    : new Bounds(Vector3.Scale(scale, bounds.center), Vector3.Scale(scale, bounds.size));
            }

            if (pose.rotation == Quaternion.identity)
            {
                return scale == Vector3.one
                    ? new Bounds(bounds.center + pose.position, bounds.size)
                    : new Bounds(Vector3.Scale(scale, bounds.center) + pose.position,
                        Vector3.Scale(scale, bounds.size));
            }

            Vector3 positionMin = float.MaxValue * Vector3.one;
            Vector3 positionMax = float.MinValue * Vector3.one;
            Vector3 boundsCenter = bounds.center;
            Vector3 boundsExtents = bounds.extents;

            if (scale == Vector3.one)
            {
                foreach (Vector3 point in CubePoints)
                {
                    Vector3 position = pose.rotation * Vector3.Scale(point, boundsExtents);
                    positionMin = Vector3.Min(positionMin, position);
                    positionMax = Vector3.Max(positionMax, position);
                }

                return new Bounds(
                    pose.position + pose.rotation * boundsCenter + (positionMax + positionMin) / 2,
                    positionMax - positionMin);
            }

            foreach (Vector3 point in CubePoints)
            {
                Vector3 localPoint = boundsCenter + Vector3.Scale(point, boundsExtents);
                Vector3 position = pose.rotation * Vector3.Scale(localPoint, scale);
                positionMin = Vector3.Min(positionMin, position);
                positionMax = Vector3.Max(positionMax, position);
            }

            return new Bounds(pose.position + (positionMax + positionMin) / 2, positionMax - positionMin);
        }

        static Bounds TransformBound(this in Bounds bounds, Transform transform)
        {
            return TransformBound(bounds, transform.AsLocalPose(), transform.localScale);
        }

        static Bounds TransformBoundWithInverse(this in Bounds bounds, Transform transform)
        {
            var (x, y, z) = transform.localScale;
            return TransformBound(bounds, transform.AsLocalPose().Inverse(),
                new Vector3(1f / x, 1f / y, 1f / z));
        }

        public static Bounds? TransformBoundWithInverse(this in Bounds? bounds, Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return bounds == null ? null : TransformBoundWithInverse(bounds.Value, transform);
        }

        public static Bounds? TransformBound(this in Bounds? bounds, Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return bounds == null ? null : TransformBound(bounds.Value, transform);
        }


        public static Bounds? CombineBounds(this IEnumerable<Bounds?> enumOfBounds)
        {
            if (enumOfBounds == null)
            {
                throw new ArgumentNullException(nameof(enumOfBounds));
            }

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
                    result.Value.Encapsulate(bounds.Value);
                }
            }

            return result;
        }

        public static Bounds TransformBoundsUntil(Bounds bounds, Transform transform, Transform endTransform)
        {
            while (transform != endTransform)
            {
                bounds = bounds.TransformBound(transform);
                transform = transform.parent;
            }

            return bounds;
        }         

        static readonly Plane[] PlaneCache = new Plane[6];

        public static bool IsVisibleFromMainCamera(this in Bounds bounds)
        {
            GeometryUtility.CalculateFrustumPlanes(Settings.MainCamera, PlaneCache);
            return GeometryUtility.TestPlanesAABB(PlaneCache, bounds);
        }
    }
}