using System.Collections.Generic;
using Iviz.Core;
using Iviz.Displays;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class PolyGlowDisplay : MeshMarkerDisplay
    {
        static Mesh squareMesh;
        [NotNull] static Mesh SquareMesh => squareMesh != null ? squareMesh : (squareMesh = CreateMesh(4));

        static Mesh circleMesh;
        [NotNull] static Mesh CircleMesh => circleMesh != null ? circleMesh : (circleMesh = CreateMesh(40));

        protected override void Awake()
        {
            base.Awake();
            Mesh = SquareMesh;
        }

        public void SetToSquare()
        {
            Mesh = SquareMesh;
        }

        public void SetToCircle()
        {
            Mesh = CircleMesh;
        }

        [NotNull]
        static Mesh CreateMesh(int numVertices)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> indices = new List<int>();
            List<Color> colors = new List<Color>();

            Color outerColor = Color.white;
            Color innerColor = Color.white.WithAlpha(0);

            for (int i = 0; i < numVertices; i++)
            {
                float a0 = Mathf.PI * 2 / numVertices * i;
                float a1 = Mathf.PI * 2 / numVertices * (i + 1);

                Vector3 dirA0 = new Vector3(Mathf.Cos(a0), 0, Mathf.Sin(a0));
                Vector3 dirA1 = new Vector3(Mathf.Cos(a1), 0, Mathf.Sin(a1));

                int j = vertices.Count;

                vertices.Add(dirA0);
                vertices.Add(dirA0 + Vector3.up);
                vertices.Add(dirA1 + Vector3.up);
                vertices.Add(dirA1);

                vertices.Add(dirA0 * 0.99f);
                vertices.Add(dirA0 * 0.99f + Vector3.up);
                vertices.Add(dirA1 * 0.99f + Vector3.up);
                vertices.Add(dirA1 * 0.99f);

                indices.Add(j);
                indices.Add(j + 1);
                indices.Add(j + 2);
                indices.Add(j + 3);

                indices.Add(j + 7);
                indices.Add(j + 6);
                indices.Add(j + 5);
                indices.Add(j + 4);

                colors.Add(outerColor);
                colors.Add(innerColor);
                colors.Add(innerColor);
                colors.Add(outerColor);
                
                colors.Add(outerColor);
                colors.Add(innerColor);
                colors.Add(innerColor);
                colors.Add(outerColor);
            }

            Mesh mesh = new Mesh();
            mesh.SetVertices(vertices);
            mesh.SetIndices(indices, MeshTopology.Quads, 0);
            mesh.SetColors(colors);
            mesh.RecalculateNormals();

            return mesh;
        }
    }
}