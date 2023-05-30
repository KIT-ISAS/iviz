#nullable enable

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.Tools;
using JetBrains.Annotations;
using Newtonsoft.Json;
using TMPro;
using Unity.Jobs;
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
        public static CultureInfo Culture => BuiltIns.Culture;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in float3 p) => MaxAbsCoeff(p.x, p.y, p.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff3(this in float4 p) => MaxAbsCoeff(p.x, p.y, p.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in Point p) => p.ToUnity().MaxAbsCoeff();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in Vector3 p) => MaxAbsCoeff(p.x, p.y, p.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in Msgs.GeometryMsgs.Vector3 p) => p.ToUnity().MaxAbsCoeff();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float MaxAbsCoeff(float x, float y, float z) =>
            Mathf.Max(Mathf.Max(Mathf.Abs(x), Mathf.Abs(y)), Mathf.Abs(z));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this Vector4 p)
        {
            return Unsafe.As<Vector4, Quaternion>(ref p).MaxAbsCoeff();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxAbsCoeff(this in Quaternion p)
        {
            return Mathf.Max(Mathf.Max(Mathf.Max(Mathf.Abs(p.x), Mathf.Abs(p.y)), Mathf.Abs(p.z)), Mathf.Abs(p.w));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ToVector(this Quaternion p)
        {
            return Unsafe.As<Quaternion, Vector4>(ref p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Magnitude(this in Vector3 v)
        {
            return Mathf.Sqrt(v.sqrMagnitude);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 InvCoeffs(this in Vector3 v)
        {
            Vector3 i;
            i.x = 1f / v.x;
            i.y = 1f / v.y;
            i.z = 1f / v.z;
            return i;
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
        public static Vector3 Abs(this in Vector3 p)
        {
            Vector3 q;
            q.x = Mathf.Abs(p.x);
            q.y = Mathf.Abs(p.y);
            q.z = Mathf.Abs(p.z);
            return q;
        }

        public static Vector2 Abs(this in Vector2 p)
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
        public static bool EqualsApprox(this float a, float b) => Mathf.Approximately(a, b);

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

        public static Pose Lerp(this in Pose p, in Pose o, float t) => new(
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
        public static T? CheckedNull<T>(this T? o) where T : UnityEngine.Object => o != null ? o : null;

        [AssertionMethod]
        public static T AssertNotNull<T>(this T? o, string name,
            [CallerFilePath] string? caller = null,
            [CallerLineNumber] int lineNumber = 0) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            if (o == null)
#else
            if (o is null)
#endif
            {
                ThrowMissingAssetField(name, null, caller, lineNumber);
            }

            return o;
        }

        public static T AssertHasComponent<T>(this GameObject? o, string name,
            [CallerFilePath] string? caller = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            if (o == null)
            {
                ThrowMissingAssetField(name, null, caller, lineNumber);
            }

            if (!o.TryGetComponent(out T t))
            {
                ThrowMissingAssetField(name, typeof(T), caller, lineNumber);
            }

            return t;
        }

        [DoesNotReturn]
        static void ThrowMissingAssetField(string name, Type? type, string? caller, int lineNumber)
        {
            string s = type == null
                ? $"Asset '{name}' has not been set!\n" +
                  $"At: {caller} line {lineNumber.ToString()}"
                : $"Asset '{name}' has not been set or does not have a component of type '{type.Name}!\n" +
                  $"At: {caller} line {lineNumber.ToString()}";
            ThrowHelper.ThrowMissingAssetField(s);
        }

        [AssertionMethod]
        public static T AssertHasComponent<T>(this Component? o, string name,
            [CallerFilePath] string? caller = null,
            [CallerLineNumber] int lineNumber = 0) =>
            AssertHasComponent<T>(o != null ? o.gameObject : null, name, caller, lineNumber);

        [AssertionMethod]
        public static T EnsureHasComponent<T>(this Component? o, ref T? t, string name,
            [CallerFilePath] string? caller = null,
            [CallerLineNumber] int lineNumber = 0) =>
            t ??= o.AssertHasComponent<T>(name, caller, lineNumber);

        [AssertionMethod]
        public static T EnsureHasComponent<T>(this GameObject? o, ref T? t, string name,
            [CallerFilePath] string? caller = null,
            [CallerLineNumber] int lineNumber = 0) =>
            t ??= o.AssertHasComponent<T>(name, caller, lineNumber);

        [AssertionMethod]
        public static Transform EnsureHasTransform(this Component o, ref Transform? t) =>
            t != null ? t : (t = o.transform);

        [AssertionMethod]
        public static RectTransform EnsureHasTransform(this Component o, ref RectTransform? t) =>
            t != null ? t : (t = (RectTransform)o.transform);

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose WithPosition(this in Pose p, in Vector3 v)
        {
            Pose q;
            q.position = v;
            q.rotation = p.rotation;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose WithRotation(this in Pose p, in Quaternion v)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithZ(this Vector2 c, float z)
        {
            Vector3 v;
            v.x = c.x;
            v.y = c.y;
            v.z = z;
            return v;
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

        public static Color ScaledBy(this in Color c, float value)
        {
            Color d;
            d.r = c.r * value;
            d.g = c.g * value;
            d.b = c.b * value;
            d.a = c.a;
            return d;
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
        public static void Deconstruct(this Vector2 v, out float x, out float y) => (x, y) = (v.x, v.y);

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

        public static void TryReturn(this SharedRent memory)
        {
            memory.Dispose();
        }

        public static void TryReturn(this Array? _)
        {
        }

        public static Array? Share(this Array _)
        {
            return null;
        }

        public static void IncreaseRefCount(this PointCloud2 msg)
        {
            msg.Data.Share();
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
        public static float AsFloat(Color32 f) => Unsafe.As<Color32, float>(ref f);

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

        public static Vector3 Up(this in Quaternion rotation)
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
            float num6 = z * num3;
            float num7 = x * num2;
            float num9 = y * num3;
            float num10 = w * num1;
            float num12 = w * num3;

            Vector3 vector3;
            vector3.x = num7 - num12;
            vector3.y = 1.0f - (num4 + num6);
            vector3.z = num9 + num10;
            return vector3;
        }

        public static Vector3 Forward(this in Pose pose) => pose.rotation.Forward();

        public static Vector3 Up(this in Pose pose) => pose.rotation.Up();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion FromEulerRad(in Vector3 euler)
        {
            // stolen from https://gist.github.com/HelloKitty/91b7af87aac6796c3da9
            float yaw = euler.x;
            float pitch = euler.y;
            float roll = euler.z;
            float rollOver2 = roll * 0.5f;
            float sinRollOver2 = Mathf.Sin(rollOver2);
            float cosRollOver2 = Mathf.Cos(rollOver2);
            float pitchOver2 = pitch * 0.5f;
            float sinPitchOver2 = Mathf.Sin(pitchOver2);
            float cosPitchOver2 = Mathf.Cos(pitchOver2);
            float yawOver2 = yaw * 0.5f;
            float sinYawOver2 = Mathf.Sin(yawOver2);
            float cosYawOver2 = Mathf.Cos(yawOver2);
            Quaternion result;
            result.x = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            result.z = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.w = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyZero(this float f) => Mathf.Abs(f) < 8 * float.Epsilon;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyZero(this double f) => Mathf.Abs((float)f) < 8 * double.Epsilon;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyZero(this in Vector3 f) => f.MaxAbsCoeff() < 8 * float.Epsilon;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ApproximatelyZero(this in Msgs.GeometryMsgs.Vector3 f) => f.ToUnity().ApproximatelyZero();

        public static WithIndexEnumerable<T> WithIndex<T>(this IEnumerable<T> source)
        {
            return new WithIndexEnumerable<T>(source);
        }

        /// <summary>
        /// Adds a component but only if it does not exist already. 
        /// </summary>
        public static T TryAddComponent<T>(this GameObject gameObject) where T : Component =>
            gameObject.TryGetComponent(out T comp) ? comp : gameObject.AddComponent<T>();

        /// <summary>
        /// Retrieves the given component from each entry in the enumerable, if it exists.
        /// </summary>
        public static IEnumerable<T> WithComponent<T>(this IEnumerable<Component> transforms) where T : Component
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
        public static IEnumerable<Transform> GetAllChildren(this Transform parent)
        {
            return parent.childCount == 0
                ? new[] { parent } // ok, we allocate here
                : GetAllChildrenCore(parent);
        }

        static IEnumerable<Transform> GetAllChildrenCore(Transform transform)
        {
            var stack = new Stack<Transform>(8);
            stack.Push(transform);

            while (stack.TryPop(out var childTransform))
            {
                yield return childTransform;
                foreach (var grandChild in childTransform.GetChildren())
                {
                    stack.Push(grandChild);
                }
            }
        }

        public static bool TryGetFirst(this HashSet<string> ts, [NotNullWhen(true)] out string? tp)
        {
            using var enumerator = ts.GetEnumerator();
            if (enumerator.MoveNext())
            {
                tp = enumerator.Current;
                return true;
            }

            tp = null;
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

        public static void SetTextRent(this TMP_Text text, in BuilderPool.BuilderRent rent)
        {
            SetTextRent(text, rent, 0, rent.Length);
        }

        public static void SetTextRent(this TMP_Text text, in BuilderPool.BuilderRent rent, int start, int count)
        {
            if (!MemoryMarshal.TryGetArray(rent.Chunk, out var segment))
            {
                // shouldn't happen
                RosLogger.Debug(
                    $"{nameof(UnityUtils)}: Failed to retrieve array from {nameof(StringBuilder)} memory chunk!");
                return;
            }

            text.SetCharArray(segment.Array, start, count);
        }

        public static TransformEnumerator GetChildren(this Transform t) => new(t);

        public static string GetPlatformName() => Application.platform switch
        {
            RuntimePlatform.OSXEditor or RuntimePlatform.OSXPlayer or RuntimePlatform.OSXServer => "osx",
            RuntimePlatform.LinuxEditor or RuntimePlatform.LinuxPlayer or RuntimePlatform.LinuxServer => "linux",
            RuntimePlatform.WindowsEditor or RuntimePlatform.WindowsPlayer or RuntimePlatform.WindowsServer => "win",
            RuntimePlatform.IPhonePlayer => "ios",
            RuntimePlatform.Android => "android",
            RuntimePlatform.WSAPlayerX64 or RuntimePlatform.WSAPlayerX86 or RuntimePlatform.WSAPlayerARM => "wsa",
            _ => Application.platform.ToString().ToLower()
        };

        public static Task AsTask(this JobHandle handle)
        {
            return GameThread.WaitUntilAsync(() => handle.IsCompleted); // IsCompleted schedules batched job
        }

        public static string FormatFloat(float x) => x.IsInvalid() || Mathf.Abs(x) >= 1e4f
            ? x.ToString("G", Culture)
            : x.ToString("#,0.###", Culture);

        public static string FormatFloat(double x) => x.IsInvalid() || Math.Abs(x) >= 1e4
            ? x.ToString("G", Culture)
            : x.ToString("#,0.###", Culture);

        public static void MakeHalfLitAlwaysVisible(this ISupportsOverrideMaterial display)
        {
            display.OverrideMaterial(Resource.Materials.LitHalfVisible.Object);
        }

        public static void TryRaise(this Action? action, object callerName,
            [CallerMemberName] string? methodName = null)
        {
            if (action == null) return;
            
            try
            {
                action();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{callerName}: Error during {methodName}", e);
            }
        }

        public static void TryRaise<T>(this Action<T>? action, T arg, object callerName,
            [CallerMemberName] string? methodName = null)
        {
            if (action == null) return;

            try
            {
                action(arg);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{callerName}: Error during {methodName}", e);
            }
        }
    }

    public struct WithIndexEnumerable<T>
    {
        readonly IEnumerator<T> a;
        int index;

        public bool MoveNext() => (++index, Success: a.MoveNext()).Success;
        public (T value, int index) Current => (a.Current, index);
        public WithIndexEnumerable(IEnumerable<T> a) => (this.a, index) = (a.GetEnumerator(), -1);
        public WithIndexEnumerable<T> GetEnumerator() => this;
    }

    public struct TransformEnumerator
    {
        readonly Transform transform;
        readonly int childCount;
        int currentIndex;

        public TransformEnumerator(Transform transform)
        {
            this.transform = transform;
            childCount = transform.childCount;
            currentIndex = -1;
        }

        public Transform Current => transform.GetChild(currentIndex);
        public bool MoveNext() => ++currentIndex < childCount;
        public TransformEnumerator GetEnumerator() => this;
    }

    public static class JsonUtils
    {
        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value) ??
                   throw new JsonException("Object could not be deserialized");
        }
    }

    public struct InterlockedBoolean
    {
        int value;

        public Action<bool>? Changed;

        public bool TrySet()
        {
            bool result = Interlocked.CompareExchange(ref value, 1, 0) == 0;
            if (result)
            {
                Changed.TryRaise(true, nameof(InterlockedBoolean));
            }

            return result;
        }

        public void Reset()
        {
            value = 0;
            Changed.TryRaise(false, nameof(InterlockedBoolean));
        }
    }
}