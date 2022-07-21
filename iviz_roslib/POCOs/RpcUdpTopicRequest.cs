using System.Runtime.Serialization;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib.XmlRpc;

[DataContract]
internal sealed class RpcUdpTopicRequest : JsonToString
{
    public byte[] Header { get; }
    [DataMember] public string Hostname { get; }
    [DataMember] public int Port { get; }
    [DataMember] public int MaxPacketSize { get; }

    public RpcUdpTopicRequest(byte[] header, string hostname, int port, int maxPacketSize)
    {
        Header = header;
        Hostname = hostname;
        Port = port;
        MaxPacketSize = maxPacketSize;
    }

    public RpcUdpTopicRequest(byte[] header, string hostname, int maxPacketSize) : this(header, hostname, 0,
        maxPacketSize)
    {
    }

    public void Deconstruct(out byte[] header, out string hostName, out int port, out int maxPacketSize) =>
        (header, hostName, port, maxPacketSize) = (Header, Hostname, Port, MaxPacketSize);

    internal RpcUdpTopicRequest WithPort(int port) => new(Header, Hostname, port, MaxPacketSize);
}