#nullable enable

using Iviz.Core;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib;

namespace Iviz.Ros
{
    public sealed class RosLoggerManager
    {
        const string RosOutTopic = "/rosout";
        const string RosOutAggTopic = "/rosout_agg";

        uint logSeq;

        public delegate void LogDelegate(in Log log);
        public event LogDelegate? MessageArrived;
        
        public LogLevel MinLogLevel { get; set; } = LogLevel.Info;
        public Listener<Log> Listener { get; }
        public Sender<Log> Sender { get; }
        
        public RosLoggerManager()
        {
            Sender = new Sender<Log>(RosOutTopic);
            Listener = new Listener<Log>(RosOutAggTopic, Handle, RosTransportHint.PreferUdp);

            RosLogger.LogExternal += Publish;
        }
        
        bool Handle(Log msg)
        {
            MessageArrived?.Invoke(msg);
            return true;
        }

        void Publish(in LogMessage msg)
        {
            if (msg.Level < MinLogLevel)
            {
                return;
            }

            var logMessage = new Log
            {
                Header = new Header(logSeq++, GameThread.TimeNow, ""),
                Level = (byte)msg.Level,
                Name = RosManager.Connection.MyId ?? "/iviz",
                Msg = msg.Message
            };

            Sender.Publish(logMessage);
        }
        
        public void Dispose()
        {
            Listener.Dispose();
            Sender.Dispose();

            MessageArrived = null;
            RosLogger.LogExternal -= Publish;
        }
    }
}