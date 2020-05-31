using Iviz.App.Listeners;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App.Displays
{
    public class ObjectClickableNode : ClickableNode
    {
        BoxCollider boxCollider;

        GameObject target;
        public GameObject Target
        {
            get => target;
            set
            {
                if (target != null)
                {
                    target.gameObject.layer = 0;
                }
                target = value;
                if (target != null)
                {
                    target.transform.parent = transform;
                    target.gameObject.layer = Resource.ClickableLayer;
                    boxCollider = value.GetComponent<BoxCollider>();
                    name = $"[{target.name}]";
                }
            }
        }

        public override Bounds Bounds => new Bounds(boxCollider.center, boxCollider.size);
        public override Bounds WorldBounds => boxCollider.bounds;

        void Awake()
        {
            //boxCollider = gameObject.AddComponent<BoxCollider>();
        }

        public static ObjectClickableNode Instantiate(string name, TFFrame frame = null)
        {
            GameObject obj = new GameObject(name);
            //obj.layer = Resource.ClickableLayer;
            ObjectClickableNode node = obj.AddComponent<ObjectClickableNode>();
            node.Parent = frame ?? TFListener.ListenersFrame;
            return node;
        }

        public override void Stop()
        {
            base.Stop();
            if (Target != null)
            {
                Target.transform.parent = null;
            }
        }
    }

}