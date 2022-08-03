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
    internal sealed class AdvertisedService<T> : IAdvertisedService where T : IService, new()
    {
        const int NumRetries = 3;
        const int WaitBetweenRetriesInMs = 500;
        
        readonly string service;
        readonly Func<T, ValueTask> callToCallback; // cached delegate that calls callback()
        Func<T, ValueTask> callback;

        public AdvertisedService(string service, Func<T, ValueTask> callback)
        {
            ThrowHelper.ThrowIfNull(service, nameof(service));
            ThrowHelper.ThrowIfNull(callback, nameof(callback));
            this.service = service;
            this.callback = callback;
            callToCallback = t => this.callback(t);
        }

        public bool TrySetCallback<TU>(Func<TU, ValueTask> newCallback) where TU : IService
        {
            if (newCallback is not Func<T, ValueTask> validatedCallback)
            {
                return false;
            }

            callback = validatedCallback;
            return true;
        }

        public async ValueTask AdvertiseAsync(IRosClient? client, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            if (client != null)
            {
                foreach (int t in ..NumRetries)
                {
                    try
                    {
                        await client.AdvertiseServiceAsync(service, callToCallback, token);
                        break;
                    }
                    catch (RoslibException e)
                    {
                        RosLogger.Error($"{this}: Failed to advertise service (try {t.ToString()}): ", e);
                        await Task.Delay(WaitBetweenRetriesInMs, token);
                    }
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