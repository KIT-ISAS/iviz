using System.Collections.Generic;
using Iviz.Core;
using Iviz.Displays;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class RingDisplay : MeshMarkerResource
    {
        static Mesh baseMesh;
        [NotNull] static Mesh BaseMesh => baseMesh != null ? baseMesh : (baseMesh = CreateMesh(null, 0.9f, 1.1f));

        protected override void Awake()
        {
            base.Awake();
            Mesh = BaseMesh;
        }

        static Mesh CreateMesh([CanBeNull] Mesh inputMesh, float r0, float r1)
        {
            Mesh mesh = inputMesh != null ? inputMesh : new Mesh();
            List<Vector3> vertices = new List<Vector3>();
            List<int> indices = new List<int>();
            List<Color> colors = new List<Color>();

            Color outerColor = Color.white;
            Color innerColor = Color.white.WithAlpha(0);

            const int numVertices = 40;
            for (int i = 0; i < numVertices; i++)
            {
                float a0 = Mathf.PI * 2 / numVertices * i;
                float a1 = Mathf.PI * 2 / numVertices * (i + 1);

                Vector3 dirA0 = new Vector3(Mathf.Cos(a0), 0, Mathf.Sin(a0));
                Vector3 dirA1 = new Vector3(Mathf.Cos(a1), 0, Mathf.Sin(a1));

                int j = vertices.Count;

                vertices.Add(dirA0 * r0);
                vertices.Add(dirA0 * r1);
                vertices.Add(dirA1 * r1);
                vertices.Add(dirA1 * r0);

                indices.Add(j + 3);
                indices.Add(j + 2);
                indices.Add(j + 1);
                indices.Add(j);
                
                colors.Add(innerColor);
                colors.Add(outerColor);
                colors.Add(outerColor);
                colors.Add(innerColor);
            }

            mesh.SetVertices(vertices);
            mesh.SetIndices(indices, MeshTopology.Quads, 0);
            mesh.SetColors(colors);
            mesh.RecalculateNormals();

            return mesh;
        }
    }
}