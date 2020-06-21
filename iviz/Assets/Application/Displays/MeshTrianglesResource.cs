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
            if (points is List<Vector3> points_v)
            {
                Mesh.SetVertices(points_v);
            }
            else if (points is Vector3[] points_a)
            {
                Mesh.vertices = points_a;
            }
            else
            {
                Mesh.vertices = points.ToArray();
            }
        }

        void SetNormals(IList<Vector3> points)
        {
            if (points is List<Vector3> points_v)
            {
                Mesh.SetNormals(points_v);
            }
            else if (points is Vector3[] points_a)
            {
                Mesh.normals = points_a;
            }
            else
            {
                Mesh.normals = points.ToArray();
            }
        }

        void SetColors(IList<Color> colors)
        {
            if (colors is List<Color> colors_v)
            {
                Mesh.SetColors(colors_v);
            }
            else if (colors is Color[] colors_a)
            {
                Mesh.colors = colors_a;
            }
            else
            {
                Mesh.colors = colors.ToArray();
            }
        }

        void SetColors(IList<Color32> colors)
        {
            if (colors is List<Color32> colors_v)
            {
                Mesh.SetColors(colors_v);
            }
            else if (colors is Color32[] colors_a)
            {
                Mesh.colors32 = colors_a;
            }
            else
            {
                Mesh.colors32 = colors.ToArray();
            }
        }

        public void SetTriangles(IList<int> indices, int i)
        {
            if (indices is List<int> indices_v)
            {
                Mesh.SetTriangles(indices_v, i);
            }
            else if (indices is int[] indices_a)
            {
                Mesh.SetTriangles(indices_a, i);
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