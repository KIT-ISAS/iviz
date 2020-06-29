using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    public class MeshMarkerResource : MarkerResource, ISupportsAROcclusion, ISupportsTint
    {
        protected MeshRenderer MainRenderer { get; private set; }

        Material textureMaterial;
        Material textureMaterialAlpha;

        [SerializeField] Texture2D texture_;
        public Texture2D Texture
        {
            get => texture_;
            set
            {
                if (texture_ == value)
                {
                    return;
                }
                textureMaterial = null;
                textureMaterialAlpha = null;
                texture_ = value;
                SetEffectiveColor();
            }
        }

        [SerializeField] Color color_ = Color.white;
        public Color Color
        {
            get => color_;
            set
            {
                color_ = value;
                SetEffectiveColor();
            }
        }

        [SerializeField] bool occlusionOnly_;
        public bool OcclusionOnly
        {
            get => occlusionOnly_;
            set
            {
                occlusionOnly_ = value;
                if (value)
                {
                    MainRenderer.material = Resource.Materials.LitOcclusionOnly.Object;
                }
                else
                {
                    SetEffectiveColor();
                }
            }
        }

        [SerializeField] Color tint_ = Color.white;
        public Color Tint
        {
            get => tint_;
            set
            {
                tint_ = value;
                SetEffectiveColor();
            }
        }

        Color EffectiveColor => Color * Tint;

        void SetEffectiveColor()
        {
            if (OcclusionOnly)
            {
                return;
            }
            Color effectiveColor = EffectiveColor;
            if (Texture is null)
            {
                Material material = effectiveColor.a > 254f / 255f ?
                    Resource.Materials.Lit.Object :
                    Resource.Materials.TransparentLit.Object;
                MainRenderer.material = material;
            }
            else if (effectiveColor.a > 254f / 255f)
            {
                if (textureMaterial is null)
                {
                    textureMaterial = Resource.TexturedMaterials.Get(Texture);
                }
                MainRenderer.material = textureMaterial;
            }
            else
            {
                if (textureMaterialAlpha is null)
                {
                    textureMaterialAlpha = Resource.TexturedMaterials.GetAlpha(Texture);
                }
                MainRenderer.material = textureMaterial;
            }
            MainRenderer.SetPropertyColor(effectiveColor);
        }

        protected override void Awake()
        {
            base.Awake();
            MainRenderer = GetComponent<MeshRenderer>();
            Color = color_;
        }

        public override void Stop()
        {
            base.Stop();
            Color = Color.white;
            ColliderEnabled = true;
            OcclusionOnly = false;
        }
    }
}