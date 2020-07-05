using System.Collections.Generic;
using Iviz.App;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Application.Displays
{
    public class AnchorLine : WrapperResource, IRecyclable, ISupportsTint
    {
        LineResource resource;
        readonly List<LineWithColor> lines = new List<LineWithColor>();

        public delegate bool FindAnchorFn(in Vector3 position, out Vector3 anchor, out Vector3 normal);
        
        public FindAnchorFn FindAnchor { get; set; }
        
        protected override IDisplay Display => resource;
        
        Color color;
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                for (int i = 0; i < lines.Count; i++)
                {
                    LineWithColor prevLine = lines[i];
                    lines[i] = new LineWithColor(prevLine.A, prevLine.B, color);
                }
                resource.LinesWithColor = lines;
            }
        }

        Vector3 position = Vector3.one * float.PositiveInfinity;

        public Vector3 Position
        {
            get => position;
            set
            {
                if (Mathf.Approximately((value - Position).sqrMagnitude, 0))
                {
                    return;
                }
                position = value;
                UpdateLines();
            }
        }

        public void UpdateLines()
        {
            lines.Clear();
            if (FindAnchor(position, out Vector3 anchor, out Vector3 normal))
            {
                LineUtils.AddLineStipple(lines, anchor, position, Color.yellow);
                LineUtils.AddCircleStipple(lines, anchor, 0.25f, normal, Color.yellow);
            }
            resource.LinesWithColor = lines;
        }

        public Color Tint
        {
            get => resource.Tint;
            set => resource.Tint = value;
        }        
        
        void Awake()
        {
            resource = ResourcePool.GetOrCreate<LineResource>(Resource.Displays.Line, transform);
            resource.UseAlpha = false;
            resource.LineScale = 0.01f;
            Color = Color.yellow;
        }
        
        public void Recycle()
        {
            ResourcePool.Dispose(Resource.Displays.Line, resource.gameObject);
        }        
    }
}