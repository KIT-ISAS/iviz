using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
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
            Color = Color.white;
        }

        Color color;
        public Color Color
        {
            get => color;
            set
            {
                color = value;

                Material material = value.a > 254f / 255f ?
                    Resource.Materials.Lit.Object :
                    Resource.Materials.TransparentLit.Object;
                mainRenderer.material = material;
                mainRenderer.SetPropertyColor(color);
            }
        }

        public override string Name => "MeshMarker";

        public override void Stop()
        {
            base.Stop();
            Color = Color.white;
        }
    }
}