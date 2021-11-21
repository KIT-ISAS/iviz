using System;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Ros;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using Pose = UnityEngine.Pose;
using Transform = UnityEngine.Transform;

/*
 * TODO:
 * reset console log
 * improve log
 * unselect description system info
 */

namespace Iviz.Controllers
{
    public sealed class XRMainController : MonoBehaviour
    {
        const string HeadFrameId = "~xr/head";

        static readonly XRGazeState DefaultGazeState = new();
        static readonly XRHandState DefaultHandState = new();

        [SerializeField] GazeController gaze;
        [SerializeField] HandController leftHand;
        [SerializeField] HandController rightHand;
        [SerializeField] Transform cameraOffset;

        bool gazeActive;
        bool leftHandActive;
        bool rightHandActive;

        uint gazeSeqNr;
        uint leftHandSeqNr;
        uint rightHandSeqNr;

        [CanBeNull] Sender<XRGazeState> gazeSender;
        [CanBeNull] Sender<XRHandState> leftHandSender;
        [CanBeNull] Sender<XRHandState> rightHandSender;

        void Start()
        {
            TfListener.ResolveFrame(HeadFrameId).ForceInvisible = true;
        }

        void Update()
        {
            gazeSender ??= new Sender<XRGazeState>("~xr/gaze");
            UpdateGazeSender(gazeSender);

            leftHandSender ??= new Sender<XRHandState>("~xr/left_hand");
            UpdateHandSender(leftHand.HandState, leftHandSender, ref leftHandSeqNr, ref leftHandActive);

            rightHandSender ??= new Sender<XRHandState>("~xr/right_hand");
            UpdateHandSender(rightHand.HandState, rightHandSender, ref rightHandSeqNr, ref rightHandActive);

            TfListener.Publish(HeadFrameId, Settings.MainCameraTransform.AsPose());
        }

        void UpdateGazeSender([NotNull] Sender<XRGazeState> sender)
        {
            if (gaze.Pose is { } gazePose)
            {
                gazeActive = true;
                var state = new XRGazeState
                {
                    IsValid = true,
                    Header = (gazeSeqNr++, TfListener.FixedFrameId),
                    Transform = ToTransform(gazePose)
                };
                sender.Publish(state);
                return;
            }

            if (gazeActive)
            {
                sender.Publish(DefaultGazeState);
                gazeActive = false;
            }
        }

        void UpdateHandSender(
            [CanBeNull] HandState handState,
            [NotNull] Sender<XRHandState> sender,
            ref uint seqNr,
            ref bool active)
        {
            if (handState != null)
            {
                active = true;
                var state = new XRHandState
                {
                    IsValid = true,
                    Header = (seqNr++, TfListener.FixedFrameId),
                    Palm = ToTransform(handState.palm),
                    Thumb = handState.fingers[(int)HandFinger.Thumb].Select(ToTransform).ToArray(),
                    Index = handState.fingers[(int)HandFinger.Index].Select(ToTransform).ToArray(),
                    Middle = handState.fingers[(int)HandFinger.Middle].Select(ToTransform).ToArray(),
                    Ring = handState.fingers[(int)HandFinger.Ring].Select(ToTransform).ToArray(),
                    Little = handState.fingers[(int)HandFinger.Pinky].Select(ToTransform).ToArray()
                };

                sender.Publish(state);
                return;
            }

            if (active)
            {
                sender.Publish(DefaultHandState);
                active = false;
            }
        }

        Msgs.GeometryMsgs.Transform ToTransform(Pose pose)
        {
            return TfListener.RelativePoseToFixedFrame(cameraOffset.TransformPose(pose)).Unity2RosTransform();
        }
    }
}