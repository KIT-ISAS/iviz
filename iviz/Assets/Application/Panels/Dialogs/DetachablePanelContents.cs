#nullable enable

using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public abstract class DetachablePanelContents : PanelContents
    {
        [SerializeField] DialogScalerWidget? scalerWidget;
        Image? panelImage;
        CanvasHolder? canvasHolder;

        protected DialogScalerWidget ScalerWidget => scalerWidget.AssertNotNull(nameof(scalerWidget));
        Image PanelImage => panelImage != null ? panelImage : (panelImage = GetComponent<Image>());

        public bool Detached
        {
            set
            {
                ScalerWidget.gameObject.SetActive(value);
                PanelImage.color = value ? Resource.Colors.DetachedPanelColor : Resource.Colors.AttachedPanelColor;

                RectTransform mTransform = (RectTransform)transform;
                if (value)
                {
                    canvasHolder = ResourcePool.RentDisplay<CanvasHolder>();
                    mTransform.SetParent(canvasHolder.Canvas.transform, false);
                    mTransform.localScale = Vector3.one;
                    mTransform.offsetMin = Vector2.zero;
                    mTransform.offsetMax = Vector2.zero;

                    canvasHolder.CanvasSize = new Vector2(400, 800);
                    GetComponentInChildren<DialogMoverWidget>().enabled = false;
                }
            }
        }
    }
}