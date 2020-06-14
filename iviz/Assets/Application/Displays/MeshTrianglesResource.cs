using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class MeshTrianglesResource : MarkerResource, ISupportsAROcclusion, ISupportsTint
    {
        MeshRenderer mainRenderer;
        public Mesh Mesh { get; private set; }

        [SerializeField] Color color_ = Color.white;
        public Color Color
        {
            get => color_;
            set
            {
                color_ = value;
                SetEffectiveColor();
            }
        }

        [SerializeField] bool occlusionOnly_;
        public bool OcclusionOnly
        {
            get => occlusionOnly_;
            set
            {
                occlusionOnly_ = value;
                if (value)
                {
                    mainRenderer.material = Resource.Materials.LitOcclusionOnly.Object;
                }
                else
                {
                    Color = color_;
                }
            }
        }

        [SerializeField] Color tint_ = Color.white;
        public Color Tint
        {
            get => tint_;
            set
            {
                tint_ = value;
                SetEffectiveColor();
            }
        }

        Color EffectiveColor => Color * Tint;

        void SetEffectiveColor()
        {
            if (!OcclusionOnly)
            {
                Color effectiveColor = EffectiveColor;
                Material material = effectiveColor.a > 254f / 255f ?
                    Resource.Materials.Lit.Object :
                    Resource.Materials.TransparentLit.Object;
                mainRenderer.material = material;
                mainRenderer.SetPropertyColor(effectiveColor);
            }
        }

        public override string Name => "MeshTriangles";

        protected override void Awake()
        {
            base.Awake();
            mainRenderer = GetComponent<MeshRenderer>();
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

        
        public void SetTriangles(IList<int> indices)
        {
            if (indices is List<int> indices_v)
            {
                Mesh.SetTriangles(indices_v, 0);
            }
            else if (indices is int[] indices_a)
            {
                Mesh.SetTriangles(indices_a, 0);
            }
            else
            {
                Mesh.SetTriangles(indices.ToArray(), 0);
            }
        }
        

        public void Set(IList<Vector3> points, IList<Color> colors = null)
        {
            if (points.Count % 3 != 0)
            {
                throw new ArgumentException("Invalid triangle list " + points.Count, nameof(points));
            }
            if (colors != null && colors.Count != points.Count)
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
            if (colors != null)
            {
                SetColors(colors);
            }
            Mesh.SetTriangles(triangles, 0);
            Mesh.RecalculateNormals();

            Collider.center = Mesh.bounds.center;
            Collider.size = Mesh.bounds.size;
        }

        public override void Stop()
        {
            base.Stop();
            Color = Color.white;
            ColliderEnabled = true;
            OcclusionOnly = false;
        }

        /*
        public void Set(IList<Vector3> points, IList<int> triangles)
        {
            if (triangles.Count % 3 != 0)
            {
                throw new ArgumentException("Invalid triangle list " + triangles.Count, nameof(triangles));
            }

            Mesh.Clear();
            SetVertices(points);
            SetTriangles(triangles);
            Mesh.RecalculateBounds();
            Mesh.RecalculateNormals();
        }
        */
    }
}