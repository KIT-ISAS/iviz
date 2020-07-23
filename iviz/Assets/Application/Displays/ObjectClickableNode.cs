using Iviz.Resources;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class ObjectClickableNode : ClickableNode
    {
        BoxCollider boxCollider;

        GameObject target;

        public GameObject Target
        {
            get => target;
            set
            {
                if (!(target is null))
                {
                    target.gameObject.layer = 0;
                }

                target = value;
                if (!(target is null))
                {
                    target.transform.parent = transform;
                    Selectable = Selectable;
                    boxCollider = value.GetComponent<BoxCollider>();
                    name = $"[{target.name}]";
                }
            }
        }

        bool selectable;
        public bool Selectable
        {
            get => selectable;
            set
            {
                selectable = value;
                if (Target is null)
                {
                    return;
                }
                Target.gameObject.layer = value ? Resource.ClickableLayer : 0;   
            }
        }

        public override Bounds Bounds => new Bounds(boxCollider.center, boxCollider.size);
        public override Bounds WorldBounds => boxCollider.bounds;

        public override string Name => Target?.name ?? "";

        public override Pose BoundsPose => Target?.transform.AsPose() ?? Pose.identity;
        public override Vector3 BoundsScale => Target?.transform.lossyScale ?? Vector3.one;

        public static ObjectClickableNode Instantiate(string name, TFFrame frame = null)
        {
            GameObject obj = new GameObject(name);
            ObjectClickableNode node = obj.AddComponent<ObjectClickableNode>();
            node.Parent = frame ?? TFListener.MapFrame;
            return node;
        }

        public override void Stop()
        {
            base.Stop();
            
            if (Target is null)
            {
                return;
            }

            Target.transform.parent = null;
            Target.gameObject.layer = 0;
        }
    }

}