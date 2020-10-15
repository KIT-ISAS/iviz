using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Parent class for all displays that use a "mesh" (MeshFilter / MeshRenderer combo)
    /// </summary>
    public class MeshMarkerResource : MarkerResource, ISupportsTintAndAROcclusion
    {
        [SerializeField] Texture2D texture;
        [SerializeField] Color emissiveColor = Color.black;
        [SerializeField] Color color = Color.white;
        [SerializeField] Color tint = Color.white;
        [SerializeField] bool occlusionOnly;

        MeshRenderer mainRenderer;
        MeshFilter meshFilter;
        Material textureMaterial;
        Material textureMaterialAlpha;

        MeshRenderer MainRenderer => mainRenderer == null ? mainRenderer = GetComponent<MeshRenderer>() : mainRenderer;
        MeshFilter MeshFilter => meshFilter == null ? meshFilter = GetComponent<MeshFilter>() : meshFilter;

        public Texture2D Texture
        {
            get => texture;
            set
            {
                if (texture == value)
                {
                    return;
                }

                textureMaterial = null;
                textureMaterialAlpha = null;
                texture = value;
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

        public Mesh Mesh => MeshFilter.sharedMesh;

        protected override void Awake()
        {
            base.Awake();
            Color = color;
            EmissiveColor = emissiveColor;
            Tint = tint;

            MainRenderer.SetPropertyMainTexST(Vector2.zero, Vector2.one);
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
        }

        void SetEffectiveColor()
        {
            if (MainRenderer == null)
            {
                return;
            }

            if (OcclusionOnly)
            {
                return;
            }

            var effectiveColor = Color * Tint;
            if (Texture == null)
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
                    textureMaterial = Resource.TexturedMaterials.Get(Texture);
                }

                MainRenderer.material = textureMaterial;
            }
            else
            {
                if (textureMaterialAlpha == null)
                {
                    textureMaterialAlpha = Resource.TexturedMaterials.GetAlpha(Texture);
                }

                MainRenderer.sharedMaterial = textureMaterial;
            }

            MainRenderer.SetPropertyColor(effectiveColor);
        }
    }
}