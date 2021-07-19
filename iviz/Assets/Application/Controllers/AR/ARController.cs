﻿using System;
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
        [DataMember] public bool EnableMeshing { get; set; }
        [DataMember] public bool EnablePlaneDetection { get; set; } = true;
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
        [NotNull] static Camera ARCamera => Settings.ARCamera.CheckedNull() ?? Settings.MainCamera;

        public enum RootMover
        {
            Anchor,
            Executor,
            ControlMarker,
            Setup
        }

        public static readonly Vector3 DefaultWorldOffset = new Vector3(0.5f, 0, -0.2f);

        //static AnchorToggleButton PinControlButton => ModuleListPanel.Instance.PinControlButton;
        //static AnchorToggleButton ShowARJoystickButton => ModuleListPanel.Instance.ShowARJoystickButton;
        static ARJoystick ARJoystick => ModuleListPanel.Instance.ARJoystick;

        readonly ARConfiguration config = new ARConfiguration();
        IModuleData moduleData;

        float? joyVelocityAngle;
        Vector3? joyVelocityPos;

        protected Canvas canvas;

        readonly MarkerDetector detector = new MarkerDetector();
        
        public ARMarkerExecutor MarkerExecutor { get; } = new ARMarkerExecutor();

        //public Sender<PoseStamped> HeadSender { get; private set; }
        public Sender<DetectedARMarkerArray> MarkerSender { get; private set; }

        public static bool HasARController => Instance != null;
        [CanBeNull] public static ARFoundationController Instance { get; protected set; }

        const string CameraFrameId = "~ar_head";

        public static bool InstanceVisible => Instance != null && Instance.Visible;

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
                if (GuiInputModule.Instance != null)
                {
                    GuiInputModule.Instance.DisableCameraLock();
                }

                TfListener.RootFrame.Transform.SetPose(value ? WorldPose : Pose.identity);
                ARCameraViewChanged?.Invoke(value);
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
                if (detector != null)
                {
                    detector.EnableAruco = value;
                }
            }
        }

        static float TfRootScale
        {
            set => TfListener.RootScale = value;
        }

        public virtual bool PinRootMarker
        {
            get => config.PinRootMarker;
            set
            {
                config.PinRootMarker = value;
                //PinControlButton.State = value;
            }
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

        public IModuleData ModuleData
        {
            get => moduleData ?? throw new InvalidOperationException("Controller has not been started!");
            set => moduleData = value ?? throw new InvalidOperationException("Cannot set null value as module data");
        }

        [CanBeNull] TfFrame cameraFrame;

        [NotNull]
        public TfFrame Frame
        {
            get
            {
                string frameId = TfListener.ResolveFrameId(CameraFrameId);
                if (cameraFrame == null || frameId != cameraFrame.Id)
                {
                    cameraFrame = TfListener.ResolveFrame(CameraFrameId);
                    cameraFrame.ForceInvisible = true;
                }

                return cameraFrame;
            }
        }

        /// <summary>
        /// AR has been enabled / disabled
        /// </summary>
        public static event Action<bool> ARActiveChanged;

        /// <summary>
        /// AR camera view has been enabled / disabled
        /// </summary>
        public static event Action<bool> ARCameraViewChanged;

        public event Action<RootMover> WorldPoseChanged;


        protected virtual void Awake()
        {
            gameObject.name = "AR";

            if (canvas == null)
            {
                canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            }

            ARJoystick.ChangedPosition += OnARJoystickChangedPosition;
            ARJoystick.ChangedAngle += OnARJoystickChangedAngle;
            ARJoystick.PointerUp += OnARJoystickPointerUp;

            if (GuiInputModule.Instance != null)
            {
                GuiInputModule.Instance.UpdateQualityLevel();
            }

            MarkerSender = new Sender<DetectedARMarkerArray>("~markers");

            detector.MarkerDetected += OnMarkerDetected;
        }

        protected void RaiseARActiveChanged()
        {
            ARActiveChanged?.Invoke(true);
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
                //var arCameraPose = RelativePoseToOrigin(ARCamera.transform.AsPose());
                //Vector3 pivot = arCameraPose.Multiply(Vector3.forward);

                var arCameraPose = ARCamera.transform.AsPose();
                Vector3 pivot = arCameraPose.Multiply(Vector3.forward);
                //Debug.Log(pivot);

                /*
                var q1 = Pose.identity.WithPosition(pivot);
                var q2 = Pose.identity.WithRotation(Quaternion.AngleAxis(joyVelocityAngle.Value, Vector3.up));
                var q3 = Pose.identity.WithPosition(-pivot);
                var targetPose = q1.Multiply(q2.Multiply(q3.Multiply(WorldPose)));
                SetWorldPose(targetPose, RootMover.ControlMarker);
                */


                Quaternion rotation = Quaternion.AngleAxis(joyVelocityAngle.Value, Vector3.up);
                var pose = new Pose(rotation * (-pivot) + pivot, rotation);
                //SetWorldPose(pose.Multiply(WorldPose), RootMover.ControlMarker);

                SetWorldPose(pose.Multiply(WorldPose), RootMover.ControlMarker);
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
                var arCameraPose = RelativePoseToOrigin(ARCamera.transform.AsPose());
                float rotY = arCameraPose.rotation.eulerAngles.y;
                Quaternion cameraRotation = Quaternion.Euler(0, rotY, 0);
                (float joyX, float joyY, float joyZ) = joyVelocityPos.Value;
                deltaWorldPosition = cameraRotation * new Vector3(joyX, joyZ, joyY);
            }

            SetWorldPosition(WorldPosition + deltaWorldPosition, RootMover.ControlMarker);
        }

        void OnARJoystickPointerUp()
        {
            joyVelocityPos = null;
            joyVelocityAngle = null;
        }

        /*
        void OnPinControlButtonClicked()
        {
            PinRootMarker = PinControlButton.State;
        }

        void OnShowARJoystickClicked()
        {
            ShowARJoystick = ShowARJoystickButton.State;
        }
        */

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

        public static Pose RelativePoseToOrigin(in Pose unityPose)
        {
            if (Instance == null || Instance.Visible)
            {
                return unityPose;
            }

            return Instance.WorldPose.Inverse().Multiply(unityPose);
        }

        public static Pose OriginToRelativePose(in Pose unityPose)
        {
            if (Instance == null || Instance.Visible)
            {
                return unityPose;
            }

            return Instance.WorldPose.Multiply(unityPose);
        }

        public virtual void Update()
        {
            var absoluteArCameraPose = RelativePoseToOrigin(ARCamera.transform.AsPose());
            var relativePose = TfListener.RelativePoseToFixedFrame(absoluteArCameraPose).Unity2RosTransform();
            TfListener.Publish(TfListener.FixedFrameId, CameraFrameId, relativePose);
        }


        uint markerSeq;
        void OnMarkerDetected([NotNull] Screenshot screenshot, [NotNull] IReadOnlyList<IMarkerCorners> markers)
        {
            DetectedARMarker ToMarker(IMarkerCorners marker) => new DetectedARMarker
            {
                Type = (byte) marker.Type,
                Header = new Header(markerSeq++, screenshot.Timestamp, TfListener.FixedFrameId),
                Code = marker.Code,
                CameraPose = TfListener.RelativePoseToFixedFrame(screenshot.CameraPose)
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

            MarkerSender.Publish(new DetectedARMarkerArray {Markers = array});
        }
        
        

        public virtual void StopController()
        {
            ARActiveChanged?.Invoke(false);

            Visible = false;
            WorldScale = 1;

            WorldPoseChanged = null;
            ARJoystick.ChangedPosition -= OnARJoystickChangedPosition;
            ARJoystick.ChangedAngle -= OnARJoystickChangedAngle;
            ARJoystick.PointerUp -= OnARJoystickPointerUp;
            ShowARJoystick = false;
            Instance = null;

            if (GuiInputModule.Instance != null)
            {
                GuiInputModule.Instance.UpdateQualityLevel();
            }

            MarkerSender.Stop();
            detector.Dispose();
        }

        void IController.ResetController()
        {
        }
    }
}