using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Iviz.Roslib;
using Iviz.Tools;
using Newtonsoft.Json;
using JsonSerializer = Utf8Json.JsonSerializer;

namespace Iviz.Bridge;

public sealed class SocketConnection : IAsyncDisposable
{
    readonly Dictionary<string, Subscription> subscriptions = new();
    readonly Dictionary<string, Advertisement> advertisements = new();
    readonly ChannelWriter<SubscriberMessage> msgQueueWriter;
    readonly Task task;
    readonly string endPoint;
    readonly IRosClient client;
    readonly WebSocket socket;
    bool disposed;

    public SocketConnection(IRosClient client, WebSocket socket, string endPoint)
    {
        this.client = client;
        this.endPoint = endPoint;
        this.socket = socket;
        var channel = Channel.CreateUnbounded<SubscriberMessage>(new UnboundedChannelOptions { SingleReader = true });
        msgQueueWriter = channel.Writer;
        task = Task.Run(async () => await RunAsync(channel.Reader));
        
        LogInfo("Starting!");
    }

    public async ValueTask DisposeAsync()
    {
        if (disposed) return;
        disposed = true;

        foreach (Subscription subscription in subscriptions.Values)
        {
            await subscription.DisposeAsync();
        }

        subscriptions.Clear();

        foreach (Advertisement advertisement in advertisements.Values)
        {
            await advertisement.DisposeAsync();
        }

        advertisements.Clear();

        msgQueueWriter.Complete();
        task.Wait();
    }

    public async ValueTask OnMessage(byte[] data)
    {
        try
        {
            await ProcessMessageAsync(data);
        }
        catch (Exception ee)
        {
            LogError("Failed to parse message", ee);
        }
    }

    async ValueTask ProcessMessageAsync(byte[] data)
    {
        var msg = JsonSerializer.Deserialize<GenericMessage>(data);
        if (msg == null)
        {
            LogError("Failed to deserialize message!");
            return;
        }

        if (string.IsNullOrWhiteSpace(msg.Topic))
        {
            LogError("Rejecting message. Reason: Topic is empty");
            return;
        }

        switch (msg.Op)
        {
            case "publish":
            {
                if (TryGetAdvertisement(msg.Topic, out var advertisement))
                {
                    advertisement.Publish(data);
                }

                break;
            }
            case "subscribe":
            {
                if (string.IsNullOrWhiteSpace(msg.Type))
                {
                    LogError("Rejecting subscribe request. Reason: Type is empty.");
                    break;
                }

                if (string.IsNullOrWhiteSpace(msg.Id))
                {
                    LogError("Rejecting subscribe request. Reason: Id is empty.");
                    break;
                }

                if (TryGetSubscription(msg.Topic, out var subscription))
                {
                    subscription.AddId(msg.Id);
                    break;
                }

                var newSubscription =
                    Subscription.CreateForType(msg.Type, msg.Topic, client, this);
                if (newSubscription == null)
                {
                    LogError($"Failed to subscribe to '{msg.Topic}'. Reason: I don't know message type '{msg.Type}'");
                    break;
                }

                AddSubscription(msg.Topic, newSubscription);
                break;
            }
            case "unsubscribe":
            {
                if (!TryGetSubscription(msg.Topic, out var subscription))
                {
                    break;
                }

                subscription.RemoveId(msg.Id);
                if (subscription.Empty)
                {
                    await subscription.DisposeAsync();
                    RemoveSubscription(msg.Topic);
                }

                break;
            }
            case "advertise":
            {
                if (string.IsNullOrWhiteSpace(msg.Type))
                {
                    LogError("Rejecting advertise request. Reason: Type is empty.");
                    break;
                }

                if (string.IsNullOrWhiteSpace(msg.Id))
                {
                    LogError("Rejecting advertise request. Reason: Id is empty.");
                    break;
                }

                if (TryGetAdvertisement(msg.Topic, out var advertisement))
                {
                    advertisement.AddId(msg.Id);
                    break;
                }

                var newAdvertisement = Advertisement.CreateForType(msg.Type, msg.Topic, client, this);
                if (newAdvertisement == null)
                {
                    LogError($"Failed to advertise '{msg.Topic}'. Reason: I don't know message type '{msg.Type}'");
                    break;
                }

                AddAdvertisement(msg.Topic, newAdvertisement);
                newAdvertisement.AddId(msg.Id);
                break;
            }
            case "unadvertise":
            {
                if (!TryGetAdvertisement(msg.Topic, out var advertisement))
                {
                    break;
                }

                advertisement.RemoveId(msg.Id);
                if (advertisement.Empty)
                {
                    await advertisement.DisposeAsync();
                    RemoveAdvertisement(msg.Topic);
                }

                break;
            }
            default:
                LogError($"Unknown command '{msg.Op}' for '{msg.Topic}'");
                break;
        }
    }

    public void MessageCallback(SubscriberMessage msg)
    {
        msgQueueWriter.TryWrite(msg);
    }

    async ValueTask RunAsync(ChannelReader<SubscriberMessage> msgQueueReader)
    {
        try
        {
            while (true)
            {
                object msg = await msgQueueReader.ReadAsync();
                string msgAsJson = JsonConvert.SerializeObject(msg);
                await SendAsync(msgAsJson);
            }
        }
        catch (InvalidOperationException)
        {
            // completed, exit gracefully
        }
        catch (Exception e)
        {
            LogError($"{nameof(RunAsync)} exited", e);
        }
    }

    async ValueTask SendAsync(string data)
    {
        using var rent = data.AsRent();
        await socket.SendAsync(rent, WebSocketMessageType.Text, true, default);
    }

    bool TryGetSubscription(string topic, [NotNullWhen(true)] out Subscription? subscription)
    {
        return subscriptions.TryGetValue(topic, out subscription);
    }

    bool TryGetAdvertisement(string topic, [NotNullWhen(true)] out Advertisement? advertisement)
    {
        return advertisements.TryGetValue(topic, out advertisement);
    }

    void AddSubscription(string topic, Subscription subscription)
    {
        subscriptions.Add(topic, subscription);
    }

    void AddAdvertisement(string topic, Advertisement advertisement)
    {
        advertisements.Add(topic, advertisement);
    }

    void RemoveSubscription(string topic)
    {
        subscriptions.Remove(topic);
    }

    void RemoveAdvertisement(string topic)
    {
        advertisements.Remove(topic);
    }

    void LogInfo(string msg) => Tools.Logger.LogFormat("** {0}: {1}", this, msg);

    internal void LogError(string msg) => Tools.Logger.LogErrorFormat("EE {0}: {1}", this, msg);

    void LogError(string msg, Exception e) => Tools.Logger.LogErrorFormat("EE {0}: {1}{2}", this, msg, e);

    public override string ToString() => $"[{endPoint}]";
}