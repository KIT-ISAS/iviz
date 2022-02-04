#nullable enable

using System.Diagnostics.CodeAnalysis;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace Iviz.Controllers.XR
{
    public static class XRUtils
    {
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
            //foreach (var subCanvas in root.GetComponentsInChildren<Canvas>(true))
            foreach (var subCanvas in root.GetAllChildren().WithComponent<Canvas>())
            {
                subCanvas.gameObject.EnsureComponent<TrackedDeviceGraphicRaycaster>();
            }

//            foreach (var mask in root.GetComponentsInChildren<RectMask2D>(true))
            foreach (var mask in root.GetAllChildren().WithComponent<RectMask2D>())
            {
                Object.Destroy(mask);
            }
        }

        public static bool TryGetMeshManager([NotNullWhen(true)] out ARMeshManager? meshManager)
        {
            if (ARController.Instance != null)
            {
                meshManager = ARController.Instance.MeshManager;
                return meshManager != null;
            }

            if (XRController.Instance != null)
            {
                meshManager = XRController.Instance.MeshManager;
                return meshManager != null;
            }

            meshManager = null;
            return false;
        }
    }
}