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
    }

    public interface IWidgetWithSecondaryScale : IWidget
    {
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
    
    public interface IWidgetCanBeResized : IWidget
    {
        public event Action<Bounds>? Resized;
    }    
    
    public interface IWidgetCanBeClicked : IWidget
    {
        public event Action<int>? Clicked;
    }        

    public interface IWidgetWithBoundary : IWidget
    {
        BoundingBox Boundary { set; }
    }

    public interface IWidgetWithBoundaries : IWidget
    {
        void Set(BoundingBoxStamped baseBox, IReadOnlyList<BoundingBoxStamped> boxes);
    }
}