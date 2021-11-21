#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
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

        readonly NativeList<float4x2> lineBuffer = new();

        ComputeBuffer? lineComputeBuffer;
        Mesh? mesh;

        [SerializeField] MeshRenderer? meshRenderer = null;
        [SerializeField] MeshFilter? meshFilter = null;

        bool linesNeedAlpha;

        Mesh Mesh => mesh != null ? mesh : (mesh = new Mesh {name = "Line Capsules"});
        MeshRenderer MeshRenderer => meshRenderer.AssertNotNull(nameof(meshRenderer));
        MeshFilter MeshFilter => meshFilter.AssertNotNull(nameof(meshFilter));
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

        Material? materialOverride;

        public Material? MaterialOverride
        {
            get => materialOverride;
            set
            {
                materialOverride = value;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsElementValid(in LineWithColor t) => IsElementValid(t.f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsElementValid(in float4x2 f) => !f.c0.HasNaN() &&
                                                            !f.c1.HasNaN() &&
                                                            (f.c0 - f.c1).MaxAbsCoeff3() > MinLineLength &&
                                                            f.c0.MaxAbsCoeff3() < MaxPositionMagnitude &&
                                                            f.c1.MaxAbsCoeff3() < MaxPositionMagnitude;
        
        public override Bounds? Bounds => Size == 0 ? null : base.Bounds;

        /// <summary>
        /// Sets the lines with the given list.
        /// </summary>
        /// <param name="lines">The line list.</param>
        /// <param name="overrideNeedsAlpha">A check of alpha colors will be done if <see cref="UseColormap"/> is disabled. Use this to override the check.</param>
        public void Set(NativeList<LineWithColor> lines, bool? overrideNeedsAlpha = null)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            lineBuffer.EnsureCapacity(lines.Length);
            lineBuffer.Clear();
            if (lines.Length != 0)
            {
                foreach (ref readonly LineWithColor t in lines.Ref())
                {
                    if (IsElementValid(t))
                    {
                        lineBuffer.Add(t.f);
                    }
                }
            }

            UpdateLines(overrideNeedsAlpha);
        }

        public void Set(LineWithColor[] lines, bool? overrideNeedsAlpha = null)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            lineBuffer.EnsureCapacity(lines.Length);
            lineBuffer.Clear();
            foreach (ref readonly LineWithColor t in lines.Ref())
            {
                if (IsElementValid(t))
                {
                    lineBuffer.Add(t.f);
                }
            }

            UpdateLines(overrideNeedsAlpha);
        }

        public void Reset()
        {
            lineBuffer.Clear();
            linesNeedAlpha = false;
            UpdateLineMesh();
        }

        /// <summary>
        /// Exposes the line list directly for manual setting.
        /// </summary>
        /// <param name="callback">
        /// A function that receives the internal line list as parameter, and returns true if alpha is needed,
        /// false if not, or null to request a manual check.
        /// </param>
        /// <param name="reserve">The expected number of lines, or 0 if unknown.</param>
        public void SetDirect(Func<NativeList<float4x2>, bool?> callback, int reserve = 0)
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
            UpdateLines(overrideNeedsAlpha);
        }

        void UpdateLines(bool? overrideNeedsAlpha)
        {
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
            if (lineBuffer.Length == 0)
            {
                return false;
            }

            foreach (ref readonly float4x2 t in lineBuffer.Ref())
            {
                Color32 cA = PointWithColor.RecastToColor32(t.c0.w);
                if (cA.a < 255)
                {
                    return true;
                }

                Color32 cB = PointWithColor.RecastToColor32(t.c1.w);
                if (cB.a < 255)
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
            MeshFilter.sharedMesh = Mesh;
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

            Properties.SetFloat(ScaleID, ElementScale);

            Bounds worldBounds = Collider.bounds;

            Material material;
            if (MaterialOverride != null)
            {
                material = MaterialOverride;
            }
            else
            {
                material = UseColormap switch
                {
                    true when !UsesAlpha => Resource.Materials.LineWithColormap.Object,
                    true => Resource.Materials.TransparentLineWithColormap.Object,
                    false when !UsesAlpha => Resource.Materials.Line.Object,
                    false => Resource.Materials.TransparentLine.Object
                };
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
                Properties.SetBuffer(LinesID, (ComputeBuffer?) null);
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
                CalculateBoundsEmpty();
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
                CalculateBoundsEmpty();
                return;
            }

            CapsuleLinesHelper.CreateCapsulesFromSegments(lineBuffer, ElementScale, Mesh);

            CalculateBounds();

            enabled = false;
            MeshRenderer.enabled = true;

            UpdateMeshMaterial();
        }

        Material GetMeshMaterial()
        {
            if (MaterialOverride != null)
            {
                return MaterialOverride;
            }
            
            return UseColormap switch
            {
                true when !UsesAlpha => Resource.Materials.LineSimpleWithColormap.Object,
                true => Resource.Materials.TransparentLineSimpleWithColormap.Object,
                false when !UsesAlpha => Resource.Materials.LineSimple.Object,
                false => Resource.Materials.TransparentLineSimple.Object
            };
        }

        void UpdateMeshMaterial()
        {
            MeshRenderer.sharedMaterial = GetMeshMaterial();
        }

        void CalculateBounds()
        {
            MinMaxJob.CalculateBounds(lineBuffer.AsArray(), Size, out Bounds bounds, out Vector2 span);
            Collider.center = bounds.center;
            Collider.size = bounds.size + ElementScale * Vector3.one;

            MeasuredIntensityBounds = span;
            if (!OverrideIntensityBounds)
            {
                IntensityBounds = span;
            }
        }

        void CalculateBoundsEmpty()
        {
            Collider.center = Vector3.zero;
            Collider.size = Vector3.zero;

            MeasuredIntensityBounds = Vector2.zero;
            if (!OverrideIntensityBounds)
            {
                IntensityBounds = Vector2.zero;
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
                Properties.SetBuffer(LinesID, (ComputeBuffer?) null);
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
            MaterialOverride = null;
            lineBuffer.Clear();

            lineComputeBuffer?.Release();
            lineComputeBuffer = null;
            Properties.SetBuffer(LinesID, (ComputeBuffer?) null);
        }
    }
}