#nullable enable

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR;

namespace Iviz.Controllers
{
    /// <summary>
    /// Custom controller for Gaze. Probably will be deprecated once the toolbox implements it. 
    /// </summary>
    public sealed class GazeController : CustomController
    {
        static readonly InputFeatureUsage<Vector3> GazePosition = new("gazePosition");
        static readonly InputFeatureUsage<Quaternion> GazeRotation = new("gazeRotation");

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
            if (controllerState == null)
            {
                return;
            }

            controllerState.poseDataFlags = PoseDataFlags.NoData;

            if (!TryGetDevice(out var device))
            {
                return;
            }

            if (!device.TryGetFeatureValue(CommonUsages.isTracked, out bool isTracking) || !isTracking)
            {
                return;
            }
            
            if (device.TryGetFeatureValue(GazeRotation, out controllerState.rotation))
            {
                controllerState.poseDataFlags |= PoseDataFlags.Rotation;
            }

            if (device.TryGetFeatureValue(CommonUsages.deviceRotation, out controllerState.rotation))
            {
                controllerState.poseDataFlags |= PoseDataFlags.Rotation;
            }

            if (device.TryGetFeatureValue(GazePosition, out controllerState.position))
            {
                controllerState.poseDataFlags |= PoseDataFlags.Position;
            }
        }

        protected override void UpdateInput(XRControllerState? controllerState)
        {
            base.UpdateInput(controllerState);
            controllerState?.ResetFrameDependentStates();
        }
    }
}