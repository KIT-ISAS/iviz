using System;
using UnityEngine;
using UnityEngine.EventSystems;

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

        public override void SetColor(Color color)
        {
            textMesh.color = color;
        }
    }
}