using System.Collections.Generic;
using Iviz.Resources;
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
        bool isEnabled;

        public Color boundaryColor = Color.white;

        void Awake()
        {
            mesh = new Mesh();
            plane = GetComponent<ARPlane>();

            lines = Resource.Displays.GetOrCreate<LineResource>(transform);
            lines.Visible = false;
            lines.ElementSize = 0.005f;
            lines.Layer = gameObject.layer;

            MeshFilter meshFilter = GetComponent<MeshFilter>();
            meshFilter.sharedMesh = mesh;

            meshCollider = GetComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;

            meshRenderer = GetComponent<MeshRenderer>();

            plane.boundaryChanged += OnBoundaryChanged;

            isEnabled = true;
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
            List<LineWithColor> lineArray = new List<LineWithColor>();
            for (int i = 0; i < boundary.Length - 1; ++i)
            {
                a = boundary[i];
                b = boundary[i + 1];
                lineArray.Add(new LineWithColor(Project(a), Project(b)));
            }

            a = boundary[boundary.Length - 1];
            b = boundary[0];
            lineArray.Add(new LineWithColor(Project(a), Project(b)));

            lines.LinesWithColor = lineArray;
            lines.Visible = true;
            pulseStart = Time.time;
        }

        float? pulseStart;
        const float PulseLength = 0.25f;

        void Update()
        {
            bool shouldBeEnabled = plane.subsumedBy != null && plane.trackingState != TrackingState.None;
            if (isEnabled != shouldBeEnabled)
            {
                isEnabled = shouldBeEnabled;
                meshRenderer.enabled = isEnabled;
                lines.Visible = isEnabled;
                meshCollider.enabled = isEnabled;
            }

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
            Color color = new Color(boundaryColor.r, boundaryColor.g, boundaryColor.b, alpha);
            lines.Tint = color;
        }
    }
}