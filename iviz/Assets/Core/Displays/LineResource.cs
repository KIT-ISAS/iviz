using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Iviz.Core;
using Iviz.Resources;
using Iviz.XmlRpc;
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
        public enum LineRenderType
        {
            Auto,
            AlwaysCapsule,
            AlwaysBillboard,
        }

        static int helperForMaxSegmentsForMesh = -1;

        static int MaxSegmentsForMesh => helperForMaxSegmentsForMesh != -1
            ? helperForMaxSegmentsForMesh
            : (helperForMaxSegmentsForMesh = Settings.SupportsComputeBuffers ? 30 : int.MaxValue);

        const float MinLineLength = 1E-06f;
        const float MaxPositionMagnitude = 1e3f;

        static readonly int LinesID = Shader.PropertyToID("_Lines");
        static readonly int ScaleID = Shader.PropertyToID("_Scale");

        readonly NativeList<float4x2> lineBuffer = new NativeList<float4x2>();

        [CanBeNull] ComputeBuffer lineComputeBuffer;
        [CanBeNull] Mesh mesh;
        [CanBeNull] MeshRenderer meshRenderer;

        bool linesNeedAlpha;

        [NotNull] Mesh Mesh => mesh != null ? mesh : (mesh = new Mesh {name = "Line Capsules"});

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

        bool UseCapsuleLines => (RenderType == LineRenderType.Auto && Size <= MaxSegmentsForMesh) ||
                                (RenderType == LineRenderType.AlwaysCapsule);

        public LineRenderType RenderType { get; set; }

        public static bool IsElementValid(in LineWithColor t) => !t.HasNaN() &&
                                                                 (t.A - t.B).MaxAbsCoeff() > MinLineLength &&
                                                                 t.A.MaxAbsCoeff() < MaxPositionMagnitude &&
                                                                 t.B.MaxAbsCoeff() < MaxPositionMagnitude;

        /// <summary>
        /// Sets the lines with the given list.
        /// </summary>
        /// <param name="lines">The line list.</param>
        /// <param name="overrideNeedsAlpha">A check of alpha colors will be done if <see cref="UseColormap"/> is disabled. Use this to override the check.</param>
        public void Set([NotNull] NativeList<LineWithColor> lines, bool? overrideNeedsAlpha = null)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            lineBuffer.EnsureCapacity(lines.Length);

            lineBuffer.Clear();
            foreach (ref LineWithColor t in lines.Ref())
            {
                if (!IsElementValid(t))
                {
                    continue;
                }

                lineBuffer.Add(t.f);
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

        public void Set([NotNull] LineWithColor[] lines, bool? overrideNeedsAlpha = null)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            lineBuffer.EnsureCapacity(lines.Length);

            lineBuffer.Clear();
            foreach (ref LineWithColor t in lines.Ref())
            {
                if (!IsElementValid(t))
                {
                    continue;
                }

                lineBuffer.Add(t.f);
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

        public void Reset()
        {
            lineBuffer.Clear();
            linesNeedAlpha = false;
            UpdateLineMesh();
        }

        /// <summary>
        /// Delegate for a function that receives a line list as parameter, sets the values,
        /// and returns true if alpha is needed, false if not, or null to request a manual check.
        /// </summary>
        /// <param name="lineBuffer">The line list to be set</param>
        public delegate bool? DirectLineSetter([NotNull] NativeList<float4x2> lineBuffer);

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
                lineBuffer.EnsureCapacity(reserve);
            }

            lineBuffer.Clear();
            bool? overrideNeedsAlpha = callback(lineBuffer);

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
            foreach (ref float4x2 t in lineBuffer.Ref())
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
            GetComponent<MeshFilter>().sharedMesh = Mesh;
            MeshRenderer.SetPropertyBlock(Properties);

            base.Awake();

            ElementScale = 0.1f;
            UseColormap = false;
        }

        void Update()
        {
            if (Size == 0)
            {
                return;
            }

            UpdateTransform();
            //Properties.SetFloat(ScaleID, ElementScale * transform.lossyScale.x);
            Properties.SetFloat(ScaleID, ElementScale);

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
                Properties.SetBuffer(LinesID, (ComputeBuffer) null);
            }

            Destroy(Mesh);
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

            CapsuleLinesHelper.CreateCapsulesFromSegments(lineBuffer, ElementScale, Mesh);

            CalculateBounds();

            enabled = false;
            MeshRenderer.enabled = true;

            UpdateMeshMaterial();
        }

        [NotNull]
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
            MinMaxJob.CalculateBounds(lineBuffer.AsArray(), Size, out Bounds bounds, out Vector2 span);
            BoxCollider.center = bounds.center;
            BoxCollider.size = bounds.size + ElementScale * Vector3.one;

            MeasuredIntensityBounds = span;
            if (!OverrideIntensityBounds)
            {
                IntensityBounds = span;
            }
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
                Properties.SetBuffer(LinesID, (ComputeBuffer) null);
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
            Mesh.Clear();
            MeshRenderer.enabled = false;

            lineBuffer.Clear();

            lineComputeBuffer?.Release();
            lineComputeBuffer = null;
            Properties.SetBuffer(LinesID, (ComputeBuffer) null);
        }
    }
}