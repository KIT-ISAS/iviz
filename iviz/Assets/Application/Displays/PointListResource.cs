using System.Collections.Generic;
using System.Runtime.InteropServices;
using Iviz.App;
using Iviz.Resources;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    [StructLayout(LayoutKind.Explicit)]
    public struct PointWithColor
    {
        [FieldOffset(0)] public Vector3 position;
        [FieldOffset(12)] public Color32 color;
        [FieldOffset(12)] public float intensity;

        public PointWithColor(Vector3 position, Color32 color) : this()
        {
            this.position = position;
            this.color = color;
        }

        public PointWithColor(Vector3 position, float intensity) : this()
        {
            this.position = position;
            this.intensity = intensity;
        }

        public static implicit operator float4(PointWithColor c) =>
            new float4(c.position.x, c.position.y, c.position.z, c.intensity);
    };

    public class PointListResource : MarkerResource
    {
        Material material;

        //PointWithColor[] pointBuffer = new PointWithColor[0];
        NativeArray<float4> pointBuffer = new NativeArray<float4>();
        ComputeBuffer pointComputeBuffer;
        ComputeBuffer quadComputeBuffer;

        bool useIntensityTexture;
        public bool UseIntensityTexture
        {
            get => useIntensityTexture;
            set
            {
                if (useIntensityTexture == value)
                {
                    return;
                }
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
                    material.SetFloat(PropIntensityCoeff, 1 / intensitySpan);
                    material.SetFloat(PropIntensityAdd, -intensityBounds.x / intensitySpan);
                }
            }
        }

        static readonly int PropPoints = Shader.PropertyToID("_Points");

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
                int reqDataSize = (int)(size_ * 1.1f);
                if (pointBuffer == null || pointBuffer.Length < reqDataSize)
                {
                    //pointBuffer = new PointWithColor[reqDataSize];
                    pointBuffer.Dispose();
                    pointBuffer = new NativeArray<float4>(reqDataSize, Allocator.Persistent);

                    if (pointComputeBuffer != null)
                    {
                        pointComputeBuffer.Release();
                    }
                    pointComputeBuffer = new ComputeBuffer(pointBuffer.Length, Marshal.SizeOf<PointWithColor>());
                    material.SetBuffer(PropPoints, pointComputeBuffer);
                }
            }
        }
        public IList<PointWithColor> PointsWithColor
        {
            set
            {
                Size = value.Count;

                int realSize = 0;
                for (int i = 0; i < value.Count; i++)
                {
                    if (float.IsNaN(value[i].position.x) ||
                        float.IsNaN(value[i].position.y) ||
                        float.IsNaN(value[i].position.z))
                    {
                        continue;
                    }
                    pointBuffer[realSize++] = value[i];
                }
                Size = realSize;
                UpdateBuffer();
            }
        }

        /*
        public void Set(IList<Vector3> points)
        {
            Size = points.Count;
            for (int i = 0; i < Size; i++)
            {
                pointBuffer[i] = new PointWithColor(points[i], Color.white);
                //pointBuffer[i].position = points[i];
                //pointBuffer[i].color = Color.white;
            }
            UpdateBuffer();
        }
        */

        /*
        public void Set(IList<PointWithColor> points)
        {
            Size = points.Count;
            for (int i = 0; i < Size; i++)
            {
                pointBuffer[i] = points[i];
            }
            UpdateBuffer();
        }
        */

        public void Set(IList<Vector3> points, IList<Color> colors = null)
        {
            Size = points.Count;
            int realSize = 0;
            if (colors == null || points.Count != colors.Count)
            {
                PointWithColor pc = new PointWithColor();
                pc.color = Color.white;
                for (int i = 0; i < points.Count; i++)
                {
                    if (float.IsNaN(points[i].x) ||
                        float.IsNaN(points[i].y) ||
                        float.IsNaN(points[i].z))
                    {
                        continue;
                    }
                    pc.position = points[i];
                    pointBuffer[realSize++] = pc;
                }
            }
            else
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (float.IsNaN(points[i].x) ||
                        float.IsNaN(points[i].y) ||
                        float.IsNaN(points[i].z))
                    {
                        continue;
                    }
                    pointBuffer[realSize++] = new PointWithColor(points[i], colors[i]);
                }
            }
            Size = realSize;
            UpdateBuffer();
        }

        void UpdateBuffer()
        {
            pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
            Bounds bounds = CalculateBounds();
            Collider.center = bounds.center;
            Collider.size = bounds.size;
        }

        Vector2 scale;
        public Vector2 Scale
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

        public override string Name => "PointList";

        protected override void Awake()
        {
            base.Awake();

            material = Instantiate(Resource.Materials.PointCloud);
            material.DisableKeyword("USE_TEXTURE");
        }

        static readonly int PropQuad = Shader.PropertyToID("_Quad");

        void UpdateQuadComputeBuffer()
        {
            Vector2[] quad = {
                    Vector2.Scale(new Vector2( 0.5f,  0.5f), Scale),
                    Vector2.Scale(new Vector2( 0.5f, -0.5f), Scale),
                    Vector2.Scale(new Vector2(-0.5f, -0.5f), Scale),
                    Vector2.Scale(new Vector2(-0.5f,  0.5f), Scale),
            };
            if (quadComputeBuffer == null)
            {
                quadComputeBuffer = new ComputeBuffer(4, Marshal.SizeOf<Vector2>());
                material.SetBuffer(PropQuad, quadComputeBuffer);
            }
            quadComputeBuffer.SetData(quad, 0, 0, 4);
        }


        /*
        void Start()
        {
            List<Vector3> points = new List<Vector3>();
            for (int i = 0; i < 20; i++)
            {
                points.Add(new Vector3(i, 0, i));
            }
            SetSize(points.Count);

            Scale = 1.0f * Vector3.one;
            Color = Color.red;
            Colors = null;
            Points = points;
        }
        */

        static readonly int PropLocalToWorld = Shader.PropertyToID("_LocalToWorld");
        static readonly int PropWorldToLocal = Shader.PropertyToID("_WorldToLocal");

        void Update()
        {
            material.SetMatrix(PropLocalToWorld, transform.localToWorldMatrix);
            material.SetMatrix(PropWorldToLocal, transform.worldToLocalMatrix);

            Bounds worldBounds = Collider.bounds;
            Graphics.DrawProcedural(material, worldBounds, MeshTopology.Quads, 4, Size);
        }

        [BurstCompile(CompileSynchronously = true)]
        private struct MinMaxJob : IJob
        {
            [ReadOnly]
            public NativeArray<float4> Input;

            [WriteOnly]
            public float4 Min;
            [WriteOnly]
            public float4 Max;

            public void Execute()
            {
                Min = new float4(float.MaxValue);
                Max = new float4(float.MinValue);
                for (int i = 0; i < Input.Length; i++)
                {
                    Min = math.min(Min, Input[i]);
                    Max = math.max(Max, Input[i]);
                }
            }
        }

        Bounds CalculateBounds()
        {
            /*
            Vector3 positionMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 positionMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            for (int i = 0; i < Size; i++)
            {
                //Vector3 position = pointBuffer[i].position;
                Vector3 position = new Vector3();
                if (float.IsNaN(position.x) ||
                    float.IsNaN(position.y) ||
                    float.IsNaN(position.z))
                {
                    continue;
                }
                positionMin = Vector3.Min(positionMin, position);
                positionMax = Vector3.Max(positionMax, position);
            }
            */
            var job = new MinMaxJob
            {
                Input = pointBuffer
            };
            job.Schedule().Complete();

            Vector3 positionMin = new Vector3(job.Min.x, job.Min.y, job.Min.z);
            Vector3 positionMax = new Vector3(job.Max.x, job.Max.y, job.Max.z);

            return new Bounds((positionMax + positionMin) / 2, positionMax - positionMin);
        }

        /*
        [BurstCompile(CompileSynchronously = true)]
        private struct ClampJob : IJob
        {
            [ReadOnly]
            public NativeArray<float4> Input;
            [ReadOnly]
            public float Min;
            [ReadOnly]
            public float Max;

            [WriteOnly]
            public NativeArray<float4> Output;

            public void Execute()
            {
                float scale = 1 / (Max - Min);

                for (int i = 0; i < Input.Length; i++)
                {
                    float4 inf = Input[i];
                    float4 outf = inf * new float4(1, 1, 1, scale);
                    Output[i] = outf;
                }
            }
        }
        */

        void OnDestroy()
        {
            if (material != null)
            {
                Destroy(material);
            }
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
            }
            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
                quadComputeBuffer = null;
            }
            if (pointBuffer != null)
            {
                pointBuffer.Dispose();
            }
        }

        void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                return;
            }
            // unity bug causes all compute buffers to disappear when focus is lost
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
            }
            if (pointBuffer.Length != 0)
            {
                pointComputeBuffer = new ComputeBuffer(pointBuffer.Length, Marshal.SizeOf<PointWithColor>());
                pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
                material.SetBuffer(PropPoints, pointComputeBuffer);
            }

            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
                quadComputeBuffer = null;
            }
            UpdateQuadComputeBuffer();

            //Debug.Log("PointCloudListener: Rebuilding compute buffers");
        }
    }
}