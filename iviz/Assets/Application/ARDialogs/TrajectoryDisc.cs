using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public class TrajectoryDisc : ARWidget
    {
        [SerializeField] DraggablePlane disc = null;
        float? distanceConstraint;

        void Awake()
        {
            disc.PointerUp += OnPointerUp;
            disc.PointerDown += OnPointerDown;
        }

        void OnPointerDown()
        {
            distanceConstraint = Settings.MainCameraTransform.InverseTransformPoint(Transform.position).z; 
        }
        
        void OnPointerUp()
        {
            distanceConstraint = null;
        }


        void Update()
        {
            var localOrientation = Vector3.up;
            (float upX, _, float upZ) = -Settings.MainCameraTransform.forward;
            Vector3 up = new Vector3(upX, 0, upZ).normalized;
            
            Vector3 right = Vector3.Cross(up, Vector3.right);
            if (right.sqrMagnitude < 1e-6)
            {
                right = Vector3.Cross(up, Vector3.forward);
            }

            if (distanceConstraint != null)
            {
                var discPosInCamera = Settings.MainCameraTransform.InverseTransformPoint(disc.Transform.position);
                discPosInCamera.z = distanceConstraint.Value;
                disc.Transform.position = Settings.MainCameraTransform.TransformPoint(discPosInCamera);
            }

            disc.Transform.rotation = Quaternion.LookRotation(right.normalized, up);
            disc.Normal = localOrientation;
        }
        
    }
}