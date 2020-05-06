using UnityEngine;

namespace Iviz.App
{
    public class TextMarkerResource : MarkerResource
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

        public override string Name => "TextMarker";

        public Color Color
        {
            get => textMesh.color;
            set => textMesh.color = value;
        }
    }
}