using System;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.App
{
    public interface IDraggable
    {
        event Action PointerDown;
        event Action PointerUp;
        event Action DoubleTap;
        event MovedAction Moved;
        bool Visible { get; set; }
        void OnPointerMove(in Vector2 cursorPos);
        void OnStartDragging();
        void OnEndDragging();
        Transform TargetTransform { get; set; }
        Action<Pose> SetTargetPose { get; set; }
    }
}