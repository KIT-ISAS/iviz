#nullable enable
namespace Iviz.Controllers.XR
{
    public static class XRNames
    {
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

        public const string HeadFrameId = "~xr/head";
        public const string CameraFrameId = "~xr/camera";

        public const string ColorTopic = "~xr/color/image_color";
        public const string CameraInfoTopic = "~xr/color/camera_info";
        public const string DepthImageTopic = "~xr/depth/image";
        public const string DepthConfidenceTopic = "~xr/depth/image_confidence";
        public const string DepthCameraInfoTopic = "~xr/depth/camera_info";
        public const string MarkersTopic = "~xr/markers";

        public const string MeshesTopic = "~xr/environment/meshes";
    }
}