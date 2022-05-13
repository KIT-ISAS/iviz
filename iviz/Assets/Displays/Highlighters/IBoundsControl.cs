using System;
using UnityEngine;

namespace Iviz.Displays.Highlighters
{
    public interface IBoundsControl
    {
        event Action PointerDown;
        event Action PointerUp;
        event Action Moved;
        event Action StartDragging;
        event Action EndDragging;
        bool Interactable { set; }
        bool Visible { set; }
        Quaternion BaseOrientation { set; }
        void Dispose();
    }
}