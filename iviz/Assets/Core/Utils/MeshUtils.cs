#nullable enable

using System;
using UnityEngine;
using Iviz.Tools;
using Unity.Collections;
using UnityEngine.Rendering;

namespace Iviz.Core
{
    public static unsafe class MeshUtils
    {
        public static void SetVertices(this Mesh mesh, ReadOnlySpan<Vector3> ps)
        {
            fixed (Vector3* psPtr = &ps[0])
            {
                mesh.SetVertices(NativeArrayUtils.CreateWrapper(psPtr, ps.Length));
            }
        }

        public static void SetVertices(this Mesh mesh, Rent<Vector3> ps)
        {
            mesh.SetVertices(ps.Array, 0, ps.Length);
        }

        public static void SetNormals(this Mesh mesh, ReadOnlySpan<Vector3> ps)
        {
            fixed (Vector3* psPtr = &ps[0])
            {
                mesh.SetNormals(NativeArrayUtils.CreateWrapper(psPtr, ps.Length));
            }
        }

        public static void SetTangents(this Mesh mesh, ReadOnlySpan<Vector4> ps)
        {
            fixed (Vector4* psPtr = &ps[0])
            {
                mesh.SetTangents(NativeArrayUtils.CreateWrapper(psPtr, ps.Length));
            }
        }

        public static void SetColors(this Mesh mesh, ReadOnlySpan<Color> ps)
        {
            fixed (Color* psPtr = &ps[0])
            {
                mesh.SetColors(NativeArrayUtils.CreateWrapper(psPtr, ps.Length));
            }
        }

        public static void SetColors(this Mesh mesh, ReadOnlySpan<Color32> ps)
        {
            fixed (Color32* psPtr = &ps[0])
            {
                mesh.SetColors(NativeArrayUtils.CreateWrapper(psPtr, ps.Length));
            }
        }

        public static void SetColors(this Mesh mesh, Rent<Color32> ps)
        {
            mesh.SetColors(ps.Array, 0, ps.Length);
        }

        public static void SetUVs(this Mesh mesh, Rent<Vector2> ps)
        {
            mesh.SetUVs(0, ps.Array, 0, ps.Length);
        }

        public static void SetUVs(this Mesh mesh, NativeArray<Vector2> ps)
        {
            mesh.SetUVs(0, ps, 0, ps.Length);
        }

        public static void SetUVs(this Mesh mesh, ReadOnlySpan<Vector3> ps, int channel = 0)
        {
            fixed (Vector3* psPtr = &ps[0])
            {
                mesh.SetUVs(channel, NativeArrayUtils.CreateWrapper(psPtr, ps.Length));
            }
        }

        public static void SetTriangles(this Mesh mesh, ReadOnlySpan<int> ps)
        {
            mesh.indexFormat = ps.Length < ushort.MaxValue
                ? IndexFormat.UInt16
                : IndexFormat.UInt32;
            fixed (int* psPtr = &ps[0])
            {
                mesh.SetIndices(NativeArrayUtils.CreateWrapper(psPtr, ps.Length), MeshTopology.Triangles, 0);
            }
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
            mesh.indexFormat = ps.Length < ushort.MaxValue
                ? IndexFormat.UInt16
                : IndexFormat.UInt32;
            mesh.SetIndices(ps, MeshTopology.Triangles, 0);
        }
    }
}