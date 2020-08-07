using Iviz.Resources;
using System;

namespace Iviz.Controllers
{
    /// <summary>
    /// Common interface for the configuration classes of the controllers.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// GUID of the controller.
        /// </summary>
        Guid Id { get; }
        
        /// <summary>
        /// Module type of the controller. 
        /// </summary>
        Resource.Module Module { get; }
        
        /// <summary>
        /// Whether the controller is visible. 
        /// </summary>
        bool Visible { get; set; }
    }
}
