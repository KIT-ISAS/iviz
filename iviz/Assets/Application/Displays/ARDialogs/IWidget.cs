#nullable enable

using System;
using UnityEngine;

namespace Iviz.Displays.ARDialogs
{
    public interface IWidget : IDisplay
    {
        bool Interactable { set; }
    }
    
    public interface IWidgetWithColor
    {
        Color Color { set; }
        Color SecondaryColor { set; }
    }

    public interface IWidgetWithCaption
    {
        string Caption { set; }
    }
    
    public interface IWidgetWithScale
    {
        float SecondaryScale { set; }
    }

    public interface IWidgetCanBeMoved
    {
        event Action<Vector3>? Moved;
    } 
    
    public interface IWidgetCanBeRotated
    {
        public event Action<float>? Moved;
    } 
    
}