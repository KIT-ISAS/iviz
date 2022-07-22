using System;
using System.Runtime.InteropServices;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class AsyncRclClient : TaskExecutor
{
    readonly RclClient client;
    readonly RclWaitSet waitSet;
    readonly RclGuardCondition guard;

    readonly List<(RclSubscriber subscriber, ISignalizable signalizable)> subscribers = new();
    readonly IntPtr[] cachedGuardHandles;
    IntPtr[] cachedSubscriberHandles = Array.Empty<IntPtr>();
    bool subscribersChanged;
    
    public string FullName => client.FullName;

    public static bool IsTypeSupported(string message) => Rcl.IsTypeSupported(message);

    public AsyncRclClient(string name, string @namespace = "")
    {
        client = new RclClient(name, @namespace);
        waitSet = client.CreateWaitSet(32, 1);
        guard = client.CreateGuardCondition();

        cachedGuardHandles = new IntPtr[1];
        guard.AddHandle(out cachedGuardHandles[0]);
    }

    public Task<RclSubscriber> SubscribeAsync(string topic, string type, ISignalizable signalizable)
    {
        return Enqueue(() =>
        {
            var subscriber = client.Subscribe(topic, type);
            subscribers.Add((subscriber, signalizable));
            subscribersChanged = true;
            return subscriber;
        });
    }

    public Task<RclPublisher> AdvertiseAsync(string topic, string type)
    {
        return Enqueue(() => client.Advertise(topic, type));
    }

    public Task<NodeName[]> GetNodeNamesAsync()
    {
        return Enqueue(client.GetNodeNames);
    }

    public Task<TopicNameType[]> GetTopicNamesAndTypesAsync()
    {
        return Enqueue(client.GetTopicNamesAndTypes);
    }

    public Task<EndpointInfo[]> GetSubscriberInfoAsync(string topic)
    {
        return Enqueue(() => client.GetSubscriberInfo(topic));
    }

    public Task<EndpointInfo[]> GetPublisherInfoAsync(string topic)
    {
        return Enqueue(() => client.GetPublisherInfo(topic));
    }

    public Task DoDisposeAsync(IDisposable disposable)
    {
        return Enqueue(disposable.Dispose);
    }

    protected override void Signal()
    {
        guard.Trigger();
    }

    protected override void Wait()
    {
        if (subscribersChanged)
        {
            cachedSubscriberHandles = new IntPtr[subscribers.Count];
            for (int i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].subscriber.AddHandle(out cachedSubscriberHandles[i]);
            }

            subscribersChanged = false;
        }

        waitSet.WaitFor(cachedSubscriberHandles, cachedGuardHandles,
            out var triggeredSubscriptions, out _);

        for (int i = 0; i < triggeredSubscriptions.Length; i++)
        {
            if (triggeredSubscriptions[i] != IntPtr.Zero)
            {
                subscribers[i].signalizable.Signal();
            }
        }
    }

    public override async ValueTask DisposeAsync()
    {
        _ = Enqueue(() =>
        {
            waitSet.Dispose();
            guard.Dispose();
            client.Dispose();
        });
        await base.DisposeAsync();
    }
}

interface ISignalizable
{
    internal void Signal();
}