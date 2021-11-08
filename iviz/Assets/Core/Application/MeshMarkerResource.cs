#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace Iviz.Displays
{
    /// <summary>
    /// Parent class for all displays that use a "mesh" (MeshFilter / MeshRenderer combo)
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(BoxCollider))]
    public class MeshMarkerResource : MarkerResource, ISupportsTint, ISupportsAROcclusion, ISupportsPbr
    {
        static readonly int MainTex = Shader.PropertyToID("_MainTex");

        [SerializeField] Texture2D? diffuseTexture;
        [SerializeField] Texture2D? bumpTexture;
        [SerializeField] Color emissiveColor = Color.black;
        [SerializeField] Color color = Color.white;
        [SerializeField] Color tint = Color.white;
        [SerializeField] bool occlusionOnly;
        [SerializeField] float smoothness = Settings.IsHololens ? 0.25f : 0.5f;
        [SerializeField] float metallic = 0.5f;
        [SerializeField] MeshRenderer? mainRenderer;
        [SerializeField] MeshFilter? meshFilter;

        Material? textureMaterial;
        Material? textureMaterialAlpha;
        [SerializeField] bool autoSelectMaterial = true;
        bool shadowsEnabled = true;

        MeshRenderer MainRenderer => mainRenderer != null ? mainRenderer : mainRenderer = GetComponent<MeshRenderer>();

        MeshFilter MeshFilter => meshFilter != null ? meshFilter : meshFilter = GetComponent<MeshFilter>();


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
                emissiveColor = value;
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
            get => smoothness;
            set
            {
                smoothness = value;
                MainRenderer.SetPropertySmoothness(smoothness);
            }
        }

        public float Metallic
        {
            get => metallic;
            set
            {
                metallic = value;
                MainRenderer.SetPropertyMetallic(metallic);
            }
        }

        public bool ShadowsEnabled
        {
            get => shadowsEnabled;
            set
            {
                shadowsEnabled = value;
                MainRenderer.shadowCastingMode = value ? ShadowCastingMode.On : ShadowCastingMode.Off;   
                MainRenderer.receiveShadows = value;   
            }
        }
        
        public Mesh Mesh
        {
            get => MeshFilter.sharedMesh;
            set => MeshFilter.sharedMesh = value != null ? value : throw new NullReferenceException("Mesh is null");
        }

        protected override void Awake()
        {
            base.Awake();

            var sharedMaterial = MainRenderer.sharedMaterial;
            if (diffuseTexture == null
                && sharedMaterial != null
                && sharedMaterial.HasProperty(MainTex)
                && sharedMaterial.mainTexture != null)
            {
                diffuseTexture = (Texture2D) sharedMaterial.mainTexture;
            }

            Color = color;
            EmissiveColor = emissiveColor;
            Tint = tint;
            Metallic = metallic;
            Smoothness = smoothness;

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
            EmissiveColor = Color.black;
            ColliderEnabled = true;
            OcclusionOnly = false;
            ShadowsEnabled = true;
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

            if (DiffuseTexture == null && BumpTexture == null)
            {
                var material = effectiveColor.a > 254f / 255f
                    ? Resource.Materials.Lit.Object
                    : Resource.Materials.TransparentLit.Object;
                MainRenderer.sharedMaterial = material;
            }
            else if (effectiveColor.a > 254f / 255f)
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
                autoSelectMaterial = false;
            }

            SetEffectiveColor();
        }

        // should only be used by the asset saver!
        public void SetMaterialValuesDirect(Texture2D texture, Color emissiveColor, Color color, Color tint)
        {
            diffuseTexture = texture;
            this.emissiveColor = emissiveColor;
            this.color = color;
            this.tint = tint;
        }
    }
}