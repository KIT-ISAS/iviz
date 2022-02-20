﻿using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib;

/// <summary>
/// Full info about a ROS service and its service type, including dependencies.
/// </summary>
[DataContract]
internal sealed class ServiceInfo : JsonToString
{
    readonly Func<IService>? generator;

    /// <summary>
    /// ROS name of this node.
    /// </summary>
    [DataMember]
    public string CallerId { get; }

    /// <summary>
    /// Name of this service.
    /// </summary>
    [DataMember]
    public string Service { get; }

    /// <summary>
    /// MD5 hash of the compact representation of the message.
    /// </summary>
    [DataMember]
    public string Md5Sum { get; }

    /// <summary>
    /// Full ROS message type.
    /// </summary>
    [DataMember]
    public string Type { get; }

    ServiceInfo(string callerId, string topic, string md5Sum, string type, Func<IService>? generator)
    {
        CallerId = callerId;
        Service = topic;
        Md5Sum = md5Sum;
        Type = type;
        this.generator = generator;
    }

    public static ServiceInfo Instantiate<T>(string callerId, string service, Func<IService>? generator = null)
        where T : IService =>
        new(
            callerId, service,
            BuiltIns.GetMd5Sum<T>(),
            BuiltIns.GetServiceType<T>(),
            generator
        );

    public IService Create() =>
        generator?.Invoke() ?? throw new InvalidOperationException("The generator has not been set");
}