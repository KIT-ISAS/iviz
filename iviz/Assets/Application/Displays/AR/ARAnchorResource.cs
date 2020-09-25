using System;
using Iviz.Controllers;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Iviz.Displays
{
    public class ARAnchorResource : MonoBehaviour
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
            
            manager = ARController.Instance.GetComponentInChildren<ARAnchorManager>();
            manager.anchorsChanged += OnAnchorsChanged;
            Anchor = GetComponent<ARAnchor>();
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