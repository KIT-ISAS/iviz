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
    // disabled until I can find a way to make it work in mobile suspend without crashing!
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
                //resource.RenderType = LineDisplay.LineRenderType.AlwaysCapsule;
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
            GameThread.EveryTenthOfASecond += CheckMesh;

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

            if (mesh.vertexCount == 0)
            {
                Resource.Reset();
                return;
            }

            indices.Clear();
            mesh.GetIndices(indices, 0);

            vertices.Clear();
            mesh.GetVertices(vertices);

            var topology = mesh.GetTopology(0);
            if (topology != MeshTopology.Triangles)
            {
                return;
            }

            int numLines = indices.Count;  // 3 indices per triangle, 3 lines per triangle => 1 line per index
            using var output =
                new NativeArray<float4x2>(numLines, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

            await new CopyTriangles
            {
                triangles = indices.AsNativeArray().Cast<int, int3>(),
                vertices = vertices.AsNativeArray().Cast<Vector3, float3>(),
                output = output
            }.Schedule().AsTask();

            // we're in a different frame now!
            Resource.Set(output.AsReadOnlySpan(), false);
        }

        [BurstCompile(CompileSynchronously = true)]
        struct CopyTriangles : IJob
        {
            [ReadOnly] public NativeArray<int3> triangles;
            [ReadOnly] public NativeArray<float3> vertices;
            [WriteOnly] public NativeArray<float4x2> output;

            public void Execute()
            {
                var output3 = output.Cast<float4x2, Float4x2x3>();

                for (int i = 0; i < triangles.Length; i++)
                {
                    int ia = triangles[i].x;
                    var a = vertices[ia];

                    int ib = triangles[i].y;
                    var b = vertices[ib];

                    int ic = triangles[i].z;
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
                f.c0.w = a.z; // unused

                f.c1.x = b.x;
                f.c1.y = b.y;
                f.c1.z = b.z;
                f.c1.w = b.z; // unused
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
            GameThread.EveryTenthOfASecond -= CheckMesh;

            Destroy(MeshFilter.sharedMesh);

            if (XRUtils.TryGetMeshManager(out var meshManager))
            {
                meshManager.meshesChanged -= OnMeshChanged;
            }
        }
    }
}