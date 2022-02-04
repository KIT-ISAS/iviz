#nullable enable

using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public class XRNotification : DisplayWrapper
    {
        public enum LevelType
        {
            Debug,
            Info,
            Warn,
            Success,
            Error,
        }

        [SerializeField] Tooltip? tooltip;
        RoundedPlaneDisplay? background;

        string title = "";
        string text = "";

        Tooltip Tooltip => tooltip.AssertNotNull(nameof(tooltip));
        protected override IDisplay Display => Tooltip;

        public string Title
        {
            set
            {
                title = value;
                UpdateLabel();
            }
        }

        public string Text
        {
            set
            {
                text = value;
                UpdateLabel();
            }
        }

        LevelType Level { get; set; }

        void UpdateLabel()
        {
            Tooltip.Caption = "<b>" + title + "</b>\n" + text;
        }

        void Awake()
        {
            Tooltip.PaddingX = 10;
            Tooltip.PaddingY = 10;
            Tooltip.FixedWidth = true;
            Tooltip.Caption = "Connected to 141.3.59.5";
            var color = Color.Lerp(Color.blue, Color.cyan, 0.5f).WithSaturation(0.5f).WithValue(0.4f);
            var marginColor = Color.Lerp(Color.blue, Color.cyan, 0.5f).WithValue(1.0f).WithSaturation(0.3f);
            
            Tooltip.Color = color;
            Tooltip.EmissiveColor = color;
            
            var margin = Tooltip.Background.Children;
            margin[1].Color = marginColor;
            margin[5].Color = marginColor;
            margin[6].Color = marginColor;
            margin[1].EmissiveColor = marginColor;
            margin[5].EmissiveColor = marginColor;
            margin[6].EmissiveColor = marginColor;
        }
    }
}