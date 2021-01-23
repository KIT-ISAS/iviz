using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;

namespace Iviz.Roslib
{
    public static class Extensions
    {
        public static void WaitForService(this RosClient client, string service, int timeoutInMs = 5000)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            using CancellationTokenSource tokenSource = new CancellationTokenSource(timeoutInMs);
            Task.Run(() => client.WaitForServiceAsync(service, tokenSource.Token), tokenSource.Token)
                .WaitAndRethrow();
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

            while (!token.IsCancellationRequested)
            {
                try
                {
                    var result = await client.RosMasterApi.LookupServiceAsync(service, token);
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

        public static void WaitForAnySubscriber(this IRosPublisher publisher, int timeoutInMs = 5000)
        {
            if (publisher == null)
            {
                throw new ArgumentNullException(nameof(publisher));
            }

            using CancellationTokenSource tokenSource = new CancellationTokenSource(timeoutInMs);
            Task.Run(() => publisher.WaitForAnySubscriberAsync(tokenSource.Token), tokenSource.Token)
                .WaitAndRethrow();
        }

        public static async Task WaitForAnySubscriberAsync(this IRosPublisher publisher,
            CancellationToken token = default)
        {
            if (publisher == null)
            {
                throw new ArgumentNullException(nameof(publisher));
            }

            while (publisher.GetState().Senders.Count == 0)
            {
                await Task.Delay(200, token);
            }
        }

        public static void WaitForAnyPublisher(this IRosSubscriber subscriber, int timeoutInMs = 5000)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            using CancellationTokenSource tokenSource = new CancellationTokenSource(timeoutInMs);
            Task.Run(() => subscriber.WaitForAnyPublisherAsync(tokenSource.Token), tokenSource.Token)
                .WaitAndRethrow();
        }

        public static async Task WaitForAnyPublisherAsync(this IRosSubscriber subscriber,
            CancellationToken token = default)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            while (subscriber.GetState().Receivers.Count == 0)
            {
                await Task.Delay(200, token);
            }
        }

        public static void WaitForTopic(this RosClient client, string topic, int timeoutInMs = 5000)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            using CancellationTokenSource tokenSource = new CancellationTokenSource(timeoutInMs);
            Task.Run(() => client.WaitForTopicAsync(topic, tokenSource.Token), tokenSource.Token)
                .WaitAndRethrow();
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

            while (!token.IsCancellationRequested)
            {
                try
                {
                    var result = await client.RosMasterApi.GetPublishedTopicsAsync(token: token);
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