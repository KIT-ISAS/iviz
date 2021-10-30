using System;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.MsgsGen.Dynamic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class ARTfFrame : MarkerResource, ISupportsTint
    {
        [SerializeField] TMP_Text text = null;
        [SerializeField] AxisFrameResource axisFrame = null;
        [SerializeField] MeshMarkerResource cylinder = null;
        [CanBeNull] FrameNode node;
        
        public string Caption
        {
            get => text.text;
            set => text.text = value;
        }

        bool frameVisible = true;

        bool FrameVisible
        {
            get => frameVisible;
            set
            {
                if (frameVisible == value)
                {
                    return;
                }

                frameVisible = value;
                axisFrame.Visible = value;
            }
        }

        Color tint = Color.white;

        public Color Tint
        {
            get => tint;
            set
            {
                tint = value;
                cylinder.Tint = tint;
                axisFrame.Tint = tint;
                text.color = tint;
            }
        }

        public string ParentFrame
        {
            set
            {
                if (node == null)
                {
                    node = FrameNode.Instantiate("ARTfFrame Node");
                    Transform.parent = node.Transform;
                }

                node.AttachTo(value);
            }
        }

        Pose? currentPose;
        public Pose TargetPose { get; set; }

        void Update()
        {
            FrameVisible = !TfListener.Instance.FramesVisible;

            const float maxDistance = 0.5f;
            const float minDistance = 0.3f;

            if (!ARController.IsVisible)
            {
                axisFrame.Visible = true;
                cylinder.Visible = true;
                text.gameObject.SetActive(true);
                Tint = Color.white;
            }
            else
            {
                float distance = (Transform.position - Settings.MainCameraTransform.position).magnitude;
                float alpha = Mathf.Max(Mathf.Min(1 - (distance - minDistance) / (maxDistance - minDistance), 1), 0);

                if (alpha == 0)
                {
                    axisFrame.Visible = false;
                    cylinder.Visible = false;
                    text.gameObject.SetActive(false);
                }
                else
                {
                    axisFrame.Visible = true;
                    cylinder.Visible = true;
                    text.gameObject.SetActive(true);
                    Tint = Color.white.WithAlpha(alpha);
                }
            }


            if (currentPose == null)
            {
                currentPose = TargetPose;
            }
            else
            {
                Vector3 deltaPosition = TargetPose.position - Transform.localPosition;
                Vector4 deltaRotation = TargetPose.rotation.ToVector() - Transform.localRotation.ToVector();
                if (deltaPosition.MaxAbsCoeff() < 0.001f && deltaRotation.MaxAbsCoeff() < 0.01f)
                {
                    return;
                }

                if (deltaPosition.MaxAbsCoeff() > 0.5f)
                {
                    Transform.localPosition = TargetPose.position;
                }
                else
                {
                    Transform.localPosition += 0.05f * deltaPosition;
                }

                Transform.localRotation = Quaternion.Slerp(Transform.localRotation, TargetPose.rotation, 0.05f);
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            currentPose = null;
        }
        
        void OnDestroy()
        {
            if (node != null)
            {
                node.DestroySelf();
            }
        }
    }
}