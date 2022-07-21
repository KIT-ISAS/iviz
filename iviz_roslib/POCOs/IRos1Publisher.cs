using Iviz.Roslib.XmlRpc;

namespace Iviz.Roslib;

public interface IRos1Publisher : IRosPublisher
{
    /// <summary>
    /// Timeout in milliseconds to wait for a subscriber handshake.
    /// </summary>             
    public int TimeoutInMs { get; set; }
    
    /// <summary>
    /// Whether to force TCP_NODELAY. Usually, it is the job of the subscriber to request this flag.
    /// When enabling this, the flag is always set regardless of the subscriber request.
    /// </summary>
    public bool ForceTcpNoDelay { get; set; }

    internal TopicRequestRpcResult RequestTopicRpc(bool requestsTcp, RpcUdpTopicRequest? requestsUdp,
        out Endpoint? tcpResponse, out RpcUdpTopicResponse? udpResponse);
}