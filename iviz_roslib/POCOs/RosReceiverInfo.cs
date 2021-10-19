using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib
{
    [DataContract]
    internal sealed class RosReceiverInfo : JsonToString, IRosReceiverInfo
    {
        [DataMember] public Uri RemoteUri { get; }
        [DataMember] public Endpoint RemoteEndpoint { get; }
        [DataMember] public Endpoint Endpoint { get; }
        [DataMember] public string Topic { get; }
        [DataMember] public string Type { get; }
        [DataMember] public IReadOnlyCollection<string> RosHeader { get; }

        public RosReceiverInfo(Uri remoteUri, Endpoint remoteEndpoint, Endpoint endpoint, string topic, string type,
            IList<string> rosHeader)
        {
            RemoteUri = remoteUri;
            RemoteEndpoint = remoteEndpoint;
            Endpoint = endpoint;
            Topic = topic;
            Type = type;
            RosHeader = rosHeader.AsReadOnly();
        }
    }
}