#nullable enable

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace Iviz.Controllers.XR
{
    public static class XRUtils
    {
        /// <summary>
        /// Makes hololens-specific adjustments
        /// </summary>
        public static void SetupForHololens()
        {
            // make thread pool huge because ROS makes dozens of connections
            // quickly and the hololens 2 cannot cope. this makes the hololens
            // freeze out of nowhere for 1 or 2 seconds
            ThreadPool.SetMinThreads(25, 20);

#if WINDOWS_UWP

            // this enables rendering from pv camera to align holograms when recording videos
            // see https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/mixed-reality-capture-unity
            var kind = global::Windows.Graphics.Holographic.HolographicViewConfigurationKind.PhotoVideoCamera;
            var viewConfiguration =
                global::Windows.Graphics.Holographic.HolographicDisplay.GetDefault()?.TryGetViewConfiguration(kind);
            if (viewConfiguration != null)
            {
                viewConfiguration.IsEnabled = true;
            }
#endif
        }

        public static void ProcessCanvasForXR(this GameObject root)
        {
            ProcessCanvasForXR(root.transform);
        }

        public static void ProcessCanvasForXR(this Component root)
        {
            ProcessCanvasForXR(root.transform);
        }

        static void ProcessCanvasForXR(this Transform root)
        {
            // add a TrackedDeviceGraphicRaycaster to ensure the canvas can interact with the XRController 
            foreach (var subCanvas in root.GetAllChildren().WithComponent<Canvas>())
            {
                subCanvas.gameObject.TryAddComponent<TrackedDeviceGraphicRaycaster>();
            }

            // remove the RectMask2D added for hololens compatibility
            // this ensures that masking works when using 16-bit depth buffers
            // when using 24-bit buffers this is not necessary
            foreach (var mask in root.GetAllChildren().WithComponent<RectMask2D>())
            {
                Object.Destroy(mask);
            }
        }

        /// <summary>
        /// Retrieves the mesh manager for the current platform if it exists
        /// </summary>
        public static bool TryGetMeshManager([NotNullWhen(true)] out ARMeshManager? meshManager)
        {
            if (ARController.Instance != null)
            {
                // this is the mesh manager for ipads and iphones with depth cameras
                meshManager = ARController.Instance.MeshManager;
                return meshManager != null;
            }

            if (XRController.Instance != null)
            {
                // this is the mesh manager for the hololens
                meshManager = XRController.Instance.MeshManager;
                return meshManager != null;
            }

            meshManager = null;
            return false;
        }
    }
}