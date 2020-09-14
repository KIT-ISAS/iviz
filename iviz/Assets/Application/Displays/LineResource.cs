using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using Iviz.Controllers;
using Iviz.Resources;
using Unity.Mathematics;
using Unity.Collections;

namespace Iviz.Displays
{
    public sealed class LineResource : MarkerResourceWithColormap
    {
        static readonly int PropLines = Shader.PropertyToID("_Lines");
        static readonly int PropFront = Shader.PropertyToID("_Front");
        static readonly int PropQuad = Shader.PropertyToID("_Quad");

        NativeArray<float4x2> lineBuffer;
        ComputeBuffer lineComputeBuffer;
        ComputeBuffer quadComputeBuffer;

        Material materialAlpha;
        Material materialNoAlpha;
        public bool UseAlpha
        {
            get => material == materialAlpha;
            private set
            {
                if (UseAlpha == value)
                {
                    return;
                }

                if (value)
                {
                    material = materialAlpha;
                }
                else
                {
                    if (materialNoAlpha == null)
                    {
                        materialNoAlpha = Resource.Materials.Line.Instantiate();
                        materialNoAlpha.DisableKeyword("USE_TEXTURE");
                    }

                    material = materialNoAlpha;
                }

                Rebuild();
            }
        }

        int size;

        public int Size
        {
            get => size;
            private set
            {
                if (value == size)
                {
                    return;
                }

                size = value;
                Reserve(size * 11 / 10);
            }
        }

        public void Reserve(int reqDataSize)
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
            material.SetBuffer(PropLines, lineComputeBuffer);
        }

        public IList<LineWithColor> LinesWithColor
        {
            set
            {
                Size = value.Count;

                bool needsAlpha = false;
                int realSize = 0;
                foreach (var t in value)
                {
                    if (t.HasNaN)
                    {
                        continue;
                    }

                    lineBuffer[realSize++] = t;
                    needsAlpha |= t.ColorA.a < 255 || t.ColorB.a < 255;
                }

                Size = realSize;
                UseAlpha = needsAlpha; 
                UpdateBuffer();
            }
        }

        public void Set(int newSize, Action<NativeArray<float4x2>> func)
        {
            Size = newSize;
            func(lineBuffer);
            UpdateBuffer();
        }

        void UpdateBuffer()
        {
            if (Size == 0)
            {
                return;
            }

            lineComputeBuffer.SetData(lineBuffer, 0, 0, Size);
            MinMaxJob.CalculateBounds(lineBuffer, Size, out Bounds bounds, out Vector2 span);
            Collider.center = bounds.center;
            Collider.size = bounds.size + LineScale * Vector3.one;
            IntensityBounds = span;
        }

        [SerializeField] float lineScale;
        public float LineScale
        {
            get => lineScale;
            set
            {
                if (Mathf.Approximately(lineScale, value))
                {
                    return;
                }

                lineScale = value;
                UpdateQuadComputeBuffer();
            }
        }

        protected override void Awake()
        {
            materialAlpha = Resource.Materials.TransparentLine.Instantiate();

            material = materialAlpha;
            material.DisableKeyword("USE_TEXTURE");

            base.Awake();

            LineScale = 0.1f;
            UseIntensityTexture = false;
            IntensityBounds = new Vector2(0, 1);
        }

        void UpdateQuadComputeBuffer()
        {
            Vector3[] quad =
            {
                new Vector3(0.1f * LineScale, 0.5f * LineScale, 1),
                new Vector3(0.1f * LineScale, -0.5f * LineScale, 1),
                new Vector3(-0.1f * LineScale, -0.5f * LineScale, 0),
                new Vector3(-0.1f * LineScale, 0.5f * LineScale, 0),
            };
            if (quadComputeBuffer == null)
            {
                quadComputeBuffer = new ComputeBuffer(4, Marshal.SizeOf<Vector3>());
                material.SetBuffer(PropQuad, quadComputeBuffer);
            }

            quadComputeBuffer.SetData(quad, 0, 0, 4);
        }

        void Update()
        {
            if (Size == 0)
            {
                return;
            }

            UpdateTransform();

            Camera mainCamera = TFListener.MainCamera;
            //material.SetVector(PropFront, transform.InverseTransformDirection(camera.transform.forward));
            material.SetVector(PropFront, transform.InverseTransformPoint(mainCamera.transform.position));

            Bounds worldBounds = Collider.bounds;
            Graphics.DrawProcedural(material, worldBounds, MeshTopology.Quads, 4, Size);
        }

        protected override void OnDestroy()
        {
            material = null;
            base.OnDestroy();

            if (lineComputeBuffer != null)
            {
                lineComputeBuffer.Release();
                lineComputeBuffer = null;
            }

            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
                quadComputeBuffer = null;
            }

            if (lineBuffer.Length > 0)
            {
                lineBuffer.Dispose();
            }

            if (materialAlpha != null)
            {
                Destroy(materialAlpha);
            }

            if (materialNoAlpha != null)
            {
                Destroy(materialNoAlpha);
            }
        }

        protected override void Rebuild()
        {
            if (lineComputeBuffer != null)
            {
                lineComputeBuffer.Release();
                lineComputeBuffer = null;
            }

            if (lineBuffer.Length != 0)
            {
                lineComputeBuffer = new ComputeBuffer(lineBuffer.Length, Marshal.SizeOf<LineWithColor>());
                lineComputeBuffer.SetData(lineBuffer, 0, 0, Size);
                material.SetBuffer(PropLines, lineComputeBuffer);
            }

            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
                quadComputeBuffer = null;
            }

            UpdateQuadComputeBuffer();

            UpdateMaterialKeywords();
            IntensityBounds = IntensityBounds;
            Colormap = Colormap;
            Tint = Tint;
        }

        public override void Suspend()
        {
            base.Suspend();
            Size = 0;
        }
    }
}