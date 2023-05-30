#nullable enable

using Iviz.Core.Configurations;

namespace Iviz.Common
{
    /// <summary>
    /// Common interface for controllers.
    /// A controller manages visualization displays, and, for listener controllers, the ROS communication.
    /// It exposes its variables with an <see cref="IConfiguration"/>.
    /// </summary>
    public abstract class Controller
    {
        /// <summary>
        /// Tells the controller to 'reset' itself.
        /// What this means is dependent on the controller.
        /// For example, it can erase its cache or removed stored information.
        /// </summary>
        public abstract void ResetController();
        
        /// <summary>
        /// Whether the controller's visualization is visible. 
        /// </summary>
        public abstract bool Visible { get; set; }
    }
}