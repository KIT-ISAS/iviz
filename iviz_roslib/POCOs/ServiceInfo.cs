using System;
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
    readonly Func<IService> generator;

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

    ServiceInfo(string callerId, string topic, string md5Sum, string type, Func<IService> generator)
    {
        CallerId = callerId;
        Service = topic;
        Md5Sum = md5Sum;
        Type = type;
        this.generator = generator;
    }

    public static ServiceInfo Instantiate<T>(string callerId, string service)
        where T : IService, new()
    {
        T generator = new T();
        return new ServiceInfo(
            callerId, service,
            generator.RosMd5Sum,
            generator.RosServiceType,
            () => new T()
        );
    }

    public IService Create() => generator();
}