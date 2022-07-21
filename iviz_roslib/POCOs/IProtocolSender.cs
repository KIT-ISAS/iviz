using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib;

internal interface IProtocolSender<TMessage> where TMessage : IMessage
{
    bool IsAlive { get; }
    long MaxQueueSizeInBytes { set; }
    ValueTask DisposeAsync(CancellationToken token);
    void Publish(in TMessage msg);
    ValueTask PublishAndWaitAsync(in TMessage message, CancellationToken token);
    Ros1SenderState State { get; }
}