#nullable enable

using System;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed class JoystickController : Controller
    {
        const string DefaultJoyTopic = "~joy";
        const string DefaultTwistTopic = "~twist";

        readonly JoystickConfiguration config = new();
        readonly TwistJoystick joystick;

        uint joySeq;
        uint twistSeq;

        public Sender<Joy>? SenderJoy { get; private set; }
        public Sender? SenderTwist { get; private set; }


        public JoystickController(TwistJoystick joystick)
        {
            this.joystick = joystick;
            this.joystick.Changed += PublishData;

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
                MaxSpeed = value.MaxSpeed.ToUnity();
                AttachToFrame = value.AttachToFrame;
                XIsFront = value.XIsFront;
                Mode = value.Mode;
            }
        }

        public string JoyTopic
        {
            get => config.JoyTopic;
            set
            {
                config.JoyTopic = string.IsNullOrWhiteSpace(value) ? DefaultJoyTopic : value.Trim();
                RebuildSenders();
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
                config.TwistTopic = string.IsNullOrWhiteSpace(value) ? DefaultTwistTopic : value.Trim();
                RebuildTwist();
            }
        }

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                joystick.Visible = value;
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
                    RebuildSenders();
                }
                else if (!value && SenderJoy != null)
                {
                    SenderJoy.Dispose();
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
                    SenderTwist.Dispose();
                    SenderTwist = null;
                }
            }
        }

        public string AttachToFrame
        {
            get => config.AttachToFrame;
            set => config.AttachToFrame = string.IsNullOrWhiteSpace(value) ? TfModule.FixedFrameId : value;
        }

        public bool XIsFront
        {
            get => config.XIsFront;
            set => config.XIsFront = value;
        }

        public Vector3 MaxSpeed
        {
            get => config.MaxSpeed.ToUnity();
            set => config.MaxSpeed = value.IsInvalid() ? default : value.ToRos();
        }

        public JoystickMode Mode
        {
            get => config.Mode;
            set
            {
                config.Mode = value;
                (joystick.LeftVisible, joystick.MiddleLeftVisible, joystick.MiddleRightVisible, joystick.RightVisible) =
                    Mode switch
                    {
                        JoystickMode.Left => (true, false, false, false),
                        JoystickMode.Right => (false, false, false, true),
                        JoystickMode.Two => (true, false, false, true),
                        JoystickMode.Four => (true, true, true, true),
                        _ => (false, false, false, false),
                    };
            }
        }

        public void Dispose()
        {
            SenderJoy?.Dispose();
            SenderTwist?.Dispose();
            joystick.Changed -= PublishData;
            Visible = false;
        }

        public override void ResetController()
        {
        }

        void RebuildSenders()
        {
            if (SenderJoy != null && SenderJoy.Topic != config.JoyTopic)
            {
                SenderJoy.Dispose();
                SenderJoy = null;
            }

            SenderJoy ??= new Sender<Joy>(JoyTopic);
        }

        void RebuildTwist()
        {
            string twistType = UseTwistStamped
                ? TwistStamped.MessageType
                : Twist.MessageType;

            if (SenderTwist != null &&
                (SenderTwist.Topic != config.TwistTopic || SenderTwist.Type != twistType))
            {
                SenderTwist.Dispose();
                SenderTwist = null;
            }

            SenderTwist ??= UseTwistStamped
                ? new Sender<TwistStamped>(TwistTopic)
                : new Sender<Twist>(TwistTopic);
        }

        void PublishData()
        {
            if (!Visible)
            {
                return;
            }

            var (leftX, leftY) = joystick.Left;
            var (rightX, rightY) = joystick.Right;

            string frameId = string.IsNullOrWhiteSpace(AttachToFrame) ? TfModule.FixedFrameId : AttachToFrame;

            if (SenderTwist != null)
            {
                var (linearX, linearY) = XIsFront ? (leftY, -leftX) : (leftX, leftY);
                var twist = new Twist(
                    new Msgs.GeometryMsgs.Vector3(linearX * MaxSpeed.x, linearY * MaxSpeed.y, 0),
                    new Msgs.GeometryMsgs.Vector3(0, 0, -rightX * MaxSpeed.z)
                );

                if (UseTwistStamped)
                {
                    var sender = (Sender<TwistStamped>)SenderTwist;
                    sender.Publish(
                        new TwistStamped(
                            TfListener.CreateHeader(twistSeq++, frameId),
                            twist
                        ));
                }
                else
                {
                    var sender = (Sender<Twist>)SenderTwist;
                    sender.Publish(twist);
                }
            }

            SenderJoy?.Publish(
                new Joy(
                    TfListener.CreateHeader(joySeq++, frameId),
                    Mode switch
                    {
                        JoystickMode.Left => new[] { leftX, leftY },
                        JoystickMode.Right => new[] { rightX, rightY },
                        JoystickMode.Two => new[] { leftX, leftY, rightX, rightY },
                        JoystickMode.Four => new[]
                        {
                            leftX, leftY,
                            joystick.MiddleLeft.x, joystick.MiddleLeft.y,
                            joystick.MiddleRight.x, joystick.MiddleRight.y,
                            rightX, rightY
                        },
                        _ => Array.Empty<float>()
                    },
                    Array.Empty<int>()
                ));
        }
    }
}