using System.Collections.Generic;
using Iviz.App;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Application.Displays
{
    public sealed class AnchorLine : WrapperResource, IRecyclable, ISupportsTint
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

        public override bool Visible
        {
            get => base.Visible;
            set
            {
                base.Visible = value;
                if (value)
                {
                    SetPosition(Position, true);
                }
            }
        }

        Vector3 position = Vector3.one * float.PositiveInfinity;

        public Vector3 Position
        {
            get => position;
            set => SetPosition(value);
        }

        public void SetPosition(in Vector3 newPosition, bool forceRebuild = false)
        {
            if (!forceRebuild && Mathf.Approximately((newPosition - Position).sqrMagnitude, 0))
            {
                return;
            }
            position = newPosition;

            if (!Visible || FindAnchor is null)
            {
                return;
            }
            lines.Clear();
            if (FindAnchor(position, out Vector3 anchor, out Vector3 normal))
            {
                LineUtils.AddLineStipple(lines, anchor, position, Color);
                LineUtils.AddCircleStipple(lines, anchor, 0.125f, normal, Color);
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
            resource.LineScale = 0.003f;
            Color = Color.yellow;
        }
        
        public void Recycle()
        {
            ResourcePool.Dispose(Resource.Displays.Line, resource.gameObject);
        }        
    }
}