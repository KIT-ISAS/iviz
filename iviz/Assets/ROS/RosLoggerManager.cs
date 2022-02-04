#nullable enable

using Iviz.Core;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib;

namespace Iviz.Ros
{
    public sealed class RosLoggerManager
    {
        uint logSeq;

        public delegate void LogDelegate(in Log log);
        public event LogDelegate? MessageArrived;
        
        public LogLevel MinLogLevel { get; set; } = LogLevel.Info;
        public Listener<Log> Listener { get; }
        public Sender<Log> Sender { get; }
        
        public RosLoggerManager()
        {
            Sender = new Sender<Log>("/rosout");
            Listener = new Listener<Log>("/rosout_agg", Handler, RosTransportHint.PreferUdp);

            RosLogger.LogExternal += LogMessage;
        }
        
        bool Handler(Log msg)
        {
            MessageArrived?.Invoke(msg);
            return true;
        }

        void LogMessage(in LogMessage msg)
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
            RosLogger.LogExternal -= LogMessage;
        }
    }
}