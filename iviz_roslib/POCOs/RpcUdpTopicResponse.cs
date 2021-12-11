using System.Runtime.Serialization;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib.XmlRpc;

[DataContract]
public sealed class RpcUdpTopicResponse : JsonToString
{
    [DataMember] public string Hostname { get; }
    [DataMember] public int Port { get; }
    [DataMember] public int ConnectionId { get; }
    [DataMember] public int MaxPacketSize { get; }
    public byte[] Header { get; }

    internal RpcUdpTopicResponse(string hostname, int port, int connectionId, int maxPacketSize, byte[] header)
    {
        Hostname = hostname;
        Port = port;
        ConnectionId = connectionId;
        MaxPacketSize = maxPacketSize;
        Header = header;
    }

    public void Deconstruct(out string hostname, out int port, out int connectionId, out int maxPacketSize,
        out byte[] header)
        => (hostname, port, connectionId, maxPacketSize, header) =
            (Hostname, Port, ConnectionId, MaxPacketSize, Header);
}