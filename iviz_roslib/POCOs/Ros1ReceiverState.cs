using System;
using System.Runtime.Serialization;

namespace Iviz.Roslib;

[DataContract]
public abstract class Ros1ReceiverState : ReceiverState
{
    [DataMember] public bool IsAlive { get; set; }
    [DataMember] public Endpoint EndPoint { get; set; }
    [DataMember] public Endpoint RemoteEndpoint { get;set; }
    [DataMember] public Uri RemoteUri { get; }
    [DataMember] public abstract TransportType? TransportType { get; }
    [DataMember] public ReceiverStatus Status { get; set; }
    [DataMember] public long NumDropped { get;set; }
    [DataMember] public ErrorMessage? ErrorDescription { get; set; }

    protected Ros1ReceiverState(Uri remoteUri)
    {
        RemoteUri = remoteUri;
    }
}

[DataContract]
public sealed class TcpReceiverState : Ros1ReceiverState
{
    [DataMember] public override TransportType? TransportType => Roslib.TransportType.Tcp;
    [DataMember] public bool RequestNoDelay { get; set; }

    public TcpReceiverState(Uri remoteUri) : base(remoteUri)
    {
    }
}

[DataContract]
public sealed class UdpReceiverState : Ros1ReceiverState
{
    [DataMember] public override TransportType? TransportType => Roslib.TransportType.Udp;
    [DataMember] public int MaxPacketSize { get; set; }

    public UdpReceiverState(Uri remoteUri) : base(remoteUri)
    {
    }
}

[DataContract]
public sealed class UninitializedReceiverState : Ros1ReceiverState
{
    [DataMember] public override TransportType? TransportType => null;

    public UninitializedReceiverState(Uri remoteUri) : base(remoteUri)
    {
    }
}
