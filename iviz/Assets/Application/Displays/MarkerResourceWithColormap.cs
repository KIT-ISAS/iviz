using Iviz.Resources;
using UnityEngine;

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
        static readonly int AtlasRowID = Shader.PropertyToID("_AtlasRow");

        [SerializeField] Resource.ColormapId colormap;

        [SerializeField] Vector2 intensityBounds;

        [SerializeField] bool flipMinMax;

        [SerializeField] Color tint;

        [SerializeField] float elementSize = 1.0f;

        protected MaterialPropertyBlock properties;

        public bool UseColormap { get; set; }

        public Resource.ColormapId Colormap
        {
            get => colormap;
            set
            {
                colormap = value;
                properties.SetFloat(AtlasRowID, (float) value + 0.5f);
            }
        }

        public Vector2 IntensityBounds
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

                properties.SetFloat(IntensityCoeffID, coeff);
                properties.SetFloat(IntensityAddID, add);
            }
        }

        public bool FlipMinMax
        {
            get => flipMinMax;
            set
            {
                flipMinMax = value;
                IntensityBounds = IntensityBounds;
            }
        }

        /// <summary>
        ///     Size multiplier for internal/instantiated elements. Example: Line width in LineResource, point size in PointList.
        /// </summary>
        public float ElementSize
        {
            get => elementSize;
            set => elementSize = value;
        }

        protected override void Awake()
        {
            base.Awake();
            properties = new MaterialPropertyBlock();
            Tint = Color.white;
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

        public virtual Color Tint
        {
            get => tint;
            set
            {
                tint = value;
                properties.SetColor(TintID, value);
            }
        }

        protected void UpdateTransform()
        {
            properties.SetFloat(ScaleID, transform.lossyScale.x * ElementSize);
            properties.SetMatrix(LocalToWorldID, transform.localToWorldMatrix);
            properties.SetMatrix(WorldToLocalID, transform.worldToLocalMatrix);
        }

        protected abstract void Rebuild();
    }
}