using System.Collections.Generic;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Application.Displays
{
    public interface IAnchorProvider
    {
        bool FindAnchor(in Vector3 position, out Vector3 anchor, out Vector3 normal);
    }

    public sealed class AnchorLine : WrapperResource, IRecyclable, ISupportsTint
    {
        readonly List<LineWithColor> lines = new List<LineWithColor>();
        LineResource resource;

        Vector3 lastPosition = Vector3.one * float.PositiveInfinity;

        public IAnchorProvider AnchorProvider { get; set; }

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
                    SetPosition(lastPosition, true);
                }
            }
        }

        public void SetPosition(in Vector3 newPosition, bool forceRebuild = false)
        {
            if (!forceRebuild && Mathf.Approximately((newPosition - lastPosition).sqrMagnitude, 0))
            {
                return;
            }

            lastPosition = newPosition;

            if (!Visible || AnchorProvider is null)
            {
                return;
            }

            lines.Clear();
            if (AnchorProvider.FindAnchor(lastPosition, out Vector3 anchor, out Vector3 normal))
            {
                LineUtils.AddLineStipple(lines, anchor, lastPosition, Color);
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

        public void SplitForRecycle()
        {
            ResourcePool.Dispose(Resource.Displays.Line, resource.gameObject);
        }
    }
}