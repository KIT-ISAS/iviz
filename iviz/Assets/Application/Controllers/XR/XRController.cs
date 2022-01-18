#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XRDialogs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using Microsoft.MixedReality.WorldLocking.Core;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;
using Mesh = UnityEngine.Mesh;

namespace Iviz.Controllers.XR
{
    public sealed class XRController : IController, IHasFrame
    {
        const string HeadFrameId = "~xr/head";
        const string LeftControllerFrameId = "~xr/left_controller";
        const string RightControllerFrameId = "~xr/right_controller";
        const string GazeFrameId = "~xr/gaze";

        public Sender<XRGazeState> GazeSender { get; } = new("~xr/gaze");
        public Sender<XRHandState> LeftHandSender { get; } = new("~xr/left_hand");
        public Sender<XRHandState> RightHandSender { get; } = new("~xr/right_hand");

        public const float NearDistance = 0.2f;
        
        readonly GazeController gaze;
        readonly XRRayInteractor gazeInteractor;
        readonly HandController leftHand;
        readonly HandController rightHand;
        readonly PaddleController leftPaddle;
        readonly PaddleController rightPaddle;
        readonly Transform cameraOffset;
        readonly CanvasHolder mainCanvasHolder;
        readonly GameObject hololensInitManipulableFrame;
        readonly ARAnchorManager anchorManager;
        readonly ButtonBar hololensInitButtonBar;

        readonly HandGestures leftGestures = new(HandType.Left);
        readonly HandGestures rightGestures = new(HandType.Right);

        readonly IPublishedFrame headFrame;
        readonly IPublishedFrame leftControllerFrame;
        readonly IPublishedFrame rightControllerFrame;
        readonly IPublishedFrame? gazeFrame;

        ARAnchor? originAnchor;

        public ARMeshManager MeshManager { get; }

        CustomController? LeftController =>
            leftHand.isActiveAndEnabled ? leftHand :
            leftPaddle.isActiveAndEnabled ? leftPaddle : null;

        CustomController? RightController =>
            rightHand.isActiveAndEnabled ? rightHand :
            rightPaddle.isActiveAndEnabled ? rightPaddle : null;

        PalmCompass? palmCompass;

        bool gazeActive;
        bool leftHandActive;
        bool rightHandActive;

        uint gazeSeqNr;
        uint leftHandSeqNr;
        uint rightHandSeqNr;

        CancellationTokenSource? canvasTokenSource;

        public static XRController? Instance { get; private set; }

        public XRConfiguration Config { get; }

        public TfFrame Frame => headFrame.TfFrame;


        public XRController(XRContents contents, XRConfiguration? configuration)
        {
            gaze = contents.Gaze;
            gazeInteractor = gaze.GetComponent<XRRayInteractor>().AssertNotNull(nameof(gazeInteractor));
            leftHand = contents.LeftHand;
            rightHand = contents.RightHand;
            leftPaddle = contents.LeftPaddle;
            rightPaddle = contents.RightPaddle;
            cameraOffset = contents.CameraOffset;
            mainCanvasHolder = contents.CanvasHolder;

            Config = configuration ?? new XRConfiguration();

            leftPaddle.gameObject.SetActive(!Settings.IsHololens);
            rightPaddle.gameObject.SetActive(!Settings.IsHololens);
            leftHand.gameObject.SetActive(Settings.IsHololens);
            rightHand.gameObject.SetActive(Settings.IsHololens);

            headFrame = TfPublisher.Instance.GetOrCreate(HeadFrameId, isInternal: true);
            leftControllerFrame = TfPublisher.Instance.GetOrCreate(LeftControllerFrameId, isInternal: true);
            rightControllerFrame = TfPublisher.Instance.GetOrCreate(RightControllerFrameId, isInternal: true);

            if (Settings.IsHololens)
            {
                gazeFrame = TfPublisher.Instance.GetOrCreate(HeadFrameId, isInternal: true);
            }

            GameThread.EveryFrame += Update;

            leftGestures.PalmClicked += () => ToggleMainCanvas(LeftController!);
            leftGestures.GestureChanged += ManageGestureLeft;

            anchorManager = contents.AnchorManager;

            hololensInitManipulableFrame = contents.ManipulableFrame;
            hololensInitButtonBar = contents.FrameButtonBar;

            if (Settings.IsHololens)
            {
                contents.HololensRig.SetActive(true);
                hololensInitManipulableFrame.SetActive(true);
                hololensInitButtonBar.Visible = true;
                hololensInitButtonBar.Clicked += OnFrameHololensInitButtonBarClicked;

                if (ModuleListPanel.Instance.TryLoadXRConfiguration(out var anchorPose))
                {
                    hololensInitManipulableFrame.transform.SetPose(anchorPose);
                }
            }

            MeshManager = contents.MeshManager;
            Instance = this;

            ModuleListPanel.CallAfterInitialized(() => mainCanvasHolder.Visible = false);
        }

        void OnFrameHololensInitButtonBarClicked(int index)
        {
            const int startButton = 0;
            const int downButton = 1;

            switch (index)
            {
                case startButton:
                    var newOriginAbsolute = hololensInitManipulableFrame.transform.AsPose();
                    TfListener.RootFrame.Transform.SetPose(newOriginAbsolute);
                    hololensInitManipulableFrame.SetActive(false);
                    hololensInitButtonBar.Visible = false;

                    WorldLockingManager.GetInstance().AttachmentPointManager.CreateAttachmentPoint(
                        newOriginAbsolute.position, null,
                        adjustment =>
                        {
                            var newPose = adjustment.Multiply(TfListener.RootFrame.Transform.AsPose());
                            TfListener.RootFrame.Transform.SetPose(newPose);
                            hololensInitManipulableFrame.transform.SetPose(newPose);
                            ModuleListPanel.Instance.SaveXRConfiguration(newPose);
                        },
                        _ => { }
                    );

                    ModuleListPanel.Instance.SaveXRConfiguration(newOriginAbsolute);
                    break;
                case downButton:
                    var clickInfo =
                        new ClickHitInfo(new Ray(hololensInitManipulableFrame.transform.position, Vector3.down));
                    if (clickInfo.TryGetRaycastResults(out var hits))
                    {
                        var colliderHit = hits.FirstOrDefault(hit => hit.GameObject.layer == LayerType.Collider);
                        if (colliderHit != null)
                        {
                            hololensInitManipulableFrame.transform.position = colliderHit.Position;
                        }
                    }

                    break;
            }
        }

