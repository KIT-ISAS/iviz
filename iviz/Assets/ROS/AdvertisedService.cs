#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Ros
{
    internal sealed class AdvertisedService<T> : AdvertisedService where T : IService, new()
    {
        const int NumRetries = 3;
        const int WaitBetweenRetriesInMs = 500;

        readonly string service;
        Func<T, ValueTask> callback;

        public AdvertisedService(string service, Func<T, ValueTask> callback)
        {
            ThrowHelper.ThrowIfNull(service, nameof(service));
            ThrowHelper.ThrowIfNull(callback, nameof(callback));
            this.service = service;
            this.callback = callback;
        }

        public override bool TrySetCallback(Delegate newCallback) 
        {
            if (newCallback is not Func<T, ValueTask> validatedCallback)
            {
                return false;
            }

            callback = validatedCallback;
            return true;
        }

        public override async ValueTask AdvertiseAsync(IRosClient? client, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            if (client == null)
            {
                return;
            }

            ValueTask CallToCallback(T t) => this.callback(t); // can't pass callback directly, it may be changed

            for (int t = 0; t < NumRetries; t++)
            {
                try
                {
                    await client.AdvertiseServiceAsync<T>(service, CallToCallback, token);
                    break;
                }
                catch (RoslibException e)
                {
                    RosLogger.Error($"{ToString()}: Failed to advertise service (try {t.ToString()}): ", e);
                    await Task.Delay(WaitBetweenRetriesInMs, token);
                }
            }
        }

        public async ValueTask UnadvertiseAsync(IRosClient? client, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            if (client != null)
            {
                await client.UnadvertiseServiceAsync(service, token);
            }
        }

        public override string ToString()
        {
            return $"[{nameof(AdvertisedService<T>)} '{service}']";
        }
    }
}