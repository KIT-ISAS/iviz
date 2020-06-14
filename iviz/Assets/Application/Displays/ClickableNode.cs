using Iviz.App.Listeners;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App.Displays
{
    public abstract class ClickableNode : DisplayNode, IPointerClickHandler
    {
        const float MovingThreshold = 100;
        const float MaxClickTime = 0.5f;

        public abstract Bounds Bounds { get; }
        public abstract Bounds WorldBounds { get; }
        public abstract Pose BoundsPose { get; }
        public abstract Vector3 BoundsScale { get; }
        public abstract string Name { get; }

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
            return
                eventData.button == PointerEventData.InputButton.Left &&
                Vector2.Distance(eventData.pressPosition, eventData.position) < MovingThreshold &&
                (Time.realtimeSinceStartup - eventData.clickTime) < MaxClickTime;
        }

        protected int LastClickCount { get; private set; }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            LastClickCount = 0;
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
            LastClickCount = clickCount;

            switch (clickCount)
            {
                case 1:
                    TFListener.GuiManager.Select(this);
                    break;
                case 2:
                    DisplayData?.Select();
                    break;
                case 3:
                    TFListener.GuiManager.ToggleSelect(this);
                    break;
                    /*
                case 3:
                    TFListener.GuiManager.OrbitCenter = transform.TransformPoint(WorldBounds.center);
                    break;
                    */
            }
        }

        public override void Stop()
        {
            base.Stop();
            DisplayData = null;
            TFListener.GuiManager.Unselect(this);
        }
    }

}