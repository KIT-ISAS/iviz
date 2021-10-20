using System;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.Roslib
{
    internal interface IProtocolReceiver
    {
        bool IsAlive { get; }
        bool IsPaused { set; }
        bool IsConnected { get; }
        Task DisposeAsync(CancellationToken token);
        Endpoint Endpoint { get; }
        Uri RemoteUri { get; }
        SubscriberReceiverState State { get; }
        ReceiverStatus Status { get; }
        ErrorMessage? ErrorDescription { get; }
    }
}