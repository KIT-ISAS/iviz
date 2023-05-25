#nullable enable

using System;
using System.Runtime.CompilerServices;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.Rendering;

namespace Iviz.Displays
{
    public sealed class DepthCloudDisplay : MarkerDisplay
    {
        [SerializeField] Texture2D? atlas;
        [SerializeField] Material? material;
        [SerializeField] int width;
        [SerializeField] int height;
        ColormapId colormap;
        Vector2 intensityBounds;
        Intrinsic? intrinsic;
        float pointSize;
        bool flipMinMax;

        ImageTexture? colorImage;
        ImageTexture? depthImage;
        ComputeBuffer? pointComputeBuffer;

        Vector2[] uvs = Array.Empty<Vector2>();

        Material Material => material != null
            ? material
            : (material = Resource.Materials.DepthCloud.Instantiate());

        Texture2D Atlas => atlas.AssertNotNull(nameof(atlas));

        public Intrinsic? Intrinsic
        {
            set
            {
                if (value is not { IsValid: true } || Nullable.Equals(intrinsic, value))
                {
                    return;
                }

                intrinsic = value;
                if (DepthImage != null)
                {
                    UpdateIntrinsicScaling(DepthImage.Texture);
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
                    depthImage.TextureChanged -= UpdateDepthTexture;
                }

                depthImage = value;
                if (value != null)
                {
                    UpdateDepthTexture(value.Texture);
                    value.TextureChanged += UpdateDepthTexture;
                }
                else
                {
                    UpdateDepthTexture(null);
                }
            }
        }

        public Vector2 IntensityBounds
        {
            get => intensityBounds;
            set
            {
                intensityBounds = value;

                float intensitySpan = intensityBounds.y - intensityBounds.x;

                if (intensitySpan == 0)
                {
                    Material.SetFloat(ShaderIds.IntensityCoeffId, 1);
                    Material.SetFloat(ShaderIds.IntensityAddId, 0);
                }
                else
                {
                    if (!FlipMinMax)
                    {
                        Material.SetFloat(ShaderIds.IntensityCoeffId, 1 / intensitySpan);
                        Material.SetFloat(ShaderIds.IntensityAddId, -intensityBounds.x / intensitySpan);
                    }
                    else
                    {
                        Material.SetFloat(ShaderIds.IntensityCoeffId, -1 / intensitySpan);
                        Material.SetFloat(ShaderIds.IntensityAddId, intensityBounds.y / intensitySpan);
                    }
                }
            }
        }

        public bool FlipMinMax
        {
            get => flipMinMax;
            set
            {
                flipMinMax = value;
                IntensityBounds = IntensityBounds;
            }
        }

        public ColormapId Colormap
        {
            get => colormap;
            set
            {
                colormap = value;
                Material.SetTexture(ShaderIds.ColorTextureId, Atlas);
                Material.SetFloat(ShaderIds.AtlasRowId,
                    (ColormapsType.AtlasSize - 0.5f - (float)value) / ColormapsType.AtlasSize);
                Material.DisableKeyword("USE_COLOR_GRAY");
                Material.EnableKeyword("USE_INTENSITY");
            }
        }

        void Awake()
        {
            Layer = LayerType.IgnoreRaycast;
        }

        void Update()
        {
            if (DepthImage?.Texture == null)
            {
                return;
            }

            var mMaterial = Material;
            mMaterial.SetFloat(ShaderIds.PointSizeId, Transform.lossyScale.x * pointSize);
            mMaterial.SetMatrix(ShaderIds.LocalToWorldId, Transform.localToWorldMatrix);
            mMaterial.SetMatrix(ShaderIds.WorldToLocalId, Transform.worldToLocalMatrix);

            Graphics.DrawProcedural(mMaterial, WorldBounds, MeshTopology.Quads, 4, uvs.Length,
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

            if (intrinsic != null)
            {
                Intrinsic = intrinsic;
            }

            if (ColorImage?.Texture != null)
            {
                return;
            }

            Colormap = Colormap;
            IntensityBounds = IntensityBounds;
        }

        void UpdateColorTexture(Texture2D? texture)
        {
            Material.SetTexture(ShaderIds.ColorTextureId, texture);

            if (texture == null)
            {
                Material.DisableKeyword("USE_COLOR_GRAY");
                Material.EnableKeyword("USE_INTENSITY");
            }
            else
            {
                Material.DisableKeyword("USE_INTENSITY");
                if (texture.format is TextureFormat.R8 or TextureFormat.R16 or TextureFormat.RFloat)
                {
                    Material.EnableKeyword("USE_COLOR_GRAY");
                }
                else
                {
                    Material.DisableKeyword("USE_COLOR_GRAY");
                }
            }
        }

        void UpdateDepthTexture(Texture2D? texture)
        {
            Material.SetTexture(ShaderIds.DepthTextureId, texture);

            UpdatePointComputeBuffers(texture);
            UpdateIntrinsicScaling(texture);

            if (depthImage?.Texture == null)
            {
                return;
            }

            float depthScale =
                depthImage.Texture.format switch
                {
                    TextureFormat.RFloat => 1,
                    TextureFormat.R16 => 65.535f, // 0..65535 values in mm to m
                    TextureFormat.R8 => 2.55f, // 0..255 values in cm to m
                    _ => 1
                };

            Material.SetFloat(ShaderIds.DepthScaleId, depthScale);
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
            for (int v = 0; v < height; v++)
            {
                for (int u = 0; u < width; u++)
                {
                    uvs[off++] = new Vector2((u + 0.5f) * invWidth, (v + 0.5f) * invHeight);
                }
            }

            pointComputeBuffer?.Release();
            pointComputeBuffer = new ComputeBuffer(uvs.Length, Unsafe.SizeOf<Vector2>());
            pointComputeBuffer.SetData(uvs, 0, 0, uvs.Length);
            Material.SetBuffer(ShaderIds.PointsId, pointComputeBuffer);
        }

        void UpdateIntrinsicScaling(Texture? texture)
        {
            if (texture == null)
            {
                return;
            }

            var mIntrinsic = intrinsic ??
                             // create default intrinsic so we can at least see something
                             new Intrinsic(60 * Mathf.Deg2Rad, texture.width, texture.height);

            float posMulX = texture.width / mIntrinsic.Fx;
            float posMulY = texture.height / mIntrinsic.Fy;

            float posAddX = -mIntrinsic.Cx / mIntrinsic.Fx;
            float posAddY = -mIntrinsic.Cy / mIntrinsic.Fy;

            pointSize = 1f / mIntrinsic.Fx;

            Material.SetVector(ShaderIds.PosStId, new Vector4(posMulX, posMulY, posAddX, posAddY));

            const float maxDepthForBounds = 5.0f;
            var size = new Vector3(posMulX, 1, posMulY) * maxDepthForBounds;
            var center = new Vector3(0, maxDepthForBounds / 2, 0);

            Collider.SetLocalBounds(new Bounds(center, size));
        }

        public override void Suspend()
        {
            base.Suspend();

            ColorImage = null;
            DepthImage = null;

            pointComputeBuffer?.Release();
            pointComputeBuffer = null;

            Material.SetTexture(ShaderIds.ColorTextureId, null);
            Material.SetTexture(ShaderIds.DepthTextureId, null);

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