#nullable enable

using UnityEngine;

namespace Iviz.Displays
{
    public sealed class StaticDraggable : XRScreenDraggable
    {
        protected override void OnPointerMove(in Ray pointerRay)
        {
            if (ReferencePointLocal == null)
            {
                InitializeReferencePoint(pointerRay);
            }
        }
    }
}