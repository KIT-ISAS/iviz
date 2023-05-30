#nullable enable
namespace Iviz.Controllers.XR
{
    public static class XRNames
    {
        public const string HeadFrameId = ARController.HeadFrameId;
        public const string LeftControllerFrameId = "~xr/left_controller";
        public const string RightControllerFrameId = "~xr/right_controller";
        public const string LeftHandFrameId = "~xr/left_hand";
        public const string RightHandFrameId = "~xr/right_hand";
        public const string GazeFrameId = "~xr/gaze";

        public const string LeftControllerTopic = LeftControllerFrameId;
        public const string RightControllerTopic = RightControllerFrameId;
        public const string LeftHandTopic = LeftHandFrameId;
        public const string RightHandTopic = RightHandFrameId;
        public const string GazeTopic = GazeFrameId;

        public const string MeshesTopic = ARController.MeshesTopic;
    }
}