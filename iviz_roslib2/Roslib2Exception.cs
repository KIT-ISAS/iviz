using Iviz.Roslib;
using Iviz.Roslib2.Rcl;

namespace Iviz.Roslib2;

public class Roslib2Exception : RoslibException
{
    protected Roslib2Exception(string message) : base(message)
    {
    }

    public Roslib2Exception(string message, Exception innerException) : base(message, innerException)
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