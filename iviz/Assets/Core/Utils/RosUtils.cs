#nullable enable

using System.Runtime.CompilerServices;
using System.Text;
using Codice.Client.BaseCommands;
using Iviz.Msgs;
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
            Vector3 q;
            q.x = -v.y;
            q.y = v.z;
            q.z = v.x;
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion Ros2Unity(this in UnityEngine.Quaternion quaternion) =>
            new(quaternion.y, -quaternion.z, -quaternion.x, quaternion.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 Ros2Unity(this in float4 v)
        {
            //new(-v.y, v.z, v.x, v.w);  
            float4 w;
            w.x = -v.y;
            w.y = v.z;
            w.z = v.x;
            w.w = v.w;
            return w;
        } 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion RosRpy2Unity(this in Vector3 v) =>
            UnityEngine.Quaternion.Euler(v.Ros2Unity() * -Mathf.Rad2Deg);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Quaternion RosRpy2Unity(this in Msgs.GeometryMsgs.Vector3 v) =>
            v.ToUnity().RosRpy2Unity();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Vector3f p) => new(-p.Y, p.Z, p.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToUnity(this in Msgs.GeometryMsgs.Vector3 p) => new((float)p.X, (float)p.Y, (float)p.Z);

        public static Msgs.GeometryMsgs.Vector3 ToRosVector3(this in Vector3 p) => new(p.x, p.y, p.z);

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
        public static Msgs.GeometryMsgs.Vector3 Unity2RosVector3(this in Vector3 p) => new(p.z, -p.x, p.y);

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ros2Unity(this in Point p)
        {
            //return new((float)-p.Y, (float)p.Z, (float)p.X);
            Vector3 f;
            f.x = (float)-p.Y;
            f.y = (float)p.Z;
            f.z = (float)p.X;
            return f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Ros2Unity(this in Point p, ref float4 f)
        {
            //(f.x, f.y, f.z) = ((float)-p.Y, (float)p.Z, (float)p.X);
            f.x = (float)-p.Y;
            f.y = (float)p.Z;
            f.z = (float)p.X;
        }

        public static void Ros2Unity(this in Point p, out Vector3 v)
        {
            v.x = (float)-p.Y;
            v.y = (float)p.Z;
            v.z = (float)p.X;
        }

        static void Ros2Unity(this in Msgs.GeometryMsgs.Vector3 p, out Vector3 v)
        {
            v.x = (float)-p.Y;
            v.y = (float)p.Z;
            v.z = (float)p.X;
        }        
        
        public static Color ToUnityColor(this in ColorRGBA p)
        {
            Color c;
            c.r = p.R;
            c.g = p.G;
            c.b = p.B;
            c.a = p.A;
            return c;
        }

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
        public static UnityEngine.Quaternion Ros2Unity(this in Quaternion p)
        {
            // (p.X == 0 && p.Y == 0 && p.Z == 0 && p.W == 0) ? UnityEngine.Quaternion.identity : p.ToUnity().Ros2Unity();
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
                q.x = (float)p.X;
                q.y = (float)p.Y;
                q.z = (float)p.Z;
                q.w = (float)p.W;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Quaternion Unity2RosQuaternion(this in UnityEngine.Quaternion p) =>
            new(-p.z, p.x, -p.y, p.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Pose Ros2Unity(this in Transform pose)
        {
            //new(pose.Translation.Ros2Unity(), pose.Rotation.Ros2Unity());
            UnityEngine.Pose q;
            pose.Translation.Ros2Unity(out q.position);
            pose.Rotation.Ros2Unity(out q.rotation);
            return q;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Pose Ros2Unity(this in Pose pose)
        {
            //new(pose.Position.Ros2Unity(), pose.Orientation.Ros2Unity());
            UnityEngine.Pose q;
            pose.Position.Ros2Unity(out q.position);
            pose.Orientation.Ros2Unity(out q.rotation);
            return q;
        }
        
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

        public static void WriteFormattedBandwidth(StringBuilder description, long bytesPerSecond)
        {
            switch (bytesPerSecond)
            {
                case < 1024:
                    string bPerSecond = bytesPerSecond.ToString("#,0", UnityUtils.Culture);
                    description.Append(bPerSecond).Append(" B/s");
                    break;
                case < 1024 * 1024:
                    string kbPerSecond = (bytesPerSecond * (1f / 1024)).ToString("#,0.0", UnityUtils.Culture);
                    description.Append(kbPerSecond).Append(" kB/s");
                    break;
                default:
                    string mbPerSecond = (bytesPerSecond * (1f / 1024 / 1024)).ToString("#,0.0", UnityUtils.Culture);
                    description.Append(mbPerSecond).Append(" MB/s");
                    break;
            }
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