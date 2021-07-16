using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Iviz.Displays
{
    public class ARPlaneResource : MonoBehaviour
    {
        Mesh mesh;
        MeshRenderer meshRenderer;
        MeshCollider meshCollider;
        ARPlane plane;
        LineResource lines;

        readonly NativeList<LineWithColor> lineArray = new NativeList<LineWithColor>();

        void Awake()
        {
            mesh = new Mesh();
            plane = GetComponent<ARPlane>();

            lines = ResourcePool.RentDisplay<LineResource>();
            lines.Visible = false;
            lines.ElementScale = 0.005f;
            lines.Tint = Color.yellow;

            MeshFilter meshFilter = GetComponent<MeshFilter>();
            meshFilter.sharedMesh = mesh;

            meshCollider = GetComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;

            meshRenderer = GetComponent<MeshRenderer>();

            //isEnabled = true;

            plane.boundaryChanged += OnBoundaryChanged;
            ARController.ARModeChanged += OnARModeChanged;
        }

        void OnARModeChanged(bool value)
        {
            lines.Visible = !value;
        }

        void OnBoundaryChanged(ARPlaneBoundaryChangedEventArgs _)
        {
            OnBoundaryChanged(plane.boundary);
        }

        void OnBoundaryChanged(in NativeArray<Vector2> boundary)
        {
            if (!ARPlaneMeshGenerators.GenerateMesh(mesh, transform.AsLocalPose(), boundary))
            {
                return;
            }

            Vector3 Project(in Vector2 v) => new Vector3(v.x, 0, v.y);

            Vector3 a, b;
            for (int i = 0; i < boundary.Length - 1; ++i)
            {
                a = boundary[i];
                b = boundary[i + 1];
                lineArray.Add(new LineWithColor(Project(a), Project(b)));
            }

            a = boundary[boundary.Length - 1];
            b = boundary[0];
            lineArray.Add(new LineWithColor(Project(a), Project(b)));
            lines.Set(lineArray);
        }

        //float? pulseStart;
        //const float PulseLength = 0.25f;

        void Update()
        {
            bool shouldBeVisible =
                plane != null && plane.subsumedBy == null && plane.trackingState != TrackingState.None;
            lines.Visible = shouldBeVisible;
            meshRenderer.enabled = shouldBeVisible;

            /*
            if (!isAREnabled)
            {
                lines.transform.SetLocalPose( ARController.RelativePoseToOrigin(transform.AsPose()));
            }

            bool shouldBeEnabled =
                plane != null && plane.subsumedBy == null && plane.trackingState != TrackingState.None;
            if (isEnabled != shouldBeEnabled)
            {
                isEnabled = shouldBeEnabled;
                meshRenderer.enabled = isEnabled;
                lines.Visible = isEnabled;
                meshCollider.enabled = isEnabled;
            }

            //Debug.Log("isEnabled: " + isEnabled);
            if (!isEnabled)
            {
                return;
            }

            if (pulseStart == null)
            {
                return;
            }

            float delta = Time.time - pulseStart.Value;
            if (delta > PulseLength)
            {
                pulseStart = null;
                lines.Visible = false;
                return;
            }

            float alpha = 1 - delta / PulseLength;
            lines.Tint = boundaryColor.WithAlpha(alpha);
            */
        }

        void OnDestroy()
        {
            lines.ReturnToPool();
            lines = null;
            ARController.ARModeChanged -= OnARModeChanged;
            if (plane != null)
            {
                plane.boundaryChanged -= OnBoundaryChanged;
            }

            lineArray.Dispose();
        }
    }
}