using System;
using Iviz.Msgs.IvizCommonMsgs;
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
    [RequireComponent(typeof(BoxCollider))]
    public abstract class MarkerResourceWithColormap : MarkerResource, ISupportsTint
    {
        protected static readonly int IntensityCoeffId = Shader.PropertyToID("_IntensityCoeff");
        protected static readonly int IntensityAddId = Shader.PropertyToID("_IntensityAdd");
        static readonly int LocalToWorldId = Shader.PropertyToID("_LocalToWorld");
        static readonly int WorldToLocalId = Shader.PropertyToID("_WorldToLocal");
        static readonly int TintId = Shader.PropertyToID("_Tint");
        static readonly int AtlasRowId = Shader.PropertyToID("_AtlasRow");

        [SerializeField] ColormapId colormap;
        [SerializeField] Vector2 intensityBounds;
        [SerializeField] bool flipMinMax;
        [SerializeField] Color tint;
        [SerializeField] float elementScale = 1.0f;

        MaterialPropertyBlock properties;

        [NotNull]
        protected MaterialPropertyBlock Properties => properties ?? (properties = new MaterialPropertyBlock());

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
                Properties.SetFloat(AtlasRowId,
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
                var intensitySpan = intensityBounds.y - intensityBounds.x;

                float coeff, add;
                if (Mathf.Approximately(intensitySpan, 0))
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

                Properties.SetFloat(IntensityCoeffId, coeff);
                Properties.SetFloat(IntensityAddId, add);
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
        /// <exception cref="ArgumentException">If the size is negative.</exception>
        public virtual float ElementScale
        {
            get => elementScale;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Invalid elementScale {elementScale}", nameof(value));
                }

                elementScale = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();
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
                Properties.SetColor(TintId, value);
                UpdateProperties();
            }
        }

        protected void UpdateTransform()
        {
            Properties.SetMatrix(LocalToWorldId, transform.localToWorldMatrix);
            Properties.SetMatrix(WorldToLocalId, transform.worldToLocalMatrix);
            UpdateProperties();
        }

        protected virtual void UpdateProperties()
        {
        }

        protected abstract void Rebuild();
    }
}