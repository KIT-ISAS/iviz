using System;
using Iviz.Controllers;
using Iviz.Core;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Iviz.Displays
{
    public sealed class ARAnchorResource : MonoBehaviour
    {
        public event Action<Pose> Moved;
        public Pose Pose => transform.AsPose();

        ARAnchorManager manager;
        public ARAnchor Anchor { get; private set; }
        
        void Awake()
        {
            if (ARController.Instance == null)
            {
                return; // shouldn't happen
            }
            
            manager = ARController.Instance.AnchorManager;
            manager.anchorsChanged += OnAnchorsChanged;
            Anchor = this.AssertHasComponent<ARAnchor>(nameof(gameObject));
        }

        void OnAnchorsChanged(ARAnchorsChangedEventArgs args)
        {
            if (args.updated.Contains(Anchor))
            {
                Moved?.Invoke(Pose);
            }
        }

        void OnDestroy()
        {
            manager.anchorsChanged -= OnAnchorsChanged;
            Moved = null;
        }
    }
}