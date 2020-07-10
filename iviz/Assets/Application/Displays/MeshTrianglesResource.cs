using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class MeshTrianglesResource : MeshMarkerResource
    {
        Mesh Mesh { get; set; }

        protected override void Awake()
        {
            base.Awake();

            Mesh = new Mesh();
            Mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            GetComponent<MeshFilter>().mesh = Mesh;
        }

        void SetVertices(IEnumerable<Vector3> points)
        {
            switch (points)
            {
                case List<Vector3> pointsV:
                    Mesh.SetVertices(pointsV);
                    break;
                case Vector3[] pointsA:
                    Mesh.vertices = pointsA;
                    break;
                default:
                    Mesh.vertices = points.ToArray();
                    break;
            }
        }

        void SetNormals(IEnumerable<Vector3> points)
        {
            switch (points)
            {
                case List<Vector3> pointsV:
                    Mesh.SetNormals(pointsV);
                    break;
                case Vector3[] pointsA:
                    Mesh.normals = pointsA;
                    break;
                default:
                    Mesh.normals = points.ToArray();
                    break;
            }
        }

        void SetColors(IEnumerable<Color> colors)
        {
            switch (colors)
            {
                case List<Color> colorsV:
                    Mesh.SetColors(colorsV);
                    break;
                case Color[] colorsA:
                    Mesh.colors = colorsA;
                    break;
                default:
                    Mesh.colors = colors.ToArray();
                    break;
            }
        }

        void SetColors(IEnumerable<Color32> colors)
        {
            switch (colors)
            {
                case List<Color32> colorsV:
                    Mesh.SetColors(colorsV);
                    break;
                case Color32[] colorsA:
                    Mesh.colors32 = colorsA;
                    break;
                default:
                    Mesh.colors32 = colors.ToArray();
                    break;
            }
        }

        void SetTriangles(IEnumerable<int> indices, int i)
        {
            switch (indices)
            {
                case List<int> indicesV:
                    Mesh.SetTriangles(indicesV, i);
                    break;
                case int[] indicesA:
                    Mesh.SetTriangles(indicesA, i);
                    break;
                default:
                    Mesh.SetTriangles(indices.ToArray(), i);
                    break;
            }
        }


        public void Set(IList<Vector3> points, IList<Color> colors = null)
        {
            if (points.Count % 3 != 0)
            {
                throw new ArgumentException("Invalid triangle list " + points.Count, nameof(points));
            }
            if (colors != null && colors.Count != 0 && colors.Count != points.Count)
            {
                throw new ArgumentException("Inconsistent color size!");
            }
            int[] triangles = new int[points.Count];
            for (int i = 0; i < triangles.Length; i++)
            {
                triangles[i] = i;
            }

            Mesh.Clear();
            SetVertices(points);
            if (colors != null && colors.Count != 0)
            {
                SetColors(colors);
            }
            Mesh.SetTriangles(triangles, 0);
            Mesh.RecalculateNormals();

            Collider.center = Mesh.bounds.center;
            Collider.size = Mesh.bounds.size;
        }

        public void Set(IList<Vector3> points, IList<Vector3> normals, IList<int> triangles, IList<Color32> colors = null)
        {
            if (points.Count % 3 != 0)
            {
                throw new ArgumentException("Invalid triangle list " + points.Count, nameof(points));
            }
            if (colors != null && colors.Count != 0 && colors.Count != points.Count)
            {
                throw new ArgumentException("Inconsistent color size!");
            }

            Mesh.Clear();
            SetVertices(points);
            SetNormals(normals);
            if (colors != null && colors.Count != 0)
            {
                SetColors(colors);
            }
            SetTriangles(triangles, 0);
            Mesh.RecalculateNormals();

            Collider.center = Mesh.bounds.center;
            Collider.size = Mesh.bounds.size;

            Mesh.Optimize();
        }

        public override void Stop()
        {
            base.Stop();
            Mesh.Clear();
        }

    }
}