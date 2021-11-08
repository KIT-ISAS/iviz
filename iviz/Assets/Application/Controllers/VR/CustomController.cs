#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers
{
    public sealed class GazeController : XRBaseController
    {
        static readonly InputFeatureUsage<Vector3> GazePosition = new("gazePosition");
        static readonly InputFeatureUsage<Quaternion> GazeRotation = new("gazeRotation");
        static bool HasFlag(InputDeviceCharacteristics full, InputDeviceCharacteristics flag) => (full & flag) == flag;

        InputDevice device;

        protected override void Awake()
        {
            base.Awake();
            InputDevices.deviceConnected += OnDeviceConnected;
            InputDevices.deviceDisconnected += OnDeviceDisconnected;
        }

        void OnDestroy()
        {
            InputDevices.deviceConnected -= OnDeviceConnected;
            InputDevices.deviceDisconnected -= OnDeviceDisconnected;
        }

        void OnDeviceConnected(InputDevice newDevice)
        {
            if (!HasFlag(newDevice.characteristics, InputDeviceCharacteristics.EyeTracking))
            {
                return;
            }

            var usages = new List<InputFeatureUsage>();
            newDevice.TryGetFeatureUsages(usages);

            if (usages.Contains((InputFeatureUsage) GazePosition) &&
                usages.Contains((InputFeatureUsage) GazeRotation))
            {
                device = newDevice;
            }
        }

        void OnDeviceDisconnected(InputDevice deadDevice)
        {
            if (deadDevice == device)
            {
                device = default;
            }
        }


        /// <inheritdoc />
        protected override void UpdateTrackingInput(XRControllerState? controllerState)
        {
            base.UpdateTrackingInput(controllerState);
            if (controllerState == null || !device.isValid)
            {
                return;
            }


            if (!device.TryGetFeatureValue(CommonUsages.trackingState, out var trackingState))
            {
                return;
            }

            if (HasFlag(trackingState, InputTrackingState.Rotation))
            {
                
            }
                && device.TryGetFeatureValue(GazeRotation, out var rotation))
            {
                Vector3 p = rotation * (2 * Vector3.forward);
                cube.transform.position = GetComponent<Camera>().transform.position + p;
            }



            //controllerState.poseDataFlags = PoseDataFlags.NoData;
            //controllerState.position = new Vector3(x, 0, 0);
            //controllerState.poseDataFlags |= PoseDataFlags.Position;

            /*
            {
                if (inputDevice.TryGetFeatureValue(CommonUsages.trackingState, out var trackingState))
                {
                    if ((trackingState & InputTrackingState.Position) != 0 &&
                        inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out var devicePosition))
                    {
                        controllerState.position = devicePosition;
                        controllerState.poseDataFlags |= PoseDataFlags.Position;
                    }

                    if ((trackingState & InputTrackingState.Rotation) != 0 &&
                        inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out var deviceRotation))
                    {
                        controllerState.rotation = deviceRotation;
                        controllerState.poseDataFlags |= PoseDataFlags.Rotation;
                    }
                }
            }
            */
        }

        /// <inheritdoc />
        protected override void UpdateInput(XRControllerState? controllerState)
        {
            base.UpdateInput(controllerState);
            controllerState?.ResetFrameDependentStates();
        }

        /// <inheritdoc />
        public override bool SendHapticImpulse(float amplitude, float duration)
        {
            return false;
        }
    }
}