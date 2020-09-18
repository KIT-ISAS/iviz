using UnityEngine;
using Iviz.Resources;
using UnityEngine.UIElements.Experimental;

namespace Iviz.Displays
{
    public abstract class MarkerResourceWithColormap : MarkerResource, ISupportsTint
    {
        public static readonly int IntensityCoeffID = Shader.PropertyToID("_IntensityCoeff");
        public static readonly int IntensityAddID = Shader.PropertyToID("_IntensityAdd");
        public static readonly int IntensityID = Shader.PropertyToID("_IntensityTexture");
        static readonly int LocalToWorldID = Shader.PropertyToID("_LocalToWorld");
        static readonly int WorldToLocalID = Shader.PropertyToID("_WorldToLocal");
        protected static readonly int BoundaryCenterID = Shader.PropertyToID("_BoundaryCenter");
        protected static readonly int PointsID = Shader.PropertyToID("_Points");
        static readonly int TintID = Shader.PropertyToID("_Tint");
        static readonly int ScaleID = Shader.PropertyToID("_Scale");

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
                material.SetTexture(IntensityID, texture);
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
                    material.SetFloat(IntensityCoeffID, 1);
                    material.SetFloat(IntensityAddID, 0);
                    return;
                }

                if (!FlipMinMax)
                {
                    material.SetFloat(IntensityCoeffID, 1 / intensitySpan);
                    material.SetFloat(IntensityAddID, -intensityBounds.x / intensitySpan);
                }
                else
                {
                    material.SetFloat(IntensityCoeffID, -1 / intensitySpan);
                    material.SetFloat(IntensityAddID, intensityBounds.y / intensitySpan);
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
                material.SetColor(TintID, value);
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
            material.SetFloat(ScaleID, transform.lossyScale.x * ElementSize);
            material.SetMatrix(LocalToWorldID, transform.localToWorldMatrix);
            material.SetMatrix(WorldToLocalID, transform.worldToLocalMatrix);
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
