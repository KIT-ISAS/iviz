using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;
using System.Runtime.Serialization;
using System.Globalization;
using Iviz.App.Listeners;
using Unity.Mathematics;

namespace Iviz
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
        //----
        public static Vector3 Unity2Ros(this Vector3 vector3) => new Vector3(vector3.z, -vector3.x, vector3.y);

        public static Vector3 Ros2Unity(this Vector3 vector3) => new Vector3(-vector3.y, vector3.z, vector3.x);

        public static Quaternion Ros2Unity(this Quaternion quaternion) =>
            new Quaternion(quaternion.y, -quaternion.z, -quaternion.x, quaternion.w);

        public static Quaternion Unity2Ros(this Quaternion quaternion) =>
            new Quaternion(-quaternion.z, quaternion.x, -quaternion.y, quaternion.w);
        //----

        public static float4 Ros2Unity(this float4 v) => new float4(-v.y, v.z, v.x, v.w);

        public static Quaternion RosRpy2Unity(this Vector3 v) =>
            Quaternion.Euler(v.y * Mathf.Rad2Deg, -v.z * Mathf.Rad2Deg, -v.x * Mathf.Rad2Deg);

        static Vector3 ToUnity(this Msgs.GeometryMsgs.Vector3 p)
        {
            return new Vector3((float) p.X, (float) p.Y, (float) p.Z);
        }

        static Vector3 ToUnity(this Msgs.GeometryMsgs.Point32 p)
        {
            return new Vector3(p.X, p.Y, p.Z);
        }

        public static Vector3 Ros2Unity(this Msgs.GeometryMsgs.Vector3 p)
        {
            return p.ToUnity().Ros2Unity();
        }

        public static Vector3 Ros2Unity(this Msgs.GeometryMsgs.Point32 p)
        {
            return p.ToUnity().Ros2Unity();
        }

        public static Vector3 Abs(this Vector3 p)
        {
            return new Vector3(Mathf.Abs(p.x), Mathf.Abs(p.y), Mathf.Abs(p.z));
        }

        public static Vector3 CwiseProduct(this Vector3 p, Vector3 o)
        {
            return Vector3.Scale(p, o);
        }

        static Msgs.GeometryMsgs.Vector3 ToRosVector3(this Vector3 p)
        {
            return new Msgs.GeometryMsgs.Vector3
            (
                X: p.x,
                Y: p.y,
                Z: p.z
            );
        }

        public static Msgs.GeometryMsgs.Vector3 Unity2RosVector3(this Vector3 p)
        {
            return ToRosVector3(p.Unity2Ros());
        }

        static Vector3 ToUnity(this Msgs.GeometryMsgs.Point p)
        {
            return new Vector3((float) p.X, (float) p.Y, (float) p.Z);
        }

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

        public static Color32 ToUnityColor32(this ColorRGBA p)
        {
            return p.ToUnityColor();
        }

        public static ColorRGBA ToRos(this Color p)
        {
            return new ColorRGBA
            (
                R: p.r,
                G: p.g,
                B: p.b,
                A: p.a
            );
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

        static Msgs.GeometryMsgs.Point ToRosPoint(this Vector3 p)
        {
            return new Msgs.GeometryMsgs.Point
            (
                X: p.x,
                Y: p.y,
                Z: p.z
            );
        }

        public static Msgs.GeometryMsgs.Point Unity2RosPoint(this Vector3 p)
        {
            return ToRosPoint(p.Unity2Ros());
        }

        static Quaternion ToUnity(this Msgs.GeometryMsgs.Quaternion p)
        {
            return new Quaternion((float) p.X, (float) p.Y, (float) p.Z, (float) p.W);
        }

        public static Quaternion Ros2Unity(this Msgs.GeometryMsgs.Quaternion p)
        {
            return p.ToUnity().Ros2Unity();
        }

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

        public static Msgs.GeometryMsgs.Quaternion Unity2RosQuaternion(this Quaternion p)
        {
            return ToRos(p.Unity2Ros());
        }

        static Pose ToUnity(this Msgs.GeometryMsgs.Transform pose)
        {
            return new Pose(pose.Translation.ToUnity(), pose.Rotation.ToUnity());
        }

        public static Pose Ros2Unity(this Msgs.GeometryMsgs.Transform pose)
        {
            return new Pose(pose.Translation.Ros2Unity(), pose.Rotation.Ros2Unity());
        }

        static Pose ToUnity(this Msgs.GeometryMsgs.Pose pose)
        {
            return new Pose(pose.Position.ToUnity(), pose.Orientation.ToUnity());
        }

        public static Pose Ros2Unity(this Msgs.GeometryMsgs.Pose pose)
        {
            return new Pose(pose.Position.Ros2Unity(), pose.Orientation.Ros2Unity());
        }

        /*
        static Msgs.GeometryMsgs.Transform ToRosTransform(this Pose p)
        {
            return new Msgs.GeometryMsgs.Transform
            (
                Translation: p.position.ToRosVector3(),
                Rotation: p.rotation.ToRos()
            );
        }
        */

        public static Msgs.GeometryMsgs.Transform Unity2RosTransform(this Pose p)
        {
            return new Msgs.GeometryMsgs.Transform
            (
                Translation: p.position.Unity2RosVector3(),
                Rotation: p.rotation.Unity2RosQuaternion()
            );
        }

        /*
        static Msgs.GeometryMsgs.Pose ToRosPose(this Pose p)
        {
            return new Msgs.GeometryMsgs.Pose
            (
                Position: p.position.ToRosPoint(),
                Orientation: p.rotation.ToRos()
            );
        }
        */

        public static Msgs.GeometryMsgs.Pose Unity2RosPose(this Pose p)
        {
            return new Msgs.GeometryMsgs.Pose
            (
                Position: p.position.Unity2RosPoint(),
                Orientation: p.rotation.Unity2RosQuaternion()
            );
        }

        public static TimeSpan ToTimeSpan(this duration duration)
        {
            return TimeSpan.FromSeconds(duration.Secs) + TimeSpan.FromTicks(duration.Nsecs / 100);
        }

        public static Header CreateHeader(uint seq = 0, string frameId = null)
        {
            return new Header
            (
                Seq: seq,
                FrameId: frameId ?? TFListener.BaseFrameId,
                Stamp: new time(DateTime.Now)
            );
        }

        public static bool HasNaN(this float4 v) => float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);

        public static bool HasNaN(this Vector3 v) => float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);

        public static bool HasNaN(this Msgs.GeometryMsgs.Vector3 v)
        {
            return double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z);
        }

        public static bool HasNaN(this Msgs.GeometryMsgs.Point v)
        {
            return double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z);
        }

        public static bool HasNaN(this Msgs.GeometryMsgs.Point32 v)
        {
            return float.IsNaN(v.X) || float.IsNaN(v.Y) || float.IsNaN(v.Z);
        }

        public static bool HasNaN(this Msgs.GeometryMsgs.Quaternion v)
        {
            return double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z) || double.IsNaN(v.W);
        }

        public static bool HasNaN(this Msgs.GeometryMsgs.Transform transform)
        {
            return HasNaN(transform.Rotation) || HasNaN(transform.Translation);
        }

        public static bool HasNaN(this Msgs.GeometryMsgs.Pose pose)
        {
            return HasNaN(pose.Orientation) || HasNaN(pose.Position);
        }
    }

    public static class UnityUtils
    {
        public static CultureInfo Culture { get; } = BuiltIns.Culture;

        public static bool TryParse(string s, out float f)
        {
            if (float.TryParse(s, NumberStyles.Any, Culture, out f))
            {
                return true;
            }

            f = 0;
            return false;
        }

        /*
        public static string SanitizedText(string text, int size)
        {
            if (text.Length <= size + 5)
            {
                return text;
            }
            else
            {
                return text.Substring(0, size) + "...\n→ " + text.Substring(size);
            }
        }
        */

        public static Pose AsPose(this Transform t)
        {
            return new Pose(t.position, t.rotation);
        }

        public static Pose AsLocalPose(this Transform t)
        {
            return new Pose(t.localPosition, t.localRotation);
        }

        public static Vector3 Multiply(this Pose p, in Vector3 v)
        {
            return p.rotation * v + p.position;
        }

        public static Pose Multiply(this Pose p, in Pose o)
        {
            return new Pose
            (
                rotation: p.rotation * o.rotation,
                position: p.rotation * o.position + p.position
            );
        }

        public static void SetPose(this Transform t, in Pose p)
        {
            t.SetPositionAndRotation(p.position, p.rotation);
        }

        public static void SetParentLocal(this Transform t, Transform parent)
        {
            t.SetParent(parent, false);
        }

        public static void SetLocalPose(this Transform t, in Pose p)
        {
            t.localPosition = p.position;
            t.localRotation = p.rotation;
        }


        public static Pose Inverse(this Pose p)
        {
            Quaternion q = Quaternion.Inverse(p.rotation);
            return new Pose(
                rotation: q,
                position: q * -p.position
            );
        }

        public static Pose Lerp(this Pose p, in Pose o, float t)
        {
            return new Pose(
                Vector3.Lerp(p.position, o.position, t),
                Quaternion.Lerp(p.rotation, o.rotation, t)
            );
        }

        public static Pose Lerp(this Transform p, in Pose o, float t)
        {
            return new Pose(
                Vector3.Lerp(p.position, o.position, t),
                Quaternion.Lerp(p.rotation, o.rotation, t)
            );
        }

        public static Pose LocalLerp(this Transform p, in Pose o, float t)
        {
            return new Pose(
                Vector3.Lerp(p.localPosition, o.position, t),
                Quaternion.Lerp(p.localRotation, o.rotation, t)
            );
        }

        public static void ForEach<T>(this IEnumerable<T> col, Action<T> action)
        {
            foreach (var item in col)
            {
                action(item);
            }
        }

        public static void ForEach<T>(this T[] col, Action<T> action)
        {
            foreach (var t in col)
            {
                action(t);
            }
        }

        public static void ForEach<T>(this IList<T> col, Action<T> action)
        {
            foreach (var t in col)
            {
                action(t);
            }
        }

        /*
        public static Bounds TransformBound(Bounds b, Transform T)
        {
            return TransformBound(b, T.rotation, T.position);
        }

        public static Bounds TransformBound(Bounds b, Quaternion q, Vector3 t)
        {
            Vector3[] R = {
                 b.extents.x * Vector3.right,
                -b.extents.x * Vector3.right,
                 b.extents.y * Vector3.up,
                -b.extents.y * Vector3.up,
                 b.extents.z * Vector3.forward,
                -b.extents.z * Vector3.forward
            };
            Vector3 positionMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 positionMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            for (int i = 0; i < R.Length; i++)
            {
                Vector3 position = q * (R[i] + b.center) + t;
                positionMin = Vector3.Min(positionMin, position);
                positionMax = Vector3.Max(positionMax, position);
            }
            return new Bounds((positionMax + positionMin) / 2, positionMax - positionMin);
        }
        */

        static MaterialPropertyBlock propBlock;
        static readonly int ColorPropId = Shader.PropertyToID("_Color");

        public static void SetPropertyColor(this MeshRenderer meshRenderer, Color color, int id = 0)
        {
            if (propBlock == null)
            {
                propBlock = new MaterialPropertyBlock();
            }

            meshRenderer.GetPropertyBlock(propBlock, id);
            propBlock.SetColor(ColorPropId, color);
            meshRenderer.SetPropertyBlock(propBlock, id);
        }

        static readonly int EmissiveColorPropId = Shader.PropertyToID("_EmissiveColor");

        public static void SetPropertyEmissiveColor(this MeshRenderer meshRenderer, Color color, int id = 0)
        {
            if (propBlock == null)
            {
                propBlock = new MaterialPropertyBlock();
            }

            meshRenderer.GetPropertyBlock(propBlock, id);
            propBlock.SetColor(EmissiveColorPropId, color);
            meshRenderer.SetPropertyBlock(propBlock, id);
        }

        static readonly int MainTexSTPropId = Shader.PropertyToID("_MainTex_ST_");

        public static void SetPropertyMainTexST(this MeshRenderer meshRenderer, in Vector2 xy, in Vector2 wh,
            int id = 0)
        {
            if (propBlock == null)
            {
                propBlock = new MaterialPropertyBlock();
            }

            meshRenderer.GetPropertyBlock(propBlock, id);
            propBlock.SetVector(MainTexSTPropId, new Vector4(wh.x, wh.y, xy.x, xy.y));
            meshRenderer.SetPropertyBlock(propBlock, id);
        }
    }
}