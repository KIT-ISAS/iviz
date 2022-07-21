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
        void Add(IListener subscriber);
        void Remove(IListener subscriber);
        ValueTask SubscribeAsync(IRosClient? client, IListener? listener = null, CancellationToken token = default);
        ValueTask UnsubscribeAsync(CancellationToken token);
        void Invalidate();
        BagListener? BagListener { set; }
    }
}