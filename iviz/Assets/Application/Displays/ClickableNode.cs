using Iviz.App.Listeners;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App.Displays
{
    public abstract class ClickableNode : DisplayNode, IPointerClickHandler
    {
        public abstract Bounds Bounds { get; }
        public abstract Bounds WorldBounds { get; }

        public DisplayData DisplayData;

        protected bool selected;
        public virtual bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                if (value)
                {
                    TFListener.GuiManager.ShowBoundary(this, WorldBounds, name, transform);
                }
            }
        }

        protected static int GetClickCount(PointerEventData eventData)
        {
            return FlyCamera.IsMobile ? Input.GetTouch(0).tapCount : eventData.clickCount;
        }

        protected bool IsRealClick(PointerEventData eventData)
        {
            const float MovingThreshold = 50;
            const float MaxClickTime = 0.5f;

            return
                eventData.button == PointerEventData.InputButton.Left &&
                Vector2.Distance(eventData.pressPosition, eventData.position) < MovingThreshold &&
                (Time.realtimeSinceStartup - eventData.clickTime) < MaxClickTime;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!FlyCamera.IsMobile && eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            if (FlyCamera.IsMobile && Input.touchCount != 1)
            {
                return;
            }

            if (!IsRealClick(eventData))
            {
                return;
            }
            int clickCount = GetClickCount(eventData);
            if (eventData.IsPointerMoving()) {
                if (clickCount != 1)
                {
                    clickCount = 1;
                }
                else
                {
                    return;
                }
            }

            switch (clickCount)
            {
                case 1:
                    TFListener.GuiManager.Select(this);
                    break;
                case 2:
                    TFListener.GuiManager.OrbitCenter = transform.TransformPoint(WorldBounds.center);
                    break;
                case 3:
                    DisplayData?.Select();
                    break;
            }
        }

        public override void Stop()
        {
            base.Stop();
            DisplayData = null;
            TFListener.GuiManager.Unselect(this);
        }
    }

    public class DisplayClickableNode : ClickableNode
    {
        IDisplay target;
        public IDisplay Target {
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
                    ((MonoBehaviour)target).gameObject.layer = Resource.ClickableLayer;
                    ((MonoBehaviour)target).transform.parent = transform;
                }
            }
        }

        public override Bounds Bounds => Target?.Bounds ?? new Bounds();
        public override Bounds WorldBounds => Target?.WorldBounds ?? new Bounds();

        public static DisplayClickableNode Instantiate(string name, TFFrame frame = null)
        {
            GameObject obj = new GameObject(name);
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

    public class ObjectClickableNode : ClickableNode
    {
        BoxCollider boxCollider;

        GameObject target;
        public GameObject Target
        {
            get => target;
            set
            {
                target = value;
                if (target != null)
                {
                    target.transform.parent = transform;
                    BoxCollider otherCollider = value.GetComponent<BoxCollider>();
                    if (otherCollider != null)
                    {
                        boxCollider.center = otherCollider.center;
                        boxCollider.size = otherCollider.size;
                    }
                }
            }
        }

        public override Bounds Bounds => new Bounds(boxCollider.center, boxCollider.size);
        public override Bounds WorldBounds => boxCollider.bounds;

        void Awake()
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }

        public static ObjectClickableNode Instantiate(string name, TFFrame frame = null)
        {
            GameObject obj = new GameObject(name);
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