using UnityEngine;
using System.Collections;
using Iviz.Resources;
using System;

namespace Iviz.Displays
{
    public abstract class MarkerResourceWithColormap : MarkerResource
    {
        static protected readonly int PropIntensityCoeff = Shader.PropertyToID("_IntensityCoeff");
        static protected readonly int PropIntensityAdd = Shader.PropertyToID("_IntensityAdd");
        static protected readonly int PropIntensity = Shader.PropertyToID("_IntensityTexture");
        static protected readonly int PropLocalToWorld = Shader.PropertyToID("_LocalToWorld");
        static protected readonly int PropWorldToLocal = Shader.PropertyToID("_WorldToLocal");
        static protected readonly int PropBoundaryCenter = Shader.PropertyToID("_BoundaryCenter");
        static protected readonly int PropPoints = Shader.PropertyToID("_Points");

        protected Material material;

        bool useIntensityTexture;
        public bool UseIntensityTexture
        {
            get => useIntensityTexture;
            set
            {
                if (useIntensityTexture == value)
                {
                    return;
                }
                useIntensityTexture = value;
                UpdateMaterialKeywords();
            }
        }

        protected virtual void UpdateMaterialKeywords()
        {
            if (UseIntensityTexture)
            {
                material.DisableKeyword("USE_TEXTURE_SCALE");
                material.EnableKeyword("USE_TEXTURE");
            }
            else
            {
                material.DisableKeyword("USE_TEXTURE_SCALE");
                material.DisableKeyword("USE_TEXTURE");
            }
        }


        Resource.ColormapId colormap;
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

        Vector2 intensityBounds;
        public Vector2 IntensityBounds
        {
            get => intensityBounds;
            set
            {
                intensityBounds = value;
                float intensitySpan = intensityBounds.y - intensityBounds.x;

                if (intensitySpan == 0)
                {
                    material.SetFloat(PropIntensityCoeff, 1);
                    material.SetFloat(PropIntensityAdd, 0);
                }
                else
                {
                    if (FlipMinMax)
                    {
                        material.SetFloat(PropIntensityCoeff, 1 / intensitySpan);
                        material.SetFloat(PropIntensityAdd, -intensityBounds.x / intensitySpan);
                    }
                    else
                    {
                        material.SetFloat(PropIntensityCoeff, -1 / intensitySpan);
                        material.SetFloat(PropIntensityAdd, intensityBounds.y / intensitySpan);
                    }
                }
            }
        }

        bool flipMinMax;
        public bool FlipMinMax
        {
            get => flipMinMax;
            set
            {
                flipMinMax = value;
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

        protected void UpdateTransform()
        {
            material.SetMatrix(PropLocalToWorld, transform.localToWorldMatrix);
            material.SetMatrix(PropWorldToLocal, transform.worldToLocalMatrix);
        }

        abstract protected void Rebuild();

        void OnApplicationFocus(bool hasFocus)
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
