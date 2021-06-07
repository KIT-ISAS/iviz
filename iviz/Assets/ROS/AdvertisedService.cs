using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;
using JetBrains.Annotations;

namespace Iviz.Ros
{
    internal interface IAdvertisedService
    {
        [NotNull]
        Task AdvertiseAsync([CanBeNull] RosClient client, CancellationToken token);

        [NotNull]
        Task UnadvertiseAsync([CanBeNull] RosClient client, CancellationToken token);

        bool TrySetCallback<TU>([NotNull] Func<TU, Task> callback) where TU : IService;
    }

    internal sealed class AdvertisedService<T> : IAdvertisedService where T : IService, new()
    {
        [NotNull] Func<T, Task> callback;
        [NotNull] readonly string service;

        public AdvertisedService([NotNull] string service, [NotNull] Func<T, Task> callback)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public bool TrySetCallback<TU>(Func<TU, Task> newCallback) where TU : IService
        {
            if (newCallback == null)
            {
                throw new ArgumentNullException(nameof(newCallback));
            }
            
            if (!(newCallback is Func<T, Task> newCallbackT))
            {
                return false;
            }

            callback = newCallbackT;
            return true;
        }

        public async Task AdvertiseAsync(RosClient client, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            string fullService = service[0] == '/' ? service : $"{client?.CallerId}/{service}";
            if (client != null)
            {
                await client.AdvertiseServiceAsync<T>(fullService, CallbackImpl, token);
            }
        }

        public async Task UnadvertiseAsync(RosClient client, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            string fullService = service[0] == '/' ? service : $"{client?.CallerId}/{service}";
            if (client != null)
            {
                await client.UnadvertiseServiceAsync(fullService, token);
            }
        }

        Task CallbackImpl(T t) => callback(t);

        [NotNull]
        public override string ToString()
        {
            return $"[AdvertisedService '{service}']";
        }
    }
}