using Iviz.Msgs;

namespace Iviz.Roslib
{
    /// <summary>
    /// Full info about a ROS service and its service type, including dependencies.
    /// </summary>
    internal sealed class ServiceInfo<T> : JsonToString
    {
        /// <summary>
        /// ROS name of this node.
        /// </summary>
        public string CallerId { get; }

        /// <summary>
        /// Name of this service.
        /// </summary>
        public string Service { get; }

        /// <summary>
        /// MD5 hash of the compact representation of the message.
        /// </summary>
        public string Md5Sum { get; }

        /// <summary>
        /// Full ROS message type.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Instance of the message used to generate others of the same type.
        /// <seealso cref="IService.Create"/>
        /// </summary>
        public T Generator { get; }

        ServiceInfo(string callerId, string topic, string md5Sum, string type, T generator)
        {
            CallerId = callerId;
            Service = topic;
            Md5Sum = md5Sum;
            Type = type;
            Generator = generator;
        }

        public ServiceInfo(string callerId, string service, T generator = default)
        : this(
                callerId, service,
                BuiltIns.GetMd5Sum(typeof(T)),
                BuiltIns.GetServiceType(typeof(T)),
                generator
                )
        {
        }
    }
}
