#nullable enable

using UnityEngine.SpatialTracking;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers.XR
{
    /// <summary>
    /// Custom controller for Gaze. Probably will be deprecated once the toolbox implements it. 
    /// </summary>
    public sealed class IndexController : XRBaseController
    {
        /*
        protected override void UpdateTrackingInput(XRControllerState? controllerState)
        {
            base.UpdateTrackingInput(controllerState);

            controllerState.poseDataFlags = PoseDataFlags.NoData;
            controllerState.poseDataFlags |= PoseDataFlags.Rotation;
            controllerState.poseDataFlags |= PoseDataFlags.Position;
            //Pose = referenceTransform.TransformPose(new Pose(controllerState.position, controllerState.rotation));
        }
        */
    }
}