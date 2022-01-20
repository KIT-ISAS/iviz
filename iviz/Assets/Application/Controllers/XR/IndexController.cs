#nullable enable

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR;

namespace Iviz.Controllers.XR
{
    /// <summary>
    /// Custom controller for Gaze. Probably will be deprecated once the toolbox implements it. 
    /// </summary>
    public sealed class IndexController : XRBaseController
    {
        protected override void UpdateTrackingInput(XRControllerState? controllerState)
        {
            base.UpdateTrackingInput(controllerState);

            controllerState.poseDataFlags = PoseDataFlags.NoData;

            controllerState.poseDataFlags |= PoseDataFlags.Rotation;
            controllerState.poseDataFlags |= PoseDataFlags.Position;
            //Pose = referenceTransform.TransformPose(new Pose(controllerState.position, controllerState.rotation));
        }
    }
}