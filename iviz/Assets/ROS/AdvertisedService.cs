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

    internal sealed class AdvertisedService<T> : IAdvertisedService where T : IService, new()
    {
        const int NumRetries = 3;
        const int WaitBetweenRetriesInMs = 500;
        
        Func<T, ValueTask> callback;
        readonly string service;

        public AdvertisedService(string service, Func<T, ValueTask> callback)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public bool TrySetCallback<TU>(Func<TU, ValueTask> newCallback) where TU : IService
        {
            if (newCallback == null)
            {
                throw new ArgumentNullException(nameof(newCallback));
            }
            
            if (newCallback is not Func<T, ValueTask> newCallbackT)
            {
                return false;
            }

            callback = newCallbackT;
            return true;
        }

        public async ValueTask AdvertiseAsync(RosClient? client, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            string fullService = service[0] == '/' ? service : $"{client?.CallerId}/{service}";
            if (client != null)
            {
                for (int t = 0; t < NumRetries; t++)
                {
                    try
                    {
                        await client.AdvertiseServiceAsync<T>(fullService, CallbackImpl, token);
                        break;
                    }
                    catch (RoslibException e)
                    {
                        Core.Logger.Error($"Failed to advertise service (try {t}): ", e);
                        await Task.Delay(WaitBetweenRetriesInMs, token);
                    }
                }
            }
        }

        public async ValueTask UnadvertiseAsync(RosClient? client, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            string fullService = service[0] == '/' ? service : $"{client?.CallerId}/{service}";
            if (client != null)
            {
                await client.UnadvertiseServiceAsync(fullService, token);
            }
        }

        ValueTask CallbackImpl(T t) => callback(t);

        public override string ToString()
        {
            return $"[AdvertisedService '{service}']";
        }
    }
}