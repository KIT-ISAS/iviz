using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Iviz.Roslib
{
    public class SubscriberReceiverState
    {
        public bool Alive { get; }
        public bool RequestNoDelay { get; }
        public Endpoint EndPoint { get; }
        public Uri RemoteUri { get; }
        public Endpoint RemoteEndpoint { get; }
        public int NumReceived { get; }
        public int BytesReceived { get; }

        internal SubscriberReceiverState(bool alive, bool requestNoDelay,
            Endpoint endPoint,
            Uri remoteUri, Endpoint remoteEndpoint,
            int numReceived, int bytesReceived)
        {
            Alive = alive;
            RequestNoDelay = requestNoDelay;
            EndPoint = endPoint; 
            RemoteUri = remoteUri;
            RemoteEndpoint = remoteEndpoint;
            NumReceived = numReceived;
            BytesReceived = bytesReceived;
        }

    }

    public class SubscriberTopicState
    {
        public string Topic { get; }
        public string Type { get; }
        public ReadOnlyCollection<string> TopicIds { get; }
        public ReadOnlyCollection<SubscriberReceiverState> Receivers { get; }

        public SubscriberTopicState(string topic, string type, IList<string> topicIds, IList<SubscriberReceiverState> receivers)
        {
            Topic = topic;
            Type = type;
            TopicIds = topicIds.AsReadOnly();
            Receivers = receivers.AsReadOnly();
        }
    }

    public class SubscriberState
    {
        public ReadOnlyCollection<SubscriberTopicState> Topics { get; }

        internal SubscriberState(IList<SubscriberTopicState> topics)
        {
            Topics = topics.AsReadOnly();
        }
    }
}
