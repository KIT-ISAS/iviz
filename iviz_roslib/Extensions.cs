using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;
using Iviz.XmlRpc;

namespace Iviz.Roslib;

public static class Extensions
{
    public static void WaitForService(this RosClient client, string service, int timeoutInMs)
    {
        using CancellationTokenSource tokenSource = new(timeoutInMs);
        try
        {
            client.WaitForService(service, tokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            throw new TimeoutException($"Wait for service '{service}' timed out");
        }
    }

    public static void WaitForService(this RosClient client, string service, CancellationToken token = default)
    {
        if (client == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(client));
        }

        if (service == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(service));
        }

        TaskUtils.Run(() => client.WaitForServiceAsync(service, token).AsTask(), token).WaitAndRethrow();
    }

    public static async ValueTask WaitForServiceAsync(this RosClient client, string service,
        CancellationToken token = default)
    {
        if (client == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(client));
        }

        if (service == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(service));
        }

        while (true)
        {
            try
            {
                var result = await client.RosMasterClient.LookupServiceAsync(service, token);
                if (result.IsValid)
                {
                    return;
                }

                await Task.Delay(200, token);
            }
            catch (XmlRpcException)
            {
            }
        }
    }

    public static void WaitForAnySubscriber(this IRosPublisher publisher, int timeoutInMs)
    {
        using CancellationTokenSource tokenSource = new(timeoutInMs);
        try
        {
            publisher.WaitForAnySubscriber(tokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            throw new TimeoutException("Wait for subscriber timed out");
        }
    }

    public static void WaitForAnySubscriber(this IRosPublisher publisher, CancellationToken token = default)
    {
        if (publisher == null)
        {
            throw new ArgumentNullException(nameof(publisher));
        }

        TaskUtils.Run(() => publisher.WaitForAnySubscriberAsync(token).AsTask(), token).WaitAndRethrow();
    }

    public static void WaitForAnySubscriber(this IRosChannelWriter writer, CancellationToken token = default)
    {
        writer.Publisher.WaitForAnySubscriber(token);
    }

    public static async ValueTask WaitForAnySubscriberAsync(this IRosPublisher publisher,
        CancellationToken token = default)
    {
        if (publisher == null)
        {
            throw new ArgumentNullException(nameof(publisher));
        }

        if (publisher.NumSubscribers != 0)
        {
            return;
        }

        using var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(token, publisher.CancellationToken);
        while (publisher.NumSubscribers == 0)
        {
            await Task.Delay(200, linkedSource.Token);
        }
    }
    
    public static ValueTask WaitForAnySubscriberAsync(this IRosChannelWriter writer, CancellationToken token = default)
    {
        return writer.Publisher.WaitForAnySubscriberAsync(token);
    }

    public static void WaitForAnyPublisher(this IRosSubscriber subscriber, int timeoutInMs)
    {
        using CancellationTokenSource tokenSource = new(timeoutInMs);
        try
        {
            subscriber.WaitForAnyPublisher(tokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            throw new TimeoutException("Wait for publisher timed out");
        }
    }

    public static void WaitForAnyPublisher(this IRosSubscriber subscriber, CancellationToken token = default)
    {
        if (subscriber == null)
        {
            throw new ArgumentNullException(nameof(subscriber));
        }

        TaskUtils.Run(() => subscriber.WaitForAnyPublisherAsync(token).AsTask(), token).WaitAndRethrow();
    }

    public static async ValueTask WaitForAnyPublisherAsync(this IRosSubscriber subscriber,
        CancellationToken token = default)
    {
        if (subscriber == null)
        {
            throw new ArgumentNullException(nameof(subscriber));
        }

        if (subscriber.NumActivePublishers != 0)
        {
            return;
        }
            
        using var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(token, subscriber.CancellationToken);
        while (subscriber.NumActivePublishers == 0)
        {
            await Task.Delay(200, linkedSource.Token);
        }
    }

    public static void WaitForTopic(this RosClient client, string topic, int timeoutInMs)
    {
        using CancellationTokenSource tokenSource = new(timeoutInMs);
        try
        {
            client.WaitForTopic(topic, tokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            throw new TimeoutException($"Wait for topic '{topic}' timed out");
        }
    }

    public static void WaitForTopic(this RosClient client, string topic, CancellationToken token = default)
    {
        if (client == null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        if (topic == null)
        {
            throw new ArgumentNullException(nameof(topic));
        }

        TaskUtils.Run(() => client.WaitForTopicAsync(topic, token).AsTask(), token).WaitAndRethrow();
    }

    public static async ValueTask WaitForTopicAsync(this RosClient client, string topic,
        CancellationToken token = default)
    {
        if (client == null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        if (topic == null)
        {
            throw new ArgumentNullException(nameof(topic));
        }

        while (true)
        {
            try
            {
                var result = await client.RosMasterClient.GetPublishedTopicsAsync(token: token);
                if (result.IsValid && result.Topics.Any(tuple => tuple.name == topic))
                {
                    return;
                }

                await Task.Delay(200, token);
            }
            catch (XmlRpcException)
            {
            }
        }
    }
}