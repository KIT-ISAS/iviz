using System;
using System.Runtime.InteropServices;
using System.Threading;
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

    bool disposed;
    bool subscribersChanged;

    public string FullName => client.FullName;

    public static bool IsTypeSupported(string message) => Rcl.IsTypeSupported(message);

    public AsyncRclClient(string name, string @namespace = "")
    {
        client = new RclClient(name, @namespace);
        waitSet = client.CreateWaitSet(32, 1);
        guard = client.CreateGuardCondition();

        cachedGuardHandles = new[] { guard.Handle };

        Start();
    }

    public Task<RclSubscriber> SubscribeAsync(string topic, string type, ISignalizable signalizable,
        RosTransportHint transportHint,
        CancellationToken token)
    {
        return Post(() =>
        {
            var profile = new QosProfile(transportHint switch
            {
                RosTransportHint.OnlyUdp or RosTransportHint.PreferUdp => ReliabilityPolicy.BestEffort,
                RosTransportHint.OnlyTcp or RosTransportHint.PreferTcp => ReliabilityPolicy.Reliable,
                _ => throw new IndexOutOfRangeException()
            });

            var subscriber = client.Subscribe(topic, type, profile);
            subscribers.Add((subscriber, signalizable));
            subscribersChanged = true;
            return subscriber;
        }, token);
    }

    public Task UnsubscribeAsync(RclSubscriber subscriber, CancellationToken token)
    {
        return Post(() =>
        {
            if (subscribers.RemoveAll(pair => pair.subscriber == subscriber) != 1)
            {
                Logger.LogErrorFormat("{0}: " + nameof(UnsubscribeAsync) + " failed to find subscriber for topic {1}",
                    this, subscriber.Topic);
            }
            else
            {
                subscribersChanged = true;
            }

            subscriber.Dispose();
        }, token);
    }

    public Task<RclPublisher> AdvertiseAsync(string topic, string type, CancellationToken token)
    {
        return Post(() => client.Advertise(topic, type), token);
    }

    public Task<NodeName[]> GetNodeNamesAsync(CancellationToken token = default)
    {
        return Post(client.GetNodeNames, token);
    }

    public Task<TopicNameType[]> GetTopicNamesAndTypesAsync(CancellationToken token)
    {
        return Post(client.GetTopicNamesAndTypes, token);
    }

    public Task<TopicNameType[]> GetPublishedTopicNamesAndTypesAsync(CancellationToken token)
    {
        return Post(() =>
                client.GetTopicNamesAndTypes()
                    .Where(topic => client.CountPublishers(topic.Topic) != 0)
                    .ToArray(),
            token);
    }

    public Task<EndpointInfo[]> GetSubscriberInfoAsync(string topic, CancellationToken token)
    {
        return Post(() => client.GetSubscriberInfo(topic), token);
    }

    public Task<EndpointInfo[]> GetPublisherInfoAsync(string topic, CancellationToken token)
    {
        return Post(() => client.GetPublisherInfo(topic), token);
    }

    public Task<SystemState> GetSystemStateAsync(CancellationToken token)
    {
        return Post(() =>
        {
            var topics = client.GetTopicNamesAndTypes();
            if (topics.Length == 0)
            {
                var emptyTuple = Array.Empty<TopicTuple>();
                return new SystemState(emptyTuple, emptyTuple, emptyTuple);
            }

            var topicSubscribers =
                topics.Length == 0
                    ? Array.Empty<TopicTuple>()
                    : topics
                        .Select(topic => new TopicTuple(topic.Topic, GetSubscribers(topic.Topic)))
                        .Where(tuple => tuple.Members.Length != 0)
                        .ToArray();

            var topicPublishers =
                topics.Length == 0
                    ? Array.Empty<TopicTuple>()
                    : topics
                        .Select(topic => new TopicTuple(topic.Topic, GetPublishers(topic.Topic)))
                        .Where(tuple => tuple.Members.Length != 0)
                        .ToArray();

            var serviceProviders = GetProviders(client);

            return new SystemState(topicPublishers, topicSubscribers, serviceProviders);

            static string NodeToString(EndpointInfo info) => info.NodeName.ToString();

            string[] GetSubscribers(string topic) =>
                client.GetSubscriberInfo(topic)
                    .Select(NodeToString)
                    .ToArray();

            string[] GetPublishers(string topic) =>
                client.GetPublisherInfo(topic)
                    .Select(NodeToString)
                    .ToArray();

            static TopicTuple[] GetProviders(RclClient client)
            {
                var nodeServices = client.GetNodeNames().Select(node =>
                    (provider: node, services: client.GetServiceNamesAndTypesByNode(node.Name, node.Namespace)));

                if (nodeServices.Count == 0)
                {
                    return Array.Empty<TopicTuple>();
                }

                var serviceDict = new Dictionary<string, List<string>>();
                foreach (var tuple in nodeServices)
                {
                    foreach (var service in tuple.services)
                    {
                        string provider = tuple.provider.ToString();
                        if (serviceDict.TryGetValue(service.Topic, out var serviceNames))
                        {
                            serviceNames.Add(provider);
                        }
                        else
                        {
                            serviceDict[service.Topic] = new List<string>(1) { provider };
                        }
                    }
                }

                if (serviceDict.Count == 0)
                {
                    return Array.Empty<TopicTuple>();
                }

                return serviceDict
                    .Select(pair => new TopicTuple(pair.Key, pair.Value.ToArray()))
                    .ToArray();
            }
        }, token);
    }

    public Task UnadvertiseAsync(RclPublisher publisher, CancellationToken token)
    {
        return Post(publisher.Dispose, token);
    }

    protected override void Signal()
    {
        guard.Trigger();
    }

    protected override void Wait()
    {
        if (subscribersChanged)
        {
            cachedSubscriberHandles = subscribers.Select(tuple => tuple.subscriber.Handle).ToArray();
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

    public override async ValueTask DisposeAsync(CancellationToken token)
    {
        if (disposed) return;
        disposed = true;

        await Post(() =>
        {
            waitSet.Dispose();
            guard.Dispose();
            client.Dispose();
            Stop();
        }, token);
        await base.DisposeAsync(token);
    }
}

interface ISignalizable
{
    internal void Signal();
}