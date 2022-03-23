#nullable enable

using System;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Iviz.Displays
{
    public sealed class ARCameraFovDisplay : MeshMarkerDisplay
    {
        [SerializeField] LineDisplay? resource;
        [SerializeField] GameObject? arCamera;
        Transform? arCameraTransform;
        bool isSetup;
        bool arActive;

        GameObject ArCamera => arCamera.AssertNotNull(nameof(arCamera));

        LineDisplay Resource =>
            resource != null ? resource : (resource = ResourcePool.RentDisplay<LineDisplay>(Transform));

        Transform ArCameraTransform =>
            arCameraTransform != null ? arCameraTransform : (arCameraTransform = ArCamera.transform);

        void Start()
        {
            Color = Color.yellow;
            Transform.SetParentLocal(TfModule.OriginFrame.Transform);
            ARController.ARCameraViewChanged += OnARCameraViewChanged;
            gameObject.SetActive(ARController.Instance is not { Visible: true });
        }

        void OnARCameraViewChanged(bool newState)
        {
            arActive = newState;
            gameObject.SetActive(!arActive);
        }

        void Update()
        {
            if (!isSetup)
            {
                var manager = ArCamera.AssertHasComponent<ARCameraManager>(nameof(ArCamera));
                if (manager.TryGetIntrinsics(out var intrinsics))
                {
                    // may take a few frames
                    ConstructCameraFrame(intrinsics);
                    isSetup = true;
                }
            }

            var pose = ARController.ARPoseToUnity(ArCameraTransform.AsPose());
            Transform.SetLocalPose(pose);
        }

        void ConstructCameraFrame(in XRCameraIntrinsics intrinsics)
        {
            const float farClip = 0.5f;

            float minX = (0 - intrinsics.principalPoint.x) / intrinsics.focalLength.x * farClip;
            float maxX = (intrinsics.resolution.x - intrinsics.principalPoint.x) / intrinsics.focalLength.x * farClip;
            float minY = (0 - intrinsics.principalPoint.y) / intrinsics.focalLength.y * farClip;
            float maxY = (intrinsics.resolution.y - intrinsics.principalPoint.y) / intrinsics.focalLength.y * farClip;

            var a = new Vector3(minX, minY, farClip);
            var b = new Vector3(maxX, minY, farClip);
            var c = new Vector3(maxX, maxY, farClip);
            var d = new Vector3(minX, maxY, farClip);
            var o = Vector3.zero;

            ReadOnlySpan<LineWithColor> lines = stackalloc[]
            {
                new LineWithColor(o, a),
                new LineWithColor(o, b),
                new LineWithColor(o, c),
                new LineWithColor(o, d),
                new LineWithColor(a, b),
                new LineWithColor(b, c),
                new LineWithColor(c, d),
                new LineWithColor(d, a),
            };
            Resource.Set(lines, false);
            Resource.Tint = Color.yellow;
            Resource.ElementScale = 0.005f;
        }

        void OnDestroy()
        {
            resource.ReturnToPool();
            ARController.ARCameraViewChanged -= OnARCameraViewChanged;
        }
    }
}