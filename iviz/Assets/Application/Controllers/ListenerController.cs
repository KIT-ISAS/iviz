#nullable enable

using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Ros;

namespace Iviz.Controllers
{
    /// <summary>
    /// Parent class for all listener controllers, i.e., controllers that subscribe and process data from a ROS topic.
    /// </summary>
    public abstract class ListenerController : IController, IHasFrame
    {
        /// <summary>
        /// The ROS subscriber of this controller.
        /// </summary>
        public abstract IListener Listener { get; }
        
        /// <summary>
        /// The frame on which the visualizations are attached.
        /// </summary>
        public abstract TfFrame? Frame { get; }

        public virtual void Dispose()
        {
            Listener.Dispose();
        }

        public virtual void ResetController()
        {
        }

        public abstract bool Visible { get; set; }

        public override string ToString() => $"[{GetType().Name} '{Listener.Topic}']";
    }
}