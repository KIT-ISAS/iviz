using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App
{
    public class MeshMarkerResource : MarkerResource
    {
        MeshRenderer mainRenderer;
        protected override void Awake()
        {
            base.Awake();
            mainRenderer = GetComponent<MeshRenderer>();
        }

        public override void SetColor(Color color)
        {
            mainRenderer.SetPropertyColor(color);
        }
    }
}