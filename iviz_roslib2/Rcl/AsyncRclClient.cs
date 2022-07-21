using System;
using System.Runtime.InteropServices;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class AsyncRclClient
{
    readonly RclClient client;
    readonly SingleThreadExecutor executor;

    public string Name => client.Name;
    public string Namespace => client.Namespace;
    public string FullName => client.FullName;

    AsyncRclClient(SingleThreadExecutor executor, RclClient client)
    {
        this.executor = executor;
        this.client = client;
    }

    public static async ValueTask<AsyncRclClient> CreateAsync(string name, string @namespace = "")
    {
        var executor = new SingleThreadExecutor();
        var client = await executor.Enqueue(() => new RclClient(name, @namespace));
        return new AsyncRclClient(executor, client);
    }

    public static AsyncRclClient Create(string name, string @namespace = "")
    {
        return TaskUtils.Run(() => CreateAsync(name, @namespace).AsTask()).WaitAndRethrow();
    }

    public Task<RclSubscriber> SubscribeAsync(string topic, string type)
    {
        return executor.Enqueue(() => client.Subscribe(topic, type));
    }

    public Task<RclPublisher> AdvertiseAsync(string topic, string type)
    {
        return executor.Enqueue(() => client.Advertise(topic, type));
    }

    public Task<NodeName[]> GetNodeNamesAsync()
    {
        return executor.Enqueue(client.GetNodeNames);
    }

    public Task<TopicNameType[]> GetTopicNamesAndTypesAsync()
    {
        return executor.Enqueue(client.GetTopicNamesAndTypes);
    }

    public Task<EndpointInfo[]> GetSubscriberInfoAsync(string topic)
    {
        return executor.Enqueue(() => client.GetSubscriberInfo(topic));
    }

    public Task<EndpointInfo[]> GetPublisherInfoAsync(string topic)
    {
        return executor.Enqueue(() => client.GetPublisherInfo(topic));
    }

    public Task DisposeAsync()
    {
        return executor.Enqueue(client.Dispose);
    }

    public Task DisposeSubscriberAsync(RclSubscriber subscriber)
    {
        return executor.Enqueue(subscriber.Dispose);
    }
    
    public Task DisposePublisherAsync(RclPublisher publisher)
    {
        return executor.Enqueue(publisher.Dispose);
    }

}