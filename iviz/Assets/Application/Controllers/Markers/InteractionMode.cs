using Iviz.Msgs.VisualizationMsgs;

namespace Iviz.Controllers.Markers
{
    public enum InteractionMode
    {
        None = InteractiveMarkerControl.NONE,
        Menu = InteractiveMarkerControl.MENU,
        Button = InteractiveMarkerControl.BUTTON,
        MoveAxis = InteractiveMarkerControl.MOVE_AXIS,
        MovePlane = InteractiveMarkerControl.MOVE_PLANE,
        RotateAxis = InteractiveMarkerControl.ROTATE_AXIS,
        MoveRotate = InteractiveMarkerControl.MOVE_ROTATE,
        Move3D = InteractiveMarkerControl.MOVE_3D,
        Rotate3D = InteractiveMarkerControl.ROTATE_3D,
        MoveRotate3D = InteractiveMarkerControl.MOVE_ROTATE_3D
    }
}