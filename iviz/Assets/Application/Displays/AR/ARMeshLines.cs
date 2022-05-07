#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Iviz.Controllers;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Tools;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Iviz.Displays
{
    [RequireComponent(typeof(MeshFilter))]
    public sealed class ARMeshLines : DisplayWrapper, IRecyclable
    {
        static GameObject? container;
        static MeshQueueType? meshQueue;

        static MeshQueueType MeshQueue => meshQueue ??= new MeshQueueType();

        readonly List<float4x2> lines = new();
        bool pulseMaterialSet;

        MeshFilter? meshFilter;
        LineDisplay? resource;

        MeshFilter MeshFilter => this.EnsureHasComponent(ref meshFilter, nameof(meshFilter));

        LineDisplay Resource
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

                resource = ResourcePool.RentDisplay<LineDisplay>(container.transform);
                resource.ElementScale = 0.001f;
                resource.Visible = ARController.IsXRVisible;
                resource.MaterialOverride = ARController.IsPulseActive
                    ? Resources.Resource.Materials.LinePulse.Object
                    : Resources.Resource.Materials.LineMesh.Object;

                return resource;
            }
        }

        protected override IDisplay Display => Resource;

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
                RosLogger.Warn($"{nameof(ARMeshLines)}: No mesh manager found!");
            }

            ARController.ARCameraViewChanged += OnARCameraViewChanged;
            OnARCameraViewChanged(ARController.IsXRVisible);
        }

        /*
        void Start()
        {
            WriteMeshLines();
        }
        */

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

        void Process()
        {
            Resource.Set(lines.AsSpan(), false);
            lines.Clear();
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
            var indices = MeshQueue.indices;
            var vertices = MeshQueue.vertices;
            var mesh = MeshFilter.sharedMesh;

            indices.Clear();
            mesh.GetIndices(indices, 0);

            vertices.Clear();
            mesh.GetVertices(vertices);

            var topology = mesh.GetTopology(0);
            var mIndices = indices.AsSpan();
            var mVertices = vertices.AsSpan();

            int count = indices.Count;

            lines.Clear();
            lines.Capacity = Mathf.Max(lines.Capacity, count);

            float4x2 p0 = default;

            switch (topology)
            {
                case MeshTopology.Triangles:
                    for (int i = 0; i < count; i += 3)
                    {
                        int ia = mIndices[i];
                        ref readonly var a = ref mVertices[ia];

                        int ib = mIndices[i + 1];
                        ref readonly var b = ref mVertices[ib];
                        Write(ref p0, a, b);
                        lines.Add(p0);

                        int ic = mIndices[i + 2];
                        ref readonly var c = ref mVertices[ic];
                        Write(ref p0, b, c);
                        lines.Add(p0);

                        Write(ref p0, c, a);
                        lines.Add(p0);
                    }

                    break;
                case MeshTopology.Quads:
                    for (int i = 0; i < count; i += 4)
                    {
                        int ia = mIndices[i];
                        ref readonly var a = ref mVertices[ia];

                        int ib = mIndices[i + 1];
                        ref readonly var b = ref mVertices[ib];
                        Write(ref p0, a, b);
                        lines.Add(p0);

                        int ic = mIndices[i + 2];
                        ref readonly var c = ref mVertices[ic];
                        Write(ref p0, b, c);
                        lines.Add(p0);

                        int id = mIndices[i + 3];
                        ref readonly var d = ref mVertices[id];
                        Write(ref p0, c, d);
                        lines.Add(p0);

                        Write(ref p0, d, a);
                        lines.Add(p0);
                    }

                    break;
                default:
                    RosLogger.Debug($"{nameof(ARMeshLines)}: Unknown topology " + topology);
                    break;
            }

            MeshQueue.Add(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Write(ref float4x2 f, in Vector3 a, in Vector3 b)
        {
            f.c0.x = a.x;
            f.c0.y = a.y;
            f.c0.z = a.z;

            f.c1.x = b.x;
            f.c1.y = b.y;
            f.c1.z = b.z;
        }

        public override void Suspend()
        {
            lines.Clear();
        }

        public void SplitForRecycle()
        {
            resource.ReturnToPool();
            resource = null;
        }

        void OnDestroy()
        {
            resource.ReturnToPool();

            ARController.ARCameraViewChanged -= OnARCameraViewChanged;

            Destroy(MeshFilter.sharedMesh);

            if (XRUtils.TryGetMeshManager(out var meshManager))
            {
                meshManager.meshesChanged -= OnMeshChanged;
            }
        }

        sealed class MeshQueueType
        {
            public readonly List<Vector3> vertices = new();
            public readonly List<int> indices = new();

            readonly HashSet<ARMeshLines> lineSet = new();
            readonly Queue<ARMeshLines> lineQueue = new();

            public MeshQueueType()
            {
                GameThread.EveryFrame += Process;
                GameThread.ApplicationPause += OnApplicationPause;
            }

            void OnApplicationPause()
            {
                lineSet.Clear();
                lineQueue.Clear();
            }

            public void Add(ARMeshLines lines)
            {
                if (lineSet.Contains(lines))
                {
                    return;
                }

                lineQueue.Enqueue(lines);
                lineSet.Add(lines);
            }

            void Process()
            {
                foreach (int _ in ..5)
                {
                    if (!lineQueue.TryDequeue(out var lines))
                    {
                        return;
                    }

                    lineSet.Remove(lines);
                    if (lines == null)
                    {
                        continue;
                    }

                    lines.Process();
                }
            }
        }
    }
}