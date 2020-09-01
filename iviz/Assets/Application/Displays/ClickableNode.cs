using Iviz.App;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Controllers
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

        public IModuleData ModuleData { get; set; }
        public bool UsesBoundaryBox { get; protected set; } = false;


        bool selected;
        public virtual bool Selected
        {
            get => selected;
            set
            {
                if (value && !selected)
                {
                    selected = true;
                    //TFListener.GuiManager.ShowBoundary(this);
                    TFListener.GuiManager.Select(this);
                }
                else if (!value && selected)
                {
                    selected = false;
                    //TFListener.GuiManager.ShowBoundary(null);
                    TFListener.GuiManager.Unselect(this);
                }
            }
        }

        static int GetClickCount(PointerEventData eventData)
        {
            return GuiCamera.IsMobile ? Input.GetTouch(0).tapCount : eventData.clickCount;
        }

        static bool IsRealClick(PointerEventData eventData)
        {
            return
                eventData.button == PointerEventData.InputButton.Right &&
                Vector2.Distance(eventData.pressPosition, eventData.position) < MovingThreshold &&
                (Time.realtimeSinceStartup - eventData.clickTime) < MaxClickTime;
        }

        int LastClickCount { get; set; }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            LastClickCount = 0;
            if (!GuiCamera.IsMobile && (eventData.button != PointerEventData.InputButton.Right || !IsRealClick(eventData)))
            {
                return;
            } 
            
            if (GuiCamera.IsMobile && Input.touchCount != 1)
            {
                return;
            }

            int clickCount = GetClickCount(eventData);
            LastClickCount = clickCount;

            switch (clickCount)
            {
                case 1:
                    OnSingleClick();
                    break;
                case 2:
                    OnDoubleClick();
                    break;
            }
        }

        protected virtual void OnSingleClick()
        {
            TFListener.GuiManager.ToggleSelect(this);
        }

        protected virtual void OnDoubleClick()
        {
            TFListener.GuiManager.Select(this);
            ModuleData?.ShowPanel();
        }
        
        public override void Stop()
        {
            base.Stop();
            ModuleData = null;
            TFListener.GuiManager.Unselect(this);
        }
    }

}