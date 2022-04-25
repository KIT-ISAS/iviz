#nullable enable

using System;
using UnityEngine;
using Iviz.Tools;

namespace Iviz.Core
{
    public static class MeshUtils
    {
        public static void SetVertices(this Mesh mesh, ReadOnlySpan<Vector3> ps)
        {
            var array = UnsafeUtils.CreateNativeArrayWrapper(ref ps.GetReference(), ps.Length);
            mesh.SetVertices(array);
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

        public static void SetIndices(this Mesh mesh, ReadOnlySpan<int> ps, MeshTopology topology, int subMesh)
        {
            var array = UnsafeUtils.CreateNativeArrayWrapper(ref ps.GetReference(), ps.Length);
            mesh.SetIndices(array, topology, subMesh);
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

        public static void SetUVs(this Mesh mesh, in ReadOnlySpan<Vector2> ps)
        {
            var array = UnsafeUtils.CreateNativeArrayWrapper(ref ps.GetReference(), ps.Length);
            mesh.SetUVs(0, array);
        }

        public static void SetUVs(this Mesh mesh, ReadOnlySpan<Vector3> ps, int channel = 0)
        {
            var array = UnsafeUtils.CreateNativeArrayWrapper(ref ps.GetReference(), ps.Length);
            mesh.SetUVs(channel, array);
        }

        public static void SetTriangles(this Mesh mesh, ReadOnlySpan<int> ps, int subMesh = 0)
        {
            mesh.SetIndices(ps, MeshTopology.Triangles, subMesh);
        }
    }
}