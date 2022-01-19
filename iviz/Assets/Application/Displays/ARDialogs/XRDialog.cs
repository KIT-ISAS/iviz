#nullable enable

using System;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XRDialogs;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public abstract class XRDialog : MonoBehaviour, IDialog
    {
        [SerializeField] Transform? _transform;
        [SerializeField] Vector3 socketPosition = Vector3.zero;
        [SerializeField] MeshMarkerResource? background;

        float scale = 1;
        DialogConnector? connector;
        FrameNode? node;
        string? pivotFrameId;
        bool resetOrientation;
        Vector3? currentPosition;

        FrameNode Node => node ??= FrameNode.Instantiate("Dialog Node");
        DialogConnector Connector => connector.AssertNotNull(nameof(connector));
        MeshMarkerResource Background => background.AssertNotNull(nameof(background));
        Vector3 BaseDisplacement => -socketPosition * scale;

        public event Action? Expired;

        public Vector3 DialogDisplacement { get; set; }
        public Vector3 PivotFrameOffset { get; set; }
        public Vector3 PivotDisplacement { get; set; }

        public float Scale
        {
            set
            {
                scale = value;
                transform.localScale = scale * Vector3.one;
            }
        }

        public Bounds? Bounds => Background.Bounds;

        public int Layer
        {
            set { }
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public virtual Color Color
        {
            set => Background.Color = value;
        }

        public string? PivotFrameId
        {
            get => pivotFrameId;
            set
            {
                pivotFrameId = value;
                if (pivotFrameId != null)
                {
                    Node.AttachTo(pivotFrameId);
                    currentPosition = null;
                    resetOrientation = true;
                }

                Connector.Visible = pivotFrameId != null;
            }
        }

        public Transform Transform => _transform != null ? _transform : (_transform = transform);

        internal bool NeedsConnector => pivotFrameId != null;
        
        internal Vector3 ConnectorStart => Transform.localPosition - BaseDisplacement;

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
            if (PivotFrameId == null)
            {
                return;
            }

            var frameLocalPosition = TfListener.RelativeToOrigin(Node.Transform.position) + PivotFrameOffset;
            var cameraLocalRotation = GetFlatCameraRotation(frameLocalPosition);

            var targetLocalPosition = frameLocalPosition + cameraLocalRotation * DialogDisplacement + BaseDisplacement;
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
    }
}