using Iviz.Msgs.VisualizationMsgs;

namespace Iviz.Controllers.Markers
{
    public enum MouseEventType
    {
        Click = InteractiveMarkerFeedback.BUTTON_CLICK,
        Down = InteractiveMarkerFeedback.MOUSE_DOWN,
        Up = InteractiveMarkerFeedback.MOUSE_UP
    }
}