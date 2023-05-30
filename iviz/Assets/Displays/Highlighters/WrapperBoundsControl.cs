#nullable enable

using System;
using Iviz.Core;
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
            draggable.PointerDown += () => PointerDown.TryRaise(this);
            draggable.PointerUp += () => PointerUp.TryRaise(this);
            draggable.Moved += () => Moved.TryRaise(this);            
            draggable.StartDragging += () => StartDragging.TryRaise(this);            
            draggable.EndDragging += () => EndDragging.TryRaise(this);            
        }

        public abstract bool Interactable { set; }
        public abstract Quaternion BaseOrientation { set; }
        public abstract bool Visible { set; }

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