        void ManageGestureLeft()
        {
            if (leftGestures.IsPalmUp && palmCompass == null)
            {
                if (leftHand.PalmTransform is not { } palmTransform)
                {
                    return;
                }

                var newPalmCompass = ResourcePool.RentDisplay<PalmCompass>(palmTransform);
                float baseScale = 0.125f / palmTransform.localScale.x;
                newPalmCompass.Transform.localScale = Vector3.zero;
                newPalmCompass.Transform.localRotation = new Quaternion(1, 0, 0, 0);
                palmCompass = newPalmCompass;
                FAnimator.Spawn(default, 0.1f,
                    t => newPalmCompass.Transform.localScale = Mathf.Sqrt(t) * baseScale * Vector3.one);
            }
            else if (!leftGestures.IsPalmUp && palmCompass != null)
            {
                var oldPalmCompass = palmCompass;
                float baseScale = 0.125f / oldPalmCompass.transform.parent.localScale.x;
                palmCompass = null;

                FAnimator.Spawn(default, 0.1f,
                    t => oldPalmCompass.Transform.localScale = baseScale * Mathf.Sqrt(1 - t) * Vector3.one,
                    () => oldPalmCompass.ReturnToPool());
            }
        }

        void Update()
        {
            UpdateGazeSender();
            UpdateHandSender(leftHand, LeftHandSender, ref leftHandSeqNr, ref leftHandActive);
            UpdateHandSender(rightHand, RightHandSender, ref rightHandSeqNr, ref rightHandActive);

            headFrame.LocalPose = TfListener.RelativeToFixedFrame(Settings.MainCameraTransform.AsPose());

            if (gaze.isActiveAndEnabled && gazeFrame != null)
            {
                if (gaze.IsActiveInFrame)
                {
                    var gazeTransform = gaze.transform;
                    var gazeForward = gazeTransform.forward;
                    var gazePosition = gazeTransform.position;
                    var gazeRotation = gazeTransform.rotation;

                    var positionToPublish = gazeInteractor.TryGetHitInfo(out var hitPosition, out _, out _, out _)
                        ? hitPosition
                        : gazePosition + 5 * gazeForward;
                    var poseToPublish = new Pose(positionToPublish, gazeRotation);
                    gazeFrame.LocalPose = TfListener.RelativeToFixedFrame(poseToPublish);
                }
                else
                {
                    gazeFrame.LocalPose = Pose.identity;
                }
            }

            leftControllerFrame.LocalPose = LeftController is { IsActiveInFrame: true } leftController
                ? leftController.transform.AsPose()
                : Pose.identity;

            rightControllerFrame.LocalPose = RightController is { IsActiveInFrame: true } rightController
                ? rightController.transform.AsPose()
                : Pose.identity;

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
                    Header = TfListener.CreateHeader(seqNr++),
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
                var state = new XRHandState
                {
                    IsValid = false,
                    Header = TfListener.CreateHeader(seqNr++),
                };
                sender.Publish(state);
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
                    Header = TfListener.CreateHeader(gazeSeqNr++),
                    Transform = ToTransform(gazePose)
                };
                GazeSender.Publish(state);
                return;
            }

            if (gazeActive)
            {
                var state = new XRGazeState
                {
                    IsValid = false,
                    Header = TfListener.CreateHeader(gazeSeqNr++),
                };
                GazeSender.Publish(state);
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
            if (canvasTokenSource is { IsCancellationRequested: false })
            {
                return;
            }

            var (targetPosition, targetRotation) = mainCanvasHolder.GetTargetPose();

            mainCanvasHolder.Visible = true;
            mainCanvasHolder.Transform.localScale = Vector3.zero;
            mainCanvasHolder.transform.rotation = targetRotation;

            canvasTokenSource = new CancellationTokenSource();
            FAnimator.Spawn(canvasTokenSource.Token, 0.25f, t =>
                {
                    float scale = 1 - Mathf.Sqrt(1 - t);
                    mainCanvasHolder.Transform.localScale = scale * Vector3.one;
                    mainCanvasHolder.Transform.position = Vector3.Lerp(originPosition, targetPosition, t);
                },
                () =>
                {
                    canvasTokenSource.Cancel();
                    mainCanvasHolder.FollowsCamera = true;
                });
        }

        void DespawnMainCanvas(Transform target)
        {
            if (canvasTokenSource is { IsCancellationRequested: false })
            {
                return;
            }

            var originPosition = mainCanvasHolder.Transform.position;

            mainCanvasHolder.FollowsCamera = false;

            canvasTokenSource = new CancellationTokenSource();
            FAnimator.Spawn(canvasTokenSource.Token, 0.25f, t =>
                {
                    float scale = Mathf.Sqrt(t);
                    mainCanvasHolder.Transform.localScale = (1 - scale) * Vector3.one;
                    mainCanvasHolder.Transform.position = Vector3.Lerp(originPosition, target.position, t);
                },
                () =>
                {
                    mainCanvasHolder.Visible = false;
                    canvasTokenSource.Cancel();
                });
        }
    }
}