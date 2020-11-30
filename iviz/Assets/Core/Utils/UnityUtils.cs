using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Core
{
    public static class UnityUtils
    {
        public static CultureInfo Culture { get; } = BuiltIns.Culture;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MagnitudeSq(this Vector3 v)
        {
            return v.x * v.x + v.y * v.y + v.z * v.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MagnitudeSq(this float3 v)
        {
            return v.x * v.x + v.y * v.y + v.z * v.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Magnitude(this Vector3 v)
        {
            return Mathf.Sqrt(v.MagnitudeSq());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(this Vector3 lhs, in Vector3 rhs)
        {
            return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.x * rhs.y - lhs.y * rhs.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalized(this Vector3 v)
        {
            return v / v.Magnitude();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Abs(this Vector3 p)
        {
            return new Vector3(Mathf.Abs(p.x), Mathf.Abs(p.y), Mathf.Abs(p.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 CwiseProduct(this Vector3 p, Vector3 o)
        {
            return Vector3.Scale(p, o);
        }

        public static bool TryParse(string s, out float f)
        {
            if (float.TryParse(s, NumberStyles.Any, Culture, out f))
            {
                return true;
            }

            f = 0;
            return false;
        }

        public static Pose AsPose([NotNull] this Transform t)
        {
            return new Pose(t.position, t.rotation);
        }

        public static Pose AsLocalPose([NotNull] this Transform t)
        {
            return new Pose(t.localPosition, t.localRotation);
        }

        public static Vector3 Multiply(this Pose p, in Vector3 v)
        {
            return p.rotation * v + p.position;
        }

        public static Pose Multiply(this Pose p, in Pose o)
        {
            return new Pose
            (
                rotation: p.rotation * o.rotation,
                position: p.rotation * o.position + p.position
            );
        }

        public static void SetPose([NotNull] this Transform t, in Pose p)
        {
            t.SetPositionAndRotation(p.position, p.rotation);
        }

        public static void SetParentLocal([NotNull] this Transform t, [CanBeNull] Transform parent)
        {
            t.SetParent(parent, false);
        }

        public static void SetLocalPose([NotNull] this Transform t, in Pose p)
        {
            t.localPosition = p.position;
            t.localRotation = p.rotation;
        }


        public static Pose Inverse(this Pose p)
        {
            Quaternion q = Quaternion.Inverse(p.rotation);
            return new Pose(
                rotation: q,
                position: q * -p.position
            );
        }

        public static Pose Lerp(this Pose p, in Pose o, float t)
        {
            return new Pose(
                Vector3.Lerp(p.position, o.position, t),
                Quaternion.Lerp(p.rotation, o.rotation, t)
            );
        }

        public static Pose Lerp([NotNull] this Transform p, in Pose o, float t)
        {
            return new Pose(
                Vector3.Lerp(p.position, o.position, t),
                Quaternion.Lerp(p.rotation, o.rotation, t)
            );
        }

        public static Pose LocalLerp([NotNull] this Transform p, in Pose o, float t)
        {
            return new Pose(
                Vector3.Lerp(p.localPosition, o.position, t),
                Quaternion.Lerp(p.localRotation, o.rotation, t)
            );
        }

        public static void ForEach<T>([NotNull] this IEnumerable<T> col, Action<T> action)
        {
            foreach (var item in col)
            {
                action(item);
            }
        }

        public static void ForEach<T>([NotNull] this T[] col, Action<T> action)
        {
            foreach (var t in col)
            {
                action(t);
            }
        }

        public static bool Any<T>([NotNull] this List<T> ts, Predicate<T> predicate)
        {
            foreach (var t in ts)
            {
                if (predicate(t))
                {
                    return true;
                }
            }

            return false;
        }

        public static ArraySegment<T> AsSegment<T>([NotNull] this T[] ts)
        {
            return new ArraySegment<T>(ts);
        }

        public static ArraySegment<T> AsSegment<T>([NotNull] this T[] ts, int offset)
        {
            return new ArraySegment<T>(ts, offset, ts.Length - offset);
        }

        public static ArraySegment<T> AsSegment<T>([NotNull] this T[] ts, int offset, int count)
        {
            return new ArraySegment<T>(ts, offset, count);
        }

        static MaterialPropertyBlock propBlock;
        static readonly int ColorPropId = Shader.PropertyToID("_Color");

        public static void SetPropertyColor([NotNull] this MeshRenderer meshRenderer, Color color, int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            if (propBlock == null)
            {
                propBlock = new MaterialPropertyBlock();
            }

            meshRenderer.GetPropertyBlock(propBlock, id);
            propBlock.SetColor(ColorPropId, color);
            meshRenderer.SetPropertyBlock(propBlock, id);
        }

        static readonly int EmissiveColorPropId = Shader.PropertyToID("_EmissiveColor");

        public static void SetPropertyEmissiveColor([NotNull] this MeshRenderer meshRenderer, Color color, int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            if (propBlock == null)
            {
                propBlock = new MaterialPropertyBlock();
            }

            meshRenderer.GetPropertyBlock(propBlock, id);
            propBlock.SetColor(EmissiveColorPropId, color);
            meshRenderer.SetPropertyBlock(propBlock, id);
        }

        static readonly int MainTexStPropId = Shader.PropertyToID("_MainTex_ST_");

        public static void SetPropertyMainTexST(
            [NotNull] this MeshRenderer meshRenderer,
            in Vector2 xy,
            in Vector2 wh,
            int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            if (propBlock == null)
            {
                propBlock = new MaterialPropertyBlock();
            }

            meshRenderer.GetPropertyBlock(propBlock, id);
            propBlock.SetVector(MainTexStPropId, new Vector4(wh.x, wh.y, xy.x, xy.y));
            meshRenderer.SetPropertyBlock(propBlock, id);
        }

        struct NativeArrayHelper<T> : IList<T>, IReadOnlyList<T> where T : struct
        {
            NativeArray<T> nArray;

            public NativeArrayHelper(in NativeArray<T> array) => nArray = array;
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => nArray.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => nArray.GetEnumerator();
            public NativeArray<T>.Enumerator GetEnumerator() => nArray.GetEnumerator();
            public void Add(T item) => throw new InvalidOperationException();
            public void Clear() => throw new InvalidOperationException();
            public bool Contains(T item) => nArray.Contains(item);
            public void CopyTo(T[] array, int arrayIndex) => array.CopyTo(array, arrayIndex);
            public bool Remove(T item) => throw new InvalidOperationException();
            public int Count => nArray.Length;
            public bool IsReadOnly => false;
            public int IndexOf(T item) => throw new InvalidOperationException();
            public void Insert(int index, T item) => throw new InvalidOperationException();
            public void RemoveAt(int index) => throw new InvalidOperationException();

            public T this[int index]
            {
                get => nArray[index];
                set => nArray[index] = value;
            }
        }

        public static IList<T> AsList<T>(this NativeArray<T> array) where T : struct =>
            new NativeArrayHelper<T>(array);

        public static IReadOnlyList<T> AsReadOnlyList<T>(this NativeArray<T> array) where T : struct =>
            new NativeArrayHelper<T>(array);

        public static void DisposeDisplay<T>([CanBeNull] this T resource) where T : MonoBehaviour, IDisplay
        {
            if (resource != null)
            {
                resource.Suspend();
                ResourcePool.DisposeDisplay(resource);
            }
        }

        public static void DisposeResource([CanBeNull] this IDisplay resource, [NotNull] Info<GameObject> info)
        {
            if (resource != null)
            {
                resource.Suspend();
                ResourcePool.Dispose(info, ((MonoBehaviour) resource).gameObject);
            }
        }

        [CanBeNull]
        public static Transform GetTransform([CanBeNull] this IDisplay resource)
        {
            return ((MonoBehaviour) resource)?.transform;
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

        static Bounds TransformBound(Bounds bounds, Pose pose, Vector3 scale)
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

        static Bounds TransformBound(Bounds bounds, [NotNull] Transform transform)
        {
            return TransformBound(bounds, transform.AsLocalPose(), transform.localScale);
        }

        public static Bounds? TransformBound(Bounds? bounds, Pose pose, Vector3 scale)
        {
            return bounds == null ? (Bounds?) null : TransformBound(bounds.Value, pose, scale);
        }

        public static Bounds? TransformBound(Bounds? bounds, [NotNull] Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return bounds == null ? (Bounds?) null : TransformBound(bounds.Value, transform);
        }


        public static Bounds? CombineBounds([NotNull] IEnumerable<Bounds?> enumOfBounds)
        {
            if (enumOfBounds == null)
            {
                throw new ArgumentNullException(nameof(enumOfBounds));
            }

            Bounds? result = null;
            using (IEnumerator<Bounds?> it = enumOfBounds.GetEnumerator())
            {
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
            }

            return result;
        }

        public static Bounds? CombineBounds([NotNull] IEnumerable<Bounds> enumOfBounds)
        {
            if (enumOfBounds == null)
            {
                throw new ArgumentNullException(nameof(enumOfBounds));
            }

            Bounds? result = null;
            using (IEnumerator<Bounds> it = enumOfBounds.GetEnumerator())
            {
                while (it.MoveNext())
                {
                    Bounds bounds = it.Current;
                    if (result == null)
                    {
                        result = bounds;
                    }
                    else
                    {
                        result.Value.Encapsulate(bounds);
                    }
                }
            }

            return result;
        }

        public static IEnumerable<(TA First, TB Second)> Zip<TA, TB>(
            [NotNull] this IEnumerable<TA> a,
            [NotNull] IEnumerable<TB> b)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            using (var enumA = a.GetEnumerator())
            using (var enumB = b.GetEnumerator())
            {
                while (enumA.MoveNext() && enumB.MoveNext())
                {
                    yield return (enumA.Current, enumB.Current);
                }
            }
        }

        public readonly struct ZipEnumerable<TA, TB> : IEnumerable<(TA First, TB Second)>
        {
            readonly IReadOnlyList<TA> a;
            readonly IReadOnlyList<TB> b;

            public struct ZipEnumerator : IEnumerator<(TA First, TB Second)>
            {
                readonly IReadOnlyList<TA> a;
                readonly IReadOnlyList<TB> b;
                int currentIndex;

                internal ZipEnumerator(IReadOnlyList<TA> a, IReadOnlyList<TB> b)
                {
                    this.a = a;
                    this.b = b;
                    currentIndex = -1;
                }

                public bool MoveNext()
                {
                    bool canMoveNext = currentIndex != Math.Min(a.Count, b.Count);
                    if (canMoveNext)
                    {
                        currentIndex++;
                    }

                    return canMoveNext;
                }

                public void Reset()
                {
                    currentIndex = -1;
                }

                public (TA, TB) Current => (a[currentIndex], b[currentIndex]);

                [NotNull] object IEnumerator.Current => Current;

                public void Dispose()
                {
                }
            }

            internal ZipEnumerable(IReadOnlyList<TA> a, IReadOnlyList<TB> b)
            {
                this.a = a;
                this.b = b;
            }

            public ZipEnumerator GetEnumerator()
            {
                return new ZipEnumerator(a, b);
            }

            IEnumerator<(TA, TB)> IEnumerable<(TA First, TB Second)>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public static ZipEnumerable<TA, TB> Zip<TA, TB>([NotNull] this IReadOnlyList<TA> a,
            [NotNull] IReadOnlyList<TB> b)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            return new ZipEnumerable<TA, TB>(a, b);
        }
    }
}