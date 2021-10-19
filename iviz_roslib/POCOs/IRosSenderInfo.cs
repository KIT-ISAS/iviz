using System.Collections.Generic;

namespace Iviz.Roslib
{
    /// <summary>
    /// Encapsulates information about the connection from which a message is sent 
    /// </summary>
    public interface IRosSenderInfo
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
}