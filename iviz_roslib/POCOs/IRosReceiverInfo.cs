using System;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    /// <summary>
    /// Encapsulates information about the connection from which a message originates 
    /// </summary>
    public interface IRosReceiverInfo : IRosConnection
    {
        /// <summary>
        /// The ROS uri of the publisher
        /// </summary>
        Uri RemoteUri { get; }
        /// <summary>
        /// The IP address of the publisher.
        /// </summary>
        Endpoint RemoteEndpoint { get; }
        /// <summary>
        /// The IP address of the receiver. 
        /// </summary>
        Endpoint Endpoint { get; }
    }
}