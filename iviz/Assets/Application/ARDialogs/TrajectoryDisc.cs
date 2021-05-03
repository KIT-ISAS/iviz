using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public class TrajectoryDisc : ARWidget
    {
        [SerializeField] DraggablePlane disc = null;
        
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

            disc.Transform.rotation = Quaternion.LookRotation(right.normalized, up);
            disc.Normal = localOrientation;
        }
        
    }
}