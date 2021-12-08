using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    public class FollowerBillboard : MonoBehaviour
    {
        [SerializeField] float minDistance = 1.0f;
        [SerializeField] float maxDistance = 1.5f;

        [NotNull] Transform Transform => transform;

        void Update()
        {
            var mainCameraPose = new Pose(
                Settings.MainCameraTransform.position,
                Quaternion.Euler(0, Settings.MainCameraTransform.eulerAngles.y, 0)
            );

            var (currentPosition, currentRotation) = Transform.AsPose();
            var currentPositionLocal = mainCameraPose.Inverse().Multiply(currentPosition);
            var targetPositionLocal = Clamp(
                currentPositionLocal, 
                new Vector3(-0.5f, -0.3f, minDistance),
                new Vector3(0.5f, 0.1f, maxDistance));

            var targetPosition = mainCameraPose.Multiply(targetPositionLocal);
            var targetRotation = mainCameraPose.rotation;
            
            Transform.SetPositionAndRotation(
                Vector3.Lerp(currentPosition, targetPosition, 0.05f), 
                Quaternion.Lerp(currentRotation, targetRotation, 0.05f));
        }

        static Vector3 Clamp(in Vector3 value, in Vector3 min, in Vector3 max)
        {
            return Vector3.Min(Vector3.Max(value, min), max);
        }
    }
}