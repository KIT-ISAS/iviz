using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Iviz.Tools;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using Color32 = UnityEngine.Color32;
using Mesh = UnityEngine.Mesh;

namespace Iviz.Core
{
    public static class UnityUtils
    {
        public static CultureInfo Culture { get; } = BuiltIns.Culture;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MagnitudeSq(this in Vector3 v)
        {
            return v.x * v.x + v.y * v.y + v.z * v.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in float3 p)
        {
            return Mathf.Max(Mathf.Max(Mathf.Abs(p.x), Mathf.Abs(p.y)), Mathf.Abs(p.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff3(this in float4 p)
        {
            return Mathf.Max(Mathf.Max(Mathf.Abs(p.x), Mathf.Abs(p.y)), Mathf.Abs(p.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in Vector3 p)
        {
            return Mathf.Max(Mathf.Max(Mathf.Abs(p.x), Mathf.Abs(p.y)), Mathf.Abs(p.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in Vector4 p)
        {
            return Mathf.Max(Mathf.Max(Mathf.Max(Mathf.Abs(p.x), Mathf.Abs(p.y)), Mathf.Abs(p.z)), Mathf.Abs(p.w));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ToVector(this in Quaternion p)
        {
            return new Vector4(p.x, p.y, p.z, p.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Magnitude(this in Vector3 v)
        {
            return Mathf.Sqrt(v.MagnitudeSq());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(this in Vector3 lhs, in Vector3 rhs)
        {
            return new Vector3(
                lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.x * rhs.y - lhs.y * rhs.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalized(this Vector3 v)
        {
            float m = v.Magnitude();
            v.x /= m;
            v.y /= m;
            v.z /= m;
            return v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Abs(this Vector3 p)
        {
            p.x = Mathf.Abs(p.x);
            p.y = Mathf.Abs(p.y);
            p.z = Mathf.Abs(p.z);
            return p;
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static float3 Abs(this in float3 p)
        //{
        //    return new float3(Mathf.Abs(p.x), Mathf.Abs(p.y), Mathf.Abs(p.z));
        //}

        public static bool TryParse(string s, out float f)
        {
            if (float.TryParse(s, NumberStyles.Any, Culture, out f))
            {
                return true;
            }

            f = 0;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose AsPose([NotNull] this Transform t)
        {
            return new Pose(t.position, t.rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose AsLocalPose([NotNull] this Transform t)
        {
            return new Pose(t.localPosition, t.localRotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(this in Pose p, in Vector3 v)
        {
            return p.rotation * v + p.position;
        }

        //public static Vector3 MultiplyDirection(this in Pose p, in Vector3 v)
        //{
        //    return p.rotation * v;
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose Multiply(this in Pose p, Pose o)
        {
            o.position = p.rotation * o.position + p.position;
            o.rotation = p.rotation * o.rotation;
            return o;
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose Inverse(this Pose p)
        {
            Quaternion q = Quaternion.Inverse(p.rotation);
            p.rotation = q;
            p.position = q * -p.position;
            return p;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsApproxIdentity(this in Pose p)
        {
            return Mathf.Approximately(p.position.x, 0)
                   && Mathf.Approximately(p.position.y, 0)
                   && Mathf.Approximately(p.position.z, 0)
                   && Mathf.Approximately(p.rotation.w, 1);
        }

        public static bool EqualsApprox(this in Pose p, in Pose q)
        {
            /*
            return Mathf.Approximately(p.position.x, q.position.x)
                   && Mathf.Approximately(p.position.y, q.position.y)
                   && Mathf.Approximately(p.position.z, q.position.z)
                   && Mathf.Approximately(p.rotation.x, q.rotation.x)
                   && Mathf.Approximately(p.rotation.y, q.rotation.y)
                   && Mathf.Approximately(p.rotation.z, q.rotation.z)
                   && Mathf.Approximately(p.rotation.w, q.rotation.w);
                   */
            return EqualsApprox(p.position, q.position) && EqualsApprox(p.rotation, q.rotation);
        }

        static bool EqualsApprox(in Vector3 lhs, in Vector3 rhs)
        {
            float num1 = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            float num3 = lhs.z - rhs.z;
            return num1 * num1 + num2 * num2 + num3 * num3 < 9.999999439624929E-11;
        }

        static bool EqualsApprox(in Quaternion a, in Quaternion b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w > 0.9999989867210388;            
        }
        
        public static Vector4 GetColumnIn(this in Matrix4x4 m, int index)
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

        public static Pose Lerp(this in Pose p, in Pose o, float t) => new Pose(
            Vector3.Lerp(p.position, o.position, t),
            Quaternion.Lerp(p.rotation, o.rotation, t)
        );

        public static Pose LocalLerp([NotNull] this Transform p, in Pose o, float t)
        {
            return new Pose(
                Vector3.Lerp(p.localPosition, o.position, t),
                Quaternion.Lerp(p.localRotation, o.rotation, t)
            );
        }

        public static ArraySegment<T> AsSegment<T>([NotNull] this T[] ts)
        {
            return new ArraySegment<T>(ts);
        }

        public static ArraySegment<T> AsSegment<T>([NotNull] this T[] ts, int offset)
        {
            return new ArraySegment<T>(ts, offset, ts.Length - offset);
        }

        static readonly Plane[] PlaneCache = new Plane[6];

        public static bool IsVisibleFromMainCamera(this in Bounds bounds)
        {
            GeometryUtility.CalculateFrustumPlanes(Settings.MainCamera, PlaneCache);
            return GeometryUtility.TestPlanesAABB(PlaneCache, bounds);
        }

        /// <summary>
        /// Returns null if the Unity object is null or null-equivalent, otherwise returns the object itself.
        /// </summary>
        /// <param name="o">The Unity object to evaluate</param>
        /// <typeparam name="T">The type of the Unity object</typeparam>
        /// <returns>The Unity object if not-null and valid, otherwise a normal C# null</returns>           
        [CanBeNull]
        public static T CheckedNull<T>([CanBeNull] this T o) where T : UnityEngine.Object => o != null ? o : null;

        public static Color WithAlpha(this Color c, float alpha)
        {
            c.a = alpha;
            return c;
        }

        public static Color32 WithAlpha(this Color32 c, byte alpha)
        {
            c.a = alpha;
            return c;
        }

        public static Pose WithPosition(this Pose p, in Vector3 v)
        {
            p.position = v;
            return p;
        }

        public static Pose WithRotation(this Pose p, in Quaternion q)
        {
            p.rotation = q;
            return p;
        }

        public static Vector3 WithX(this Vector3 c, float x)
        {
            c.x = x;
            return c;
        }

        public static Vector3 WithY(this Vector3 c, float y)
        {
            c.y = y;
            return c;
        }

        public static Vector3 WithZ(this Vector3 c, float z)
        {
            c.z = z;
            return c;
        }

        public static Vector2 WithX(this Vector2 c, float x)
        {
            c.x = x;
            return c;
        }

        public static Vector2 WithY(this Vector2 c, float y)
        {
            c.y = y;
            return c;
        }


        public static Color WithSaturation(this in Color c, float saturation)
        {
            Color.RGBToHSV(c, out float h, out _, out float v);
            return Color.HSVToRGB(h, saturation, v).WithAlpha(c.a);
        }
        
        public static Color WithValue(this in Color c, float value)
        {
            Color.RGBToHSV(c, out float h, out float s, out _);
            return Color.HSVToRGB(h, s, value).WithAlpha(c.a);
        }        
        
        public static bool IsUsable(this in Pose pose)
        {
            const int maxPoseMagnitude = 100000;
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

        static Func<object, Array> extractArrayFromListTypeFn;

        static Func<object, Array> ExtractArrayFromList
        {
            get
            {
                if (extractArrayFromListTypeFn != null)
                {
                    return extractArrayFromListTypeFn;
                }

                var assembly = Assembly.GetAssembly(typeof(Mesh));
                var type = assembly?.GetType("UnityEngine.NoAllocHelpers");
                var methodInfo = type?.GetMethod("ExtractArrayFromList", BindingFlags.Static | BindingFlags.Public);
                if (methodInfo == null)
                {
                    throw new InvalidOperationException("Failed to retrieve function ExtractArrayFromList");
                }

                extractArrayFromListTypeFn =
                    (Func<object, Array>)methodInfo.CreateDelegate(typeof(Func<object, Array>));

                return extractArrayFromListTypeFn;
            }
        }

        public static T[] ExtractArray<T>(this List<T> list) => (T[])ExtractArrayFromList(list);
    }

    public static class ResourceUtils
    {
        public static void ReturnToPool<T>([CanBeNull] this T resource) where T : MonoBehaviour, IDisplay
        {
            if (resource == null)
            {
                return;
            }

            resource.Suspend();
            ResourcePool.ReturnDisplay(resource);
        }

        public static void ReturnToPool([CanBeNull] this IDisplay resource, [NotNull] Info<GameObject> info)
        {
            if (resource == null)
            {
                return;
            }

            resource.Suspend();
            ResourcePool.Return(info, ((MonoBehaviour)resource).gameObject);
        }

        [NotNull]
        public static ReadOnlyDictionary<T, TU> AsReadOnly<T, TU>([NotNull] this Dictionary<T, TU> t)
        {
            return new ReadOnlyDictionary<T, TU>(t);
        }

        [NotNull]
        public static IEnumerable<(T item, int index)> WithIndex<T>([NotNull] this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }
    }

    public static class BoundsUtils
    {
        [ContractAnnotation("null => null; notnull => notnull")]
        [CanBeNull]
        public static Transform GetTransform([CanBeNull] this IDisplay resource)
        {
            return ((MonoBehaviour)resource)?.transform;
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

        static Bounds TransformBound(this in Bounds bounds, [NotNull] Transform transform)
        {
            return TransformBound(bounds, transform.AsLocalPose(), transform.localScale);
        }

        static Bounds TransformBoundWithInverse(this in Bounds bounds, [NotNull] Transform transform)
        {
            var (x, y, z) = transform.localScale;
            return TransformBound(bounds, transform.AsLocalPose().Inverse(),
                new Vector3(1f / x, 1f / y, 1f / z));
        }

        public static Bounds? TransformBoundWithInverse(this in Bounds? bounds, [NotNull] Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return bounds == null ? (Bounds?)null : TransformBoundWithInverse(bounds.Value, transform);
        }

        public static Bounds? TransformBound(this in Bounds? bounds, [NotNull] Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return bounds == null ? (Bounds?)null : TransformBound(bounds.Value, transform);
        }


        public static Bounds? CombineBounds([NotNull] this IEnumerable<Bounds?> enumOfBounds)
        {
            if (enumOfBounds == null)
            {
                throw new ArgumentNullException(nameof(enumOfBounds));
            }

            Bounds? result = null;
            using (var it = enumOfBounds.GetEnumerator())
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

        [NotNull]
        public static T EnsureComponent<T>([NotNull] this GameObject gameObject) where T : Component =>
            gameObject.TryGetComponent(out T comp) ? comp : gameObject.AddComponent<T>();

        [NotNull]
        public static T Instantiate<T>([NotNull] this Info<GameObject> o, [CanBeNull] Transform parent = null) =>
            o.Instantiate(parent).GetComponent<T>();
    }

    public static class MeshUtils
    {
        public static void SetVertices([NotNull] this Mesh mesh, in Rent<Vector3> ps)
        {
            mesh.SetVertices(ps.Array, 0, ps.Length);
        }

        public static void SetNormals([NotNull] this Mesh mesh, in Rent<Vector3> ps)
        {
            mesh.SetNormals(ps.Array, 0, ps.Length);
        }

        public static void SetTangents([NotNull] this Mesh mesh, in Rent<Vector4> ps)
        {
            mesh.SetTangents(ps.Array, 0, ps.Length);
        }

        public static void SetIndices([NotNull] this Mesh mesh, in Rent<int> ps, MeshTopology topology, int subMesh)
        {
            mesh.SetIndices(ps.Array, 0, ps.Length, topology, subMesh);
        }

        public static void SetColors([NotNull] this Mesh mesh, in Rent<Color> ps)
        {
            mesh.SetColors(ps.Array, 0, ps.Length);
        }

        public static void SetColors([NotNull] this Mesh mesh, in Rent<Color32> ps)
        {
            mesh.SetColors(ps.Array, 0, ps.Length);
        }

        public static void SetUVs([NotNull] this Mesh mesh, in Rent<Vector2> ps)
        {
            mesh.SetUVs(0, ps.Array, 0, ps.Length);
        }

        public static void SetUVs([NotNull] this Mesh mesh, in Rent<Vector3> ps)
        {
            mesh.SetUVs(0, ps.Array, 0, ps.Length);
        }

        public static void SetUVs([NotNull] this Mesh mesh, int channel, in Rent<Vector3> ps)
        {
            mesh.SetUVs(channel, ps.Array, 0, ps.Length);
        }

        public static void SetTriangles([NotNull] this Mesh mesh, in Rent<int> ps, int subMesh = 0)
        {
            mesh.SetTriangles(ps.Array, 0, ps.Length, subMesh);
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

        public static void SetPropertyEmissiveColor([NotNull] this MeshRenderer meshRenderer, in Color color,
            int id = 0)
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

        public static void Deconstruct(this in Vector3 v, out float x, out float y, out float z) =>
            (x, y, z) = (v.x, v.y, v.z);

        public static void Deconstruct(this in Vector3Int v, out int x, out int y, out int z) =>
            (x, y, z) = (v.x, v.y, v.z);

        public static void Deconstruct(this Vector2 v, out float x, out float y) =>
            (x, y) = (v.x, v.y);

        public static void Deconstruct(this in float3 v, out float x, out float y, out float z) =>
            (x, y, z) = (v.x, v.y, v.z);

        public static void Deconstruct(this in Vector4 v, out float x, out float y, out float z, out float w) =>
            (x, y, z, w) = (v.x, v.y, v.z, v.w);

        public static void Deconstruct(this in Bounds b, out Vector3 center, out Vector3 size) =>
            (center, size) = (b.center, b.size);

        public static void Deconstruct(this in Pose p, out Vector3 position, out Quaternion rotation) =>
            (position, rotation) = (p.position, p.rotation);

        public static void Deconstruct(this in Msgs.GeometryMsgs.TransformStamped p,
            out string parentId, out string childId, out Msgs.GeometryMsgs.Transform transform, out time stamp) =>
            (parentId, childId, transform, stamp) = (p.Header.FrameId, p.ChildFrameId, p.Transform, p.Header.Stamp);
    }
}