#nullable enable

using System;
using UnityEngine;

namespace Iviz.Displays.Highlighters
{
    public abstract class WrapperBoundsControl : IBoundsControl 
    {
        public event Action? PointerDown;
        public event Action? PointerUp;
        public event Action? Moved;
        public event Action? StartDragging;
        public event Action? EndDragging;
        
        public abstract Bounds Bounds { set; }
        public abstract float MarkerScale { set; }
        
        protected void RegisterDraggable(ScreenDraggable draggable)
        {
            draggable.PointerDown += () => PointerDown?.Invoke();
            draggable.PointerUp += () => PointerUp?.Invoke();
            draggable.Moved += () => Moved?.Invoke();            
            draggable.StartDragging += () => StartDragging?.Invoke();            
            draggable.EndDragging += () => EndDragging?.Invoke();            
        }

        public abstract bool Interactable { set; }

        public virtual void Dispose()
        {
            PointerDown = null;
            PointerUp = null;
            Moved = null;
            StartDragging = null;
            EndDragging = null;
        }
    }
}