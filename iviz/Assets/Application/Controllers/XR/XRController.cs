using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Displays;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;

namespace Iviz.Controllers
{
    public class XRController : MonoBehaviour
    {
#if false
        void Update()
        {
            if (Time.time - time > 5)
            {
                time = Time.time;
                Debug.Log("Trigger!");
                ARMeshLines.TriggerPulse(Vector3.zero);
            }
        }

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
            //InputDevices.deviceConnected += OnDeviceConnected;
            //InputDevices.deviceDisconnected += OnDeviceDisconnected;
        }

        void OnDeviceConnected(InputDevice device)
        {
            Debug.Log(device.name);
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

                if (deadDevice == InputsOfInterest.LeftHand)
                {
                    ClearBones();
                }
            }
        }

        void OnDestroy()
        {
            InputDevices.deviceConnected -= OnDeviceConnected;
        }

        void GazeUpdate()
        {
            if (devices.TryGetValue(InputsOfInterest.Gaze, out var device)
                && device.TryGetFeatureValue(CommonUsages.trackingState, out var trackingState)
                && HasFlag(trackingState, InputTrackingState.Rotation)
                && device.TryGetFeatureValue(GazeRotation, out var rotation))
            {
                Vector3 p = rotation * (2 * Vector3.forward);
                cube.transform.position = camera.transform.position + p;
            }
        }

        readonly Dictionary<Bone, GameObject> bones = new();
        readonly List<Bone> boneList = new();

        void HandUpdate()
        {
            if (!devices.TryGetValue(InputsOfInterest.LeftHand, out var device)
                || !device.TryGetFeatureValue(HandData, out var hand))
            {
                return;
            }

            if (hand.TryGetRootBone(out var palm))
            {
                HandleBone(palm);
            }

            for (int i = 0; i < 5; i++)
            {
                var finger = (HandFinger) i;
                hand.TryGetFingerBones(finger, boneList);
                
                foreach (var bone in boneList)
                {
                    HandleBone(bone);
                }
            }

        }

        void HandleBone(Bone bone)
        {
            GameObject boneObject;
            if (bones.TryGetValue(bone, out var existingObject))
            {
                boneObject = existingObject;
            }
            else
            {
                boneObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                boneObject.transform.localScale = 0.01f * Vector3.one;
                bones[bone] = boneObject;
            }

            if (bone.TryGetPosition(out var position))
            {
                boneObject.transform.localPosition = position + 1.36f * Vector3.up;
            }
        }

        void ClearBones()
        {
            foreach (var pair in bones)
            {
                Destroy(pair.Value);
            }

            bones.Clear();
        }
#endif
    }
}