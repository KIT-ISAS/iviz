#nullable enable

using Iviz.Core;
using Iviz.Displays;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Iviz.Controllers
{
    public sealed class ARContents : MonoBehaviour
    {
        [SerializeField] Camera? arCamera = null;
        [SerializeField] ARSession? arSession = null;
        [SerializeField] ARSessionOrigin? arSessionOrigin = null;
        [SerializeField] Light? arLight = null;
        [SerializeField] ARCameraFovDisplay? fovDisplay = null;
        [SerializeField] MeshFilter? meshPrefab = null;
        [SerializeField] ARCameraManager? cameraManager = null;
        [SerializeField] AROcclusionManager? occlusionManager = null;
        [SerializeField] ARPlaneManager? planeManager = null;
        [SerializeField] ARRaycastManager? raycaster = null;
        [SerializeField] ARAnchorManager? anchorManager = null;

        public Camera Camera => arCamera.AssertNotNull(nameof(arCamera));
        public Light ARLight => arLight.AssertNotNull(nameof(arLight));
        public ARSessionOrigin ARSessionOrigin => arSessionOrigin.AssertNotNull(nameof(arSessionOrigin));
        public ARSession Session => arSession.AssertNotNull(nameof(arSession));
        public ARCameraManager CameraManager => cameraManager.AssertNotNull(nameof(cameraManager));
        public AROcclusionManager OcclusionManager => occlusionManager.AssertNotNull(nameof(occlusionManager));
        public ARPlaneManager PlaneManager => planeManager.AssertNotNull(nameof(planeManager));
        public ARRaycastManager Raycaster => raycaster.AssertNotNull(nameof(raycaster));
        public ARAnchorManager AnchorManager => anchorManager.AssertNotNull(nameof(anchorManager));
        public MeshFilter? MeshPrefab => meshPrefab;
        public ARCameraFovDisplay? FovDisplay => fovDisplay;
    }
}