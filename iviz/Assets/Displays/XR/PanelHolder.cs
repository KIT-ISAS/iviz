#nullable enable

using System;
using System.Threading;
using Iviz.Core;
using Iviz.Resources;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public class PanelHolder : MonoBehaviour, IDisplay, IRecyclable
    {
        const float HeaderHeight = 0.06f;

        [SerializeField] BoxCollider? panelCollider;
        [SerializeField] BoxCollider? boxCollider;
        [SerializeField] FixedDistanceDraggable? draggable;
        [SerializeField] TMP_Text? label;
        [SerializeField] float gapSize = 0.03f;

        CancellationTokenSource? tokenSource;
        RoundedPlaneDisplay? background;
        SelectionFrame? frame;
        Transform? mTransform;
        bool isDragging;

        BoxCollider BoxCollider => boxCollider.AssertNotNull(nameof(boxCollider));
        BoxCollider PanelCollider => panelCollider.AssertNotNull(nameof(panelCollider));

        RoundedPlaneDisplay Background =>
            background != null
                ? background
                : (background = ResourcePool.RentDisplay<RoundedPlaneDisplay>(Holder));

        FixedDistanceDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        Transform Holder => Draggable.Transform;
        TMP_Text Label => label.AssertNotNull(nameof(label));
        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        public string Title
        {
            get => Label.text;
            set => Label.text = value;
        }

        void UpdateSize()
        {
            var (sizeX, sizeY, _) = Vector3.Scale(PanelCollider.size, PanelCollider.transform.localScale);
            Background.Size = new Vector2(sizeX, HeaderHeight);
            BoxCollider.size = new Vector3(sizeX, HeaderHeight, 0.005f);
            PanelCollider.transform.localPosition = (sizeY + HeaderHeight + gapSize) / 2 * Vector3.up;
        }

        public bool FollowsCamera
        {
            set => enabled = value;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void Awake()
        {
            UpdateSize();
            Background.Radius = 0.01f;
            Background.Color = Resource.Colors.TooltipBackground;
            Background.EnableShadows = false;
            Background.Layer = LayerType.Clickable;

            Draggable.ForwardScale = 5f;
            Draggable.StateChanged += () =>
            {
                bool visible = Draggable.IsDragging || Draggable.IsHovering;
                if (visible && frame == null)
                {
                    frame = ResourcePool.RentDisplay<SelectionFrame>(Holder);
                    frame.Size = BoxCollider.size;
                    frame.Color = Draggable.IsDragging ? Color.cyan : Color.white;
                    frame.EmissiveColor = Draggable.IsDragging ? Color.blue : Color.black;
                    frame.ColumnWidth = Draggable.IsDragging ? 0.01f : 0.005f;
                }
                else if (!visible && frame != null)
                {
                    frame.ReturnToPool();
                    frame = null;
                }
            };

            Draggable.StartDragging += OnStartDragging;
            Draggable.EndDragging += OnEndDragging;

            if (Title.Length == 0)
            {
                Title = "Main";
            }
        }

        void Start()
        {
            MoveToFront();
        }

        void OnStartDragging()
        {
            isDragging = true;
        }
        
        void OnEndDragging()
        {
            isDragging = false;
            AnimateMoveToFront();
        }

        void AnimateMoveToFront()
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            var start = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            FAnimator.Spawn(tokenSource.Token, 0.25f,
                t => transform.rotation = Quaternion.Lerp(start, CalculateOrientationToCamera(), t));
        }

        public void MoveToFront()
        {
            transform.rotation = CalculateOrientationToCamera();
        }

        Quaternion CalculateOrientationToCamera()
        {
            return Quaternion.LookRotation((transform.position - Settings.MainCameraTransform.position).WithY(0));
        }

        void Update()
        {
            const float damping = 0.05f;

            var mainCameraPose = new Pose(
                Settings.MainCameraTransform.position,
                Quaternion.Euler(0, Settings.MainCameraTransform.eulerAngles.y, 0)
            );

            var currentPose = Transform.AsPose();
            var (targetPosition, targetRotation) = ClampTargetPose(currentPose, mainCameraPose);

            Transform.SetPositionAndRotation(
                Vector3.Lerp(currentPose.position, targetPosition, damping),
                Quaternion.Lerp(currentPose.rotation, targetRotation, damping * 0.5f));
        }

        /*
        Pose ClampTargetPose(in Pose currentPose, in Pose mainCameraPose)
        {
            const float minX = -0.5f;
            const float maxX = 0.5f;
            const float minY = -0.4f;
            const float maxY = 0f;
            const float minZ = 1.0f;
            const float maxZ = 2.0f;
            const float angleThreshold = 15;

            var (currentPositionLocal, currentRotationLocal) = mainCameraPose.InverseMultiply(currentPose);
            var targetPositionLocal = Clamp(currentPositionLocal);

            if (!isDragging)
            {
                targetPositionLocal.z = cameraZ;
            }

            float currentAngleLocal = UnityUtils.RegularizeAngle(currentRotationLocal.eulerAngles.y);

            var targetRotationLocal = Math.Abs(currentAngleLocal) < angleThreshold
                ? currentRotationLocal
                : Quaternion.identity;

            return mainCameraPose.Multiply(new Pose(targetPositionLocal, targetRotationLocal));

            static Vector3 Clamp(in Vector3 value) => new(
                Math.Clamp(value.x, minX, maxX),
                Math.Clamp(value.y, minY, maxY),
                Math.Clamp(value.z, minZ, maxZ)
            );
        }
        */

        Pose ClampTargetPose(in Pose currentPose, in Pose mainCameraPose)
        {
            if (!isDragging)
            {
                return currentPose;
            }

            /*
            const float angleThreshold = 15;

            var currentRotationLocal = mainCameraPose.rotation.Inverse() * currentPose.rotation;

            float currentAngleLocal = UnityUtils.RegularizeAngle(currentRotationLocal.eulerAngles.y);

            var targetRotationLocal = Math.Abs(currentAngleLocal) < angleThreshold
                ? currentRotationLocal
                : Quaternion.identity;

            return new Pose(currentPose.position, mainCameraPose.rotation * targetRotationLocal);
            */
            return new Pose(currentPose.position, CalculateOrientationToCamera());
        }


        public void Suspend()
        {
            tokenSource?.Cancel();
            frame.ReturnToPool();
            frame = null;
        }

        public void SplitForRecycle()
        {
            frame.ReturnToPool();
            background.ReturnToPool();
        }
    }
}