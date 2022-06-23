#nullable enable
using System.Runtime.CompilerServices;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Unity.Mathematics;
using UnityEngine;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;
using Quaternion = Iviz.Msgs.GeometryMsgs.Quaternion;
using Transform = Iviz.Msgs.GeometryMsgs.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Core
{
    public static class ValidationUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this float f) => (Unsafe.As<float, int>(ref f) & 0x7FFFFFFF) == 0x7F800000;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this double f) =>
            (Unsafe.As<double, long>(ref f) & 0x7FFFFFFFFFFFFFFF) == 0x7FF0000000000000;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid3(this in float4 v) =>
            v.x.IsInvalid() || v.y.IsInvalid() || v.z.IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in float3 v) =>
            v.x.IsInvalid() || v.y.IsInvalid() || v.z.IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in Vector3 v) =>
            v.x.IsInvalid() || v.y.IsInvalid() || v.z.IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in ColorRGBA v) =>
            v.R.IsInvalid() || v.G.IsInvalid() || v.B.IsInvalid() || v.A.IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in Msgs.GeometryMsgs.Vector3 v) => v.ToUnity().IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in Point v) => v.ToUnity().IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in Point32 v) =>
            v.X.IsInvalid() || v.Y.IsInvalid() || v.Z.IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsInvalid(this in Quaternion v) => v.ToUnity().IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in UnityEngine.Quaternion v) =>
            v.x.IsInvalid() || v.y.IsInvalid() || v.z.IsInvalid() || v.w.IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in Transform transform) =>
            IsInvalid(transform.Translation) || IsInvalid(transform.Rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in Pose pose) => IsInvalid(pose.Position.ToUnity()) || IsInvalid(pose.Orientation.ToUnity());
     
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in Bounds v) => v.center.IsInvalid() || v.size.IsInvalid();
    }
}