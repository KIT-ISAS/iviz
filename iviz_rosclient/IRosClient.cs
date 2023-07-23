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
        where T : IMessage, new()
    {
        (string id, publisher) = TaskUtils.RunSync(() => AdvertiseAsync<T>(topic, latchingEnabled));
        return id;
    }

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
        IRosSubscriptionProfile? profile = null)
        where T : IMessage, new()
    {
        (string id, subscriber) = TaskUtils.RunSync(() => SubscribeAsync(topic, callback, profile));
        return id;
    }

    /// <summary>
    /// Subscribes to the given topic.
    /// </summary>
    /// <typeparam name="T">Message type.</typeparam>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="callback">Function to be called when a message arrives.</param>
    /// <param name="subscriber"> The shared subscriber for this topic, used by all subscribers from this client. </param>
    /// <param name="transportHint">Specifies the policy of which protocol (TCP, UDP) to prefer</param>
    /// <returns>An identifier that can be used to unsubscribe from this topic.</returns>
    string Subscribe<T>(string topic, RosCallback<T> callback, out IRosSubscriber<T> subscriber,
        IRosSubscriptionProfile? profile = null)
        where T : IMessage, new()
    {
        (string id, subscriber) = TaskUtils.RunSync(() => SubscribeAsync(topic, callback, profile));
        return id;
    }

    /// <summary>
    /// Subscribes to the given topic.
    /// </summary>
    /// <typeparam name="T">Message type.</typeparam>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="callback">Function to be called when a message arrives.</param>
    /// <param name="token">An optional cancellation token</param>
    /// <returns>A pair containing an identifier that can be used to unsubscribe from this topic, and the subscriber object.</returns>        
    ValueTask<(string id, IRosSubscriber<T> subscriber)>
        SubscribeAsync<T>(string topic, Action<T> callback, IRosSubscriptionProfile? profile = null,
            CancellationToken token = default)
        where T : IMessage, new();

    /// <summary>
    /// Subscribes to the given topic. This variant uses a callback that includes information about
    /// the receiver socket and who sent the message.
    /// </summary>
    /// <typeparam name="T">Message type.</typeparam>
    /// <param name="topic">Name of the topic.</param>
    /// <param name="callback">Function to be called when a message arrives.</param>
    /// <param name="token">An optional cancellation token</param>
    /// <returns>A pair containing an identifier that can be used to unsubscribe from this topic, and the subscriber object.</returns>
    ValueTask<(string id, IRosSubscriber<T> subscriber)> SubscribeAsync<T>(
        string topic, RosCallback<T> callback, IRosSubscriptionProfile? profile = null,
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
        where T : IService, new()
    {
        ValueTask Callback(T service)
        {
            callback(service);
            return default;
        }

        return TaskUtils.RunSync(() => AdvertiseServiceAsync<T>(serviceName, Callback, token), token);
    }

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
    void CallService<TRequest, TResponse>(string serviceName, IService<TRequest, TResponse> service,
        bool persistent = false, int timeoutInMs = 5000)
        where TRequest : IRequest
        where TResponse : IResponse
    {
        using var timeoutTs = new CancellationTokenSource(timeoutInMs);
        var token = timeoutTs.Token;
        TaskUtils.RunSync(() => CallServiceAsync(serviceName, service, persistent, token: token), token);
    }

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
    /// <returns>Whether the call succeeded.</returns>
    ValueTask CallServiceAsync<TRequest, TResponse>(string serviceName, IService<TRequest, TResponse> service,
        bool persistent = false, CancellationToken token = default)
        where TRequest : IRequest
        where TResponse : IResponse;

    /// <summary>
    /// Unadvertises the service.
    /// </summary>
    /// <param name="name">Name of the service</param>
    /// <param name="token">An optional cancellation token</param>
    /// <exception cref="ArgumentException">Thrown if name is null</exception>
    void UnadvertiseService(string name, CancellationToken token = default)
    {
        TaskUtils.RunSync(() => UnadvertiseServiceAsync(name, token), token);
    }

    /// <summary>
    /// Unadvertises the service.
    /// </summary>
    /// <param name="name">Name of the service</param>
    /// <param name="token">An optional cancellation token</param>
    /// <exception cref="ArgumentException">Thrown if name is null</exception>        
    ValueTask UnadvertiseServiceAsync(string name, CancellationToken token = default);

    IReadOnlyList<SubscriberState> GetSubscriberStatistics() => TaskUtils.RunSync(GetSubscriberStatisticsAsync);

    IReadOnlyList<PublisherState> GetPublisherStatistics() => TaskUtils.RunSync(GetPublisherStatisticsAsync);

    ValueTask<SubscriberState[]> GetSubscriberStatisticsAsync(CancellationToken token = default);

    ValueTask<PublisherState[]> GetPublisherStatisticsAsync(CancellationToken token = default);

    bool IsServiceAvailable(string service) => TaskUtils.RunSync(() => IsServiceAvailableAsync(service));

    ValueTask<bool> IsServiceAvailableAsync(string service, CancellationToken token = default);

    /// <summary>
    /// Asks the master for all the published topics in the system with at least one publisher.
    /// </summary>
    /// <returns>List of topic names and message types.</returns>
    TopicNameType[] GetSystemPublishedTopics() => TaskUtils.RunSync(GetSystemPublishedTopicsAsync);

    /// <summary>
    /// Asks the master for all the published topics in the system with at least one publisher.
    /// </summary>
    /// <returns>List of topic names and message types.</returns>
    ValueTask<TopicNameType[]> GetSystemPublishedTopicsAsync(CancellationToken token = default);

    /// <summary>
    /// Asks the master for all the topics in the system.
    /// Corresponds to the function 'getTopicTypes' in the ROS Master API.
    /// </summary>
    /// <returns>List of topic names and message types.</returns>
    TopicNameType[] GetSystemTopics() => TaskUtils.RunSync(GetSystemTopicsAsync);

    /// <summary>
    /// Asks the master for all the topics in the system.
    /// Corresponds to the function 'getTopicTypes' in the ROS Master API.
    /// </summary>
    /// <returns>List of topic names and message types.</returns>
    ValueTask<TopicNameType[]> GetSystemTopicsAsync(CancellationToken token = default);

    /// <summary>
    /// Asks the master for the nodes and topics in the system.
    /// Corresponds to the function 'getSystemState' in the ROS Master API.
    /// </summary>
    /// <returns>List of advertised topics, subscribed topics, and offered services, together with the involved nodes.</returns>
    SystemState GetSystemState() => TaskUtils.RunSync(GetSystemStateAsync);

    /// <summary>
    /// Asks the master for the nodes and topics in the system.
    /// Corresponds to the function 'getSystemState' in the ROS Master API.
    /// </summary>
    /// <returns>List of advertised topics, subscribed topics, and offered services, together with the involved nodes.</returns>
    ValueTask<SystemState> GetSystemStateAsync(CancellationToken token = default);

    /// <summary>
    /// Gets the parameter names from the default parameter server.
    /// ROS1: The ROS master
    /// ROS2: The calling node.
    /// </summary>
    /// <returns>Array of parameter names.</returns>
    string[] GetParameterNames() => TaskUtils.RunSync(GetParameterNamesAsync);

    /// <summary>
    /// Gets the parameter names from the default parameter server.
    /// ROS1: The ROS master
    /// ROS2: The calling node.
    /// </summary>
    /// <returns>Array of parameter names.</returns>
    ValueTask<string[]> GetParameterNamesAsync(CancellationToken token = default);

    /// <summary>
    /// Gets a parameter from the default parameter server.
    /// ROS1: The ROS master
    /// ROS2: The calling node.
    /// </summary>
    /// <param name="key">The key of the parameter.</param>
    /// <returns>A value wrapping the parameter value.</returns>
    RosValue GetParameter(string key) => TaskUtils.RunSync(() => GetParameterAsync(key));

    /// <summary>
    /// Gets a parameter from the default parameter server.
    /// ROS1: The ROS master
    /// ROS2: The calling node.
    /// </summary>
    /// <param name="key">The key of the parameter.</param>
    /// <param name="token">An optional cancellation token.</param>
    /// <returns>A value wrapping the parameter value.</returns>
    ValueTask<RosValue> GetParameterAsync(string key, CancellationToken token = default);

    /// <summary>
    /// Close this connection. Unsubscribes and unadvertises all topics and services.
    /// </summary>
    ValueTask DisposeAsync(CancellationToken token = default);

    void IDisposable.Dispose() => TaskUtils.RunSync((Func<ValueTask>)DisposeAsync);

    ValueTask IAsyncDisposable.DisposeAsync() => DisposeAsync();
}

public interface IRosSubscriptionProfile
{
}