using System;
using System.Linq;
using Iviz.Core;
using Iviz.Resources;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
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
        
        [FormerlySerializedAs("texture")] [SerializeField] Texture2D diffuseTexture;
        [SerializeField] Texture2D bumpTexture;
        [SerializeField] Color emissiveColor = Color.black;
        [SerializeField] Color color = Color.white;
        [SerializeField] Color tint = Color.white;
        [SerializeField] bool occlusionOnly;
        [SerializeField] float smoothness = Settings.IsHololens ? 0.25f : 0.5f;
        [SerializeField] float metallic = 0.5f;
        
        MeshRenderer mainRenderer;
        MeshFilter meshFilter;
        Material textureMaterial;
        Material textureMaterialAlpha;

        [NotNull]
        MeshRenderer MainRenderer => mainRenderer != null ? mainRenderer : mainRenderer = GetComponent<MeshRenderer>();

        [NotNull]
        MeshFilter MeshFilter => meshFilter != null ? meshFilter : meshFilter = GetComponent<MeshFilter>();

        [CanBeNull]
        public Texture2D DiffuseTexture
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

        [CanBeNull]
        public Texture2D BumpTexture
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
        
        public bool CastsShadows
        {
            get => MainRenderer.shadowCastingMode == ShadowCastingMode.On;
            set => MainRenderer.shadowCastingMode = value ? ShadowCastingMode.On : ShadowCastingMode.Off;
        }

        [NotNull]
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
            
            if (Settings.IsHololens)
            {
                var materials = MainRenderer.materials.ToArray();
                foreach (ref var material in materials.Ref()) 
                {
                    if (material.name == "White") // bug: fix this!
                    {
                        material = Resource.Materials.Lit.Object;
                    }
                }

                MainRenderer.materials = materials;
            }
        }

        public bool OcclusionOnly
        {
            get => occlusionOnly;
            set
            {
                occlusionOnly = value;
                if (value)
                {
                    MainRenderer.sharedMaterial = Resource.Materials.LitOcclusionOnly.Object;
                }
                else
                {
                    SetEffectiveColor();
                }
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
            CastsShadows = true;
        }

        void SetEffectiveColor()
        {
            if (MainRenderer == null || OcclusionOnly)
            {
                return;
            }

            var effectiveColor = Color * Tint;
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

        public void OverrideMaterial(Material material)
        {
            MainRenderer.sharedMaterial = material;
            var effectiveColor = Color * Tint;
            MainRenderer.SetPropertyColor(effectiveColor);
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