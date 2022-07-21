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
        ValueTask AdvertiseAsync(IRosClient? client, CancellationToken token);
        ValueTask UnadvertiseAsync(IRosClient? client, CancellationToken token);
        bool TrySetCallback<TU>(Func<TU, ValueTask> callback) where TU : IService;
    }
}