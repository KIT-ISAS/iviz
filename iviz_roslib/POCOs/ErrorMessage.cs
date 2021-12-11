using System;
using System.Runtime.Serialization;
using Iviz.Tools;

namespace Iviz.Roslib;

[DataContract]
public sealed class ErrorMessage
{
    [DataMember] public DateTime Time { get; }
    [DataMember] public string Message { get; }

    public ErrorMessage(string message)
    {
        Time = DateTime.Now;
        Message = message;
    } 

    public ErrorMessage(Exception e)
    {
        Time = DateTime.Now;
        Message = e.CheckMessage();
    } 

    public void Deconstruct(out DateTime time, out string message) => (time, message) = (Time, Message);
    public override string ToString() => Message;
}