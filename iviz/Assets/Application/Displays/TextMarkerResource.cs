using UnityEngine;

namespace Iviz.Displays
{
    public sealed class TextMarkerResource : MarkerResource
    {
        TextMesh textMesh;

        protected override void Awake()
        {
            base.Awake();
            textMesh = GetComponent<TextMesh>();
        }

        public string Text
        {
            get => textMesh.text;
            set
            {
                textMesh.text = value;
            }
        }

        public Color Color
        {
            get => textMesh.color;
            set => textMesh.color = value;
        }
    }
}