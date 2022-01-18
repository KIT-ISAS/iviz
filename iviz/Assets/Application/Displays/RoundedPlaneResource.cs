#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Displays
{
    public class RoundedPlaneResource : MeshMarkerHolderResource
    {
        [SerializeField] Mesh? cornerMesh;
        [SerializeField] Mesh? quadMesh;

        Mesh QuadMesh => quadMesh != null ? quadMesh : (quadMesh = GetQuadMesh());
        Mesh CornerMesh => cornerMesh.AssertNotNull(nameof(cornerMesh));
        static Mesh GetQuadMesh() => Resource.Displays.Square.Object.GetComponent<MeshMarkerResource>().Mesh;

        Vector2 size;
        float radius;

        public new Bounds Bounds => Collider.GetBounds();

        public Vector2 Size
        {
            set
            {
                if (Mathf.Approximately(value.x, size.x) && Mathf.Approximately(value.y, size.y))
                {
                    return;
                }

                size = value.Abs();
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
                children = CreateObjects();
            }


            float vRadius = Math.Min(radius, Math.Min(size.x / 2, size.y / 2));

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

        MeshMarkerResource[] CreateObjects()
        {
            var objects = new MeshMarkerResource[7];
            foreach (int i in ..3)
            {
                var o = new GameObject { name = "Frame" };
                var resource = o.AddComponent<MeshMarkerResource>();
                resource.Mesh = QuadMesh;
                resource.Transform.SetParentLocal(transform);
                resource.EnableCollider = false;
                resource.EnableShadows = false;
                resource.Layer = LayerType.IgnoreRaycast;
                objects[i] = resource;
            }

            foreach (int i in 3..7)
            {
                var o = new GameObject { name = "Corner" };
                var resource = o.AddComponent<MeshMarkerResource>();
                resource.Mesh = CornerMesh;
                resource.Transform.SetParentLocal(transform);
                resource.EnableCollider = false;
                resource.EnableShadows = false;
                resource.Layer = LayerType.IgnoreRaycast;
                objects[i] = resource;
            }

            objects[0].Transform.localPosition = Vector3.zero;
            objects[4].Transform.localRotation =
                new Quaternion(0, 0.707106769f, 0, 0.707106769f); // Quaternion.AngleAxis(90, Vector3.up);
            objects[5].Transform.localRotation =
                new Quaternion(0, 1, 0, 0); // Quaternion.AngleAxis(180, Vector3.up);
            objects[6].Transform.localRotation =
                new Quaternion(0, 0.707106769f, 0, -0.707106769f); // Quaternion.AngleAxis(90, Vector3.up);
            return objects;
        }

        /*
        static Mesh CreateCornerMesh()
        {
            var vertices = new List<Vector3>
            {
                Vector3.zero,
                Vector3.right,
            };

            const int numVertices = 10;
            foreach (int i in 1..numVertices)
            {
                float angle = Mathf.PI / 2 * i / numVertices;
                vertices.Add(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));
            }

            vertices.Add(Vector3.forward);

            var indices = new List<int>();
            foreach (int i in ..numVertices)
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
            
            
            using (var builder = BuilderPool.Rent())
            {
                builder.Append("o Corner").AppendLine();
                foreach ((float x, float y, float z) in vertices)
                {
                    builder.Append("v ")
                        .Append(-x).Append(" ")
                        .Append(y).Append(" ")
                        .Append(z).AppendLine();
                }

                foreach (int i in ..numVertices)
                {
                    builder.Append("f ")
                        .Append(i + 3).Append(" ")
                        .Append(1).Append(" ")
                        .Append(i + 2).AppendLine()
                        ;
                }
            }

            return mesh;
        }
        */

        void Awake()
        {
            if (children.Length == 0)
            {
                Rebuild();
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            OcclusionOnly = false;
            EnableShadows = false;
            EmissiveColor = Color.black;
        }
    }
}