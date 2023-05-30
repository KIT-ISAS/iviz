#nullable enable
using System.Threading;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public class XRHolder : MonoBehaviour
    {
        Transform? mTransform;
        float baseScale;
        protected float cameraZ = 1.5f;

        protected bool isDragging;
        protected CancellationTokenSource? tokenSource;

        [SerializeField] float minX = -0.5f;
        [SerializeField] float maxX = 0.5f;
        [SerializeField] float minY = -0.4f;
        [SerializeField] float maxY = 0f;
        [SerializeField] float minZ = 1.0f;
        [SerializeField] float maxZ = 2.0f;
        [SerializeField] float targetY = -0.2f;
        [SerializeField] float targetZ = 1.5f;
        [SerializeField] float angleThreshold = 15; 

        public Transform Transform => this.EnsureHasTransform(ref mTransform);

        void Awake()
        {
            baseScale = Transform.localScale.x;
        }
        
        void Update()
        {
            const float damping = 0.05f;

            var mainCameraPose = new Pose(
                Settings.MainCameraPose.position,
                Quaternion.Euler(0, Settings.MainCameraPose.rotation.eulerAngles.y, 0)
            );

            var currentPose = Transform.AsPose();
            var (targetPosition, targetRotation) = ClampTargetPose(currentPose, mainCameraPose);

            Transform.SetPositionAndRotation(
                Vector3.Lerp(currentPose.position, targetPosition, damping),
                Quaternion.Lerp(currentPose.rotation, targetRotation, damping * 0.5f));
        }

        protected virtual Pose ProcessPose(in Pose pose)
        {
            return pose;
        }
        
        public Pose GetTargetPose()
        {
            var mainCameraPose = new Pose(
                Settings.MainCameraPose.position,
                Quaternion.Euler(0, Settings.MainCameraPose.rotation.eulerAngles.y, 0)
            );

            /*
            var canvasTransform = (RectTransform)Canvas.transform;
            float sizeY = canvasTransform.rect.size.y * canvasTransform.localScale.y;

            var targetPosition = mainCameraPose.position + mainCameraPose.forward * 1.5f - mainCameraPose.up * 0.2f;
            targetPosition.y = Mathf.Max(targetPosition.y, sizeY / 2 + 0.5f);
            */
            cameraZ = 1.5f; // approx

            var targetPosition = mainCameraPose.position + mainCameraPose.forward * targetZ + mainCameraPose.up * targetY;
            var targetRotation = CalculateOrientationToCamera();

            var targetPose = ProcessPose(new Pose(targetPosition, targetRotation));

            return ClampTargetPose(targetPose, mainCameraPose);
        }

        public void InitializePose()
        {
            transform.SetPose(GetTargetPose());

            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            FAnimator.Spawn(tokenSource.Token, 0.15f, t =>
            {
                float scale = Mathf.Sqrt(t);
                transform.localScale = baseScale * scale * Vector3.one;
            });
        }
        
        Pose ClampTargetPose(in Pose currentPose, in Pose mainCameraPose)
        {
            var (currentPositionLocal, currentRotationLocal) = mainCameraPose.InverseMultiply(currentPose);
            var targetPositionLocal = Clamp(currentPositionLocal);

            if (!isDragging)
            {
                targetPositionLocal.z = targetZ;
            }

            float currentAngleLocal = UnityUtils.RegularizeAngle(currentRotationLocal.eulerAngles.y);

            var targetRotationLocal = Mathf.Abs(currentAngleLocal) < angleThreshold
                ? currentRotationLocal
                : Quaternion.identity;

            return mainCameraPose.Multiply(new Pose(targetPositionLocal, targetRotationLocal));

            Vector3 Clamp(in Vector3 value) => new(
                Mathf.Clamp(value.x, minX, maxX),
                Mathf.Clamp(value.y, minY, maxY),
                Mathf.Clamp(value.z, minZ, maxZ)
            );
        }
        
        protected Quaternion CalculateOrientationToCamera()
        {
            return Quaternion.LookRotation((Transform.position - Settings.MainCameraPose.position).WithY(0));
        }
    }
}