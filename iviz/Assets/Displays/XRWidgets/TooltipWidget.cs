#nullable enable

using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class TooltipWidget : MonoBehaviour, IWidgetWithColor, IWidgetWithCaption, IRecyclable
    {
        [SerializeField] Tooltip? tooltip;

        Tooltip Tooltip => ResourcePool.RentChecked(ref tooltip, transform);

        void Awake()
        {
            Tooltip.Scale = 0.02f;
        }

        public void Suspend()
        {
            Tooltip.Scale = 0.02f;
        }

        public bool Interactable
        {
            set { } // no effect
        }

        public Color Color
        {
            set => Tooltip.Color = value;
        }

        public Color SecondColor
        {
            set => Tooltip.CaptionColor = value;
        }

        public string Caption
        {
            set => Tooltip.Caption = value;
        }

        public void SplitForRecycle()
        {
            tooltip.ReturnToPool();
        }
    }
}