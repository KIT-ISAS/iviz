#nullable enable

using System.Collections.Generic;
using Iviz.Core;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public enum PolyGlowModeType
    {
        Square,
        Circle,
    }
    
    public sealed class PolyGlowDisplay : MeshMarkerDisplay
    {
        static Mesh? squareMesh;
        static Mesh SquareMesh => squareMesh != null ? squareMesh : (squareMesh = CreateMesh(4));

        static Mesh? circleMesh;
        static Mesh CircleMesh => circleMesh != null ? circleMesh : (circleMesh = CreateMesh(40));

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

        static Mesh CreateMesh(int numVertices)
        {
            var vertices = new List<Vector3>();
            var indices = new List<int>();
            var colors = new List<Color>();

            var outerColor = Color.white;
            var innerColor = Color.white.WithAlpha(0);

            foreach (int i in ..numVertices)
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

            var mesh = new Mesh { name = "PolyGlow" };
            mesh.SetVertices(vertices);
            mesh.SetIndices(indices, MeshTopology.Quads, 0);
            mesh.SetColors(colors);
            mesh.RecalculateNormals();

            return mesh;
        }
    }
}