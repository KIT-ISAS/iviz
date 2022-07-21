using System.Collections.Generic;
using System.Net.Sockets;

namespace Iviz.Roslib;

public interface IRos1Sender : IRosSender
{
    TransportType TransportType { get; }
    IReadOnlyList<string> RosHeader { get; }
    Ros1SenderState State { get; }
}

public interface IUdpSender : IRos1Sender
{
    UdpClient UdpClient { get; }
    int MaxPacketSize { get; }
}
    
public interface ITcpSender : IRos1Sender
{
    TcpClient TcpClient { get; }
}