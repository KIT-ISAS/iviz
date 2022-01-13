#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MarkerDetection;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using Iviz.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Time = UnityEngine.Time;
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
    }

    public abstract class ARController : IController, IHasFrame
    {
        public enum RootMover
        {
            Anchor,
            Executor,
            ControlMarker,
            Setup
        }

        const string HeadFrameId = "~xr/head";
        protected const string CameraFrameId = "~xr/camera";

        static Camera ARCamera => Settings.ARCamera.CheckedNull() ?? Settings.MainCamera;
        static ARJoystick ARJoystick => ModuleListPanel.Instance.ARJoystick;

        public static readonly Vector3 DefaultWorldOffset = new(0.5f, 0, -0.2f);
        public static bool IsActive => Instance != null;
        public static ARFoundationController? Instance { get; protected set; }
        public static bool IsXRVisible => Settings.IsHololens || Instance is { Visible: true };

        readonly ARConfiguration config = new();
        readonly MarkerDetector detector = new();
        readonly Dictionary<(ARMarkerType, string), float> activeMarkerHighlighters = new();

        readonly IPublishedFrame headFrame;
        protected readonly IPublishedFrame cameraFrame;

        float? joyVelocityAngle;
        Vector3? joyVelocityPos;
        uint markerSeq;

        public ARMarkerExecutor MarkerExecutor { get; } = new();
        public Sender<ARMarkerArray>? MarkerSender { get; }
        public Sender<Image>? ColorSender { get; }
        public Sender<Image>? DepthSender { get; }
        public Sender<Image>? DepthConfidenceSender { get; }
        protected Sender<CameraInfo>? ColorInfoSender { get; }
        protected Sender<CameraInfo>? DepthInfoSender { get; }

        public ARConfiguration Config
        {
            get => config;
            protected set
            {
                Visible = value.Visible;
                WorldScale = value.WorldScale;

                OcclusionQuality = value.OcclusionQuality;
                EnableMeshing = value.EnableMeshing;
                EnablePlaneDetection = value.EnablePlaneDetection;
                EnableArucoDetection = value.EnableArucoDetection;
                EnableQrDetection = value.EnableQrDetection;

                PublicationFrequency = value.PublicationFrequency;
            }
        }

        public virtual OcclusionQualityType OcclusionQuality
        {
            get => config.OcclusionQuality;
            set => config.OcclusionQuality = value;
        }

        protected Vector3 WorldPosition
        {
            get => config.WorldOffset;
            private set => config.WorldOffset = value;
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
        public virtual bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                GuiInputModule.Instance.DisableCameraLock();
                TfListener.RootFrame.Transform.SetPose(value ? WorldPose : Pose.identity);
            }
        }

        public bool EnableQrDetection
        {
            get => config.EnableQrDetection;
            set
            {
                config.EnableQrDetection = value;
                detector.EnableQr = value;
            }
        }

        public virtual bool EnableMeshing
        {
            get => config.EnableMeshing;
            set => config.EnableMeshing = value;
        }

        public virtual bool EnablePlaneDetection
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
                detector.EnableAruco = value;
            }
        }

        public PublicationFrequency PublicationFrequency
        {
            get => config.PublicationFrequency;
            set => config.PublicationFrequency = value;
        }

        static float TfRootScale
        {
            set => TfListener.RootScale = value;
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
                ARJoystick.Visible = value;

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
            ARJoystick.ChangedPosition += OnARJoystickChangedPosition;
            ARJoystick.ChangedAngle += OnARJoystickChangedAngle;
            ARJoystick.PointerUp += OnARJoystickPointerUp;
            ARJoystick.Close += ModuleListPanel.Instance.ARSidePanel.ToggleARJoystick;
            GameThread.EveryFrame += Update;

            GuiInputModule.Instance.UpdateQualityLevel();

            MarkerSender = new Sender<ARMarkerArray>("~xr/markers");
            ColorSender = new Sender<Image>("~xr/color/image_color");
            DepthSender = new Sender<Image>("~xr/depth/image");
            DepthConfidenceSender = new Sender<Image>("~xr/depth/image_confidence");
            ColorInfoSender = new Sender<CameraInfo>("~xr/color/camera_info");
            DepthInfoSender = new Sender<CameraInfo>("~xr/depth/camera_info");

            headFrame = TfPublisher.Instance.GetOrCreate(HeadFrameId, isInternal: true);
            cameraFrame = TfPublisher.Instance.GetOrCreate(CameraFrameId, isInternal: true);

            detector.MarkerDetected += OnMarkerDetected;

            pulseTokenSource?.Cancel();
        }

        protected static void RaiseARStateChanged()
        {
            ARStateChanged?.Invoke(true);
            ARCameraViewChanged?.Invoke(true);
        }

        protected static void RaiseARCameraViewChanged(bool value)
        {
            ARCameraViewChanged?.Invoke(value);
        }

        void OnARJoystickChangedAngle(float dA)
        {
            float newVelocityAngle;
            if (joyVelocityAngle is not { } velocityAngle)
            {
                newVelocityAngle = 0;
            }
            else if (Math.Sign(velocityAngle) != 0 && Math.Sign(velocityAngle) != Math.Sign(dA))
            {
                newVelocityAngle = 0;
            }
            else
            {
                newVelocityAngle = velocityAngle + 0.02f * dA;
            }

            joyVelocityAngle = newVelocityAngle;

            if (ARJoystick.IsGlobal)
            {
                SetWorldAngle(WorldAngle + newVelocityAngle, RootMover.ControlMarker);
            }
            else
            {
                var arCameraPose = ARCamera.transform.AsPose();
                Vector3 pivot = arCameraPose.Multiply(Vector3.forward);

                Quaternion rotation = Quaternion.AngleAxis(newVelocityAngle, Vector3.up);
                var pose = new Pose(rotation * (-pivot) + pivot, rotation);

                SetWorldPose(pose.Multiply(WorldPose), RootMover.ControlMarker);
            }
        }

        void OnARJoystickChangedPosition(Vector3 dPos)
        {
            Vector3 newVelocityPos;
            if (joyVelocityPos is not { } velocityPos)
            {
                newVelocityPos = Vector3.zero;
            }
            else if (Sign(velocityPos) != 0 && Sign(velocityPos) != Sign(dPos))
            {
                newVelocityPos = Vector3.zero;
            }
            else
            {
                newVelocityPos = velocityPos + 0.0005f * dPos;
            }

            joyVelocityPos = newVelocityPos;

            Vector3 deltaWorldPosition;
            if (ARJoystick.IsGlobal)
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

        void OnARJoystickPointerUp()
        {
            joyVelocityPos = null;
            joyVelocityAngle = null;
        }

        void UpdateWorldPose(in Pose pose, RootMover mover)
        {
            WorldPose = pose;
            if (Visible)
            {
                TfListener.RootFrame.Transform.SetPose(pose);
            }

            WorldPoseChanged?.Invoke(mover);
        }

        protected static float AngleFromPose(in Pose unityPose)
        {
            return UnityUtils.RegularizeAngle(unityPose.rotation.eulerAngles.y);
        }

        public void SetWorldPose(in Pose unityPose, RootMover mover)
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
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            UpdateWorldPose(new Pose(WorldPosition, rotation), mover);
        }

        /// <summary>
        /// Compensates the offset between AR View and Virtual View when AR is enabled. 
        /// </summary>
        /// <param name="unityPose">The pose to compensate</param>
        public static Pose ARPoseToUnity(in Pose unityPose)
        {
            if (Instance is null || Instance.Visible)
            {
                return unityPose;
            }

            return Instance.WorldPose.Inverse().Multiply(unityPose);
        }

        protected virtual void Update()
        {
            var absoluteArCameraPose = ARPoseToUnity(ARCamera.transform.AsPose());
            headFrame.LocalPose = TfListener.RelativeToFixedFrame(absoluteArCameraPose);
        }

        void OnMarkerDetected(Screenshot screenshot, IMarkerCorners[] markers)
        {
            ARMarker ToMarker(IMarkerCorners marker) => new()
            {
                Type = (byte)marker.Type,
                Header = new Header(markerSeq++, screenshot.Timestamp, TfListener.FixedFrameId),
                Code = marker.Code,
                CameraPose = TfListener.RelativeToFixedFrame(ARPoseToUnity(screenshot.CameraPose))
                    .Unity2RosPose()
                    .ToCameraFrame(),
                Corners = marker.Corners
                    .Select(corner => new Msgs.GeometryMsgs.Vector3(corner.X, corner.Y, 0))
                    .ToArray(),
                CameraIntrinsic = screenshot.Intrinsic.ToArray(),
            };

            var array = markers.Select(ToMarker).ToArray();
            foreach (var marker in array)
            {
                MarkerExecutor.Process(marker);
            }

            MarkerSender?.Publish(new ARMarkerArray(array));

            foreach (var marker in array)
            {
                var key = ((ARMarkerType)marker.Type, marker.Code);
                if (activeMarkerHighlighters.TryGetValue(key, out float existingExpirationTime) 
                    && Time.time < existingExpirationTime)
                {
                    continue;
                }

                ARMarkerHighlighter.Highlight(marker, 5);
                float expirationTime = Time.time + 5;
                activeMarkerHighlighters[key] = expirationTime;
            }
        }

        public static Pose GetAbsoluteMarkerPose(ARMarker marker)
        {
            var rosPoseToFixed = (Msgs.GeometryMsgs.Transform)marker.CameraPose * marker.PoseRelativeToCamera;
            var unityPoseToFixed = rosPoseToFixed.Ros2Unity();
            return TfListener.FixedFramePose.Multiply(unityPoseToFixed);
        }

        public virtual void Dispose()
        {
            ARStateChanged?.Invoke(false);
            ARCameraViewChanged?.Invoke(false);

            Visible = false;
            WorldScale = 1;

            WorldPoseChanged = null;
            ARJoystick.ChangedPosition -= OnARJoystickChangedPosition;
            ARJoystick.ChangedAngle -= OnARJoystickChangedAngle;
            ARJoystick.PointerUp -= OnARJoystickPointerUp;
            GameThread.EveryFrame -= Update;

            ShowARJoystick = false;
            Instance = null;

            GuiInputModule.Instance.UpdateQualityLevel();

            MarkerSender?.Dispose();
            detector.Dispose();
            MarkerExecutor.Dispose();

            ColorInfoSender?.Dispose();
            ColorSender?.Dispose();
            DepthSender?.Dispose();
            DepthConfidenceSender?.Dispose();
        }

        void IController.ResetController()
        {
        }

        public static void ClearResources()
        {
            ARStateChanged = null;
            ARCameraViewChanged = null;
        }


        static readonly int PulseCenter = Shader.PropertyToID("_PulseCenter");
        static readonly int PulseTime = Shader.PropertyToID("_PulseTime");
        static readonly int PulseDelta = Shader.PropertyToID("_PulseDelta");

        static CancellationTokenSource? pulseTokenSource;
        public static bool IsPulseActive => pulseTokenSource is { IsCancellationRequested: true };

        protected static void TriggerPulse(in Vector3 start)
        {
            pulseTokenSource?.Cancel();
            pulseTokenSource = new CancellationTokenSource();

            var material = Resources.Resource.Materials.LinePulse.Object;
            material.SetVector(PulseCenter, start);
            material.SetFloat(PulseDelta, 0.25f);

            FAnimator.Spawn(pulseTokenSource.Token, 10,
                static t =>
                {
                    float timeDiff = t * 10;
                    var material = Resources.Resource.Materials.LinePulse.Object;
                    material.SetFloat(PulseTime, (timeDiff - 0.5f));
                },
                static () => pulseTokenSource.Cancel()
            );
        }
    }
}