using System.Collections.Generic;

namespace Iviz.Roslib
{
    /// <summary>
    /// Encapsulates information about the connection from which a message is sent 
    /// </summary>
    public interface IRosTcpSender
    {
        string Topic { get; }
        string Type { get; }
        string? RemoteCallerId { get; }
        Endpoint RemoteEndpoint { get; }
        Endpoint Endpoint { get; }
        IReadOnlyCollection<string> TcpHeader { get; }
        PublisherSenderState State { get; }
        bool IsAlive { get; }
        SenderStatus Status { get; }
    }
}