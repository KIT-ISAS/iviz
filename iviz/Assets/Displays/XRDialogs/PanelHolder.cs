#nullable enable

using System;
using System.Threading;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class PanelHolder : MonoBehaviour, IDisplay, IRecyclable
    {
        const float HeaderHeight = 0.06f;

        [SerializeField] BoxCollider? panelCollider;
        [SerializeField] BoxCollider? boxCollider;
        [SerializeField] MeshMarkerDisplay? back;
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
        RoundedPlaneDisplay Background => ResourcePool.RentChecked(ref background, Holder);
        FixedDistanceDraggable Draggable => draggable.AssertNotNull(nameof(draggable));
        MeshMarkerDisplay Back => back.AssertNotNull(nameof(back));
        Transform Holder => Draggable.Transform;
        TMP_Text Label => label.AssertNotNull(nameof(label));
        public Transform Transform => this.EnsureHasTransform(ref mTransform);

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
            Back.Transform.localScale = new Vector3(sizeX, sizeY, 1);
            Back.Transform.localPosition = new Vector3(0, (sizeY + HeaderHeight + gapSize) / 2, 0.01f);
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

            if (PanelCollider.TryGetComponent<ISupportsDynamicBounds>(out var bounds))
            {
                bounds.BoundsChanged += UpdateSize;
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

            if (Title.Length == 0)
            {
                Title = "Main";
            }
        }

        void Start()
        {
            RotateToFront();
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

            var start = Quaternion.Euler(0, Transform.rotation.eulerAngles.y, 0);
            FAnimator.Spawn(tokenSource.Token, 0.25f,
                t => Transform.rotation = Quaternion.Lerp(start, CalculateOrientationToCamera(), t));
        }

        public void RotateToFront()
        {
            Transform.rotation = CalculateOrientationToCamera();
        }

        Quaternion CalculateOrientationToCamera()
        {
            return Quaternion.LookRotation((Transform.position - Settings.MainCameraPose.position).WithY(0));
        }

        void Update()
        {
            const float damping = 0.05f;

            var currentPose = Transform.AsPose();
            var (targetPosition, targetRotation) = ClampTargetPose(currentPose);

            Transform.SetPositionAndRotation(
                Vector3.Lerp(currentPose.position, targetPosition, damping),
                Quaternion.Lerp(currentPose.rotation, targetRotation, damping * 0.5f));
        }

        Pose ClampTargetPose(in Pose currentPose)
        {
            if (!isDragging)
            {
                return currentPose;
            }

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