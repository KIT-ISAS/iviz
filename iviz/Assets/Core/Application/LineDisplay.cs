#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using Unity.Burst;
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
    public sealed class LineDisplay : MarkerDisplayWithColormap
    {
        public enum LineRenderType
        {
            Auto,
            AlwaysCapsule,
            AlwaysBillboard,
        }

        static int? maxSegmentsForMesh;

        static int MaxSegmentsForMesh => maxSegmentsForMesh ??
                                         (maxSegmentsForMesh = Settings.SupportsComputeBuffers ? 30 : int.MaxValue)
                                         .Value;

        const float MinLineLength = 1E-06f;
        const float MaxPositionMagnitude = 1e3f;

        readonly NativeList<float4x2> lineBuffer = new();
        ComputeBuffer? lineComputeBuffer;
        Mesh? mesh;
        Material[]? cachedMaterials;
        Material[]? cachedMeshMaterials;
        bool linesNeedAlpha;

        [SerializeField] MeshRenderer? meshRenderer;
        [SerializeField] MeshFilter? meshFilter;


        Mesh Mesh => mesh != null ? mesh : (mesh = new Mesh { name = "Line Capsules" });
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
                    UpdateProperties();
                }
            }
        }

        int Size => lineBuffer.Length;

        bool UseCapsuleLines => !Settings.SupportsComputeBuffers ||
                                (RenderType == LineRenderType.Auto && Size <= MaxSegmentsForMesh) ||
                                RenderType == LineRenderType.AlwaysCapsule;

        public LineRenderType RenderType { get; set; }

        public override Bounds? Bounds => Size == 0 ? null : base.Bounds;

        /// <summary>
        /// Sets the lines with the given list.
        /// </summary>
        /// <param name="lines">The line list.</param>
        /// <param name="overrideNeedsAlpha">
        /// A check of alpha colors will be done if <see cref="UseColormap"/> is disabled.
        /// Use this to override the check.
        /// </param>
        public void Set(ReadOnlySpan<LineWithColor> lines, bool? overrideNeedsAlpha = null)
        {
            Set(MemoryMarshal.Cast<LineWithColor, float4x2>(lines), overrideNeedsAlpha);
        }

        public void Set(ReadOnlySpan<float4x2> lines, bool? needsAlpha)
        {
            lineBuffer.Clear();
            lineBuffer.AddRange(lines);
            UpdateLines(needsAlpha);
        }

        public void Set(Action<NativeList<float4x2>> callback, int reserve, bool? needsAlpha)
        {
            ThrowHelper.ThrowIfNull(callback, nameof(callback));
            lineBuffer.EnsureCapacity(reserve);
            lineBuffer.Clear();
            callback(lineBuffer);
            UpdateLines(needsAlpha);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsElementValid(in float4x2 f)
        {
            if (f.c0.x.IsInvalid() || Mathf.Abs(f.c0.x) > MaxPositionMagnitude) return false;
            if (f.c1.x.IsInvalid() || Mathf.Abs(f.c1.x) > MaxPositionMagnitude) return false;
            bool lengthIsValid = Mathf.Abs(f.c0.x - f.c1.x) > MinLineLength;

            if (f.c0.y.IsInvalid() || Mathf.Abs(f.c0.y) > MaxPositionMagnitude) return false;
            if (f.c1.y.IsInvalid() || Mathf.Abs(f.c1.y) > MaxPositionMagnitude) return false;
            lengthIsValid = lengthIsValid || Mathf.Abs(f.c0.y - f.c1.y) > MinLineLength;

            if (f.c0.z.IsInvalid() || Mathf.Abs(f.c0.z) > MaxPositionMagnitude) return false;
            if (f.c1.z.IsInvalid() || Mathf.Abs(f.c1.z) > MaxPositionMagnitude) return false;
            return lengthIsValid || Mathf.Abs(f.c0.z - f.c1.z) > MinLineLength;
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
        public void SetDirect(Func<NativeList<float4x2>, bool?> callback, int reserve)
        {
            ThrowHelper.ThrowIfNull(callback, nameof(callback));

            lineBuffer.EnsureCapacity(reserve);
            lineBuffer.Clear();

            bool? overrideNeedsAlpha = callback(lineBuffer);
            UpdateLines(overrideNeedsAlpha);
        }

        void UpdateLines(bool? needsAlpha)
        {
            linesNeedAlpha = !UseColormap && (needsAlpha ?? CheckIfAlphaNeeded());

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
            int bufferLength = lineBuffer.Length;
            if (bufferLength == 0)
            {
                return false;
            }

            unsafe
            {
                return LineDisplayUtils.CheckIfAlphaNeeded(lineBuffer.GetUnsafePtr(), bufferLength);
            }

            /*
            for (int i = 0; i < lineSpan.Length; i++)
            {
                ref float4x2 line = ref lineSpan[i];
                uint cA = Unsafe.As<float, uint>(ref line.c0.w);
                if (cA >> 24 < 255)
                {
                    return true;
                }

                uint cB = Unsafe.As<float, uint>(ref line.c1.w);
                if (cB >> 24 < 255)
                {
                    return true;
                }
            }

            return false;
            */
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
            protected get => base.ElementScale;
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
            Mesh.MarkDynamic();
            MeshFilter.sharedMesh = Mesh;

            if (Settings.SupportsComputeBuffers)
            {
                lineComputeBuffer = new ComputeBuffer(1, Unsafe.SizeOf<float4x2>());
                Properties.SetBuffer(ShaderIds.LinesId, lineComputeBuffer);
            }

            MeshRenderer.SetPropertyBlock(Properties);

            base.Awake();

            //ElementScale = 0.1f;
            UseColormap = false;
        }

        void Update()
        {
            if (Size == 0)
            {
                return;
            }

            UpdateTransform();

            Properties.SetFloat(ShaderIds.ScaleId, ElementScale);

            var worldBounds = Collider.bounds;
            var material = GetMaterial();

            Graphics.DrawProcedural(material, worldBounds, MeshTopology.Quads, 2 * 4, Size,
                null, Properties, ShadowCastingMode.Off, false, gameObject.layer);
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
                Properties.SetBuffer(ShaderIds.LinesId, (ComputeBuffer?)null);
                lineComputeBuffer?.Release();
                lineComputeBuffer = new ComputeBuffer(lineBuffer.Capacity, Unsafe.SizeOf<float4x2>());
                Properties.SetBuffer(ShaderIds.LinesId, lineComputeBuffer);
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
                Mesh.Clear();
                CalculateBoundsEmpty();
                return;
            }

            CapsuleLinesHelper.CreateCapsulesFromSegments(lineBuffer, ElementScale, Mesh);

            CalculateBounds();

            enabled = false;
            MeshRenderer.enabled = true;

            UpdateMeshMaterial();
        }

        Material GetMaterial()
        {
            if (materialOverride != null)
            {
                return materialOverride;
            }

            cachedMaterials ??= new[]
            {
                Resource.Materials.LineWithColormap.Object,
                Resource.Materials.TransparentLineWithColormap.Object,
                Resource.Materials.Line.Object,
                Resource.Materials.TransparentLine.Object
            };

            int index = GetMaterialIndex();
            return cachedMaterials[index];
        }

        Material GetMeshMaterial()
        {
            if (materialOverride != null)
            {
                return materialOverride;
            }

            cachedMeshMaterials ??= new[]
            {
                Resource.Materials.LineSimpleWithColormap.Object,
                Resource.Materials.TransparentLineSimpleWithColormap.Object,
                Resource.Materials.LineSimple.Object,
                Resource.Materials.TransparentLineSimple.Object
            };

            int index = GetMaterialIndex();
            return cachedMeshMaterials[index];
        }


        /*
        int GetMaterialIndex() => (base.UseColormap, UsesAlpha) switch
        {
            (true, false) => 0,
            (true, true) => 1,
            (false, false) => 2,
            _ => 3
        };
        */

        int GetMaterialIndex() => (base.UseColormap ? 0 : 2) | (UsesAlpha ? 1 : 0);

        void UpdateMeshMaterial()
        {
            MeshRenderer.sharedMaterial = GetMeshMaterial();
        }

        void CalculateBounds()
        {
            MinMaxJobs.CalculateBounds(lineBuffer, out Bounds bounds, out Vector2 span);
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
            Collider.SetLocalBounds(default);

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
                Properties.SetBuffer(ShaderIds.LinesId, (ComputeBuffer?)null);
            }

            if (lineBuffer.Capacity != 0)
            {
                lineComputeBuffer = new ComputeBuffer(lineBuffer.Capacity, Unsafe.SizeOf<float4x2>());
                lineComputeBuffer.SetData(lineBuffer.AsArray(), 0, 0, Size);
                Properties.SetBuffer(ShaderIds.LinesId, lineComputeBuffer);
            }

            IntensityBounds = IntensityBounds;
            Colormap = Colormap;
            Tint = Tint;
        }

        public override string ToString() => $"[{nameof(LineDisplay)}]";

        public override void Suspend()
        {
            base.Suspend();
            Mesh.Clear();
            MeshRenderer.enabled = false;
            MaterialOverride = null;

            lineBuffer.Reset();

            Properties.SetBuffer(ShaderIds.LinesId, (ComputeBuffer?)null);

            lineComputeBuffer?.Release();
            lineComputeBuffer = null;
        }

        void OnDestroy()
        {
            lineBuffer.Dispose();

            Properties.SetBuffer(ShaderIds.LinesId, (ComputeBuffer?)null);

            if (lineComputeBuffer != null)
            {
                lineComputeBuffer.Release();
                lineComputeBuffer = null;
            }

            Mesh.Clear();
            Destroy(Mesh);
        }
    }
}