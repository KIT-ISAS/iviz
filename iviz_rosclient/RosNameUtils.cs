using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Iviz.Tools;

namespace Iviz.Roslib;

public static class RosNameUtils
{
    public static string? EnvironmentRosNamespace
    {
        get
        {
            string? ns = Environment.GetEnvironmentVariable("ROS_NAMESPACE");
            if (ns.IsNullOrEmpty())
            {
                return null;
            }

            return !IsValidResourceName(ns) ? null : ns;
        }
    }

    public static string CreateOwnIdFrom(string namespacePrefix, string? ownId)
    {
        if (string.IsNullOrWhiteSpace(ownId))
        {
            ownId = RosNameUtils.CreateCallerId();
        }

        if (ownId[0] != '/')
        {
            ownId = $"{namespacePrefix}{ownId}";
        }

        if (ownId[0] == '~')
        {
            throw new RosInvalidResourceNameException("ROS node names may not start with a '~'");
        }

        RosNameUtils.ValidateResourceName(ownId);

        return ownId;        
    }    
    
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

        for (int i = 1; i < name.Length; i++)
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
            ThrowInvalidResourceName("Resource name is empty");
        }

        char c0 = name[0];
        if (!char.IsLetter(c0) && c0 is not ('/' or '~'))
        {
            ThrowInvalidResourceName(
                $"Resource name '{name}' is not valid. It must start with an alphanumeric character, " +
                $"'/' or '~'. Current start is '{c0}'");
        }

        for (int i = 1; i < name.Length; i++)
        {
            char c = name[i];
            if (!char.IsLetterOrDigit(c) && c is not ('_' or '/'))
            {
                ThrowInvalidResourceName(
                    $"Resource name '{name}' is not valid. It must only contain alphanumeric characters, " +
                    $"'/' or '_'. Character at position {i} is '{c}'");
            }
        }
    }
    
    [DoesNotReturn]
    static void ThrowInvalidResourceName(string message) => throw new RosInvalidResourceNameException(message);
}

