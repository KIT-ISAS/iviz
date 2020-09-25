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

        public BusInfo(int id, Uri destinationId, string direction, string topic, string transport = "TCPROS", int status = 1)
        {
            ConnectionId = id;
            DestinationId = destinationId;
            Direction = direction;
            Transport = transport;
            Topic = topic;
            Connected = status;
        }
    }
}
