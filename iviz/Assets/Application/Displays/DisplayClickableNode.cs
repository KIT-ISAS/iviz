using Iviz.App.Listeners;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App.Displays
{
    public class DisplayClickableNode : ClickableNode
    {
        IDisplay target;
        public IDisplay Target
        {
            get => target;
            set
            {
                if (target != null)
                {
                    ((MonoBehaviour)target).gameObject.layer = 0;
                }
                target = value;
                if (target != null)
                {
                    GameObject targetObject = ((MonoBehaviour)target).gameObject;
                    targetObject.layer = Resource.ClickableLayer;
                    targetObject.transform.parent = transform;
                    name = $"[{targetObject.name}]";
                }
            }
        }

        public override Bounds Bounds => Target?.Bounds ?? new Bounds();
        public override Bounds WorldBounds => Target?.WorldBounds ?? new Bounds();

        public static DisplayClickableNode Instantiate(string name, TFFrame frame = null)
        {
            GameObject obj = new GameObject(name);
            //obj.layer = Resource.ClickableLayer;
            DisplayClickableNode node = obj.AddComponent<DisplayClickableNode>();
            node.Parent = frame ?? TFListener.ListenersFrame;
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