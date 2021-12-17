#nullable enable

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using Color32 = UnityEngine.Color32;
using Mesh = UnityEngine.Mesh;

namespace Iviz.Core
{
    public static class UnityUtils
    {
        /// <summary>
        ///  Max amount of indices that a mesh can hold with short indices
        /// </summary>
        public const int MeshUInt16Threshold = ushort.MaxValue;
        
        public static readonly CultureInfo Culture = BuiltIns.Culture;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MagnitudeSq(this in Vector3 v)
        {
            return v.x * v.x + v.y * v.y + v.z * v.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in float3 p) => MaxAbsCoeff(p.x, p.y, p.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff3(this in float4 p) => MaxAbsCoeff(p.x, p.y, p.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in Vector3 p) => MaxAbsCoeff(p.x, p.y, p.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float MaxAbsCoeff(float x, float y, float z)
        {
            return Mathf.Max(Mathf.Max(Mathf.Abs(x), Mathf.Abs(y)), Mathf.Abs(z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in Vector4 p)
        {
            return Mathf.Max(Mathf.Max(Mathf.Max(Mathf.Abs(p.x), Mathf.Abs(p.y)), Mathf.Abs(p.z)), Mathf.Abs(p.w));
        }

        public static Vector3 InvCoeff(this Vector3 p)
        {
            p.x = 1f / p.x;
            p.y = 1f / p.y;
            p.z = 1f / p.z;
            return p;
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

        public static Vector2 Abs(this Vector2 p)
        {
            p.x = Mathf.Abs(p.x);
            p.y = Mathf.Abs(p.y);
            return p;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose AsPose(this Transform t)
        {
            return new Pose(t.position, t.rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose AsLocalPose(this Transform t)
        {
            return new Pose(t.localPosition, t.localRotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(this in Pose p, in Vector3 v)
        {
            return p.rotation * v + p.position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose Multiply(this in Pose p, Pose o)
        {
            o.position = p.rotation * o.position + p.position;
            o.rotation = p.rotation * o.rotation;
            return o;
        }

        public static void SetPose(this Transform t, in Pose p)
        {
            t.SetPositionAndRotation(p.position, p.rotation);
        }

        public static void SetParentLocal(this Transform t, Transform? parent)
        {
            t.SetParent(parent, false);
        }

        public static void SetLocalPose(this Transform t, in Pose p)
        {
            t.localPosition = p.position;
            t.localRotation = p.rotation;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose Inverse(this Pose p)
        {
            ref var rotation = ref p.rotation;
            rotation.x = -rotation.x;
            rotation.y = -rotation.y;
            rotation.z = -rotation.z;
            p.position = rotation * -p.position;
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
            return EqualsApprox(p.position, q.position) && EqualsApprox(p.rotation, q.rotation);
        }

        static bool EqualsApprox(in Vector3 lhs, in Vector3 rhs)
        {
            // from unity
            float num1 = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            float num3 = lhs.z - rhs.z;
            return num1 * num1 + num2 * num2 + num3 * num3 < 9.999999439624929E-11;
        }

        static bool EqualsApprox(in Quaternion a, in Quaternion b)
        {
            // from unity
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w > 0.9999989867210388;
        }

        public static Pose Lerp(this in Pose p, in Pose o, float t) => new Pose(
            Vector3.Lerp(p.position, o.position, t),
            Quaternion.Lerp(p.rotation, o.rotation, t)
        );

        public static Pose PoseFromUp(in Vector3 position, in Vector3 up)
        {
            var side = Mathf.Approximately(Vector3.forward.Cross(up).MagnitudeSq(), 0)
                ? Vector3.right
                : Vector3.forward;

            var forward = side.Cross(up);

            return new Pose(position, Quaternion.LookRotation(forward, up));
        }

        /// <summary>
        /// Returns null if the Unity object is null or null-equivalent, otherwise returns the object itself.
        /// </summary>
        /// <param name="o">The Unity object to evaluate</param>
        /// <typeparam name="T">The type of the Unity object</typeparam>
        /// <returns>The Unity object if not-null and valid, otherwise a normal C# null</returns>           
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("o")]
        public static T? CheckedNull<T>(this T? o) where T : UnityEngine.Object => o != null ? o : null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AssertNotNull<T>(this T? o, string name,
            [CallerFilePath] string? caller = null,
            [CallerLineNumber] int lineNumber = 0) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            return o != null
#else
            return o is not null
#endif
                ? o
                : throw new MissingAssetFieldException($"Asset '{name}' has not been set!\n" +
                                                       $"At: {caller} line {lineNumber}");
        }

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

        public static bool TryGetFirst<T>(this IEnumerable<T> enumerable, out T? t)
        {
            using var enumerator = enumerable.GetEnumerator();
            if (enumerator.MoveNext())
            {
                t = enumerator.Current;
                return true;
            }

            t = default;
            return false;
        }

        static Func<object, Array>? extractArrayFromListTypeFn;

        static Func<object, Array> ExtractArrayFromList
        {
            get
            {
                if (extractArrayFromListTypeFn != null)
                {
                    return extractArrayFromListTypeFn;
                }

                var assembly = Assembly.GetAssembly(typeof(Mesh) /* any unity type */);
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

        public static void PlaneIntersection(in Ray plane, in Ray ray, out Vector3 intersection, out float scaleRay)
        {
            scaleRay = Vector3.Dot(ray.origin - plane.origin, plane.direction) /
                       Vector3.Dot(-ray.direction, plane.direction);
            intersection = ray.origin + scaleRay * ray.direction;
        }

        public static void ClosestPointBetweenLines(in Ray ray, in Ray other, out float scaleRay, out float scaleOther)
        {
            var m = new float3x3(
                ray.direction,
                ray.direction.Cross(other.direction),
                -other.direction
            );
            var mInv = math.inverse(m);

            (scaleRay, _, scaleOther) = math.mul(mInv, other.origin - ray.origin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddInPlace(this ref Vector3 v, in Vector3 o)
        {
            v.x += o.x;
            v.y += o.y;
            v.z += o.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Mult(this in Vector3 v, Vector3 o)
        {
            o.x *= v.x;
            o.y *= v.y;
            o.z *= v.z;
            return o;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Mult(this in Vector3Int v, Vector3 o)
        {
            o.x *= v.x;
            o.y *= v.y;
            o.z *= v.z;
            return o;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Vector3 v, out float x, out float y, out float z) =>
            (x, y, z) = (v.x, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Vector3Int v, out int x, out int y, out int z) =>
            (x, y, z) = (v.x, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this Vector2 v, out float x, out float y) =>
            (x, y) = (v.x, v.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in float3 v, out float x, out float y, out float z) =>
            (x, y, z) = (v.x, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Vector4 v, out float x, out float y, out float z, out float w) =>
            (x, y, z, w) = (v.x, v.y, v.z, v.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in float4 v, out float x, out float y, out float z) =>
            (x, y, z) = (v.x, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Bounds b, out Vector3 center, out Vector3 size) =>
            (center, size) = (b.center, b.size);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Pose p, out Vector3 position, out Quaternion rotation) =>
            (position, rotation) = (p.position, p.rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Ray r, out Vector3 origin, out Vector3 direction) =>
            (origin, direction) = (r.origin, r.direction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Msgs.GeometryMsgs.TransformStamped p,
            out string parentId, out string childId, out Msgs.GeometryMsgs.Transform transform, out time stamp) =>
            (parentId, childId, transform, stamp) = (p.Header.FrameId, p.ChildFrameId, p.Transform, p.Header.Stamp);

        public static unsafe Span<T> AsSpan<T>(this in NativeArray<T> array) where T : unmanaged
        {
            return new Span<T>(array.GetUnsafePtr(), array.Length);
        }

        public static unsafe ReadOnlySpan<T> AsReadOnlySpan<T>(this in NativeArray<T> array) where T : unmanaged
        {
            return new ReadOnlySpan<T>(array.GetUnsafeReadOnlyPtr(), array.Length);
        }

        public static Span<T> AsSpan<T>(this List<T> list) where T : unmanaged
        {
            return new Span<T>(list.ExtractArray(), 0, list.Count);
        }

        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this List<T> list) where T : unmanaged
        {
            return new ReadOnlySpan<T>(list.ExtractArray(), 0, list.Count);
        }

        public static T Read<T>(this ReadOnlySpan<byte> span) where T : unmanaged
        {
            return MemoryMarshal.Read<T>(span);
        }
        
        public static void TryReturn<T>(this T[] _) where T : unmanaged
        {
        }

        public static void TryReturn<T>(this Memory<T> memory) where T : unmanaged
        {
            if (memory.Length == 0)
            {
                return;
            }

            if (!MemoryMarshal.TryGetArray(memory, out ArraySegment<T> segment))
            {
                return;
            }
            
            ArrayPool<T>.Shared.Return(segment.Array);
        }

        public static Span<T> AsSpan<T>(this Memory<T> memory) where T : unmanaged
        {
            return memory.Span;
        }
        
        public static unsafe NativeArray<T> CreateNativeArrayWrapper<T>(T* ptr, int length) where T : unmanaged
        {
            var array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(ptr, length, Allocator.None);
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref array, AtomicSafetyHandle.GetTempMemoryHandle());
            return array;
        }

        public static void CopyFrom<T>(this NativeArray<T> dst, ReadOnlySpan<T> span) where T : unmanaged
        {
            span.CopyTo(dst.AsSpan());
        }
        
        /*
        public static Pose InverseTransformPose(this Transform transform, in Pose pose)
        {
            return transform.AsPose().Inverse().Multiply(pose);
        }
        */

    }
}