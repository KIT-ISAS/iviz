#nullable enable

using Iviz.Core;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public abstract class DetachablePanelContents : PanelContents
    {
        [SerializeField] DialogScalerWidget? scalerWidget = null;
        Image? panelImage;

        protected DialogScalerWidget ScalerWidget => scalerWidget.AssertNotNull(nameof(scalerWidget));
        Image PanelImage => panelImage != null ? panelImage : (panelImage = GetComponent<Image>());

        public bool Detached
        {
            set
            {
                ScalerWidget.gameObject.SetActive(value);
                PanelImage.color = value ? Resource.Colors.DetachedPanelColor : Resource.Colors.AttachedPanelColor;
            }
        }
    }
}