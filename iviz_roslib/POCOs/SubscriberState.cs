using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib
{
    [DataContract]
    public sealed class SubscriberReceiverState : JsonToString
    {
        [DataMember] public TransportType? TransportType { get; internal set; }
        [DataMember] public ReceiverStatus Status { get; internal set; }
        [DataMember] public bool RequestNoDelay { get; internal set; }
        [DataMember] public Endpoint? EndPoint { get; internal set; }
        [DataMember] public Uri RemoteUri { get; }
        [DataMember] public Endpoint? RemoteEndpoint { get; internal set; }
        [DataMember] public long NumReceived { get; internal set; }
        [DataMember] public long NumDropped { get; internal set; }
        [DataMember] public long BytesReceived { get; internal set; }
        [DataMember] public ErrorMessage? ErrorDescription { get; internal set; }

        public bool IsAlive => Status == ReceiverStatus.Connected;

        internal SubscriberReceiverState(Uri remoteUri)
        {
            RemoteUri = remoteUri;
        }
    }

    [DataContract]
    public sealed class SubscriberTopicState : JsonToString
    {
        [DataMember] public string Topic { get; }
        [DataMember] public string Type { get; }
        [DataMember] public ReadOnlyCollection<string> SubscriberIds { get; }
        [DataMember] public ReadOnlyCollection<SubscriberReceiverState> Receivers { get; }

        internal SubscriberTopicState(string topic, string type, IList<string> topicIds,
            IList<SubscriberReceiverState> receivers)
        {
            Topic = topic;
            Type = type;
            SubscriberIds = topicIds.AsReadOnly();
            Receivers = receivers.AsReadOnly();
        }

        public void Deconstruct(out string topic, out string type, out ReadOnlyCollection<string> subscriberIds,
            out ReadOnlyCollection<SubscriberReceiverState> receivers)
            => (topic, type, subscriberIds, receivers) = (Topic, Type, SubscriberIds, Receivers);
    }

    [DataContract]
    public sealed class SubscriberState
    {
        [DataMember] public ReadOnlyCollection<SubscriberTopicState> Topics { get; }

        internal SubscriberState(IList<SubscriberTopicState> topics)
        {
            Topics = topics.AsReadOnly();
        }
    }
}