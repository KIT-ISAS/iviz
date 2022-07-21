using System;
using System.Net.Sockets;
using Iviz.Msgs;

namespace Iviz.Roslib;

/// <summary>
/// Encapsulates information about the connection from which a message originates.
/// </summary>
public interface IRosReceiver : IRosConnection
{
    string? RemoteId { get; }
    /// <summary>
    /// The IP address of the remote publisher.
    /// </summary>
    Endpoint RemoteEndpoint { get; }
    /// <summary>
    /// The own IP address of the receiver. 
    /// </summary>
    Endpoint Endpoint { get; }
}
