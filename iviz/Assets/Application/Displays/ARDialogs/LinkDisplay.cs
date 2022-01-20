#nullable enable

using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class LinkDisplay : MeshMarkerDisplay
    {
        static Mesh? baseLinkMesh;
        static Mesh BaseLinkMesh => baseLinkMesh != null ? baseLinkMesh : (baseLinkMesh = CreateMesh());

        static Mesh CreateMesh()
        {
            var cubeMesh = Resource.Displays.Cube.Object.GetComponent<MeshFilter>().sharedMesh;
            var vertices = cubeMesh.vertices;

            return new Mesh
            {
                name = "Link Mesh",
                vertices = vertices,
                normals = cubeMesh.normals,
                colors = vertices.Select(
                    vertex => vertex.z < 0
                        ? Color.white.WithAlpha(0)
                        : Color.white
                ).ToArray(),
                triangles = cubeMesh.triangles
            };
        }
        
        protected override void Awake()
        {
            base.Awake();
            Mesh = BaseLinkMesh;
        }
    }
}