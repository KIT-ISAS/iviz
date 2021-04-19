﻿using Iviz.Resources;

namespace Iviz.Msgs.IvizCommonMsgs
{
    /// <summary>
    /// Common interface for the configuration classes of the controllers.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// GUID of the controller.
        /// </summary>
        string Id { get; set; }
        
        /// <summary>
        /// Module type of the controller. 
        /// </summary>
        Resource.ModuleType ModuleType { get; }
        
        /// <summary>
        /// Whether the controller is visible. 
        /// </summary>
        bool Visible { get; }
    }
}