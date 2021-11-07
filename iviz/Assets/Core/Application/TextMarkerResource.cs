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
    public sealed class TextMarkerResource : MarkerResource
    {
        [SerializeField] TextMesh? textMesh;
        [SerializeField] Billboard? billboard;
        [SerializeField] MeshRenderer? meshRenderer;

        TextMesh TextMesh => textMesh.AssertNotNull(nameof(textMesh));
        Billboard Billboard => billboard.AssertNotNull(nameof(billboard));
        MeshRenderer MeshRenderer => meshRenderer.AssertNotNull(nameof(meshRenderer));

        public string Text
        {
            get => TextMesh.text;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                TextMesh.text = value;
            }
        }

        public Color Color
        {
            get => TextMesh.color;
            set => TextMesh.color = value;
        }

        public bool BillboardEnabled
        {
            get => Billboard.enabled;
            set => Billboard.enabled = value;
        }

        public Vector3 BillboardOffset
        {
            get => Billboard.Offset;
            set => Billboard.Offset = value;
        }

        public float ElementSize
        {
            get => transform.localScale.x;
            set => transform.localScale = Vector3.one * value;
        }

        public bool AlwaysVisible
        {
            get => MeshRenderer.sharedMaterial == Resource.Materials.FontMaterial.Object;
            set => MeshRenderer.sharedMaterial =
                value ? Resource.Materials.FontMaterial.Object : Resource.Materials.FontMaterialZWrite.Object;
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