#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Msgs.IvizMsgs;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public interface IWidget : IDisplay, IIsInteractable
    {
    }

    public interface IWidgetWithColor : IWidget
    {
        Color Color { set; }

        Color SecondColor
        {
            set { }
        }
    }

    public interface IWidgetWithCaption : IWidget
    {
        string Caption { set; }

        string SecondCaption
        {
            set { }
        }
    }

    public interface IWidgetWithScale : IWidget
    {
        float Scale { set; }

        float SecondScale
        {
            set { }
        }
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

    public interface IWidgetProvidesTrajectory : IWidget
    {
        public event Action<List<Vector3>, float>? ProvidedTrajectory;
    }
}