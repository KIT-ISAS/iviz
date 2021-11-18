using System.Collections.Generic;

namespace Iviz.Common
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

        /// <summary>
        /// Updates the configuration of the module
        /// </summary>
        /// <param name="configAsJson">The new configuration, encoded as JSON</param>
        /// <param name="fields">The fields in the configuration that are active.</param>
        void UpdateConfiguration(string configAsJson, IEnumerable<string> fields);
    }
}