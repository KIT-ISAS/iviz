
using JetBrains.Annotations;

namespace Iviz.Controllers
{
    /// <summary>
    /// Interface to a ModuleData object.
    /// This interface exists only to ensure that controllers do not reference any classes outside of the Iviz.Controllers or Iviz.Displays namespaces.
    /// </summary>
    public interface IModuleData
    {
        /// <summary>
        /// Tells the ModuleData to re-read all information for the controller and update the panel, if active.  
        /// </summary>
        void ResetPanel();
        /// <summary>
        /// Tells the ModuleData to activate the panel that corresponds to this controller.
        /// </summary>
        void ShowPanel();
    }
    
    /// <summary>
    /// Common interface for controllers. The basic idea is 
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// The <see cref="ModuleData"/> of this controller.
        /// </summary>
        [NotNull] IModuleData ModuleData { get; }
        /// <summary>
        /// Tells the controller to finalize and dispose its resources. 
        /// </summary>
        void StopController();
        /// <summary>
        /// Tells the controller to 'reset' itself.
        /// What this means is dependent on the controller.
        /// For example, it can erase its cache or removed stored information.
        /// </summary>
        void ResetController();
    }
}
