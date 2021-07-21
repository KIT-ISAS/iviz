using System;
using System.Runtime.Serialization;
using Iviz.Msgs.RosgraphMsgs;
using JetBrains.Annotations;

namespace Iviz.Core
{
    [DataContract]
    public readonly struct LogMessage
    {
        [CanBeNull, DataMember] public string SourceId { get; }
        [DataMember] public DateTime Stamp { get; }
        [DataMember] public LogLevel Level { get; }
        [NotNull, DataMember] public string Message { get; }

        public LogMessage(LogLevel level, [NotNull] string message)
        {
            SourceId = null;
            Stamp = GameThread.Now;
            Level = level;
            Message = message;
        }

        public LogMessage(in Log msg)
        {
            SourceId = msg.Name;
            Stamp = msg.Header.Stamp.ToDateTime();
            Level = (LogLevel) msg.Level;
            Message = msg.Msg ?? "";
        }
    }
}