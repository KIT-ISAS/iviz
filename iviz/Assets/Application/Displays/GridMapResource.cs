using System;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class GridMapResource : MarkerResourceWithColormap
    {
        static readonly int PropInputTexture = Shader.PropertyToID("_InputTex");
        static readonly int PropSquareTexture = Shader.PropertyToID("_SquareTex");
        static readonly int PropSquareCoeff = Shader.PropertyToID("_SquareCoeff");

        //[SerializeField] Texture squareTexture = null;
        Texture2D inputTexture;
        [SerializeField] Material material;

        Mesh mesh;
        int cellsX;
        int cellsY;

        [CanBeNull] MeshRenderer meshRenderer;

        [NotNull]
        MeshRenderer MeshRenderer => meshRenderer != null ? meshRenderer : meshRenderer = GetComponent<MeshRenderer>();

        protected override void Awake()
        {
            material = Resource.Materials.GridMap.Instantiate();
            mesh = new Mesh {name = "GridMap Mesh"};

            base.Awake();

            MeshRenderer.material = material;
            GetComponent<MeshFilter>().sharedMesh = mesh;

            IntensityBounds = new Vector2(0, 1);
            Colormap = Resource.ColormapId.gray;
        }

        protected override void Rebuild()
        {
            // not needed
        }

        public void Set(int newCellsX, int newCellsY, float width, float height, [NotNull] float[] data, int length)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            EnsureSize(newCellsX, newCellsY);

            Transform mTransform = transform;
            mTransform.localScale = new Vector3(width, height, 1).Ros2Unity().Abs();
            mTransform.localPosition = new Vector3(-width / 2, -height / 2, 0).Ros2Unity();

            NativeArray<float>.Copy(data, 0,
                inputTexture.GetRawTextureData<float>().GetSubArray(0, length), 0, length);
            inputTexture.Apply();

            float min = float.MaxValue, max = float.MinValue;
            for (int i = 0; i < length; i++)
            {
                float val = data[i];
                if (val < min)
                {
                    min = val;
                }

                if (val > max)
                {
                    max = val;
                }
            }


            BoxCollider.center = new Vector3(0.5f, 0.5f, (max + min) / 2).Ros2Unity();
            BoxCollider.size = new Vector3(1, 1, max - min).Ros2Unity().Abs();
            IntensityBounds = new Vector2(min, max);
        }

        void EnsureSize(int newWidth, int newHeight)
        {
            if (newWidth == cellsX && newHeight == cellsY)
            {
                return;
            }

            cellsX = newWidth;
            cellsY = newHeight;

            int verticesSize = (cellsX + 1) * (cellsY + 1);
            int indexSize = cellsX * cellsY;

            using (var pointsArray = new Rent<Vector3>(verticesSize))
            using (var indicesArray = new Rent<int>(indexSize * 4))
            {
                Vector3[] points = pointsArray.Array;
                float stepX = 1f / cellsX;
                float stepY = 1f / cellsY;
                for (int v = 0, off = 0; v <= cellsY; v++)
                {
                    for (int u = 0; u <= cellsX; u++, off++)
                    {
                        points[off] = new Vector3(
                            u * stepX,
                            v * stepY,
                            0
                        ).Ros2Unity();
                    }
                }

                int[] indices = indicesArray.Array;
                for (int v = 0; v < cellsY; v++)
                {
                    int iOffset = v * cellsX * 4;
                    int pOffset = v * (cellsX + 1);
                    for (int u = 0; u < cellsX; u++, iOffset += 4, pOffset++)
                    {
                        indices[iOffset + 3] = pOffset;
                        indices[iOffset + 2] = pOffset + 1;
                        indices[iOffset + 1] = pOffset + (cellsX + 1) + 1;
                        indices[iOffset + 0] = pOffset + (cellsX + 1);
                    }
                }

                mesh.SetVertices(pointsArray);
                mesh.SetIndices(indicesArray, MeshTopology.Quads, 0);
                mesh.Optimize();
            }

            if (inputTexture != null)
            {
                Destroy(inputTexture);
            }

            inputTexture = new Texture2D(cellsX, cellsY, TextureFormat.RFloat, true);
            material.SetTexture(PropInputTexture, inputTexture);
            material.SetVector(PropSquareCoeff, new Vector4(cellsX, cellsY, 1f / cellsX, 1f / cellsY));
        }

        void OnDestroy()
        {
            if (inputTexture != null)
            {
                Destroy(inputTexture);
            }

            if (material != null)
            {
                Destroy(material);
            }

            Destroy(mesh);
        }

        protected override void UpdateProperties()
        {
            MeshRenderer.SetPropertyBlock(Properties);
        }

        [NotNull]
        static Texture2D GenerateSquareTexture()
        {
            const int size = 64;
            const int border = 4;
            Color32 white = Color.white;
            Color32 black = Color.black;

            Color32[] colors = new Color32[size * size];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = white;
            }

            for (int v = 0; v < size; v++)
            {
                for (int i = 0; i < border; i++)
                {
                    colors[v * size + i] = black;
                    colors[(v + 1) * size - 1 - i] = black;
                }
            }

            for (int i = 0; i < size * border; i++)
            {
                colors[i] = black;
            }

            for (int i = size * (size - border); i < size * size; i++)
            {
                colors[i] = black;
            }

            Texture2D texture = new Texture2D(size, size);
            texture.SetPixels32(colors);
            texture.Apply();
            texture.Compress(true);
            return texture;


            //Texture2D texture = GenerateTexture();
            //byte[] png = texture.EncodeToPNG();
            //File.WriteAllBytes("square.png", png);
        }
    }
}