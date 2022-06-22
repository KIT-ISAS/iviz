#nullable enable

using System;
using System.Threading;
using Iviz.Core;
using Iviz.Resources;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class CanvasHolder : MonoBehaviour, IDisplay, IRecyclable
    {
        const float HeaderHeight = 0.075f;

        [SerializeField] Canvas? canvas;
        [SerializeField] BoxCollider? boxCollider;
        [SerializeField] FixedDistanceDraggable? draggable;
        [SerializeField] TMP_Text? label;
        [SerializeField] bool followsCamera;
        [SerializeField] Vector2 canvasSize = new Vector2(800, 600);

        CancellationTokenSource? tokenSource;
        RoundedPlaneDisplay? background;
        SelectionFrame? frame;
        Transform? mTransform;

        bool isDragging;
        float cameraZ = 1.5f;

        BoxCollider BoxCollider => boxCollider.AssertNotNull(nameof(boxCollider));
        RoundedPlaneDisplay Background => ResourcePool.RentChecked(ref background, Holder);
        FixedDistanceDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        Transform Holder => Draggable.Transform;
        TMP_Text Label => label.AssertNotNull(nameof(label));

        public Transform Transform => this.EnsureHasTransform(ref mTransform);

        public string Title
        {
            get => Label.text;
            set => Label.text = value;
        }

        public Canvas Canvas => canvas.AssertNotNull(nameof(canvas));

        public Vector2 CanvasSize
        {
            get => canvasSize;
            set
            {
                canvasSize = value;
                var canvasTransform = (RectTransform)Canvas.transform;
                canvasTransform.sizeDelta = canvasSize;
                var (sizeX, sizeY) = Vector2.Scale(canvasTransform.rect.size, canvasTransform.localScale);
                Background.Size = new Vector2(sizeX, HeaderHeight);
                BoxCollider.size = new Vector3(sizeX, HeaderHeight, 0.005f);
                Draggable.Transform.localPosition = (sizeY + HeaderHeight + 0.01f) / 2 * Vector3.up;
            }
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
            if (Canvas.worldCamera == null)
            {
                Canvas.worldCamera = Settings.MainCamera;
            }

            Background.Radius = 0.01f;
            Background.Color = Resource.Colors.TooltipBackground;
            Background.EnableShadows = false;
            Background.Layer = LayerType.Clickable;

            Draggable.ForwardScale = 5f;
            Draggable.StateChanged += () =>
            {
                bool visible = Draggable.IsDragging || Draggable.IsHovering;
                if (visible)
                {
                    if (frame == null)
                    {
                        frame = ResourcePool.RentDisplay<SelectionFrame>(Holder);
                        frame.Size = BoxCollider.size;
                    }

                    frame.Color = Draggable.IsDragging ? Color.cyan : Color.white;
                    frame.EmissiveColor = Draggable.IsDragging ? Color.blue : Color.black;
                    frame.ColumnWidth = Draggable.IsDragging ? 0.002f : 0.001f;
                }
                else
                {
                    frame.ReturnToPool();
                    frame = null;
                }                
            };

            Draggable.StartDragging += OnStartDragging;
            Draggable.EndDragging += OnEndDragging;

            CanvasSize = CanvasSize;

            if (Title.Length == 0)
            {
                Title = "Main";
            }

            FollowsCamera = followsCamera;
        }

        public Pose GetTargetPose()
        {
            var mainCameraPose = new Pose(
                Settings.MainCameraPose.position,
                Quaternion.Euler(0, Settings.MainCameraPose.rotation.eulerAngles.y, 0)
            );

            var canvasTransform = (RectTransform)Canvas.transform;
            float sizeY = canvasTransform.rect.size.y * canvasTransform.localScale.y;
            var targetPosition = mainCameraPose.position + mainCameraPose.forward * 1.5f - mainCameraPose.up * 0.2f;
            targetPosition.y = Mathf.Max(targetPosition.y, sizeY / 2 + 0.5f);
            cameraZ = 1.5f;

            var targetRotation = CalculateOrientationToCamera();

            return ClampTargetPose(new Pose(targetPosition, targetRotation), mainCameraPose);
        }

        public void InitializePose()
        {
            transform.SetPose(GetTargetPose());

            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            FAnimator.Spawn(tokenSource.Token, 0.15f, t =>
            {
                float scale = Mathf.Sqrt(t);
                transform.localScale = scale * Vector3.one;
            });
        }

        void OnStartDragging()
        {
            isDragging = true;
        }
        
        void OnEndDragging()
        {
            isDragging = false;
            
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();
            
            var mainCameraPose = new Pose(
                Settings.MainCameraPose.position,
                Quaternion.Euler(0, Settings.MainCameraPose.rotation.eulerAngles.y, 0)
            );
            var currentPose = Transform.AsPose();

            const float minZ = 1.0f;
            const float maxZ = 2.0f;

            cameraZ = Mathf.Clamp(mainCameraPose.InverseMultiply(currentPose).position.z, minZ, maxZ);

            var start = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            FAnimator.Spawn(tokenSource.Token, 0.25f,
                t => transform.rotation = Quaternion.Lerp(start, CalculateOrientationToCamera(), t));
        }

        Quaternion CalculateOrientationToCamera()
        {
            return Quaternion.LookRotation((Transform.position - Settings.MainCameraPose.position).WithY(0));
        }

        public void ReturnToPool()
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            FAnimator.Spawn(tokenSource.Token, 0.15f, t =>
            {
                float scale = Mathf.Sqrt(1 - t);
                transform.localScale = scale * Vector3.one;
            }, () =>
            {
                transform.localScale = Vector3.one;
                ResourceUtils.ReturnToPool(this);
            });
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

            var targetRotationLocal = Mathf.Abs(currentAngleLocal) < angleThreshold
                ? currentRotationLocal
                : Quaternion.identity;

            return mainCameraPose.Multiply(new Pose(targetPositionLocal, targetRotationLocal));

            static Vector3 Clamp(in Vector3 value) => new(
                Mathf.Clamp(value.x, minX, maxX),
                Mathf.Clamp(value.y, minY, maxY),
                Mathf.Clamp(value.z, minZ, maxZ)
            );
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