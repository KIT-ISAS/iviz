using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App
{
    public class PointListResource : MarkerResource
    {
        Material material;
        BoxCollider boxCollider;

        struct PointWithColor
        {
            public Vector3 position;
            public Color32 color;
        };

        PointWithColor[] pointBuffer = new PointWithColor[0];
        ComputeBuffer pointComputeBuffer;
        ComputeBuffer quadComputeBuffer;
        int dataSize;

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
                if (value == null)
                {
                    for (int i = 0; i < dataSize; i++)
                    {
                        pointBuffer[index++].color = Color;
                    }
                }
                else
                {
                    foreach (Color32 color in value)
                    {
                        pointBuffer[index++].color = Color * color;
                    }
                }
            }
        }

        public IEnumerable<Vector3> Points
        {
            get => pointBuffer.Select(x => x.position);
            set
            {
                int index = 0;
                foreach (Vector3 pos in value)
                {
                    pointBuffer[index++].position = pos;
                }
                pointComputeBuffer.SetData(pointBuffer, 0, 0, dataSize);
                Bounds = CalculateBounds();
                boxCollider.center = Bounds.center;
                boxCollider.size = Bounds.size;
            }
        }

        static readonly int PropPoints = Shader.PropertyToID("_Points");
        public void SetSize(int size)
        {
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
            dataSize = size;
        }

        Vector3 scale;
        public Vector3 Scale
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
            Resource.Materials.Initialize();
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
            Graphics.DrawProcedural(material, worldBounds, MeshTopology.Quads, 4, dataSize);
        }

        Bounds CalculateBounds()
        {
            Vector3 positionMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 positionMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            for (int i = 0; i < dataSize; i++)
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
    }
}