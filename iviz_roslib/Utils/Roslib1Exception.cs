using System;

namespace Iviz.Roslib;

public class Roslib1Exception : RoslibException
{
    public Roslib1Exception(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}

/// <summary>
/// Thrown when an error happened during the connection.
/// </summary>
public class RosConnectionException : Roslib1Exception
{
    public RosConnectionException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}

/// <summary>
/// Thrown when an error happened during the connection.
/// </summary>
public sealed class RosUriBindingException : RosConnectionException
{
    public RosUriBindingException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

/// <summary>
/// Thrown when the uri provided for the caller (this node) is not reachable.
/// </summary>
public sealed class RosUnreachableUriException : Roslib1Exception
{
    public RosUnreachableUriException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}

/// <summary>
/// Wrapper around a <see cref="Iviz.XmlRpc.XmlRpcException"/>
/// </summary>
public class RosRpcException : Roslib1Exception
{
    public RosRpcException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}

public class RosHandshakeException : RosRpcException
{
    public RosHandshakeException(string message, Exception? e = null) : base(message, e)
    {
    }
}

public sealed class RosServiceNotFoundException : Roslib1Exception
{
    public string ServiceName { get; }
    public string ErrorMessage { get; }

    public RosServiceNotFoundException(string service, string message, Exception? innerException = null)
        : base($"Failed to call service {service}. Reason: {message}", innerException)
    {
        ServiceName = service;
        ErrorMessage = message;
    }
}

public sealed class RosServiceCallFailed : Roslib1Exception
{
    public string ServiceName { get; }
    public string ServerMessage { get; }

    public RosServiceCallFailed(string service, string message) : base(
        $"Server failed to execute call to '{service}'. Reason given: {(message.Length == 0 ? "(empty)" : message)}")
    {
        ServiceName = service;
        ServerMessage = message;
    }
}

public sealed class RosInvalidPackageSizeException : Roslib1Exception
{
    public RosInvalidPackageSizeException(string message) : base(message)
    {
    }
}

public sealed class RosInvalidHeaderException : RosHandshakeException
{
    public RosInvalidHeaderException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}

public sealed class RosQueueOverflowException : RosQueueException
{
    public RosQueueOverflowException(string message) : base(message)
    {
    }
}

public class RosQueueException : Roslib1Exception
{
    public RosQueueException(string message, Exception? e = null) : base(message, e)
    {
    }
}

/// <summary>
/// Thrown when an error happened during the connection.
/// </summary>
public class RosServiceRequestTimeoutException : Roslib1Exception
{
    public RosServiceRequestTimeoutException(string message, Exception? innerException = null) :
        base(message, innerException)
    {
    }
}