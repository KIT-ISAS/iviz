#nullable enable

using System.Text;
using Iviz.Roslib;

namespace Iviz.Ros
{
    /// <summary>
    /// A wrapper around a <see cref="RosSubscriber{T}"/> that persists even if the connection is interrupted.
    /// There can be multiple IListeners that refer to the same shared subscriber
    /// The original is stored in a <see cref="SubscribedTopic{T}"/>.  
    /// </summary>
    public interface IListener
    {
        string Topic { get; }
        string Type { get; }
        RosTransportHint TransportHint { get; }
        RosListenerStats Stats { get; }
        bool Subscribed { get; }
        
        void Dispose();
        
        /// <summary>
        /// Sets the suspended state of the listener.
        /// If suspended, the topic will be unsubscribed. If unsuspended, the topic will be resubscribed.  
        /// </summary>        
        void SetSuspend(bool value);
        
        /// <summary>
        /// Unsubscribes and resubscribes from the topic.
        /// </summary>
        void Reset();
        
        /// <summary>
        /// Sets the paused state of the subscriber.
        /// When paused, the subscriber will still receive data, but will not parse it or generate a message. 
        /// </summary>      
        void SetPause(bool value);
        void WriteDescriptionTo(StringBuilder b);
    }
}