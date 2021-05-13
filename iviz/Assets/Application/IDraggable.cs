using System;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.App
{
    public interface IDraggable
    {
        event Action PointerDown;
        event Action PointerUp;
        event MovedAction Moved;
        bool Visible { get; set; }
        void OnPointerMove(in Vector2 cursorPos);
        void OnPointerMove(in Ray ray);
        void OnStartDragging();
        void OnEndDragging();
        Transform TargetTransform { get; set; }
    }
}