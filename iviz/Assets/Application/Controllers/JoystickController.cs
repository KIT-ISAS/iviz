using System;
using System.Runtime.Serialization;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class JoystickConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string JoyTopic { get; set; } = "";
        [DataMember] public bool PublishJoy { get; set; }
        [DataMember] public string TwistTopic { get; set; } = "";
        [DataMember] public bool PublishTwist { get; set; } = true;
        [DataMember] public bool UseTwistStamped { get; set; }
        [DataMember] public SerializableVector3 MaxSpeed { get; set; } = Vector3.one * 0.25f;
        [DataMember, NotNull] public string AttachToFrame { get; set; } = TfListener.MapFrameId;
        [DataMember] public bool XIsFront { get; set; } = true;
        [DataMember, NotNull] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Joystick;
        [DataMember] public bool Visible { get; set; } = true;
    }


    public sealed class JoystickController : IController
    {
        readonly JoystickConfiguration config = new JoystickConfiguration();
        uint joySeq;
        uint twistSeq;

        TwistJoystick joystick;

        public JoystickController([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData;
            Config = new JoystickConfiguration();
        }

        public JoystickConfiguration Config
        {
            get => config;
            set
            {
                Visible = value.Visible;
                JoyTopic = value.JoyTopic;
                PublishJoy = value.PublishJoy;
                TwistTopic = value.TwistTopic;
                PublishTwist = value.PublishTwist;
                MaxSpeed = value.MaxSpeed;
                AttachToFrame = value.AttachToFrame;
                XIsFront = value.XIsFront;
            }
        }

        public string JoyTopic
        {
            get => config.JoyTopic;
            set
            {
                config.JoyTopic = string.IsNullOrWhiteSpace(value) ? "~joy" : value.Trim();
                RebuildJoy();
            }
        }

        public bool UseTwistStamped
        {
            get => config.UseTwistStamped;
            set
            {
                config.UseTwistStamped = value;
                RebuildTwist();
            }
        }

        public string TwistTopic
        {
            get => config.TwistTopic;
            set
            {
                config.TwistTopic = string.IsNullOrWhiteSpace(value) ? "~twist" : value.Trim();
                RebuildTwist();
            }
        }

        public TwistJoystick Joystick
        {
            get => joystick;
            set
            {
                if (joystick == value)
                {
                    return;
                }

                if (joystick != null)
                {
                    joystick.Changed -= PublishData;
                }

                joystick = value;

                if (joystick != null)
                {
                    joystick.Changed += PublishData;
                    Joystick.Visible = Visible;
                }
            }
        }

        public Sender<Joy> SenderJoy { get; private set; }
        public ISender SenderTwist { get; private set; }

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
                if (value && SenderJoy == null)
                {
                    RebuildJoy();
                }
                else if (!value && SenderJoy != null)
                {
                    SenderJoy.Stop();
                    SenderJoy = null;
                }
            }
        }

        public bool PublishTwist
        {
            get => config.PublishTwist;
            set
            {
                config.PublishTwist = value;
                if (value && SenderTwist == null)
                {
                    RebuildTwist();
                }
                else if (!value && SenderTwist != null)
                {
                    SenderTwist.Stop();
                    SenderTwist = null;
                }
            }
        }

        [NotNull]
        public string AttachToFrame
        {
            get => config.AttachToFrame;
            set => config.AttachToFrame = value;
        }

        public bool XIsFront
        {
            get => config.XIsFront;
            set => config.XIsFront = value;
        }

        public Vector3 MaxSpeed
        {
            get => config.MaxSpeed;
            set => config.MaxSpeed = value.HasNaN() ? Vector3.zero : value;
        }

        public IModuleData ModuleData { get; }

        public void StopController()
        {
            SenderJoy?.Stop();
            SenderTwist?.Stop();
            Visible = false;
            Joystick = null;
        }

        public void ResetController()
        {
        }

        void RebuildJoy()
        {
            if (SenderJoy != null && SenderJoy.Topic != config.JoyTopic)
            {
                SenderJoy.Stop();
                SenderJoy = null;
            }

            if (SenderJoy is null)
            {
                SenderJoy = new Sender<Joy>(JoyTopic);
            }
        }

        void RebuildTwist()
        {
            string twistType = UseTwistStamped
                ? TwistStamped.RosMessageType
                : Twist.RosMessageType;

            if (SenderTwist != null &&
                (SenderTwist.Topic != config.TwistTopic || SenderTwist.Type != twistType))
            {
                SenderTwist.Stop();
                SenderTwist = null;
            }

            if (SenderTwist == null)
            {
                SenderTwist = UseTwistStamped
                    ? (ISender) new Sender<TwistStamped>(TwistTopic)
                    : new Sender<Twist>(TwistTopic);
            }
        }

        void PublishData(TwistJoystick.Source _, Vector2 __)
        {
            if (!Visible)
            {
                return;
            }

            if (SenderTwist != null)
            {
                Vector2 leftDir = Joystick.Left;
                Vector2 rightDir = Joystick.Right;

                Vector2 linear = XIsFront ? new Vector2(leftDir.y, -leftDir.x) : new Vector2(leftDir.x, leftDir.y);

                Twist twist = (
                    (linear.x * MaxSpeed.x, linear.y * MaxSpeed.y, 0),
                    (0, 0, -rightDir.x * MaxSpeed.z)
                );

                if (UseTwistStamped)
                {
                    string frameId = string.IsNullOrEmpty(AttachToFrame) ? TfListener.FixedFrameId : AttachToFrame;
                    TwistStamped twistStamped = new TwistStamped(
                        (twistSeq++, frameId),
                        twist
                    );
                    SenderTwist.Publish(twistStamped);
                }
                else
                {
                    ((Sender<Twist>) SenderTwist).Publish(twist);
                }
            }

            if (SenderJoy != null)
            {
                Vector2 leftDir = Joystick.Left;
                Vector2 rightDir = Joystick.Right;

                string frameId = string.IsNullOrEmpty(AttachToFrame) ? TfListener.FixedFrameId : AttachToFrame;
                Joy joy = new Joy(
                    (joySeq++, frameId),
                    new[] {leftDir.x, leftDir.y, rightDir.x, rightDir.y},
                    Array.Empty<int>()
                );
                SenderJoy.Publish(joy);
            }
        }
    }
}