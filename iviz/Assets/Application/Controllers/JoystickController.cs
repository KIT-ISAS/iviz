using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using Iviz.RoslibSharp;
using System;
using Iviz.Resources;

namespace Iviz.App.Listeners
{
    [DataContract]
    public sealed class JoystickConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.Joystick;
        [DataMember] public bool Visible { get; set; } = true;

        [DataMember] public bool PublishJoy { get; set; } = true;
        [DataMember] public bool PublishTwist { get; set; } = true;
        [DataMember] public SerializableVector3 MaxSpeed { get; set; } = Vector3.one * 0.25f;
        [DataMember] public string AttachToFrame { get; set; } = "";
        [DataMember] public bool XIsFront { get; set; } = true;
    }


    public sealed class JoystickController : MonoBehaviour, IController
    {
        public ModuleData ModuleData { get; set; }

        readonly JoystickConfiguration config = new JoystickConfiguration();
        public JoystickConfiguration Config
        {
            get => config;
            set
            {
                Visible = config.Visible;
                PublishJoy = config.PublishJoy;
                PublishTwist = config.PublishTwist;
                MaxSpeed = config.MaxSpeed;
                AttachToFrame = config.AttachToFrame;
                XIsFront = config.XIsFront;
            }
        }

        public string JoyTopic => $"{ConnectionManager.MyId}/joy";
        public string TwistTopic => $"{ConnectionManager.MyId}/twist";

        Joystick joystick_;
        public Joystick Joystick
        {
            get => joystick_;
            set
            {
                joystick_ = value;
                Joystick.Visible = Visible;
            }
        }

        public RosSender<Msgs.SensorMsgs.Joy> RosSenderJoy { get; private set; }
        public RosSender<Msgs.GeometryMsgs.TwistStamped> RosSenderTwist { get; private set; }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                if (Joystick != null)
                {
                    Joystick.Visible = value;
                }
            }
        }

        public bool PublishJoy
        {
            get => config.PublishJoy;
            set
            {
                config.PublishJoy = value;
                if (value)
                {
                    if (RosSenderJoy != null && RosSenderJoy.Topic != JoyTopic)
                    {
                        RosSenderJoy.Stop();
                        RosSenderJoy = null;
                    }
                    if (RosSenderJoy == null)
                    {
                        RosSenderJoy = new RosSender<Msgs.SensorMsgs.Joy>(JoyTopic);
                    }
                }
            }
        }

        public bool PublishTwist
        {
            get => config.PublishTwist;
            set
            {
                config.PublishTwist = value;
                if (value)
                {
                    if (RosSenderTwist != null && RosSenderTwist.Topic != TwistTopic)
                    {
                        RosSenderTwist.Stop();
                        RosSenderTwist = null;
                    }
                    if (RosSenderTwist == null)
                    {
                        RosSenderTwist = new RosSender<Msgs.GeometryMsgs.TwistStamped>(TwistTopic);
                    }

                }
            }
        }

        public string AttachToFrame
        {
            get => config.AttachToFrame;
            set
            {
                config.AttachToFrame = value;
            }
        }

        public bool XIsFront
        {
            get => config.XIsFront;
            set
            {
                config.XIsFront = value;
            }
        }

        public Vector3 MaxSpeed
        {
            get => config.MaxSpeed;
            set => config.MaxSpeed = value.HasNaN() ? Vector3.zero : value;
        }

        void Awake()
        {
            Config = new JoystickConfiguration();
        }

        uint twistSeq = 0;
        uint joySeq = 0;
        void Update()
        {
            if (Joystick == null)
            {
                return;
            }
            if (RosSenderTwist != null)
            {
                Vector2 leftDir = Joystick.Left;
                Vector2 rightDir = Joystick.Right;

                string frame = string.IsNullOrWhiteSpace(AttachToFrame) ?
                    TFListener.BaseFrameId :
                    AttachToFrame;

                Vector2 linear = XIsFront ?
                    new Vector2(leftDir.y, -leftDir.x) :
                    new Vector2(leftDir.x, leftDir.y);

                Msgs.GeometryMsgs.TwistStamped twist = new Msgs.GeometryMsgs.TwistStamped(
                    Header: RosUtils.CreateHeader(twistSeq++, frame),
                    Twist: new Msgs.GeometryMsgs.Twist(
                        Linear: new Msgs.GeometryMsgs.Vector3(linear.x * MaxSpeed.x, linear.y * MaxSpeed.y, 0),
                        Angular: new Msgs.GeometryMsgs.Vector3(0, 0, rightDir.x * MaxSpeed.z)
                        )
                    );
                RosSenderTwist.Publish(twist);
            }
            if (RosSenderJoy != null)
            {
                Vector2 leftDir = Joystick.Left;
                Vector2 rightDir = Joystick.Right;

                string frame = string.IsNullOrWhiteSpace(AttachToFrame) ?
                    TFListener.BaseFrameId :
                    AttachToFrame;

                Msgs.SensorMsgs.Joy joy = new Msgs.SensorMsgs.Joy(
                    Header: RosUtils.CreateHeader(joySeq++, frame),
                    Axes: new[] { leftDir.x, leftDir.y, rightDir.x, rightDir.y },
                    Buttons: Array.Empty<int>()
                    );
                RosSenderJoy.Publish(joy);
            }
        }

        public void Stop()
        {
            RosSenderJoy.Stop();
            RosSenderTwist.Stop();
        }
    }
}
