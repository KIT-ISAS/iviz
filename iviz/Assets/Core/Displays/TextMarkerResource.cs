using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(TextMesh))]
    [RequireComponent(typeof(Billboard))]
    public sealed class TextMarkerResource : MarkerResource
    {
        TextMesh textMesh;
        Billboard billboard;

        [NotNull] TextMesh TextMesh => textMesh != null ? textMesh : textMesh = GetComponent<TextMesh>();
        [NotNull] Billboard Billboard => billboard != null ? billboard : billboard = GetComponent<Billboard>();

        [NotNull]
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
            get => Billboard.offset;
            set => Billboard.offset = value;
        }
    }
}