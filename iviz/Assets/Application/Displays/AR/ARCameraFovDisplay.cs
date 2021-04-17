using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Iviz.Displays
{
    public sealed class ARCameraFovDisplay : MonoBehaviour
    {
        [SerializeField] LineResource resource = null;
        [SerializeField] GameObject arCamera = null;
        bool isSetup;
        bool arActive;

        void Start()
        {
            if (resource == null)
            {
                resource = ResourcePool.RentDisplay<LineResource>(transform);
            }

            transform.SetParentLocal(TfListener.OriginFrame.Transform);
            ARController.ARModeChanged += OnARModeChanged;
            gameObject.SetActive(false);
        }

        void OnARModeChanged(bool newState)
        {
            arActive = newState;
            gameObject.SetActive(!arActive);
        }

        void Update()
        {
            if (arCamera == null)
            {
                return;
            }

            if (!isSetup)
            {
                ARCameraManager manager = arCamera.GetComponent<ARCameraManager>();
                if (manager.TryGetIntrinsics(out var intrinsics))
                {
                    ConstructCameraFrame(intrinsics);
                    isSetup = true;
                }
            }

            //Debug.Log(arCamera.transform.rotation);
            transform.SetLocalPose( ARController.RelativePoseToWorld(arCamera.transform.AsPose()));
        }

        void ConstructCameraFrame(in XRCameraIntrinsics intrinsics)
        {
            const float farClip = 0.5f;

            float minX = (0 - intrinsics.principalPoint.x) / intrinsics.focalLength.x * farClip;
            float maxX = (intrinsics.resolution.x - intrinsics.principalPoint.x) / intrinsics.focalLength.x * farClip;
            float minY = (0 - intrinsics.principalPoint.y) / intrinsics.focalLength.y * farClip;
            float maxY = (intrinsics.resolution.y - intrinsics.principalPoint.y) / intrinsics.focalLength.y * farClip;

            Vector3 a = new Vector3(minX, minY, farClip);
            Vector3 b = new Vector3(maxX, minY, farClip);
            Vector3 c = new Vector3(maxX, maxY, farClip);
            Vector3 d = new Vector3(minX, maxY, farClip);
            Vector3 o = Vector3.zero;

            using (var lines = new NativeList<LineWithColor>
            {
                new LineWithColor(o, a),
                new LineWithColor(o, b),
                new LineWithColor(o, c),
                new LineWithColor(o, d),
                new LineWithColor(a, b),
                new LineWithColor(b, c),
                new LineWithColor(c, d),
                new LineWithColor(d, a),
            })
            {
                resource.Set(lines);
                resource.Tint = Color.yellow;
                resource.ElementScale = 0.005f;
            }
        }

        void OnDestroy()
        {
            resource.ReturnToPool();
            ARController.ARModeChanged -= OnARModeChanged;
        }
    }
}