using System;

namespace Iviz.Roslib;

public class Roslib1Exception : RoslibException
{
    protected Roslib1Exception(string message) : base(message)
    {
    }

    public Roslib1Exception(string message, Exception innerException) : base(message, innerException)
    {
    }
}

/// <summary>
/// Thrown when an error happened during the connection.
/// </summary>
public class RosConnectionException : Roslib1Exception
{
    public RosConnectionException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public RosConnectionException(string message) : base(message)
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

    public RosUriBindingException(string message) : base(message)
    {
    }
}

/// <summary>
/// Thrown when the uri provided for the caller (this node) is not reachable.
/// </summary>
public sealed class RosUnreachableUriException : Roslib1Exception
{
    public RosUnreachableUriException(string message) : base(message)
    {
    }

    public RosUnreachableUriException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

/// <summary>
/// Wrapper around a <see cref="Iviz.XmlRpc.XmlRpcException"/>
/// </summary>
public class RosRpcException : Roslib1Exception
{
    public RosRpcException(string message) : base(message)
    {
    }

    public RosRpcException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class RosHandshakeException : RosRpcException
{
    public RosHandshakeException(string message) : base(message)
    {
    }

    public RosHandshakeException(string message, Exception e) : base(message, e)
    {
    }
}

public sealed class RosServiceNotFoundException : Roslib1Exception
{
    public string ServiceName { get; }
    public string ErrorMessage { get; }

    public RosServiceNotFoundException(string service, string message)
        : base($"Failed to call service {service}. Reason: {message}")
    {
        ServiceName = service;
        ErrorMessage = message;
    }

    public RosServiceNotFoundException(string service, string message, Exception innerException)
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
    public RosInvalidHeaderException(string message) : base(message)
    {
    }

    public RosInvalidHeaderException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public sealed class RosQueueOverflowException : RosQueueException
{
    public RosQueueOverflowException(string message, IRosSender sender) : base(message, sender)
    {
    }
}

public class RosQueueException : Roslib1Exception
{
    public IRosSender Sender { get; }

    public RosQueueException(string message, IRosSender sender) : base(message)
    {
        Sender = sender;
    }

    public RosQueueException(string message, Exception innerException, IRosSender sender) : base(message,
        innerException)
    {
        Sender = sender;
    }
}

/// <summary>
/// Thrown when an error happened during the connection.
/// </summary>
public class RosServiceRequestTimeoutException : Roslib1Exception
{
    public RosServiceRequestTimeoutException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public RosServiceRequestTimeoutException(string message) : base(message)
    {
    }
}