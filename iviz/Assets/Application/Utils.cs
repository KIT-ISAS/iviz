using UnityEngine;
using System.Runtime.InteropServices;
using System;
using RosSharp;

using System.Collections.Generic;
using Iviz.Msgs.std_msgs;
using Iviz.Msgs;

namespace Iviz.App
{
    [Serializable]
    public struct SerializableColor
    {
        public float r, g, b, a;

        public static implicit operator Color(SerializableColor i)
        {
            return new Color(i.r, i.g, i.b, i.a);
        }

        public static implicit operator SerializableColor(Color color)
        {
            return new SerializableColor { 
                r = color.r,
                g = color.g,
                b = color.g,
                a = color.a,
            };
        }
    }

    public static class Utils
    {

        public static Vector3 ToUnity(this Iviz.Msgs.geometry_msgs.Vector3 p)
        {
            return new Vector3((float)p.x, (float)p.y, (float)p.z);
        }

        public static Vector3 Ros2Unity(this Iviz.Msgs.geometry_msgs.Vector3 p)
        {
            return ToUnity(p).Ros2Unity();
        }

        public static Vector3 Abs(this Vector3 p)
        {
            return new Vector3(Mathf.Abs(p.x), Mathf.Abs(p.y), Mathf.Abs(p.z));
        }

        public static Vector3 CwiseProduct(this Vector3 p, Vector3 o)
        {
            return Vector3.Scale(p, o);
        }

        public static Iviz.Msgs.geometry_msgs.Vector3 ToRosVector3(this Vector3 p)
        {
            return new Iviz.Msgs.geometry_msgs.Vector3
            {
                x = p.x,
                y = p.y,
                z = p.z
            };
        }

        public static Iviz.Msgs.geometry_msgs.Vector3 Unity2RosVector3(this Vector3 p)
        {
            return ToRosVector3(p.Unity2Ros());
        }

        public static Vector3 ToUnity(this Iviz.Msgs.geometry_msgs.Point p)
        {
            return new Vector3((float)p.x, (float)p.y, (float)p.z);
        }

        public static Vector3 Ros2Unity(this Iviz.Msgs.geometry_msgs.Point p)
        {
            return ToUnity(p).Ros2Unity();
        }

        public static Color ToUnityColor(this ColorRGBA p)
        {
            return new Color(p.r, p.g, p.b, p.a);
        }

        public static Color32 ToUnityColor32(this ColorRGBA p)
        {
            return new Color32((byte)(p.r * 255), (byte)(p.g * 255), (byte)(p.b * 255), (byte)(p.a * 255));
        }

        public static ColorRGBA ToRos(this Color p)
        {
            return new ColorRGBA
            {
                r = p.r,
                g = p.g,
                b = p.b,
                a = p.a
            };
        }

        public static ColorRGBA ToRos(this Color32 p)
        {
            return new ColorRGBA
            {
                r = p.r / 255f,
                g = p.g / 255f,
                b = p.b / 255f,
                a = p.a / 255f
            };
        }

        public static Iviz.Msgs.geometry_msgs.Point ToRosPoint(this Vector3 p)
        {
            return new Iviz.Msgs.geometry_msgs.Point
            {
                x = p.x,
                y = p.y,
                z = p.z
            };
        }

        public static Iviz.Msgs.geometry_msgs.Point Unity2RosPoint(this Vector3 p)
        {
            return ToRosPoint(p.Unity2Ros());
        }

        public static Quaternion ToUnity(this Iviz.Msgs.geometry_msgs.Quaternion p)
        {
            return new Quaternion((float)p.x, (float)p.y, (float)p.z, (float)p.w);
        }

        public static Quaternion Ros2Unity(this Iviz.Msgs.geometry_msgs.Quaternion p)
        {
            return ToUnity(p).Ros2Unity();
        }

        public static Iviz.Msgs.geometry_msgs.Quaternion ToRos(this Quaternion p)
        {
            return new Iviz.Msgs.geometry_msgs.Quaternion
            {
                x = p.x,
                y = p.y,
                z = p.z,
                w = p.w
            };
        }

        public static Iviz.Msgs.geometry_msgs.Quaternion Unity2RosQuaternion(this Quaternion p)
        {
            return ToRos(p.Unity2Ros());
        }

        public static Pose ToUnity(this Iviz.Msgs.geometry_msgs.Transform pose)
        {
            return new Pose(pose.translation.ToUnity(), pose.rotation.ToUnity());
        }

