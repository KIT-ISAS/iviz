using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

namespace Iviz.Controllers
{
    public class XRController : MonoBehaviour
    {
        static readonly InputFeatureUsage<Vector3> GazePosition = new("gazePosition");
        static readonly InputFeatureUsage<Quaternion> GazeRotation = new("gazeRotation");
        static readonly InputFeatureUsage<Hand> HandData = CommonUsages.handData;

        static readonly InputFeatureUsage GazePositionBase = (InputFeatureUsage) GazePosition;
        static readonly InputFeatureUsage GazeRotationBase = (InputFeatureUsage) GazeRotation;
        static readonly InputFeatureUsage HandDataBase = (InputFeatureUsage) HandData;

        enum InputsOfInterest
        {
            Invalid,
            LeftHand,
            RightHand,
            Gaze
        }

        readonly Dictionary<InputsOfInterest, InputDevice> devices = new();
        readonly List<InputFeatureUsage> usages = new();

        [SerializeField] GameObject camera;
        [SerializeField] GameObject cube;

        public void Awake()
        {
            InputDevices.deviceConnected += OnDeviceConnected;
            InputDevices.deviceDisconnected += OnDeviceDisconnected;
        }

        void OnDeviceConnected(InputDevice device)
        {
            if (HasFlag(device.characteristics, InputDeviceCharacteristics.EyeTracking))
            {
                usages.Clear();
                device.TryGetFeatureUsages(usages);

                if (usages.Contains(GazePositionBase) && usages.Contains(GazeRotationBase))
                {
                    devices[InputsOfInterest.Gaze] = device;
                }
            }
            else if (HasFlag(device.characteristics, InputDeviceCharacteristics.HandTracking))
            {
                usages.Clear();
                device.TryGetFeatureUsages(usages);
                bool leftHand = HasFlag(device.characteristics, InputDeviceCharacteristics.Left);
                if (usages.Contains(HandDataBase))
                {
                    devices[leftHand ? InputsOfInterest.LeftHand : InputsOfInterest.RightHand] = device;
                }
            }
        }

        static bool HasFlag(InputDeviceCharacteristics full, InputDeviceCharacteristics flag) => (full & flag) == flag;
        static bool HasFlag(InputTrackingState full, InputTrackingState flag) => (full & flag) == flag;

        void OnDeviceDisconnected(InputDevice device)
        {
            var deadDevice = devices.FirstOrDefault(pair => pair.Value == device).Key;
            if (deadDevice != InputsOfInterest.Invalid)
            {
                devices.Remove(deadDevice);
            }
        }

        void OnDestroy()
        {
            InputDevices.deviceConnected -= OnDeviceConnected;
        }

        void Update()
        {
            if (devices.TryGetValue(InputsOfInterest.Gaze, out var device)
                && device.TryGetFeatureValue(CommonUsages.trackingState, out var trackingState)
                && HasFlag(trackingState, InputTrackingState.Position)
                && HasFlag(trackingState, InputTrackingState.Rotation)
                && device.TryGetFeatureValue(GazePosition, out var position)
                && device.TryGetFeatureValue(GazeRotation, out var rotation))
            {
                Vector3 p = rotation * (2 * Vector3.forward);
                cube.transform.position = camera.transform.position + p;
            }
        }
    }
}