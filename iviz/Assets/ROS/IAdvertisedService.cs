#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.Ros
{
    internal interface IAdvertisedService
    {
        ValueTask AdvertiseAsync(RosClient? client, CancellationToken token);
        ValueTask UnadvertiseAsync(RosClient? client, CancellationToken token);
        bool TrySetCallback<TU>(Func<TU, ValueTask> callback) where TU : IService;
    }
}