using System;
using System.Drawing;

namespace Iviz.Displays.Highlighters
{
    public interface IBoundsControl
    {
        event Action PointerDown;
        event Action PointerUp;
        event Action Moved;
        bool Interactable { set; }
        void Stop();
    }
}