using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class MeshListResource : MarkerResource
    {
        Material material;
        BoxCollider boxCollider;
        Bounds baseBounds;

        struct PointWithColor
        {
            public Vector3 position;
            public Color32 color;
        };

        PointWithColor[] pointBuffer;
        int pointBufferSize;
        ComputeBuffer pointComputeBuffer;
        
        readonly uint[] argsBuffer = new uint[5] { 0, 0, 0, 0, 0 };
        ComputeBuffer argsComputeBuffer;

        Mesh mesh;
        public Mesh Mesh
        {
            get => mesh;
            set
            {
                mesh = value;
                argsBuffer[0] = mesh.GetIndexCount(0);
                argsBuffer[2] = mesh.GetIndexStart(0);
                argsBuffer[3] = mesh.GetBaseVertex(0);
            }
        }

        public Color Color { get; set; } = Color.white;

        static readonly int PropPoints = Shader.PropertyToID("_Points");
        void SetCapacity(int reqDataSize)
        {
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
        }

        public IEnumerable<Color32> Colors
        {
            get => pointBuffer.Select(x => x.color);
            set
            {
                int index = 0;
                if (value == null)
                {
                    for (int i = 0; i < pointBufferSize; i++)
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

                pointComputeBuffer.SetData(pointBuffer, 0, 0, pointBufferSize);
                UpdateBounds();
            }
        }



        static readonly int PropLocalScale = Shader.PropertyToID("_LocalScale");

        Vector3 scale;
        public Vector3 Scale
        {
            get => scale;
            set
            {
                scale = value;
                material.SetVector(PropLocalScale, new Vector4(scale.x, scale.y, scale.z, 1));
            }
        }

        public override string Name => "MeshListResource";

        void UpdateBounds()
        {
            baseBounds = CalculateBounds();

            boxCollider.center = Bounds.center;
            boxCollider.size = Bounds.size;
        }

        protected override void Awake()
        {
            base.Awake();

            material = Instantiate(Resource.Materials.MeshList);
            material.enableInstancing = true;

            Mesh = Resource.Markers.SphereSimple.GameObject.GetComponent<MeshFilter>().sharedMesh;

            boxCollider = Collider as BoxCollider;

            SetCapacity(10);
        }

        public void SetSize(int size)
        {
            int reqDataSize = (int)(size * 1.1f);
            SetCapacity(reqDataSize);

            if (argsComputeBuffer == null)
            {
                argsComputeBuffer = new ComputeBuffer(1, argsBuffer.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
            }

            argsBuffer[1] = (uint)size;
            argsComputeBuffer.SetData(argsBuffer);

            pointBufferSize = size;
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

            Scale = 0.15f * Vector3.one;
            Colors = null;
            Color = Color.red;
            Points = points;
        }
        */

        static readonly int PropLocalToWorld = Shader.PropertyToID("_LocalToWorld");

        void Update()
        {
            material.SetMatrix(PropLocalToWorld, transform.localToWorldMatrix);

            if (pointBufferSize == 0)
            {
                return;
            }
            Bounds worldBounds = boxCollider.bounds;
            material.SetVector("_BoundaryCenter", worldBounds.center);
            Graphics.DrawMeshInstancedIndirect(mesh, 0, material, worldBounds, argsComputeBuffer);
        }

        Bounds CalculateBounds()
        {
            Vector3 positionMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 positionMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            for (int i = 0; i < pointBufferSize; i++)
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
            if (argsComputeBuffer != null)
            {
                argsComputeBuffer.Release();
            }
        }



        void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                return;
            }

            material.SetBuffer(PropPoints, pointComputeBuffer);
            Debug.Log("MeshList: Rebuilding compute buffers");
        }
    }
}