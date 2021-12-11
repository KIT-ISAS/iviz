#nullable enable

using System;
using Iviz.Core;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Resource that constructs a mesh from vertices, triangles, normals, etc.
    /// </summary>
    public sealed class MeshTrianglesResource : MeshMarkerResource
    {
        const int MaxVerticesShort = 65000;

        public bool FlipWinding { get; set; }

        Mesh? mesh;

        public override Bounds? Bounds =>
            mesh != null && mesh.vertexCount != 0 ? new Bounds(Collider.center, Collider.size) : null;

        public string MeshName
        {
            set
            {
                if (mesh != null)
                {
                    mesh.name = value;
                }
            }
        }

        Mesh EnsureOwnMesh(int numPointsNeeded)
        {
            var indexFormat = numPointsNeeded >= MaxVerticesShort
                ? UnityEngine.Rendering.IndexFormat.UInt32
                : UnityEngine.Rendering.IndexFormat.UInt16;

            if (mesh != null)
            {
                if (mesh.indexFormat != indexFormat)
                {
                    mesh.indexFormat = indexFormat;
                }

                return mesh;
            }

            var tmpMesh = new Mesh
            {
                indexFormat = indexFormat,
                name = "MeshTrianglesResource Mesh"
            };


            mesh = tmpMesh;
            GetComponent<MeshFilter>().sharedMesh = tmpMesh;
            return tmpMesh;
        }

        public void Clear()
        {
            EnsureOwnMesh(0).Clear();
        }

        public void Set(in Rent<Vector3> points, ReadOnlySpan<Color> colors = default)
        {
            if (points.Length % 3 != 0)
            {
                throw new ArgumentException($"Invalid triangle list {points.Length}", nameof(points));
            }

            if (colors.Length != 0 && colors.Length != points.Length)
            {
                throw new ArgumentException("Inconsistent color size!", nameof(colors));
            }

            var ownMesh = EnsureOwnMesh(points.Length);

            ownMesh.Clear();
            ownMesh.SetVertices(points);
            if (colors.Length != 0)
            {
                ownMesh.SetColors(colors);
            }

            using (var triangles = new Rent<int>(points.Length))
            {
                int[] array = triangles.Array;
                if (FlipWinding)
                {
                    for (int i = 0; i < triangles.Length; i += 3)
                    {
                        array[i] = i;
                        array[i + 1] = i + 2;
                        array[i + 2] = i + 1;
                    }
                }
                else
                {
                    for (int i = 0; i < triangles.Length; i++)
                    {
                        array[i] = i;
                    }
                }

                ownMesh.SetTriangles(triangles);
            }

            ownMesh.RecalculateNormals();

            Collider.center = ownMesh.bounds.center;
            Collider.size = ownMesh.bounds.size;
        }
        
        
        /*
        public void Set(
            in Rent<Vector3> points,
            in Rent<Vector3> normals,
            in Rent<Vector4> tangents,
            in Rent<Vector3> diffuseTexCoords,
            in Rent<Vector3> bumpTexCoords,
            in Rent<int> triangles,
            in Rent<Color32> colors = default)
            */
        public void Set(
            ReadOnlySpan<Vector3> points,
            ReadOnlySpan<Vector3> normals,
            ReadOnlySpan<Vector4> tangents,
            ReadOnlySpan<Vector3> diffuseTexCoords,
            ReadOnlySpan<Vector3> bumpTexCoords,
            ReadOnlySpan<int> triangles,
            ReadOnlySpan<Color32> colors = default)
        {
            if (triangles.Length % 3 != 0)
            {
                throw new ArgumentException($"Invalid triangle list {points.Length}", nameof(points));
            }

            if (normals.Length != 0 && normals.Length != points.Length)
            {
                throw new ArgumentException("Inconsistent normals size!", nameof(normals));
            }

            if (colors.Length != 0 && colors.Length != points.Length)
            {
                throw new ArgumentException("Inconsistent color size!", nameof(colors));
            }

            var ownMesh = EnsureOwnMesh(points.Length);

            ownMesh.Clear();
            ownMesh.SetVertices(points);
            if (normals.Length != 0)
            {
                ownMesh.SetNormals(normals);
            }

            if (diffuseTexCoords.Length != 0)
            {
                ownMesh.SetUVs(diffuseTexCoords);
            }

            if (bumpTexCoords.Length != 0)
            {
                ownMesh.SetUVs(bumpTexCoords, 1);
            }

            if (colors.Length != 0)
            {
                ownMesh.SetColors(colors);
            }

            ownMesh.SetTriangles(triangles);


            if (normals.Length == 0)
            {
                ownMesh.RecalculateNormals();
            }


            if (tangents.Length != 0)
            {
                ownMesh.SetTangents(tangents);
            }
            else
            {
                ownMesh.RecalculateTangents();
            }

            Collider.center = ownMesh.bounds.center;
            Collider.size = ownMesh.bounds.size;
        }

        public override void Suspend()
        {
            base.Suspend();
            if (mesh != null)
            {
                mesh.Clear();
            }
        }

        void OnDestroy()
        {
            if (mesh != null)
            {
                Destroy(mesh);
            }
        }        
    }
}