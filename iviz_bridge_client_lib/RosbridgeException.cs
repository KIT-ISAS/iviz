using System;
using Iviz.Roslib;

namespace iviz_bridge_client_lib;

public class RosbridgeException : RoslibException
{
    public RosbridgeException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}

/// <summary>
/// Thrown when an error happened during the connection.
/// </summary>
public class RosConnectionException : RosbridgeException
{
    public RosConnectionException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}