#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XRDialogs
{
    public sealed class XRIconPlane : MonoBehaviour, ISupportsColor
    {
        [SerializeField] Texture2D[] icons = Array.Empty<Texture2D>();
        [SerializeField] MeshRenderer? meshRenderer;
        [SerializeField] Color color = Color.white;
        [SerializeField] Color emissiveColor = Color.white;

        MeshRenderer MeshRenderer => meshRenderer.AssertNotNull(nameof(meshRenderer));

        public Color Color
        {
            set
            {
                color = value;
                MeshRenderer.SetPropertyColor(value);
            }
        }

        public Color EmissiveColor
        {
            set
            {
                emissiveColor = value;
                MeshRenderer.SetPropertyEmissiveColor(value);
            }
        }

        public XRIcon Icon
        {
            set
            {
                if (value == XRIcon.None)
                {
                    gameObject.SetActive(false);
                    return;
                }

                gameObject.SetActive(true);
                int index = (int)value - 1;
                MeshRenderer.material = Resource.TexturedMaterials.GetAlpha(icons[index]);
            }
        }

        void Awake()
        {
            Color = color;
            EmissiveColor = emissiveColor;
        }
    }
}