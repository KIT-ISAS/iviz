using System;
using System.Diagnostics.CodeAnalysis;
using Iviz.Msgs;

namespace Iviz.Roslib;

/// <summary>
/// Parent class for the exceptions of this library.
/// </summary>
public class RoslibException : RosException
{
    protected RoslibException(string message, Exception? e = null) : base(message, e)
    {
    }
}

/// <summary>
/// Thrown when the provided message type is not correct.
/// </summary>
public sealed class RosInvalidMessageTypeException : RoslibException
{
    public RosInvalidMessageTypeException(string message, Exception? e = null) : base(message, e)
    {
    }

    public RosInvalidMessageTypeException(string topic, string baseType, string newType)
        : base($"There is already an entity for '{topic}' with a different type [{baseType}] - " +
               $"requested type was [{newType}]")
    {
    }
}

public static class RosExceptionUtils
{
    [DoesNotReturn]
    public static void ThrowInvalidMessageType(string expectedType, IMessage wrongType, Exception? e = null) =>
        throw new RosInvalidMessageTypeException("Message does not match the expected type. " +
                                                 $"Expected [{expectedType}], received [{wrongType.RosMessageType}] instead",
            e);
}

public sealed class RosInvalidResourceNameException : RoslibException
{
    public RosInvalidResourceNameException(string message, Exception? e = null) : base(message, e)
    {
    }
}

public sealed class RosDynamicMessageException : RoslibException
{
    public RosDynamicMessageException(string message, Exception e) : base(message, e)
    {
    }

    public RosDynamicMessageException(string message) : base(message)
    {
    }
}

public sealed class RosParameterNotFoundException : RoslibException
{
    public RosParameterNotFoundException(string message, Exception? e = null) : base(message, e)
    {
    }

    public RosParameterNotFoundException() : base("Parameter not found")
    {
    }
}