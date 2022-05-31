#nullable enable

using System;
using UnityEngine;
using Iviz.Tools;
using Unity.Collections;
using UnityEngine.Rendering;

namespace Iviz.Core
{
    public static class MeshUtils
    {
        public static void SetVertices(this Mesh mesh, ReadOnlySpan<Vector3> ps)
        {
            var array = UnsafeUtils.CreateNativeArrayWrapper(ref ps.GetReference(), ps.Length);
            mesh.SetVertices(array);
        }

        public static void SetVertices(this Mesh mesh, Rent<Vector3> ps)
        {
            mesh.SetVertices(ps.Array, 0, ps.Length);
        }

        public static void SetNormals(this Mesh mesh, ReadOnlySpan<Vector3> ps)
        {
            var array = UnsafeUtils.CreateNativeArrayWrapper(ref ps.GetReference(), ps.Length);
            mesh.SetNormals(array);
        }

        public static void SetTangents(this Mesh mesh, ReadOnlySpan<Vector4> ps)
        {
            var array = UnsafeUtils.CreateNativeArrayWrapper(ref ps.GetReference(), ps.Length);
            mesh.SetTangents(array);
        }

        public static void SetColors(this Mesh mesh, ReadOnlySpan<Color> ps)
        {
            var array = UnsafeUtils.CreateNativeArrayWrapper(ref ps.GetReference(), ps.Length);
            mesh.SetColors(array);
        }

        public static void SetColors(this Mesh mesh, ReadOnlySpan<Color32> ps)
        {
            var array = UnsafeUtils.CreateNativeArrayWrapper(ref ps.GetReference(), ps.Length);
            mesh.SetColors(array);
        }

        public static void SetColors(this Mesh mesh, Rent<Color32> ps)
        {
            mesh.SetColors(ps.Array, 0, ps.Length);
        }

        public static void SetUVs(this Mesh mesh, Rent<Vector2> ps)
        {
            mesh.SetUVs(0, ps.Array, 0, ps.Length);
        }

        public static void SetUVs(this Mesh mesh, ReadOnlySpan<Vector3> ps, int channel = 0)
        {
            var array = UnsafeUtils.CreateNativeArrayWrapper(ref ps.GetReference(), ps.Length);
            mesh.SetUVs(channel, array);
        }

        public static void SetTriangles(this Mesh mesh, ReadOnlySpan<int> ps)
        {
            var array = UnsafeUtils.CreateNativeArrayWrapper(ref ps.GetReference(), ps.Length);
            mesh.indexFormat = ps.Length < ushort.MaxValue
                ? IndexFormat.UInt16
                : IndexFormat.UInt32;
            mesh.SetIndices(array, MeshTopology.Triangles, 0);
        }

        public static void SetTriangles(this Mesh mesh, Rent<int> ps)
        {
            mesh.SetIndices(ps, MeshTopology.Triangles);
        }
        
        public static void SetIndices(this Mesh mesh, Rent<int> ps, MeshTopology topology)
        {
            mesh.indexFormat = ps.Length < ushort.MaxValue
                ? IndexFormat.UInt16
                : IndexFormat.UInt32;
            mesh.SetIndices(ps.Array, 0, ps.Length, topology, 0);
        }
        
        public static void SetTriangles(this Mesh mesh, NativeArray<int> ps)
        {
            mesh.SetIndices(ps, MeshTopology.Triangles, 0);
        }
    }
}