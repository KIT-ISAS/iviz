using UnityEngine;

namespace Iviz.Core.XR
{
    public interface IXRController
    {
        bool IsActiveInFrame { get; }
        bool HasCursor { get; }
        bool ButtonState { get; }
        bool ButtonUp { get; }
        bool ButtonDown { get; }
        bool IsNearInteraction { get; }
    }
}