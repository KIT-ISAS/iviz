using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
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
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class LineResource : MarkerResourceWithColormap
    {
        const int MaxSegmentsForMesh = 30;
        const float MinLineWidthSq = 1E-09f;
        const float MaxPositionMagnitudeSq = 1e9f;

        static readonly int LinesID = Shader.PropertyToID("_Lines");
        static readonly int ScaleID = Shader.PropertyToID("_Scale");

        readonly CapsuleLinesHelper capsuleHelper = new CapsuleLinesHelper();
        
        NativeList<float4x2> lineBuffer;
        [CanBeNull] ComputeBuffer lineComputeBuffer;

        bool linesNeedAlpha;
        Mesh mesh;

        [CanBeNull] MeshRenderer meshRenderer;

        [NotNull]
        MeshRenderer MeshRenderer => meshRenderer != null ? meshRenderer : meshRenderer = GetComponent<MeshRenderer>();

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

        public static bool IsElementValid(in LineWithColor t) => !t.HasNaN() &&
                                                                 (t.A - t.B).MagnitudeSq() > MinLineWidthSq &&
                                                                 t.A.MagnitudeSq() < MaxPositionMagnitudeSq &&
                                                                 t.B.MagnitudeSq() < MaxPositionMagnitudeSq;

        /// <summary>
        /// Sets the lines with the given collection.
        /// </summary>
        [NotNull]
        public IReadOnlyCollection<LineWithColor> LinesWithColor
        {
            //get => lineBuffer.Select(f => new LineWithColor(f)).ToArray();
            set => Set(value, value.Count);
        }

        /// <summary>
        /// Sets the lines with the given enumeration.
        /// </summary>
        /// <param name="lines">The line enumerator.</param>
        /// <param name="reserve">The expected number of lines, or 0 if unknown.</param>
        /// <param name="overrideNeedsAlpha">A check of alpha colors will be done if <see cref="UseColormap"/> is disabled. Use this to override the check.</param>
        public void Set([NotNull] IEnumerable<LineWithColor> lines, int reserve = 0, bool? overrideNeedsAlpha = null)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            if (reserve != 0)
            {
                lineBuffer.Capacity = Math.Max(lineBuffer.Capacity, reserve);
            }

            lineBuffer.Clear();
            foreach (LineWithColor t in lines)
            {
                if (!IsElementValid(t))
                {
                    continue;
                }

                lineBuffer.Add(t);
            }

            linesNeedAlpha = !UseColormap && (overrideNeedsAlpha ?? CheckIfAlphaNeeded());

            if (UseCapsuleLines)
            {
                UpdateLineMesh();
            }
            else
            {
                UpdateLineBuffer();
            }
        }

        /// <summary>
        /// Delegate for a function that receives a line list as parameter, sets the values,
        /// and returns true if alpha is needed, false if not, or null to request a manual check.
        /// </summary>
        /// <param name="lineBuffer">The line list to be set</param>
        public delegate bool? DirectLineSetter(ref NativeList<float4x2> lineBuffer);

        /// <summary>
        /// Exposes the line list directly for manual setting.
        /// </summary>
        /// <param name="callback">
        /// A function that receives the internal line list as parameter, and returns true if alpha is needed,
        /// false if not, or null to request a manual check.
        /// </param>
        /// <param name="reserve">The expected number of lines, or 0 if unknown.</param>
        public void SetDirect([NotNull] DirectLineSetter callback, int reserve = 0)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (reserve != 0)
            {
                lineBuffer.Capacity = Math.Max(lineBuffer.Capacity, reserve);
            }

            lineBuffer.Clear();
            bool? overrideNeedsAlpha = callback(ref lineBuffer);

            linesNeedAlpha = !UseColormap && (overrideNeedsAlpha ?? CheckIfAlphaNeeded());

            if (UseCapsuleLines)
            {
                UpdateLineMesh();
            }
            else
            {
                UpdateLineBuffer();
            }
        }

        bool CheckIfAlphaNeeded()
        {
            foreach (float4x2 t in lineBuffer)
            {
                Color32 cA = PointWithColor.ColorFromFloatBits(t.c0.w);
                Color32 cB = PointWithColor.ColorFromFloatBits(t.c1.w);
                if (cA.a < 255 || cB.a < 255)
                {
                    return true;
                }
            }

            return false;
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
            mesh = new Mesh {name = "Line Capsules"};
            GetComponent<MeshFilter>().sharedMesh = mesh;
            MeshRenderer.SetPropertyBlock(Properties);

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
            lineBuffer.Dispose();

            if (lineComputeBuffer != null)
            {
                lineComputeBuffer.Release();
                lineComputeBuffer = null;
                Properties.SetBuffer(LinesID, null);
            }

            Destroy(mesh);
        }

        protected override void UpdateProperties()
        {
            MeshRenderer.SetPropertyBlock(Properties);
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
            MeshRenderer.enabled = false;
        }


        void UpdateLineMesh()
        {
            if (Size == 0)
            {
                enabled = false;
                MeshRenderer.enabled = false;
                return;
            }

            capsuleHelper.CreateCapsulesFromSegments(lineBuffer, ElementScale);
            capsuleHelper.UpdateMesh(mesh);

            CalculateBounds();

            enabled = false;
            MeshRenderer.enabled = true;

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
            MeshRenderer.sharedMaterial = GetMeshMaterial();
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
            MeshRenderer.enabled = false;

            lineBuffer.Clear();

            lineComputeBuffer?.Release();
            lineComputeBuffer = null;
            Properties.SetBuffer(LinesID, null);
        }
    }
}