using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Iviz.Tools;

namespace Iviz.Roslib;

public static class RosNameUtils
{
    /// <summary>
    /// Retrieves the environment variable ROS_HOSTNAME as a uri.
    /// If this fails, retrieves ROS_IP.
    /// </summary>
    public static string? EnvironmentCallerHostname =>
        Environment.GetEnvironmentVariable("ROS_HOSTNAME") ??
        Environment.GetEnvironmentVariable("ROS_IP");

    /// <summary>
    /// Creates a unique id based on the project and computer name
    /// </summary>
    /// <returns>A more or less unique id</returns>
    public static string CreateCallerId()
    {
        string seed = EnvironmentCallerHostname + Assembly.GetCallingAssembly().GetName().Name;
        return $"iviz_{seed.GetDeterministicHashCode().ToString("x8")}";
    }

    /// <summary>
    /// Checks if the given name is a valid ROS resource name
    /// </summary>  
    public static bool IsValidResourceName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        char c0 = name[0];
        if (c0 is not ('/' or '~') && !char.IsLetter(c0))
        {
            return false;
        }

        foreach (int i in 1..name.Length)
        {
            char c = name[i];
            if (!char.IsLetterOrDigit(c) && c is not ('_' or '/'))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Checks if the given name is a valid ROS resource name, and throws an exception with an error message if not.
    /// </summary>  
    public static void ValidateResourceName([NotNull] string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new RosInvalidResourceNameException("Resource name is empty");
        }

        char c0 = name[0];
        if (!char.IsLetter(c0) && c0 is not ('/' or '~'))
        {
            throw new RosInvalidResourceNameException(
                $"Resource name '{name}' is not valid. It must start with an alphanumeric character, " +
                $"'/' or '~'. Current start is '{c0}'");
        }

        foreach (int i in 1..name.Length)
        {
            char c = name[i];
            if (!char.IsLetterOrDigit(c) && c is not ('_' or '/'))
            {
                throw new RosInvalidResourceNameException(
                    $"Resource name '{name}' is not valid. It must only contain alphanumeric characters, " +
                    $"'/' or '_'. Character at position {i} is '{c}'");
            }
        }
    }
}

public static class RosExceptionUtils
{
    [DoesNotReturn]
    public static void ThrowInvalidMessageType() =>
        throw new RosInvalidMessageTypeException("Message does not match the expected type");
}