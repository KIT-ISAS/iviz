using System;
using UnityEngine;
using System.Runtime.InteropServices;
using Iviz.Resources;

namespace Iviz.Displays
{
    public sealed class DepthCloudResource : MarkerResource
    {
        [SerializeField] Material material;

        [SerializeField] float elementSize = 1;
        
        /// <summary>
        /// Size multiplier for points
        /// </summary>
        public float ElementSize
        {
            get => elementSize;
            set
            {
                elementSize = value;
                UpdateQuadComputeBuffer();
            }
        }

        [SerializeField] float fovAngle;
        public float FovAngle
        {
            get => fovAngle;
            set
            {
                fovAngle = value;
                if (DepthImage != null)
                {
                    UpdatePosValues(DepthImage.Texture);
                }
            }
        }

        ImageTexture colorImage;
        public ImageTexture ColorImage
        {
            get => colorImage;
            set
            {
                if (colorImage != null)
                {
                    colorImage.TextureChanged -= UpdateColorTexture;
                }
                colorImage = value;
                if (colorImage != null)
                {
                    UpdateColorTexture(colorImage.Texture);
                    colorImage.TextureChanged += UpdateColorTexture;
                }
                else
                {
                    UpdateColorTexture(null);
                }
            }
        }

        ImageTexture depthImage;
        public ImageTexture DepthImage
        {
            get => depthImage;
            set
            {
                if (depthImage != null)
                {
                    depthImage.TextureChanged -= UpdatePointComputeBuffers;
                    depthImage.ColormapChanged -= UpdateColormap;
                }
                depthImage = value;
                if (depthImage != null)
                {
                    UpdateDepthTexture(depthImage.Texture);
                    UpdateColormap(depthImage.ColormapTexture);
                    depthImage.TextureChanged += UpdateDepthTexture;
                    depthImage.ColormapChanged += UpdateColormap;
                }
                else
                {
                    UpdateDepthTexture(null);
                }
            }
        }

        [SerializeField] int width;
        [SerializeField] int height;

        Vector2[] uvs = new Vector2[0];

        ComputeBuffer pointComputeBuffer;
        ComputeBuffer quadComputeBuffer;

        protected override void Awake()
        {
            base.Awake();
            material = Resource.Materials.DepthImageProjector.Instantiate();
        }

        static readonly int PQuad = Shader.PropertyToID("_Quad");
        void UpdateQuadComputeBuffer()
        {
            Vector2[] quad = {
                    new Vector2( 0.5f,  0.5f),
                    new Vector2( 0.5f, -0.5f),
                    new Vector2(-0.5f, -0.5f),
                    new Vector2(-0.5f,  0.5f),
            };
            if (quadComputeBuffer == null)
            {
                quadComputeBuffer = new ComputeBuffer(4, Marshal.SizeOf<Vector2>());
                material.SetBuffer(PQuad, quadComputeBuffer);
            }
            quadComputeBuffer.SetData(quad, 0, 0, 4);
        }

        static readonly int PColor = Shader.PropertyToID("_ColorTexture");
        void UpdateColorTexture(Texture2D texture)
        {
            material.SetTexture(PColor, texture);

            if (texture == null)
            {
                material.EnableKeyword("USE_INTENSITY");
            }
            else
            {
                material.DisableKeyword("USE_INTENSITY");
            }
        }


        static readonly int PDepth = Shader.PropertyToID("_DepthTexture");
        void UpdateDepthTexture(Texture2D texture)
        {
            material.SetTexture(PDepth, texture);

            UpdatePointComputeBuffers(texture);
            UpdatePosValues(texture);
        }

