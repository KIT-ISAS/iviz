using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class MeshTrianglesResource : MeshMarkerResource
    {
        public Mesh Mesh { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Mesh = new Mesh();
            Mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            GetComponent<MeshFilter>().mesh = Mesh;
        }

        void SetVertices(IList<Vector3> points)
        {
            if (points is List<Vector3> pointsV)
            {
                Mesh.SetVertices(pointsV);
            }
            else if (points is Vector3[] pointsA)
            {
                Mesh.vertices = pointsA;
            }
            else
            {
                Mesh.vertices = points.ToArray();
            }
        }

        void SetNormals(IEnumerable<Vector3> points)
        {
            if (points is List<Vector3> pointsV)
            {
                Mesh.SetNormals(pointsV);
            }
            else if (points is Vector3[] pointsA)
            {
                Mesh.normals = pointsA;
            }
            else
            {
                Mesh.normals = points.ToArray();
            }
        }

        void SetColors(IEnumerable<Color> colors)
        {
            if (colors is List<Color> colorsV)
            {
                Mesh.SetColors(colorsV);
            }
            else if (colors is Color[] colorsA)
            {
                Mesh.colors = colorsA;
            }
            else
            {
                Mesh.colors = colors.ToArray();
            }
        }

        void SetColors(IEnumerable<Color32> colors)
        {
            if (colors is List<Color32> colorsV)
            {
                Mesh.SetColors(colorsV);
            }
            else if (colors is Color32[] colorsA)
            {
                Mesh.colors32 = colorsA;
            }
            else
            {
                Mesh.colors32 = colors.ToArray();
            }
        }

        void SetTriangles(IEnumerable<int> indices, int i)
        {
            if (indices is List<int> indicesV)
            {
                Mesh.SetTriangles(indicesV, i);
            }
            else if (indices is int[] indicesA)
            {
                Mesh.SetTriangles(indicesA, i);
            }
            else
            {
                Mesh.SetTriangles(indices.ToArray(), i);
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