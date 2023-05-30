using Iviz.Roslib;
using Iviz.Roslib2.RclInterop;

namespace Iviz.Roslib2;

public class Roslib2Exception : RoslibException
{
    public Roslib2Exception(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}

public sealed class RosUnsupportedMessageException : Roslib2Exception
{
    public RosUnsupportedMessageException(string messageType) : base($"Message or service type {messageType} is not supported")
    {
    }
}

public sealed class RosRclException : Roslib2Exception
{
    public RosRclException(string message) : base(message)
    {
    }

    public RosRclException(string message, int ret) : base($"{message} [Error code: {(RclRet)ret}]")
    {
    }
}

public sealed class RosMissingRclWrapperException : Roslib2Exception
{
    public RosMissingRclWrapperException() : base("Rcl wrapper has not been set!")
    {
    }
}