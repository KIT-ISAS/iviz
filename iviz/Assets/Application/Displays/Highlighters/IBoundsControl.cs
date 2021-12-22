using System;
using System.Drawing;
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
        Quaternion BaseOrientation { set; }
        void Dispose();
    }
}