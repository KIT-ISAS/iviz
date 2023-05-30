#nullable enable

using System.Threading;
using System.Threading.Tasks;
using Iviz.Roslib;

namespace Iviz.Ros
{
    internal interface ISubscribedTopic
    {
        IRosSubscriber? Subscriber { get; }
        int Count { get; }
        void Add(Listener subscriber);
        void Remove(Listener subscriber);
        ValueTask SubscribeAsync(IRosClient? client, Listener? listener = null, CancellationToken token = default);
        ValueTask UnsubscribeAsync(CancellationToken token);
        void Invalidate();
        BagListener? BagListener { set; }
    }
}