#nullable enable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Iviz.Controllers;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Resources;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using XRUtils = Iviz.Controllers.XRUtils;

namespace Iviz.Displays
{
    [RequireComponent(typeof(MeshFilter))]
    public sealed class ARMeshLines : MonoBehaviour, IDisplay
    {
        static GameObject? container;

        bool writing;
        bool pulseMaterialSet;
        readonly List<int> indices = new();
        readonly List<Vector3> vertices = new();

        Transform? mTransform;
        MeshFilter? meshFilter;
        LineResource? resource;

        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);
        MeshFilter MeshFilter => meshFilter != null ? meshFilter : (meshFilter = GetComponent<MeshFilter>());

        LineResource Resource
        {
            get
            {
                if (resource != null)
                {
                    return resource;
                }

                if (container == null)
                {
                    container = new GameObject("AR Mesh Lines");
                }

                resource = ResourcePool.RentDisplay<LineResource>(container.transform);
                resource.ElementScale = 0.001f;
                resource.Visible = ARController.IsXRVisible;
                resource.MaterialOverride = ARController.IsPulseActive
                    ? Resources.Resource.Materials.LinePulse.Object
                    : Resources.Resource.Materials.LineMesh.Object;

                return resource;
            }
        }

        public Bounds? Bounds => Resource != null ? Resource.Bounds : null;

        public int Layer { get; set; }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        void Awake()
        {
            if (Settings.IsHololens)
            {
                enabled = false;
                return;
            }
            
            pulseMaterialSet = ARController.IsPulseActive;
            MeshFilter.sharedMesh = new Mesh { name = "AR Mesh" };
            Resource.Visible = Visible;

            if (XRUtils.TryGetMeshManager(out var meshManager))
            {
                meshManager.meshesChanged += OnMeshChanged;
            }
            else
            {
                RosLogger.Warn("ARMeshLines: No mesh manager found!");
            }

            ARController.ARCameraViewChanged += OnARCameraViewChanged;
            OnARCameraViewChanged(ARController.IsXRVisible);
        }

        void Start()
        {
            WriteMeshLines();
        }

        void OnARCameraViewChanged(bool value)
        {
            Resource.Visible = ARController.IsPulseActive || !value;
        }

        void OnMeshChanged(ARMeshesChangedEventArgs args)
        {
            if (args.added.Contains(MeshFilter) || args.updated.Contains(MeshFilter))
            {
                WriteMeshLines();
            }

            if (args.removed.Contains(MeshFilter))
            {
                Resource.Reset();
            }
        }

        void Update()
        {
            if (Resource.Visible)
            {
                var unityPose = ARController.ARPoseToUnity(Transform.AsPose());
                Resource.Transform.SetPose(unityPose);
            }

            bool hasPulse = ARController.IsPulseActive;
            if (hasPulse && !pulseMaterialSet)
            {
                Resource.MaterialOverride = Resources.Resource.Materials.LinePulse.Object;
                pulseMaterialSet = true;
                Resource.Visible = true;
            }
            else if (!hasPulse && pulseMaterialSet)
            {
                Resource.MaterialOverride = Resources.Resource.Materials.LineMesh.Object;
                pulseMaterialSet = false;
                Resource.Visible = !ARController.IsXRVisible;
            }
        }

        void WriteMeshLines()
        {
            if (writing)
            {
                Debug.LogError("Whee!");
            }

            writing = true;
            var mesh = MeshFilter.sharedMesh;

            indices.Clear();
            mesh.GetIndices(indices, 0);

            vertices.Clear();
            mesh.GetVertices(vertices);

            Resource.SetDirect(DirectLineSetter, indices.Count);
            writing = false;
        }

        bool? DirectLineSetter(NativeList<float4x2> lineBuffer)
        {
            var mesh = MeshFilter.sharedMesh;
            var topology = mesh.GetTopology(0);
            int[] mIndices = indices.ExtractArray();
            Vector3[] mVertices = vertices.ExtractArray();

            int count = indices.Count;
            lineBuffer.Resize(count);

            var lines = lineBuffer.AsSpan();
            switch (topology)
            {
                case MeshTopology.Triangles:
                    for (int i = 0; i < count; i += 3)
                    {
                        int ia = mIndices[i];
                        int ib = mIndices[i + 1];
                        int ic = mIndices[i + 2];

                        Vector3 a = mVertices[ia];
                        Vector3 b = mVertices[ib];
                        Vector3 c = mVertices[ic];

                        Write(ref lines[i], a, b);
                        Write(ref lines[i + 1], b, c);
                        Write(ref lines[i + 2], c, a);
                    }

                    break;
                case MeshTopology.Quads:
                    for (int i = 0; i < count; i += 4)
                    {
                        int ia = mIndices[i];
                        int ib = mIndices[i + 1];
                        int ic = mIndices[i + 2];
                        int id = mIndices[i + 3];

                        Vector3 a = mVertices[ia];
                        Vector3 b = mVertices[ib];
                        Vector3 c = mVertices[ic];
                        Vector3 d = mVertices[id];

                        Write(ref lines[i], a, b);
                        Write(ref lines[i + 1], b, c);
                        Write(ref lines[i + 2], c, d);
                        Write(ref lines[i + 3], d, a);
                    }

                    break;
                default:
                    RosLogger.Debug("MeshToLinesHelper: Unknown topology " + topology);
                    break;
            }

            return false;

            static void Write(ref float4x2 f, in Vector3 a, in Vector3 b)
            {
                (f.c0.x, f.c0.y, f.c0.z) = a;
                (f.c1.x, f.c1.y, f.c1.z) = b;
            }
        }

        public void Suspend()
        {
            indices.Clear();
            vertices.Clear();
        }

        void OnDestroy()
        {
            if (resource != null)
            {
                resource.ReturnToPool();
            }

            ARController.ARCameraViewChanged -= OnARCameraViewChanged;

            Destroy(MeshFilter.sharedMesh);

            if (XRUtils.TryGetMeshManager(out var meshManager))
            {
                meshManager.meshesChanged -= OnMeshChanged;
            }
        }
    }
}