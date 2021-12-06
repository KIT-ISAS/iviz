#nullable enable

namespace Iviz.Common
{
    /// <summary>
    /// Common interface for controllers. The basic idea is 
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// The <see cref="ModuleData"/> of this controller.
        /// </summary>
        IModuleData ModuleData { get; }

        /// <summary>
        /// Tells the controller to finalize and dispose its resources. 
        /// </summary>
        void Dispose();

        /// <summary>
        /// Tells the controller to 'reset' itself.
        /// What this means is dependent on the controller.
        /// For example, it can erase its cache or removed stored information.
        /// </summary>
        void ResetController();
        
        bool Visible { get; set; }
    }
}