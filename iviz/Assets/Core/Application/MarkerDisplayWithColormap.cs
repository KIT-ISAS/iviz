#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Parent class for displays that use instanced elements like points or lines.
    /// Instanced elements can be shown either with colors or with intensities and colormap textures.
    /// </summary>
    /// 
    public abstract class MarkerDisplayWithColormap : MarkerDisplay, ISupportsTint
    {
        ColormapId colormap;
        Vector2 intensityBounds;
        bool flipMinMax;
        Color tint;
        float elementScale = 1.0f;

        MaterialPropertyBlock? properties;

        protected MaterialPropertyBlock Properties => properties ??= new MaterialPropertyBlock();

        /// <summary>
        /// Whether to use the element color or the element intensity overlaid with a colormap. 
        /// </summary>
        public virtual bool UseColormap { get; set; }

        /// <summary>
        /// The colormap to be used if <see cref="UseColormap"/> is active.
        /// </summary>
        public ColormapId Colormap
        {
            get => colormap;
            set
            {
                colormap = value;
                Properties.SetFloat(ShaderIds.AtlasRowId,
                    (ColormapsType.AtlasSize - 0.5f - (float) value) / ColormapsType.AtlasSize);
                UpdateProperties();
            }
        }

        /// <summary>
        /// Gets or sets the bounds for the colormap. Min intensity is mapped to colormap position 0, max to 1. 
        /// </summary>
        public virtual Vector2 IntensityBounds
        {
            get => intensityBounds;
            set
            {
                intensityBounds = value;
                float intensitySpan = intensityBounds.y - intensityBounds.x;

                float coeff, add;
                if (intensitySpan.ApproximatelyZero())
                {
                    coeff = 1;
                    add = 0;
                }
                else if (!FlipMinMax)
                {
                    coeff = 1 / intensitySpan;
                    add = -intensityBounds.x / intensitySpan;
                }
                else
                {
                    coeff = -1 / intensitySpan;
                    add = intensityBounds.y / intensitySpan;
                }

                Properties.SetFloat(ShaderIds.IntensityCoeffId, coeff);
                Properties.SetFloat(ShaderIds.IntensityAddId, add);
                UpdateProperties();
            }
        }
        
        public Vector2 MeasuredIntensityBounds { get; protected set; }
        
        public bool OverrideIntensityBounds { get; set; }

        /// <summary>
        /// Whether to use a flipped version of IntensityBounds.
        /// </summary>
        public virtual bool FlipMinMax
        {
            get => flipMinMax;
            set
            {
                flipMinMax = value;
                IntensityBounds = IntensityBounds;
            }
        }

        /// <summary>
        /// Size of the instanced element (line, points, etc.).
        /// </summary>
        public virtual float ElementScale
        {
            get => elementScale;
            set
            {
                if (value < 0)
                {
                    return;
                }

                elementScale = value;
            }
        }

        protected virtual void Awake()
        {
            Tint = Color.white;
            IntensityBounds = new Vector2(0, 1);
            Layer = LayerType.IgnoreRaycast;
        }

        protected virtual void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                return;
            }

            Rebuild();
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

        /// <summary>
        /// Multiplies all colors with this.
        /// </summary>
        public virtual Color Tint
        {
            get => tint;
            set
            {
                tint = value;
                Properties.SetColor(ShaderIds.TintId, value);
                UpdateProperties();
            }
        }

        protected void UpdateTransform()
        {
            var mTransform = Transform;
            Properties.SetMatrix(ShaderIds.LocalToWorldId, mTransform.localToWorldMatrix);
            Properties.SetMatrix(ShaderIds.WorldToLocalId, mTransform.worldToLocalMatrix);
            UpdateProperties();
        }

        protected abstract void UpdateProperties();

        protected abstract void Rebuild();
    }
}