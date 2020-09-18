using UnityEngine;

namespace Iviz.Displays
{
    public sealed class TextMarkerResource : MarkerResource
    {
        TextMesh textMesh;
        Billboard billboard;

        protected override void Awake()
        {
            base.Awake();
            textMesh = GetComponent<TextMesh>();
            billboard = GetComponent<Billboard>();
        }

        public string Text
        {
            get => textMesh.text;
            set => textMesh.text = value;
        }

        public Color Color
        {
            get => textMesh.color;
            set => textMesh.color = value;
        }
        
        public bool BillboardEnabled
        {
            get => billboard.enabled;
            set => billboard.enabled = value;
        }
    }
}