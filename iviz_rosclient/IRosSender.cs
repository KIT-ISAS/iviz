using System.Collections.Generic;
using System.Net.Sockets;

namespace Iviz.Roslib;

/// <summary>
/// Encapsulates information about the connection from which a message is sent 
/// </summary>
public interface IRosSender
{
    string Topic { get; }
    string Type { get; }
    string? RemoteCallerId { get; }
    Endpoint RemoteEndpoint { get; }
    Endpoint Endpoint { get; }
    bool IsAlive { get; }
}
