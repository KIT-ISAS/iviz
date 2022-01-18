using Iviz.Common.Configurations;

#nullable enable

namespace Iviz.Common
{
    /// <summary>
    /// Common interface for controllers.
    /// A controller manages visualization displays, and, for listener controllers, the ROS communication.
    /// It exposes its variables with an <see cref="IConfiguration"/>.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Tells the controller to 'reset' itself.
        /// What this means is dependent on the controller.
        /// For example, it can erase its cache or removed stored information.
        /// </summary>
        void ResetController();
        
        /// <summary>
        /// Whether the controller's visualization is visible. 
        /// </summary>
        bool Visible { get; set; }
    }
}