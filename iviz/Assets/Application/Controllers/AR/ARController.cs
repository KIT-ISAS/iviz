using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.App;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MarkerDetection;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib.Utils;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
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

    [DataContract]
    public sealed class ARConfiguration : JsonToString, IConfiguration
    {
        [IgnoreDataMember] public float WorldScale { get; set; } = 1.0f;
        [IgnoreDataMember] public SerializableVector3 WorldOffset { get; set; } = ARController.DefaultWorldOffset;

        [DataMember] public bool EnableQrDetection { get; set; }
        [DataMember] public bool EnableArucoDetection { get; set; }
        [DataMember] public SerializableVector3 MarkerOffset { get; set; } = Vector3.zero;
        [DataMember] public OcclusionQualityType OcclusionQuality { get; set; }

        [IgnoreDataMember] public float WorldAngle { get; set; }
        [IgnoreDataMember] public bool ShowARJoystick { get; set; }
        [IgnoreDataMember] public bool PinRootMarker { get; set; }

        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.AugmentedReality;
        [DataMember] public bool Visible { get; set; } = true;
    }

    public abstract class ARController : MonoBehaviour, IController, IHasFrame
    {
        public enum RootMover
        {
            Anchor,
            Executor,
            ControlMarker,
            Setup
        }

        public static readonly Vector3 DefaultWorldOffset = new Vector3(0.5f, 0, -0.2f);

        //static ARSessionInfo savedSessionInfo;
        static AnchorToggleButton PinControlButton => ModuleListPanel.Instance.PinControlButton;
        static AnchorToggleButton ShowARJoystickButton => ModuleListPanel.Instance.ShowARJoystickButton;
        static ARJoystick ARJoystick => ModuleListPanel.Instance.ARJoystick;

        readonly ARConfiguration config = new ARConfiguration();
        IModuleData moduleData;

        float? joyVelocityAngle;
        Vector3? joyVelocityPos;

        protected Canvas canvas;

        readonly MarkerDetector detector = new MarkerDetector();
        readonly ARMarkerExecutor executor = new ARMarkerExecutor();

        public Sender<PoseStamped> HeadSender { get; private set; }
        public Sender<DetectedARMarkerArray> MarkerSender { get; private set; }


        public static bool HasARController => Instance != null;
        [CanBeNull] public static ARFoundationController Instance { get; protected set; }

        public ARConfiguration Config
        {
            get => config;
            set
            {
                Visible = value.Visible;
                WorldScale = value.WorldScale;

                OcclusionQuality = value.OcclusionQuality;
            }
        }

        public virtual OcclusionQualityType OcclusionQuality
        {
            get => config.OcclusionQuality;
            set => config.OcclusionQuality = value;
        }

        public Vector3 WorldPosition
        {
            get => config.WorldOffset;
            private set => config.WorldOffset = value;
        }

        public float WorldAngle
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

        public virtual bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                GuiInputModule.Instance.DisableCameraLock();
                TfListener.RootFrame.transform.SetPose(value ? WorldPose : Pose.identity);
                ARModeChanged?.Invoke(value);
            }
        }

        public bool EnableQrDetection
        {
            get => config.EnableQrDetection;
            set
            {
                config.EnableQrDetection = value;
                if (detector != null)
                {
                    detector.EnableQr = value;
                }
            }
        }

        public bool EnableArucoDetection
        {
            get => config.EnableArucoDetection;
            set
            {
                config.EnableArucoDetection = value;
                if (detector != null)
                {
                    detector.EnableAruco = value;
                }
            }
        }

        static float TfRootScale
        {
            set => TfListener.RootFrame.transform.localScale = value * Vector3.one;
        }

        protected virtual bool PinRootMarker
        {
            get => config.PinRootMarker;
            set
            {
                config.PinRootMarker = value;
                PinControlButton.State = value;
            }
        }

        bool ShowARJoystick
        {
            get => config.ShowARJoystick;
            set
            {
                config.ShowARJoystick = value;
                ShowARJoystickButton.State = value;
                ARJoystick.Visible = value;

                if (value)
                {
                    ModuleListPanel.Instance.AllGuiVisible = false;
                }
            }
        }

        protected virtual void Awake()
        {
            gameObject.name = "AR";

            if (canvas == null)
            {
                canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            }

            //node = FrameNode.Instantiate("AR Node");

            PinControlButton.Visible = true;
            ShowARJoystickButton.Visible = true;

            PinControlButton.Clicked += OnPinControlButtonClicked;
            ShowARJoystickButton.Clicked += OnShowARJoystickClicked;

            ARJoystick.ChangedPosition += OnARJoystickChangedPosition;
            ARJoystick.ChangedAngle += OnARJoystickChangedAngle;
            ARJoystick.PointerUp += OnARJoystickPointerUp;

            GuiInputModule.Instance.UpdateQualityLevel();

            HeadSender = new Sender<PoseStamped>("head");
            MarkerSender = new Sender<DetectedARMarkerArray>("markers");

            detector.MarkerDetected += OnMarkerDetected;
        }

        static int Sign(float f) => f > 0 ? 1 : f < 0 ? -1 : 0;
        static int Sign(Vector3 v) => Sign(v.x) + Sign(v.y) + Sign(v.z); // only one of the components is nonzero

        void OnARJoystickChangedAngle(float dA)
        {
            if (joyVelocityAngle == null)
            {
                joyVelocityAngle = 0;
            }
            else if (Sign(joyVelocityAngle.Value) != 0 && Sign(joyVelocityAngle.Value) != Sign(dA))
            {
                joyVelocityAngle = 0;
            }
            else
            {
                joyVelocityAngle += 0.02f * dA;
            }

            if (ARJoystick.IsGlobal)
            {
                SetWorldAngle(WorldAngle + joyVelocityAngle.Value, RootMover.ControlMarker);
            }
            else
            {
                Vector3 forward = Settings.MainCameraTransform.forward;
                Vector3 cameraPosition = Settings.MainCameraTransform.position + forward;
                var q1 = Pose.identity.WithPosition(cameraPosition);
                var q2 = Pose.identity.WithRotation(Quaternion.AngleAxis(joyVelocityAngle.Value, Vector3.up));
                var q3 = Pose.identity.WithPosition(-cameraPosition);
                SetWorldPose(q1.Multiply(q2.Multiply(q3.Multiply(WorldPose))), RootMover.ControlMarker);
            }
        }

        void OnARJoystickChangedPosition(Vector3 dPos)
        {
            Vector3 deltaPosition = 0.0005f * dPos;
            if (joyVelocityPos == null)
            {
                joyVelocityPos = Vector3.zero;
            }
            else if (Sign(joyVelocityPos.Value) != 0 && Sign(joyVelocityPos.Value) != Sign(dPos))
            {
                joyVelocityPos = Vector3.zero;
            }
            else
            {
                joyVelocityPos += deltaPosition;
            }

            Vector3 deltaWorldPosition;
            if (ARJoystick.IsGlobal)
            {
                deltaWorldPosition = WorldPose.rotation * joyVelocityPos.Value.Ros2Unity();
            }
            else
            {
                float rotY = Settings.MainCameraTransform.rotation.eulerAngles.y;
                Quaternion cameraRotation = Quaternion.Euler(0, rotY, 0);
                (float joyX, float joyY, float joyZ) = joyVelocityPos.Value;
                deltaWorldPosition = cameraRotation * new Vector3(joyY, joyZ, joyX);
            }

            SetWorldPosition(WorldPosition + deltaWorldPosition, RootMover.ControlMarker);
        }

        void OnARJoystickPointerUp()
        {
            joyVelocityPos = null;
            joyVelocityAngle = null;
        }

        void OnPinControlButtonClicked()
        {
            PinRootMarker = PinControlButton.State;
        }

        void OnShowARJoystickClicked()
        {
            ShowARJoystick = ShowARJoystickButton.State;
        }

        public IModuleData ModuleData
        {
            get => moduleData ?? throw new InvalidOperationException("Controller has not been started!");
            set => moduleData = value ?? throw new InvalidOperationException("Cannot set null value as module data");
        }

        [NotNull] public TfFrame Frame => TfListener.Instance.FixedFrame;

        public static event Action<bool> ARModeChanged;

        public event Action<RootMover> WorldPoseChanged;

        void UpdateWorldPose(in Pose pose, RootMover mover)
        {
            WorldPose = pose;
            if (Visible)
            {
                TfListener.RootFrame.transform.SetPose(pose);
            }

            WorldPoseChanged?.Invoke(mover);
        }

        protected static float AngleFromPose(in Pose unityPose)
        {
            float angle = unityPose.rotation.eulerAngles.y;
            if (angle > 180)
            {
                angle -= 360;
            }

            return angle;
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

        public static Pose RelativePoseToWorld(in Pose unityPose)
        {
            return Instance == null ? unityPose : Instance.WorldPose.Inverse().Multiply(unityPose);
        }

        uint headSeq;

        public virtual void Update()
        {
            HeadSender.Publish(new PoseStamped(
                new Header(headSeq++, time.Now(), TfListener.FixedFrameId ?? ""),
                TfListener.RelativePoseToFixedFrame(Settings.MainCameraTransform.AsPose()).Unity2RosPose()
                    .ToCameraFrame()
            ));
        }


        uint markerSeq;

        void OnMarkerDetected(Screenshot screenshot, IEnumerable<IMarkerCorners> markers)
        {
            var array = markers.Select(marker => new DetectedARMarker
            {
                MarkerType = marker is ArucoMarkerCorners ? ARMarkerType.Aruco : ARMarkerType.QrCode,
                Header = new Header(markerSeq++, screenshot.Timestamp, TfListener.FixedFrameId ?? ""),
                ArucoId = marker is ArucoMarkerCorners aruco ? (uint) aruco.Id : 0,
                QrCode = marker is QrMarkerCorners qr ? qr.Code : "",
                CameraPose = TfListener.RelativePoseToFixedFrame(screenshot.CameraPose).Unity2RosPose()
                    .ToCameraFrame(),
                Corners = marker.Corners.ToArray(),
                Intrinsic = new Intrinsic(screenshot.Fx, screenshot.Cx, screenshot.Fy, screenshot.Cy)
            }).ToArray();

            MarkerSender.Publish(new DetectedARMarkerArray {Markers = array});
            
            foreach (var marker in array)
            {
                if (marker.MarkerType == ARMarkerType.QrCode && marker.QrCode.HasPrefix(ARMarkerExecutor.Prefix))
                {
                    executor.Execute(marker);
                }
            }
        }

        public virtual void StopController()
        {
            Visible = false;
            WorldScale = 1;

            PinControlButton.Visible = false;
            ShowARJoystickButton.Visible = false;

            WorldPoseChanged = null;
            ARJoystick.ChangedPosition -= OnARJoystickChangedPosition;
            ARJoystick.ChangedAngle -= OnARJoystickChangedAngle;
            ARJoystick.PointerUp -= OnARJoystickPointerUp;
            PinControlButton.Clicked -= OnPinControlButtonClicked;
            ShowARJoystickButton.Clicked -= OnShowARJoystickClicked;
            ShowARJoystick = false;
            Instance = null;

            GuiInputModule.Instance.UpdateQualityLevel();

            HeadSender.Stop();
            MarkerSender.Stop();

            detector.Dispose();
        }

        void IController.ResetController()
        {
        }

        /*
        void OnDestroy()
        {
            Debug.Log("AR Controller Destroyed!");
        }
        */
    }
}