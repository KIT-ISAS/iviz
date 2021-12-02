using System;
using System.Drawing;

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
        void Dispose();
    }
}