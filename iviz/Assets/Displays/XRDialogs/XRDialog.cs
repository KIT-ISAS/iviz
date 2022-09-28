#nullable enable

using System;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Resources;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public abstract class XRDialog : MonoBehaviour, IDialog
    {
        [SerializeField] Color backgroundColor = Resource.Colors.DefaultBackgroundColor;
        [SerializeField] XRDialogConnector? connector;
        [SerializeField] GameObject? background;

        Transform? mTransform;
        FrameNode? node;
        string? pivotFrameId;
        bool resetOrientation = true;
        Vector3? currentPosition;
        float scale = 1;
        BindingType bindingType = BindingType.None;
        RoundedPlaneDisplay? backgroundObject;

        FrameNode Node => node ??= new FrameNode("Dialog Node");
        XRDialogConnector Connector => connector.AssertNotNull(nameof(connector));

        protected Vector3 SocketPosition { get; set; }

        protected RoundedPlaneDisplay Background
        {
            get
            {
                if (backgroundObject != null)
                {
                    return backgroundObject;
                }

                var validatedBackground = background.AssertNotNull(nameof(background));
                validatedBackground.SetActive(false);
                var backgroundScale3 = validatedBackground.transform.localScale;
                var backgroundSize = new Vector2(backgroundScale3.x, backgroundScale3.y);

                var display = ResourcePool.RentDisplay<RoundedPlaneDisplay>(Transform);
                display.Size = backgroundSize;
                display.Radius = 0.05f;
                backgroundObject = display;
                return backgroundObject;
            }
        }

        public event Action? Expired;

        public BindingType BindingType
        {
            get => bindingType;
            set
            {
                bindingType = value;
                UpdateConnectorVisible();
            }
        }

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
                    UpdateConnectorVisible();
                    return;
                }

                pivotFrameId = null;
                Connector.Visible = false;
            }
        }

        void UpdateConnectorVisible()
        {
            Connector.Visible = BindingType == BindingType.Tf && pivotFrameId != null;
        }

        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        internal Vector3 ConnectorStart =>
            TfModule.RelativeToOrigin(Transform.TransformPoint(SocketPosition));

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
            if (BindingType is BindingType.Tf or BindingType.None)
            {
                Transform.SetParentLocal(TfModule.OriginTransform);
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
                case BindingType.Tf or BindingType.None:
                {
                    /*
                    var framePosition = TfModule.RelativeToOrigin(Node.Transform.TransformPoint(TfFrameOffset));
                    return TfDisplacement.MaxAbsCoeff() == 0
                        ? framePosition
                        : framePosition + GetFlatCameraRotationRelativeTo(framePosition) * TfDisplacement;
                        */


                    var absolutePivotPosition = Node.Transform.TransformPoint(TfFrameOffset);
                    var localFramePosition = TfModule.RelativeToOrigin(absolutePivotPosition);
                    var localCameraRotation = GetFlatCameraRotationRelativeTo(localFramePosition);
                    var localTargetPosition =
                        localFramePosition + localCameraRotation * (TfDisplacement + DialogDisplacement) -
                        SocketPosition;
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

            Connector.Reset();
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

        public static Rect GetCaptionBounds(TMP_Text text)
        {
            var captionTransform = text.rectTransform;
            var captionPosition = captionTransform.anchoredPosition;
            var captionScale = captionTransform.localScale;
            float captionWidth = Mathf.Min(text.preferredWidth, captionTransform.sizeDelta.x) *
                                 captionScale.x;
            float captionHeight = text.preferredHeight * captionScale.y;

            return Rect.MinMaxRect(
                captionPosition.x - captionWidth / 2,
                captionPosition.y - captionHeight,
                captionPosition.x + captionWidth / 2,
                captionPosition.y);
        }

        public static Rect GetCaptionBoundsLeft(TMP_Text text)
        {
            var captionTransform = text.rectTransform;
            var captionPosition = captionTransform.anchoredPosition;
            var captionScale = captionTransform.localScale;
            float sizeDeltaX = captionTransform.sizeDelta.x * captionScale.x;
            float captionWidth = Mathf.Min(text.preferredWidth * captionScale.x, sizeDeltaX);
            float captionHeight = text.preferredHeight * captionScale.y;

            return Rect.MinMaxRect(
                captionPosition.x - sizeDeltaX / 2,
                captionPosition.y - captionHeight,
                captionPosition.x - sizeDeltaX / 2 + captionWidth,
                captionPosition.y);
        }

        protected static Rect GetTitleBounds(TMP_Text text)
        {
            var textTransform = text.rectTransform;
            var titlePosition = textTransform.anchoredPosition;
            var titleScale = textTransform.localScale;
            float titleWidth = text.preferredWidth * titleScale.x;
            float titleHeight = text.preferredHeight * titleScale.y;

            return Rect.MinMaxRect(
                titlePosition.x - titleWidth / 2,
                titlePosition.y - titleHeight / 2,
                titlePosition.x + titleWidth / 2,
                titlePosition.y + titleHeight / 2);
        }

        protected static Rect GetTitleBoundsLeft(TMP_Text text)
        {
            var textTransform = text.rectTransform;
            var titlePosition = textTransform.anchoredPosition;
            var titleScale = textTransform.localScale;
            float sizeDeltaX = textTransform.sizeDelta.x * titleScale.x;
            ;
            float titleWidth = text.preferredWidth * titleScale.x;
            float titleHeight = text.preferredHeight * titleScale.y;

            return Rect.MinMaxRect(
                titlePosition.x - sizeDeltaX / 2,
                titlePosition.y - titleHeight / 2,
                titlePosition.x - sizeDeltaX / 2 + titleWidth,
                titlePosition.y + titleHeight / 2);
        }

        public static Rect GetIconBounds(MonoBehaviour icon)
        {
            var iconTransform = icon.transform;
            var iconScale = iconTransform.localScale;
            var iconPosition = iconTransform.localPosition;
            float iconWidth = iconScale.x;
            float iconHeight = iconScale.z;

            return Rect.MinMaxRect(
                iconPosition.x - iconWidth / 2,
                iconPosition.y - iconHeight / 2,
                iconPosition.x + iconWidth / 2,
                iconPosition.y + iconHeight / 2);
        }

        protected static Rect GetButtonBounds(XRButton button)
        {
            var (center, size) = button.Bounds;
            float buttonScale = button.Transform.localScale.x;
            var scaledCenter = center * buttonScale + button.Transform.localPosition;
            var scaledSize = size * buttonScale;

            return Rect.MinMaxRect(
                scaledCenter.x - scaledSize.x / 2,
                scaledCenter.y - scaledSize.y / 2,
                scaledCenter.x + scaledSize.x / 2,
                scaledCenter.y + scaledSize.y / 2);
        }
    }
}