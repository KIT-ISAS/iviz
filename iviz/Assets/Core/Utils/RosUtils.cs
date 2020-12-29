using UnityEngine;
using System;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Mathematics;

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

        public static implicit operator Vector3(in SerializableVector3 i)
        {
            return new Vector3(i.X, i.Y, i.Z);
        }

        public static implicit operator SerializableVector3(in Vector3 v)
        {
            return new SerializableVector3(
                x: v.x,
                y: v.y,
                z: v.z
            );
        }
    }

    public static class RosUtils
    {
        const string BaseFrameId = "map";
        
        public static readonly Vector3 Ros2UnityScale = new Vector3(1, -1, 1);
        public static readonly Quaternion Ros2UnityRotation = new Quaternion(0.5f, -0.5f, 0.5f, 0.5f);

        public static readonly Vector3 Unity2RosScale = new Vector3(1, 1, -1);
        public static readonly Quaternion Unity2RosRotation = new Quaternion(0.5f, -0.5f, -0.5f, 0.5f);

        //----
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Unity2Ros(this Vector3 vector3) => new Vector3(vector3.z, -vector3.x, vector3.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this Vector3 vector3) => new Vector3(-vector3.y, vector3.z, vector3.x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Ros2Unity(this float3 vector3) => new Vector3(-vector3.y, vector3.z, vector3.x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Ros2Unity(this Quaternion quaternion) =>
            new Quaternion(quaternion.y, -quaternion.z, -quaternion.x, quaternion.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Quaternion Unity2Ros(this Quaternion quaternion) =>
            new Quaternion(-quaternion.z, quaternion.x, -quaternion.y, quaternion.w);
        //----

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 Ros2Unity(this float4 v) => new float4(-v.y, v.z, v.x, v.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion RosRpy2Unity(this Vector3 v) => Quaternion.Euler(v.Ros2Unity() * -Mathf.Rad2Deg);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Vector3 ToUnity(this Msgs.GeometryMsgs.Vector3 p)
        {
            return new Vector3((float) p.X, (float) p.Y, (float) p.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Vector3 ToUnity(this Msgs.GeometryMsgs.Point32 p)
        {
            return new Vector3(p.X, p.Y, p.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this Msgs.GeometryMsgs.Vector3 p)
        {
            return p.ToUnity().Ros2Unity();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this Msgs.GeometryMsgs.Point32 p)
        {
            return p.ToUnity().Ros2Unity();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Msgs.GeometryMsgs.Vector3 ToRosVector3(this Vector3 p)
        {
            return new Msgs.GeometryMsgs.Vector3(p.x, p.y, p.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Msgs.GeometryMsgs.Vector3 Unity2RosVector3(this Vector3 p)
        {
            return ToRosVector3(p.Unity2Ros());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Vector3 ToUnity(this Msgs.GeometryMsgs.Point p)
        {
            return new Vector3((float) p.X, (float) p.Y, (float) p.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this Msgs.GeometryMsgs.Point p)
        {
            return p.ToUnity().Ros2Unity();
        }

        public static Color ToUnityColor(this ColorRGBA p)
        {
            return new Color(p.R, p.G, p.B, p.A);
        }

        public static ColorRGBA Sanitize(this ColorRGBA p)
        {
            return new ColorRGBA
            (
                R: SanitizeColor(p.R),
                G: SanitizeColor(p.G),
                B: SanitizeColor(p.B),
                A: SanitizeColor(p.A)
            );
        }

        static float SanitizeColor(float f)
        {
            return float.IsNaN(f) ? 0 : Mathf.Max(Mathf.Min(f, 1), 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color32 ToUnityColor32(this ColorRGBA p)
        {
            return (Color32) p.ToUnityColor();
            // note: Color -> Color32 sanitizes implicitly 
        }

        public static ColorRGBA ToRos(this Color p)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Msgs.GeometryMsgs.Point ToRosPoint(this Vector3 p)
        {
            return new Msgs.GeometryMsgs.Point
            (
                X: p.x,
                Y: p.y,
                Z: p.z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Msgs.GeometryMsgs.Point Unity2RosPoint(this Vector3 p)
        {
            return ToRosPoint(p.Unity2Ros());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Quaternion ToUnity(this Msgs.GeometryMsgs.Quaternion p)
        {
            return new Quaternion((float) p.X, (float) p.Y, (float) p.Z, (float) p.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Ros2Unity(this Msgs.GeometryMsgs.Quaternion p)
        {
            return p.ToUnity().Ros2Unity();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Msgs.GeometryMsgs.Quaternion ToRos(this Quaternion p)
        {
            return new Msgs.GeometryMsgs.Quaternion
            (
                X: p.x,
                Y: p.y,
                Z: p.z,
                W: p.w
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Msgs.GeometryMsgs.Quaternion Unity2RosQuaternion(this Quaternion p)
        {
            return ToRos(p.Unity2Ros());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Pose ToUnity(this Msgs.GeometryMsgs.Transform pose)
        {
            return new Pose(pose.Translation.ToUnity(), pose.Rotation.ToUnity());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose Ros2Unity(this Msgs.GeometryMsgs.Transform pose)
        {
            return new Pose(pose.Translation.Ros2Unity(), pose.Rotation.Ros2Unity());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Pose ToUnity(this Msgs.GeometryMsgs.Pose pose)
        {
            return new Pose(pose.Position.ToUnity(), pose.Orientation.ToUnity());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose Ros2Unity(this Msgs.GeometryMsgs.Pose pose)
        {
            return new Pose(pose.Position.Ros2Unity(), pose.Orientation.Ros2Unity());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Msgs.GeometryMsgs.Transform Unity2RosTransform(this Pose p)
        {
            return new Msgs.GeometryMsgs.Transform(p.position.Unity2RosVector3(), p.rotation.Unity2RosQuaternion());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Msgs.GeometryMsgs.Pose Unity2RosPose(this Pose p)
        {
            return new Msgs.GeometryMsgs.Pose(p.position.Unity2RosPoint(), p.rotation.Unity2RosQuaternion());
        }

        [NotNull]
        public static Header CreateHeader(uint seq = 0, [CanBeNull] string frameId = null, time? timestamp = null)
        {
            return new Header(seq, timestamp ?? time.Now(), frameId ?? BaseFrameId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this float f)
        {
            return float.IsNaN(f) || float.IsInfinity(f);
        }      
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this double f)
        {
            return double.IsNaN(f) || double.IsInfinity(f);
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this float4 v) => float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this float3 v) => float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this Vector3 v) => float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this ColorRGBA v) =>
            float.IsNaN(v.R) || float.IsNaN(v.G) || float.IsNaN(v.B) || float.IsNaN(v.A);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this Msgs.GeometryMsgs.Vector3 v)
        {
            return double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this Msgs.GeometryMsgs.Point v)
        {
            return double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this Msgs.GeometryMsgs.Point32 v)
        {
            return float.IsNaN(v.X) || float.IsNaN(v.Y) || float.IsNaN(v.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool HasNaN(this Msgs.GeometryMsgs.Quaternion v)
        {
            return double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z) || double.IsNaN(v.W);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this Msgs.GeometryMsgs.Transform transform)
        {
            return HasNaN(transform.Rotation) || HasNaN(transform.Translation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNaN(this Msgs.GeometryMsgs.Pose pose)
        {
            return HasNaN(pose.Orientation) || HasNaN(pose.Position);
        }
    }
}