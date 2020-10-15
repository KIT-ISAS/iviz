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

        [SerializeField] int size;

        NativeArray<float4x2> lineBuffer;
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


        int Size
        {
            get => size;
            set
            {
                if (value == size)
                {
                    return;
                }

                size = value;
                Reserve(size * 11 / 10);
            }
        }

        bool UseCapsuleLines => Size <= MaxSegmentsForMesh;

        public static bool IsElementValid(in LineWithColor t) => !t.HasNaN() &&
                                                                 (t.A - t.B).sqrMagnitude > MinLineWidthSq &&
                                                                 t.A.sqrMagnitude < MaxPositionMagnitudeSq &&
                                                                 t.B.sqrMagnitude < MaxPositionMagnitudeSq;

        /// <summary>
        /// Sets the lines with the given collection.
        /// </summary>
        public IReadOnlyCollection<LineWithColor> LinesWithColor
        {
            get => new GetHelper(lineBuffer);
            set => Set(value.Count, value);
        }

        /// <summary>
        /// Sets the lines with the given enumeration.
        /// </summary>
        /// <param name="count">The number of lines, or at least an upper bound.</param>
        /// <param name="lines">The line enumerator.</param>
        public void Set(int count, IEnumerable<LineWithColor> lines)
        {
            if (count < 0)
            {
                throw new ArgumentException("Invalid count " + count, nameof(count));
            }

            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            Size = count;

            linesNeedAlpha = false;
            if (UseColormap)
            {
                int realSize = 0;
                foreach (LineWithColor t in lines)
                {
                    if (!IsElementValid(t)) { continue; }

                    lineBuffer[realSize++] = t;
                }

                Size = realSize;
            }
            else
            {
                int realSize = 0;
                foreach (LineWithColor t in lines)
                {
                    if (!IsElementValid(t)) { continue; }

                    lineBuffer[realSize++] = t;
                    linesNeedAlpha |= t.ColorA.a < 255 || t.ColorB.a < 255;
                }

                Size = realSize;
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

            if (lineBuffer.Length > 0)
            {
                lineBuffer.Dispose();
            }
        }

        void Reserve(int reqDataSize)
        {
            if (lineBuffer.Length >= reqDataSize)
            {
                return;
            }

            if (lineBuffer.Length != 0)
            {
                lineBuffer.Dispose();
            }

            lineBuffer = new NativeArray<float4x2>(reqDataSize, Allocator.Persistent);

            lineComputeBuffer?.Release();
            lineComputeBuffer = new ComputeBuffer(lineBuffer.Length, Marshal.SizeOf<LineWithColor>());
            Properties.SetBuffer(LinesID, lineComputeBuffer);
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

            if (lineComputeBuffer == null)
            {
                Debug.Log("Aa");
            }
            
            lineComputeBuffer.SetData(lineBuffer, 0, 0, Size);
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
                LineUtils.CreateCapsulesFromSegments(lineBuffer.AsReadOnlyList(), ElementScale);

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

            if (lineBuffer.Length != 0)
            {
                lineComputeBuffer = new ComputeBuffer(lineBuffer.Length, Marshal.SizeOf<LineWithColor>());
                lineComputeBuffer.SetData(lineBuffer, 0, 0, Size);
                Properties.SetBuffer(LinesID, lineComputeBuffer);
            }

            IntensityBounds = IntensityBounds;
            Colormap = Colormap;
            Tint = Tint;
        }

        public override void Suspend()
        {
            base.Suspend();
            Size = 0;
            mesh.Clear();
            meshRenderer.enabled = false;

            Debug.Log(lineBuffer.Length);
            if (lineBuffer.Length != 0)
            {
                lineBuffer.Dispose();
            }            
            
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