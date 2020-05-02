using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Iviz.App
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
    };

    public class PointListResource : MarkerResource
    {
        Material material;
        BoxCollider boxCollider;

        PointWithColor[] pointBuffer = new PointWithColor[0];
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

        int size;
        public int Size
        {
            get => size;
            set
            {
                if (value == size)
                {
                    return;
                }
                size = value;
                int reqDataSize = (int)(size * 1.1f);
                if (pointBuffer == null || pointBuffer.Length < reqDataSize)
                {
                    pointBuffer = new PointWithColor[reqDataSize];

                    if (pointComputeBuffer != null)
                    {
                        pointComputeBuffer.Release();
                    }
                    pointComputeBuffer = new ComputeBuffer(pointBuffer.Length, Marshal.SizeOf<PointWithColor>());
                    material.SetBuffer(PropPoints, pointComputeBuffer);
                }
                Size = size;
            }
        }

        public override void SetColor(Color color)
        {
            // do nothing
        }

        public IEnumerable<Color32> Colors
        {
            get => pointBuffer.Select(x => x.color);
            set
            {
                int index = 0;
                if (value == null || value.Count() == 0)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        pointBuffer[index++].color = Color;
                    }
                }
                else if (value.Count() == pointBuffer.Length)
                {
                    foreach (Color32 color in value)
                    {
                        pointBuffer[index++].color = Color * color;
                    }
                }
                else
                {
                    throw new ArgumentException(
                        "Input color has size " + value.Count() + ", but buffer size is " + pointBuffer.Length,
                        nameof(value));
                }
            }
        }

        public IEnumerable<Vector3> Points
        {
            get => pointBuffer.Select(x => x.position);
            set
            {
                Size = value.Count();

                int index = 0;
                foreach (Vector3 pos in value)
                {
                    pointBuffer[index++].position = pos;
                }
                pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
                Bounds = CalculateBounds();
                boxCollider.center = Bounds.center;
                boxCollider.size = Bounds.size;
            }
        }

        public IList<PointWithColor> PointsWithColor
        {
            get => pointBuffer;
        }

        public void SetPointsWithColor(IList<PointWithColor> points, int size)
        {
            Size = size;
            for (int i = 0; i < size; i++)
            {
                pointBuffer[i] = points[i];
            }
            pointComputeBuffer.SetData(pointBuffer, 0, 0, Size);
            Bounds = CalculateBounds();
            boxCollider.center = Bounds.center;
            boxCollider.size = Bounds.size;
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

        protected override void Awake()
        {
            base.Awake();
            boxCollider = Collider as BoxCollider;

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

            Bounds worldBounds = boxCollider.bounds;
            Graphics.DrawProcedural(material, worldBounds, MeshTopology.Quads, 4, Size);
        }

        Bounds CalculateBounds()
        {
            Vector3 positionMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 positionMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            for (int i = 0; i < Size; i++)
            {
                Vector3 position = pointBuffer[i].position;
                if (float.IsNaN(position.x) ||
                    float.IsNaN(position.y) ||
                    float.IsNaN(position.z))
                {
                    continue;
                }
                positionMin = Vector3.Min(positionMin, position);
                positionMax = Vector3.Max(positionMax, position);
            }
            return new Bounds((positionMax + positionMin) / 2, positionMax - positionMin);
        }

        void OnDestroy()
        {
            if (material != null)
            {
                Destroy(material);
            }
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
            }
            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
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

            Debug.Log("PointCloudListener: Rebuilding compute buffers");
        }
    }
}