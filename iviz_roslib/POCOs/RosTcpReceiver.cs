using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib
{
    [DataContract]
    internal sealed class RosTcpReceiver : JsonToString, IRosTcpReceiver
    {
        [DataMember] public Uri RemoteUri { get; }
        [DataMember] public Endpoint RemoteEndpoint { get; }
        [DataMember] public Endpoint Endpoint { get; }
        [DataMember] public string Topic { get; }
        [DataMember] public string Type { get; }
        [DataMember] public IReadOnlyCollection<string> TcpHeader { get; }

        public RosTcpReceiver(Uri remoteUri, Endpoint remoteEndpoint, Endpoint endpoint, string topic, string type,
            IList<string> tcpHeader)
        {
            RemoteUri = remoteUri;
            RemoteEndpoint = remoteEndpoint;
            Endpoint = endpoint;
            Topic = topic;
            Type = type;
            TcpHeader = tcpHeader.AsReadOnly();
        }
    }
}