using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;

namespace Iviz.Roslib
{
    public interface IRosClient : IDisposable
#if !NETSTANDARD2_0
        , IAsyncDisposable
#endif
    {
        /// <summary>
        /// ID of this node.
        /// </summary>
        string CallerId { get; }

        /// <summary>
        /// Advertises the given topic.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="publisher">
        /// The shared publisher for this topic, used by all advertisers from this client.
        /// Use this structure to publish messages to your topic.
        /// </param>
        /// <returns>An identifier that can be used to unadvertise from this publisher.</returns>        
        string Advertise<T>(string topic, out IRosPublisher<T> publisher) where T : IMessage;

        /// <summary>
        /// Advertises the given topic with the given dynamic message.
        /// The dynamic message must have been initialized beforehand.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="generator">The dynamic message containing the ROS definition.</param>
        /// <param name="publisher">
        /// The shared publisher for this topic, used by all advertisers from this client.
        /// Use this structure to publish messages to your topic.
        /// </param>
        /// <returns>An identifier that can be used to unadvertise from this publisher.</returns>
        string Advertise(string topic, DynamicMessage generator, out IRosPublisher<DynamicMessage> publisher);

        /// <summary>
        /// Advertises the given topic.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="token">An optional cancellation token</param>
        /// <returns>A pair containing an identifier that can be used to unadvertise from this publisher, and the publisher object.</returns>
        ValueTask<(string id, IRosPublisher<T> publisher)> AdvertiseAsync<T>(string topic,
            CancellationToken token = default)
            where T : IMessage;

        /// <summary>
        /// Advertises the given topic with the given dynamic message.
        /// The dynamic message must have been initialized beforehand.
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="generator">The dynamic message containing the ROS definition.</param>
        /// <param name="token">An optional cancellation token</param>
        /// <returns>A pair containing an identifier that can be used to unadvertise from this publisher, and the publisher object.</returns>
        ValueTask<(string id, IRosPublisher<DynamicMessage> publisher)> AdvertiseAsync(string topic,
            DynamicMessage generator, CancellationToken token = default);

        /// <summary>
        /// Subscribes to the given topic.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="callback">Function to be called when a message arrives.</param>
        /// <param name="subscriber"> The shared subscriber for this topic, used by all subscribers from this client. </param>
        /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
        /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
        /// <returns>An identifier that can be used to unsubscribe from this topic.</returns>
        string Subscribe<T>(string topic, Action<T> callback, out IRosSubscriber<T> subscriber,
            bool requestNoDelay = true, RosTransportHint transportHint = RosTransportHint.PreferTcp)
            where T : IMessage, IDeserializable<T>, new();

        /// <summary>
        /// Subscribes to the given topic. The subscriber will try to reconstruct the message based on information
        /// transmitted during handshake. The message type will be <see cref="DynamicMessage"/> if the message type is not known. 
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="callback">Function to be called when a message arrives.</param>
        /// <param name="subscriber"> The shared subscriber for this topic, used by all subscribers from this client. </param>
        /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
        /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
        /// <returns>A pair containing an identifier that can be used to unsubscribe from this topic, and the subscriber object.</returns>      
        string Subscribe(string topic, Action<IMessage> callback, out IRosSubscriber subscriber,
            bool requestNoDelay = true, RosTransportHint transportHint = RosTransportHint.PreferTcp);

        /// <summary>
        /// Subscribes to the given topic.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="callback">Function to be called when a message arrives.</param>
        /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
        /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
        /// <param name="token">An optional cancellation token</param>
        /// <returns>A pair containing an identifier that can be used to unsubscribe from this topic, and the subscriber object.</returns>        
        ValueTask<(string id, IRosSubscriber<T> subscriber)>
            SubscribeAsync<T>(string topic, Action<T> callback, bool requestNoDelay = true,
                RosTransportHint transportHint = RosTransportHint.PreferTcp, CancellationToken token = default)
            where T : IMessage, IDeserializable<T>, new();

        /// <summary>
        /// Subscribes to the given topic. The subscriber will try to reconstruct the message based on information
        /// transmitted during handshake. The message type will be <see cref="DynamicMessage"/> if the message type is not known. 
        /// </summary>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="callback">Function to be called when a message arrives.</param>
        /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
        /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
        /// <param name="token">An optional cancellation token</param>
        /// <returns>A pair containing an identifier that can be used to unsubscribe from this topic, and the subscriber object.</returns>        
        ValueTask<(string id, IRosSubscriber subscriber)>
            SubscribeAsync(string topic, Action<IMessage> callback, bool requestNoDelay = true,
                RosTransportHint transportHint = RosTransportHint.PreferTcp,
                CancellationToken token = default);

        /// <summary>
        /// Subscribes to the given topic. This variant uses a callback that includes information about
        /// the receiver socket and who sent the message.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="topic">Name of the topic.</param>
        /// <param name="callback">Function to be called when a message arrives.</param>
        /// <param name="requestNoDelay">Whether a request of NoDelay should be sent.</param>
        /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
        /// <param name="token">An optional cancellation token</param>
        /// <returns>A pair containing an identifier that can be used to unsubscribe from this topic, and the subscriber object.</returns>
        ValueTask<(string id, IRosSubscriber<T> subscriber)> SubscribeAsync<T>(
            string topic, RosCallback<T> callback, bool requestNoDelay = true,
            RosTransportHint transportHint = RosTransportHint.PreferTcp, CancellationToken token = default)
            where T : IMessage, IDeserializable<T>, new();

        /// <summary>
        /// Advertises the given service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service.</param>
        /// <param name="callback">Function to be called when a service request arrives. The response should be written in the response field.</param>
        /// <param name="token">An optional cancellation token.</param>
        /// <returns>Whether the advertisement was new. If false, the service already existed, but can still be used.</returns>
        /// <typeparam name="T">Service type.</typeparam>
        bool AdvertiseService<T>(string serviceName, Action<T> callback, CancellationToken token = default)
            where T : IService, new();

        /// <summary>
        /// Advertises the given service. The callback function may be async.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service.</param>
        /// <param name="callback">Async function to be called when a service request arrives. The response should be written in the response field.</param>
        /// <param name="token">An optional cancellation token.</param>
        /// <typeparam name="T">Service type.</typeparam>
        ValueTask<bool> AdvertiseServiceAsync<T>(string serviceName, Func<T, Task> callback,
            CancellationToken token = default) where T : IService, new();

        /// <summary>
        /// Calls the given ROS service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service</param>
        /// <param name="service">Service message. The response will be written in the response field.</param>
        /// <param name="persistent">Whether a persistent connection with the provider should be maintained.</param>
        /// <param name="timeoutInMs">Maximal time to wait.</param>
        /// <typeparam name="T">Service type.</typeparam>
        /// <returns>Whether the call succeeded.</returns>
        /// <exception cref="TaskCanceledException">The operation timed out.</exception>
        T CallService<T>(string serviceName, T service, bool persistent = false, int timeoutInMs = 5000)
            where T : IService;

        /// <summary>
        /// Calls the given ROS service.
        /// </summary>
        /// <param name="serviceName">Name of the ROS service</param>
        /// <param name="service">Service message. The response will be written in the response field.</param>
        /// <param name="persistent">
        /// Whether a persistent connection with the provider should be maintained.
        /// The connection will be stopped if any exception is thrown or the token is canceled.
        /// </param>
        /// <param name="token">A cancellation token</param>
        /// <typeparam name="T">Service type.</typeparam>
        /// <returns>Whether the call succeeded.</returns>
        ValueTask<T> CallServiceAsync<T>(string serviceName, T service, bool persistent = false,
            CancellationToken token = default)
            where T : IService;
    }
}