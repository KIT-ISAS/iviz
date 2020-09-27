using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using Iviz.Controllers;
using Iviz.Resources;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine.Rendering;

namespace Iviz.Displays
{
    public sealed class LineResource : MarkerResourceWithColormap
    {
        static readonly int LinesID = Shader.PropertyToID("_Lines");
        static readonly int FrontID = Shader.PropertyToID("_Front");

        NativeArray<float4x2> lineBuffer;
        ComputeBuffer lineComputeBuffer; 

        bool useAlpha;

        [SerializeField] int size;

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
            properties.SetBuffer(LinesID, lineComputeBuffer);
        }

        bool linesNeedAlpha;

        public IList<LineWithColor> LinesWithColor
        {
            set
            {
                Size = value.Count;

                linesNeedAlpha = false;
                int realSize = 0;
                foreach (var t in value)
                {
                    if (t.HasNaN)
                    {
                        continue;
                    }

                    lineBuffer[realSize++] = t;
                    linesNeedAlpha |= t.ColorA.a < 255 || t.ColorB.a < 255;
                }

                Size = realSize;
                useAlpha = linesNeedAlpha || Tint.a <= 254f / 255f;
                UpdateBuffer();
            }
        }

        public override Color Tint
        {
            get => base.Tint;
            set
            {
                base.Tint = value;
                useAlpha = linesNeedAlpha || Tint.a <= 254f / 255f;
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
            boxCollider.center = bounds.center;
            boxCollider.size = bounds.size + ElementSize * Vector3.one;
            IntensityBounds = span;
        }

        protected override void Awake()
        {
            properties = new MaterialPropertyBlock();

            base.Awake();

            ElementSize = 0.1f;
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

            Camera mainCamera = TFListener.MainCamera;
            properties.SetVector(FrontID, transform.InverseTransformPoint(mainCamera.transform.position));

            Bounds worldBounds = boxCollider.bounds;

            Material newMaterial = null;
            switch (useAlpha)
            {
                case true when UseColormap:
                    newMaterial = Resource.Materials.TransparentLineWithColormap.Object;
                    break;
                case true when !UseColormap:
                    newMaterial = Resource.Materials.TransparentLine.Object;
                    break;
                case false when UseColormap:
                    newMaterial = Resource.Materials.LineWithColormap.Object;
                    break;
                case false when !UseColormap:
                    newMaterial = Resource.Materials.Line.Object;
                    break;
            }
            
            Graphics.DrawProcedural(newMaterial, worldBounds, MeshTopology.Quads, 2 * 4, Size,
                null, properties, ShadowCastingMode.Off, false, gameObject.layer);
        }

        void OnDestroy()
        {
            if (lineComputeBuffer != null)
            {
                lineComputeBuffer.Release();
                lineComputeBuffer = null;
                properties.SetBuffer(LinesID, null);
            }

            if (lineBuffer.Length > 0)
            {
                lineBuffer.Dispose();
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
                properties.SetBuffer(LinesID, lineComputeBuffer);
            }

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