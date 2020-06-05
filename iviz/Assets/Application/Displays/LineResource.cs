using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using Iviz.Resources;
using Iviz.App.Listeners;
using Unity.Mathematics;

namespace Iviz.Displays
{

    public class LineResource : MarkerResource
    {
        Material material;

        LineWithColor[] lineBuffer = Array.Empty<LineWithColor>();
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
                Reserve(size_);
            }
        }

        public void Reserve(int size_)
        {
            int reqDataSize = (int)(size_ * 1.1f);
            if (lineBuffer == null || lineBuffer.Length < reqDataSize)
            {
                lineBuffer = new LineWithColor[reqDataSize];

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
            get => lineBuffer;
            set
            {
                Size = value.Count;
                for (int i = 0; i < Size; i++)
                {
                    lineBuffer[i] = value[i];
                }
                lineComputeBuffer.SetData(lineBuffer, 0, 0, Size);
                CalculateBounds(out Bounds bounds, out Vector2 span);
                Collider.center = bounds.center;
                Collider.size = bounds.size;
                IntensityBounds = span;
            }
        }

        public void Set(int size, Action<LineWithColor[]> func)
        {
            Size = size;
            func(lineBuffer);
            lineComputeBuffer.SetData(lineBuffer, 0, 0, Size);
            CalculateBounds(out Bounds bounds, out Vector2 span);
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

        bool useIntensityTexture;
        public bool UseIntensityTexture
        {
            get => useIntensityTexture;
            set
            {
                useIntensityTexture = value;
                if (useIntensityTexture)
                {
                    material.EnableKeyword("USE_TEXTURE");
                }
                else
                {
                    material.DisableKeyword("USE_TEXTURE");
                }
            }
        }

        static readonly int PropIntensity = Shader.PropertyToID("_IntensityTexture");

        Resource.ColormapId colormap;
        public Resource.ColormapId Colormap
        {
            get => colormap;
            set
            {
                colormap = value;

                Texture2D texture = Resource.Colormaps.Textures[Colormap];
                material.SetTexture(PropIntensity, texture);
            }
        }

        static readonly int PropIntensityCoeff = Shader.PropertyToID("_IntensityCoeff");
        static readonly int PropIntensityAdd = Shader.PropertyToID("_IntensityAdd");

        Vector2 intensityBounds;
        public Vector2 IntensityBounds
        {
            get => intensityBounds;
            set
            {
                intensityBounds = value;
                float intensitySpan = intensityBounds.y - intensityBounds.x;

                if (intensitySpan == 0)
                {
                    material.SetFloat(PropIntensityCoeff, 1);
                    material.SetFloat(PropIntensityAdd, 0);
                }
                else
                {
                    if (FlipMinMax)
                    {
                        material.SetFloat(PropIntensityCoeff, 1 / intensitySpan);
                        material.SetFloat(PropIntensityAdd, -intensityBounds.x / intensitySpan);
                    }
                    else
                    {
                        material.SetFloat(PropIntensityCoeff, -1 / intensitySpan);
                        material.SetFloat(PropIntensityAdd, intensityBounds.y / intensitySpan);
                    }
                }
            }
        }

        bool flipMinMax;
        public bool FlipMinMax
        {
            get => flipMinMax;
            set
            {
                flipMinMax = value;
                IntensityBounds = IntensityBounds;
            }
        }

        public override bool Visible
        {
            get => base.Visible;
            set
            {
                base.Visible = value;
                if (value)
                {
                    OnApplicationFocus(true);
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

        static readonly int PropLocalToWorld = Shader.PropertyToID("_LocalToWorld");
        static readonly int PropWorldToLocal = Shader.PropertyToID("_WorldToLocal");

        void Update()
        {
            material.SetMatrix(PropLocalToWorld, transform.localToWorldMatrix);
            material.SetMatrix(PropWorldToLocal, transform.worldToLocalMatrix);

            Camera camera = TFListener.MainCamera;
            material.SetVector("_Front", transform.InverseTransformDirection(camera.transform.forward));

            Bounds worldBounds = Collider.bounds;
            Graphics.DrawProcedural(material, worldBounds, MeshTopology.Quads, 4, Size);
        }

        void CalculateBounds(out Bounds bounds, out Vector2 intensityBounds)
        {
            float4 min = new float4(float.MaxValue);
            float4 max = new float4(float.MinValue);
            for (int i = 0; i < Size; i++)
            {
                float4 pA = lineBuffer[i].PA;
                if (math.any(math.isnan(pA)))
                {
                    continue;
                }
                min = math.min(min, pA);
                max = math.max(max, pA);

                float4 pB = lineBuffer[i].PB;
                if (math.any(math.isnan(pB)))
                {
                    continue;
                }
                min = math.min(min, pB);
                max = math.max(max, pB);
            }

            Vector3 positionMin = new Vector3(min.x, min.y, min.z);
            Vector3 positionMax = new Vector3(max.x, max.y, max.z);

            bounds = new Bounds((positionMax + positionMin) / 2, positionMax - positionMin);
            intensityBounds = new Vector2(min.w, max.w);
        }

        void OnDestroy()
        {
            if (material != null)
            {
                Destroy(material);
            }
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

        void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                return;
            }
            // unity bug causes all compute buffers to disappear when focus is lost
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

            //Debug.Log("LineResource: Rebuilding compute buffers");
        }

        public override void Stop()
        {
            base.Stop();
            Size = 0;
        }
    }
}