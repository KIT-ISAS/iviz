using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_WSA
using Microsoft.MixedReality.Toolkit.Input;
#endif

namespace Iviz.Controllers
{
    public abstract class ClickableNode : 
        DisplayNode, IPointerClickHandler
#if UNITY_WSA
        , IMixedRealityPointerHandler
#endif
    {
        const float MovingThreshold = 100;
        const float MaxClickTime = 0.5f;

        public abstract Bounds Bounds { get; }
        public abstract Bounds WorldBounds { get; }
        public abstract Pose BoundsPose { get; }
        public abstract Vector3 BoundsScale { get; }
        [NotNull] public abstract string Name { get; }
        [CanBeNull] public IModuleData ModuleData { get; set; }
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
                    TfListener.GuiCamera.Select(this);
                }
                else if (!value && selected)
                {
                    selected = false;
                    TfListener.GuiCamera.Unselect(this);
                }
            }
        }

        static int GetClickCount([NotNull] PointerEventData eventData)
        {
            return Settings.IsMobile ? Input.GetTouch(0).tapCount : eventData.clickCount;
        }

        static bool IsRealClick([NotNull] PointerEventData eventData)
        {
            return
                eventData.button == PointerEventData.InputButton.Right &&
                Vector2.Distance(eventData.pressPosition, eventData.position) < MovingThreshold &&
                (Time.realtimeSinceStartup - eventData.clickTime) < MaxClickTime;
        }

        public virtual void OnPointerClick([NotNull] PointerEventData eventData)
        {
            if (!Settings.IsMobile && (eventData.button != PointerEventData.InputButton.Right || !IsRealClick(eventData)))
            {
                return;
            } 
            
            if (Settings.IsMobile && Input.touchCount != 1)
            {
                return;
            }

            int clickCount = GetClickCount(eventData);

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
            TfListener.GuiCamera.ToggleSelect(this);
        }

        protected virtual void OnDoubleClick()
        {
            TfListener.GuiCamera.Select(this);
            ModuleData?.ShowPanel();
        }
        
        public override void Stop()
        {
            base.Stop();
            ModuleData = null;
            TfListener.GuiCamera.Unselect(this);
        }

#if UNITY_WSA
        public virtual void OnPointerDown(MixedRealityPointerEventData eventData)
        {
        }

        public virtual void OnPointerDragged(MixedRealityPointerEventData eventData)
        {
        }

        public virtual void OnPointerUp(MixedRealityPointerEventData eventData)
        {
        }

        public virtual void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            int clickCount = eventData.Count;

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
#endif
    }

}