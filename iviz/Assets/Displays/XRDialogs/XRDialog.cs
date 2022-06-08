#nullable enable

using System;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public abstract class XRDialog : MonoBehaviour, IDialog
    {
        [SerializeField] Transform? _transform;
        [SerializeField] Vector2 backgroundSize = new Vector2(1, 1);
        [SerializeField] Vector3 socketPosition = Vector3.zero;
        [SerializeField] Color backgroundColor = Resource.Colors.DefaultBackgroundColor;
        [SerializeField] XRDialogConnector? connector;
        [SerializeField] GameObject? background;

        FrameNode? node;
        string? pivotFrameId;
        bool resetOrientation = true;
        Vector3? currentPosition;
        float scale = 1;

        ISupportsColor? backgroundObject;

        FrameNode Node => node ??= new FrameNode("Dialog Node");
        XRDialogConnector Connector => connector.AssertNotNull(nameof(connector));

        ISupportsColor Background
        {
            get
            {
                if (backgroundObject != null)
                {
                    return backgroundObject;
                }

                if (background != null && background.TryGetComponent<ISupportsColor>(out var newBackgroundObject))
                {
                    backgroundObject = newBackgroundObject;
                    return backgroundObject;
                }

                var display = ResourcePool.RentDisplay<RoundedPlaneDisplay>(Transform);
                display.Size = backgroundSize;
                display.Radius = 0.05f;
                backgroundObject = display;
                return backgroundObject;
            }
        }

        public abstract bool Interactable { set; }

        public event Action? Expired;

        public BindingType BindingType { get; set; }
        public Vector3 DialogDisplacement { get; set; }
        public Vector3 TfFrameOffset { get; set; }
        public Vector3 TfDisplacement { get; set; }

        public float PositionDamping { get; set; } = 0.05f;

        public float Scale
        {
            set
            {
                scale = value;
                transform.localScale = scale * Vector3.one;
            }
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public virtual Color Color
        {
            set
            {
                backgroundColor = value;
                Background.Color = value;
            }
        }

        public string? PivotFrameId
        {
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    pivotFrameId = value;
                    Node.AttachTo(pivotFrameId);
                    currentPosition = null;
                    resetOrientation = true;
                    Connector.Visible = pivotFrameId != null;
                    return;
                }

                pivotFrameId = null;
                Connector.Visible = false;
            }
        }

        Transform Transform => _transform != null ? _transform : (_transform = transform);

        internal Vector3 ConnectorStart =>
            TfModule.RelativeToOrigin(Transform.TransformPoint(socketPosition));

        internal Vector3 ConnectorEnd
        {
            get
            {
                var framePosition = TfModule.RelativeToOrigin(Node.Transform.TransformPoint(TfFrameOffset));
                return TfDisplacement.MaxAbsCoeff() == 0
                    ? framePosition
                    : framePosition + GetFlatCameraRotationRelativeTo(framePosition) * TfDisplacement;
            }
        }

        public void Initialize()
        {
            if (BindingType == BindingType.Tf)
            {
                Transform.SetParentLocal(TfModule.OriginTransform);
                Connector.Visible = true;
            }

            resetOrientation = true;
            currentPosition = null;

            GameThread.AfterFramesUpdatedLate += UpdatePose;
        }

        protected virtual void Awake()
        {
            if (backgroundColor.a != 0)
            {
                Color = backgroundColor;
            }

            Scale = scale;
            PivotFrameId = pivotFrameId;
        }

        void UpdatePose()
        {
            UpdatePosition();
            UpdateRotation();
        }

        void UpdateRotation()
        {
            var cameraPosition = Settings.MainCameraPose.position;
            if (cameraPosition.ApproximatelyZero())
            {
                // not initialized yet
                return;
            }

            var direction = Transform.position - cameraPosition;
            var targetRotation = Quaternion.LookRotation(direction.ApproximatelyZero() ? Vector3.forward : direction);
            if (resetOrientation)
            {
                Transform.rotation = targetRotation;
                resetOrientation = false;
                return;
            }

            var currentRotation = Transform.rotation;
            Transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, 0.05f);
        }

        void UpdatePosition()
        {
            Vector3 absoluteTargetPosition;

            switch (BindingType)
            {
                case BindingType.None:
                case BindingType.Tf:
                {
                    var absolutePivotPosition = Node.Transform.TransformPoint(TfFrameOffset);
                    var localFramePosition = TfModule.RelativeToOrigin(absolutePivotPosition);
                    var localCameraRotation = GetFlatCameraRotationRelativeTo(localFramePosition);
                    var localTargetPosition =
                        localFramePosition + localCameraRotation * DialogDisplacement - socketPosition;
                    absoluteTargetPosition = TfModule.OriginTransform.TransformPoint(localTargetPosition);
                    break;
                }
                case BindingType.User:
                {
                    var cameraForward = Settings.MainCameraPose.Forward();
                    if ((cameraForward - Vector3.forward).ApproximatelyZero())
                    {
                        // not initialized yet
                        return;
                    }

                    var absoluteRotation = Quaternion.LookRotation(cameraForward.WithY(0));
                    var absolutePose = new Pose(Settings.MainCameraPose.position, absoluteRotation);
                    absoluteTargetPosition = absolutePose.Multiply(DialogDisplacement) + TfFrameOffset;
                    break;
                }
                default:
                    return;
            }

            Vector3 nextAbsolutePosition;
            if (currentPosition is not { } position)
            {
                nextAbsolutePosition = absoluteTargetPosition;
            }
            else
            {
                var deltaPosition = absoluteTargetPosition - Transform.position;
                if (deltaPosition.MaxAbsCoeff() < 0.001f)
                {
                    return;
                }

                nextAbsolutePosition = position + deltaPosition * PositionDamping;
            }

            currentPosition = nextAbsolutePosition;
            Transform.position = nextAbsolutePosition;
        }

        public virtual void Suspend()
        {
            DialogDisplacement = Vector3.zero;
            TfDisplacement = Vector3.zero;
            TfFrameOffset = Vector3.zero;

            currentPosition = null;
            resetOrientation = true;

            Connector.Visible = false;
            Expired?.Invoke();
            Expired = null;
            
            GameThread.AfterFramesUpdatedLate -= UpdatePose;
        }

        static Quaternion GetFlatCameraRotationRelativeTo(in Vector3 localPosition)
        {
            var originTransform = TfModule.OriginTransform;
            var absolutePosition = originTransform.TransformPoint(localPosition);
            var direction = absolutePosition - Settings.MainCameraPose.position;
            var absoluteRotation =
                Quaternion.LookRotation((direction.ApproximatelyZero() ? Vector3.forward : direction).WithY(0));
            return originTransform.rotation.Inverse() * absoluteRotation;
        }

        protected static void SetupButtons(XRButton button1, XRButton button2, XRButton button3, ButtonSetup value)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;

            switch (value)
            {
                case ButtonSetup.Ok:
                    button1.Visible = true;
                    button1.Icon = XRIcon.Ok;
                    button1.Caption = "OK";
                    break;
                case ButtonSetup.Forward:
                    button1.Visible = true;
                    button1.Icon = XRIcon.Forward;
                    button1.Caption = "OK";
                    break;
                case ButtonSetup.Backward:
                    button1.Visible = true;
                    button1.Icon = XRIcon.Backward;
                    button1.Caption = "Back";
                    break;
                case ButtonSetup.YesNo:
                    button2.Visible = true;
                    button2.Icon = XRIcon.Ok;
                    button2.Caption = "Yes";
                    button3.Visible = true;
                    button3.Icon = XRIcon.Cross;
                    button3.Caption = "No";
                    break;
                case ButtonSetup.ForwardBackward:
                    button2.Visible = true;
                    button2.Icon = XRIcon.Backward;
                    button2.Caption = "Back";
                    button3.Visible = true;
                    button3.Icon = XRIcon.Forward;
                    button3.Caption = "Forward";
                    break;
                case ButtonSetup.OkCancel:
                    button2.Visible = true;
                    button2.Icon = XRIcon.Ok;
                    button2.Caption = "OK";
                    button3.Visible = true;
                    button3.Icon = XRIcon.Cross;
                    button3.Caption = "Cancel";
                    break;
            }
        }
    }
}