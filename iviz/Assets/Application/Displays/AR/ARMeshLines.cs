#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Iviz.Controllers;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Tools;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Iviz.Displays
{
    // disabled until I can find a way to make it work in mobile suspend without crashing
    [RequireComponent(typeof(MeshFilter))]
    public sealed class ARMeshLines : DisplayWrapper, IRecyclable
    {
        static GameObject? container;

        readonly List<Vector3> vertices = new();
        readonly List<int> indices = new();

        bool pulseMaterialSet;
        bool dirty;

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
            GameThread.EverySecond += CheckMesh;

            OnARCameraViewChanged(ARController.IsXRVisible);
        }

        void OnARCameraViewChanged(bool value)
        {
            Resource.Visible = ARController.IsPulseActive || !value;
        }

        void OnMeshChanged(ARMeshesChangedEventArgs args)
        {
            if (args.added.Contains(MeshFilter) || args.updated.Contains(MeshFilter))
            {
                dirty = true;
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

        async void CheckMesh()
        {
            if (!dirty)
            {
                return;
            }

            dirty = false;

            var mesh = MeshFilter.sharedMesh;

            indices.Clear();
            mesh.GetIndices(indices, 0);

            vertices.Clear();
            mesh.GetVertices(vertices);

            var topology = mesh.GetTopology(0);
            if (topology != MeshTopology.Triangles)
            {
                return;
            }

            using var output = new NativeArray<float4x2>(indices.Count * 3, Allocator.TempJob);

            await new CopyTriangles
            {
                indices = indices.AsNativeArray().Cast<int, int3>(),
                vertices = vertices.AsNativeArray().Cast<Vector3, float3>(),
                output = output
            }.Schedule().AsTask();

            // we're in a different frame now!
            Resource.Set(output.AsReadOnlySpan(), false);
        }

        [BurstCompile(CompileSynchronously = true)]
        struct CopyTriangles : IJob
        {
            [ReadOnly] public NativeArray<int3> indices;
            [ReadOnly] public NativeArray<float3> vertices;
            [WriteOnly] public NativeArray<float4x2> output;

            public void Execute()
            {
                var output3 = output.Cast<float4x2, Float4x2x3>();
                
                for (int i = 0; i < indices.Length; i++)
                {
                    int ia = indices[i].x;
                    var a = vertices[ia];

                    int ib = indices[i].y;
                    var b = vertices[ib];

                    int ic = indices[i].z;
                    var c = vertices[ic];

                    Float4x2x3 f;
                    Write(out f.a, a, b);
                    Write(out f.b, b, c);
                    Write(out f.c, c, a);

                    output3[i] = f;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static void Write(out float4x2 f, in float3 a, in float3 b)
            {
                f.c0.x = a.x;
                f.c0.y = a.y;
                f.c0.z = a.z;
                f.c0.w = a.z;

                f.c1.x = b.x;
                f.c1.y = b.y;
                f.c1.z = b.z;
                f.c1.w = b.z;
            }

            struct Float4x2x3
            {
                public float4x2 a, b, c;
            }
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
            GameThread.EverySecond -= CheckMesh;

            Destroy(MeshFilter.sharedMesh);

            if (XRUtils.TryGetMeshManager(out var meshManager))
            {
                meshManager.meshesChanged -= OnMeshChanged;
            }
        }
    }
}