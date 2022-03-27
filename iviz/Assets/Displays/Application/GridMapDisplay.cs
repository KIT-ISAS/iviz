#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.Rendering;
using Color = UnityEngine.Color;

namespace Iviz.Displays
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class GridMapDisplay : MarkerDisplayWithColormap, ISupportsPbr, ISupportsShadows
    {
        static float baseOffset = 0.001f + 5e-6f;

        static readonly int PropInputTexture = Shader.PropertyToID("_InputTex");
        static readonly int PropSquareTexture = Shader.PropertyToID("_SquareTex");
        static readonly int PropSquareCoeff = Shader.PropertyToID("_SquareCoeff");
        static readonly int PropTint = Shader.PropertyToID("_Tint");
        static readonly int PropSmoothness = Shader.PropertyToID("_Smoothness");
        static readonly int PropMetallic = Shader.PropertyToID("_Metallic");

        [SerializeField] Material? opaqueMaterial;
        [SerializeField] Material? transparentMaterial;
        [SerializeField] MeshRenderer? meshRenderer;

        readonly float zOffset;

        Texture2D? texture;
        Mesh? mesh;
        int cellsX;
        int cellsY;
        float smoothness = 0.5f;
        float metallic = 0.5f;
        float scaleHeight = 1;

        MeshRenderer MeshRenderer => meshRenderer.AssertNotNull(nameof(meshRenderer));

        public float ScaleHeight
        {
            set
            {
                scaleHeight = value;
                Transform.localScale = Transform.localScale.WithY(value);
            }
        }

        public float Smoothness
        {
            set
            {
                smoothness = value;
                UpdateMaterial();
            }
        }

        public float Metallic
        {
            set
            {
                metallic = value;
                UpdateMaterial();
            }
        }

        public bool EnableShadows
        {
            set
            {
                MeshRenderer.shadowCastingMode = value ? ShadowCastingMode.On : ShadowCastingMode.Off;
                MeshRenderer.receiveShadows = value;
            }
        }

        public override Color Tint
        {
            get => base.Tint;
            set
            {
                base.Tint = value;
                if (value.a <= 254f / 255)
                {
                    if (transparentMaterial == null)
                    {
                        transparentMaterial = Resource.Materials.TransparentGridMap.Instantiate();
                    }

                    MeshRenderer.sharedMaterial = transparentMaterial;
                }
                else
                {
                    if (opaqueMaterial == null)
                    {
                        opaqueMaterial = Resource.Materials.GridMap.Instantiate();
                    }

                    MeshRenderer.sharedMaterial = opaqueMaterial;
                }

                UpdateMaterial();
            }
        }

        public GridMapDisplay()
        {
            zOffset = (baseOffset += 1e-5f);
        }

        protected override void Awake()
        {
            base.Awake();

            Tint = Tint;
            Colormap = ColormapId.jet;
        }

        void UpdateMaterial()
        {
            var material = MeshRenderer.sharedMaterial;
            material.SetTexture(PropInputTexture, texture);
            material.SetVector(PropSquareCoeff,
                new Vector4(cellsX, cellsY, 1f / cellsX, 1f / cellsY));
            material.SetColor(PropTint, Tint);
            material.SetFloat(PropSmoothness, smoothness);
            material.SetFloat(PropMetallic, metallic);
            UpdateProperties();
        }

        protected override void Rebuild()
        {
            // not needed
        }

        public void Set(int newCellsX, int newCellsY, float width, float height, ReadOnlySpan<float> data)
        {
            Transform.localScale = new Vector3(width, height, scaleHeight).Ros2Unity().Abs();
            Transform.localPosition = new Vector3(-width / 2, -height / 2, zOffset).Ros2Unity();

            var textureToUse = EnsureSize(newCellsX, newCellsY);
            textureToUse.CopyFrom(data);
            textureToUse.Apply();

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

            var span = new Vector2(min, max);
            MeasuredIntensityBounds = span;
            if (!OverrideIntensityBounds)
            {
                IntensityBounds = span;
            }
        }

        Texture2D EnsureSize(int newWidth, int newHeight)
        {
            if (texture != null && newWidth == cellsX && newHeight == cellsY)
            {
                return texture;
            }

            if (mesh == null)
            {
                mesh = new Mesh { name = "GridMap Mesh" };
                GetComponent<MeshFilter>().sharedMesh = mesh;
            }

            cellsX = newWidth;
            cellsY = newHeight;

            int verticesSize = (cellsX + 1) * (cellsY + 1);
            int indexSize = cellsX * cellsY;

            using (var pointsArray = new Rent<Vector3>(verticesSize))
            using (var indicesArray = new Rent<int>(indexSize * 4))
            {
                var points = pointsArray.AsSpan();
                float stepX = 1f / cellsX;
                float stepY = 1f / cellsY;
                int off = 0;
                foreach (int v in ..cellsY)
                {
                    foreach (int u in ..(cellsX + 1))
                    {
                        points[off++] = new Vector3(
                            u * stepX,
                            v * stepY,
                            0
                        ).Ros2Unity();
                    }
                }

                var indices = indicesArray.AsSpan();
                foreach (int v in ..cellsY)
                {
                    int iOffset = v * cellsX * 4;
                    int pOffset = v * (cellsX + 1);
                    foreach (int _ in ..cellsX)
                    {
                        indices[iOffset + 0] = pOffset + (cellsX + 1);
                        indices[iOffset + 1] = pOffset + (cellsX + 1) + 1;
                        indices[iOffset + 2] = pOffset + 1;
                        indices[iOffset + 3] = pOffset;

                        iOffset += 4;
                        pOffset++;
                    }
                }

                mesh.SetVertices(pointsArray);
                mesh.SetIndices(indicesArray, MeshTopology.Quads, 0);
                mesh.RecalculateNormals();
                mesh.Optimize();
            }

            if (texture != null)
            {
                Destroy(texture);
            }

            texture = new Texture2D(cellsX, cellsY, TextureFormat.RFloat, true);
            var textureParams = new Vector4(cellsX, cellsY, 1f / cellsX, 1f / cellsY);
            if (opaqueMaterial != null)
            {
                opaqueMaterial.SetTexture(PropInputTexture, texture);
                opaqueMaterial.SetVector(PropSquareCoeff, textureParams);
            }

            if (transparentMaterial != null)
            {
                transparentMaterial.SetTexture(PropInputTexture, texture);
                transparentMaterial.SetVector(PropSquareCoeff, textureParams);
            }

            return texture;
        }

        void OnDestroy()
        {
            if (texture != null)
            {
                Destroy(texture);
            }

            if (opaqueMaterial != null)
            {
                Destroy(opaqueMaterial);
            }

            if (transparentMaterial != null)
            {
                Destroy(transparentMaterial);
            }

            if (mesh != null)
            {
                Destroy(mesh);
            }
        }

        protected override void UpdateProperties()
        {
            MeshRenderer.SetPropertyBlock(Properties);
        }

        public void Reset()
        {
            if (texture == null)
            {
                return;
            }

            texture.AsSpan().Fill(0);
            texture.Apply();

            Collider.center = new Vector3(0.5f, 0.5f, 0).Ros2Unity();
            Collider.size = new Vector3(1, 1, 0).Ros2Unity().Abs();

            var span = Vector2.zero;
            
            MeasuredIntensityBounds = span;
            if (!OverrideIntensityBounds)
            {
                IntensityBounds = span;
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            Reset();
            Tint = Color.white;
        }

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