using Iviz.Core;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class ColorScheme
    {
        public Color FontEnabled { get; } = new Color(0.196f, 0.196f, 0.196f);
        public Color FontDisabled { get; } = (new Color(0.196f, 0.196f, 0.196f) * 3).WithAlpha(1);
        public Color EnabledPanel { get; } = new Color(0.88f, 0.99f, 1f, 0.373f);
        public Color DisabledPanel { get; } = new Color(0.777f, 0.777f, 0.777f, 0.373f);

        public Color AttachedPanelColor { get; } = new Color(0.91f, 0.95f, 1, 0.75f);
        public Color DetachedPanelColor { get; } = new Color(0.95f, 1f, 1, 0.75f);

        public Color GridGreenLine { get; } = new Color(0, 0.5f, 0).WithSaturation(0.75f);
        public Color GridRedLine { get; } = new Color(0.5f, 0, 0).WithSaturation(0.75f);
        public Color GridInterior { get; } = Color.white.WithValue(0.6f);
        public Color GridLine { get; } = Color.white.WithValue(0.25f * 0.6f);

        public Color AxisX { get; } = Color.red.WithSaturation(0.7f);
        public Color AxisY { get; } = Color.green.WithSaturation(0.7f);
        public Color AxisZ { get; } = Color.blue.WithSaturation(0.7f);

        public Color CameraOverlayAxisX { get; } = new Color(0.9f, 0.4f, 0, 1);
        public Color CameraOverlayAxisY { get; } = new Color(0, 0.9f, 0.6f, 1);
        public Color CameraOverlayAxisZ { get; } = new Color(0.6f, 0, 0.9f, 1);
        
        public Color ConnectionPanelConnected { get; } = new Color(0.6f, 1f, 0.5f, 0.4f);
        public Color ConnectionPanelOwnMaster { get; } = new Color(0.4f, 0.95f, 1f, 0.4f);
        public Color ConnectionPanelDisconnected { get; } = new Color(0.9f, 0.95f, 1f, 0.4f);
        public Color ConnectionPanelWarning { get; } = new Color(1f, 0.8f, 0.3f, 0.4f);
        
        public Color EnabledListener { get; } = new Color(0.71f, 0.98f, 1, 0.733f);
        public Color EnabledSender { get; } = new Color(0.59f, 0.79f, 0.90f, 0.90f);

        public Color HighlighterBackground { get; } = Color.blue.WithValue(0.5f).WithSaturation(0.25f);

        public Color DraggableDefaultColor { get; } = Color.grey;
        public Color DraggableHoverColor { get; } = Color.white;
        public Color DraggableSelectedColor { get; } = Color.cyan;
        public Color DraggableSelectedEmissive { get; } = Color.blue;
        
        public Color EnabledSideFont { get; } = Color.black; 
        public Color DisabledSideFont { get; } = new Color(0.25f, 0.25f, 0.25f, 0.5f); 
        public Color EnabledSideFrame { get; } = new Color(0.2f, 0.3f, 0.4f); 
        public Color DisabledSideFrame { get; } = new Color(0.75f, 0.75f, 0.75f, 1);

    }
}