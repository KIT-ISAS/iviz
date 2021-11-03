using System.Collections.Generic;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Application.Displays
{
    public class RoundedPlaneResource : MonoBehaviour, IDisplay, ISupportsTint, ISupportsAROcclusion, ISupportsPbr
    {
        static Mesh cornerMesh;
        static Mesh quadMesh;

        [NotNull] static Mesh QuadMesh => quadMesh != null ? quadMesh : (quadMesh = GetQuadMesh());
        [NotNull] static Mesh CornerMesh => cornerMesh != null ? cornerMesh : (cornerMesh = CreateCornerMesh());

        Vector2 size;
        float radius;
        MeshMarkerResource[] objects;

        [CanBeNull] Transform mTransform;
        [NotNull] public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        [NotNull]
        static Mesh GetQuadMesh() => Resource.Displays.Square.Object.GetComponent<MeshMarkerResource>().Mesh;

        [NotNull]
        static Mesh CreateCornerMesh()
        {
            var vertices = new List<Vector3>
            {
                Vector3.zero,
                new Vector3(1, 0, 0)
            };

            const int numVertices = 10;
            for (int i = 1; i < numVertices; i++)
            {
                float angle = Mathf.PI / 2 * i / numVertices;
                vertices.Add(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));
            }

            vertices.Add(new Vector3(0,  0,1));

            var indices = new List<int>();
            for (int i = 0; i < numVertices; i++)
            {
                indices.Add(i + 1);
                indices.Add(0);
                indices.Add(i + 2);
            }

            Mesh mesh = new Mesh
            {
                name = "Corner Mesh",
                vertices = vertices.ToArray(),
                triangles = indices.ToArray()
            };
            mesh.RecalculateNormals();
            return mesh;
        }

        public Vector2 Size
        {
            get => size;
            set
            {
                if (Mathf.Approximately(value.x, size.x) && Mathf.Approximately(value.y, size.y))
                {
                    return;
                }

                size = value;
                Rebuild();
            }
        }

        public float Radius
        {
            get => radius;
            set
            {
                if (Mathf.Approximately(value, radius))
                {
                    return;
                }

                radius = value;
                Rebuild();
            }
        }
        
        public Color EmissiveColor
        {
            get => objects[0].EmissiveColor;
            set
            {
                foreach (var resource in objects)
                {
                    resource.EmissiveColor = value;
                }
            }
        }

        public Color Color
        {
            get => objects[0].Color;
            set
            {
                foreach (var resource in objects)
                {
                    resource.Color = value;
                }
            }
        }

        public Color Tint
        {
            get => objects[0].Tint;
            set
            {
                foreach (var resource in objects)
                {
                    resource.Tint = value;
                }
            }
        }
        
        public float Smoothness
        {
            get => objects[0].Smoothness;
            set
            {
                foreach (var resource in objects)
                {
                    resource.Smoothness = value;
                }
            }
        }

        public float Metallic
        {
            get => objects[0].Metallic;
            set
            {
                foreach (var resource in objects)
                {
                    resource.Metallic = value;
                }
            }
        }

        public bool ShadowsEnabled
        {
            get => objects[0].ShadowsEnabled;
            set
            {
                foreach (var resource in objects)
                {
                    resource.ShadowsEnabled = value;
                }
            }
        }
        
        public bool OcclusionOnly
        {
            get => objects[0].OcclusionOnly;
            set
            {
                foreach (var resource in objects)
                {
                    resource.OcclusionOnly = value;
                }
            }
        }

        void Rebuild()
        {
            if (objects == null)
            {
                CreateObjects();
            }

            float vRadius = Mathf.Min(radius, Mathf.Min(size.x / 2, size.y / 2));
            
            objects[0].Transform.localScale = new Vector3(size.x - 2 * vRadius, 1, size.y);
            objects[1].Transform.localPosition = new Vector3(-size.x / 2 + vRadius / 2, 0, 0);
            objects[1].Transform.localScale = new Vector3(vRadius, 1, size.y - 2 * vRadius);
            objects[2].Transform.localPosition = new Vector3(size.x / 2 - vRadius / 2, 0, 0);
            objects[2].Transform.localScale = new Vector3(vRadius, 1, size.y - 2 * vRadius);
            
            objects[3].Transform.localPosition = new Vector3(size.x / 2 - vRadius, 0, size.y / 2 - vRadius);
            objects[4].Transform.localPosition = new Vector3(size.x / 2 - vRadius, 0, -size.y / 2 + vRadius);
            objects[5].Transform.localPosition = new Vector3(-size.x / 2 + vRadius, 0, -size.y / 2 + vRadius);
            objects[6].Transform.localPosition = new Vector3(-size.x / 2 + vRadius, 0, size.y / 2 - vRadius);
            
            var radiusScale = new Vector3(vRadius, vRadius, vRadius);
            objects[3].Transform.localScale = radiusScale;
            objects[4].Transform.localScale = radiusScale;
            objects[5].Transform.localScale = radiusScale;
            objects[6].Transform.localScale = radiusScale;
        }
        
        void CreateObjects()
        {
            objects = new MeshMarkerResource[7];
            for (int i = 0; i < 3; i++)
            {
                var o = new GameObject { name = "Frame" };
                var resource = o.AddComponent<MeshMarkerResource>();
                resource.Mesh = QuadMesh;
                resource.Transform.SetParentLocal(Transform);
                objects[i] = resource;
            }

            for (int i = 0; i < 4; i++)
            {
                var o = new GameObject { name = "Corner" };;
                var resource = o.AddComponent<MeshMarkerResource>();
                resource.Mesh = CornerMesh;
                resource.Transform.SetParentLocal(Transform);
                objects[3 + i] = resource;
            }
                
            objects[0].Transform.localPosition = Vector3.zero;
            objects[4].Transform.localRotation = Quaternion.AngleAxis(90, Vector3.up);
            objects[5].Transform.localRotation = Quaternion.AngleAxis(180, Vector3.up);
            objects[6].Transform.localRotation = Quaternion.AngleAxis(270, Vector3.up);
        }

        void Awake()
        {
            if (objects == null)
            {
                Rebuild();
            }
        }

        Bounds? IDisplay.Bounds => Bounds;

        public Bounds Bounds => new Bounds(Vector3.zero, new Vector3(size.x, 0.01f, size.y));

        public int Layer
        {
            get => objects[0].Layer;
            set
            {
                foreach (var resource in objects)
                {
                    resource.Layer = value;
                }                
            }
        }
        
        public void Suspend()
        {
            OcclusionOnly = false;
            ShadowsEnabled = false;
            EmissiveColor = Color.black;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        [NotNull]
        public string Name
        {
            get => name;
            set => name = value;
        }
    }
}