        public static Pose Ros2Unity(this Iviz.Msgs.geometry_msgs.Transform pose)
        {
            return new Pose(pose.translation.Ros2Unity(), pose.rotation.Ros2Unity());
        }

        public static Pose ToUnity(this Iviz.Msgs.geometry_msgs.Pose pose)
        {
            return new Pose(pose.position.ToUnity(), pose.orientation.ToUnity());
        }

        public static Pose Ros2Unity(this Iviz.Msgs.geometry_msgs.Pose pose)
        {
            return new Pose(pose.position.Ros2Unity(), pose.orientation.Ros2Unity());
        }

        public static Iviz.Msgs.geometry_msgs.Transform ToRosTransform(this Pose p)
        {
            return new Iviz.Msgs.geometry_msgs.Transform
            {
                translation = p.position.ToRosVector3(),
                rotation = p.rotation.ToRos()
            };
        }

        public static Iviz.Msgs.geometry_msgs.Transform Unity2RosTransform(this Pose p)
        {
            return new Iviz.Msgs.geometry_msgs.Transform
            {
                translation = p.position.Unity2RosVector3(),
                rotation = p.rotation.Unity2RosQuaternion()
            };
        }

        public static Iviz.Msgs.geometry_msgs.Pose ToRosPose(this Pose p)
        {
            return new Iviz.Msgs.geometry_msgs.Pose
            {
                position = p.position.ToRosPoint(),
                orientation = p.rotation.ToRos()
            };
        }

        public static Iviz.Msgs.geometry_msgs.Pose Unity2RosPose(this Pose p)
        {
            return new Iviz.Msgs.geometry_msgs.Pose
            {
                position = p.position.Unity2RosPoint(),
                orientation = p.rotation.Unity2RosQuaternion()
            };
        }

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

        public static void SetPose(this Transform t, in Pose p)
        {
            t.position = p.position;
            t.rotation = p.rotation;
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
            Pose q = new Pose();
            q.rotation = Quaternion.Inverse(p.rotation);
            q.position = -(q.rotation * p.position);
            return q;
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

        public static void FromByteArray<T>(byte[] source, ref T[] destination) where T : struct
        {
            int size = source.Length / Marshal.SizeOf(typeof(T));
            if (destination == null || destination.Length != size)
            {
                //Debug.Log("Utils.FromByteArray: Forcing array resize! Expected " + size + ", got " +
                //    (destination == null ? 0 : destination.Length));
                destination = new T[size];
            }
            GCHandle handle = GCHandle.Alloc(destination, GCHandleType.Pinned);
            try
            {
                IntPtr pointer = handle.AddrOfPinnedObject();
                Marshal.Copy(source, 0, pointer, source.Length);
            }
            finally
            {
                if (handle.IsAllocated)
                    handle.Free();
            }
        }

        public static void ToByteArray<T>(T[] source, ref byte[] destination) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(source, GCHandleType.Pinned);
            try
            {
                IntPtr pointer = handle.AddrOfPinnedObject();
                int size = source.Length * Marshal.SizeOf(typeof(T));
                if (destination == null || destination.Length != size)
                {
                    destination = new byte[size];
                }
                Marshal.Copy(pointer, destination, 0, destination.Length);
            }
            finally
            {
                if (handle.IsAllocated)
                    handle.Free();
            }
        }

        public static time GetRosTime()
        {
            /*
            long timeMs = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            return new time
            {
                secs = (uint)(timeMs / 1000),
                nsecs = (uint)(timeMs % 1000) * 1000000
            };
            */
            return new time(DateTime.Now);
        }

        public static TimeSpan ToTimeSpan(this duration duration)
        {
            return TimeSpan.FromSeconds(duration.secs) + TimeSpan.FromTicks(duration.nsecs / 100);
        }

        public static bool IsZero(this duration duration)
        {
            return duration.secs == 0 && duration.nsecs == 0;
        }

        public static Header CreateHeader(uint seq = 0, string frame_id = "")
        {
            return new Header
            {
                seq = seq,
                frame_id = frame_id,
                stamp = GetRosTime()
            };
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
            for (int i = 0; i < col.Length; i++)
            {
                action(col[i]);
            }
        }

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
        public static void SetPropertyMainTexST(this MeshRenderer meshRenderer, Vector2 xy, Vector2 wh, int id = 0)
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
