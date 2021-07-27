using System;
using System.Runtime.InteropServices;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

namespace Iviz.Displays
{
    public sealed class DepthCloudResource : MarkerResource
    {
        //static readonly int PQuad = Shader.PropertyToID("_Quad");
        static readonly int PColor = Shader.PropertyToID("_ColorTexture");
        static readonly int PDepth = Shader.PropertyToID("_DepthTexture");
        static readonly int PPoints = Shader.PropertyToID("_Points");
        static readonly int PIntensity = Shader.PropertyToID("_IntensityTexture");
        static readonly int PIntensityCoeff = Shader.PropertyToID("_IntensityCoeff");
        static readonly int PIntensityAdd = Shader.PropertyToID("_IntensityAdd");
        static readonly int PropPointSize = Shader.PropertyToID("_PointSize");
        static readonly int PropPosSt = Shader.PropertyToID("_Pos_ST");
        static readonly int PLocalToWorld = Shader.PropertyToID("_LocalToWorld");
        static readonly int PWorldToLocal = Shader.PropertyToID("_WorldToLocal");
        static readonly int PScale = Shader.PropertyToID("_Scale");
        static readonly int PDepthScale = Shader.PropertyToID("_DepthScale");

        [SerializeField] Material material;
        [SerializeField] float elementScale = 1;
        [SerializeField] int width;
        [SerializeField] int height;
        Intrinsic intrinsic;
        float pointSize;

        [CanBeNull] ImageTexture colorImage;
        [CanBeNull] ImageTexture depthImage;
        [CanBeNull] ComputeBuffer pointComputeBuffer;

        [NotNull] Vector2[] uvs = Array.Empty<Vector2>();

        public Intrinsic Intrinsic
        {
            get => intrinsic;
            set
            {
                if (value.Equals(intrinsic))
                {
                    return;
                }

                intrinsic = value;
                if (DepthImage != null)
                {
                    UpdatePosValues(DepthImage.Texture);
                }
            }
        }

        [CanBeNull]
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
                if (value != null)
                {
                    UpdateColorTexture(value.Texture);
                    value.TextureChanged += UpdateColorTexture;
                }
                else
                {
                    UpdateColorTexture(null);
                }
            }
        }

        [CanBeNull]
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
                if (value != null)
                {
                    UpdateDepthTexture(value.Texture);
                    UpdateColormap(value.ColormapTexture);
                    value.TextureChanged += UpdateDepthTexture;
                    value.ColormapChanged += UpdateColormap;
                }
                else
                {
                    UpdateDepthTexture(null);
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            material = Resource.Materials.DepthCloud.Instantiate();
        }

        void Update()
        {
            if (DepthImage?.Texture == null)
            {
                return;
            }

            material.SetFloat(PropPointSize, Transform.lossyScale.x * pointSize);
            material.SetMatrix(PLocalToWorld, Transform.localToWorldMatrix);
            material.SetMatrix(PWorldToLocal, Transform.worldToLocalMatrix);

            Graphics.DrawProcedural(material, WorldBounds, MeshTopology.Quads, 4, uvs.Length,
                castShadows: ShadowCastingMode.Off, receiveShadows: false, layer: gameObject.layer);
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

            uvs = Array.Empty<Vector2>();
            width = 0;
            height = 0;

            ColorImage = ColorImage;
            DepthImage = DepthImage;
        }

        void UpdateColorTexture([CanBeNull] Texture2D texture)
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

        void UpdateDepthTexture([CanBeNull] Texture2D texture)
        {
            material.SetTexture(PDepth, texture);

            UpdatePointComputeBuffers(texture);
            UpdatePosValues(texture);

            if (depthImage == null || depthImage.Texture == null)
            {
                return;
            }

            switch (depthImage.Texture.format)
            {
                case TextureFormat.RFloat:
                    depthImage.IntensityBounds = new Vector2(0, 5);
                    material.SetFloat(PDepthScale, 1);
                    break;
                case TextureFormat.R16:
                    depthImage.IntensityBounds = new Vector2(0, 5000 / 65535f);
                    material.SetFloat(PDepthScale, 65.535f);
                    break;
            }
        }

        void UpdatePointComputeBuffers([CanBeNull] Texture2D sourceTexture)
        {
            if (sourceTexture == null
                || sourceTexture.width == width && sourceTexture.height == height)
            {
                return;
            }

            width = sourceTexture.width;
            height = sourceTexture.height;
            uvs = new Vector2[width * height]; // mostly > 1 MB, not worth renting
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

        void UpdateColormap([CanBeNull] Texture2D texture)
        {
            material.SetTexture(PIntensity, texture);
        }

        void UpdateIntensityValues(float intensityCoeff, float intensityAdd)
        {
            material.SetFloat(PIntensityCoeff, intensityCoeff);
            material.SetFloat(PIntensityAdd, intensityAdd);
        }

        void UpdatePosValues([CanBeNull] Texture2D texture)
        {
            if (texture == null)
            {
                return;
            }

            float posMulX = texture.width / intrinsic.Fx;
            float posMulY = texture.height / intrinsic.Fy;

            float posAddX = -intrinsic.Cx / intrinsic.Fx;
            float posAddY = -intrinsic.Cy / intrinsic.Fy;

            pointSize = 1f / intrinsic.Fx;

            material.SetVector(PropPosSt, new Vector4(posMulX, posMulY, posAddX, posAddY));

            const float maxDepthForBounds = 5.0f;
            Vector3 size = new Vector3(posMulX, 1, posMulY) * maxDepthForBounds;
            Vector3 center = new Vector3(0, maxDepthForBounds / 2, 0);

            BoxCollider.center = center;
            BoxCollider.size = size;
        }

        public override void Suspend()
        {
            base.Suspend();

            ColorImage = null;
            DepthImage = null;

            pointComputeBuffer?.Release();
            pointComputeBuffer = null;

            material.SetTexture(PColor, null);
            material.SetTexture(PDepth, null);

            width = 0;
            height = 0;
            uvs = Array.Empty<Vector2>();
        }

        void OnDestroy()
        {
            ColorImage = null;
            DepthImage = null;

            pointComputeBuffer?.Release();

            if (material != null)
            {
                Destroy(material);
            }
        }
    }
}