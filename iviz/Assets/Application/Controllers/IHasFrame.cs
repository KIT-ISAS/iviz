using Iviz.App;

namespace Iviz.Controllers
{
    /// <summary>
    /// Interface for all controllers whose visualization is tied to a transform frame.
    /// This is used, for example, by the <see cref="FrameWidget"/> of the module's panel.
    /// The frame is generally provided by the ROS header in the message.
    /// </summary>
    public interface IHasFrame
    {
        /// <summary>
        /// The transform frame associated to the controller.
        /// </summary>
        TfFrame Frame { get; }
    }
}
