using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
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
        public static float MaxAbsCoeff(this float3 p)
        {
            return Mathf.Max(Mathf.Max(Mathf.Abs(p.x), Mathf.Abs(p.y)), Mathf.Abs(p.z));
        }

        public static float MaxAbsCoeff(this Vector3 p)
        {
            return Mathf.Max(Mathf.Max(Mathf.Abs(p.x), Mathf.Abs(p.y)), Mathf.Abs(p.z));
        }

        public static float MaxAbsCoeff(this Quaternion p)
        {
            return Mathf.Max(Mathf.Max(Mathf.Max(Mathf.Abs(p.x), Mathf.Abs(p.y)), Mathf.Abs(p.z)), Mathf.Abs(p.w));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Magnitude(this Vector3 v)
        {
            return Mathf.Sqrt(v.MagnitudeSq());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(this Vector3 lhs, in Vector3 rhs)
        {
            return new Vector3(
                lhs.y * rhs.z - lhs.z * rhs.y, 
                lhs.z * rhs.x - lhs.x * rhs.z,
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
        public static float3 Abs(this float3 p)
        {
            return new float3(Mathf.Abs(p.x), Mathf.Abs(p.y), Mathf.Abs(p.z));
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
                p.rotation * o.position + p.position,
                p.rotation * o.rotation
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

        public static bool IsApproxIdentity(this Pose p)
        {
            return Mathf.Approximately(p.position.x, 0)
                   && Mathf.Approximately(p.position.y, 0)
                   && Mathf.Approximately(p.position.z, 0)
                   && Mathf.Approximately(p.rotation.x, 0)
                   && Mathf.Approximately(p.rotation.y, 0)
                   && Mathf.Approximately(p.rotation.z, 0)
                   && Mathf.Approximately(p.rotation.w, 1);
        }

        public static bool EqualsApprox(this Pose p, in Pose q)
        {
            return Mathf.Approximately(p.position.x, q.position.x)
                   && Mathf.Approximately(p.position.y, q.position.y)
                   && Mathf.Approximately(p.position.z, q.position.z)
                   && Mathf.Approximately(p.rotation.x, q.rotation.x)
                   && Mathf.Approximately(p.rotation.y, q.rotation.y)
                   && Mathf.Approximately(p.rotation.z, q.rotation.z)
                   && Mathf.Approximately(p.rotation.w, q.rotation.w);
        }

        public static Vector4 GetColumnIn(this Matrix4x4 m, int index)
        {
            switch (index)
            {
                case 0:
                    return new Vector4(m.m00, m.m10, m.m20, m.m30);
                case 1:
                    return new Vector4(m.m01, m.m11, m.m21, m.m31);
                case 2:
                    return new Vector4(m.m02, m.m12, m.m22, m.m32);
                case 3:
                    return new Vector4(m.m03, m.m13, m.m23, m.m33);
                default:
                    throw new IndexOutOfRangeException("Invalid column index!");
            }
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

        public static void SetVertices([NotNull] this Mesh mesh, Rent<Vector3> ps)
        {
            mesh.SetVertices(ps.Array, 0, ps.Length);
        }

        public static void SetNormals([NotNull] this Mesh mesh, Rent<Vector3> ps)
        {
            mesh.SetNormals(ps.Array, 0, ps.Length);
        }

        public static void SetTangents([NotNull] this Mesh mesh, Rent<Vector4> ps)
        {
            mesh.SetTangents(ps.Array, 0, ps.Length);
        }

        public static void SetIndices([NotNull] this Mesh mesh, Rent<int> ps, MeshTopology topology, int subMesh)
        {
            mesh.SetIndices(ps.Array, 0, ps.Length, topology, subMesh);
        }

        public static void SetColors([NotNull] this Mesh mesh, Rent<Color> ps)
        {
            mesh.SetColors(ps.Array, 0, ps.Length);
        }

        public static void SetColors([NotNull] this Mesh mesh, Rent<Color32> ps)
        {
            mesh.SetColors(ps.Array, 0, ps.Length);
        }

        public static void SetUVs([NotNull] this Mesh mesh, int channel, Rent<Vector2> ps)
        {
            mesh.SetUVs(channel, ps.Array, 0, ps.Length);
        }

        public static void SetUVs([NotNull] this Mesh mesh, int channel, Rent<Vector3> ps)
        {
            mesh.SetUVs(channel, ps.Array, 0, ps.Length);
        }

        public static void SetTriangles([NotNull] this Mesh mesh, Rent<int> ps, int subMesh = 0)
        {
            mesh.SetTriangles(ps.Array, 0, ps.Length, subMesh);
        }

        public static void ReturnToPool<T>([CanBeNull] this T resource) where T : MonoBehaviour, IDisplay
        {
            if (resource != null)
            {
                resource.Suspend();
                ResourcePool.ReturnDisplay(resource);
            }
        }

        public static void ReturnToPool([CanBeNull] this IDisplay resource, [NotNull] Info<GameObject> info)
        {
            if (resource != null)
            {
                resource.Suspend();
                ResourcePool.Return(info, ((MonoBehaviour) resource).gameObject);
            }
        }

        [ContractAnnotation("null => null; notnull => notnull")]
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

        static Bounds TransformBound(this Bounds bounds, Pose pose, Vector3 scale)
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

        static Bounds TransformBound(this Bounds bounds, [NotNull] Transform transform)
        {
            return TransformBound(bounds, transform.AsLocalPose(), transform.localScale);
        }

        static Bounds TransformBoundWithInverse(this Bounds bounds, [NotNull] Transform transform)
        {
            var (x, y, z) = transform.localScale;
            return TransformBound(bounds, transform.AsLocalPose().Inverse(),
                new Vector3(1f / x, 1f / y, 1f / z));
        }

        public static Bounds? TransformBound(this Bounds? bounds, in Pose pose, in Vector3 scale)
        {
            return bounds == null ? (Bounds?) null : TransformBound(bounds.Value, pose, scale);
        }

        public static Bounds? TransformBoundWithInverse(this Bounds? bounds, [NotNull] Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return bounds == null ? (Bounds?) null : TransformBoundWithInverse(bounds.Value, transform);
        }

        public static Bounds? TransformBound(this Bounds? bounds, [NotNull] Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return bounds == null ? (Bounds?) null : TransformBound(bounds.Value, transform);
        }


        public static Bounds? CombineBounds([NotNull] this IEnumerable<Bounds?> enumOfBounds)
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

        public static Bounds? CombineBounds([NotNull] this IEnumerable<Bounds> enumOfBounds)
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

        static readonly Plane[] PlaneCache = new Plane[6];

        public static bool IsVisibleFromMainCamera(this Bounds bounds)
        {
            GeometryUtility.CalculateFrustumPlanes(Settings.MainCamera, PlaneCache);
            return GeometryUtility.TestPlanesAABB(PlaneCache, bounds);
        }

        [CanBeNull]
        public static T SafeNull<T>(this T o) where T : UnityEngine.Object => o != null ? o : null;
        public static Color WithAlpha(this Color c, float alpha) => new Color(c.r, c.g, c.b, alpha);
        public static Color32 WithAlpha(this Color32 c, byte alpha) => new Color32(c.r, c.g, c.b, alpha);
        public static Pose WithPosition(this Pose p, in Vector3 v) => new Pose(v, p.rotation);
        public static Pose WithRotation(this Pose p, in Quaternion q) => new Pose(p.position, q);

        public static bool IsUsable(this Pose pose)
        {
            const int maxPoseMagnitude = 10000;
            return pose.position.MaxAbsCoeff() < maxPoseMagnitude;
        }

        [ContractAnnotation("=> false, t:null; => true, t:notnull")]
        public static bool TryGetFirst<T>([NotNull] this IEnumerable<T> enumerable, [CanBeNull] out T t)
        {
            using (var enumerator = enumerable.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    t = enumerator.Current;
                    return true;
                }

                t = default;
                return false;
            }
        }
    }

    public static class FileUtils
    {
        static async Task WriteAllBytesAsync([NotNull] string filePath, [NotNull] byte[] bytes, int count,
            CancellationToken token)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create,
                FileAccess.Write, FileShare.None, 4096, true))
            {
                await stream.WriteAsync(bytes, 0, count, token);
            }
        }

        [NotNull]
        public static Task WriteAllBytesAsync([NotNull] string filePath, Rent<byte> bytes,
            CancellationToken token)
        {
            return WriteAllBytesAsync(filePath, bytes.Array, bytes.Length, token);
        }

        [NotNull]
        public static Task WriteAllBytesAsync([NotNull] string filePath, [NotNull] byte[] bytes,
            CancellationToken token)
        {
            return WriteAllBytesAsync(filePath, bytes, bytes.Length, token);
        }

        public static async ValueTask<Rent<byte>> ReadAllBytesAsync([NotNull] string filePath, CancellationToken token)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open,
                FileAccess.Read, FileShare.None, 4096, true))
            {
                var rent = new Rent<byte>((int) stream.Length);
                await stream.ReadAsync(rent.Array, 0, rent.Length, token);
                return rent;
            }
        }

        [NotNull]
        public static async Task WriteAllTextAsync([NotNull] string filePath, [NotNull] string text,
            CancellationToken token)
        {
            using (var bytes = text.AsRent())
            {
                await WriteAllBytesAsync(filePath, bytes.Array, bytes.Length, token);
            }
        }

        [ItemNotNull]
        public static async ValueTask<string> ReadAllTextAsync([NotNull] string filePath, CancellationToken token)
        {
            using (var bytes = await ReadAllBytesAsync(filePath, token))
            {
                return BuiltIns.UTF8.GetString(bytes.Array);
            }
        }
    }

    public static class MeshRendererUtils
    {
        static MaterialPropertyBlock propBlock;
        [NotNull] static MaterialPropertyBlock PropBlock => propBlock ?? (propBlock = new MaterialPropertyBlock());

        static readonly int ColorPropId = Shader.PropertyToID("_Color");
        static readonly int EmissiveColorPropId = Shader.PropertyToID("_EmissiveColor");
        static readonly int MainTexStPropId = Shader.PropertyToID("_MainTex_ST_");
        static readonly int BumpMapStPropId = Shader.PropertyToID("_BumpMap_ST_");
        static readonly int SmoothnessPropId = Shader.PropertyToID("_Smoothness");
        static readonly int MetallicPropId = Shader.PropertyToID("_Metallic");

        public static void SetPropertyColor([NotNull] this MeshRenderer meshRenderer, in Color color, int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            meshRenderer.GetPropertyBlock(PropBlock, id);
            PropBlock.SetColor(ColorPropId, color);
            meshRenderer.SetPropertyBlock(PropBlock, id);
        }

        public static void SetPropertyEmissiveColor([NotNull] this MeshRenderer meshRenderer, in Color color, int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            meshRenderer.GetPropertyBlock(PropBlock, id);
            PropBlock.SetColor(EmissiveColorPropId, color);
            meshRenderer.SetPropertyBlock(PropBlock, id);
        }

        public static void SetPropertySmoothness([NotNull] this MeshRenderer meshRenderer, float smoothness, int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            meshRenderer.GetPropertyBlock(PropBlock, id);
            PropBlock.SetFloat(SmoothnessPropId, smoothness);
            meshRenderer.SetPropertyBlock(PropBlock, id);
        }

        public static void SetPropertyMetallic([NotNull] this MeshRenderer meshRenderer, float metallic, int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            meshRenderer.GetPropertyBlock(PropBlock, id);
            PropBlock.SetFloat(MetallicPropId, metallic);
            meshRenderer.SetPropertyBlock(PropBlock, id);
        }

        public static void ResetPropertyTextureScale([NotNull] this MeshRenderer meshRenderer)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            meshRenderer.GetPropertyBlock(PropBlock, 0);
            PropBlock.SetVector(MainTexStPropId, new Vector4(1, 1, 0, 0));
            PropBlock.SetVector(BumpMapStPropId, new Vector4(1, 1, 0, 0));
            meshRenderer.SetPropertyBlock(PropBlock, 0);
        }

        public static void Deconstruct(this Vector3 v, out float x, out float y, out float z) =>
            (x, y, z) = (v.x, v.y, v.z);

        public static void Deconstruct(this float3 v, out float x, out float y, out float z) =>
            (x, y, z) = (v.x, v.y, v.z);

        public static void Deconstruct(this Vector4 v, out float x, out float y, out float z, out float w) =>
            (x, y, z, w) = (v.x, v.y, v.z, v.w);

        public static void Deconstruct(this Pose p, out Vector3 position, out Quaternion rotation) =>
            (position, rotation) = (p.position, p.rotation);
        
        public static void Deconstruct(this Msgs.GeometryMsgs.TransformStamped p, 
            out string parentId, out string childId, out Msgs.GeometryMsgs.Transform transform, out time stamp) =>
            (parentId, childId, transform, stamp) = (p.Header.FrameId, p.ChildFrameId, p.Transform, p.Header.Stamp);
    }
}