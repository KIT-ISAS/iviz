using System.Runtime.CompilerServices;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class AsyncRclClient : TaskExecutor
{
    readonly RclClient client;
    readonly RclWaitSet waitSet;
    readonly RclGuardCondition guard;

    readonly List<(IHasHandle handler, Signalizable signalizable)> subscribers = new();
    readonly List<(IHasHandle handler, Signalizable signalizable)> serviceClients = new();
    readonly List<(IHasHandle handler, Signalizable signalizable)> serviceServers = new();

    readonly IntPtr[] guardHandles;
    IntPtr[] subscriberHandles = Array.Empty<IntPtr>();
    IntPtr[] serviceClientHandles = Array.Empty<IntPtr>();
    IntPtr[] serviceServerHandles = Array.Empty<IntPtr>();

    bool disposed;

    public long GraphChangedTicks { get; private set; }

    public string FullName => client.FullName;

    public static bool IsMessageTypeSupported(string message) => Rcl.IsMessageTypeSupported(message);

    public static bool IsServiceTypeSupported(string message) => Rcl.IsServiceTypeSupported(message);

    public AsyncRclClient(string name, string @namespace = "")
    {
        client = new RclClient(name, @namespace);
        waitSet = client.CreateWaitSet(32, 2, 32, 32);
        guard = client.CreateGuardCondition();

        var graphGuardHandle = client.GetGraphGuardCondition();

        guardHandles = new[] { guard.Handle, graphGuardHandle };

        Start();
    }

    protected override void Signal()
    {
        guard.Trigger();
    }

    public void Wait()
    {
        const int timeoutInMs = 5000;

        bool success = waitSet.WaitFor(subscriberHandles, guardHandles,
            serviceClientHandles, serviceServerHandles,
            out var triggeredSubscriptions, out var triggeredGuards,
            out var triggeredClients, out var triggeredServers,
            timeoutInMs);

        if (!success)
        {
            return; // timeout, nothing triggered
        }

        if (IsNotZero(triggeredGuards[1]))
        {
            GraphChangedTicks = DateTime.Now.Ticks;
        }

        for (int i = 0; i < subscriberHandles.Length; i++)
        {
            if (IsNotZero(triggeredSubscriptions[i]))
            {
                subscribers[i].signalizable.Signal();
            }
        }

        for (int i = 0; i < serviceClientHandles.Length; i++)
        {
            if (IsNotZero(triggeredClients[i]))
            {
                serviceClients[i].signalizable.Signal();
            }
        }

        for (int i = 0; i < serviceServerHandles.Length; i++)
        {
            if (IsNotZero(triggeredServers[i]))
            {
                serviceServers[i].signalizable.Signal();
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool IsNotZero(IntPtr ptr) => Unsafe.As<IntPtr, nint>(ref ptr) != 0;
    
    void RebuildSubscribers()
    {
        subscriberHandles = subscribers.Select(tuple => tuple.handler.Handle).ToArray();
    }

    public Task<RclSubscriber> SubscribeAsync(string topic, string type, Signalizable signalizable, QosProfile profile,
        CancellationToken token)
    {
        return Post(() =>
        {
            var subscriber = client.CreateSubscriber(topic, type, profile);
            subscribers.Add((subscriber, signalizable));
            RebuildSubscribers();
            return subscriber;
        }, token);
    }

    public Task UnsubscribeAsync(RclSubscriber subscriber, CancellationToken token)
    {
        return Post(() =>
        {
            subscriber.Dispose();
            if (subscribers.RemoveAll(tuple => tuple.handler == subscriber) != 1)
            {
                Logger.LogErrorFormat("{0}: {2} failed to find subscriber for topic {1}",
                    this, subscriber.Topic, nameof(UnsubscribeAsync));
                return;
            }

            RebuildSubscribers();
        }, token);
    }

    public Task<RclPublisher> AdvertiseAsync(string topic, string type, QosProfile profile, CancellationToken token)
    {
        return Post(() => client.CreatePublisher(topic, type, profile), token);
    }

    public Task DisposePublisher(RclPublisher publisher, CancellationToken token)
    {
        return Post(publisher.Dispose, token);
    }

    void RebuildServiceClients()
    {
        serviceClientHandles = serviceClients.Select(tuple => tuple.handler.Handle).ToArray();
    }

    public Task<RclServiceClient> CreateServiceClientAsync(string topic, string type, Signalizable signalizable,
        QosProfile profile, CancellationToken token)
    {
        return Post(() =>
        {
            var serverClient = client.CreateServerClient(topic, type, profile);
            serviceClients.Add((serverClient, signalizable));
            RebuildServiceClients();
            return serverClient;
        }, token);
    }

    public Task DisposeServiceClientAsync(RclServiceClient serviceClient, CancellationToken token)
    {
        return Post(() =>
        {
            serviceClient.Dispose();
            if (serviceClients.RemoveAll(tuple => tuple.handler == serviceClient) != 1)
            {
                Logger.LogErrorFormat("{0}: {2} failed to find service client for service {1}",
                    this, serviceClient.Service, nameof(DisposeServiceClientAsync));
                return;
            }

            RebuildServiceClients();
        }, token);
    }

    void RebuildServiceServers()
    {
        serviceServerHandles = serviceServers.Select(tuple => tuple.handler.Handle).ToArray();
    }

    public Task<RclServiceServer> AdvertiseServiceAsync(string topic, string type, Signalizable signalizable,
        QosProfile profile, CancellationToken token)
    {
        return Post(() =>
        {
            var serviceServer = client.CreateServiceServer(topic, type, profile);
            serviceServers.Add((serviceServer, signalizable));
            RebuildServiceServers();
            return serviceServer;
        }, token);
    }

    public Task UnadvertiseServiceAsync(RclServiceServer serviceServer, CancellationToken token)
    {
        return Post(() =>
        {
            serviceServer.Dispose();
            if (serviceServers.RemoveAll(tuple => tuple.handler == serviceServer) != 1)
            {
                Logger.LogErrorFormat("{0}: {2} failed to find service server for service {1}",
                    this, serviceServer.Service, nameof(UnadvertiseServiceAsync));
                return;
            }

            RebuildServiceServers();
        }, token);
    }

    public Task<NodeName[]> GetNodeNamesAsync(CancellationToken token)
    {
        return Post(client.GetNodeNames, token);
    }

    public Task<TopicNameType[]> GetTopicNamesAndTypesAsync(CancellationToken token)
    {
        return Post(client.GetTopicNamesAndTypes, token);
    }
    
    public Task<TopicNameType[]> GetServiceNamesAndTypesAsync(CancellationToken token)
    {
        return Post(client.GetServiceNamesAndTypes, token);
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
                        .Where(static tuple => tuple.Members.Length != 0)
                        .ToArray();

            var topicPublishers =
                topics.Length == 0
                    ? Array.Empty<TopicTuple>()
                    : topics
                        .Select(topic => new TopicTuple(topic.Topic, GetPublishers(topic.Topic)))
                        .Where(static tuple => tuple.Members.Length != 0)
                        .ToArray();

            var serviceProviders = GetProviders(client);

            return new SystemState(topicPublishers, topicSubscribers, serviceProviders);

            static string NodeToString(EndpointInfo info) => info.NodeName.ToString();

            string[] GetSubscribers(string topic) =>
                client.GetSubscriberInfo(topic).Select(NodeToString).ToArray();

            string[] GetPublishers(string topic) =>
                client.GetPublisherInfo(topic).Select(NodeToString).ToArray();

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