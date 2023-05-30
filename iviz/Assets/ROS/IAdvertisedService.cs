#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.Ros
{
    internal abstract class AdvertisedService
    {
        public abstract ValueTask AdvertiseAsync(IRosClient? client, CancellationToken token);
        public abstract bool TrySetCallback(Delegate callback);
    }
}