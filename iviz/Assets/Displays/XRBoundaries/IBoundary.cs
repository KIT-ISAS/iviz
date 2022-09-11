#nullable enable
using System;
using Iviz.Msgs.IvizMsgs;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public interface IBoundary : IDisplay, IIsInteractable
    {
        string Id { set; } 
        Vector3 Scale { set; }
        string Caption { set; }     
        Color Color { set; }   
        Color SecondColor { set; }   
    }

    public interface IBoundaryCanCollide
    {
        public event Action<string>? EnteredCollision;
        public event Action<string>? ExitedCollision;
    }
}