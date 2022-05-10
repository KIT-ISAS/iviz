#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(TextMesh))]
    [RequireComponent(typeof(Billboard))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class TextMarkerDisplay : MarkerDisplay
    {
        [SerializeField] TextMesh? textMesh;
        [SerializeField] Billboard? billboard;
        [SerializeField] MeshRenderer? meshRenderer;

        TextMesh TextMesh => textMesh.AssertNotNull(nameof(textMesh));
        Billboard Billboard => billboard.AssertNotNull(nameof(billboard));
        MeshRenderer MeshRenderer => meshRenderer.AssertNotNull(nameof(meshRenderer));

        public string Text
        {
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                TextMesh.text = value;
            }
        }

        public Color Color
        {
            set => TextMesh.color = value;
        }

        public bool BillboardEnabled
        {
            set => Billboard.enabled = value;
        }

        public Vector3 BillboardOffset
        {
            set => Billboard.Offset = value;
        }

        public float ElementSize
        {
            set => transform.localScale = Vector3.one * value;
        }

        public bool AlwaysVisible
        {
            set => MeshRenderer.sharedMaterial = value 
                ? Resource.Materials.FontMaterial.Object 
                : Resource.Materials.FontMaterialZWrite.Object;
        }

        public override void Suspend()
        {
            base.Suspend();
            ElementSize = 1;
            Color = Color.white;
            AlwaysVisible = false;
        }
    }
}