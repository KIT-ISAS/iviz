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
            var currentPositionLocal = Settings.MainCameraTransform.InverseTransformPoint(Transform.position);
            var targetPositionLocal = new Vector3
            (
                Clamp(currentPositionLocal.x, -0.2f, 0.2f),
                Clamp(currentPositionLocal.y, -0.3f, 0.3f),
                Clamp(currentPositionLocal.z, minDistance, maxDistance)
            );
            
            
            

        }

        static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }
    }
}