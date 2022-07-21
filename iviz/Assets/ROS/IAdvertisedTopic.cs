#nullable enable

using System.Threading;
using System.Threading.Tasks;
using Iviz.Roslib;

namespace Iviz.Ros
{
    internal interface IAdvertisedTopic
    {
        IRosPublisher? Publisher { get; }
        int? Id { get; set; }
        int Count { get; }
        void Add(ISender subscriber);
        void Remove(ISender subscriber);
        ValueTask AdvertiseAsync(IRosClient? client, CancellationToken token);
        ValueTask UnadvertiseAsync(CancellationToken token);
        void Invalidate();
    }
}