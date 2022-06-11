using System;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.ImageDecoders;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// This module is attached to the "iviz" object on the scene and exists only
    /// to clear static variables and call Dispose() on <see cref="ModuleListPanel"/> to finalize gracefully.
    /// </summary>
    public class ModuleInitializer : MonoBehaviour
    {
        void Awake()
        {
#if UNITY_EDITOR
            Settings.IsShuttingDown = false;
#endif
            // clear static stuff in case domain reloading is disabled
            // this is only really needed in the editor
            Settings.ClearResources();
            Resource.ClearResources();
            GuiWidgetListener.ClearResources();
            ARController.ClearResources();
            ResourcePool.ClearResources();
            GameThread.ClearResources();
        }

        void OnApplicationQuit()
        {
#if UNITY_EDITOR
            Settings.IsShuttingDown = true;
#endif
            ModuleListPanel.Instance.Dispose();
        }
    }
}