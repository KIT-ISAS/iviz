using Iviz.App.Listeners;
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
            Debug.Log("was here");
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

}