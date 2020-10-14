using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Resource that constructs a mesh from vertices, triangles, normals, etc.
    /// </summary>
    public sealed class MeshTrianglesResource : MeshMarkerResource
    {
        Mesh mesh;
        Bounds localBounds;

        public Bounds LocalBounds
        {
            get => localBounds;
            private set
            {
                localBounds = value;
                if (BoxCollider == null)
                {
                    return;
                }
                BoxCollider.center = localBounds.center;
                BoxCollider.size = localBounds.size;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Color = Color;
            LocalBounds = LocalBounds;
        }

        void EnsureOwnMesh(int pointsCount)
        {
            if (mesh != null)
            {
                return;
            }

            mesh = new Mesh
            {
                indexFormat = pointsCount > ushort.MaxValue
                    ? UnityEngine.Rendering.IndexFormat.UInt32
                    : UnityEngine.Rendering.IndexFormat.UInt16,
                name = "MeshTrianglesResource Mesh"
            };


            GetComponent<MeshFilter>().sharedMesh = mesh;
        }

        void SetVertices(IEnumerable<Vector3> points)
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

        void SetNormals(IEnumerable<Vector3> points)
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

        void SetTexCoords(IEnumerable<Vector2> uvs)
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

        void SetColors(IEnumerable<Color> colors)
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

        void SetColors(IEnumerable<Color32> colors)
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

        void SetTriangles(IEnumerable<int> indices, int i)
        {
            switch (indices)
            {
                case List<int> indicesV:
                    mesh.SetTriangles(indicesV, i);
                    break;
                case int[] indicesA:
                    mesh.SetTriangles(indicesA, i);
                    break;
                default:
                    mesh.SetTriangles(indices.ToArray(), i);
                    break;
            }
        }


        public void Set(IReadOnlyCollection<Vector3> points, IReadOnlyCollection<Color> colors = null)
        {
            if (points is null)
            {
                throw new ArgumentNullException(nameof(points));
            }
            
            if (points.Count % 3 != 0)
            {
                throw new ArgumentException($"Invalid triangle list {points.Count}", nameof(points));
            }

            if (colors != null && colors.Count != 0 && colors.Count != points.Count)
            {
                throw new ArgumentException("Inconsistent color size!", nameof(colors));
            }

            int[] triangles = Enumerable.Range(0, points.Count).ToArray();

            EnsureOwnMesh(points.Count);

            mesh.Clear();
            SetVertices(points);
            if (colors != null && colors.Count != 0)
            {
                SetColors(colors);
            }

            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();

            LocalBounds = mesh.bounds;
        }

        public void Set(
            IReadOnlyCollection<Vector3> points,
            IReadOnlyCollection<Vector3> normals,
            IReadOnlyCollection<Vector2> texCoords,
            IReadOnlyCollection<int> triangles,
            IReadOnlyCollection<Color32> colors = null)
        {
            if (points is null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (triangles is null)
            {
                throw new ArgumentNullException(nameof(triangles));
            }

            if (triangles.Count % 3 != 0)
            {
                throw new ArgumentException($"Invalid triangle list {points.Count}", nameof(points));
            }

            if (normals != null && normals.Count != 0 && normals.Count != points.Count)
            {
                throw new ArgumentException("Inconsistent normals size!", nameof(normals));
            }
            
            if (colors != null && colors.Count != 0 && colors.Count != points.Count)
            {
                throw new ArgumentException("Inconsistent color size!", nameof(colors));
            }

            EnsureOwnMesh(points.Count);

            mesh.Clear();
            SetVertices(points);
            SetNormals(normals);
            if (texCoords != null && texCoords.Count != 0)
            {
                SetTexCoords(texCoords);
            }

            if (colors != null && colors.Count != 0)
            {
                SetColors(colors);
            }

            SetTriangles(triangles, 0);

            mesh.Optimize();

            LocalBounds = mesh.bounds;

        }

        public override void Suspend()
        {
            base.Suspend();
            mesh?.Clear();
        }
    }
}