using System.Runtime.Serialization;

namespace Iviz.Roslib;

/// <summary>
/// The type of transport protocol being used
/// </summary>
public enum TransportType
{
    Tcp,
    Udp
}

[DataContract]
public sealed class Ros1SenderState : SenderState
{
    [DataMember] public bool IsAlive { get; set; }
    [DataMember] public Endpoint Endpoint { get; set; }
    [DataMember] public Endpoint RemoteEndpoint { get; set; }
    [DataMember] public TransportType TransportType { get; set; }
    [DataMember] public int CurrentQueueSize { get; set; }
    [DataMember] public long MaxQueueSizeInBytes { get; set; }
    [DataMember] public long NumDropped { get; set; }
    [DataMember] public long BytesDropped { get; set; }
}