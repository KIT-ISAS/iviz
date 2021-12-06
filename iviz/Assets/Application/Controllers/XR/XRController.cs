#nullable enable

using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers.XR
{
    public sealed class XRController : IController, IHasFrame
    {
        const string HeadFrameId = "~xr/head";
        const string LeftControllerFrameId = "~xr/left_controller";
        const string RightControllerFrameId = "~xr/right_controller";
        const string GazeFrameId = "~xr/gaze";

        static readonly XRGazeState DefaultGazeState = new();
        static readonly XRHandState DefaultHandState = new();

        public Sender<XRGazeState> GazeSender { get; } = new("~xr/gaze");
        public Sender<XRHandState> LeftHandSender { get; } = new("~xr/left_hand");
        public Sender<XRHandState> RightHandSender { get; } = new("~xr/right_hand");

        readonly GazeController gaze;
        readonly XRRayInteractor gazeInteractor;
        readonly HandController leftHand;
        readonly HandController rightHand;
        readonly PaddleController leftPaddle;
        readonly PaddleController rightPaddle;
        readonly Transform cameraOffset;

        bool gazeActive;
        bool leftHandActive;
        bool rightHandActive;

        uint gazeSeqNr;
        uint leftHandSeqNr;
        uint rightHandSeqNr;

        public XRConfiguration Config { get; }

        public IModuleData ModuleData { get; }

        public TfFrame Frame => TfListener.GetOrCreateFrame(HeadFrameId);

        public XRController(IModuleData moduleData, XRMainController controller, XRConfiguration? configuration)
        {
            gaze = controller.Gaze;
            gazeInteractor = gaze.GetComponent<XRRayInteractor>().AssertNotNull(nameof(gazeInteractor));
            leftHand = controller.LeftHand;
            rightHand = controller.RightHand;
            leftPaddle = controller.LeftPaddle;
            rightPaddle = controller.RightPaddle;
            cameraOffset = controller.CameraOffset;
            ModuleData = moduleData;

            Config = configuration ?? new XRConfiguration();

            leftPaddle.gameObject.SetActive(!Settings.IsHololens);
            rightPaddle.gameObject.SetActive(!Settings.IsHololens);
            leftHand.gameObject.SetActive(Settings.IsHololens);
            rightHand.gameObject.SetActive(Settings.IsHololens);

            Frame.ForceInvisible = true;
            TfListener.GetOrCreateFrame(LeftControllerFrameId).ForceInvisible = true;
            TfListener.GetOrCreateFrame(RightControllerFrameId).ForceInvisible = true;
            TfListener.GetOrCreateFrame(GazeFrameId).ForceInvisible = true;

            GameThread.EveryFrame += Update;
        }

        void Update()
        {

            UpdateGazeSender();
            UpdateHandSender(leftHand, LeftHandSender, ref leftHandSeqNr, ref leftHandActive);
            UpdateHandSender(rightHand, RightHandSender, ref rightHandSeqNr, ref rightHandActive);

            TfListener.Publish(HeadFrameId, Settings.MainCameraTransform.AsPose());

            if (gaze.isActiveAndEnabled)
            {
                if (gaze.IsActiveInFrame)
                {
                    var gazeTransform = gaze.transform;
                    var gazeForward = gazeTransform.forward;
                    var gazePosition = gazeTransform.position;
                    var gazeRotation = gazeTransform.rotation;

                    var pose = gazeInteractor.TryGetHitInfo(out var hitPosition, out _, out _, out _)
                        ? new Pose(hitPosition, gazeRotation)
                        : new Pose(gazePosition + 5 * gazeForward, gazeRotation);
                    TfListener.Publish(GazeFrameId, pose);
                }
                else
                {
                    TfListener.Publish(GazeFrameId, default(Pose));
                }
            }

            CustomController? leftController =
                leftHand.isActiveAndEnabled ? leftHand :
                leftPaddle.isActiveAndEnabled ? leftPaddle : null;
            CustomController? rightController =
                rightHand.isActiveAndEnabled ? rightHand :
                rightPaddle.isActiveAndEnabled ? rightPaddle : null;

            TfListener.Publish(LeftControllerFrameId,
                leftController is { IsActiveInFrame: true } ? leftController.transform.AsPose() : default);

            TfListener.Publish(RightControllerFrameId,
                rightController is { IsActiveInFrame: true } ? rightController.transform.AsPose() : default);
        }

        void UpdateHandSender(
            HandController handController,
            Sender<XRHandState> sender,
            ref uint seqNr,
            ref bool active)
        {
            if (!handController.isActiveAndEnabled)
            {
                return;
            }

            var handState = handController.HandState;
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

        void UpdateGazeSender()
        {
            if (!gaze.isActiveAndEnabled)
            {
                return;
            }

            if (gaze.Pose is { } gazePose)
            {
                gazeActive = true;
                var state = new XRGazeState
                {
                    IsValid = true,
                    Header = (gazeSeqNr++, TfListener.FixedFrameId),
                    Transform = ToTransform(gazePose)
                };
                GazeSender.Publish(state);
                return;
            }

            if (gazeActive)
            {
                GazeSender.Publish(DefaultGazeState);
                gazeActive = false;
            }
        }

        Msgs.GeometryMsgs.Transform ToTransform(Pose pose)
        {
            return TfListener.RelativePoseToFixedFrame(cameraOffset.TransformPose(pose)).Unity2RosTransform();
        }

        public void Dispose()
        {
            GameThread.EveryFrame -= Update;
        }

        public void ResetController()
        {
        }

        public bool Visible { get; set; }
    }
}