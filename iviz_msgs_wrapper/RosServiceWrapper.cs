using System.Runtime.Serialization;
using Iviz.Msgs;

namespace Iviz.MsgsWrapper
{
    /// <summary>
    /// Creates a ROS service out of a C# class.
    /// </summary>
    /// <typeparam name="T">The type around with to create the service.</typeparam>
    /// <typeparam name="TRequest">The request type associated to the service.</typeparam>
    /// <typeparam name="TResponse">The response type associated to the service.</typeparam>
    public abstract class RosServiceWrapper<T, TRequest, TResponse> : IService
        where T : RosServiceWrapper<T, TRequest, TResponse>, IService, new()
        where TRequest : RosRequestWrapper<T, TRequest, TResponse> , new()
        where TResponse : RosResponseWrapper<T, TRequest, TResponse>, new()
    {
        static RosServiceDefinition<T, TRequest, TResponse>? serviceDefinition;

        static RosServiceDefinition<T, TRequest, TResponse> ServiceDefinition =>
            serviceDefinition ??= new RosServiceDefinition<T, TRequest, TResponse>();
        
        /// <summary> MD5 hash of a compact representation of the service. </summary>
        [Preserve]
        public static string RosMd5Sum => ServiceDefinition.RosMessageMd5;

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public static string RosDependenciesBase64 => ServiceDefinition.RosDependenciesBase64;

        /// <summary> Concatenated dependencies file. </summary>
        public static string RosDependencies => ServiceDefinition.RosDependencies;

        /// <summary>
        /// Creates a ROS definition for this message (the contents of a .srv file).
        /// </summary>
        [IgnoreDataMember]
        public static string RosDefinition => ServiceDefinition.RosDefinition;

        /// <summary> Request message. </summary>
        [DataMember]
        public TRequest Request { get; set; } = new();

        /// <summary> Response message. </summary>
        [DataMember]
        public TResponse Response { get; set; } = new();

        [IgnoreDataMember]
        IRequest IService.Request
        {
            get => Request;
            set => Request = (TRequest) value;
        }

        [IgnoreDataMember]
        IResponse IService.Response
        {
            get => Response;
            set => Response = (TResponse) value;
        }

        /// <summary>
        /// Alias for the name of the message.
        /// </summary>
        [IgnoreDataMember]
        string IService.RosType => ServiceDefinition.RosType;
    }
}