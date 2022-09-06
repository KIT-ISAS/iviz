using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib;

internal abstract class IProtocolSender<TMessage> where TMessage : IMessage
{
    public abstract bool IsAlive { get; }
    public abstract long MaxQueueSizeInBytes { get; set; }
    public abstract ValueTask DisposeAsync(CancellationToken token);
    public abstract void Publish(in TMessage msg);
    public abstract ValueTask PublishAndWaitAsync(in TMessage message, CancellationToken token);
    public abstract Ros1SenderState State { get; }
}