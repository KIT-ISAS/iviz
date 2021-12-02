#nullable enable

using Iviz.Common;
using Iviz.Core;
using UnityEngine;

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
        public string Id { get; set; } = "";
        public ModuleType ModuleType => ModuleType.XR;
        public bool Visible { get; set; }
    }

    public sealed class XRMainController : MonoBehaviour
    {
        [SerializeField] GazeController? gaze;
        [SerializeField] HandController? leftHand;
        [SerializeField] HandController? rightHand;
        [SerializeField] PaddleController? leftPaddle;
        [SerializeField] PaddleController? rightPaddle;
        [SerializeField] Transform? cameraOffset;

        public GazeController Gaze => gaze.AssertNotNull(nameof(gaze));
        public HandController LeftHand => leftHand.AssertNotNull(nameof(leftHand));
        public HandController RightHand => rightHand.AssertNotNull(nameof(rightHand));
        public PaddleController LeftPaddle => leftPaddle.AssertNotNull(nameof(leftPaddle));
        public PaddleController RightPaddle => rightPaddle.AssertNotNull(nameof(rightPaddle));
        public Transform CameraOffset => cameraOffset.AssertNotNull(nameof(cameraOffset));
    }
}