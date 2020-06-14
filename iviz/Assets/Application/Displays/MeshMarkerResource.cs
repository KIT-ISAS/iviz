using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    public class MeshMarkerResource : MarkerResource, ISupportsAROcclusion, ISupportsTint
    {
        MeshRenderer mainRenderer;

        [SerializeField] Color color = Color.white;
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                SetEffectiveColor();
            }
        }

        public override string Name => "MeshMarker";

        [SerializeField] bool occlusionOnly_;
        public bool OcclusionOnly
        {
            get => occlusionOnly_;
            set
            {
                occlusionOnly_ = value;
                if (value)
                {
                    mainRenderer.material = Resource.Materials.LitOcclusionOnly.Object;
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
            if (!OcclusionOnly)
            {
                Color effectiveColor = EffectiveColor;
                Material material = effectiveColor.a > 254f / 255f ?
                    Resource.Materials.Lit.Object :
                    Resource.Materials.TransparentLit.Object;
                mainRenderer.material = material;
                mainRenderer.SetPropertyColor(effectiveColor);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            mainRenderer = GetComponent<MeshRenderer>();
            Color = color;
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