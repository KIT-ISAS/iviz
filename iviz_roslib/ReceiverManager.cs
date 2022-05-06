using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;
using Nito.AsyncEx;

namespace Iviz.Roslib;

internal sealed class ReceiverManager<T> where T : IMessage
{
    const int DefaultTimeoutInMs = 5000;

    readonly AsyncLock mutex = new();
    readonly ConcurrentDictionary<Uri, ReceiverConnector> connectorsByUri = new();
    readonly ConcurrentDictionary<Uri, IProtocolReceiver> receiversByUri = new();
    readonly RosClient client;
    readonly RosSubscriber<T> subscriber;
    readonly TopicInfo<T> topicInfo;
    readonly RosTransportHint transportHint;
    HashSet<Uri> cachedPublisherUris = new();

    bool isPaused;

    public bool IsPaused
    {
        get => isPaused;
        set
        {
            isPaused = value;
            foreach (var receiver in receiversByUri.Values)
            {
                receiver.IsPaused = value;
            }
        }
    }

    public ReceiverManager(RosSubscriber<T> subscriber, RosClient client, TopicInfo<T> topicInfo,
        bool requestNoDelay, RosTransportHint transportHint)
    {
        this.subscriber = subscriber;
        this.client = client;
        this.topicInfo = topicInfo;
        this.transportHint = transportHint;
        RequestNoDelay = requestNoDelay;
    }

    public string Topic => topicInfo.Topic;
    public string TopicType => topicInfo.Type;
    public int NumConnections => receiversByUri.Count;
    public int NumActiveConnections => receiversByUri.Count(pair => pair.Value.IsConnected);
    public bool RequestNoDelay { get; }
    public int TimeoutInMs { get; set; } = DefaultTimeoutInMs;

    internal void MessageCallback(in T msg, IRosReceiver receiver)
    {
        subscriber.MessageCallback(msg, receiver);
    }

    public async ValueTask PublisherUpdateRpcAsync(IEnumerable<Uri> publisherUris, CancellationToken token)
    {
        bool numConnectionsHasChanged;

        using (await mutex.LockAsync(token))
        {
            var newPublishers = new HashSet<Uri>(publisherUris);
            cachedPublisherUris = newPublishers;

            if (receiversByUri.Keys.Any(key => !newPublishers.Contains(key)))
            {
                var toDelete = receiversByUri
                    .Where(pair => !newPublishers.Contains(pair.Key))
                    .Select(pair => pair.Value)
                    .ToArray();

                await toDelete.Select(receiver => receiver.DisposeAsync(token).AsTask())
                    .WhenAll()
                    .AwaitNoThrow(this);
            }

            if (newPublishers.Any(uri => !receiversByUri.ContainsKey(uri)))
            {
                var toAdd = newPublishers.Where(uri => !receiversByUri.ContainsKey(uri));

                foreach (Uri remoteUri in toAdd)
                {
                    var udpTopicRequest = transportHint != RosTransportHint.OnlyTcp
                        ? UdpReceiver<T>.CreateRequest(client.CallerUri.Host, remoteUri.Host, topicInfo)
                        : null;

                    if (connectorsByUri.TryGetValue(remoteUri, out var oldConnector))
                    {
                        if (oldConnector.IsAlive)
                        {
                            continue;
                        }

                        await oldConnector.DisposeAsync(token);
                    }

                    var rosNodeClient = client.CreateNodeClient(remoteUri);
                    Logger.LogDebugFormat("{0}: Adding connector for '{1}'", this, remoteUri);
                    var receiverConnector = new ReceiverConnector(rosNodeClient, topicInfo.Topic, transportHint,
                        udpTopicRequest, OnConnectionSucceeded);
                    connectorsByUri[remoteUri] = receiverConnector;
                }
            }

            numConnectionsHasChanged = await CleanupAsync(token);
        }

        if (numConnectionsHasChanged)
        {
            subscriber.RaiseNumPublishersChanged();
        }
    }

    void OnConnectionSucceeded(ReceiverConnector connector, ReceiverConnector.Response response)
    {
        IProtocolReceiver receiver;
        var (tcpEndpoint, udpResponse, udpClient) = response;
        if (tcpEndpoint is { } endPoint)
        {
            udpClient?.Dispose();
            receiver =
                new TcpReceiver<T>(this, connector.RemoteUri, endPoint, topicInfo, RequestNoDelay, TimeoutInMs)
                    { IsPaused = IsPaused };
        }
        else if (udpResponse != null && udpClient != null)
        {
            receiver = new UdpReceiver<T>(this, udpResponse, udpClient, connector.RemoteUri, topicInfo);
        }
        else
        {
            Logger.LogErrorFormat("{0}: Internal error, neither TCP nor UDP was set when creating connection", this);
            return;
        }

        receiversByUri[receiver.RemoteUri] = receiver;
    }

