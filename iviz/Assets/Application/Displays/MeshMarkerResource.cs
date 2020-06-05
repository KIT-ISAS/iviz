using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Iviz.Displays
{
    public class MeshMarkerResource : MarkerResource
    {
        MeshRenderer mainRenderer;
        protected override void Awake()
        {
            base.Awake();
            mainRenderer = GetComponent<MeshRenderer>();
        }

        Color color;
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                mainRenderer.SetPropertyColor(color);
            }
        }

        public override string Name => "MeshMarker";
    }
}