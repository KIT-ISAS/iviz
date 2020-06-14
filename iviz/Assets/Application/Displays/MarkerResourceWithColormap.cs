using UnityEngine;
using System.Collections;
using Iviz.Resources;
using System;

namespace Iviz.Displays
{
    public abstract class MarkerResourceWithColormap : MarkerResource, ISupportsTint
    {
        public static readonly int PropIntensityCoeff = Shader.PropertyToID("_IntensityCoeff");
        public static readonly int PropIntensityAdd = Shader.PropertyToID("_IntensityAdd");
        public static readonly int PropIntensity = Shader.PropertyToID("_IntensityTexture");
        static protected readonly int PropLocalToWorld = Shader.PropertyToID("_LocalToWorld");
        static protected readonly int PropWorldToLocal = Shader.PropertyToID("_WorldToLocal");
        static protected readonly int PropBoundaryCenter = Shader.PropertyToID("_BoundaryCenter");
        static protected readonly int PropPoints = Shader.PropertyToID("_Points");
        static protected readonly int PropTint = Shader.PropertyToID("_Tint");

        [SerializeField] protected Material material;

        [SerializeField] bool useIntensityTexture_;
        public bool UseIntensityTexture
        {
            get => useIntensityTexture_;
            set
            {
                if (useIntensityTexture_ == value)
                {
                    return;
                }
                useIntensityTexture_ = value;
                UpdateMaterialKeywords();
            }
        }

        protected virtual void UpdateMaterialKeywords()
        {
            if (UseIntensityTexture)
            {
                material.EnableKeyword("USE_TEXTURE");
            }
            else
            {
                material.DisableKeyword("USE_TEXTURE");
            }
        }


        [SerializeField] Resource.ColormapId colormap;
        public Resource.ColormapId Colormap
        {
            get => colormap;
            set
            {
                colormap = value;

                Texture2D texture = Resource.Colormaps.Textures[Colormap];
                material.SetTexture(PropIntensity, texture);
            }
        }

        [SerializeField] Vector2 intensityBounds_;
        public Vector2 IntensityBounds
        {
            get => intensityBounds_;
            set
            {
                intensityBounds_ = value;
                float intensitySpan = intensityBounds_.y - intensityBounds_.x;

                if (intensitySpan == 0)
                {
                    material.SetFloat(PropIntensityCoeff, 1);
                    material.SetFloat(PropIntensityAdd, 0);
                }
                else
                {
                    if (!FlipMinMax)
                    {
                        material.SetFloat(PropIntensityCoeff, 1 / intensitySpan);
                        material.SetFloat(PropIntensityAdd, -intensityBounds_.x / intensitySpan);
                    }
                    else
                    {
                        material.SetFloat(PropIntensityCoeff, -1 / intensitySpan);
                        material.SetFloat(PropIntensityAdd, intensityBounds_.y / intensitySpan);
                    }
                }
            }
        }

        [SerializeField] bool flipMinMax_;
        public bool FlipMinMax
        {
            get => flipMinMax_;
            set
            {
                flipMinMax_ = value;
                IntensityBounds = IntensityBounds;
            }
        }

        public override bool Visible
        {
            get => base.Visible;
            set
            {
                if (value && !base.Visible)
                {
                    Rebuild();
                }
                base.Visible = value;
            }
        }

        [SerializeField] Color tint_;
        public Color Tint
        {
            get => tint_;
            set
            {
                tint_ = value;
                material.SetColor(PropTint, value);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Tint = Color.white;
        }

        protected void UpdateTransform()
        {
            material.SetMatrix(PropLocalToWorld, transform.localToWorldMatrix);
            material.SetMatrix(PropWorldToLocal, transform.worldToLocalMatrix);
        }

        abstract protected void Rebuild();

        protected virtual void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                return;
            }
            Rebuild();
        }

        protected virtual void OnDestroy()
        {
            if (material != null)
            {
                Destroy(material);
            }
        }
    }
}