    public async void RetryConnection(Uri remoteUri)
    {
        var token = subscriber.CancellationToken;
        try
        {
            using (await mutex.LockAsync(token))
            {
                if (connectorsByUri.TryGetValue(remoteUri, out var existingConnector))
                {
                    await existingConnector.DisposeAsync(token);
                }

                token.ThrowIfCancellationRequested();

                var udpTopicRequest = transportHint != RosTransportHint.OnlyTcp
                    ? UdpReceiver<T>.CreateRequest(client.CallerUri.Host, remoteUri.Host, topicInfo)
                    : null;

                var rosNodeClient = client.CreateNodeClient(remoteUri);
                var receiverConnector = new ReceiverConnector(rosNodeClient, topicInfo.Topic, transportHint,
                    udpTopicRequest, OnConnectionSucceeded);

                connectorsByUri[remoteUri] = receiverConnector;
                Logger.LogDebugFormat("{0}: Adding connector for '{1}' (retry!)", this, remoteUri);
            }
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception e)
        {
            Logger.LogDebugFormat("{0}: Failed to retry connection for uri {1}: {2}", this, remoteUri, e);
        }
    }
    
    public bool TryGetLoopbackReceiver(Endpoint endPoint, out ILoopbackReceiver<T>? receiver)
    {
        IProtocolReceiver? newReceiver =
            receiversByUri.FirstOrDefault(pair => endPoint.Equals(pair.Value.Endpoint)).Value;
        receiver = newReceiver as ILoopbackReceiver<T>;
        return receiver != null;
    }

    async ValueTask<bool> CleanupAsync(CancellationToken token)
    {
        var receiversToDelete = receiversByUri.Values.Where(receiver => !receiver.IsAlive).ToArray();
        var deleteTasks = receiversToDelete.Select(receiver =>
        {
            receiversByUri.TryRemove(receiver.RemoteUri, out _);
            Logger.LogDebugFormat("{0}: Removing connection with uri '{1}' - dead x_x: {2}", this,
                receiver.RemoteUri, receiver.ErrorDescription);
            return receiver.DisposeAsync(token).AsTask();
        });

        await deleteTasks.WhenAll().AwaitNoThrow(this);

        var connectorsToDelete = connectorsByUri.Values.Where(connector => !connector.IsAlive).ToArray();
        var connectorTasks = connectorsToDelete.Select(connector =>
        {
            connectorsByUri.TryRemove(connector.RemoteUri, out _);
            Logger.LogDebugFormat("{0}: Removing connector with uri '{1}' - dead x_x: {2}", this,
                connector.RemoteUri, connector.ErrorDescription);
            return connector.DisposeAsync(token).AsTask();
        });

        await connectorTasks.WhenAll().AwaitNoThrow(this);

        return receiversToDelete.Length != 0 || connectorsToDelete.Length != 0;
    }

    public void Stop()
    {
        TaskUtils.Run(() => StopAsync(default).AsTask()).WaitNoThrow(this);
    }

    public async ValueTask StopAsync(CancellationToken token)
    {
        var receivers = receiversByUri.Values.ToArray();
        receiversByUri.Clear();
        var connectors = connectorsByUri.Values.ToArray();
        connectorsByUri.Clear();

        await receivers.Select(receiver => receiver.DisposeAsync(token).AsTask()).WhenAll();
        await connectors.Select(connector => connector.DisposeAsync(token).AsTask()).WhenAll();
    }

    public SubscriberReceiverState[] GetStates()
    {
        var publisherUris = cachedPublisherUris;
        var receivers = new Dictionary<Uri, IProtocolReceiver>(receiversByUri);
        var connectors = new Dictionary<Uri, ReceiverConnector>(connectorsByUri);

        SubscriberReceiverState[] states = new SubscriberReceiverState[publisherUris.Count];
        int receiverIndex = 0;

        void Add(SubscriberReceiverState state) => states[receiverIndex++] = state;
        
        foreach (Uri uri in publisherUris)
        {
            if (receivers.TryGetValue(uri, out var receiver))
            {
                if (receiver.IsAlive)
                {
                    Add(receiver.State);
                    continue;
                }

                if (connectors.TryGetValue(uri, out var connector) && connector.IsAlive)
                {
                    Add(connector.State);
                    continue;
                }

                Add(receiver.State);
                continue;
            }
            else
            {
                if (connectors.TryGetValue(uri, out var connector))
                {
                    Add(connector.State);
                    continue;
                }
            }

            Add(new UninitializedReceiverState(uri));
        }

        return states;
    }

    public override string ToString() => $"[ReceiverManager '{Topic}']";
}