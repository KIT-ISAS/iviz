using System;
using Iviz.Roslib.Utils;
using Iviz.Roslib.XmlRpc;

namespace Iviz.Roslib
{
    /// <summary>
    /// Class for generic information about a connection.
    /// </summary>
    internal sealed class BusInfo : JsonToString
    {
        public enum DirectionType
        {
            In,
            Out
        }

        public int ConnectionId { get; }
        public Uri DestinationId { get; }
        public DirectionType Direction { get; }
        public TransportType TransportType { get; }
        public string Topic { get; }
        public bool Connected { get; }

        public BusInfo(int id, Uri destinationId, DirectionType direction, string topic, TransportType transport,
            bool status = true)
        {
            ConnectionId = id;
            DestinationId = destinationId ?? throw new ArgumentNullException(nameof(destinationId));
            Direction = direction;
            TransportType = transport;
            Topic = topic;
            Connected = status;
        }

        public BusInfo(int id, string topic, SubscriberReceiverState receiver) :
            this(id, receiver.RemoteUri, DirectionType.In, topic,
                receiver.TransportType ?? throw new ArgumentException("Invalid receiver passed to busInfo!"),
                receiver.Status == ReceiverStatus.Connected)
        {
        }
    }
}