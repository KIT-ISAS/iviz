#nullable enable

using System;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib;

namespace Iviz.Ros
{
    public sealed class RosOutLogger
    {
        const string RosOutTopic = "/rosout";
        const string RosOutAggTopic = "/rosout_agg";

        static uint logSeq;

        public delegate void LogDelegate(in LogMessage log);

        public static LogDelegate? MessageArrived;

        public LogLevel MinLogLevel { get; set; } = LogLevel.Info;
        public Listener Listener { get; private set; }
        public Sender Sender { get; private set; }

        public RosOutLogger()
        {
            (Sender, Listener) = CreateSenderAndListener(RosManager.Connection.RosVersion);

            RosConnection.RosVersionChanged += UpdateRosVersion;
            RosLogger.LogExternal += Publish;
        }

        void UpdateRosVersion(RosVersion version)
        {
            Sender.Dispose();
            Listener.Dispose();
            (Sender, Listener) = CreateSenderAndListener(RosManager.Connection.RosVersion);
        }

        static (Sender, Listener) CreateSenderAndListener(RosVersion version)
        {
            return (
                version is RosVersion.ROS1 or RosVersion.ROSBridge
                    ? new Sender<Msgs.RosgraphMsgs.Log>(RosOutTopic)
                    : new Sender<Msgs.RclInterfaces.Log>(RosOutTopic),
                version is RosVersion.ROS1 or RosVersion.ROSBridge
                    ? new Listener<Msgs.RosgraphMsgs.Log>(RosOutAggTopic, Handle, RosSubscriptionProfile.SpammyCanDrop)
                    : new Listener<Msgs.RclInterfaces.Log>(RosOutTopic, Handle, RosSubscriptionProfile.SpammyCanDrop)
            );
        }

        static bool Handle(Msgs.RosgraphMsgs.Log msg, IRosConnection _)
        {
            try
            {
                MessageArrived?.Invoke(new LogMessage(msg));
            }
            catch (Exception e)
            {
                RosLogger.Error($"{nameof(RosOutLogger)}: Error during {nameof(Handle)}", e);
            }

            return true;
        }

        static bool Handle(Msgs.RclInterfaces.Log msg, IRosConnection _)
        {
            try
            {
                MessageArrived?.Invoke(new LogMessage(msg));
            }
            catch (Exception e)
            {
                RosLogger.Error($"{nameof(RosOutLogger)}: Error during {nameof(Handle)}", e);
            }

            return true;
        }

        void Publish(in LogMessage msg)
        {
            if (msg.Level < MinLogLevel)
            {
                return;
            }

            switch (Sender)
            {
                case Sender<Msgs.RosgraphMsgs.Log> sender1:
                    sender1.Publish(ToRos1(msg));
                    break;
                case Sender<Msgs.RclInterfaces.Log> sender2:
                    sender2.Publish(ToRos2(msg));
                    break;
            }
        }

        static Msgs.RosgraphMsgs.Log ToRos1(in LogMessage msg) => new()
        {
            Header = new Header(logSeq++, GameThread.TimeNow, ""),
            Level = LogMessage.ToRos1(msg.Level),
            Name = RosManager.Connection.MyId ?? "/iviz",
            Msg = msg.Message
        };

        static Msgs.RclInterfaces.Log ToRos2(in LogMessage msg) => new()
        {
            Stamp = GameThread.TimeNow,
            Level = LogMessage.ToRos2(msg.Level),
            Name = RosManager.Connection.MyId ?? "/iviz",
            Msg = msg.Message
        };

        public void Dispose()
        {
            Listener.Dispose();
            Sender.Dispose();

            MessageArrived = null;
            RosLogger.LogExternal -= Publish;
            RosConnection.RosVersionChanged -= UpdateRosVersion;
        }
    }
}