#nullable enable

using System;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class CollidableBoundary : ColliderBoundary, IBoundaryCanCollide
    {
        public event Action<string>? EnteredCollision;
        public event Action<string>? ExitedCollision;

        void OnTriggerEnter(Collider collision)
        {
            Debug.Log("Enter");
            if (collision.gameObject.TryGetComponent<ColliderBoundary>(out var boundary))
            {
                EnteredCollision?.Invoke(boundary.Id ?? "");
            }
        }

        void OnTriggerExit(Collider collision)
        {
            Debug.Log("Exit");
            if (collision.gameObject.TryGetComponent<ColliderBoundary>(out var boundary))
            {
                ExitedCollision?.Invoke(boundary.Id ?? "");
            }
        }
        
        public override void Suspend()
        {
            base.Suspend();
            EnteredCollision = null;
            ExitedCollision = null;
        }        
    }
}
