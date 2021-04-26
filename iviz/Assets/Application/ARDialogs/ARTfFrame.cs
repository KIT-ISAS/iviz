using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using TMPro;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public class ARTfFrame : MarkerResource
    {
        [SerializeField] Transform pivotTransform;
        [SerializeField] TMP_Text text;
        [SerializeField] AxisFrameResource axisFrame;

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

        Pose? currentPose;
        public Pose TargetPose { get; set; }

        void Update()
        {
            FrameVisible = !TfListener.Instance.FramesVisible;
            if (currentPose == null)
            {
                currentPose = TargetPose;
            }
            else
            {
                Vector3 deltaPosition = TargetPose.position - Transform.position;
                Vector4 deltaRotation = TargetPose.rotation.ToVector() - Transform.rotation.ToVector();
                if (deltaPosition.MaxAbsCoeff() < 0.001f && deltaRotation.MaxAbsCoeff() < 0.01f)
                {
                    return;
                }

                Transform.position += 0.05f * deltaPosition;
                Transform.rotation = Quaternion.Slerp(Transform.rotation, TargetPose.rotation, 0.05f);
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            currentPose = null;
        }
    }
}