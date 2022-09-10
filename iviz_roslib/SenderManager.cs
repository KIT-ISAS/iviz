﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.XmlRpc;
using Iviz.Tools;
using Nito.AsyncEx;

namespace Iviz.Roslib;

internal sealed class SenderManager<TMessage> : ILatchedMessageProvider<TMessage> where TMessage : IMessage
{
    const int DefaultTimeoutInMs = 5000;

    readonly AsyncLock mutex = new();
    readonly HashSet<IProtocolSender<TMessage>> senders = new();
    readonly RosPublisher<TMessage> publisher;
    readonly TopicInfo topicInfo;
    readonly CancellationTokenSource tokenSource = new();
    readonly TcpListener listener;
    readonly Task task;

    IProtocolSender<TMessage>[] cachedSenders = Array.Empty<IProtocolSender<TMessage>>();
    NullableMessage<TMessage> latchedMessage;

    int maxQueueSizeInBytes;
    bool latchingEnabled;
    bool forceTcpNoDelay;
    bool disposed;

    bool KeepRunning => !tokenSource.IsCancellationRequested;
    public Endpoint Endpoint => new((IPEndPoint)listener.LocalEndpoint);

    public string Topic => topicInfo.Topic;
    public string TopicType => topicInfo.Type;
    public int NumConnections => cachedSenders.Count(sender => sender.IsAlive);
    public int TimeoutInMs { get; set; } = DefaultTimeoutInMs;

    public bool ForceTcpNoDelay
    {
        get => forceTcpNoDelay;
        set
        {
            forceTcpNoDelay = value;
            if (!value)
            {
                return;
            }

            var tcpSenders = cachedSenders.OfType<TcpSender<TMessage>>();
            foreach (var tcpSender in tcpSenders)
            {
                tcpSender.TcpNoDelay = true;
            }
        }
    }

    public bool LatchingEnabled
    {
        get => latchingEnabled;
        set
        {
            latchingEnabled = value;
            latchedMessage = default;
        }
    }

    public int MaxQueueSizeInBytes
    {
        get => maxQueueSizeInBytes;
        set
        {
            if (value < 1)
            {
                throw new ArgumentException($"Cannot set max queue size to {value}");
            }

            maxQueueSizeInBytes = value;
            foreach (var sender in cachedSenders)
            {
                sender.MaxQueueSizeInBytes = value;
            }
        }
    }

    public TMessage LatchedMessage
    {
        set => latchedMessage = value ?? throw new NullReferenceException("Latched message cannot be null");
    }
    
    NullableMessage<TMessage> ILatchedMessageProvider<TMessage>.GetLatchedMessage() => latchedMessage;

    public SenderManager(RosPublisher<TMessage> publisher, TopicInfo topicInfo)
    {
        this.publisher = publisher;
        this.topicInfo = topicInfo;

        listener = new TcpListener(IPAddress.IPv6Any, 0) { Server = { DualMode = true } };
        listener.Start();

        task = TaskUtils.Run(async () => await RunTcpReceiverLoop().AwaitNoThrow(this));

        Logger.LogDebugFormat("{0}: Starting at :{1}", this, Endpoint.Port.ToString());
    }


    async ValueTask RunTcpReceiverLoop()
    {
        try
        {
            while (KeepRunning)
            {
                var client = await listener.AcceptTcpClientAsync();
                if (!KeepRunning)
                {
                    break;
                }

                if (client.Client.RemoteEndPoint == null || client.Client.LocalEndPoint == null)
                {
                    Logger.LogFormat("{0}: Received a request, but failed to initialize connection.", this);
                    continue;
                }

                var sender = new TcpSender<TMessage>(client, topicInfo, this);
                if (ForceTcpNoDelay)
                {
                    sender.TcpNoDelay = true;
                }

                if (publisher.TryGetLoopbackReceiver(sender.RemoteEndpoint, out var loopbackReceiver))
                {
                    sender.LoopbackReceiver = loopbackReceiver;
                }

                using (await mutex.LockAsync(tokenSource.Token))
                {
                    senders.Add(sender);
                    await CleanupAsync(tokenSource.Token);
                }
            }
        }
        catch (Exception e)
        {
            if (e is not (ObjectDisposedException or OperationCanceledException))
            {
                Logger.LogFormat("{0}: Stopped thread {1}", this, e);
            }

            return;
        }

        Logger.LogDebugFormat("{0}: Leaving task", this); // also expected
    }

