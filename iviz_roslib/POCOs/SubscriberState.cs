using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib
{
    public sealed class SubscriberReceiverState : JsonToString
    {
        public bool IsAlive { get; internal set; }
        public bool IsConnected { get; internal set;}
        public bool RequestNoDelay { get; internal set;}
        public Endpoint? EndPoint { get; internal set;}
        public Uri RemoteUri { get; }
        public Endpoint? RemoteEndpoint { get; internal set;}
        public long NumReceived { get; internal set;}
        public long BytesReceived { get; internal set;}
        public string? ErrorDescription { get; internal set;}

        internal SubscriberReceiverState(Uri remoteUri)
        {
            RemoteUri = remoteUri;
        }
    }

    public sealed class SubscriberTopicState : JsonToString
    {
        public string Topic { get; }
        public string Type { get; }
        public ReadOnlyCollection<string> TopicIds { get; }
        public ReadOnlyCollection<SubscriberReceiverState> Receivers { get; }

        internal SubscriberTopicState(string topic, string type, IList<string> topicIds, IList<SubscriberReceiverState> receivers)
        {
            Topic = topic;
            Type = type;
            TopicIds = topicIds.AsReadOnly();
            Receivers = receivers.AsReadOnly();
        }
    }

    public sealed class SubscriberState
    {
        public ReadOnlyCollection<SubscriberTopicState> Topics { get; }

        internal SubscriberState(IList<SubscriberTopicState> topics)
        {
            Topics = topics.AsReadOnly();
        }
    }
}
