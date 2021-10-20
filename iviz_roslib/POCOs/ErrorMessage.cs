using System;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib
{
    [DataContract]
    public sealed class ErrorMessage
    {
        [DataMember] public DateTime Time { get; }
        [DataMember] public string Message { get; }

        public ErrorMessage(string message) => (Message, Time) = (message, DateTime.Now);
        public void Deconstruct(out DateTime time, out string message) => (time, message) = (Time, Message);
        public override string ToString() => Message;
    }
}