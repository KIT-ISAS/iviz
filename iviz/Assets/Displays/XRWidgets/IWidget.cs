#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Msgs.IvizMsgs;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public interface IWidget : IDisplay
    {
        bool Interactable { set; }
    }
    
    public interface IWidgetWithColor : IWidget
    {
        Color Color { set; }
        Color SecondaryColor { set; }
    }

    public interface IWidgetWithCaption : IWidget
    {
        string Caption { set; }
    }
    
    public interface IWidgetWithScale : IWidget
    {
        float Scale { set; }
        float SecondaryScale { set; }
    }

    public interface IWidgetCanBeMoved : IWidget
    {
        event Action<Vector3>? Moved;
    } 
    
    public interface IWidgetCanBeRotated : IWidget
    {
        public event Action<float>? Moved;
    } 
    
    public interface IWidgetWithBoundaries : IWidget
    {
        IReadOnlyList<BoundingBox> BoundingBoxes { set; }
    }
    
}