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
        MovePlaneYz,
        RotateAxisX,
        MovePlaneYzRotateAxisX,
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
        bool ColliderCanInteract { get; }
        new Bounds? Bounds { set; }
        bool EnableMenu { get; set; }
        bool Interactable { get; set; }
        void SetColliderInteractable();

        event MovedAction Moved;
        event Action PointerUp;
        event Action PointerDown;
        event Action<Vector3> MenuClicked;
    }
}