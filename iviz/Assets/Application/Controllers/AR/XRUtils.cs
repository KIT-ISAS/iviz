#nullable enable

using System.Diagnostics.CodeAnalysis;
using Iviz.Controllers.XR;
using UnityEngine.XR.ARFoundation;

namespace Iviz.Controllers
{
    public static class XRUtils
    {
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