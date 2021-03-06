using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.XmlRpc;

namespace Iviz.Roslib
{
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
                throw new ArgumentNullException(nameof(client));
            }

            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            Task.Run(() => client.WaitForServiceAsync(service, token), token).WaitAndRethrow();
        }

        public static async Task WaitForServiceAsync(this RosClient client, string service,
            CancellationToken token = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
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

            Task.Run(() => publisher.WaitForAnySubscriberAsync(token), token).WaitAndRethrow();
        }

        public static async Task WaitForAnySubscriberAsync(this IRosPublisher publisher,
            CancellationToken token = default)
        {
            if (publisher == null)
            {
                throw new ArgumentNullException(nameof(publisher));
            }

            using var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(token, publisher.CancellationToken);

            while (publisher.GetState().Senders.Count == 0)
            {
                await Task.Delay(200, linkedSource.Token);
            }
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

            Task.Run(() => subscriber.WaitForAnyPublisherAsync(token), token).WaitAndRethrow();
        }

        public static async Task WaitForAnyPublisherAsync(this IRosSubscriber subscriber,
            CancellationToken token = default)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            using var linkedSource = CancellationTokenSource.CreateLinkedTokenSource(token, subscriber.CancellationToken);

            while (subscriber.GetState().Receivers.Count == 0)
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

            Task.Run(() => client.WaitForTopicAsync(topic, token), token).WaitAndRethrow();
        }

        public static async Task WaitForTopicAsync(this RosClient client, string topic,
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
}