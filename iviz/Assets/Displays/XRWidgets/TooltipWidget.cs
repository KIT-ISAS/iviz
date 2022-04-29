#nullable enable

using System;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XR;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public class TooltipWidget : MonoBehaviour, IWidgetWithColor, IWidgetWithCaption, IRecyclable
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

        public Color SecondaryColor
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