#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.Rendering;

namespace Iviz.Displays
{
    /// <summary>
    /// Parent class for all displays that use a "mesh" (MeshFilter / MeshRenderer combo)
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class MeshMarkerDisplay : MarkerDisplay,
        ISupportsColor, ISupportsTint, ISupportsAROcclusion, ISupportsPbr, ISupportsShadows
    {
        [SerializeField] Texture2D? diffuseTexture;
        [SerializeField] Texture2D? bumpTexture;
        [SerializeField] Color emissiveColor = Color.black;
        [SerializeField] Color color = Color.white;
        [SerializeField] MeshRenderer? mainRenderer;
        [SerializeField] MeshFilter? meshFilter;

        Color tint = Color.white;
        bool occlusionOnly;
        bool autoSelectMaterial = true;

        Material? textureMaterial;
        Material? textureMaterialAlpha;

        MeshRenderer MainRenderer => mainRenderer != null
            ? mainRenderer
            : mainRenderer = this.AssertHasComponent<MeshRenderer>(nameof(gameObject));

        MeshFilter MeshFilter => meshFilter != null
            ? meshFilter
            : meshFilter = this.AssertHasComponent<MeshFilter>(nameof(gameObject));

        public Texture2D? DiffuseTexture
        {
            get => diffuseTexture;
            set
            {
                if (diffuseTexture == value)
                {
                    return;
                }

                textureMaterial = null;
                textureMaterialAlpha = null;
                diffuseTexture = value;
                SetEffectiveColor();
            }
        }

        public Texture2D? BumpTexture
        {
            get => bumpTexture;
            set
            {
                if (bumpTexture == value)
                {
                    return;
                }

                textureMaterial = null;
                textureMaterialAlpha = null;
                bumpTexture = value;
                SetEffectiveColor();
            }
        }

        public Color EmissiveColor
        {
            get => emissiveColor;
            set
            {
                emissiveColor = value.WithAlpha(0);
                MainRenderer.SetPropertyEmissiveColor(emissiveColor);
            }
        }

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                SetEffectiveColor();
            }
        }

        public float Smoothness
        {
            set => MainRenderer.SetPropertySmoothness(value);
        }

        public float Metallic
        {
            set => MainRenderer.SetPropertyMetallic(value);
        }

        public bool EnableShadows
        {
            set
            {
                MainRenderer.shadowCastingMode = value ? ShadowCastingMode.On : ShadowCastingMode.Off;
                MainRenderer.receiveShadows = value;
            }
        }

        public Mesh Mesh
        {
            get => MeshFilter.sharedMesh.AssertNotNull(nameof(Mesh));
            set => MeshFilter.sharedMesh = value != null ? value : throw new NullReferenceException("Mesh is null");
        }

        protected virtual void Awake()
        {
            var sharedMaterial = MainRenderer.sharedMaterial;
            if (diffuseTexture == null
                && sharedMaterial != null
                && sharedMaterial.HasProperty(ShaderIds.MainTexId)
                && sharedMaterial.mainTexture != null)
            {
                diffuseTexture = (Texture2D)sharedMaterial.mainTexture;
            }

            Color = color;
            EmissiveColor = emissiveColor;
            Tint = tint;

            Metallic = 0.5f;
            Smoothness = 0.5f;

            MainRenderer.ResetPropertyTextureScale();
        }

        public bool OcclusionOnly
        {
            get => occlusionOnly;
            set
            {
                occlusionOnly = value;
                if (value)
                {
                    SetOcclusionMaterial();
                }
                else
                {
                    SetEffectiveColor();
                }
            }
        }

        void SetOcclusionMaterial()
        {
            if (autoSelectMaterial)
            {
                MainRenderer.sharedMaterial = Resource.Materials.LitOcclusionOnly.Object;
                MainRenderer.enabled = true;
            }
        }

        public Color Tint
        {
            get => tint;
            set
            {
                tint = value;
                SetEffectiveColor();
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            Color = Color.white;
            Tint = Color.white;
            EmissiveColor = Color.black;
            EnableCollider = true;
            OcclusionOnly = false;
            EnableShadows = true;
        }

        void SetEffectiveColor()
        {
            if (OcclusionOnly)
            {
                return;
            }

            var effectiveColor = Color * Tint;
            if (!autoSelectMaterial)
            {
                MainRenderer.SetPropertyColor(effectiveColor);
                return;
            }

            bool isOpaque = effectiveColor.a > 254f / 255;
            bool useSimpleMaterials = Settings.UseSimpleMaterials;

            if (DiffuseTexture == null && BumpTexture == null)
            {
                var material = (isOpaque, useSimpleMaterials) switch
                {
                    (true, false) => Resource.Materials.Lit.Object,
                    (false, false) => Resource.Materials.TransparentLit.Object,
                    (true, true) => Resource.Materials.SimpleLit.Object,
                    (false, true) => Resource.Materials.SimpleTransparentLit.Object,
                };
                MainRenderer.sharedMaterial = material;
            }
            else if (isOpaque)
            {
                if (textureMaterial == null)
                {
                    textureMaterial = Resource.TexturedMaterials.Get(DiffuseTexture, BumpTexture);
                }

                MainRenderer.sharedMaterial = textureMaterial;
            }
            else
            {
                if (textureMaterialAlpha == null)
                {
                    textureMaterialAlpha = Resource.TexturedMaterials.GetAlpha(DiffuseTexture, BumpTexture);
                }

                MainRenderer.sharedMaterial = textureMaterialAlpha;
            }

            MainRenderer.SetPropertyColor(effectiveColor);
            MainRenderer.enabled = effectiveColor.a > 0;
        }

        public void OverrideMaterial(Material? material)
        {
            if (material == null)
            {
                autoSelectMaterial = true;
            }
            else
            {
                MainRenderer.sharedMaterial = material;
                MainRenderer.enabled = true;
                autoSelectMaterial = false;
            }

            SetEffectiveColor();
        }

        public void UpdateMaterial()
        {
            textureMaterial = null;
            textureMaterialAlpha = null;
            SetEffectiveColor();
        }

#if UNITY_EDITOR
        // should only be used by the asset saver!
        public void SetMaterialValuesDirect(Texture2D texture, Color emissiveColor, Color color, Color tint)
        {
            diffuseTexture = texture;
            this.emissiveColor = emissiveColor;
            this.color = color;
            this.tint = tint;
        }
#endif
    }
}