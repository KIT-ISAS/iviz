#nullable enable

using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib;
using UnityEngine.PlayerLoop;

namespace Iviz.Ros
{
    public sealed class RosOutLogger
    {
        const string RosOutTopic = "/rosout";
        const string RosOutAggTopic = "/rosout_agg";

        static uint logSeq;
        RosVersion currentVersion;

        public delegate void LogDelegate(in LogMessage log);

        public static event LogDelegate? MessageArrived;
        
        public LogLevel MinLogLevel { get; set; } = LogLevel.Info;
        public IListener Listener { get; private set; }
        public ISender Sender { get; private set; }

        public RosOutLogger()
        {
            (Sender, Listener) = CreateSenderAndListener(RosManager.Connection.RosVersion);
            currentVersion = RosManager.Connection.RosVersion;
            
            RoslibConnection.RosVersionChanged += UpdateRosVersion;
            RosLogger.LogExternal += Publish;
        }

        void UpdateRosVersion(RosVersion version)
        {
            Sender.Dispose();
            Listener.Dispose();
            (Sender, Listener) = CreateSenderAndListener(RosManager.Connection.RosVersion);
            currentVersion = version;
        }

        static (ISender, IListener) CreateSenderAndListener(RosVersion version)
        {
            return (
                version == RosVersion.ROS1
                    ? new Sender<Msgs.RosgraphMsgs.Log>(RosOutTopic)
                    : new Sender<Msgs.RclInterfaces.Log>(RosOutTopic),
                version == RosVersion.ROS1
                    ? new Listener<Msgs.RosgraphMsgs.Log>(RosOutAggTopic, Handle, RosTransportHint.PreferUdp)
                    : new Listener<Msgs.RclInterfaces.Log>(RosOutTopic, Handle, RosTransportHint.PreferUdp)
            );
        }

        static bool Handle(Msgs.RosgraphMsgs.Log msg)
        {
            MessageArrived?.Invoke(new LogMessage(msg));
            return true;
        }

        static bool Handle(Msgs.RclInterfaces.Log msg)
        {
            MessageArrived?.Invoke(new LogMessage(msg));
            return true;
        }

        void Publish(in LogMessage msg)
        {
            if (msg.Level < MinLogLevel)
            {
                return;
            }

            if (currentVersion == RosVersion.ROS1)
            {
                ((Sender<Msgs.RosgraphMsgs.Log>)Sender).Publish(ToRos1(msg));
            }
            else
            {
                ((Sender<Msgs.RclInterfaces.Log>)Sender).Publish(ToRos2(msg));
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
            RoslibConnection.RosVersionChanged -= UpdateRosVersion;
        }
    }
}