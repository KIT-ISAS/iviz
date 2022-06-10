#nullable enable

using Iviz.Common;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays.XR;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/*
 * TODO:
 * reset console log
 * improve log
 * unselect description system info
 */

namespace Iviz.Controllers.XR
{
    public sealed class XRConfiguration : IConfiguration
    {
        public string Id { get; set; } = nameof(ModuleType.XR);
        public ModuleType ModuleType => ModuleType.XR;
        public bool Visible { get; set; }
        public float WorldScale { get; set; } = 1;
    }

    public sealed class XRContents : MonoBehaviour
    {
        [SerializeField] GameObject? hololensRig;
        [SerializeField] ARMeshManager? meshManager;
        [SerializeField] ARAnchorManager? anchorManager;
        [SerializeField] GazeController? gaze;
        [SerializeField] HandController? leftHand;
        [SerializeField] HandController? rightHand;
        [SerializeField] PaddleController? leftPaddle;
        [SerializeField] PaddleController? rightPaddle;
        [SerializeField] Transform? cameraOffset;
        [SerializeField] CanvasHolder? canvasHolder;

        [SerializeField] GameObject? manipulableFrame;
        [SerializeField] ButtonBar? frameButtonBar;

        public GameObject HololensRig => hololensRig.AssertNotNull(nameof(hololensRig)); 
        public ARMeshManager MeshManager => meshManager.AssertNotNull(nameof(meshManager)); 
        public ARAnchorManager AnchorManager => anchorManager.AssertNotNull(nameof(anchorManager)); 
        public GazeController Gaze => gaze.AssertNotNull(nameof(gaze));
        public HandController LeftHand => leftHand.AssertNotNull(nameof(leftHand));
        public HandController RightHand => rightHand.AssertNotNull(nameof(rightHand));
        public PaddleController LeftPaddle => leftPaddle.AssertNotNull(nameof(leftPaddle));
        public PaddleController RightPaddle => rightPaddle.AssertNotNull(nameof(rightPaddle));
        public Transform CameraOffset => cameraOffset.AssertNotNull(nameof(cameraOffset));
        public CanvasHolder CanvasHolder => canvasHolder.AssertNotNull(nameof(canvasHolder));
        public GameObject ManipulableFrame => manipulableFrame.AssertNotNull(nameof(manipulableFrame));
        public ButtonBar FrameButtonBar => frameButtonBar.AssertNotNull(nameof(frameButtonBar));
    }
}