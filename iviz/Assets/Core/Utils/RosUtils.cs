#nullable enable

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Unity.Mathematics;
using UnityEngine;
using Color32 = UnityEngine.Color32;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;
using Quaternion = Iviz.Msgs.GeometryMsgs.Quaternion;
using Transform = Iviz.Msgs.GeometryMsgs.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Core
{
    [DataContract]
    public struct SerializableColor
    {
        [DataMember] public float R { get; set; }
        [DataMember] public float G { get; set; }
        [DataMember] public float B { get; set; }
        [DataMember] public float A { get; set; }

        public SerializableColor(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static implicit operator Color(in SerializableColor i) => new(i.R, i.G, i.B, i.A);

        public static implicit operator SerializableColor(in Color color) => new(color.r, color.g, color.b, color.a);
    }

    [DataContract]
    public struct SerializableVector3
    {
        [DataMember] public float X { get; set; }
        [DataMember] public float Y { get; set; }
        [DataMember] public float Z { get; set; }

        public SerializableVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator Vector3(in SerializableVector3 i) => new(i.X, i.Y, i.Z);
        public static implicit operator SerializableVector3(in Vector3 v) => new(v.x, v.y, v.z);
    }

    public static class RosUtils
    {
        static readonly Quaternion XFrontToZFront = (0.5, -0.5, 0.5, -0.5);

        /// Make camera point to +Z instead of +X
        public static Pose ToCameraFrame(this in Pose pose) =>
            (pose.Position, pose.Orientation * XFrontToZFront);

        public static Transform ToCameraFrame(this in Transform pose) =>
            (pose.Translation, pose.Rotation * XFrontToZFront);

        //----

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this Vector3 v)
        {
            float x = v.x;
            v.x = -v.y;
            v.y = v.z;
            v.z = x;
            return v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion Ros2Unity(this in UnityEngine.Quaternion quaternion) =>
            new(quaternion.y, -quaternion.z, -quaternion.x, quaternion.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 Ros2Unity(this in float4 v) =>
            new(-v.y, v.z, v.x, v.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion RosRpy2Unity(this in Vector3 v) =>
            UnityEngine.Quaternion.Euler(v.Ros2Unity() * -Mathf.Rad2Deg);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion RosRpy2Unity(this in Msgs.GeometryMsgs.Vector3 v) =>
            v.ToUnity().RosRpy2Unity();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Vector3f p) =>
            new(-p.Y, p.Z, p.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToUnity(this in Msgs.GeometryMsgs.Vector3 p) =>
            new((float)p.X, (float)p.Y, (float)p.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Msgs.GeometryMsgs.Vector3 p) =>
            new((float)-p.Y, (float)p.Z, (float)p.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Point32 p) =>
            new(-p.Y, p.Z, p.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Msgs.GeometryMsgs.Vector3 Unity2RosVector3(this in Vector3 p) =>
            new(p.z, -p.x, p.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Unity2RosPoint(this in Vector3 p) =>
            new(p.z, -p.x, p.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Point p) =>
            new((float)-p.Y, (float)p.Z, (float)p.X);

        public static Color ToUnityColor(this in ColorRGBA p) =>
            new(p.R, p.G, p.B, p.A);

        public static ColorRGBA Sanitize(this in ColorRGBA p)
        {
            return new ColorRGBA
            (
                R: SanitizeColor(p.R),
                G: SanitizeColor(p.G),
                B: SanitizeColor(p.B),
                A: SanitizeColor(p.A)
            );
        }

        static float SanitizeColor(float f) => float.IsNaN(f) ? 0 : Mathf.Clamp01(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color32 ToUnityColor32(this in ColorRGBA c) =>
            new(
                (byte)Mathf.Round(Mathf.Clamp01(c.R) * byte.MaxValue),
                (byte)Mathf.Round(Mathf.Clamp01(c.G) * byte.MaxValue),
                (byte)Mathf.Round(Mathf.Clamp01(c.B) * byte.MaxValue),
                (byte)Mathf.Round(Mathf.Clamp01(c.A) * byte.MaxValue)
            );
        // note: taken from unity Color 

        public static ColorRGBA ToRos(this in Color p) => new(p.r, p.g, p.b, p.a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static UnityEngine.Quaternion ToUnity(this in Quaternion p) =>
            new((float)p.X, (float)p.Y, (float)p.Z, (float)p.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion Ros2Unity(this in Quaternion p) =>
            (p.X == 0 && p.Y == 0 && p.Z == 0 && p.W == 0) ? UnityEngine.Quaternion.identity : p.ToUnity().Ros2Unity();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Quaternion Unity2RosQuaternion(this in UnityEngine.Quaternion p) =>
            new(-p.z, p.x, -p.y, p.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Pose Ros2Unity(this in Transform pose) =>
            new(pose.Translation.Ros2Unity(), pose.Rotation.Ros2Unity());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Pose Ros2Unity(this in Pose pose) =>
            new(pose.Position.Ros2Unity(), pose.Orientation.Ros2Unity());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform Unity2RosTransform(this in UnityEngine.Pose p) =>
            new(p.position.Unity2RosVector3(), p.rotation.Unity2RosQuaternion());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose Unity2RosPose(this in UnityEngine.Pose p) =>
            new Pose(p.position.Unity2RosPoint(), p.rotation.Unity2RosQuaternion());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this float f) =>
            float.IsNaN(f) || float.IsInfinity(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this double f) =>
            double.IsNaN(f) || double.IsInfinity(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this in float4 v) =>
            float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this in float3 v) =>
            float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this in Vector3 v) =>
            float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this in ColorRGBA v) =>
            float.IsNaN(v.R) || float.IsNaN(v.G) || float.IsNaN(v.B) || float.IsNaN(v.A);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this in Msgs.GeometryMsgs.Vector3 v) =>
            double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this in Point v) =>
            double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this in Point32 v) =>
            float.IsNaN(v.X) || float.IsNaN(v.Y) || float.IsNaN(v.Z);

        static bool HasNaN(this in Quaternion v) =>
            double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z) || double.IsNaN(v.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this in Transform transform) =>
            HasNaN(transform.Rotation) || HasNaN(transform.Translation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this in Pose pose) => HasNaN(pose.Orientation) || HasNaN(pose.Position);
    }
}