using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib;

public static class Extensions
{
    public static void WaitForService(this IRosClient client, string service, int timeoutInMs)
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

    public static void WaitForService(this IRosClient client, string service, CancellationToken token = default)
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

    public static async ValueTask WaitForServiceAsync(this IRosClient client, string service,
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
            if (await client.IsServiceAvailableAsync(service, token))
            {
                return;
            }

            await Task.Delay(200, token);
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

        if (subscriber.NumPublishers != 0)
        {
            return;
        }

        using var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(token, subscriber.CancellationToken);
        while (subscriber.NumPublishers == 0)
        {
            await Task.Delay(200, linkedSource.Token);
        }
    }

    public static void WaitForTopic(this IRosClient client, string topic, int timeoutInMs)
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

    public static void WaitForTopic(this IRosClient client, string topic, CancellationToken token = default)
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

    public static async ValueTask WaitForTopicAsync(this IRosClient client, string topic,
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
            var result = await client.GetSystemPublishedTopicsAsync(token);
            if (result.Any(tuple => tuple.Topic == topic))
            {
                return;
            }

            await Task.Delay(200, token);
        }
    }
}