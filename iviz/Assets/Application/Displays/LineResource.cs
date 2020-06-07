using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using Iviz.Resources;
using Iviz.App.Listeners;
using Unity.Mathematics;
using Unity.Collections;

namespace Iviz.Displays
{
    public class LineResource : MarkerResourceWithColormap
    {
        NativeArray<float4x2> lineBuffer = new NativeArray<float4x2>();
        ComputeBuffer lineComputeBuffer;
        ComputeBuffer quadComputeBuffer;

        public Color Color { get; set; } = Color.white;

        static readonly int PropLines = Shader.PropertyToID("_Lines");

        int size_;
        public int Size
        {
            get => size_;
            private set
            {
                if (value == size_)
                {
                    return;
                }
                size_ = value;
                Reserve(size_ * 11 / 10);
            }
        }

        public void Reserve(int reqDataSize)
        {
            if (lineBuffer == null || lineBuffer.Length < reqDataSize)
            {
                if (lineBuffer.Length != 0)
                {
                    lineBuffer.Dispose();
                }
                lineBuffer = new NativeArray<float4x2>(reqDataSize, Allocator.Persistent);

                if (lineComputeBuffer != null)
                {
                    lineComputeBuffer.Release();
                }
                lineComputeBuffer = new ComputeBuffer(lineBuffer.Length, Marshal.SizeOf<LineWithColor>());
                material.SetBuffer(PropLines, lineComputeBuffer);
            }
        }

        public IList<LineWithColor> LinesWithColor
        {
            set
            {
                Size = value.Count;

                int realSize = 0;
                for (int i = 0; i < value.Count; i++)
                {
                    if (value[i].HasNaN)
                    {
                        continue;
                    }
                    lineBuffer[realSize++] = value[i];
                }
                Size = realSize;
                UpdateBuffer();
            }
        }

        public void Set(int size, Action<NativeArray<float4x2>> func)
        {
            Size = size;
            func(lineBuffer);
            UpdateBuffer();
        }

        void UpdateBuffer()
        {
            lineComputeBuffer.SetData(lineBuffer, 0, 0, Size);
            MinMaxJob.CalculateBounds(lineBuffer, Size, out Bounds bounds, out Vector2 span);
            Collider.center = bounds.center;
            Collider.size = bounds.size;
            IntensityBounds = span;
        }

        float scale;
        public float Scale
        {
            get => scale;
            set
            {
                if (scale != value)
                {
                    scale = value;
                    UpdateQuadComputeBuffer();
                }
            }
        }

        public override string Name => "LineResource";

        protected override void Awake()
        {
            base.Awake();

            material = Resource.Materials.Line.Instantiate();
            material.DisableKeyword("USE_TEXTURE");

            Scale = 0.1f;

            UseIntensityTexture = false;
            IntensityBounds = new Vector2(0, 1);
        }

        static readonly int PropQuad = Shader.PropertyToID("_Quad");

        void UpdateQuadComputeBuffer()
        {
            Vector3[] quad = {
                    new Vector3( 0.5f * Scale,  0.5f * Scale, 1),
                    new Vector3( 0.5f * Scale, -0.5f * Scale, 1),
                    new Vector3(-0.5f * Scale, -0.5f * Scale, 0),
                    new Vector3(-0.5f * Scale,  0.5f * Scale, 0),
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
            UpdateTransform();

            Camera camera = TFListener.MainCamera;
            material.SetVector("_Front", transform.InverseTransformDirection(camera.transform.forward));

            Bounds worldBounds = Collider.bounds;
            Graphics.DrawProcedural(material, worldBounds, MeshTopology.Quads, 4, Size);
        }

        protected override void OnDestroy()
        {
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

            UseIntensityTexture = UseIntensityTexture;
            IntensityBounds = IntensityBounds;
            Colormap = Colormap;
        }

        public override void Stop()
        {
            base.Stop();
            Size = 0;
        }
    }
}