#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers.XR
{
    public enum HandType
    {
        Left,
        Right
    }

    public sealed class HandState
    {
        public Pose Palm { get; set; }
        public Ray? Cursor { get; set; }

        public Pose[][] Fingers { get; } =
        {
            Array.Empty<Pose>(),
            Array.Empty<Pose>(),
            Array.Empty<Pose>(),
            Array.Empty<Pose>(),
            Array.Empty<Pose>(),
        };

        public Pose ThumbFingertip => Fingers[0][^1];
        public Pose IndexFingertip => Fingers[1][^1];
        public SelectEnumerable<Pose[][], Pose[], Pose> Fingertips => Fingers.Select(GetTip);
        
        static readonly Func<Pose[], Pose> GetTip = finger => finger[^1]; 
    }

    public sealed class HandController : CustomController
    {
        static readonly InputFeatureUsage<Hand> HandData = CommonUsages.handData;
        static readonly InputFeatureUsage HandDataBase = (InputFeatureUsage)HandData;

        readonly List<Bone> cachedBoneList = new();
        readonly MeshMarkerResource?[] fingerTips = new MeshMarkerResource?[5];
        readonly MeshMarkerResource?[] fingerBases = new MeshMarkerResource?[5];
        readonly HandState cachedHandState = new();

        [SerializeField] HandType handType;
        [SerializeField] Transform? cameraTransform;
        [SerializeField] Vector3 shoulderToCamera = new(-0.1f, -0.2f, -0.05f);

        GameObject? modelRoot;

        public HandState? State { get; private set; }

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
            State = null;
            ModelVisible = false;
            IsActiveInFrame = false;
            HasCursor = false;

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

            var referenceTransform = transform.parent;

            if (!TryGetHandData(hand, referenceTransform))
            {
                return;
            }

            ModelVisible = true;
            IsActiveInFrame = true;
            State = cachedHandState;

            if (cameraTransform == null)
            {
                return;
            }

            HandleBoneMesh(ref fingerTips[0], cachedHandState.ThumbFingertip);
            HandleBoneMesh(ref fingerTips[1], cachedHandState.IndexFingertip);
            HandleBoneMesh(ref fingerTips[2], cachedHandState.Palm);


            var (palmPosition, palmRotation) = cachedHandState.Palm;

            const float palmOffset = 0.03f;
            var controllerPosition = palmPosition + palmRotation *
                ((handType == HandType.Left ? palmOffset : -palmOffset) * Vector3.right);
            var pivot = cameraTransform.TransformPoint(shoulderToCamera);
            var controllerForward = controllerPosition - pivot;
            var controllerRotation = Quaternion.LookRotation(controllerForward);

            controllerState.poseDataFlags = PoseDataFlags.Position | PoseDataFlags.Rotation;
            controllerState.position = referenceTransform.InverseTransformPoint(controllerPosition);
            controllerState.rotation = controllerRotation;

            HasCursor = Vector3.Dot(palmRotation * Vector3.up, cameraTransform.forward) < 0;
            cachedHandState.Cursor = HasCursor
                ? new Ray(controllerPosition, controllerForward.normalized)
                : null;
        }

        bool TryGetHandData(Hand hand, Transform referenceTransform)
        {
            if (!hand.TryGetRootBone(out var palm)
                || !palm.TryGetPosition(out var palmPosition)
                || !palm.TryGetRotation(out var palmRotation))
            {
                return false;
            }

            cachedHandState.Palm = referenceTransform.TransformPose(new Pose(palmPosition, palmRotation));

            foreach (int i in ..5)
            {
                var finger = (HandFinger)i;
                if (!hand.TryGetFingerBones(finger, cachedBoneList))
                {
                    return false;
                }

                Pose[] fingers;
                if (cachedHandState.Fingers[i].Length != cachedBoneList.Count)
                {
                    fingers = new Pose[cachedBoneList.Count];
                    cachedHandState.Fingers[i] = fingers;
                }
                else
                {
                    fingers = cachedHandState.Fingers[i];
                }

                foreach (int j in ..cachedBoneList.Count)
                {
                    if (!cachedBoneList[j].TryGetPosition(out var fingerPosition)
                        || !cachedBoneList[j].TryGetRotation(out var fingerRotation))
                    {
                        return false;
                    }

                    fingers[j] = referenceTransform.TransformPose(new Pose(fingerPosition, fingerRotation));
                }
            }

            return true;
        }


        /// <inheritdoc />
        protected override void UpdateInput(XRControllerState? controllerState)
        {
            const float distanceTransitionToOpen = 0.04f;
            const float distanceTransitionToClose = 0.025f;

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

            float distance = Vector3.Distance(thumb.Transform.position, index.Transform.position);
            bool newButtonState = distance < (ButtonState ? distanceTransitionToOpen : distanceTransitionToClose);

            ButtonDown = newButtonState && !ButtonState;
            ButtonUp = !newButtonState && ButtonState;
            ButtonState = newButtonState;

            controllerState.selectInteractionState.SetFrameState(ButtonState);
            controllerState.uiPressInteractionState.SetFrameState(ButtonState);


            // --------

            var color = ButtonState ? Color.white : Color.white.WithAlpha(0.3f);
            var scale = new Vector3(0.005f, 0.005f, 0.001f) * (ButtonState ? 2 : 1);

            thumb.Color = color;
            thumb.Transform.localScale = scale;
            index.Color = color;
            index.Transform.localScale = scale;
        }

        void HandleBoneMesh(ref MeshMarkerResource? resource, in Pose pose)
        {
            if (resource == null)
            {
                resource = CreateBoneObject();
            }

            resource.transform.SetPose(pose);
        }

        MeshMarkerResource CreateBoneObject()
        {
            var boneObject = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Reticle, RootTransform);
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