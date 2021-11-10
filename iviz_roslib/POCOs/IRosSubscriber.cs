using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    /// <summary>
    /// Interface for all ROS subscribers.
    /// </summary>
    public interface IRosSubscriber : IDisposable
    {
        /// <summary>
        /// A cancellation token that gets canceled when the subscriber is disposed.
        /// Used for external wrappers like <see cref="RosChannelReader{T}"/>. 
        /// </summary>
        public CancellationToken CancellationToken { get; } 
        
        /// <summary>
        /// Timeout in milliseconds to wait for a publisher handshake.
        /// </summary>
        public int TimeoutInMs { get; set; }

        /// <summary>
        /// The name of the topic.
        /// </summary>        
        public string Topic { get; }

        /// <summary>
        /// The ROS message type of the topic.
        /// </summary>        
        public string TopicType { get; }

        /// <summary>
        /// The number of publishers in the topic. Includes connections in progress, which may or may not succeed.
        /// </summary>
        public int NumPublishers { get; }

        /// <summary>
        /// The number of publishers in the topic. Only includes established connections.
        /// </summary>
        public int NumActivePublishers { get; }

        /// <summary>
        /// Sets whether the subscriber is paused.
        /// When paused, the connections will still receive data, but will not process or parse it.
        /// </summary>
        public bool IsPaused { get; set; }
        
        /// <summary>
        /// Returns a structure that represents the internal state of the subscriber. 
        /// </summary>           
        public SubscriberTopicState GetState();

        /// <summary>
        /// Checks whether this subscriber has provided the given id from a Subscribe() call.
        /// </summary>
        /// <param name="id">Identifier to check.</param>
        /// <returns>Whether the id was provided by this subscriber.</returns>        
        public bool ContainsId(string id);

        /// <summary>
        /// Checks whether the class of the subscriber message type corresponds to the given type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>Whether the class type matches.</returns>
        public bool MessageTypeMatches(Type type);

        /// <summary>
        /// Generates a new subscriber id with the given callback function.
        /// </summary>
        /// <param name="callback">The function to call when a message arrives.</param>
        /// <returns>The subscribed id.</returns>
        /// <exception cref="ArgumentNullException">The callback is null.</exception>
        public string Subscribe(Action<IMessage> callback);

        /// <summary>
        /// Generates a new subscriber id with the given callback function.
        /// </summary>
        /// <param name="callback">The function to call when a message arrives.</param>
        /// <returns>The subscribed id.</returns>
        /// <exception cref="ArgumentNullException">The callback is null.</exception>
        public string Subscribe(Action<IMessage, IRosReceiver> callback);
 
        /// <summary>
        /// Unregisters the given id from the subscriber. If the subscriber has no ids left, the topic will be unsubscribed from the master.
        /// </summary>
        /// <param name="id">The id to be unregistered.</param>
        /// <returns>Whether the id belonged to the subscriber.</returns>        
        public bool Unsubscribe(string id);

        /// <summary>
        /// Unregisters the given id from the subscriber. If the subscriber has no ids left, the topic will be unsubscribed from the master.
        /// </summary>
        /// <param name="id">The id to be unregistered.</param>
        /// <param name="token">An optional cancellation token</param>
        /// <returns>Whether the id belonged to the subscriber.</returns>        
        public ValueTask<bool> UnsubscribeAsync(string id, CancellationToken token = default);

        /// <summary>
        /// Called by the ROS client to notify the subscriber that the list of publishers has changed.
        /// </summary>
        /// <param name="publisherUris">The new list of publishers.</param>
        /// <param name="token">A cancellation token</param>
        internal ValueTask PublisherUpdateRpcAsync(IEnumerable<Uri> publisherUris, CancellationToken token);

        public ValueTask DisposeAsync(CancellationToken token);
    }
    
    public delegate void RosCallback<T>(in T message, IRosReceiver info) where T : IMessage;

    public interface IRosSubscriber<T> : IRosSubscriber where T : IMessage
    {
        /// <summary>
        /// Generates a new subscriber id with the given callback function.
        /// </summary>
        /// <param name="callback">The function to call when a message arrives.</param>
        /// <returns>The subscribed id.</returns>
        /// <exception cref="ArgumentNullException">The callback is null.</exception>
        string Subscribe(Action<T> callback);
        
        /// <summary>
        /// Generates a new subscriber id with the given callback function.
        /// </summary>
        /// <param name="callback">The function to call when a message arrives.</param>
        /// <returns>The subscribed id.</returns>
        /// <exception cref="ArgumentNullException">The callback is null.</exception>        
        string Subscribe(RosCallback<T> callback);
        
        internal bool TryGetLoopbackReceiver(in Endpoint uri, out ILoopbackReceiver<T>? receiver);
    }    
}