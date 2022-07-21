using System;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.Roslib;

internal interface IProtocolReceiver
{
    bool IsAlive { get; }
    bool IsPaused { set; }
    bool IsConnected { get; }
    ValueTask DisposeAsync(CancellationToken token);
    Endpoint Endpoint { get; }
    Uri RemoteUri { get; }
    Ros1ReceiverState State { get; }
    ReceiverStatus Status { get; }
    ErrorMessage? ErrorDescription { get; }
}