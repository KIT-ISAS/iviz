using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib
{
    /// <summary>
    /// Full info about a ROS service and its service type, including dependencies.
    /// </summary>
    [DataContract]
    internal sealed class ServiceInfo<T> : JsonToString where T : IService
    {
        readonly T? generator;

        /// <summary>
        /// ROS name of this node.
        /// </summary>
        [DataMember] public string CallerId { get; }

        /// <summary>
        /// Name of this service.
        /// </summary>
        [DataMember] public string Service { get; }

        /// <summary>
        /// MD5 hash of the compact representation of the message.
        /// </summary>
        [DataMember] public string Md5Sum { get; }

        /// <summary>
        /// Full ROS message type.
        /// </summary>
        [DataMember] public string Type { get; }

        /// <summary>
        /// Instance of the message used to generate others of the same type.
        /// <seealso cref="IService.Create"/>
        /// </summary>
        public T Generator =>
            generator ?? throw new InvalidOperationException("This service does not have a generator!");

        ServiceInfo(string callerId, string topic, string md5Sum, string type, T? generator)
        {
            CallerId = callerId;
            Service = topic;
            Md5Sum = md5Sum;
            Type = type;
            this.generator = generator;
        }

        public ServiceInfo(string callerId, string service, T? generator = default)
            : this(
                callerId, service,
                BuiltIns.GetMd5Sum<T>(),
                BuiltIns.GetServiceType<T>(),
                generator
            )
        {
        }
    }
}