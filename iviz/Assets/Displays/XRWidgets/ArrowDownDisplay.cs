#nullable enable

using Iviz.Core;
using Iviz.Displays;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class ArrowDownDisplay : MeshMarkerDisplay
    {
        static Mesh? baseMesh;

        protected override void Awake()
        {
            base.Awake();
            
            if (baseMesh == null)
            {
                baseMesh = CreateMesh(Mesh);
            }

            Mesh = baseMesh;
        }

        static Mesh CreateMesh(Mesh sourceMesh)
        {
            var vertices = sourceMesh.vertices;
            var normals = sourceMesh.normals;
            int[] triangles = sourceMesh.triangles;
            var colors = vertices.Select(
                    vertex => Color.Lerp(Color.white.WithAlpha(0), Color.white, vertex.z))
                .ToArray();

            return new Mesh
            {
                name = "Arrow Mesh",
                vertices = vertices,
                normals = normals,
                colors = colors,
                triangles = triangles
            };
        }
    }
}