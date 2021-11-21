#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers
{
    public enum HandType
    {
        Left,
        Right
    }

    public class HandState
    {
        public Pose palm;

        public readonly Pose[][] fingers =
        {
            Array.Empty<Pose>(),
            Array.Empty<Pose>(),
            Array.Empty<Pose>(),
            Array.Empty<Pose>(),
            Array.Empty<Pose>(),
        };
    }

    public sealed class HandController : CustomController
    {
        static readonly InputFeatureUsage<Hand> HandData = CommonUsages.handData;
        static readonly InputFeatureUsage HandDataBase = (InputFeatureUsage)HandData;

        readonly List<Bone> boneList = new();
        readonly MeshMarkerResource?[] fingerTips = new MeshMarkerResource?[5];
        readonly MeshMarkerResource?[] fingerBases = new MeshMarkerResource?[5];

        [SerializeField] HandType handType;
        [SerializeField] Transform? cameraTransform;
        [SerializeField] Vector3 shoulderToCamera = new(-0.1f, -0.2f, -0.05f);
        GameObject? modelRoot;

        readonly HandState cachedHandState = new();

        public HandState? HandState { get; private set; }

        protected override bool MatchesDevice(InputDeviceCharacteristics characteristics,
            List<InputFeatureUsage> usages)
        {
            var handednessFlag = handType == HandType.Left
                ? InputDeviceCharacteristics.Left
                : InputDeviceCharacteristics.Right;
            return HasFlag(characteristics, InputDeviceCharacteristics.HandTracking)
                   && HasFlag(characteristics, handednessFlag)
                   && usages.Contains(HandDataBase);
        }

        protected override void UpdateTrackingInput(XRControllerState? controllerState)
        {
            base.UpdateTrackingInput(controllerState);
            HandState = null;
            ModelVisible = false;

            if (controllerState == null)
            {
                return;
            }

            controllerState.poseDataFlags = PoseDataFlags.NoData;

            if (!TryGetDevice(out var device)
                || !device.TryGetFeatureValue(HandData, out var hand))
            {
                return;
            }

            if (!hand.TryGetRootBone(out var palm)
                || !palm.TryGetPosition(out cachedHandState.palm.position)
                || !palm.TryGetRotation(out cachedHandState.palm.rotation))
            {
                return;
            }

            for (int i = 0; i < 5; i++)
            {
                var finger = (HandFinger)i;
                if (!hand.TryGetFingerBones(finger, boneList))
                {
                    return;
                }

                ref var fingers = ref cachedHandState.fingers[i];
                if (fingers.Length != boneList.Count)
                {
                    fingers = new Pose[boneList.Count];
                }

                for (int j = 0; j < boneList.Count; j++)
                {
                    if (!boneList[j].TryGetPosition(out fingers[j].position)
                        || !boneList[j].TryGetRotation(out fingers[j].rotation))
                    {
                        return;
                    }
                }
            }

            ModelVisible = true;
            HandState = cachedHandState;

            /*
            for (int i = 0; i < 5; i++)
            {
                var finger = (HandFinger)i;
                if (!hand.TryGetFingerBones(finger, boneList))
                {
                    fingerTips[i] = null;
                    fingerBases[i] = null;
                    continue;
                }

                fingerTips[i] = HandleBone(boneList[^1]);
                fingerBases[i] = HandleBone(boneList[^3]);
            }

            ModelVisible = true;

            for (int i = 0; i < 5; i++)
            {
                var finger = (HandFinger)i;
                if (!hand.TryGetFingerBones(finger, boneList))
                {
                    fingerTips[i] = null;
                    fingerBases[i] = null;
                    continue;
                }

                fingerTips[i] = HandleBone(boneList[^1]);
                fingerBases[i] = HandleBone(boneList[^3]);
            }

            Pose? palmPose = hand.TryGetRootBone(out var palm)
                ? HandleBone(palm).transform.AsPose()
                : null;

            if (cameraTransform == null
                || palmPose is not { } palmReference
                || fingerBases[0]?.Transform.AsPose() is not { } thumbReference)
            {
                return;
            }
            */

            if (cameraTransform == null)
            {
                return;
            }

            for (int i = 0; i < 5; i++)
            {
                HandleBone(ref fingerTips[i], HandState.fingers[i][^1]);
                HandleBone(ref fingerBases[i], HandState.fingers[i][^3]);
            }


            var cameraOffset = transform.parent;

            var (palmPosition, palmRotation) = cameraOffset.TransformPose(HandState.palm);

            const float palmOffset = 0.03f;
            var controllerPosition = palmPosition + palmRotation *
                ((handType == HandType.Left ? palmOffset : -palmOffset) * Vector3.right);
            var pivot = cameraTransform.TransformPoint(shoulderToCamera);
            var controllerRotation = Quaternion.LookRotation(controllerPosition - pivot);

            controllerState.poseDataFlags = PoseDataFlags.Position | PoseDataFlags.Rotation;
            controllerState.position = cameraOffset.InverseTransformPoint(controllerPosition);
            controllerState.rotation = controllerRotation;
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

            if (fingerTips[0] is not { } thumb
                || fingerTips[1] is not { } index)
            {
                return;
            }

            bool isPressed = Vector3.Distance(thumb.Transform.position, index.Transform.position) < 0.025f;
            var color = isPressed ? Color.white : Color.white.WithAlpha(0.3f);
            var scale = new Vector3(0.005f, 0.005f, 0.001f) * (isPressed ? 2 : 1);

            thumb.Color = color;
            thumb.Transform.localScale = scale;
            index.Color = color;
            index.Transform.localScale = scale;

            controllerState.selectInteractionState.SetFrameState(isPressed);
            controllerState.uiPressInteractionState.SetFrameState(isPressed);
        }

        Transform RootTransform
        {
            get
            {
                if (modelRoot != null)
                {
                    return modelRoot.transform;
                }

                modelRoot = new GameObject(name + " Model");
                modelRoot.transform.SetParentLocal(transform.parent);
                modelRoot.SetActive(false);
                return modelRoot.transform;
            }
        }

        bool ModelVisible
        {
            set => RootTransform.gameObject.SetActive(value);
        }

        /*
        MeshMarkerResource HandleBone(Bone bone)
        {
            MeshMarkerResource boneObject;
            if (bones.TryGetValue(bone, out var existingObject))
            {
                boneObject = existingObject;
            }
            else
            {
                bones[bone] = boneObject = CreateBoneObject();
            }

            var localPose = new Pose();
            if (bone.TryGetPosition(out localPose.position) && bone.TryGetRotation(out localPose.rotation))
            {
                boneObject.transform.SetLocalPose(localPose);
            }

            return boneObject;
        }
        */

        void HandleBone(ref MeshMarkerResource? resource, in Pose pose)
        {
            if (resource == null)
            {
                resource = CreateBoneObject();
            }

            resource.transform.SetLocalPose(pose);
        }

        MeshMarkerResource CreateBoneObject()
        {
            var boneObject = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere, RootTransform);
            boneObject.transform.localScale = new Vector3(0.005f, 0.005f, 0.001f);
            boneObject.Color = Color.white.WithAlpha(0.3f);
            boneObject.EmissiveColor = Color.blue;
            return boneObject;
        }

        void OnDestroy()
        {
            if (modelRoot != null)
            {
                Destroy(modelRoot);
            }
        }
    }
}