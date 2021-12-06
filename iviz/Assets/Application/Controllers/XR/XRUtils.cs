#nullable enable

using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace Iviz.Controllers.XR
{
    public static class XRUtils
    {
        public static void ProcessCanvasForXR(this Transform root)
        {
            foreach (var subCanvas in root.GetComponentsInChildren<Canvas>(true))
            {
                subCanvas.gameObject.EnsureComponent<TrackedDeviceGraphicRaycaster>();
            }
            
            foreach (var mask in root.GetComponentsInChildren<RectMask2D>(true))
            {
                Object.Destroy(mask);
            }            
        }
    }
}