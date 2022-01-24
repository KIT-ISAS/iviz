#nullable enable

using System;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XRDialogs
{
    public abstract class XRDialog : MonoBehaviour, IDialog
    {
        [SerializeField] Transform? _transform;
        [SerializeField] Vector3 socketPosition = Vector3.zero;
        [SerializeField] MeshMarkerDisplay? background;
        [SerializeField] Color backgroundColor = Resource.Colors.DefaultBackgroundColor;
        [SerializeField] XRDialogConnector? connector;

        FrameNode? node;
        string? pivotFrameId;
        bool resetOrientation;
        Vector3? currentPosition;
        float scale = 1;

        FrameNode Node => node ??= FrameNode.Instantiate("Dialog Node");
        XRDialogConnector Connector => connector.AssertNotNull(nameof(connector));
        MeshMarkerDisplay Background => background.AssertNotNull(nameof(background));
        Vector3 baseDisplacement;

        public event Action? Expired;

        public Vector3 DialogDisplacement { get; set; }
        public Vector3 PivotFrameOffset { get; set; }
        public Vector3 PivotDisplacement { get; set; }

        public float Scale
        {
            set
            {
                scale = value;
                baseDisplacement = -socketPosition * scale;
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

        public string PivotFrameId
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
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

        internal Vector3 ConnectorStart => Transform.AsLocalPose().Multiply(-baseDisplacement);

        internal Vector3 ConnectorEnd
        {
            get
            {
                var framePosition = TfListener.RelativeToOrigin(Node.Transform.position) + PivotFrameOffset;
                return PivotDisplacement.MaxAbsCoeff() == 0
                    ? framePosition
                    : framePosition + GetFlatCameraRotation(framePosition) * PivotDisplacement;
            }
        }

        public void Initialize()
        {
            Transform.SetParentLocal(TfListener.OriginFrame.Transform);
            Connector.Visible = true;
            resetOrientation = true;
            Update();
        }

        protected virtual void Awake()
        {
            Color = backgroundColor;
            Scale = scale;
        }

        void Update()
        {
            UpdatePosition();
            UpdateRotation();
        }

        void UpdateRotation()
        {
            var targetRotation = Quaternion.LookRotation(Transform.position - Settings.MainCameraTransform.position);
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
            if (pivotFrameId == null)
            {
                return;
            }

            var frameLocalPosition = TfListener.RelativeToOrigin(Node.Transform.position) + PivotFrameOffset;
            var cameraLocalRotation = GetFlatCameraRotation(frameLocalPosition);

            var targetLocalPosition = frameLocalPosition + cameraLocalRotation * DialogDisplacement + baseDisplacement;
            var targetAbsolutePosition = TfListener.OriginFrame.Transform.TransformPoint(targetLocalPosition);

            Vector3 nextAbsolutePosition;
            if (currentPosition is not { } position)
            {
                nextAbsolutePosition = targetAbsolutePosition;
            }
            else
            {
                var deltaPosition = targetAbsolutePosition - Transform.position;
                if (deltaPosition.MaxAbsCoeff() < 0.001f)
                {
                    return;
                }

                nextAbsolutePosition = position + deltaPosition * 0.05f;
            }

            currentPosition = nextAbsolutePosition;
            Transform.position = nextAbsolutePosition;
        }

        public virtual void Suspend()
        {
            Connector.Visible = false;
            currentPosition = null;
            Expired?.Invoke();
            Expired = null;
        }

        static Quaternion GetFlatCameraRotation(in Vector3 localPosition)
        {
            var absolutePosition = TfListener.OriginFrame.Transform.TransformPoint(localPosition);
            (float x, _, float z) = absolutePosition - Settings.MainCameraTransform.position;
            float targetAngle = -Mathf.Atan2(z, x) * Mathf.Rad2Deg + 90;
            var absoluteRotation = Quaternion.AngleAxis(targetAngle, Vector3.up);

            return TfListener.OriginFrame.Transform.rotation.Inverse() * absoluteRotation;
        }
        
        internal static void SetupButtons(XRButton button1, XRButton button2, XRButton button3, XRButtonSetup value)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;

            switch (value)
            {
                case XRButtonSetup.Ok:
                    button1.Visible = true;
                    button1.Icon = XRIcon.Ok;
                    button1.Caption = "Ok";
                    break;
                case XRButtonSetup.Forward:
                    button1.Visible = true;
                    button1.Icon = XRIcon.Forward;
                    button1.Caption = "Ok";
                    break;
                case XRButtonSetup.Backward:
                    button1.Visible = true;
                    button1.Icon = XRIcon.Backward;
                    button1.Caption = "Back";
                    break;
                case XRButtonSetup.YesNo:
                    button2.Visible = true;
                    button2.Icon = XRIcon.Ok;
                    button2.Caption = "Yes";
                    button3.Visible = true;
                    button3.Icon = XRIcon.Cross;
                    button3.Caption = "No";
                    break;
                case XRButtonSetup.ForwardBackward:
                    button2.Visible = true;
                    button2.Icon = XRIcon.Backward;
                    button2.Caption = "Back";
                    button3.Visible = true;
                    button3.Icon = XRIcon.Forward;
                    button3.Caption = "Forward";
                    break;
                case XRButtonSetup.OkCancel:
                    button2.Visible = true;
                    button2.Icon = XRIcon.Ok;
                    button2.Caption = "Ok";
                    button3.Visible = true;
                    button3.Icon = XRIcon.Cross;
                    button3.Caption = "Cancel";
                    break;
            }
        }        
    }
}