    async ValueTask CleanupAsync(CancellationToken token)
    {
        var allSenders = senders.ToArray();
        if (allSenders.All(deadSender => deadSender.IsAlive))
        {
            cachedSenders = senders.ToArray();
            return;
        }

        var sendersToDelete = allSenders.Where(deadSender => !deadSender.IsAlive).ToList();

        var tasks = sendersToDelete.Select(async deadSender =>
        {
            await deadSender.DisposeAsync(token).AwaitNoThrow(this);
        }).ToArray();

        await tasks.WhenAll().AwaitNoThrow(this);

        foreach (var deadSender in sendersToDelete)
        {
            senders.Remove(deadSender);
            Logger.LogDebugFormat("{0}: Removing connection with '{1}' - dead x_x", this, deadSender);
        }

        publisher.RaiseNumSubscribersChanged();

        cachedSenders = senders.ToArray();
    }

    public RpcUdpTopicResponse CreateUdpConnection(RpcUdpTopicRequest request, string hostname)
    {
        var newSender = new UdpSender<TMessage>(request, topicInfo, this, out byte[] responseHeader);

        if (publisher.TryGetLoopbackReceiver(newSender.RemoteEndpoint, out var loopbackReceiver))
        {
            newSender.LoopbackReceiver = loopbackReceiver;
        }

        TaskUtils.Run(async () =>
        {
            using (await mutex.LockAsync(tokenSource.Token))
            {
                senders.Add(newSender);
                cachedSenders = senders.ToArray();
                await CleanupAsync(tokenSource.Token);
            }
        }).WaitNoThrow(this);

        return new RpcUdpTopicResponse(hostname, newSender.Endpoint.Port, 0, request.MaxPacketSize, responseHeader);
    }

    public void Publish(in TMessage msg)
    {
        if (LatchingEnabled)
        {
            latchedMessage = msg;
        }

        foreach (var sender in cachedSenders)
        {
            sender.Publish(msg);
        }
    }

    public ValueTask PublishAndWaitAsync(in TMessage msg, CancellationToken token)
    {
        if (LatchingEnabled)
        {
            latchedMessage = msg;
        }

        var localSenders = cachedSenders;
        switch (localSenders.Length)
        {
            case 0:
                return default;
            case 1:
                return localSenders[0].PublishAndWaitAsync(msg, token);
            default:
                var tasks = new Task[localSenders.Length];
                for (int i = 0; i < localSenders.Length; i++)
                {
                    tasks[i] = localSenders[i].PublishAndWaitAsync(msg, token).AsTask();
                }

                return tasks.WhenAll().AsValueTask();
        }
    }

    public void Dispose()
    {
        TaskUtils.RunSync(DisposeAsync);
    }

    public async ValueTask DisposeAsync(CancellationToken token)
    {
        if (disposed) return;
        disposed = true;
        
        tokenSource.Cancel();

        // try to make the listener come out
        await StreamUtils.EnqueueLocalConnectionAsync(Endpoint.Port, this, token);
        listener.Stop();
        
        await task.AwaitNoThrow(2000, this, token);

        await senders.Select(sender => sender.DisposeAsync(token).AsTask()).WhenAll().AwaitNoThrow(this);
        senders.Clear();

        cachedSenders = Array.Empty<IProtocolSender<TMessage>>();
        latchedMessage = default;
    }

    public Ros1SenderState[] GetStates() => cachedSenders.Select(sender => sender.State).ToArray();

    public void UnsetLatch()
    {
        latchedMessage = default;
    }

    public override string ToString()
    {
        return $"[{nameof(SenderManager<TMessage>)} '{Topic}']";
    }
}

internal readonly struct NullableMessage<TMessage>
{
    public readonly TMessage? value;
    public readonly bool hasValue;

    NullableMessage(in TMessage element)
    {
        value = element;
        hasValue = true;
    }

    public static implicit operator NullableMessage<TMessage>(in TMessage message) => new(message);
}

internal interface ILatchedMessageProvider<TMessage>
{
    bool HasLatchedMessage() => GetLatchedMessage().hasValue;
    NullableMessage<TMessage> GetLatchedMessage();
}