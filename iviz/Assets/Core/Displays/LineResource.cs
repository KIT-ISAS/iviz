using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Iviz.Resources;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Iviz.Displays
{
    /// <summary>
    /// Displays sets of lines.
    /// It has two modalities: lines as capsule meshes if there are less than 30 segments, otherwise
    /// using mesh instantiation. 
    /// </summary>
    public sealed class LineResource : MarkerResourceWithColormap
    {
        const int MaxSegmentsForMesh = 30;
        const float MinLineWidthSq = 1E-09f;
        const float MaxPositionMagnitudeSq = 1e9f;

        static readonly int LinesID = Shader.PropertyToID("_Lines");
        static readonly int ScaleID = Shader.PropertyToID("_Scale");

        NativeList<float4x2> lineBuffer;
        ComputeBuffer lineComputeBuffer;
        
        bool linesNeedAlpha;

        Mesh mesh;
        MeshRenderer meshRenderer;

        bool UsesAlpha => linesNeedAlpha || Tint.a <= 254f / 255f;

        public override bool UseColormap
        {
            get => base.UseColormap;
            set
            {
                base.UseColormap = value;
                if (UseCapsuleLines)
                {
                    UpdateMeshMaterial();
                }
            }
        }


        int Size => lineBuffer.Length;

        bool UseCapsuleLines => Size <= MaxSegmentsForMesh;

        static bool IsElementValid(in LineWithColor t) => !t.HasNaN() &&
                                                          (t.A - t.B).MagnitudeSq() > MinLineWidthSq &&
                                                          t.A.MagnitudeSq() < MaxPositionMagnitudeSq &&
                                                          t.B.MagnitudeSq() < MaxPositionMagnitudeSq;

        /// <summary>
        /// Sets the lines with the given collection.
        /// </summary>
        public IReadOnlyCollection<LineWithColor> LinesWithColor
        {
            get => new GetHelper(lineBuffer);
            set => Set(value, value.Count);
        }

        /// <summary>
        /// Sets the lines with the given enumeration.
        /// </summary>
        /// <param name="lines">The line enumerator.</param>
        /// <param name="reserve">The expected number of lines, or 0 if unknown.</param>
        public void Set(IEnumerable<LineWithColor> lines, int reserve = 0)
        {
            if (reserve < 0)
            {
                throw new ArgumentException($"Invalid count {reserve}", nameof(reserve));
            }

            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            if (reserve > 0)
            {
                lineBuffer.Capacity = Math.Max(lineBuffer.Capacity, reserve);
            }

            linesNeedAlpha = false;
            lineBuffer.Clear();
            if (UseColormap)
            {
                foreach (LineWithColor t in lines)
                {
                    if (!IsElementValid(t))
                    {
                        continue;
                    }

                    lineBuffer.Add(t);
                }
            }
            else
            {
                foreach (LineWithColor t in lines)
                { 
                    if (!IsElementValid(t))
                    {
                        continue;
                    }

                    lineBuffer.Add(t);
                    linesNeedAlpha |= t.ColorA.a < 255 || t.ColorB.a < 255;
                }
            }
            
            if (UseCapsuleLines)
            {
                UpdateLineMesh();
            }
            else
            {
                UpdateLineBuffer();
            }
        }

        public override Color Tint
        {
            get => base.Tint;
            set
            {
                base.Tint = value;
                if (UseCapsuleLines)
                {
                    UpdateMeshMaterial();
                }
            }
        }

        public override float ElementScale
        {
            get => base.ElementScale;
            set
            {
                base.ElementScale = value;
                if (UseCapsuleLines)
                {
                    UpdateLineMesh();
                }
            }
        }

        protected override void Awake()
        {
            lineBuffer = new NativeList<float4x2>(Allocator.Persistent);
            mesh = new Mesh {name = "Line Capsule"};
            GetComponent<MeshFilter>().sharedMesh = mesh;
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.SetPropertyBlock(Properties);

            base.Awake();
            ElementScale = 0.1f;
            UseColormap = false;
            IntensityBounds = new Vector2(0, 1);
        }

        void Update()
        {
            if (Size == 0)
            {
                return;
            }

            UpdateTransform();
            Properties.SetFloat(ScaleID, ElementScale * transform.lossyScale.x);

            Bounds worldBounds = BoxCollider.bounds;

            Material material;
            switch (UseColormap)
            {
                case true when !UsesAlpha:
                    material = Resource.Materials.LineWithColormap.Object;
                    break;
                case true:
                    material = Resource.Materials.TransparentLineWithColormap.Object;
                    break;
                case false when !UsesAlpha:
                    material = Resource.Materials.Line.Object;
                    break;
                case false:
                    material = Resource.Materials.TransparentLine.Object;
                    break;
            }

            Graphics.DrawProcedural(material, worldBounds, MeshTopology.Quads, 2 * 4, Size,
                null, Properties, ShadowCastingMode.Off, false, gameObject.layer);
        }

        void OnDestroy()
        {
            if (lineComputeBuffer != null)
            {
                lineComputeBuffer.Release();
                lineComputeBuffer = null;
                Properties.SetBuffer(LinesID, null);
            }

            lineBuffer.Dispose();
        }

        protected override void UpdateProperties()
        {
            if (meshRenderer != null)
            {
                meshRenderer.SetPropertyBlock(Properties);
            }
        }

        void UpdateLineBuffer()
        {
            if (Size == 0)
            {
                return;
            }

            if (lineComputeBuffer == null || lineComputeBuffer.count < lineBuffer.Capacity)
            {
                lineComputeBuffer?.Release();
                lineComputeBuffer = new ComputeBuffer(lineBuffer.Capacity, Marshal.SizeOf<LineWithColor>());
                Properties.SetBuffer(LinesID, lineComputeBuffer);
            }

            lineComputeBuffer.SetData(lineBuffer.AsArray(), 0, 0, Size);
            CalculateBounds();

            enabled = true;
            meshRenderer.enabled = false;
        }


        void UpdateLineMesh()
        {
            if (Size == 0)
            {
                enabled = false;
                meshRenderer.enabled = false;
                return;
            }

            var (points, colors, indices, coords) =
                LineUtils.CreateCapsulesFromSegments(lineBuffer, ElementScale);

            mesh.Clear();
            mesh.vertices = points;
            mesh.triangles = indices;
            mesh.colors32 = colors;
            mesh.uv = coords;

            CalculateBounds();

            enabled = false;
            meshRenderer.enabled = true;

            UpdateMeshMaterial();
        }

        Material GetMeshMaterial()
        {
            switch (UseColormap)
            {
                case true when !UsesAlpha:
                    return Resource.Materials.LineSimpleWithColormap.Object;
                case true:
                    return Resource.Materials.TransparentLineSimpleWithColormap.Object;
                case false when !UsesAlpha:
                    return Resource.Materials.LineSimple.Object;
                case false:
                    return Resource.Materials.TransparentLineSimple.Object;
            }
        }

        void UpdateMeshMaterial()
        {
            if (meshRenderer != null)
            {
                meshRenderer.sharedMaterial = GetMeshMaterial();
            }
        }

        void CalculateBounds()
        {
            MinMaxJob.CalculateBounds(lineBuffer, Size, out Bounds bounds, out Vector2 span);
            BoxCollider.center = bounds.center;
            BoxCollider.size = bounds.size + ElementScale * Vector3.one;
            IntensityBounds = span;
        }

        protected override void Rebuild()
        {
            if (UseCapsuleLines)
            {
                return;
            }

            if (lineComputeBuffer != null)
            {
                lineComputeBuffer.Release();
                lineComputeBuffer = null;
                Properties.SetBuffer(LinesID, null);
            }

            if (lineBuffer.Capacity != 0)
            {
                lineComputeBuffer = new ComputeBuffer(lineBuffer.Capacity, Marshal.SizeOf<LineWithColor>());
                lineComputeBuffer.SetData(lineBuffer.AsArray(), 0, 0, Size);
                Properties.SetBuffer(LinesID, lineComputeBuffer);
            }

            IntensityBounds = IntensityBounds;
            Colormap = Colormap;
            Tint = Tint;
        }

        public override void Suspend()
        {
            base.Suspend();
            mesh.Clear();
            meshRenderer.enabled = false;

            lineBuffer.Clear();

            lineComputeBuffer?.Release();
            lineComputeBuffer = null;
            Properties.SetBuffer(LinesID, null);
        }

        class GetHelper : IReadOnlyCollection<LineWithColor>
        {
            readonly NativeArray<float4x2> nArray;
            public GetHelper(in NativeArray<float4x2> array) => nArray = array;
            public int Count => nArray.Length;

            public IEnumerator<LineWithColor> GetEnumerator() =>
                nArray.Select(f => new LineWithColor(f)).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() =>
                nArray.Select(f => new LineWithColor(f)).GetEnumerator();
        }
    }
}