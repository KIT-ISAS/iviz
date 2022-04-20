#nullable enable

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Tools;
using Iviz.Urdf;
using JetBrains.Annotations;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Color = UnityEngine.Color;
using Color32 = UnityEngine.Color32;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Core
{
    public static class UnityUtils
    {
        /// <summary>
        ///  Max amount of indices that a mesh can hold with short indices
        /// </summary>
        public const int MeshUInt16Threshold = ushort.MaxValue;

        public static CultureInfo Culture => BuiltIns.Culture;

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
        public static float MaxAbsCoeff(this in Point p) => MaxAbsCoeff(p.X, p.Y, p.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in Msgs.GeometryMsgs.Vector3 p) => MaxAbsCoeff(p.X, p.Y, p.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in Vector3 p) => MaxAbsCoeff(p.x, p.y, p.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float MaxAbsCoeff(float x, float y, float z) =>
            Mathf.Max(Mathf.Max(Mathf.Abs(x), Mathf.Abs(y)), Mathf.Abs(z));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float MaxAbsCoeff(double x, double y, double z) =>
            Mathf.Max(Mathf.Max(Mathf.Abs((float)x), Mathf.Abs((float)y)), Mathf.Abs((float)z));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in Vector4 p)
        {
            return Mathf.Max(Mathf.Max(Mathf.Max(Mathf.Abs(p.x), Mathf.Abs(p.y)), Mathf.Abs(p.z)), Mathf.Abs(p.w));
        }

        public static Vector3 InvCoeff(this Vector3 p)
        {
            Vector3 q;
            q.x = 1f / p.x;
            q.y = 1f / p.y;
            q.z = 1f / p.z;
            return q;
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
            Vector3 r;
            r.x = lhs.y * rhs.z - lhs.z * rhs.y;
            r.y = lhs.z * rhs.x - lhs.x * rhs.z;
            r.z = lhs.x * rhs.y - lhs.y * rhs.x;
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalized(this Vector3 p)
        {
            Vector3 q;
            float m = p.Magnitude();
            q.x = p.x / m;
            q.y = p.y / m;
            q.z = p.z / m;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Abs(this Vector3 p)
        {
            Vector3 q;
            q.x = Mathf.Abs(p.x);
            q.y = Mathf.Abs(p.y);
            q.z = Mathf.Abs(p.z);
            return q;
        }

        public static Vector2 Abs(this Vector2 p)
        {
            Vector2 q;
            q.x = Mathf.Abs(p.x);
            q.y = Mathf.Abs(p.y);
            return q;
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
            Pose p;
            p.position = t.position;
            p.rotation = t.rotation;
            return p;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose AsLocalPose(this Transform t)
        {
            Pose p;
            p.position = t.localPosition;
            p.rotation = t.localRotation;
            return p;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(this in Pose p, in Vector3 v)
        {
            return p.rotation * v + p.position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose Multiply(this in Pose p, in Pose o)
        {
            Pose q;
            q.position = p.rotation * o.position + p.position;
            q.rotation = p.rotation * o.rotation;
            return q;
        }

        public static void SetPose(this Transform t, in Pose p)
        {
            t.SetPositionAndRotation(p.position, p.rotation);
        }

        public static void SetParentLocal(this Transform t, Transform? parent)
        {
            t.SetParent(parent, false);
        }

        public static void SetLocalPose(this Transform t, in Pose p) => SetLocalPose(t, p.position, p.rotation);

        public static void SetLocalPose(this Transform t, in Vector3 p, in Quaternion q)
        {
            t.localPosition = p;
            t.localRotation = q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose Inverse(this in Pose p)
        {
            Pose q;
            q.rotation = p.rotation.Inverse();
            q.position = q.rotation * -p.position;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose InverseMultiply(this in Pose p, in Pose o)
        {
            Quaternion inv = p.rotation.Inverse();
            Pose q;
            q.rotation = inv * o.rotation;
            q.position = inv * (o.position - p.position);
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Inverse(this in Quaternion p)
        {
            Quaternion q;
            q.x = -p.x;
            q.y = -p.y;
            q.z = -p.z;
            q.w = p.w;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsApprox(this in Pose p, in Pose q)
        {
            return EqualsApprox(p.position, q.position) && EqualsApprox(p.rotation, q.rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool EqualsApprox(in Vector3 lhs, in Vector3 rhs)
        {
            // from unity
            float num1 = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            float num3 = lhs.z - rhs.z;
            return num1 * num1 + num2 * num2 + num3 * num3 < 9.999999439624929E-11f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool EqualsApprox(in Quaternion a, in Quaternion b)
        {
            // from unity
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w > 0.9999989867210388f;
        }

        public static Pose Lerp(this in Pose p, in Pose o, float t) => new Pose(
            Vector3.Lerp(p.position, o.position, t),
            Quaternion.Lerp(p.rotation, o.rotation, t)
        );

        public static Pose PoseFromUp(in Vector3 position, in Vector3 up)
        {
            var side = Vector3.forward.Cross(up).ApproximatelyZero()
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
        public static T? CheckedNull<T>(this T? o) where T : UnityEngine.Object => o != null ? o : null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AssertNotNull<T>(this T? o, string name,
            [CallerFilePath] string? caller = null,
            [CallerLineNumber] int lineNumber = 0) where T : class
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AssertHasComponent<T>(this GameObject? o, string name,
            [CallerFilePath] string? caller = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            if (o == null)
            {
                throw new MissingAssetFieldException(
                    $"Asset '{name}' has not been set!\n" +
                    $"At: {caller} line {lineNumber}");
            }

            if (!o.TryGetComponent(out T t))
            {
                throw new MissingAssetFieldException(
                    $"Asset '{name}' has does not have a component of type '{typeof(T).Name}!\n" +
                    $"At: {caller} line {lineNumber}");
            }

            return t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AssertHasComponent<T>(this Component? o, string name,
            [CallerFilePath] string? caller = null,
            [CallerLineNumber] int lineNumber = 0) =>
            AssertHasComponent<T>(o != null ? o.gameObject : null, name, caller, lineNumber);

        public static T EnsureHasComponent<T>(this Component? o, ref T? t, string name,
            [CallerFilePath] string? caller = null,
            [CallerLineNumber] int lineNumber = 0) =>
            t ??= o.AssertHasComponent<T>(name, caller, lineNumber);

        public static T EnsureHasComponent<T>(this GameObject? o, ref T? t, string name,
            [CallerFilePath] string? caller = null,
            [CallerLineNumber] int lineNumber = 0) =>
            t ??= o.AssertHasComponent<T>(name, caller, lineNumber);

        public static Transform EnsureHasTransform(this Component o, ref Transform? t) => t ??= o.transform;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color WithAlpha(this in Color c, float alpha)
        {
            Color q;
            q.r = c.r;
            q.g = c.g;
            q.b = c.b;
            q.a = alpha;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color32 WithAlpha(this Color32 c, byte alpha)
        {
            c.a = alpha;
            return c;
        }

        public static Color Clamp(this in Color c)
        {
            Color q;
            q.r = Mathf.Clamp01(c.r);
            q.g = Mathf.Clamp01(c.g);
            q.b = Mathf.Clamp01(c.b);
            q.a = Mathf.Clamp01(c.a);
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose WithPosition(this in Pose p, in Vector3 v)
        {
            Pose q;
            q.position = v;
            q.rotation = p.rotation;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose WithRotation(this Pose p, in Quaternion v)
        {
            Pose q;
            q.position = p.position;
            q.rotation = v;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithX(this in Vector3 c, float x)
        {
            Vector3 p;
            p.x = x;
            p.y = c.y;
            p.z = c.z;
            return p;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithY(this in Vector3 c, float y)
        {
            Vector3 p;
            p.x = c.x;
            p.y = y;
            p.z = c.z;
            return p;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithZ(this in Vector3 c, float z)
        {
            Vector3 p;
            p.x = c.x;
            p.y = c.y;
            p.z = z;
            return p;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 WithX(this Vector2 c, float x)
        {
            c.x = x;
            return c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        public static float GetValue(this in Color c)
        {
            Color.RGBToHSV(c, out _, out _, out float value);
            return value;
        }

        public static bool IsUsable(this in Pose pose)
        {
            const int maxPoseMagnitude = 100000;
            return pose.position.MaxAbsCoeff() < maxPoseMagnitude;
        }

        public static void PlaneIntersection(in Ray plane, in Ray ray, out Vector3 intersection, out float scaleRay)
        {
            scaleRay = Vector3.Dot(ray.origin - plane.origin, plane.direction) /
                       -Vector3.Dot(ray.direction, plane.direction);
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
        public static void Deconstruct(this in Vector3 v, out float x, out float y, out float z)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this Vector2 v, out float x, out float y) =>
            (x, y) = (v.x, v.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in float3 v, out float x, out float y, out float z)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Vector4 v, out float x, out float y, out float z, out float w) =>
            (x, y, z, w) = (v.x, v.y, v.z, v.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in float4 v, out float x, out float y, out float z) =>
            (x, y, z) = (v.x, v.y, v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Quaternion v, out float x, out float y, out float z, out float w) =>
            (x, y, z, w) = (v.x, v.y, v.z, v.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Bounds b, out Vector3 center, out Vector3 size) =>
            (center, size) = (b.center, b.size);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Pose p, out Vector3 position, out Quaternion rotation)
        {
            position = p.position;
            rotation = p.rotation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Ray r, out Vector3 origin, out Vector3 direction) =>
            (origin, direction) = (r.origin, r.direction);

        /// <summary>
        /// Returns the array inside a <see cref="Memory{T}"/> object to the <see cref="ArrayPool{T}"/>.
        /// Used by some iviz messages which rent arrays from the pool instead of creating a new one.
        /// </summary>
        /// <param name="memory"></param>
        /// <typeparam name="T"></typeparam>
        public static void TryReturn<T>(this SharedRent<T> memory) where T : unmanaged
        {
            memory.Dispose();
        }

        /// <summary>
        /// Empty function. Used for debugging purposes as an overload for <see cref="TryReturn{T}(SharedRent{T})"/>,
        /// in case an iviz message is temporarily set to contain an array instead of a Memory.
        /// </summary>
        public static void TryReturn(this Array? _)
        {
        }

        public static Array? Share(this Array _)
        {
            return null;
        }

        public static float RegularizeAngle(float angleInDeg) =>
            angleInDeg switch
            {
                <= -180 => angleInDeg + 360,
                > 180 => angleInDeg - 360,
                _ => angleInDeg
            };

        public static Vector3 RegularizeRpy(in Vector3 p)
        {
            Vector3 q;
            q.x = RegularizeAngle(p.x);
            q.y = RegularizeAngle(p.y);
            q.z = RegularizeAngle(p.z);
            return q;
        }

        public static void SetLocalBounds(this BoxCollider collider, in Bounds bounds) =>
            (collider.center, collider.size) = bounds;

        public static Bounds GetLocalBounds(this BoxCollider collider) => new(collider.center, collider.size);

        public static float GetHorizontalFov(this Camera camera)
        {
            float verticalFovInRad = camera.fieldOfView * Mathf.Deg2Rad;
            float horizontalFovInRad = 2 * Mathf.Atan(Mathf.Tan(verticalFovInRad / 2) * camera.aspect);
            return horizontalFovInRad * Mathf.Rad2Deg;
        }

        public static void SetHorizontalFov(this Camera camera, float horizontalFovInDeg)
        {
            float horizontalFovInRad = horizontalFovInDeg * Mathf.Deg2Rad;
            float verticalFovInRad = 2 * Mathf.Atan(Mathf.Tan(horizontalFovInRad / 2) / camera.aspect);
            camera.fieldOfView = verticalFovInRad * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Color representation from the bits of a float.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Color32 AsColor32(float f) => Unsafe.As<float, Color32>(ref f);

        /// <summary>
        /// Float representation from the bits of a Color32.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float AsFloat(Color32 f) =>  Unsafe.As<Color32, float>(ref f);

        public static Vector3 Forward(this in Quaternion rotation)
        {
            float x = rotation.x;
            float y = rotation.y;
            float z = rotation.z;
            float w = rotation.w;

            // copied from unity code
            float num1 = x * 2f;
            float num2 = y * 2f;
            float num3 = z * 2f;
            float num4 = x * num1;
            float num5 = y * num2;
            float num8 = x * num3;
            float num9 = y * num3;
            float num10 = w * num1;
            float num11 = w * num2;

            Vector3 vector3;
            vector3.x = num8 + num11;
            vector3.y = num9 - num10;
            vector3.z = 1.0f - (num4 + num5);
            return vector3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyZero(this float f) => Mathf.Abs(f) < 8 * float.Epsilon;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyZero(this double f) => Mathf.Abs((float)f) < 8 * double.Epsilon;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyZero(this in Vector3 f) => f.MaxAbsCoeff() < 8 * float.Epsilon;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyZero(this in Msgs.GeometryMsgs.Vector3 f) =>
            f.MaxAbsCoeff() < 8 * double.Epsilon;

        public static ReadOnlyDictionary<T, TU> AsReadOnly<T, TU>(this Dictionary<T, TU> t)
        {
            return new ReadOnlyDictionary<T, TU>(t);
        }

        public static WithIndexEnumerable<T> WithIndex<T>(this IEnumerable<T> source)
        {
            return new WithIndexEnumerable<T>(source);
        }

        public static T EnsureComponent<T>(this GameObject gameObject) where T : Component =>
            gameObject.TryGetComponent(out T comp) ? comp : gameObject.AddComponent<T>();

        /// <summary>
        /// Retrieves the given component from each entry in the enumerable, if it exists.
        /// </summary>
        public static IEnumerable<T> WithComponent<T>(this IEnumerable<Component> transforms)
        {
            foreach (var transform in transforms)
            {
                if (transform.TryGetComponent(out T t))
                {
                    yield return t;
                }
            }
        }

        /// <inheritdoc cref="GetAllChildren(UnityEngine.Transform)"/>
        public static IEnumerable<Transform> GetAllChildren(this GameObject transform) =>
            GetAllChildren(transform.transform);

        /// <summary>
        /// Retrieves all children without allocating an array with all results.
        /// Note: The parent is included in the enumeration.
        /// </summary>
        /// <param name="parent">The node to start the search in</param>
        /// <returns></returns>
        public static IEnumerable<Transform> GetAllChildren(this Transform parent)
        {
            return parent.childCount == 0
                ? new[] { parent } // ok, we allocate here
                : GetAllChildrenImpl(parent);

            static IEnumerable<Transform> GetAllChildrenImpl(Transform transform)
            {
                var stack = new Stack<Transform>();
                stack.Push(transform);

                while (stack.TryPop(out var childTransform))
                {
                    yield return childTransform;
                    foreach (int i in ..childTransform.childCount)
                    {
                        stack.Push(childTransform.GetChild(i));
                    }
                }
            }
        }

        public static bool TryGetFirst<T>(this IEnumerable<T> ts, [NotNullWhen(true)] out T? tp)
        {
            foreach (T t in ts)
            {
                tp = t!;
                return true;
            }

            tp = default;
            return false;
        }

        public static bool TryGetFirst<T>(this IEnumerable<T> ts, Predicate<T> p, [NotNullWhen(true)] out T? tp)
        {
            foreach (T t in ts)
            {
                if (p(t))
                {
                    tp = t!;
                    return true;
                }
            }

            tp = default;
            return false;
        }

        public static bool TryGetFirst<T>(this IReadOnlyList<T> ts, Predicate<T> p, [NotNullWhen(true)] out T? tp)
        {
            foreach (int i in ..ts.Count)
            {
                T t = ts[i];
                if (p(t))
                {
                    tp = t!;
                    return true;
                }
            }

            tp = default;
            return false;
        }

        public static void SetTextRent(this TMP_Text text, in BuilderPool.BuilderRent rent)
        {
            SetTextRent(text, rent, 0, rent.Length);
        }

        public static void SetTextRent(this TMP_Text text, in BuilderPool.BuilderRent rent, int start, int count)
        {
            if (!MemoryMarshal.TryGetArray(rent.Chunk, out ArraySegment<char> segment))
            {
                // shouldn't happen
                RosLogger.Debug($"{nameof(UnityUtils)}: Failed to retrieve array from StringBuilder memory chunk!");
                return;
            }

            text.SetCharArray(segment.Array, start, count);
        }

        /// <summary>
        /// Smallest power of 2 that is greater or equal. 
        /// </summary>
        public static int ClosestPot(int value)
        {
            int p = value >= 256 ? 256 : 16;
            while (true)
            {
                if (p >= value)
                {
                    return p;
                }

                p *= 2;
            }
        }
    }

    public readonly struct WithIndexEnumerable<T>
    {
        readonly IEnumerable<T> a;

        public struct Enumerator
        {
            readonly IEnumerator<T> a;
            int index;

            internal Enumerator(IEnumerator<T> a) => (this.a, index) = (a, -1);

            public bool MoveNext()
            {
                ++index;
                return a.MoveNext();
            }

            public (T, int) Current => (a.Current, index);
        }

        public WithIndexEnumerable(IEnumerable<T> a) => this.a = a;
        public Enumerator GetEnumerator() => new(a.GetEnumerator());
    }
}
