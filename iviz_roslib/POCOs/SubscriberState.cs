using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Iviz.Roslib
{
    public class SubscriberReceiverState
    {
        public bool Alive { get; }
        public bool RequestNoDelay { get; }
        public string Hostname { get; }
        public int Port { get; }
        public Uri RemoteUri { get; }
        public string RemoteHostname { get; }
        public int RemotePort { get; }
        public int NumReceived { get; }
        public int BytesReceived { get; }

        internal SubscriberReceiverState(bool alive, bool requestNoDelay,
            string hostname, int port,
            Uri remoteUri, string remoteHostname, int remotePort,
            int numReceived, int bytesReceived)
        {
            Alive = alive;
            RequestNoDelay = requestNoDelay;
            Hostname = hostname;
            Port = port;
            RemoteUri = remoteUri;
            RemoteHostname = remoteHostname;
            RemotePort = remotePort;
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
            TopicIds = new ReadOnlyCollection<string>(topicIds);
            Receivers = new ReadOnlyCollection<SubscriberReceiverState>(receivers);
        }
    }

    public class SubscriberState
    {
        public ReadOnlyCollection<SubscriberTopicState> Topics { get; }

        internal SubscriberState(SubscriberTopicState[] topics)
        {
            Topics = new ReadOnlyCollection<SubscriberTopicState>(topics);
        }
    }
}
