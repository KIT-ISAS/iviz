using System.IO;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Displays
{
    public sealed class GridMapResource : MarkerResourceWithColormap
    {
        static readonly int PropInputTexture = Shader.PropertyToID("_InputTexture");
        static readonly int PropSquareTexture = Shader.PropertyToID("_SquareTexture");
        static readonly int PropSquareCoeff = Shader.PropertyToID("_SquareCoeff");

        [SerializeField] Texture squareTexture = null;
        Texture2D inputTexture;

        Mesh mesh;
        public int CellsX { get; private set; }
        public int CellsY { get; private set; }

        protected override void Awake()
        {
            material = Resource.Materials.GridMap.Instantiate();
            mesh = new Mesh();

            base.Awake();

            GetComponent<MeshRenderer>().material = material;
            GetComponent<MeshFilter>().sharedMesh = mesh;

            IntensityBounds = new Vector2(0, 1);
            Colormap = Resource.ColormapId.gray;

            material.SetTexture(PropSquareTexture, squareTexture);
        }

        protected override void Rebuild()
        {
            // not needed
        }

        public void Set(int cellsX, int cellsY, float width, float height, float[] data)
        {
            EnsureSize(cellsX, cellsY);

            Transform mTransform = transform;
            mTransform.localScale = new Vector3(width, height, 1).Ros2Unity().Abs();
            mTransform.localPosition = new Vector3(-width / 2, -height / 2, 0).Ros2Unity();

            //Debug.Log(CellsX + "  " + CellsY);
            inputTexture.GetRawTextureData<float>().CopyFrom(data);
            inputTexture.Apply();

            float min = float.MaxValue, max = float.MinValue;
            foreach (float val in data)
            {
                if (val < min)
                {
                    min = val;
                }

                if (val > max)
                {
                    max = val;
                }
            }

            Collider.center = new Vector3(0.5f, 0.5f, (max + min) / 2).Ros2Unity();
            Collider.size = new Vector3(1, 1, max - min).Ros2Unity().Abs();
            IntensityBounds = new Vector2(min, max);
        }

        void EnsureSize(int newWidth, int newHeight)
        {
            if (newWidth == CellsX && newHeight == CellsY)
            {
                return;
            }

            CellsX = newWidth;
            CellsY = newHeight;

            int verticesSize = (CellsX + 1) * (CellsY + 1);
            Vector3[] points = new Vector3[verticesSize];
            float stepX = 1f / CellsX;
            float stepY = 1f / CellsY;
            for (int v = 0, off = 0; v <= CellsY; v++)
            {
                for (int u = 0; u <= CellsX; u++, off++)
                {
                    points[off] = new Vector3(
                        u * stepX,
                        v * stepY,
                        0
                    ).Ros2Unity();
                }
            }

            int indexSize = CellsX * CellsY;
            int[] indices = new int[indexSize * 4];
            for (int v = 0; v < CellsY; v++)
            {
                int iOffset = v * CellsX * 4;
                int pOffset = v * (CellsX + 1);
                for (int u = 0; u < CellsX; u++, iOffset += 4, pOffset++)
                {
                    indices[iOffset + 3] = pOffset;
                    indices[iOffset + 2] = pOffset + 1;
                    indices[iOffset + 1] = pOffset + (CellsX + 1) + 1;
                    indices[iOffset + 0] = pOffset + (CellsX + 1);
                }
            }

            mesh.vertices = points;
            mesh.SetIndices(indices, MeshTopology.Quads, 0);
            mesh.Optimize();

            if (!(inputTexture is null))
            {
                Destroy(inputTexture);
            }

            inputTexture = new Texture2D(CellsX, CellsY, TextureFormat.RFloat, false);
            material.SetTexture(PropInputTexture, inputTexture);
            material.SetVector(PropSquareCoeff, new Vector4(CellsX, CellsY, 1f / CellsX, 1f / CellsY));

            /*
            if (intensityTexture != null)
            {
                Destroy(intensityTexture);
            }
            intensityTexture = new Texture2D(Width, Height, TextureFormat.RFloat, false);
            material.SetTexture("_IntensityTexture", intensityTexture);
            */
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (inputTexture != null)
            {
                Destroy(inputTexture);
            }

            Destroy(mesh);
        }


        static Texture2D GenerateTexture()
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