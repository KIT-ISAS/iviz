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

        public static implicit operator Color(in SerializableColor i)
        {
            return new Color(i.R, i.G, i.B, i.A);
        }

        public static implicit operator SerializableColor(in Color color)
        {
            return new SerializableColor(
                r: color.r,
                g: color.g,
                b: color.b,
                a: color.a
            );
        }
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

        public static implicit operator Vector3(in SerializableVector3 i) => new Vector3(i.X, i.Y, i.Z);
        public static implicit operator SerializableVector3(in Vector3 v) => new SerializableVector3(v.x, v.y, v.z);
    }

    public static class RosUtils
    {
        static readonly Quaternion XFrontToZFront = (0.5, -0.5, 0.5, -0.5);
        //public static readonly Msgs.GeometryMsgs.Quaternion ZFrontToXFront = XFrontToZFront.Inverse;

        /// Make camera point to +Z instead of +X
        public static Pose ToCameraFrame(this in Pose pose) =>
            (pose.Position, pose.Orientation * XFrontToZFront);

        public static Transform ToCameraFrame(this in Transform pose) =>
            (pose.Translation, pose.Rotation * XFrontToZFront);

        public static readonly Vector3 Ros2UnityScale = new Vector3(1, -1, 1);

        public static readonly UnityEngine.Quaternion Ros2UnityRotation =
            new UnityEngine.Quaternion(0.5f, -0.5f, 0.5f, 0.5f);

        public static readonly Vector3 Unity2RosScale = new Vector3(1, 1, -1);

        public static readonly UnityEngine.Quaternion Unity2RosRotation =
            new UnityEngine.Quaternion(0.5f, -0.5f, -0.5f, 0.5f);

        //----

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this Vector3 v)
        {
            float x = v.x;
            v.x = -v.y;
            v.y = v.z;
            v.z = x;
            return v;
            // new Vector3(-vector3.y, vector3.z, vector3.x);
         }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Ros2Unity(this in float3 vector3) => new Vector3(-vector3.y, vector3.z, vector3.x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion Ros2Unity(this in UnityEngine.Quaternion quaternion) =>
            new UnityEngine.Quaternion(quaternion.y, -quaternion.z, -quaternion.x, quaternion.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 Ros2Unity(this in float4 v) => new float4(-v.y, v.z, v.x, v.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion RosRpy2Unity(this in Vector3 v) =>
            UnityEngine.Quaternion.Euler(v.Ros2Unity() * -Mathf.Rad2Deg);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion RosRpy2Unity(this in Msgs.GeometryMsgs.Vector3 v) =>
            v.ToUnity().RosRpy2Unity();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Vector3 ToUnity(this in Vector3f v) => new Vector3(v.X, v.Y, v.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Vector3f v) => v.ToUnity().Ros2Unity();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToUnity(this in Msgs.GeometryMsgs.Vector3 p) =>
            new Vector3((float)p.X, (float)p.Y, (float)p.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Vector3 ToUnity(this in Point32 p) => new Vector3(p.X, p.Y, p.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Msgs.GeometryMsgs.Vector3 p) => p.ToUnity().Ros2Unity();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Point32 p) => p.ToUnity().Ros2Unity();

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //static Msgs.GeometryMsgs.Vector3 ToRosVector3(this in Vector3 p) => new Msgs.GeometryMsgs.Vector3(p.x, p.y, p.z);

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //static Vector3 Unity2Ros(this in Vector3 vector3) => new Vector3(vector3.z, -vector3.x, vector3.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Msgs.GeometryMsgs.Vector3 Unity2RosVector3(this in Vector3 p)
        {
            return new Msgs.GeometryMsgs.Vector3(p.z, -p.x, p.y);
            //return ToRosVector3(p.Unity2Ros());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Unity2RosPoint(this in Vector3 p)
        {
            return new Point(p.z, -p.x, p.y);
            //return ToRosPoint(p.Unity2Ros());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Vector3 ToUnity(this in Point p) => new Vector3((float)p.X, (float)p.Y, (float)p.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Point p) => p.ToUnity().Ros2Unity();

        public static Color ToUnityColor(this in ColorRGBA p) => new Color(p.R, p.G, p.B, p.A);

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

        static float SanitizeColor(float f) => float.IsNaN(f) ? 0 : Mathf.Max(Mathf.Min(f, 1), 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color32 ToUnityColor32(this in ColorRGBA p)
        {
            return (Color32)p.ToUnityColor();
            // note: Color -> Color32 sanitizes implicitly 
        }

        public static ColorRGBA ToRos(this in Color p)
        {
            return new ColorRGBA(p.r, p.g, p.b, p.a);
        }

        public static ColorRGBA ToRos(this Color32 p)
        {
            return new ColorRGBA
            (
                R: p.r / 255f,
                G: p.g / 255f,
                B: p.b / 255f,
                A: p.a / 255f
            );
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //static Point ToRosPoint(this in Vector3 p) => new Point(p.x, p.y, p.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static UnityEngine.Quaternion ToUnity(this in Quaternion p) =>
            new UnityEngine.Quaternion((float)p.X, (float)p.Y, (float)p.Z, (float)p.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion Ros2Unity(this in Quaternion p) =>
            (p.X == 0 && p.Y == 0 && p.Z == 0 && p.W == 0) ? UnityEngine.Quaternion.identity : p.ToUnity().Ros2Unity();

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //static Quaternion ToRos(this in UnityEngine.Quaternion p) => new Quaternion(p.x, p.y, p.z, p.w);

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //static UnityEngine.Quaternion Unity2Ros(this in UnityEngine.Quaternion quaternion) =>
        //    new UnityEngine.Quaternion(-quaternion.z, quaternion.x, -quaternion.y, quaternion.w);
        //----

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Quaternion Unity2RosQuaternion(this in UnityEngine.Quaternion p)
        {
            return new Quaternion(-p.z, p.x, -p.y, p.w);
            //  ToRos(p.Unity2Ros());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static UnityEngine.Pose ToUnity(this in Transform pose) =>
            new UnityEngine.Pose(pose.Translation.ToUnity(), pose.Rotation.ToUnity());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Pose Ros2Unity(this in Transform pose) =>
            new UnityEngine.Pose(pose.Translation.Ros2Unity(), pose.Rotation.Ros2Unity());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static UnityEngine.Pose ToUnity(this in Pose pose) =>
            new UnityEngine.Pose(pose.Position.ToUnity(), pose.Orientation.ToUnity());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Pose Ros2Unity(this in Pose pose) =>
            new UnityEngine.Pose(pose.Position.Ros2Unity(), pose.Orientation.Ros2Unity());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform Unity2RosTransform(this in UnityEngine.Pose p) =>
            new Transform(p.position.Unity2RosVector3(), p.rotation.Unity2RosQuaternion());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose Unity2RosPose(this in UnityEngine.Pose p) =>
            new Pose(p.position.Unity2RosPoint(), p.rotation.Unity2RosQuaternion());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this float f) => float.IsNaN(f) || float.IsInfinity(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this double f) => double.IsNaN(f) || double.IsInfinity(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this in float4 v) => float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this in float3 v) => float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this in Vector3 v) => float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);

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