using System;
using System.Runtime.Serialization;

namespace Iviz.Roslib
{
    [DataContract]
    public sealed class ErrorMessage
    {
        [DataMember] public DateTime Time { get; }
        [DataMember] public string Message { get; }

        public ErrorMessage(string message)
        {
            Time = DateTime.Now;
            
            // check for weird mono bug
            int terminatorIndex = message.IndexOf('\r'); 
            Message = terminatorIndex != -1 ? message.Substring(0, terminatorIndex) : message;
            
        } 
        public void Deconstruct(out DateTime time, out string message) => (time, message) = (Time, Message);
        public override string ToString() => Message;
    }
}