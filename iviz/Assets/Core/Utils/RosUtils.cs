#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Text;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Tools;
using Unity.Mathematics;
using UnityEngine;
using Color32 = UnityEngine.Color32;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;
using Quaternion = Iviz.Msgs.GeometryMsgs.Quaternion;
using Transform = Iviz.Msgs.GeometryMsgs.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Core
{
    public static class RosUtils
    {
        /// Make camera point to +Z instead of +X
        public static Pose ToCameraFrame(this in Pose p)
        {
            var xFrontToZFront = new Quaternion(0.5, -0.5, 0.5, -0.5);

            Pose q;
            q.Position = p.Position;
            q.Orientation = p.Orientation * xFrontToZFront;
            return q;
        }

        public static Pose FromCameraFrame(this in Pose pose)
        {
            Pose q;
            q.Position = pose.Position;
            q.Orientation = pose.Orientation * /*XFrontToZFront.Inverse*/ new Quaternion(-0.5, 0.5, -0.5, -0.5);
            return q;
        }

        public static UnityEngine.Pose ToCameraFrame(this in UnityEngine.Pose p)
        {
            var xFrontToZFront = new Quaternion(0.5, -0.5, 0.5, -0.5);

            UnityEngine.Pose q;
            q.position = p.position;
            p.rotation.Unity2Ros(out var rosOrientation);
            (rosOrientation * xFrontToZFront).Ros2Unity(out q.rotation);
            return q;
        }

        //----

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Vector3 p)
        {
            Vector3 q;
            q.x = -p.y;
            q.y = p.z;
            q.z = p.x;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion Ros2Unity(this in UnityEngine.Quaternion quaternion) =>
            new(quaternion.y, -quaternion.z, -quaternion.x, quaternion.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion RosRpy2Unity(this in Vector3 v) =>
            UnityEngine.Quaternion.Euler(v.Ros2Unity() * -Mathf.Rad2Deg);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion RosRpy2Unity(this in Msgs.GeometryMsgs.Vector3 v) =>
            v.ToUnity().RosRpy2Unity();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Unity2RosRpy(this in Vector3 v) => (v * -Mathf.Deg2Rad).Unity2Ros();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Vector3f p)
        {
            Vector3 q;
            q.x = -p.Y;
            q.y = p.Z;
            q.z = p.X;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToUnity(this in Msgs.GeometryMsgs.Vector3 p)
        {
            Vector3 q;
            q.x = (float)p.X;
            q.y = (float)p.Y;
            q.z = (float)p.Z;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion ToUnity(this in Quaternion p)
        {
            UnityEngine.Quaternion q;
            q.x = (float)p.X;
            q.y = (float)p.Y;
            q.z = (float)p.Z;
            q.w = (float)p.W;
            return q;
        }

        public static Msgs.GeometryMsgs.Vector3 ToRos(this in Vector3 p) => new(p.x, p.y, p.z);

        public static Quaternion ToRos(this in UnityEngine.Quaternion p) => new(p.x, p.y, p.z, p.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Msgs.GeometryMsgs.Vector3 p)
        {
            //new((float)-p.Y, (float)p.Z, (float)p.X);
            Vector3 q;
            q.x = (float)-p.Y;
            q.y = (float)p.Z;
            q.z = (float)p.X;
            return q;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Point32 p)
        {
            //return new(-p.Y, p.Z, p.X);
            Vector3 q;
            q.x = -p.Y;
            q.y = p.Z;
            q.z = p.X;
            return q;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Msgs.GeometryMsgs.Vector3 Abs(this in Msgs.GeometryMsgs.Vector3 p)
        {
            Msgs.GeometryMsgs.Vector3 q;
            q.X = Math.Abs(p.X);
            q.Y = Math.Abs(p.Y);
            q.Z = Math.Abs(p.Z);
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Msgs.GeometryMsgs.Vector3 Unity2RosVector3(this in Vector3 p)
        {
            //new(p.z, -p.x, p.y);
            Msgs.GeometryMsgs.Vector3 q;
            q.X = p.z;
            q.Y = -p.x;
            q.Z = p.y;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Unity2Ros(this in Vector3 p, out Msgs.GeometryMsgs.Vector3 q)
        {
            //new(p.z, -p.x, p.y);
            q.X = p.z;
            q.Y = -p.x;
            q.Z = p.y;
        }

        static void Unity2Ros(this in Vector3 p, out Point q)
        {
            //new(p.z, -p.x, p.y);
            q.X = p.z;
            q.Y = -p.x;
            q.Z = p.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Unity2RosPoint(this in Vector3 p)
        {
            // new(p.z, -p.x, p.y);
            Point q;
            q.X = p.z;
            q.Y = -p.x;
            q.Z = p.y;
            return q;
        }

        public static Vector3 Unity2Ros(this in Vector3 p)
        {
            //new(p.z, -p.x, p.y);
            Vector3 q;
            q.x = p.z;
            q.y = -p.x;
            q.z = p.y;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Point p)
        {
            //return new((float)-p.Y, (float)p.Z, (float)p.X);
            Vector3 q;
            q.x = (float)-p.Y;
            q.y = (float)p.Z;
            q.z = (float)p.X;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Ros2Unity(this in Point p, float w, out float4 q)
        {
            //(f.x, f.y, f.z) = ((float)-p.Y, (float)p.Z, (float)p.X);
            q.x = (float)-p.Y;
            q.y = (float)p.Z;
            q.z = (float)p.X;
            q.w = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Ros2Unity(this in Point p, out Vector3 q)
        {
            q.x = (float)-p.Y;
            q.y = (float)p.Z;
            q.z = (float)p.X;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Ros2Unity(this in Msgs.GeometryMsgs.Vector3 p, out Vector3 q)
        {
            q.x = (float)-p.Y;
            q.y = (float)p.Z;
            q.z = (float)p.X;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ToUnity(this in ColorRGBA c)
        {
            Color d;
            d.r = c.R;
            d.g = c.G;
            d.b = c.B;
            d.a = c.A;
            return d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorRGBA Sanitize(this in ColorRGBA c)
        {
            ColorRGBA d;
            d.R = SanitizeChannel(c.R);
            d.G = SanitizeChannel(c.G);
            d.B = SanitizeChannel(c.B);
            d.A = SanitizeChannel(c.A);
            return d;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static float SanitizeChannel(float f) => f.IsInvalid() ? 0 : Mathf.Clamp(f, 0, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color32 ToUnityColor32(this in ColorRGBA c)
        {
            var d = Sanitize(c);
            return new Color32(
                (byte)(d.R * 255),
                (byte)(d.G * 255),
                (byte)(d.B * 255),
                (byte)(d.A * 255)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorRGBA ToRos(this in Color p) => new(p.r, p.g, p.b, p.a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion Ros2Unity(this in Quaternion p)
        {
            Ros2Unity(p, out var q);
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Ros2Unity(this in Quaternion p, out UnityEngine.Quaternion q)
        {
            // (p.X == 0 && p.Y == 0 && p.Z == 0 && p.W == 0) ? UnityEngine.Quaternion.identity : p.ToUnity().Ros2Unity();
            if (p.X == 0 && p.Y == 0 && p.Z == 0 && p.W == 0)
            {
                q.x = 0;
                q.y = 0;
                q.z = 0;
                q.w = 1;
            }
            else
            {
                q.x = (float)p.Y;
                q.y = (float)-p.Z;
                q.z = (float)-p.X;
                q.w = (float)p.W;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Unity2Ros(this in UnityEngine.Quaternion p, out Quaternion q)
        {
            //new(-p.z, p.x, -p.y, p.w);
            q.X = -p.z;
            q.Y = p.x;
            q.Z = -p.y;
            q.W = p.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Pose Ros2Unity(this in Transform p)
        {
            //new(pose.Translation.Ros2Unity(), pose.Rotation.Ros2Unity());
            UnityEngine.Pose q;
            p.Translation.Ros2Unity(out q.position);
            p.Rotation.Ros2Unity(out q.rotation);
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Pose Ros2Unity(this in Pose p)
        {
            //new(pose.Position.Ros2Unity(), pose.Orientation.Ros2Unity());
            UnityEngine.Pose q;
            p.Position.Ros2Unity(out q.position);
            p.Orientation.Ros2Unity(out q.rotation);
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform Unity2RosTransform(this in UnityEngine.Pose p)
        {
            //new(p.position.Unity2RosVector3(), p.rotation.Unity2RosQuaternion());
            Transform q;
            p.position.Unity2Ros(out q.Translation);
            p.rotation.Unity2Ros(out q.Rotation);
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pose Unity2RosPose(this in UnityEngine.Pose p)
        {
            Pose q;
            p.position.Unity2Ros(out q.Position);
            p.rotation.Unity2Ros(out q.Orientation);
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unity2Ros(this in UnityEngine.Pose p, out Transform q)
        {
            p.position.Unity2Ros(out q.Translation);
            p.rotation.Unity2Ros(out q.Rotation);
        }

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
        public static bool IsInvalid(this in Msgs.GeometryMsgs.Vector3 v) =>
            v.X.IsInvalid() || v.Y.IsInvalid() || v.Z.IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in Point v) =>
            v.X.IsInvalid() || v.Y.IsInvalid() || v.Z.IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in Point32 v) =>
            v.X.IsInvalid() || v.Y.IsInvalid() || v.Z.IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsInvalid(this in Quaternion v) =>
            v.X.IsInvalid() || v.Y.IsInvalid() || v.Z.IsInvalid() || v.W.IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in UnityEngine.Quaternion v) =>
            v.x.IsInvalid() || v.y.IsInvalid() || v.z.IsInvalid() || v.w.IsInvalid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in Transform transform) =>
            IsInvalid(transform.Translation) || IsInvalid(transform.Rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this in Pose pose) => IsInvalid(pose.Position) || IsInvalid(pose.Orientation);

        public static BuilderPool.BuilderRent AppendBandwidth(
            this in BuilderPool.BuilderRent description, long bytesPerSecond)
        {
            AppendBandwidth((StringBuilder)description, bytesPerSecond);
            return description;
        }

        public static StringBuilder AppendBandwidth(this StringBuilder description, long bytesPerSecond)
        {
            switch (bytesPerSecond)
            {
                case < 1024:
                    string bPerSecond = bytesPerSecond.ToString("#,0", UnityUtils.Culture);
                    description.Append(bPerSecond).Append(" B");
                    break;
                case < 1024 * 1024:
                    string kbPerSecond = (bytesPerSecond / 1024.0).ToString("#,0.0", UnityUtils.Culture);
                    description.Append(kbPerSecond).Append(" kB");
                    break;
                default:
                    string mbPerSecond = (bytesPerSecond / 1024.0 / 1024.0).ToString("#,0.0", UnityUtils.Culture);
                    description.Append(mbPerSecond).Append(" MB");
                    break;
            }

            return description;
        }

        public enum PoseFormat
        {
            OnlyPosition,
            OnlyRotation,
            AllWithoutRoll,
            All
        }

        public static void FormatPose(in UnityEngine.Pose unityPose, StringBuilder description,
            PoseFormat format = PoseFormat.All, int positionPrecision = 3)
        {
            if (format != PoseFormat.OnlyRotation)
            {
                var (pX, pY, pZ) = unityPose.position.Unity2RosVector3();
                string positionFormat = positionPrecision switch
                {
                    3 => "#,0.###",
                    2 => "#,0.##",
                    1 => "#,0.#",
                    _ => "#,0.",
                };

                string pXStr = (pX == 0) ? "0" : pX.ToString(positionFormat, UnityUtils.Culture);
                string pYStr = (pY == 0) ? "0" : pY.ToString(positionFormat, UnityUtils.Culture);
                string pZStr = (pZ == 0) ? "0" : pZ.ToString(positionFormat, UnityUtils.Culture);

                description.Append(pXStr).Append(", ").Append(pYStr).Append(", ").Append(pZStr);
            }

            if (format == PoseFormat.OnlyPosition)
            {
                return;
            }

            if (format != PoseFormat.OnlyRotation)
            {
                description.AppendLine();
            }

            var (unityX, unityY, unityZ) = unityPose.rotation.eulerAngles;
            var (rXRaw, rYRaw, rZRaw) = (-unityZ, unityX, -unityY);

            if (format is PoseFormat.All or PoseFormat.OnlyRotation)
            {
                float rX = UnityUtils.RegularizeAngle(rXRaw);
                string rXStr = (rX == 0) ? "0" : rX.ToString("#,0.#", UnityUtils.Culture);

                description.Append("r: ").Append(rXStr).Append(", ");
            }

            float rY = UnityUtils.RegularizeAngle(rYRaw);
            float rZ = UnityUtils.RegularizeAngle(rZRaw);

            string rYStr = (rY == 0) ? "0" : rY.ToString("#,0.#", UnityUtils.Culture);
            string rZStr = (rZ == 0) ? "0" : rZ.ToString("#,0.#", UnityUtils.Culture);

            description.Append("p: ").Append(rYStr).Append(", y: ").Append(rZStr);
        }

        public static string GetCameraInfoTopic(string imageTopic)
        {
            int lastSlash = imageTopic.LastIndexOf('/');
            return lastSlash != -1 ? $"{imageTopic[..lastSlash]}/camera_info" : "/camera_info";
        }
    }
}