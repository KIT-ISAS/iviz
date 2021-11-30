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
        public IListener? Listener { get; protected set; }

        public abstract TfFrame? Frame { get; }

        public abstract IModuleData ModuleData { get; }

        public virtual void StopController()
        {
            Listener?.Stop();
        }

        public virtual void ResetController()
        {
            Listener?.Reset();
        }

        public abstract bool Visible { get; set; }

        public override string ToString() => $"[{GetType().Name}:'{Listener?.Topic}']";
    }
}