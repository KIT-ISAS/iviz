#nullable enable

namespace Iviz.Controllers.TF
{
    /// <summary>
    /// Interface for all controllers whose visualization is tied to a transform frame.
    /// This is used, for example, by the frame widget of the module's panel.
    /// The frame is generally provided by the ROS header in the message.
    /// </summary>
    public interface IHasFrame
    {
        /// <summary>
        /// The transform frame associated to the controller.
        /// </summary>
        TfFrame? Frame { get; }
    }
}