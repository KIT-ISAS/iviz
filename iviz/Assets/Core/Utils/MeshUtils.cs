#nullable enable

using System;
using Iviz.Tools;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Iviz.Core
{
    public static class MeshUtils
    {
        public static unsafe void SetVertices(this Mesh mesh, ReadOnlySpan<Vector3> span)
        {
            fixed (Vector3* ptr = span)
            {
                using var array =
                    NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3>(ptr, span.Length, Allocator.None);
                mesh.SetVertices(array);
            }
        }

        public static void SetVertices(this Mesh mesh, in Rent<Vector3> ps)
        {
            mesh.SetVertices(ps.Array, 0, ps.Length);
        }

        public static unsafe void SetNormals(this Mesh mesh, ReadOnlySpan<Vector3> ps)
        {
            fixed (Vector3* ptr = ps)
            {
                using var array =
                    NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3>(ptr, ps.Length, Allocator.None);
                mesh.SetNormals(array);
            }
        }

        public static void SetNormals(this Mesh mesh, in Rent<Vector3> ps)
        {
            mesh.SetNormals(ps.Array, 0, ps.Length);
        }

        public static unsafe void SetTangents(this Mesh mesh, ReadOnlySpan<Vector4> ps)
        {
            fixed (Vector4* ptr = ps)
            {
                using var array =
                    NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector4>(ptr, ps.Length, Allocator.None);
                mesh.SetTangents(array);
            }
        }
        
        public static void SetTangents(this Mesh mesh, in Rent<Vector4> ps)
        {
            mesh.SetTangents(ps.Array, 0, ps.Length);
        }
        
        public static unsafe void SetIndices(this Mesh mesh, ReadOnlySpan<int> ps, MeshTopology topology, int subMesh)
        {
            fixed (int* ptr = ps)
            {
                using var array =
                    NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(ptr, ps.Length, Allocator.None);
                mesh.SetIndices(array, topology, subMesh);
            }
        }        

        public static void SetIndices(this Mesh mesh, in Rent<int> ps, MeshTopology topology, int subMesh)
        {
            mesh.SetIndices(ps.Array, 0, ps.Length, topology, subMesh);
        }
        
        public static unsafe void SetColors(this Mesh mesh, ReadOnlySpan<Color> ps)
        {
            fixed (Color* ptr = ps)
            {
                using var array =
                    NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Color>(ptr, ps.Length, Allocator.None);
                mesh.SetColors(array);
            }
        }        

        public static void SetColors(this Mesh mesh, in Rent<Color> ps)
        {
            mesh.SetColors(ps.Array, 0, ps.Length);
        }

        public static unsafe void SetColors(this Mesh mesh, ReadOnlySpan<Color32> ps)
        {
            fixed (Color32* ptr = ps)
            {
                using var array =
                    NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Color32>(ptr, ps.Length, Allocator.None);
                mesh.SetColors(array);
            }
        }  
        
        public static void SetColors(this Mesh mesh, in Rent<Color32> ps)
        {
            mesh.SetColors(ps.Array, 0, ps.Length);
        }
        
        public static unsafe void SetUVs(this Mesh mesh, ReadOnlySpan<Vector2> ps)
        {
            fixed (Vector2* ptr = ps)
            {
                using var array =
                    NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector2>(ptr, ps.Length, Allocator.None);
                mesh.SetUVs(0, array);
            }
        }          

        public static void SetUVs(this Mesh mesh, in Rent<Vector2> ps)
        {
            mesh.SetUVs(0, ps.Array, 0, ps.Length);
        }

        public static unsafe void SetUVs(this Mesh mesh, ReadOnlySpan<Vector3> ps, int channel = 0)
        {
            fixed (Vector3* ptr = ps)
            {
                using var array =
                    NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3>(ptr, ps.Length, Allocator.None);
                mesh.SetUVs(channel, array);
            }
        }

        public static void SetUVs(this Mesh mesh, in Rent<Vector3> ps, int channel = 0)
        {
            mesh.SetUVs(channel, ps.Array, 0, ps.Length);
        }

        public static void SetTriangles(this Mesh mesh, ReadOnlySpan<int> ps, int subMesh = 0)
        {
            mesh.SetIndices(ps, MeshTopology.Triangles, subMesh);
        }         

        public static void SetTriangles(this Mesh mesh, in Rent<int> ps, int subMesh = 0)
        {
            mesh.SetTriangles(ps.Array, 0, ps.Length, subMesh);
        }
    }
}