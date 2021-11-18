#nullable enable

using Iviz.Tools;
using UnityEngine;

namespace Iviz.Core
{
    public static class MeshUtils
    {
        public static void SetVertices(this Mesh mesh, in Rent<Vector3> ps)
        {
            mesh.SetVertices(ps.Array, 0, ps.Length);
        }

        public static void SetNormals(this Mesh mesh, in Rent<Vector3> ps)
        {
            mesh.SetNormals(ps.Array, 0, ps.Length);
        }

        public static void SetTangents(this Mesh mesh, in Rent<Vector4> ps)
        {
            mesh.SetTangents(ps.Array, 0, ps.Length);
        }

        public static void SetIndices(this Mesh mesh, in Rent<int> ps, MeshTopology topology, int subMesh)
        {
            mesh.SetIndices(ps.Array, 0, ps.Length, topology, subMesh);
        }

        public static void SetColors(this Mesh mesh, in Rent<Color> ps)
        {
            mesh.SetColors(ps.Array, 0, ps.Length);
        }

        public static void SetColors(this Mesh mesh, in Rent<Color32> ps)
        {
            mesh.SetColors(ps.Array, 0, ps.Length);
        }

        public static void SetUVs(this Mesh mesh, in Rent<Vector2> ps)
        {
            mesh.SetUVs(0, ps.Array, 0, ps.Length);
        }

        public static void SetUVs(this Mesh mesh, in Rent<Vector3> ps)
        {
            mesh.SetUVs(0, ps.Array, 0, ps.Length);
        }

        public static void SetUVs(this Mesh mesh, int channel, in Rent<Vector3> ps)
        {
            mesh.SetUVs(channel, ps.Array, 0, ps.Length);
        }

        public static void SetTriangles(this Mesh mesh, in Rent<int> ps, int subMesh = 0)
        {
            mesh.SetTriangles(ps.Array, 0, ps.Length, subMesh);
        }
    }
}