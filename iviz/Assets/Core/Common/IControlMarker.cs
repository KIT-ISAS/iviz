using System;
using UnityEngine;

namespace Iviz.Displays
{
    public delegate void MovedAction(in Pose pose);

    public enum InteractionModeType
    {
        None,
        ClickOnly,

        MoveAxisX,
        MovePlaneYZ,
        RotateAxisX,
        MovePlaneYZ_RotateAxisX,
        Frame,

        Move3D,
        Rotate3D,
        MoveRotate3D
    }
    
    public interface IControlMarker : IDisplay
    {
        Transform TargetTransform { get; set; }
        bool PointsToCamera { get; set; }
        bool HandlesPointToCamera { get; set; }
        bool KeepAbsoluteRotation { get; set; }
        InteractionModeType InteractionMode { get; set; }
        new Bounds? Bounds { set; }
        bool EnableMenu { get; set; }

        event MovedAction Moved;
        event Action PointerUp;
        event Action PointerDown;
        event Action<Vector3> MenuClicked;
    }
}