using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib
{
    [DataContract]
    internal sealed class RosTcpSender : JsonToString, IRosTcpSender
    {
        [DataMember] public string Topic { get; }
        [DataMember] public string Type { get; }
        [DataMember] public string? RemoteCallerId { get; }
        [DataMember] public Endpoint RemoteEndpoint { get; }
        [DataMember] public Endpoint Endpoint { get; }
        [DataMember] public IReadOnlyCollection<string> TcpHeader { get; }
        [DataMember] public PublisherSenderState State { get; }
        [DataMember] public bool IsAlive { get; }
        [DataMember] public SenderStatus Status { get; }

        public RosTcpSender(string topic, string type, string? remoteCallerId, Endpoint remoteEndpoint,
            Endpoint endpoint, IReadOnlyCollection<string> tcpHeader, PublisherSenderState state, bool isAlive,
            SenderStatus status)
        {
            Topic = topic;
            Type = type;
            RemoteCallerId = remoteCallerId;
            RemoteEndpoint = remoteEndpoint;
            Endpoint = endpoint;
            TcpHeader = tcpHeader;
            State = state;
            IsAlive = isAlive;
            Status = status;
        }
    }
}