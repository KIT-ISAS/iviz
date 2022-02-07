#nullable enable

using Iviz.Core;
using Iviz.Displays;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Iviz.Controllers
{
    public sealed class ARContents : MonoBehaviour
    {
        [SerializeField] Camera? arCamera;
        [SerializeField] ARSession? arSession;
        [SerializeField] ARSessionOrigin? arSessionOrigin;
        [SerializeField] Light? arLight;
        [SerializeField] ARCameraFovDisplay? fovDisplay;
        [SerializeField] MeshFilter? meshPrefab;
        [SerializeField] ARCameraManager? cameraManager;
        [SerializeField] AROcclusionManager? occlusionManager;
        [SerializeField] ARPlaneManager? planeManager;
        [SerializeField] ARRaycastManager? raycaster;
        [SerializeField] ARAnchorManager? anchorManager;

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