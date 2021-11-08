using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    internal interface IProtocolSender<TMessage> where TMessage : IMessage
    {
        bool IsAlive { get; }
        long MaxQueueSizeInBytes { set; }
        Task DisposeAsync(CancellationToken token);
        void Publish(in TMessage msg);
        Task PublishAndWaitAsync(in TMessage message, CancellationToken token);
        PublisherSenderState State { get; }
    }
}