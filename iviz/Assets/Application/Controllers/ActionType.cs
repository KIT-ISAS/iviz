#nullable enable
using Iviz.Msgs.IvizMsgs;

namespace Iviz.Controllers
{
    public enum ActionType
    {
        Add = Widget.ACTION_ADD,
        Remove = Widget.ACTION_REMOVE,
        RemoveAll = Widget.ACTION_REMOVEALL
    }

    public enum WidgetType
    {
        RotationDisc = Widget.TYPE_ROTATIONDISC,
        SpringDisc = Widget.TYPE_SPRINGDISC,
        SpringDisc3D = Widget.TYPE_SPRINGDISC3D,
        TrajectoryDisc = Widget.TYPE_TRAJECTORYDISC,
        TrajectoryDisc3D = Widget.TYPE_TRAJECTORYDISC3D,
        Tooltip = Widget.TYPE_TOOLTIP,
        TargetArea = Widget.TYPE_TARGETAREA,
        PositionDisc = Widget.TYPE_POSITIONDISC,
        PositionDisc3D = Widget.TYPE_POSITIONDISC3D,
    }

    public enum BoundaryType
    {
        Simple = Boundary.TYPE_SIMPLE,
        CircleHighlight = Boundary.TYPE_CIRCLE_HIGHLIGHT,
        SquareHighlight = Boundary.TYPE_SQUARE_HIGHLIGHT,
    }
    
    public enum FeedbackType
    {
        Expired = Feedback.TYPE_DIALOG_EXPIRED,
        ButtonClick = Feedback.TYPE_BUTTON_CLICK,
        MenuEntryClick = Feedback.TYPE_MENUENTRY_CLICK,
        PositionChanged = Feedback.TYPE_POSITION_CHANGED,
        OrientationChanged = Feedback.TYPE_ORIENTATION_CHANGED,
        ScaleChanged = Feedback.TYPE_SCALE_CHANGED,
        TrajectoryChanged = Feedback.TYPE_TRAJECTORY_CHANGED,
        ColliderEntered = Feedback.TYPE_COLLIDER_ENTERED,
        ColliderExited = Feedback.TYPE_COLLIDER_EXITED,
    }
}