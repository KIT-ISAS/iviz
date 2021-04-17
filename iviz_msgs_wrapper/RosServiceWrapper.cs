using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.MsgsWrapper
{
    /// <summary>
    /// Creates a ROS message out of a C# class.
    /// </summary>
    /// <typeparam name="T">The type around with to create the message.</typeparam>
    public abstract class RosServiceWrapper<T, TRequest, TResponse> : IService
        where T : RosServiceWrapper<T, TRequest, TResponse>, IService, new()
        where TRequest : RosRequestWrapper<T, TRequest, TResponse> , new()
        where TResponse : RosResponseWrapper<T, TRequest, TResponse>, new()
    {
        static RosServiceDefinition<T, TRequest, TResponse>? serviceDefinition;

        static RosServiceDefinition<T, TRequest, TResponse> ServiceDefinition =>
            serviceDefinition ??= new RosServiceDefinition<T, TRequest, TResponse>();


        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public static string RosMd5Sum => ServiceDefinition.RosMessageMd5;

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public static string RosDependenciesBase64 => ServiceDefinition.RosDependenciesBase64;

        /// <summary> Concatenated dependencies file. </summary>
        public static string RosDependencies => ServiceDefinition.RosDependencies;

        /// <summary>
        /// Creates a ROS definition for this message (the contents of a .msg file).
        /// </summary>
        [IgnoreDataMember]
        public static string RosDefinition => ServiceDefinition.RosDefinition;

        /// <summary> Request message. </summary>
        [DataMember]
        public TRequest Request { get; set; } = new();

        /// <summary> Response message. </summary>
        [DataMember]
        public TResponse Response { get; set; } = new();

        IService IService.Create() => new T();

        IRequest IService.Request
        {
            get => Request;
            set => Request = (TRequest) value;
        }

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

    internal sealed class RosServiceDefinition<T, TRequest, TResponse>
        where T : RosServiceWrapper<T, TRequest, TResponse>, IService, new()
        where TRequest : RosRequestWrapper<T, TRequest, TResponse> , new()
        where TResponse : RosResponseWrapper<T, TRequest, TResponse>, new()
    {
        public string RosType { get; }
        public string RosDefinition { get; }
        public string RosMessageMd5 { get; }
        public string RosDependencies { get; }
        public string RosDependenciesBase64 => RosWrapperBase.CompressDependencies(RosDependencies);

        public RosServiceDefinition()
        {
            try
            {
                RosType = BuiltIns.GetServiceType<T>();
            }
            catch (RosInvalidMessageException)
            {
                throw new RosIncompleteWrapperException(
                    $"Type '{typeof(T).Name}' must have a string constant named RosMessageType. " +
                    "It should also be tagged with the attribute [MessageName].");
            }
            
            const string serviceSeparator =
                "===";

            RosDefinition = RosRequestWrapper<T, TRequest, TResponse>.RosDefinition + "\n" +
                            serviceSeparator +
                            RosResponseWrapper<T, TRequest, TResponse>.RosDefinition + "\n";

            RosMessageMd5 = RosWrapperBase.CreateMd5(
                RosRequestWrapper<T, TRequest, TResponse>.RosInputForMd5 +
                RosResponseWrapper<T, TRequest, TResponse>.RosInputForMd5);

            var alreadyWritten = new HashSet<Type>();
            RosDependencies = RosDefinition +
                RosSerializableDefinition<TRequest>.CreatePartialDependencies(alreadyWritten) +
                RosSerializableDefinition<TResponse>.CreatePartialDependencies(alreadyWritten);
        }
    }
}