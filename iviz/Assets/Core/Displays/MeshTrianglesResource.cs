using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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

        [CanBeNull] Mesh mesh;
        Bounds localBounds;

        public Bounds LocalBounds
        {
            get => localBounds;
            private set
            {
                localBounds = value;
                if (!HasBoxCollider)
                {
                    return;
                }

                BoxCollider.center = localBounds.center;
                BoxCollider.size = localBounds.size;
            }
        }

        void OnDestroy()
        {
            if (mesh != null)
            {
                Destroy(mesh);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Color = Color;
            LocalBounds = LocalBounds;
        }

        [NotNull]
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

            Mesh tmpMesh = new Mesh
            {
                indexFormat = indexFormat,
                name = "MeshTrianglesResource Mesh"
            };


            mesh = tmpMesh;
            GetComponent<MeshFilter>().sharedMesh = tmpMesh;
            return tmpMesh;
        }

        static void SetVertices([NotNull] IEnumerable<Vector3> points, [NotNull] Mesh mesh)
        {
            switch (points)
            {
                case List<Vector3> pointsV:
                    mesh.SetVertices(pointsV);
                    break;
                case Vector3[] pointsA:
                    mesh.vertices = pointsA;
                    break;
                default:
                    mesh.vertices = points.ToArray();
                    break;
            }
        }

        static void SetNormals([NotNull] IEnumerable<Vector3> points, [NotNull] Mesh mesh)
        {
            switch (points)
            {
                case List<Vector3> pointsV:
                    mesh.SetNormals(pointsV);
                    break;
                case Vector3[] pointsA:
                    mesh.normals = pointsA;
                    break;
                default:
                    mesh.normals = points.ToArray();
                    break;
            }
        }

        static void SetTexCoords([NotNull] IEnumerable<Vector2> uvs, [NotNull] Mesh mesh)
        {
            switch (uvs)
            {
                case List<Vector2> uvsV:
                    mesh.SetUVs(0, uvsV);
                    break;
                case Vector2[] uvsA:
                    mesh.uv = uvsA;
                    break;
                default:
                    mesh.uv = uvs.ToArray();
                    break;
            }
        }

        static void SetColors([NotNull] IEnumerable<Color> colors, [NotNull] Mesh mesh)
        {
            switch (colors)
            {
                case List<Color> colorsV:
                    mesh.SetColors(colorsV);
                    break;
                case Color[] colorsA:
                    mesh.colors = colorsA;
                    break;
                default:
                    mesh.colors = colors.ToArray();
                    break;
            }
        }

        static void SetColors([NotNull] IEnumerable<Color32> colors, [NotNull] Mesh mesh)
        {
            switch (colors)
            {
                case List<Color32> colorsV:
                    mesh.SetColors(colorsV);
                    break;
                case Color32[] colorsA:
                    mesh.colors32 = colorsA;
                    break;
                default:
                    mesh.colors32 = colors.ToArray();
                    break;
            }
        }

        static void SetTriangles([NotNull] IEnumerable<int> indices, [NotNull] Mesh mesh, int subMesh)
        {
            switch (indices)
            {
                case List<int> indicesV:
                    mesh.SetTriangles(indicesV, subMesh);
                    break;
                case int[] indicesA:
                    mesh.SetTriangles(indicesA, subMesh);
                    break;
                default:
                    mesh.SetTriangles(indices.ToArray(), subMesh);
                    break;
            }
        }


        public void Set([NotNull] Vector3[] points, [CanBeNull] Color[] colors = null)
        {
            if (points is null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Length % 3 != 0)
            {
                throw new ArgumentException($"Invalid triangle list {points.Length}", nameof(points));
            }

            if (colors != null && colors.Length != 0 && colors.Length != points.Length)
            {
                throw new ArgumentException("Inconsistent color size!", nameof(colors));
            }

            int[] triangles = Enumerable.Range(0, points.Length).ToArray();

            Mesh ownMesh = EnsureOwnMesh(points.Length);

            ownMesh.Clear();
            SetVertices(points, ownMesh);
            if (colors != null && colors.Length != 0)
            {
                SetColors(colors, ownMesh);
            }

            if (FlipWinding)
            {
                unsafe
                {
                    fixed (int* tPtr = triangles)
                    {
                        int* endPtr = tPtr + triangles.Length;
                        for (int* ptr = tPtr; ptr != endPtr; ptr += 3)
                        {
                            int t = ptr[1];
                            ptr[1] = ptr[2];
                            ptr[2] = t;
                        }
                    }
                }
            }

            SetTriangles(triangles, ownMesh, 0);
            ownMesh.RecalculateNormals();

            LocalBounds = ownMesh.bounds;
        }

        public void Set(
            [NotNull] Vector3[] points,
            [CanBeNull] Vector3[] normals,
            [CanBeNull] Vector2[] texCoords,
            [NotNull] int[] triangles,
            [CanBeNull] Color32[] colors = null)
        {
            if (points is null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (triangles is null)
            {
                throw new ArgumentNullException(nameof(triangles));
            }

            if (triangles.Length % 3 != 0)
            {
                throw new ArgumentException($"Invalid triangle list {points.Length}", nameof(points));
            }

            if (normals != null && normals.Length != 0 && normals.Length != points.Length)
            {
                throw new ArgumentException("Inconsistent normals size!", nameof(normals));
            }

            if (colors != null && colors.Length != 0 && colors.Length != points.Length)
            {
                throw new ArgumentException("Inconsistent color size!", nameof(colors));
            }

            Mesh ownMesh = EnsureOwnMesh(points.Length);
            bool hasNormals = normals != null && normals.Length != 0;


            ownMesh.Clear();
            SetVertices(points, ownMesh);
            if (hasNormals)
            {
                SetNormals(normals, ownMesh);
            }

            if (texCoords != null && texCoords.Length != 0)
            {
                SetTexCoords(texCoords, ownMesh);
            }

            if (colors != null && colors.Length != 0)
            {
                SetColors(colors, ownMesh);
            }

            SetTriangles(triangles, ownMesh, 0);

            if (!hasNormals)
            {
                ownMesh.RecalculateNormals();
            }

            ownMesh.Optimize();
            LocalBounds = ownMesh.bounds;
        }

        public override void Suspend()
        {
            base.Suspend();
            if (mesh != null)
            {
                mesh.Clear();
            }
        }
    }
}