using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib;

public interface IRosClient : IDisposable, IAsyncDisposable
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
    /// <param name="latchingEnabled">
    /// Whether latching is enabled. When active, new subscribers will automatically receive a copy of the last message sent.
    /// </param>
    /// <returns>An identifier that can be used to unadvertise from this publisher.</returns>        
    string Advertise<T>(string topic, out IRosPublisher<T> publisher, bool latchingEnabled = false)
        where T : IMessage, new();

    /// <summary>
    /// Advertises the given topic.
    /// </summary>
    /// <typeparam name="T">Message type.</typeparam>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="token">An optional cancellation token</param>
    /// <param name="latchingEnabled">
    /// Whether latching is enabled. When active, new subscribers will automatically receive a copy of the last message sent.
    /// </param> 
    /// <returns>A pair containing an identifier that can be used to unadvertise from this publisher, and the publisher object.</returns>
    ValueTask<(string id, IRosPublisher<T> publisher)> AdvertiseAsync<T>(string topic, bool latchingEnabled = false,
        CancellationToken token = default)
        where T : IMessage, new();

    /// <summary>
    /// Subscribes to the given topic.
    /// </summary>
    /// <typeparam name="T">Message type.</typeparam>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="callback">Function to be called when a message arrives.</param>
    /// <param name="subscriber"> The shared subscriber for this topic, used by all subscribers from this client. </param>
    /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
    /// <returns>An identifier that can be used to unsubscribe from this topic.</returns>
    string Subscribe<T>(string topic, Action<T> callback, out IRosSubscriber<T> subscriber,
        RosTransportHint transportHint = RosTransportHint.PreferTcp)
        where T : IMessage, new();

    /// <summary>
    /// Subscribes to the given topic.
    /// </summary>
    /// <typeparam name="T">Message type.</typeparam>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="callback">Function to be called when a message arrives.</param>
    /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
    /// <param name="token">An optional cancellation token</param>
    /// <returns>A pair containing an identifier that can be used to unsubscribe from this topic, and the subscriber object.</returns>        
    ValueTask<(string id, IRosSubscriber<T> subscriber)>
        SubscribeAsync<T>(string topic, Action<T> callback, RosTransportHint transportHint = RosTransportHint.PreferTcp,
            CancellationToken token = default)
        where T : IMessage, new();

    /// <summary>
    /// Subscribes to the given topic. This variant uses a callback that includes information about
    /// the receiver socket and who sent the message.
    /// </summary>
    /// <typeparam name="T">Message type.</typeparam>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="callback">Function to be called when a message arrives.</param>
    /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
    /// <param name="token">An optional cancellation token</param>
    /// <returns>A pair containing an identifier that can be used to unsubscribe from this topic, and the subscriber object.</returns>
    ValueTask<(string id, IRosSubscriber<T> subscriber)> SubscribeAsync<T>(
        string topic, RosCallback<T> callback, RosTransportHint transportHint = RosTransportHint.PreferTcp,
        CancellationToken token = default)
        where T : IMessage, new();

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
    ValueTask<bool> AdvertiseServiceAsync<T>(string serviceName, Func<T, ValueTask> callback,
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
    void CallService<T>(string serviceName, T service, bool persistent = false, int timeoutInMs = 5000)
        where T : IService, new();

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
    ValueTask CallServiceAsync<T>(string serviceName, T service, bool persistent = false,
        CancellationToken token = default)
        where T : IService, new();

    /// <summary>
    /// Unadvertises the service.
    /// </summary>
    /// <param name="name">Name of the service</param>
    /// <param name="token">An optional cancellation token</param>
    /// <exception cref="ArgumentException">Thrown if name is null</exception>
    void UnadvertiseService(string name, CancellationToken token = default);

    /// <summary>
    /// Unadvertises the service.
    /// </summary>
    /// <param name="name">Name of the service</param>
    /// <param name="token">An optional cancellation token</param>
    /// <exception cref="ArgumentException">Thrown if name is null</exception>        
    ValueTask UnadvertiseServiceAsync(string name, CancellationToken token = default);


    IReadOnlyList<SubscriberState> GetSubscriberStatistics();

    IReadOnlyList<PublisherState> GetPublisherStatistics();

    ValueTask<IReadOnlyList<SubscriberState>> GetSubscriberStatisticsAsync(CancellationToken token = default);

    ValueTask<IReadOnlyList<PublisherState>> GetPublisherStatisticsAsync(CancellationToken token = default);

    bool IsServiceAvailable(string service);

    ValueTask<bool> IsServiceAvailableAsync(string service, CancellationToken token = default);

    public TopicNameType[] GetSystemPublishedTopics();

    public ValueTask<TopicNameType[]> GetSystemPublishedTopicsAsync(CancellationToken token = default);

    public TopicNameType[] GetSystemTopics();

    public ValueTask<TopicNameType[]> GetSystemTopicsAsync(CancellationToken token = default);

    SystemState GetSystemState();

    ValueTask<SystemState> GetSystemStateAsync(CancellationToken token = default);

    string[] GetParameterNames();

    ValueTask<string[]> GetParameterNamesAsync(CancellationToken token = default);

    RosValue GetParameter(string key);

    ValueTask<RosValue> GetParameterAsync(string key, CancellationToken token = default);


    /// <summary>
    /// Close this connection. Unsubscribes and unadvertises all topics and services.
    /// </summary>
    ValueTask DisposeAsync(CancellationToken token = default);
}