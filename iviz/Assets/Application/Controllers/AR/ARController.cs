#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Displays;
using Iviz.MarkerDetection;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.MeshMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Transform = Iviz.Msgs.GeometryMsgs.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OcclusionQualityType
    {
        Off,
        Fast,
        Medium,
        Best
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PublicationFrequency
    {
        Off,
        Fps5,
        Fps10,
        Fps15,
        Fps20,
        Fps30,
        FpsMax
    }

    public enum RootMover
    {
        Anchor,
        Executor,
        ControlMarker,
        Setup
    }
    public abstract class ARController : Controller, IHasFrame
    {
        public const bool EnableMeshingSubsystem =
            //true ||
            Settings.IsAndroid || Settings.IsWSA || Settings.IsIPhone;

        public enum RootMover
        {
            Anchor,
            Executor,
            ControlMarker,
            Setup
        }

        static Camera ARCamera => Settings.ARCamera.CheckedNull() ?? Settings.MainCamera;
        static ARJoystickPanel ARJoystickPanel => ModuleListPanel.Instance.ARJoystickPanel;

        public static Vector3 DefaultWorldOffset => new(0.5f, 0, -0.2f);
        public static bool IsActive => Instance != null;
        public static ARFoundationController? Instance { get; protected set; }
        public static bool IsXRVisible => Settings.IsHololens || Instance is { Visible: true };

        readonly ARConfiguration config = new();

        readonly IPublishedFrame headFrame;

        protected readonly IPublishedFrame cameraFrame;
        protected readonly Sender<CameraInfo> colorInfoSender;
        protected readonly PulseManager pulseManager;
        protected Sender<CameraInfo>? depthInfoSender;

        protected readonly ARMarkerManager markerManager = new();
        protected readonly MarkerDetector markerDetector = new();

        float? joyVelocityAngle;
        Vector3? joyVelocityPos;
        float? joyVelocityScale;
        uint markerSeq;
        uint meshSeq;

        public static bool IsPulseActive => Instance?.pulseManager.HasPulse is true;

        public Sender<XRMarkerArray>? MarkerSender { get; private set; }
        public Sender<Image> ColorSender { get; }
        public Sender<Image>? DepthSender { get; private set; }
        public Sender<Image>? DepthConfidenceSender { get; private set; }
        public Sender<MeshGeometryStamped>? MeshSender { get; private set; }

        public bool ProvidesOcclusion { get; protected set; }
        public bool ProvidesMesh { get; protected set; }

        public PublicationFrequency PublicationFrequency { get; set; } = PublicationFrequency.Off;

        public ARConfiguration Config
        {
            get => config;
            set
            {
                Visible = value.Visible;
                WorldScale = value.WorldScale;

                OcclusionQuality = value.OcclusionQuality;
                EnableMeshing = value.EnableMeshing;
                EnablePlaneDetection = value.EnablePlaneDetection;
                EnableArucoDetection = value.EnableArucoDetection;
                EnableQrDetection = value.EnableQrDetection;
            }
        }

        public virtual OcclusionQualityType OcclusionQuality
        {
            get => config.OcclusionQuality;
            set => config.OcclusionQuality = value;
        }

        protected Vector3 WorldPosition
        {
            get => config.WorldOffset.ToUnity();
            private set => config.WorldOffset = value.ToRos();
        }

        protected float WorldAngle
        {
            get => config.WorldAngle;
            private set => config.WorldAngle = value;
        }

        protected Pose WorldPose { get; private set; } = Pose.identity;

        public float WorldScale
        {
            get => config.WorldScale;
            set
            {
                config.WorldScale = value;
                TfRootScale = value;
            }
        }

        /// <summary>
        /// Is the AR View enabled?
        /// </summary>
        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                GuiInputModule.Instance.DisableCameraLock();
                TfModule.RootFrame.Transform.SetPose(value ? WorldPose : Pose.identity);
            }
        }

        public bool EnableQrDetection
        {
            get => config.EnableQrDetection;
            set
            {
                config.EnableQrDetection = value;
                markerDetector.EnableQr = value;
            }
        }

        public virtual bool EnableMeshing
        {
            get => config.EnableMeshing;
            set => config.EnableMeshing = value;
        }

        public bool EnablePlaneDetection
        {
            get => config.EnablePlaneDetection;
            set => config.EnablePlaneDetection = value;
        }

        public bool EnableArucoDetection
        {
            get => config.EnableArucoDetection;
            set
            {
                config.EnableArucoDetection = value;
                markerDetector.EnableAruco = value;
            }
        }

        static float TfRootScale
        {
            set => TfModule.RootScale = value;
        }

        public virtual bool PinRootMarker
        {
            get => config.PinRootMarker;
            set => config.PinRootMarker = value;
        }

        public bool ShowARJoystick
        {
            get => config.ShowARJoystick;
            set
            {
                config.ShowARJoystick = value;
                ARJoystickPanel.Visible = value;

                if (value)
                {
                    ModuleListPanel.Instance.AllGuiVisible = false;
                }
            }
        }

        public TfFrame Frame => headFrame.TfFrame;

        /// <summary>
        /// AR has been enabled / disabled
        /// </summary>
        public static event Action<bool>? ARStateChanged;

        /// <summary>
        /// AR camera view has been enabled / disabled
        /// </summary>
        public static event Action<bool>? ARCameraViewChanged;

        public event Action<RootMover>? WorldPoseChanged;

        protected ARController()
        {
            ARJoystickPanel.ChangedPosition += OnARJoystickChangedPosition;
            ARJoystickPanel.ChangedAngle += OnARJoystickChangedAngle;
            ARJoystickPanel.ChangedScale += OnARJoystickChangedScale;
            ARJoystickPanel.PointerUp += OnARJoystickPointerUp;
            ARJoystickPanel.ResetScale += OnARJoystickResetScale;
            ARJoystickPanel.Close += ModuleListPanel.Instance.ARToolbarPanel.ToggleARJoystick;
            GameThread.EveryFrame += Update;

            Settings.SettingsManager.UpdateQualityLevel();

            ColorSender = new Sender<Image>(XRNames.ColorTopic);
            colorInfoSender = new Sender<CameraInfo>(XRNames.CameraInfoTopic);

            headFrame = TfPublisher.Instance.GetOrCreate(XRNames.HeadFrameId, isInternal: true);
            cameraFrame = TfPublisher.Instance.GetOrCreate(XRNames.CameraFrameId, isInternal: true);

            markerDetector.MarkerDetected += OnMarkerDetected;
            pulseManager = new PulseManager();
        }

        protected void PostInitialize()
        {
            if (ProvidesOcclusion)
            {
                DepthSender = new Sender<Image>(XRNames.DepthImageTopic);
                DepthConfidenceSender = new Sender<Image>(XRNames.DepthConfidenceTopic);
                depthInfoSender = new Sender<CameraInfo>(XRNames.DepthCameraInfoTopic);
            }

            if (ProvidesMesh)
            {
                MeshSender = new Sender<MeshGeometryStamped>(XRNames.MeshesTopic);
            }

            if (MarkerDetector.IsEnabled)
            {
                MarkerSender = new Sender<XRMarkerArray>(XRNames.MarkersTopic);
            }
        }

        protected static void RaiseARStateChanged()
        {
            try
            {
                ARStateChanged?.Invoke(true);
                ARCameraViewChanged?.Invoke(true);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{nameof(ARController)}: " +
                                $"Error during {nameof(RaiseARStateChanged)}", e);
            }
        }

        protected static void RaiseARCameraViewChanged(bool value)
        {
            try
            {
                ARCameraViewChanged?.Invoke(value);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{nameof(ARController)}: " +
                                $"Error during {nameof(RaiseARCameraViewChanged)}", e);
            }
        }

        void OnARJoystickChangedAngle(float dA)
        {
            float newVelocityAngle;
            if (joyVelocityAngle is { } velocityAngle &&
                (velocityAngle == 0 || Math.Sign(velocityAngle) == Math.Sign(dA)))
            {
                newVelocityAngle = velocityAngle + 0.02f * dA;
            }
            else
            {
                newVelocityAngle = 0;
            }

            joyVelocityAngle = newVelocityAngle;

            if (ARJoystickPanel.IsGlobal)
            {
                SetWorldAngle(WorldAngle + newVelocityAngle, RootMover.ControlMarker);
            }
            else
            {
                var arCameraPose = ARCamera.transform.AsPose();
                var pivot = arCameraPose.Multiply(Vector3.forward);

                var rotation = Quaternion.AngleAxis(newVelocityAngle, Vector3.up);
                var pose = new Pose(rotation * (-pivot) + pivot, rotation);

                SetWorldPose(pose.Multiply(WorldPose), RootMover.ControlMarker);
            }
        }

        void OnARJoystickChangedPosition(Vector3 dPos)
        {
            Vector3 newVelocityPos;
            if (joyVelocityPos is { } velocityPos
                && (Sign(velocityPos) == 0 || Sign(velocityPos) == Sign(dPos)))
            {
                newVelocityPos = velocityPos + 0.0005f * dPos;
            }
            else
            {
                newVelocityPos = Vector3.zero;
            }

            joyVelocityPos = newVelocityPos;

            Vector3 deltaWorldPosition;
            if (ARJoystickPanel.IsGlobal)
            {
                deltaWorldPosition = WorldPose.rotation * newVelocityPos.Ros2Unity();
            }
            else
            {
                var arCameraPose = ARPoseToUnity(ARCamera.transform.AsPose());
                float rotY = arCameraPose.rotation.eulerAngles.y;
                Quaternion cameraRotation = Quaternion.Euler(0, rotY, 0);
                (float joyX, float joyY, float joyZ) = newVelocityPos;
                deltaWorldPosition = cameraRotation * new Vector3(joyX, joyZ, joyY);
            }

            SetWorldPosition(WorldPosition + deltaWorldPosition, RootMover.ControlMarker);

            static int Sign(in Vector3 v) =>
                Math.Sign(v.x) + Math.Sign(v.y) + Math.Sign(v.z); // only one of the components is nonzero
        }

        void OnARJoystickChangedScale(float dA)
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

        void OnARJoystickPointerUp()
        {
            joyVelocityPos = null;
            joyVelocityAngle = null;
            joyVelocityScale = null;
        }

        void OnARJoystickResetScale()
        {
            joyVelocityScale = null;
            WorldScale = 1;
        }

        void UpdateWorldPose(in Pose pose, RootMover mover)
        {
            WorldPose = pose;
            if (Visible)
            {
                TfModule.RootFrame.Transform.SetPose(pose);
            }

            WorldPoseChanged?.Invoke(mover);
        }

        protected static float AngleFromPose(in Pose unityPose)
        {
            return UnityUtils.RegularizeAngle(unityPose.rotation.eulerAngles.y);
        }

        protected void SetWorldPose(in Pose unityPose, RootMover mover)
        {
            float angle = AngleFromPose(unityPose);
            WorldPosition = unityPose.position;
            WorldAngle = angle;
            UpdateWorldPose(unityPose, mover);
        }

        void SetWorldPosition(in Vector3 unityPosition, RootMover mover)
        {
            WorldPosition = unityPosition;
            UpdateWorldPose(WorldPose.WithPosition(unityPosition), mover);
        }

        void SetWorldAngle(float angle, RootMover mover)
        {
            WorldAngle = angle;
            var rotation = Quaternion.AngleAxis(angle, Vector3.up);
            UpdateWorldPose(WorldPose.WithRotation(rotation), mover);
        }

        /// <summary>
        /// Compensates the offset between AR View and Virtual View when AR is enabled. 
        /// </summary>
        public static Pose ARPoseToUnity(in Pose unityPose)
        {
            // if AR is visible, or completely disabled, then we do nothing
            if (Instance is not { } instance || instance.Visible)
            {
                return unityPose;
            }

            // but when AR is enabled and we are in virtual view, we compensate for the fact that the camera
            // is displaced depending on where we put the AR origin during setup.
            return instance.WorldPose.InverseMultiply(unityPose);
        }

        protected virtual void Update()
        {
            var absoluteArCameraPose = ARPoseToUnity(ARCamera.transform.AsPose());
            headFrame.LocalPose = TfModule.RelativeToFixedFrame(absoluteArCameraPose).ToCameraFrame();
        }

        static bool arMarkerWarningShown;

        void OnMarkerDetected(Screenshot screenshot, IReadOnlyList<DetectedMarker> markers)
        {
            if (!(TfModule.RootScale - 1).ApproximatelyZero())
            {
                if (arMarkerWarningShown) return;
                arMarkerWarningShown = true;
                RosLogger.Warn($"{this}: Marker detection disabled if scale is not 1");
                return;
            }

            XRMarker ToMarker(DetectedMarker marker) => new()
            {
                Type = (byte)marker.Type,
                Header = new Header(markerSeq++, screenshot.Timestamp, TfModule.FixedFrameId),
                Code = marker.Code,
                CameraPose = TfModule.RelativeToFixedFrame(ARPoseToUnity(screenshot.CameraPose))
                    .Unity2RosPose()
                    .ToCameraFrame(),
                Corners = marker.Corners
                    .Select(corner => ((Vector3)corner).ToRos())
                    .ToArray(),
                CameraIntrinsic = screenshot.Intrinsic.ToArray(),
            };

            var array = markers.Select(ToMarker).ToArray();
            foreach (var marker in array)
            {
                markerManager.Process(marker);
                markerManager.Highlight(marker);
            }

            MarkerSender?.Publish(new XRMarkerArray(array));
        }

        public static Pose GetAbsoluteMarkerPose(XRMarker marker)
        {
            var rosPoseToFixed = (Transform)marker.CameraPose * marker.PoseRelativeToCamera;
            var unityPoseToFixed = rosPoseToFixed.Ros2Unity();
            return TfModule.FixedFrameToAbsolute(unityPoseToFixed);
        }
        
        public virtual void Dispose()
        {
            ARStateChanged?.Invoke(false);
            ARCameraViewChanged?.Invoke(false);

            Visible = false;
            WorldScale = 1;

            WorldPoseChanged = null;
            ARJoystickPanel.ChangedPosition -= OnARJoystickChangedPosition;
            ARJoystickPanel.ChangedAngle -= OnARJoystickChangedAngle;
            ARJoystickPanel.PointerUp -= OnARJoystickPointerUp;
            GameThread.EveryFrame -= Update;

            ShowARJoystick = false;
            Instance = null;

            Settings.SettingsManager.UpdateQualityLevel();

            MarkerSender?.Dispose();
            markerDetector.Dispose();

            colorInfoSender.Dispose();
            ColorSender.Dispose();
            DepthSender?.Dispose();
            DepthConfidenceSender?.Dispose();
            MeshSender?.Dispose();

            pulseManager.Dispose();

            TfPublisher.Instance.Remove(XRNames.HeadFrameId, true);
            TfPublisher.Instance.Remove(XRNames.CameraFrameId, true);
        }

        public override void ResetController()
        {
        }

        public static void ClearResources()
        {
            Instance = null;
            ARStateChanged = null;
            ARCameraViewChanged = null;
        }

        protected sealed class PulseManager
        {
            CancellationTokenSource? pulseTokenSource;
            public bool HasPulse => pulseTokenSource is { IsCancellationRequested: false };

            public void TriggerPulse(in Vector3 start)
            {
                pulseTokenSource?.Cancel();
                pulseTokenSource = new CancellationTokenSource();

                var material = Resource.Materials.LinePulse.Object;
                material.SetVector(ShaderIds.PulseCenterId, start);
                material.SetFloat(ShaderIds.PulseDeltaId, 0.25f);

                FAnimator.Spawn(pulseTokenSource.Token, 10,
                    static t =>
                    {
                        float timeDiff = t * 10;
                        var material = Resource.Materials.LinePulse.Object;
                        material.SetFloat(ShaderIds.PulseTimeId, (timeDiff - 0.5f));
                    },
                    () => pulseTokenSource.Cancel()
                );
            }

            public void Dispose()
            {
                pulseTokenSource?.Cancel();
            }
        }
    }
}