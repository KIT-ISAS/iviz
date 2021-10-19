using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib
{
    [DataContract]
    internal sealed class RosSenderInfo : JsonToString, IRosSenderInfo
    {
        [DataMember] public string Topic { get; }
        [DataMember] public TransportType TransportType { get; }
        [DataMember] public string Type { get; }
        [DataMember] public string? RemoteCallerId { get; }
        [DataMember] public Endpoint RemoteEndpoint { get; }
        [DataMember] public Endpoint Endpoint { get; }
        [DataMember] public IReadOnlyCollection<string> RosHeader { get; }
        [DataMember] public PublisherSenderState State { get; }
        [DataMember] public bool IsAlive { get; }

        public RosSenderInfo(string topic, string type, TransportType transportType, string? remoteCallerId,
            Endpoint remoteEndpoint, Endpoint endpoint, IReadOnlyCollection<string> rosHeader,
            PublisherSenderState state, bool isAlive)
        {
            Topic = topic;
            Type = type;
            TransportType = transportType;
            RemoteCallerId = remoteCallerId;
            RemoteEndpoint = remoteEndpoint;
            Endpoint = endpoint;
            RosHeader = rosHeader;
            State = state;
            IsAlive = isAlive;
        }
    }
}