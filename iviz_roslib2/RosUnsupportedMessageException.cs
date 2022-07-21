using Iviz.Roslib;

namespace Iviz.Roslib2;

public sealed class RosUnsupportedMessageException : RoslibException
{
    public RosUnsupportedMessageException(string messageType) : base($"Message type {messageType} is not supported")
    {
    }

}