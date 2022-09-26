#nullable enable
using System;
using Iviz.Msgs.IvizMsgs;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public enum BehaviorType
    {
        None = Boundary.BEHAVIOR_NONE,
        Collider = Boundary.BEHAVIOR_COLLIDER,
        NotifyCollision = Boundary.BEHAVIOR_NOTIFY_COLLISION
    }
    
    public interface IBoundary : IDisplay, IIsInteractable
    {
        string Id { set; } 
        BehaviorType Behavior { set; }
        Vector3 Scale { set; }
        string Caption { set; }     
        Color Color { set; }   
        Color SecondColor { set; }


        event Action<string>? EnteredCollision;
        event Action<string>? ExitedCollision;
    }
}