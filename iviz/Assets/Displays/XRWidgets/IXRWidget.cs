#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Msgs.IvizMsgs;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public interface IXRWidget : IDisplay, IIsInteractable
    {
    }

    public interface IWidgetWithColor : IXRWidget
    {
        Color Color { set; }

        Color SecondColor
        {
            set { }
        }
    }

    public interface IWidgetWithCaption : IXRWidget
    {
        string Caption { set; }

        string SecondCaption
        {
            set { }
        }
    }

    public interface IWidgetWithScale : IXRWidget
    {
        float Scale { set; }

        float SecondScale
        {
            set { }
        }
    }

    public interface IWidgetCanBeMoved : IXRWidget
    {
        event Action<Vector3>? Moved;
    }

    public interface IWidgetCanBeRotated : IXRWidget
    {
        public event Action<float>? Moved;
    }

    public interface IWidgetCanBeResized : IXRWidget
    {
        public event Action<Bounds>? Resized;
    }

    public interface IWidgetCanBeClicked : IXRWidget
    {
        public event Action<int>? Clicked;
    }

    public interface IWidgetProvidesTrajectory : IXRWidget
    {
        public event Action<List<Vector3>, float>? ProvidedTrajectory;
    }
}