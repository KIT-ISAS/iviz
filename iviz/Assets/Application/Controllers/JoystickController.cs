#nullable enable

using System;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using JetBrains.Annotations;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed class JoystickController : IController
    {
        readonly JoystickConfiguration config = new();
        readonly TwistJoystick joystick;

        uint joySeq;
        uint twistSeq;

        public Sender<Joy>? SenderJoy { get; private set; }
        public ISender? SenderTwist { get; private set; }

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

        public bool Visible
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
                    RebuildJoy();
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
            set => config.AttachToFrame = string.IsNullOrEmpty(value) ? TfListener.FixedFrameId : value;
        }

        public bool XIsFront
        {
            get => config.XIsFront;
            set => config.XIsFront = value;
        }

        public Vector3 MaxSpeed
        {
            get => config.MaxSpeed;
            set => config.MaxSpeed = value.IsInvalid() ? Vector3.zero : value;
        }

        public void Dispose()
        {
            SenderJoy?.Dispose();
            SenderTwist?.Dispose();
            Visible = false;
        }

        public void ResetController()
        {
        }

        void RebuildJoy()
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
                ? TwistStamped.RosMessageType
                : Twist.RosMessageType;

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

        void PublishData(TwistJoystick.Source _, Vector2 __)
        {
            if (!Visible)
            {
                return;
            }

            if (SenderTwist != null)
            {
                Vector2 leftDir = joystick.Left;
                Vector2 rightDir = joystick.Right;

                Vector2 linear = XIsFront ? new Vector2(leftDir.y, -leftDir.x) : new Vector2(leftDir.x, leftDir.y);

                var twist = new Twist(
                    (linear.x * MaxSpeed.x, linear.y * MaxSpeed.y, 0),
                    (0, 0, -rightDir.x * MaxSpeed.z)
                );

                if (UseTwistStamped)
                {
                    string frameId = string.IsNullOrEmpty(AttachToFrame) ? TfListener.FixedFrameId : AttachToFrame;
                    var twistStamped = new TwistStamped(
                        TfListener.CreateHeader(twistSeq++, frameId),
                        twist
                    );
                    SenderTwist.Publish(twistStamped);
                }
                else
                {
                    ((Sender<Twist>)SenderTwist).Publish(twist);
                }
            }

            if (SenderJoy != null)
            {
                var leftDir = joystick.Left;
                var rightDir = joystick.Right;

                string frameId = string.IsNullOrEmpty(AttachToFrame) ? TfListener.FixedFrameId : AttachToFrame;
                var joy = new Joy(
                    TfListener.CreateHeader(joySeq++, frameId),
                    new[] { leftDir.x, leftDir.y, rightDir.x, rightDir.y },
                    Array.Empty<int>()
                );
                SenderJoy.Publish(joy);
            }
        }
    }
}