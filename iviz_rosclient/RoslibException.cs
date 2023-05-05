using System;
using Iviz.Msgs;

namespace Iviz.Roslib;

/// <summary>
/// Parent class for the exceptions of this library.
/// </summary>
public class RoslibException : RosException
{
    protected RoslibException(string message) : base(message)
    {
    }

    public RoslibException(string message, Exception? innerException) : base(message, innerException)
    {
    }
}

/// <summary>
/// Thrown when the provided message type is not correct.
/// </summary>
public sealed class RosInvalidMessageTypeException : RoslibException
{
    public RosInvalidMessageTypeException(string message) : base(message)
    {
    }

    public RosInvalidMessageTypeException(string topic, string baseType, string newType)
        : base($"There is already an entity for '{topic}' with a different type [{baseType}] - " +
               $"requested type was [{newType}]")
    {
    }
}

public sealed class RosInvalidResourceNameException : RoslibException
{
    public RosInvalidResourceNameException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public RosInvalidResourceNameException(string message) : base(message)
    {
    }
}

public class RosDynamicMessageException : RoslibException
{
    public RosDynamicMessageException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public RosDynamicMessageException(string message) : base(message)
    {
    }
}

public sealed class RosParameterNotFoundException : RoslibException
{
    public RosParameterNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public RosParameterNotFoundException() : base("Parameter not found")
    {
    }
}