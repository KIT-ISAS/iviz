using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;

namespace Iviz.Roslib
{
    public interface IRosClient : IDisposable
#if !NETSTANDARD2_0
        , IAsyncDisposable
#endif
    {
        string CallerId { get; }

        string Advertise<T>(string topic, out IRosPublisher<T> publisher) where T : IMessage;

        string Advertise(string topic, DynamicMessage generator, out IRosPublisher<DynamicMessage> publisher);

        ValueTask<(string id, IRosPublisher<T> publisher)> AdvertiseAsync<T>(string topic,
            CancellationToken token = default)
            where T : IMessage;

        ValueTask<(string id, IRosPublisher<DynamicMessage> publisher)> AdvertiseAsync(string topic,
            DynamicMessage generator, CancellationToken token = default);

        string Advertise(string topic, Type msgType, out IRosPublisher publisher);

        ValueTask<(string id, IRosPublisher publisher)> AdvertiseAsync(string topic, Type msgType,
            CancellationToken token = default);

        string Subscribe<T>(string topic, Action<T> callback, out IRosSubscriber<T> subscriber,
            bool requestNoDelay = true, RosTransportHint transportHint = RosTransportHint.OnlyTcp)
            where T : IMessage, IDeserializable<T>, new();

        string Subscribe(string topic, Action<IMessage> callback, out IRosSubscriber subscriber,
            bool requestNoDelay = true, RosTransportHint transportHint = RosTransportHint.OnlyTcp);

        ValueTask<(string id, IRosSubscriber<T> subscriber)>
            SubscribeAsync<T>(string topic, Action<T> callback, bool requestNoDelay = true,
                RosTransportHint transportHint = RosTransportHint.OnlyTcp, CancellationToken token = default)
            where T : IMessage, IDeserializable<T>, new();

        ValueTask<(string id, IRosSubscriber subscriber)>
            SubscribeAsync(string topic, Action<IMessage> callback, bool requestNoDelay = true,
                RosTransportHint transportHint = RosTransportHint.OnlyTcp,
                CancellationToken token = default);

        bool AdvertiseService<T>(string serviceName, Action<T> callback, CancellationToken token = default)
            where T : IService, new();

        ValueTask<bool> AdvertiseServiceAsync<T>(string serviceName, Func<T, Task> callback,
            CancellationToken token = default) where T : IService, new();

        T CallService<T>(string serviceName, T service, bool persistent = false, int timeoutInMs = 5000)
            where T : IService;

        ValueTask<T> CallServiceAsync<T>(string serviceName, T service, bool persistent = false,
            CancellationToken token = default)
            where T : IService;
    }
}