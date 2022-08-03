using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Iviz.Roslib;

public interface IRos1Receiver : IRosReceiver
{
    /// <summary>
    /// The ROS uri of the remote publisher
    /// </summary>
    Uri RemoteUri { get; }
}

public interface IUdpReceiver : IRos1Receiver
{
    /// <summary>
    /// A direct reference to the socket.
    /// </summary>
    UdpClient UdpClient { get; }
        
    /// <summary>
    /// The maximum packet size negotiated during handshake.
    /// </summary>
    int MaxPacketSize { get; }
}
    
public interface ITcpReceiver : IRos1Receiver
{
    /// <summary>
    /// A direct reference to the socket.
    /// </summary>
    TcpClient? TcpClient { get; }
}