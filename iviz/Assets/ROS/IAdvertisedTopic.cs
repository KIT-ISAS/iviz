#nullable enable

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Roslib;

namespace Iviz.Ros
{
    internal abstract class AdvertisedTopic
    {
        int? id;
        protected readonly HashSet<Sender> senders = new();

        public int? Id
        {
            get => id;
            set
            {
                id = value;
                foreach (var sender in senders)
                {
                    sender.Id = value;
                }
            }
        }
        public IRosPublisher? Publisher { get; protected set; }
        public int Count => senders.Count;

        public abstract void Add(Sender subscriber);
        public abstract void Remove(Sender subscriber);
        public abstract ValueTask AdvertiseAsync(IRosClient? client, CancellationToken token);
        public abstract ValueTask UnadvertiseAsync(CancellationToken token);
        public abstract void Invalidate();
    }
}