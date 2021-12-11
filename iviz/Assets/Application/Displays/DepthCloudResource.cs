#nullable enable

using System;
using System.Runtime.InteropServices;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.Rendering;

namespace Iviz.Displays
{
    public sealed class DepthCloudResource : MarkerResource
    {
        static readonly int PColor = Shader.PropertyToID("_ColorTexture");
        static readonly int PDepth = Shader.PropertyToID("_DepthTexture");
        static readonly int PPoints = Shader.PropertyToID("_Points");
        static readonly int PIntensity = Shader.PropertyToID("_IntensityTexture");
        static readonly int PropPointSize = Shader.PropertyToID("_PointSize");
        static readonly int PropPosSt = Shader.PropertyToID("_Pos_ST");
        static readonly int PLocalToWorld = Shader.PropertyToID("_LocalToWorld");
        static readonly int PWorldToLocal = Shader.PropertyToID("_WorldToLocal");
        static readonly int PDepthScale = Shader.PropertyToID("_DepthScale");

        [SerializeField] Material? material = null;
        [SerializeField] int width;
        [SerializeField] int height;
        Intrinsic? intrinsic;
        float pointSize;

        ImageTexture? colorImage;
        ImageTexture? depthImage;
        ComputeBuffer? pointComputeBuffer;

        Vector2[] uvs = Array.Empty<Vector2>();

        Material Material => material != null
            ? material
            : (material = Resource.Materials.DepthCloud.Instantiate());

        public Intrinsic Intrinsic
        {
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

        public ImageTexture? ColorImage
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

        public ImageTexture? DepthImage
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

        void Awake()
        {
            Layer = LayerType.IgnoreRaycast;
        }

        void Update()
        {
            if (DepthImage?.Texture == null || Material == null)
            {
                return;
            }

            Material.SetFloat(PropPointSize, Transform.lossyScale.x * pointSize);
            Material.SetMatrix(PLocalToWorld, Transform.localToWorldMatrix);
            Material.SetMatrix(PWorldToLocal, Transform.worldToLocalMatrix);

            Graphics.DrawProcedural(Material, WorldBounds, MeshTopology.Quads, 4, uvs.Length,
                castShadows: ShadowCastingMode.Off, receiveShadows: false, layer: gameObject.layer);
        }

        public void SetIntrinsic(double[] array)
        {
            if (intrinsic == null || !intrinsic.Equals(array))
            {
                Intrinsic = new Intrinsic(array);
            }
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

        void UpdateColorTexture(Texture2D? texture)
        {
            Material.SetTexture(PColor, texture);

            if (texture == null)
            {
                Material.EnableKeyword("USE_INTENSITY");
            }
            else
            {
                Material.DisableKeyword("USE_INTENSITY");
            }
        }

        void UpdateDepthTexture(Texture2D? texture)
        {
            Material.SetTexture(PDepth, texture);

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
                    Material.SetFloat(PDepthScale, 1);
                    break;
                case TextureFormat.R16:
                    depthImage.IntensityBounds = new Vector2(0, 5000 / 65535f);
                    Material.SetFloat(PDepthScale, 65.535f);
                    break;
            }
        }

        void UpdatePointComputeBuffers(Texture2D? sourceTexture)
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
            foreach (int v in ..height)
            {
                foreach (int u in ..width)
                {
                    uvs[off] = new Vector2((u + 0.5f) * invWidth, (v + 0.5f) * invHeight);
                    off++;
                }
            }

            pointComputeBuffer?.Release();
            pointComputeBuffer = new ComputeBuffer(uvs.Length, Marshal.SizeOf<Vector2>());
            pointComputeBuffer.SetData(uvs, 0, 0, uvs.Length);
            Material.SetBuffer(PPoints, pointComputeBuffer);
        }

        void UpdateColormap(Texture2D? texture)
        {
            Material.SetTexture(PIntensity, texture);
        }

        void UpdatePosValues(Texture? texture)
        {
            if (texture == null)
            {
                return;
            }

            var mIntrinsic = intrinsic is { } validatedIntrinsic
                ? validatedIntrinsic
                // create default intrinsic so we can at least see something
                : new Intrinsic(60 * Mathf.Deg2Rad, texture.width, texture.height);

            float posMulX = texture.width / mIntrinsic.Fx;
            float posMulY = texture.height / mIntrinsic.Fy;

            float posAddX = -mIntrinsic.Cx / mIntrinsic.Fx;
            float posAddY = -mIntrinsic.Cy / mIntrinsic.Fy;

            pointSize = 1f / mIntrinsic.Fx;

            Material.SetVector(PropPosSt, new Vector4(posMulX, posMulY, posAddX, posAddY));

            const float maxDepthForBounds = 5.0f;
            var size = new Vector3(posMulX, 1, posMulY) * maxDepthForBounds;
            var center = new Vector3(0, maxDepthForBounds / 2, 0);

            Collider.center = center;
            Collider.size = size;
        }

        public override void Suspend()
        {
            base.Suspend();

            ColorImage = null;
            DepthImage = null;

            pointComputeBuffer?.Release();
            pointComputeBuffer = null;

            Material.SetTexture(PColor, null);
            Material.SetTexture(PDepth, null);

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