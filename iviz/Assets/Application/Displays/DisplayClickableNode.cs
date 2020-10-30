using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class DisplayClickableNode : ClickableNode
    {
        IDisplay target;
        public IDisplay Target
        {
            get => target;
            set
            {
                if (target != null)
                {
                    Target.Layer = 0;
                }
                target = value;
                if (target != null)
                {
                    Target.Layer = Resource.ClickableLayer;
                    Target.Parent = transform;
                    name = Name;
                }
            }
        }

        public override Bounds Bounds => Target?.Bounds ?? new Bounds();
        public override Bounds WorldBounds => Target?.WorldBounds ?? new Bounds();
        public override Vector3 BoundsScale => Target?.WorldScale ?? Vector3.one;

        string displayName;
        public override string Name => displayName ?? Target?.Name;

        public void SetName(string newName)
        {
            this.name = newName;
            displayName = newName;
        }

        public override Pose BoundsPose => Target?.WorldPose ?? Pose.identity;

        public static DisplayClickableNode Instantiate(string name, TfFrame frame = null)
        {
            GameObject obj = new GameObject(name);
            DisplayClickableNode node = obj.AddComponent<DisplayClickableNode>();
            node.SetName(name);
            node.Parent = frame ?? TfListener.MapFrame;
            return node;
        }

        public override void Stop()
        {
            base.Stop();
            if (Target != null)
            {
                Target.Parent = null;
            }
        }
    }

}