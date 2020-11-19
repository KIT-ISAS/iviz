using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    /// <summary>
    /// Interface for all ROS publishers.
    /// </summary>
    public interface IRosPublisher : IDisposable
    {
        /// <summary>
        /// A cancellation token that gets canceled when the publisher is disposed.
        /// </summary>
        public CancellationToken CancellationToken { get; } 
        
        /// <summary>
        /// The name of the topic.
        /// </summary>
        public string Topic { get; }

        /// <summary>
        /// The ROS message type of the topic.
        /// </summary>        
        public string TopicType { get; }

        /// <summary>
        /// Timeout in milliseconds to wait for a subscriber handshake.
        /// </summary>             
        public int TimeoutInMs { get; set; }

        /// <summary>
        /// The number of publishers in the topic.
        /// </summary>
        public int NumSubscribers { get; }

        /// <summary>
        /// Publishes the given message into the topic. 
        /// </summary>
        /// <param name="message">The message to be published.</param>
        /// <exception cref="ArgumentNullException">The message is null</exception>
        /// <exception cref="InvalidMessageTypeException">The message type does not match.</exception>          
        public void Publish(IMessage message);

        /// <summary>
        /// Unregisters the given id from the publisher. If the publisher has no ids left, the topic will be unadvertised from the master.
        /// </summary>
        /// <param name="id">The id to be unregistered.</param>
        /// <returns>Whether the id belonged to the publisher.</returns>        
        public bool Unadvertise(string id);

        /// <summary>
        /// Unregisters the given id from the publisher. If the publisher has no ids left, the topic will be unadvertised from the master.
        /// </summary>
        /// <param name="id">The id to be unregistered.</param>
        /// <returns>Whether the id belonged to the publisher.</returns>
        public Task<bool> UnadvertiseAsync(string id);

        /// <summary>
        /// Generates a new advertisement id. Use this string for Unadvertise().
        /// </summary>
        /// <returns>The advertisement id.</returns>
        public string Advertise();

        /// <summary>
        /// Checks whether this publisher has provided the given id from an Advertise() call.
        /// </summary>
        /// <param name="id">Identifier to check.</param>
        /// <returns>Whether the id was provided by this publisher.</returns>
        public bool ContainsId(string id);

        /// <summary>
        /// Checks whether this publisher's message type corresponds to the given type
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>Whether the class type matches.</returns>        
        public bool MessageTypeMatches(Type type);

        /// <summary>
        /// Returns a structure that represents the internal state of the publisher. 
        /// </summary>        
        public PublisherTopicState GetState();

        internal Endpoint? RequestTopicRpc(string remoteCallerId);
    }
    
    public interface IRosPublisher<in T> : IRosPublisher where T : IMessage
    {
        public void Publish(T message);
    }

}