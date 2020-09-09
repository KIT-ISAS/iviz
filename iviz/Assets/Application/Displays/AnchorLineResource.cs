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

    public sealed class AnchorLineResource : DisplayWrapperResource, IRecyclable, ISupportsTint
    {
        [SerializeField] public GameObject anchorPlane;
        
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

        public Vector3? SetPosition(in Vector3 newPosition, bool forceRebuild = false)
        {
            if (!forceRebuild && Mathf.Approximately((newPosition - lastPosition).sqrMagnitude, 0))
            {
                return null;
            }

            lastPosition = newPosition;

            if (!Visible || AnchorProvider is null)
            {
                return null;
            }

            bool foundAnchor = AnchorProvider.FindAnchor(lastPosition, out Vector3 anchor, out Vector3 normal);
            
            lines.Clear();
            if (foundAnchor)
            {
                LineUtils.AddLineStipple(lines, anchor, lastPosition, Color);
                //LineUtils.AddCircleStipple(lines, anchor, 0.125f, normal, Color);
                
                Vector3 right = (normal == Vector3.right) ? Vector3.forward : Vector3.right;
                Vector3 forward = Vector3.Cross(right, normal).normalized;
                anchorPlane.transform.LookAt(anchor + forward, normal);
                anchorPlane.transform.position = anchor;
            }

            anchorPlane.SetActive(foundAnchor);
            resource.LinesWithColor = lines;
            
            return foundAnchor ? anchor : (Vector3?)null;
        }

        public Color Tint
        {
            get => resource.Tint;
            set => resource.Tint = value;
        }

        void Awake()
        {
            resource = ResourcePool.GetOrCreate<LineResource>(Resource.Displays.Line, transform);
            resource.UseAlpha = true;
            resource.LineScale = 0.003f;
            Color = new Color(1, 1, 0, 0.25f);
        }

        public void SplitForRecycle()
        {
            ResourcePool.Dispose(Resource.Displays.Line, resource.gameObject);
        }
    }
}