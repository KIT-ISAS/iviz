using System;
using Iviz.Roslib;

namespace Iviz.Bridge.Client;

public class RosbridgeException : RoslibException
{
    public RosbridgeException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}

/// <summary>
/// Thrown when an error happened during the connection.
/// </summary>
public class RosbridgeConnectionException : RosbridgeException
{
    public RosbridgeConnectionException(string message, Exception? innerException = null) : base(message,
        innerException)
    {
    }
}