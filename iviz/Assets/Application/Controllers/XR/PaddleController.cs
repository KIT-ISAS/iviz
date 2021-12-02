#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers.XR
{
    public sealed class PaddleController : CustomController
    {
        [SerializeField] HandType handType;
        [SerializeField] float triggerAxisThreshold = 0.1f;
        
        public Pose? Pose { get; private set; }

        public PaddleController()
        {
            HasCursor = true;
        }
        
        protected override bool MatchesDevice(InputDeviceCharacteristics characteristics,
            List<InputFeatureUsage> usages)
        {
            var handednessFlag = handType == HandType.Left
                ? InputDeviceCharacteristics.Left
                : InputDeviceCharacteristics.Right;
            return HasFlag(characteristics, InputDeviceCharacteristics.Controller)
                   && HasFlag(characteristics, handednessFlag);
        }

        protected override void UpdateTrackingInput(XRControllerState? controllerState)
        {
            base.UpdateTrackingInput(controllerState);
            IsActiveInFrame = false;

            if (controllerState == null)
            {
                return;
            }

            controllerState.poseDataFlags = PoseDataFlags.NoData;

            if (!TryGetDevice(out var device)
                || !device.TryGetFeatureValue(CommonUsages.isTracked, out bool isTracking) 
                || !isTracking)
            {
                return;
            }

            if (device.TryGetFeatureValue(CommonUsages.deviceRotation, out controllerState.rotation))
            {
                controllerState.poseDataFlags |= PoseDataFlags.Rotation;
            }

            if (device.TryGetFeatureValue(CommonUsages.devicePosition, out controllerState.position))
            {
                controllerState.poseDataFlags |= PoseDataFlags.Position;
            }

            if (controllerState.poseDataFlags != (PoseDataFlags.Position | PoseDataFlags.Rotation))
            {
                return;
            }

            IsActiveInFrame = true;
            Pose = new Pose(controllerState.position, controllerState.rotation);
            
            if (device.TryGetFeatureValue(CommonUsages.trigger, out float squeeze))
            {
                bool newButtonState = squeeze > triggerAxisThreshold;
                ButtonDown = newButtonState && !ButtonState;
                ButtonUp = !newButtonState && ButtonState;
                ButtonState = newButtonState;
            }

        }

        /// <inheritdoc />
        protected override void UpdateInput(XRControllerState? controllerState)
        {
            base.UpdateInput(controllerState);
            if (controllerState == null)
            {
                return;
            }

            controllerState.ResetFrameDependentStates();
            controllerState.selectInteractionState.SetFrameState(ButtonState);
            controllerState.uiPressInteractionState.SetFrameState(ButtonState);
        }
    }
}