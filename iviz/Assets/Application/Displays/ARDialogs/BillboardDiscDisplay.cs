using System;
using Iviz.Core;
using Iviz.Displays;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class BillboardDiscDisplay : MonoBehaviour
    {
        [SerializeField] PlaneDraggable disc = null;
        float? distanceConstraint;
        
        Transform mTransform;
        [NotNull] Transform Transform => mTransform != null ? mTransform : (mTransform = transform);
        
        public event Action PointerDown;
        public event Action PointerUp;
        public event Action EndDragging;
        public event Action StartDragging;

        void Awake()
        {
            disc.PointerUp += () => PointerUp?.Invoke();
            disc.PointerDown += () => PointerDown?.Invoke();;
            disc.EndDragging += () =>
            {
                distanceConstraint = null;
                EndDragging?.Invoke();
            };
            disc.StartDragging += () =>
            {
                distanceConstraint = Settings.MainCameraTransform.InverseTransformPoint(Transform.position).z; 
                StartDragging?.Invoke();
            };
        }

        void Update()
        {
            var localOrientation = Vector3.up;
            (float upX, _, float upZ) = -Settings.MainCameraTransform.forward;
            Vector3 up = new Vector3(upX, 0, upZ).normalized;

            if (distanceConstraint != null)
            {
                var discPosInCamera = Settings.MainCameraTransform.InverseTransformPoint(disc.Transform.position);
                discPosInCamera.z = distanceConstraint.Value;
                disc.Transform.position = Settings.MainCameraTransform.TransformPoint(discPosInCamera);
            }

            disc.Transform.rotation = Quaternion.LookRotation(up) * Quaternion.AngleAxis(90, Vector3.right);
            disc.Normal = localOrientation;
        }
        
    }
}