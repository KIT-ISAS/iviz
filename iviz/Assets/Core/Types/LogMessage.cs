#nullable enable

using System;
using System.Runtime.Serialization;

namespace Iviz.Core
{
    [DataContract]
    public readonly struct LogMessage
    {
        [DataMember] public readonly string? SourceId;
        [DataMember] public readonly string Message;
        [DataMember] public readonly DateTime Stamp;
        [DataMember] public readonly LogLevel Level;

        public LogMessage(LogLevel level, string message)
        {
            SourceId = null;
            Stamp = GameThread.Now;
            Level = level;
            Message = message;
        }

        public LogMessage(Msgs.RosgraphMsgs.Log msg)
        {
            SourceId = msg.Name;
            Stamp = msg.Header.Stamp.ToDateTime();
            Level = FromRos1(msg.Level);
            Message = msg.Msg ?? "";
        }
        
        public LogMessage(Msgs.RclInterfaces.Log msg)
        {
            SourceId = msg.Name;
            Stamp = msg.Stamp.ToDateTime();
            Level = FromRos2(msg.Level);
            Message = msg.Msg ?? "";
        }
        
        public static byte ToRos1(LogLevel level) => level switch
        {
            LogLevel.Debug => Msgs.RosgraphMsgs.Log.DEBUG,
            LogLevel.Info => Msgs.RosgraphMsgs.Log.INFO,
            LogLevel.Warn => Msgs.RosgraphMsgs.Log.WARN,
            LogLevel.Error => Msgs.RosgraphMsgs.Log.ERROR,
            LogLevel.Fatal => Msgs.RosgraphMsgs.Log.FATAL,
            _ => throw new IndexOutOfRangeException()
        };
        
        public static byte ToRos2(LogLevel level) => level switch
        {
            LogLevel.Debug => Msgs.RclInterfaces.Log.DEBUG,
            LogLevel.Info => Msgs.RclInterfaces.Log.INFO,
            LogLevel.Warn => Msgs.RclInterfaces.Log.WARN,
            LogLevel.Error => Msgs.RclInterfaces.Log.ERROR,
            LogLevel.Fatal => Msgs.RclInterfaces.Log.FATAL,
            _ => throw new IndexOutOfRangeException()
        };
        
        static LogLevel FromRos1(byte level) => level switch
        {
            <= Msgs.RosgraphMsgs.Log.DEBUG => LogLevel.Debug,
            <= Msgs.RosgraphMsgs.Log.INFO => LogLevel.Info,
            <= Msgs.RosgraphMsgs.Log.WARN => LogLevel.Warn, 
            <= Msgs.RosgraphMsgs.Log.ERROR => LogLevel.Error,
            _ => LogLevel.Fatal,
        };
        
        static LogLevel FromRos2(byte level) => level switch
        {
            <= Msgs.RclInterfaces.Log.DEBUG => LogLevel.Debug,
            <= Msgs.RclInterfaces.Log.INFO => LogLevel.Info,
            <= Msgs.RclInterfaces.Log.WARN => LogLevel.Warn, 
            <= Msgs.RclInterfaces.Log.ERROR => LogLevel.Error,
            _ => LogLevel.Fatal,
        };
    }
}