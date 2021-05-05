using System;
using System.Collections.Generic;
using Iviz.Msgs;

namespace Iviz.MsgsWrapper
{
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
            
            const string serviceSeparator = "---";

            RosDefinition = RosRequestWrapper<T, TRequest, TResponse>.RosDefinition + "\n" +
                            serviceSeparator + "\n" +
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