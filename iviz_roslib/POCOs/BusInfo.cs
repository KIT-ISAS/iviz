using System;

namespace Iviz.Roslib
{
    class BusInfo : JsonToString
    {
        public int ConnectionId { get; }
        public Uri DestinationId { get; }
        public string Direction { get; }
        public string Transport { get; }
        public string Topic { get; }
        public int Connected { get; }

        public BusInfo(int id, Uri destinationId, string direction, string topic, bool status = true, string transport = "TCPROS")
        {
            ConnectionId = id;
            DestinationId = destinationId;
            Direction = direction;
            Transport = transport;
            Topic = topic;
            Connected = status ? 1 : 0;
        }
        
        public BusInfo(int id, string topic, SubscriberReceiverState receiver) : 
            this(id, receiver.RemoteUri, "i", topic, status: receiver.IsAlive) {}
        
    }
}
