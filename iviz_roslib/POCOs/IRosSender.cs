using System.Collections.Generic;
using System.Net.Sockets;

namespace Iviz.Roslib
{
    /// <summary>
    /// Encapsulates information about the connection from which a message is sent 
    /// </summary>
    public interface IRosSender
    {
        string Topic { get; }
        string Type { get; }
        TransportType TransportType { get; }
        string? RemoteCallerId { get; }
        Endpoint RemoteEndpoint { get; }
        Endpoint Endpoint { get; }
        IReadOnlyCollection<string> RosHeader { get; }
        PublisherSenderState State { get; }
        bool IsAlive { get; }
    }

    public interface IUdpSender : IRosSender
    {
        UdpClient UdpClient { get; }
        int MaxPacketSize { get; }
    }
    
    public interface ITcpSender : IRosSender
    {
        TcpClient TcpClient { get; }
    }
}