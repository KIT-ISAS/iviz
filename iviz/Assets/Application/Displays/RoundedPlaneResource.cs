#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    public class RoundedPlaneResource : MeshMarkerHolderResource
    {
        static Mesh? cornerMesh;
        static Mesh? quadMesh;
        
        static Mesh QuadMesh => quadMesh != null ? quadMesh : (quadMesh = GetQuadMesh());
        static Mesh CornerMesh => cornerMesh != null ? cornerMesh : (cornerMesh = CreateCornerMesh());
        static Mesh GetQuadMesh() => Resource.Displays.Square.Object.GetComponent<MeshMarkerResource>().Mesh;

        Vector2 size;
        float radius;

        public Vector2 Size
        {
            set
            {
                if (Mathf.Approximately(value.x, size.x) && Mathf.Approximately(value.y, size.y))
                {
                    return;
                }

                size = value;
                Collider.size = new Vector3(size.x, 0.001f, size.y);
                Rebuild();
            }
        }

        public float Radius
        {
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
        
        void Rebuild()
        {
            if (children.Length == 0)
            {
                children = CreateObjects(Transform);
            }
            

            float vRadius = Mathf.Min(radius, Mathf.Min(size.x / 2, size.y / 2));

            const float zScale = 0.001f;
            
            children[0].Transform.localScale = new Vector3(size.x - 2 * vRadius, zScale, size.y);
            children[1].Transform.localPosition = new Vector3(-size.x / 2 + vRadius / 2, 0, 0);
            children[1].Transform.localScale = new Vector3(vRadius, zScale, size.y - 2 * vRadius);
            children[2].Transform.localPosition = new Vector3(size.x / 2 - vRadius / 2, 0, 0);
            children[2].Transform.localScale = new Vector3(vRadius, zScale, size.y - 2 * vRadius);
            
            children[3].Transform.localPosition = new Vector3(size.x / 2 - vRadius, 0, size.y / 2 - vRadius);
            children[4].Transform.localPosition = new Vector3(size.x / 2 - vRadius, 0, -size.y / 2 + vRadius);
            children[5].Transform.localPosition = new Vector3(-size.x / 2 + vRadius, 0, -size.y / 2 + vRadius);
            children[6].Transform.localPosition = new Vector3(-size.x / 2 + vRadius, 0, size.y / 2 - vRadius);
            
            var radiusScale = new Vector3(vRadius, zScale, vRadius);
            children[3].Transform.localScale = radiusScale;
            children[4].Transform.localScale = radiusScale;
            children[5].Transform.localScale = radiusScale;
            children[6].Transform.localScale = radiusScale;
        }
        
        static MeshMarkerResource[] CreateObjects(Transform transform)
        {
            var objects = new MeshMarkerResource[7];
            for (int i = 0; i < 3; i++)
            {
                var o = new GameObject { name = "Frame" };
                var resource = o.AddComponent<MeshMarkerResource>();
                resource.Mesh = QuadMesh;
                resource.Transform.SetParentLocal(transform);
                resource.ColliderEnabled = false;
                resource.Layer = LayerType.IgnoreRaycast;
                objects[i] = resource;
            }

            for (int i = 0; i < 4; i++)
            {
                var o = new GameObject { name = "Corner" };;
                var resource = o.AddComponent<MeshMarkerResource>();
                resource.Mesh = CornerMesh;
                resource.Transform.SetParentLocal(transform);
                resource.ColliderEnabled = false;
                resource.Layer = LayerType.IgnoreRaycast;
                objects[3 + i] = resource;
            }
                
            objects[0].Transform.localPosition = Vector3.zero;
            objects[4].Transform.localRotation = Quaternion.AngleAxis(90, Vector3.up);
            objects[5].Transform.localRotation = Quaternion.AngleAxis(180, Vector3.up);
            objects[6].Transform.localRotation = Quaternion.AngleAxis(270, Vector3.up);
            return objects;
        }

        static Mesh CreateCornerMesh()
        {
            var vertices = new List<Vector3>
            {
                Vector3.zero,
                Vector3.right,
            };

            const int numVertices = 10;
            for (int i = 1; i < numVertices; i++)
            {
                float angle = Mathf.PI / 2 * i / numVertices;
                vertices.Add(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));
            }

            vertices.Add(Vector3.forward);

            var indices = new List<int>();
            for (int i = 0; i < numVertices; i++)
            {
                indices.Add(i + 1);
                indices.Add(0);
                indices.Add(i + 2);
            }

            var mesh = new Mesh
            {
                name = "Corner Mesh",
                vertices = vertices.ToArray(),
                triangles = indices.ToArray()
            };
            mesh.RecalculateNormals();
            return mesh;
        }
        
        void Awake()
        {
            if (children.Length == 0)
            {
                Rebuild();
            }
        }

        public override void Suspend()
        {
            OcclusionOnly = false;
            ShadowsEnabled = false;
            EmissiveColor = Color.black;
        }
    }
}