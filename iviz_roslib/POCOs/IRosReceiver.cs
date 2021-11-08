using System;
using System.Net.Sockets;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    /// <summary>
    /// Encapsulates information about the connection from which a message originates.
    /// </summary>
    public interface IRosReceiver : IRosConnection
    {
        /// <summary>
        /// The ROS uri of the remote publisher
        /// </summary>
        Uri RemoteUri { get; }
        /// <summary>
        /// The IP address of the remote publisher.
        /// </summary>
        Endpoint RemoteEndpoint { get; }
        /// <summary>
        /// The own IP address of the receiver. 
        /// </summary>
        Endpoint Endpoint { get; }
    }
    
    public interface IUdpReceiver : IRosReceiver
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
    
    public interface ITcpReceiver : IRosReceiver
    {
        /// <summary>
        /// A direct reference to the socket.
        /// </summary>
        TcpClient? TcpClient { get; }
    }
}