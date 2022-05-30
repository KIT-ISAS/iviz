#nullable enable

using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XR;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public abstract class DetachableDialogPanel : DialogPanel
    {
        [SerializeField] DialogScalerWidget? scalerWidget;
        
        Image? panelImage;
        CanvasHolder? canvasHolder;
        Transform? originalParent;
        Vector3 originalScale;
        Vector2 originalOffsetMin;
        Vector2 originalOffsetMax;

        Image PanelImage => this.EnsureHasComponent(ref panelImage, nameof(panelImage));
        
        protected DialogScalerWidget ScalerWidget => scalerWidget.AssertNotNull(nameof(scalerWidget));

        public bool Detached
        {
            set
            {
                ScalerWidget.gameObject.SetActive(value);
                PanelImage.color = value ? Resource.Colors.DetachedPanelColor : Resource.Colors.AttachedPanelColor;

                if (Settings.IsXR)
                {
                    DetachXR(value);
                }
            }
        }

        void DetachXR(bool value)
        {
            var mTransform = (RectTransform)transform;
            var dialogMover = GetComponentInChildren<DialogMoverWidget>();
            
            if (value && canvasHolder == null)
            {
                canvasHolder = ResourcePool.RentDisplay<CanvasHolder>();
                canvasHolder.Title = "Dialog";
                
                originalParent = mTransform.parent;
                mTransform.SetParent(canvasHolder.Canvas.transform, false);

                originalScale = mTransform.localScale;
                originalOffsetMin = mTransform.offsetMin;
                originalOffsetMax = mTransform.offsetMax;
                    
                mTransform.localScale = Vector3.one;
                mTransform.offsetMin = Vector2.zero;
                mTransform.offsetMax = Vector2.zero;

                canvasHolder.CanvasSize = new Vector2(400, 600);
                dialogMover.enabled = false;
                
                canvasHolder.InitializePose();
            }
            else if (!value && canvasHolder != null)
            {
                mTransform.SetParent(originalParent, false);
                mTransform.localScale = originalScale;
                mTransform.offsetMin = originalOffsetMin;
                mTransform.offsetMax = originalOffsetMax;
                dialogMover.enabled = true;
                    
                canvasHolder.ReturnToPool();
                canvasHolder = null;
            }            
        }
    }
}