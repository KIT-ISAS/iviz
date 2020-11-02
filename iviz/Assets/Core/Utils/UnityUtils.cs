using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Iviz.Msgs;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Core
{
    public static class UnityUtils
    {
        public static CultureInfo Culture { get; } = BuiltIns.Culture;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MagnitudeSq(this Vector3 v)
        {
            return v.x * v.x + v.y * v.y + v.z * v.z;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MagnitudeSq(this float3 v)
        {
            return v.x * v.x + v.y * v.y + v.z * v.z;
        }        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Magnitude(this Vector3 v)
        {
            return Mathf.Sqrt(v.MagnitudeSq());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(this Vector3 lhs, in Vector3 rhs)
        {
            return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);   
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalized(this Vector3 v)
        {
            return v / v.Magnitude();   
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Abs(this Vector3 p)
        {
            return new Vector3(Mathf.Abs(p.x), Mathf.Abs(p.y), Mathf.Abs(p.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 CwiseProduct(this Vector3 p, Vector3 o)
        {
            return Vector3.Scale(p, o);
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

        public static void SetPose([NotNull] this Transform t, in Pose p)
        {
            t.SetPositionAndRotation(p.position, p.rotation);
        }

        public static void SetParentLocal([NotNull] this Transform t, [CanBeNull] Transform parent)
        {
            t.SetParent(parent, false);
        }

        public static void SetLocalPose([NotNull] this Transform t, in Pose p)
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

        public static Pose Lerp([NotNull] this Transform p, in Pose o, float t)
        {
            return new Pose(
                Vector3.Lerp(p.position, o.position, t),
                Quaternion.Lerp(p.rotation, o.rotation, t)
            );
        }

        public static Pose LocalLerp([NotNull] this Transform p, in Pose o, float t)
        {
            return new Pose(
                Vector3.Lerp(p.localPosition, o.position, t),
                Quaternion.Lerp(p.localRotation, o.rotation, t)
            );
        }

        public static void ForEach<T>([NotNull] this IEnumerable<T> col, Action<T> action)
        {
            foreach (var item in col)
            {
                action(item);
            }
        }

        public static void ForEach<T>([NotNull] this T[] col, Action<T> action)
        {
            foreach (var t in col)
            {
                action(t);
            }
        }

        public static bool Any<T>([NotNull] this List<T> ts, Predicate<T> predicate)
        {
            foreach (var t in ts)
            {
                if (predicate(t))
                {
                    return true;
                }
            }

            return false;
        }

        public static ArraySegment<T> AsSegment<T>([NotNull] this T[] ts)
        {
            return new ArraySegment<T>(ts);
        }

        public static ArraySegment<T> AsSegment<T>([NotNull] this T[] ts, int offset)
        {
            return new ArraySegment<T>(ts, offset, ts.Length - offset);
        }        
        
        public static ArraySegment<T> AsSegment<T>([NotNull] this T[] ts, int offset, int count)
        {
            return new ArraySegment<T>(ts, offset, count);
        }        

        static MaterialPropertyBlock propBlock;
        static readonly int ColorPropId = Shader.PropertyToID("_Color");

        public static void SetPropertyColor([NotNull] this MeshRenderer meshRenderer, Color color, int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            if (propBlock == null)
            {
                propBlock = new MaterialPropertyBlock();
            }

            meshRenderer.GetPropertyBlock(propBlock, id);
            propBlock.SetColor(ColorPropId, color);
            meshRenderer.SetPropertyBlock(propBlock, id);
        }

        static readonly int EmissiveColorPropId = Shader.PropertyToID("_EmissiveColor");

        public static void SetPropertyEmissiveColor([NotNull] this MeshRenderer meshRenderer, Color color, int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            if (propBlock == null)
            {
                propBlock = new MaterialPropertyBlock();
            }

            meshRenderer.GetPropertyBlock(propBlock, id);
            propBlock.SetColor(EmissiveColorPropId, color);
            meshRenderer.SetPropertyBlock(propBlock, id);
        }

        static readonly int MainTexStPropId = Shader.PropertyToID("_MainTex_ST_");

        public static void SetPropertyMainTexST(
            [NotNull] this MeshRenderer meshRenderer, 
            in Vector2 xy, 
            in Vector2 wh,
            int id = 0)
        {
            if (meshRenderer == null)
            {
                throw new ArgumentNullException(nameof(meshRenderer));
            }

            if (propBlock == null)
            {
                propBlock = new MaterialPropertyBlock();
            }

            meshRenderer.GetPropertyBlock(propBlock, id);
            propBlock.SetVector(MainTexStPropId, new Vector4(wh.x, wh.y, xy.x, xy.y));
            meshRenderer.SetPropertyBlock(propBlock, id);
        }
        
        struct NativeArrayHelper<T> : IList<T>, IReadOnlyList<T> where T : struct
        {
            NativeArray<T> nArray;
            
            public NativeArrayHelper(in NativeArray<T> array) => nArray = array;
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => nArray.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => nArray.GetEnumerator();
            public NativeArray<T>.Enumerator GetEnumerator() => nArray.GetEnumerator();
            public void Add(T item) => throw new InvalidOperationException();
            public void Clear() => throw new InvalidOperationException();
            public bool Contains(T item) => nArray.Contains(item);
            public void CopyTo(T[] array, int arrayIndex) => array.CopyTo(array, arrayIndex);
            public bool Remove(T item) => throw new InvalidOperationException();
            public int Count => nArray.Length;
            public bool IsReadOnly => false;
            public int IndexOf(T item) => throw new InvalidOperationException();
            public void Insert(int index, T item) => throw new InvalidOperationException();
            public void RemoveAt(int index) => throw new InvalidOperationException();
            
            public T this[int index] {
                get => nArray[index];
                set => nArray[index] = value;
            }
        }        

        public static IList<T> AsList<T>(this NativeArray<T> array) where T : struct => 
            new NativeArrayHelper<T>(array);

        public static IReadOnlyList<T> AsReadOnlyList<T>(this NativeArray<T> array) where T : struct => 
            new NativeArrayHelper<T>(array);
    }
}