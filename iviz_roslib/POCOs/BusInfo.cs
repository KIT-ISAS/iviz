using System;

namespace Iviz.Roslib
{
    /// <summary>
    /// Class for generic information about a connection.
    /// </summary>
    internal sealed class BusInfo : JsonToString
    {
        public enum DirectionType
        {
            In, Out
        }
        
        public int ConnectionId { get; }
        public Uri DestinationId { get; }
        public DirectionType Direction { get; }
        public string Transport { get; }
        public string Topic { get; }
        public bool Connected { get; }

        public BusInfo(int id, Uri? destinationId, DirectionType direction, string topic, bool status = true, string transport = "TCPROS")
        {
            ConnectionId = id;
            DestinationId = destinationId ?? throw new ArgumentNullException(nameof(destinationId));
            Direction = direction;
            Transport = transport;
            Topic = topic;
            Connected = status;
        }
        
        public BusInfo(int id, string topic, SubscriberReceiverState receiver) : 
            this(id, receiver.RemoteUri, DirectionType.In, topic, receiver.IsAlive) {}
        
    }
}
