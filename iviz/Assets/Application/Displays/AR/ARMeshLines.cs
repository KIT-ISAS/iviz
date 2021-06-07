using System;
using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Logger = Iviz.Core.Logger;

namespace Iviz.Displays
{
    [RequireComponent(typeof(MeshFilter))]
    public sealed class ARMeshLines : MonoBehaviour, IDisplay
    {
        readonly List<int> indices = new List<int>();
        readonly List<Vector3> vertices = new List<Vector3>();
        static readonly float White = PointWithColor.FloatFromColorBits(Color.white);

        Transform mTransform;
        [NotNull] Transform Transform => mTransform != null ? mTransform : (mTransform = transform);


        [CanBeNull] MeshFilter meshFilter;
        [NotNull] MeshFilter MeshFilter => meshFilter != null ? meshFilter : (meshFilter = GetComponent<MeshFilter>());

        LineResource resource;

        public Bounds? Bounds => resource.Bounds;

        public int Layer { get; set; }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        [NotNull]
        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        void Awake()
        {
            resource = ResourcePool.RentDisplay<LineResource>(TfListener.RootFrame.Transform);
            resource.ElementScale = 0.001f;
            resource.Visible = ARController.Instance != null && !ARController.Instance.Visible;

            MeshFilter.sharedMesh = new Mesh {name = "AR Mesh"};

            if (Settings.ARCamera != null)
            {
                var manager = Settings.ARCamera.GetComponent<ARMeshManager>();
                manager.meshesChanged += OnManagerChanged;
            }
            else
            {
                Logger.Warn("ARMeshLines: No mesh manager found!");
            }

            ARController.ARModeChanged += OnARModeChanged;
        }

        void Start()
        {
            WriteMeshLines();
        }

        void OnARModeChanged(bool value)
        {
            resource.Visible = !value;
        }

        void OnManagerChanged(ARMeshesChangedEventArgs args)
        {
            if (args.added.Contains(MeshFilter) || args.updated.Contains(MeshFilter))
            {
                WriteMeshLines();
            }
        }

        void Update()
        {
            if (resource.Visible)
            {
                var unityPose = ARController.RelativePoseToWorld(Transform.AsPose());
                resource.Transform.SetPose(unityPose);
            }
        }

        void WriteMeshLines()
        {
            var mesh = MeshFilter.sharedMesh;

            indices.Clear();
            mesh.GetIndices(indices, 0);

            vertices.Clear();
            mesh.GetVertices(vertices);

            var topology = mesh.GetTopology(0);
            int[] mIndices = indices.ExtractArray();
            Vector3[] mVertices = vertices.ExtractArray();

            resource.SetDirect(DirectLineSetter);

            bool? DirectLineSetter(NativeList<float4x2> lineBuffer)
            {
                int count = indices.Count;
                lineBuffer.Resize(count);
                switch (topology)
                {
                    case MeshTopology.Triangles:
                        for (int i = 0; i < count; i += 3)
                        {
                            Write(ref lineBuffer[i], mVertices[mIndices[i]], mVertices[mIndices[i + 1]]);
                            Write(ref lineBuffer[i + 1], mVertices[mIndices[i + 1]], mVertices[mIndices[i + 2]]);
                            Write(ref lineBuffer[i + 2], mVertices[mIndices[i + 2]], mVertices[mIndices[i]]);
                        }

                        break;
                    case MeshTopology.Quads:
                        for (int i = 0; i < count; i += 4)
                        {
                            Write(ref lineBuffer[i], mVertices[mIndices[i]], mVertices[mIndices[i + 1]]);
                            Write(ref lineBuffer[i + 1], mVertices[mIndices[i + 1]], mVertices[mIndices[i + 2]]);
                            Write(ref lineBuffer[i + 2], mVertices[mIndices[i + 2]], mVertices[mIndices[i + 3]]);
                            Write(ref lineBuffer[i + 3], mVertices[mIndices[i + 3]], mVertices[mIndices[i]]);
                        }

                        break;
                    default:
                        Logger.Debug("MeshToLinesHelper: Unknown topology " + topology);
                        break;
                }

                return false;
            }
        }

        public void Suspend()
        {
        }

        void OnDestroy()
        {
            resource.ReturnToPool();
            ARController.ARModeChanged -= OnARModeChanged;
            Destroy(MeshFilter.sharedMesh);

            if (Settings.ARCamera == null)
            {
                return;
            }

            var manager = Settings.ARCamera.GetComponent<ARMeshManager>();
            manager.meshesChanged -= OnManagerChanged;
        }

        static void Write(ref float4x2 f, in Vector3 a, in Vector3 b)
        {
            f.c0.x = a.x;
            f.c0.y = a.y;
            f.c0.z = a.z;
            f.c0.w = White;

            f.c1.x = b.x;
            f.c1.y = b.y;
            f.c1.z = b.z;
            f.c1.w = White;
        }
    }
}