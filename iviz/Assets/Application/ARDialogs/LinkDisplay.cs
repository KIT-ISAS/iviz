using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class LinkDisplay : MeshMarkerResource
    {
        static Mesh baseLinkMesh;
        [NotNull] static Mesh BaseLinkMesh => baseLinkMesh != null ? baseLinkMesh : (baseLinkMesh = CreateMesh());
        
        void Awake()
        {
            Mesh = BaseLinkMesh;            
        }

        static Mesh CreateMesh()
        {
            Color[] colors = new Color[24];

            Mesh cubeMesh = Resource.Displays.Cube.Object.GetComponent<MeshFilter>().sharedMesh;
            var vertices = cubeMesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                colors[i] = vertices[i].x < 0
                    ? Color.white.WithAlpha(0)
                    : Color.white;
            }

            Mesh mesh = new Mesh
            {
                name = "Link Mesh",
                vertices = vertices,
                normals = cubeMesh.normals,
                colors = colors,
                triangles = cubeMesh.triangles
            };

            return mesh;                   
        }
    }
}