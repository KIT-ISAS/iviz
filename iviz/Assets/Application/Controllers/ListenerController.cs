using UnityEngine;

namespace Iviz.Controllers
{
    /// <summary>
    /// Parent class for all listener controllers, i.e., controllers that subscribe and process data from a ROS topic.
    /// </summary>
    public abstract class ListenerController : IController, IHasFrame
    {
        /// <summary>
        /// The ROS subscriber of this controller. Only active after <see cref="StartListening"/> is called.
        /// </summary>
        public RosListener Listener { get; protected set; }

        public abstract TFFrame Frame { get; }

        public abstract IModuleData ModuleData { get; }

        /// <summary>
        /// Activates the listener and tells it to subscribe to the topic. 
        /// </summary>
        public abstract void StartListening();
        
        public virtual void Stop()
        {
            Listener?.Stop();
            Listener = null;
        }

        public virtual void Reset()
        {
            Debug.Log(this + ": Resetting!");
            Listener?.Reset();
        }

        public override string ToString()
        {
            return $"[{GetType().Name}:'{Listener?.Topic}']";
        }
    }
}