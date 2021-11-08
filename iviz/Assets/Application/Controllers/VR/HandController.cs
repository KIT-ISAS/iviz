#nullable enable

using System.Collections.Generic;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers
{
    public sealed class HandController : CustomController
    {
        static readonly InputFeatureUsage<Hand> HandData = CommonUsages.handData;
        static readonly InputFeatureUsage HandDataBase = (InputFeatureUsage)HandData;

        public enum HandType
        {
            Left,
            Right
        }

        readonly List<Bone> boneList = new();
        readonly Dictionary<Bone, MeshMarkerResource> bones = new();
        readonly MeshMarkerResource?[] fingerTips = new MeshMarkerResource?[5];
        readonly MeshMarkerResource?[] fingerBases = new MeshMarkerResource?[5];

        GameObject? modelRoot;
        [SerializeField] HandType handType;
        [SerializeField] Transform? cameraTransform;
        [FormerlySerializedAs("localPivot")] [SerializeField] Vector3 shoulderToCamera = new Vector3(-0.1f, -0.2f, -0.7f);

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

        /// <inheritdoc />
        protected override void UpdateTrackingInput(XRControllerState? controllerState)
        {
            base.UpdateTrackingInput(controllerState);
            if (controllerState == null)
            {
                return;
            }

            controllerState.poseDataFlags = PoseDataFlags.NoData;

            if (!TryGetDevice(out var device)
                || !device.TryGetFeatureValue(HandData, out var hand))
            {
                ModelVisible = false;
                return;
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
                || palmPose is not {} palmReference
                || fingerBases[0]?.Transform.AsPose() is not {} thumbReference)
            {
                return;
            }

            var controllerPosition = (palmReference.position + thumbReference.position) / 2; 
            var pivot = cameraTransform.TransformPoint(shoulderToCamera);
            var controllerRotation = Quaternion.LookRotation(controllerPosition - pivot);

            var cameraOffsetTransform = cameraTransform.parent;
            controllerState.poseDataFlags = PoseDataFlags.Position | PoseDataFlags.Rotation;
            controllerState.position = cameraOffsetTransform.InverseTransformPoint(controllerPosition);
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
            var emissive = isPressed ? Color.blue : Color.black;
            var scale = new Vector3(0.005f, 0.005f, 0.001f) * (isPressed ? 2 : 1);

            
            thumb.Color = color; 
            thumb.EmissiveColor = emissive; 
            thumb.Transform.localScale = scale;
            index.Color = color; 
            index.EmissiveColor = emissive;
            index.Transform.localScale = scale;
            
            controllerState.selectInteractionState.SetFrameState(isPressed);
            controllerState.uiPressInteractionState.SetFrameState(isPressed);
        }

        bool ModelVisible
        {
            set
            {
                if (modelRoot != null)
                {
                    modelRoot.SetActive(value);
                }
            }
        }

        MeshMarkerResource HandleBone(Bone bone)
        {
            MeshMarkerResource boneObject;
            if (bones.TryGetValue(bone, out var existingObject))
            {
                boneObject = existingObject;
            }
            else
            {
                if (modelRoot == null)
                {
                    modelRoot = new GameObject("Hand Model");
                    modelRoot.transform.SetParentLocal(transform.parent);
                }

                boneObject = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Sphere, modelRoot.transform);
                boneObject.transform.localScale = new Vector3(0.005f, 0.005f, 0.001f);
                boneObject.Color = Color.white.WithAlpha(0.3f);
                bones[bone] = boneObject;
            }

            var localPose = new Pose();
            if (bone.TryGetPosition(out localPose.position) && bone.TryGetRotation(out localPose.rotation))
            {
                boneObject.transform.SetLocalPose(localPose);
            }

            return boneObject;
        }
    }
}