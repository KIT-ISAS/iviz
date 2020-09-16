using UnityEngine;
using Iviz.Resources;
using UnityEngine.UIElements.Experimental;

namespace Iviz.Displays
{
    public abstract class MarkerResourceWithColormap : MarkerResource, ISupportsTint
    {
        public static readonly int PropIntensityCoeff = Shader.PropertyToID("_IntensityCoeff");
        public static readonly int PropIntensityAdd = Shader.PropertyToID("_IntensityAdd");
        public static readonly int PropIntensity = Shader.PropertyToID("_IntensityTexture");
        static readonly int PLocalToWorld = Shader.PropertyToID("_LocalToWorld");
        static readonly int PWorldToLocal = Shader.PropertyToID("_WorldToLocal");
        protected static readonly int PropBoundaryCenter = Shader.PropertyToID("_BoundaryCenter");
        protected static readonly int PropPoints = Shader.PropertyToID("_Points");
        static readonly int PTint = Shader.PropertyToID("_Tint");
        static readonly int PScale = Shader.PropertyToID("_Scale");

        [SerializeField] protected Material material;

        [SerializeField] bool useIntensityTexture;
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

        [SerializeField] Vector2 intensityBounds;
        public Vector2 IntensityBounds
        {
            get => intensityBounds;
            set
            {
                intensityBounds = value;
                float intensitySpan = intensityBounds.y - intensityBounds.x;
                
                if (Mathf.Approximately(intensitySpan,0))
                {
                    material.SetFloat(PropIntensityCoeff, 1);
                    material.SetFloat(PropIntensityAdd, 0);
                    return;
                }

                if (!FlipMinMax)
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

        [SerializeField] bool flipMinMax;
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

        [SerializeField] Color tint;

        public virtual Color Tint
        {
            get => tint;
            set
            {
                tint = value;
                material.SetColor(PTint, value);
            }
        }
        
        [SerializeField] float elementSize = 1.0f;

        /// <summary>
        /// Size multiplier for internal/instantiated elements. Example: Line width in LineResource, point size in PointList.
        /// </summary>
        public float ElementSize
        {
            get => elementSize;
            set => elementSize = value;
        }        

        protected override void Awake()
        {
            base.Awake();
            Tint = Color.white;
        }

        protected void UpdateTransform()
        {
            material.SetFloat(PScale, transform.lossyScale.x * ElementSize);
            material.SetMatrix(PLocalToWorld, transform.localToWorldMatrix);
            material.SetMatrix(PWorldToLocal, transform.worldToLocalMatrix);
        }

        protected abstract void Rebuild();

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
