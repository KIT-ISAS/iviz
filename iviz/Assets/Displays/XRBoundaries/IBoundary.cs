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
}