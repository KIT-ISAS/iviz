#nullable enable

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers.XR
{
    /// <summary>
    /// Custom controller for Gaze. Probably will be deprecated once the toolbox implements it. 
    /// </summary>
    public sealed class GazeController : CustomController
    {
        static readonly InputFeatureUsage<Vector3> GazePosition = new("gazePosition");
        static readonly InputFeatureUsage<Quaternion> GazeRotation = new("gazeRotation");

        public Pose? Pose { get; private set; }
        
        protected override bool MatchesDevice(InputDeviceCharacteristics characteristics,
            List<InputFeatureUsage> usages)
        {
            if (!enabled)
            {
                return false;
            }
            
            return HasFlag(characteristics, InputDeviceCharacteristics.EyeTracking)
                   && usages.Contains((InputFeatureUsage) GazePosition)
                   && usages.Contains((InputFeatureUsage) GazeRotation);
        }

        protected override void UpdateTrackingInput(XRControllerState? controllerState)
        {
            base.UpdateTrackingInput(controllerState);
            
            Pose = null;
            IsActiveInFrame = false;
            
            if (controllerState == null)
            {
                return;
            }

            //controllerState.poseDataFlags = PoseDataFlags.NoData;
            controllerState.inputTrackingState = InputTrackingState.None;

            if (!TryGetDevice(out var device) 
                || !device.TryGetFeatureValue(CommonUsages.isTracked, out bool isTracking) 
                || !isTracking)
            {
                return;
            }

            if (device.TryGetFeatureValue(GazeRotation, out controllerState.rotation))
            {
                //controllerState.poseDataFlags = PoseDataFlags.Rotation;
                controllerState.inputTrackingState = InputTrackingState.Rotation;
            }

            if (device.TryGetFeatureValue(GazePosition, out controllerState.position))
            {
                //controllerState.poseDataFlags |= PoseDataFlags.Position;
                controllerState.inputTrackingState |= InputTrackingState.Position;
            }

            //if (controllerState.poseDataFlags == (PoseDataFlags.Position | PoseDataFlags.Rotation))
            if (controllerState.inputTrackingState != (InputTrackingState.Position | InputTrackingState.Rotation))
            {
                IsActiveInFrame = true;
                
                var referenceTransform = transform.parent;
                Pose = referenceTransform.TransformPose(new Pose(controllerState.position, controllerState.rotation));
            }

        }

        protected override void UpdateInput(XRControllerState? controllerState)
        {
            base.UpdateInput(controllerState);
            controllerState?.ResetFrameDependentStates();
        }
    }
}