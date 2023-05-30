#nullable enable

using System;
using System.Threading;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays.XR;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.MeshMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
//using Microsoft.MixedReality.WorldLocking.Core;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers.XR
{
    public sealed class XRController : Controller, IHasFrame
    {
        public Sender<XRGazeState> GazeSender { get; } = new(XRNames.GazeTopic);
        public Sender<XRHandState> LeftHandSender { get; } = new(XRNames.LeftHandTopic);
        public Sender<XRHandState> RightHandSender { get; } = new(XRNames.RightHandTopic);

        public Sender<MeshGeometryStamped>? MeshSender { get; } =
            ARController.EnableMeshingSubsystem && Settings.IsHololens 
                ? new(XRNames.MeshesTopic) 
                : null;

        public const float NearDistance = 0.05f;

        readonly GazeController gaze;
        readonly XRRayInteractor gazeInteractor;
        readonly HandController leftHand;
        readonly HandController rightHand;
        readonly PaddleController leftPaddle;
        readonly PaddleController rightPaddle;
        readonly Transform cameraOffset;
        readonly CanvasHolder mainCanvasHolder;
        readonly GameObject manipulableFrame;
        readonly ARAnchorManager anchorManager;
        readonly RadialButtonBar frameButtonBar;

        readonly PalmCompass? palmCompass;

        readonly HandGestures leftGestures = new(HandType.Left);
        //readonly HandGestures rightGestures = new(HandType.Right);

        readonly IPublishedFrame headFrame;
        readonly IPublishedFrame leftControllerFrame;
        readonly IPublishedFrame rightControllerFrame;
        readonly IPublishedFrame? gazeFrame;

        readonly XRConfiguration config = new();
        readonly bool handCompassVisible;

        ARAnchor? originAnchor;


        bool gazeActive;
        bool leftHandActive;
        bool rightHandActive;

        uint gazeSeqNr;
        uint leftHandSeqNr;
        uint rightHandSeqNr;

        float? joyVelocityAngle;
        Vector3? joyVelocityPos;
        float? joyVelocityScale;

        CancellationTokenSource? canvasTokenSource;

        public ARMeshManager MeshManager { get; }

        CustomController? LeftController =>
            leftHand.isActiveAndEnabled ? leftHand :
            leftPaddle.isActiveAndEnabled ? leftPaddle : null;

        CustomController? RightController =>
            rightHand.isActiveAndEnabled ? rightHand :
            rightPaddle.isActiveAndEnabled ? rightPaddle : null;

        public static XRController? Instance { get; private set; }

        public XRConfiguration Config
        {
            get => config;
            set => WorldScale = value.WorldScale;
        }

        public TfFrame Frame => headFrame.TfFrame;

        public float WorldScale
        {
            get => config.WorldScale;
            set
            {
                config.WorldScale = value;
                TfModule.RootScale = value;
            }
        }

        public XRController(XRContents contents, XRConfiguration? configuration)
        {
            gaze = contents.Gaze;
            gazeInteractor = gaze.AssertHasComponent<XRRayInteractor>(nameof(gazeInteractor));
            leftHand = contents.LeftHand;
            rightHand = contents.RightHand;
            leftPaddle = contents.LeftPaddle;
            rightPaddle = contents.RightPaddle;
            cameraOffset = contents.CameraOffset;
            mainCanvasHolder = contents.CanvasHolder;
            handCompassVisible = contents.CompassVisible;

            Config = configuration ?? new XRConfiguration();

            //leftPaddle.gameObject.SetActive(!Settings.IsHololens);
            //rightPaddle.gameObject.SetActive(!Settings.IsHololens);
            //leftHand.gameObject.SetActive(Settings.IsHololens);
            //rightHand.gameObject.SetActive(Settings.IsHololens);

            var tfPublisher = TfPublisher.Instance;
            headFrame = tfPublisher.GetOrCreate(XRNames.HeadFrameId, isInternal: true);
            leftControllerFrame = tfPublisher.GetOrCreate(XRNames.LeftControllerFrameId, isInternal: true);
            rightControllerFrame = tfPublisher.GetOrCreate(XRNames.RightControllerFrameId, isInternal: true);

            if (Settings.IsHololens)
            {
                gazeFrame = tfPublisher.GetOrCreate(XRNames.GazeFrameId, isInternal: true);
            }

            GameThread.EveryFrame += Update;

            leftGestures.PalmClicked += () => ToggleMainCanvas(LeftController!);
            leftGestures.GestureChanged += ManageGestureLeft;

            leftPaddle.SecondaryClicked += () => ToggleMainCanvas(leftPaddle);
            rightPaddle.SecondaryClicked += () => ToggleMainCanvas(rightPaddle);

            anchorManager = contents.AnchorManager;

            manipulableFrame = contents.ManipulableFrame;
            frameButtonBar = contents.FrameButtonBar;

            if (Settings.IsHololens)
            {
                contents.HololensRig.SetActive(true);
                manipulableFrame.SetActive(true);
                frameButtonBar.Visible = true;
                frameButtonBar.Clicked += OnFrameButtonBarClicked;

                if (ModuleListPanel.Instance.TryLoadXRConfiguration(out var anchorPose))
                {
                    manipulableFrame.transform.SetPose(anchorPose);
                }

                palmCompass = contents.PalmCompass;
                //palmCompass.Visible = true;
                //palmCompass.Transform.SetParentLocal(leftHand.PalmTransform);

                var joystickPanel = contents.JoystickPanel;
                var holder = contents.JoystickHolder;
                joystickPanel.ChangedPosition += OnXRJoystickChangedPosition;
                joystickPanel.ChangedAngle += OnXRJoystickChangedAngle;
                joystickPanel.ChangedScale += OnXRJoystickChangedScale;
                joystickPanel.PointerUp += OnXRJoystickPointerUp;
                //joystickPanel.ResetScale += OnARJoystickResetScale;
                joystickPanel.Close += ToggleJoystickPanel;

                palmCompass.ButtonBar.Clicked += i =>
                {
                    switch (i)
                    {
                        case 0:
                            ToggleJoystickPanel();
                            break;
                    }
                };

                void ToggleJoystickPanel()
                {
                    joystickPanel.Visible = !joystickPanel.Visible;
                    if (joystickPanel.Visible)
                    {
                        holder.InitializePose();
                    }
                }
            }

            MeshManager = contents.MeshManager;
            Instance = this;

            ModuleListPanel.CallAfterInitialized(() => mainCanvasHolder.Visible = false);
        }


        void OnXRJoystickChangedPosition(Vector3 dPos)
        {
            Vector3 newVelocityPos;
            if (joyVelocityPos is { } velocityPos
                && (Sign(velocityPos) == 0 || Sign(velocityPos) == Sign(dPos)))
            {
                newVelocityPos = velocityPos + 5e-6f * dPos;
            }
            else
            {
                newVelocityPos = Vector3.zero;
            }

            joyVelocityPos = newVelocityPos;

            Vector3 deltaWorldPosition = WorldPose.rotation * newVelocityPos.Ros2Unity();
            SetWorldPosition(WorldPose.position + deltaWorldPosition);

            static int Sign(in Vector3 v) =>
                Math.Sign(v.x) + Math.Sign(v.y) + Math.Sign(v.z); // only one of the components is nonzero
        }

        void OnXRJoystickChangedAngle(float dA)
        {
            float newVelocityAngle;
            if (joyVelocityAngle is { } velocityAngle &&
                (velocityAngle == 0 || Math.Sign(velocityAngle) == Math.Sign(dA)))
            {
                newVelocityAngle = velocityAngle + 0.001f * dA;
            }
            else
            {
                newVelocityAngle = 0;
            }

            joyVelocityAngle = newVelocityAngle;

            float worldAngle = AngleFromPose(WorldPose);
            SetWorldAngle(worldAngle + newVelocityAngle);
        }

        void OnXRJoystickChangedScale(float dA)
        {
            float newVelocityScale;
            if (joyVelocityScale is { } velocityScale &&
                (velocityScale == 0 || Math.Sign(velocityScale) == Math.Sign(dA)))
            {
                newVelocityScale = velocityScale + 1e-4f * dA;
            }
            else
            {
                newVelocityScale = 0;
            }

            joyVelocityScale = newVelocityScale;

            WorldScale *= Mathf.Exp(newVelocityScale);
        }

        void OnXRJoystickPointerUp()
        {
            joyVelocityPos = null;
            joyVelocityAngle = null;
            joyVelocityScale = null;
        }

        static Pose WorldPose
        {
            get => TfModule.RootFrame.Transform.AsPose();
            set => TfModule.RootFrame.Transform.SetPose(value);
        }

        static void SetWorldPosition(in Vector3 unityPosition)
        {
            WorldPose = WorldPose.WithPosition(unityPosition);
        }

        static void SetWorldAngle(float angle)
        {
            var rotation = Quaternion.AngleAxis(angle, Vector3.up);
            WorldPose = WorldPose.WithRotation(rotation);
        }

        static float AngleFromPose(in Pose unityPose)
        {
            return UnityUtils.RegularizeAngle(unityPose.rotation.eulerAngles.y);
        }

        void OnFrameButtonBarClicked(int index)
        {
            const int startButton = 0;
            const int downButton = 1;

            switch (index)
            {
                case startButton:
                    var newOriginAbsolute = manipulableFrame.transform.AsPose();
                    TfModule.RootFrame.Transform.SetPose(newOriginAbsolute);
                    manipulableFrame.SetActive(false);
                    frameButtonBar.Visible = false;

                    /*
                    WorldLockingManager.GetInstance().AttachmentPointManager.CreateAttachmentPoint(
                        newOriginAbsolute.position, null,
                        adjustment =>
                        {
                            var newPose = adjustment.Multiply(TfModule.RootFrame.Transform.AsPose());
                            TfModule.RootFrame.Transform.SetPose(newPose);
                            manipulableFrame.transform.SetPose(newPose);
                            _ = ModuleListPanel.Instance.SaveXRConfigurationAsync(newPose);
                        },
                        _ => { }
                    );
                    */

                    _ = ModuleListPanel.Instance.SaveXRConfigurationAsync(newOriginAbsolute);
                    break;
                case downButton:
                    var clickInfo =
                        new ClickHitInfo(new Ray(manipulableFrame.transform.position, Vector3.down));
                    if (clickInfo.TryGetRaycastResults(out var hits)
                        && hits.TryGetFirst(hit => hit.GameObject.layer == LayerType.Collider, out var colliderHit))
                    {
                        manipulableFrame.transform.position = colliderHit.Position;
                    }

                    break;
            }
        }

        void ManageGestureLeft()
        {
            //const float animationDurationInSec = 0.1f;

            if (!handCompassVisible)
            {
                return;
            }

            if (palmCompass == null)
            {
                Debug.LogError($"{ToString()}: Palm compass is visible but not set!");
                return;
            }

            bool isPalmUp = leftGestures.IsPalmUp;
            bool isPalmCompassVisible = palmCompass.Visible;

            if (isPalmUp)
            {
                if (isPalmCompassVisible || leftHand.PalmTransform is not { } palmTransform)
                {
                    return;
                }

                palmCompass.Transform.SetParentLocal(palmTransform);
                //var newPalmCompass = ResourcePool.RentDisplay<PalmCompass>(palmTransform);
                float baseScale = 0.125f / palmTransform.localScale.x;
                //palmCompass.Transform.localScale = Vector3.zero;
                palmCompass.Transform.localScale = baseScale * Vector3.one;
                palmCompass.Transform.localRotation = Quaternions.Rotate180AroundX;
                palmCompass.Visible = true;

                //FAnimator.Spawn(default, animationDurationInSec,
                //    t => newPalmCompass.Transform.localScale = Mathf.Sqrt(t) * baseScale * Vector3.one);
            }
            else if (!isPalmUp)
            {
                if (!isPalmCompassVisible)
                {
                    return;
                }

                //var oldPalmCompass = palmCompass;
                //float baseScale = 0.125f / oldPalmCompass.transform.parent.localScale.x;
                //palmCompass = null;
                palmCompass.Visible = false;

                //FAnimator.Spawn(default, animationDurationInSec,
                //    t => oldPalmCompass.Transform.localScale = baseScale * Mathf.Sqrt(1 - t) * Vector3.one,
                //    oldPalmCompass.ReturnToPool);
            }
        }

        void Update()
        {
            UpdateGazeSender();
            UpdateHandSender(leftHand, LeftHandSender, ref leftHandSeqNr, ref leftHandActive);
            UpdateHandSender(rightHand, RightHandSender, ref rightHandSeqNr, ref rightHandActive);

            headFrame.LocalPose = TfModule.RelativeToFixedFrame(Settings.MainCameraTransform);

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
                    gazeFrame.LocalPose = TfModule.RelativeToFixedFrame(poseToPublish);
                }
                else
                {
                    gazeFrame.LocalPose = Pose.identity;
                }
            }

            leftControllerFrame.LocalPose = LeftController is { IsActiveInFrame: true } leftController
                ? TfModule.RelativeToFixedFrame(leftController.transform.AsPose())
                : Pose.identity;

            rightControllerFrame.LocalPose = RightController is { IsActiveInFrame: true } rightController
                ? TfModule.RelativeToFixedFrame(rightController.transform.AsPose())
                : Pose.identity;

            leftGestures.Process(leftHand.State, rightHand.State);
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
            TfModule.RelativeToFixedFrame(pose).Unity2RosTransform();

        public void Dispose()
        {
            GameThread.EveryFrame -= Update;
            Instance = null;
        }

        public override void ResetController()
        {
        }

        public override bool Visible { get; set; }

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

            canvasTokenSource?.Cancel();
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