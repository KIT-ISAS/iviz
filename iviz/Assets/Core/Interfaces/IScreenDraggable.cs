#nullable enable

using System;
using UnityEngine;

namespace Iviz.Common
{
    public interface IDraggable
    {
        event Action? PointerDown;
        event Action? PointerUp;
        event Action? Moved;
        event Action? StartDragging;
        event Action? EndDragging;
    }

    public interface IScreenDraggable : IDraggable
    {
        void OnPointerMove(in Ray ray);
        void OnStartDragging();
        void OnEndDragging();
        Transform TargetTransform { set; }
        Vector3? ReferencePoint { get; }
        Vector3? ReferenceNormal { get; }
    }
}