using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace Iviz.Controllers
{
    public static class XRUtils
    {
        public static void ProcessCanvasForXR(this Canvas rootCanvas)
        {
            foreach (var subCanvas in rootCanvas.GetComponentsInChildren<Canvas>(true))
            {
                subCanvas.gameObject.AddComponent<TrackedDeviceGraphicRaycaster>();
            }
        }
    }
}