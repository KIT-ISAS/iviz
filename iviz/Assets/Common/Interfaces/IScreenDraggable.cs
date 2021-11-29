#nullable enable

using System;
using UnityEngine;

namespace Iviz.Common
{
    public interface IScreenDraggable
    {
        event Action? PointerDown;
        event Action? PointerUp;
        event Action? Moved;
        bool Visible { get; set; }
        void OnPointerMove(in Ray ray);
        void OnStartDragging();
        void OnEndDragging();
        Transform TargetTransform { get; set; }
        Vector3? ReferencePoint { get; }
        Vector3? ReferenceNormal { get; }
        bool IsHovering { get; }
        bool IsDragging { get; }
    }
}