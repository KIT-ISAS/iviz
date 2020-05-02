namespace Iviz.RoslibSharp
{
    class BusInfo : JsonToString
    {
        public int ConnectionId { get; }
        public string DestinationId { get; }
        public string Direction { get; }
        public string Transport { get; }
        public string Topic { get; }
        public int Connected { get; }

        public BusInfo(int id, string destinationId, string direction, string transport, string topic, int status)
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