        static readonly int PPoints = Shader.PropertyToID("_Points");
        void UpdatePointComputeBuffers(Texture2D sourceTexture)
        {
            if (sourceTexture is null || (sourceTexture.width == width && sourceTexture.height == height))
            {
                return;
            }

            width = sourceTexture.width;
            height = sourceTexture.height;
            uvs = new Vector2[width * height];
            float invWidth = 1.0f / width;
            float invHeight = 1.0f / height;
            int off = 0;
            for (int v = 0; v < height; v++)
            {
                for (int u = 0; u < width; u++, off++)
                {
                    uvs[off] = new Vector2((u + 0.5f) * invWidth, (v + 0.5f) * invHeight);
                }
            }

            pointComputeBuffer?.Release();
            pointComputeBuffer = new ComputeBuffer(uvs.Length, Marshal.SizeOf<Vector2>());
            pointComputeBuffer.SetData(uvs, 0, 0, uvs.Length);
            material.SetBuffer(PPoints, pointComputeBuffer);
        }

        static readonly int PIntensity = Shader.PropertyToID("_IntensityTexture");
        void UpdateColormap(Texture2D texture)
        {
            material.SetTexture(PIntensity, texture);
        }


        static readonly int PIntensityCoeff = Shader.PropertyToID("_IntensityCoeff");
        static readonly int PIntensityAdd = Shader.PropertyToID("_IntensityAdd");
        void UpdateIntensityValues(float intensityCoeff, float intensityAdd)
        {
            material.SetFloat(PIntensityCoeff, intensityCoeff);
            material.SetFloat(PIntensityAdd, intensityAdd);
        }

        static readonly int PropPointSize = Shader.PropertyToID("_PointSize");
        static readonly int PropPosST = Shader.PropertyToID("_Pos_ST");
        void UpdatePosValues(Texture2D texture)
        {
            if (texture is null)
            {
                return;
            }
            float ratio = (float)texture.height / texture.width;
            float posCoeffX = 2 * Mathf.Tan(FovAngle * Mathf.Deg2Rad / 2);
            float posCoeffY = posCoeffX * ratio;
            float posAddX = -0.5f * posCoeffX;
            float posAddY = -0.5f * posCoeffY;

            material.SetFloat(PropPointSize, posCoeffX / texture.width * ElementSize);
            material.SetVector(PropPosST, new Vector4(posCoeffX, posCoeffY, posAddX, posAddY));

            const float maxDepth = 5.0f;
            Vector3 size = new Vector3(posCoeffX, 1, posCoeffY) * maxDepth;
            Vector3 center = new Vector3(0, maxDepth / 2, 0);

            Collider.center = center;
            Collider.size = size;
        }


        static readonly int PLocalToWorld = Shader.PropertyToID("_LocalToWorld");
        static readonly int PWorldToLocal = Shader.PropertyToID("_WorldToLocal");
        static readonly int PScale = Shader.PropertyToID("_Scale");

        void Update()
        {
            if (DepthImage?.Texture is null)
            {
                return;
            }

            material.SetFloat(PScale, transform.lossyScale.x);
            material.SetMatrix(PLocalToWorld, transform.localToWorldMatrix);
            material.SetMatrix(PWorldToLocal, transform.worldToLocalMatrix);

            Graphics.DrawProcedural(material, WorldBounds, MeshTopology.Quads, 4, uvs.Length);
        }

        public override void Suspend()
        {
            base.Suspend();

            if (colorImage != null)
            {
                colorImage.TextureChanged -= UpdateColorTexture;
                colorImage = null;
            }
            if (depthImage != null)
            {
                depthImage.TextureChanged -= UpdateDepthTexture;
                depthImage.ColormapChanged -= UpdateColormap;
                depthImage = null;
            }
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
            }
            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
                quadComputeBuffer = null;
            }

            width = 0;
            height = 0;
            uvs = Array.Empty<Vector2>();
        }

        void OnDestroy()
        {
            if (material != null)
            {
                Destroy(material);
            }

            pointComputeBuffer?.Release();
            quadComputeBuffer?.Release();
        }

        void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                return;
            }
            // unity bug causes all compute buffers to disappear when focus is lost
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
            }
            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
                quadComputeBuffer = null;
            }

            uvs = Array.Empty<Vector2>();
            width = 0;
            height = 0;

            UpdateQuadComputeBuffer();
            ColorImage = ColorImage;
            DepthImage = DepthImage;
        }
    }
}