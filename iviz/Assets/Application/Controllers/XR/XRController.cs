#nullable enable

using System;
using System.Threading;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
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
        readonly CanvasHolder mainCanvasHolder;

        readonly HandGestures leftGestures = new(HandType.Left);
        readonly HandGestures rightGestures = new(HandType.Right);

        CustomController? LeftController =>
            leftHand.isActiveAndEnabled ? leftHand :
            leftPaddle.isActiveAndEnabled ? leftPaddle : null;

        CustomController? RightController =>
            rightHand.isActiveAndEnabled ? rightHand :
            rightPaddle.isActiveAndEnabled ? rightPaddle : null;

        bool gazeActive;
        bool leftHandActive;
        bool rightHandActive;

        uint gazeSeqNr;
        uint leftHandSeqNr;
        uint rightHandSeqNr;

        CancellationTokenSource? tokenSource;

        public static XRController? Instance { get; private set; }

        public XRConfiguration Config { get; }

        public IModuleData ModuleData { get; }

        public TfFrame Frame => TfListener.GetOrCreateFrame(HeadFrameId);

        public XRController(IModuleData moduleData, XRContents controller, XRConfiguration? configuration)
        {
            gaze = controller.Gaze;
            gazeInteractor = gaze.GetComponent<XRRayInteractor>().AssertNotNull(nameof(gazeInteractor));
            leftHand = controller.LeftHand;
            rightHand = controller.RightHand;
            leftPaddle = controller.LeftPaddle;
            rightPaddle = controller.RightPaddle;
            cameraOffset = controller.CameraOffset;
            mainCanvasHolder = controller.CanvasHolder;
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

            leftGestures.PalmClicked += _ => ToggleMainCanvas(LeftController!);

            Instance = this;

            ModuleListPanel.CallAfterInitialized(() => mainCanvasHolder.Visible = false);
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

            TfListener.Publish(LeftControllerFrameId,
                LeftController is { IsActiveInFrame: true } leftController
                    ? leftController.transform.AsPose()
                    : default);

            TfListener.Publish(RightControllerFrameId,
                RightController is { IsActiveInFrame: true } rightController
                    ? rightController.transform.AsPose()
                    : default);


            leftGestures.Process(leftHand.State);
            leftGestures.Process(leftHand.State, rightHand.State);
            rightGestures.Process(rightHand.State);
            rightGestures.Process(rightHand.State, leftHand.State);
        }

        static void UpdateHandSender(
            HandController handController,
            Sender<XRHandState> sender,
            ref uint seqNr,
            ref bool active)
        {
            if (!handController.isActiveAndEnabled)
            {
                return;
            }

            var handState = handController.State;
            if (handState != null)
            {
                active = true;
                var state = new XRHandState
                {
                    IsValid = true,
                    Header = (seqNr++, TfListener.FixedFrameId),
                    Palm = ToTransform(handState.Palm),
                    Thumb = handState.Fingers[(int)HandFinger.Thumb].Select(ToTransform).ToArray(),
                    Index = handState.Fingers[(int)HandFinger.Index].Select(ToTransform).ToArray(),
                    Middle = handState.Fingers[(int)HandFinger.Middle].Select(ToTransform).ToArray(),
                    Ring = handState.Fingers[(int)HandFinger.Ring].Select(ToTransform).ToArray(),
                    Little = handState.Fingers[(int)HandFinger.Pinky].Select(ToTransform).ToArray()
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

        static readonly Func<Pose, Msgs.GeometryMsgs.Transform> ToTransform = pose =>
            TfListener.RelativeToFixedFrame(pose).Unity2RosTransform();


        public void Dispose()
        {
            GameThread.EveryFrame -= Update;
            Instance = null;
        }

        public void ResetController()
        {
        }

        public bool Visible { get; set; }

        public void ToggleMainCanvas(CustomController controller)
        {
            if (!mainCanvasHolder.Visible)
            {
                SpawnMainCanvas(controller.transform.position);
            }
            else
            {
                DespawnMainCanvas(controller.transform);
            }
        }

        void SpawnMainCanvas(Vector3 originPosition)
        {
            if (tokenSource is { IsCancellationRequested: false })
            {
                return;
            }

            var (targetPosition, targetRotation) = mainCanvasHolder.GetTargetPose();

            mainCanvasHolder.Visible = true;
            mainCanvasHolder.Transform.localScale = Vector3.zero;
            mainCanvasHolder.transform.rotation = targetRotation;

            tokenSource = new CancellationTokenSource();
            FAnimator.Spawn(tokenSource.Token, 0.25f, t =>
                {
                    float scale = 1 - Mathf.Sqrt(1 - t);
                    mainCanvasHolder.Transform.localScale = scale * Vector3.one;
                    mainCanvasHolder.Transform.position = Vector3.Lerp(originPosition, targetPosition, t);
                },
                () =>
                {
                    tokenSource.Cancel();
                    mainCanvasHolder.FollowsCamera = true;
                });
        }

        void DespawnMainCanvas(Transform target)
        {
            if (tokenSource is { IsCancellationRequested: false })
            {
                return;
            }

            var originPosition = mainCanvasHolder.Transform.position;

            mainCanvasHolder.FollowsCamera = false;

            tokenSource = new CancellationTokenSource();
            FAnimator.Spawn(tokenSource.Token, 0.25f, t =>
                {
                    float scale = Mathf.Sqrt(t);
                    mainCanvasHolder.Transform.localScale = (1 - scale) * Vector3.one;
                    mainCanvasHolder.Transform.position = Vector3.Lerp(originPosition, target.position, t);
                },
                () =>
                {
                    mainCanvasHolder.Visible = false;
                    tokenSource.Cancel();
                });
        }
    }
}