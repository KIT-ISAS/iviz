using UnityEngine;

namespace Iviz.Resources
{
    public sealed class ColorScheme
    {
        public Color EnabledFontColor { get; }
        public Color DisabledFontColor { get; }
        public Color EnabledPanelColor { get; }
        public Color DisabledPanelColor { get; }

        public ColorScheme()
        {
            EnabledFontColor = new Color(0.196f, 0.196f, 0.196f, 1.0f);
            DisabledFontColor = EnabledFontColor * 3;

            EnabledPanelColor = new Color(0.88f, 0.99f, 1f, 0.373f);
            DisabledPanelColor = new Color(0.777f, 0.777f, 0.777f, 0.373f);
        }
    }
}