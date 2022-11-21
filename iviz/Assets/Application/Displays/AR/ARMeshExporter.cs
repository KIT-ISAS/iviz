#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Controllers;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.MeshMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Ros;
using Iviz.Tools;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Displays
{
    [RequireComponent(typeof(MeshFilter))]
    public sealed class ARMeshExporter : MonoBehaviour
    {
        static GameObject? container;

        static readonly List<Vector3> vertices = new();
        static readonly List<int> indices = new();

        static int numMeshes;
        static uint seqNr;
        static Sender<MeshGeometryStamped>? sender;

        MeshFilter? meshFilter;
        MeshFilter MeshFilter => this.EnsureHasComponent(ref meshFilter, nameof(meshFilter));

        void Awake()
        {
            if (XRUtils.TryGetMeshManager(out var meshManager))
            {
                meshManager.meshesChanged += OnMeshChanged;
            }
            else
            {
                RosLogger.Warn($"{nameof(ARMeshExporter)}: No mesh manager found!");
            }

            numMeshes++;
            sender ??= new Sender<MeshGeometryStamped>("~xr/environment/meshes");
        }

        void OnMeshChanged(ARMeshesChangedEventArgs args)
        {
            if (args.added.Contains(MeshFilter))
            {
                CheckMesh();
            }

            if (args.updated.Contains(MeshFilter))
            {
                CheckMesh();
            }

            if (args.removed.Contains(MeshFilter))
            {
                CheckMesh();
            }
        }

        void CheckMesh()
        {
            var mesh = MeshFilter.sharedMesh;

            vertices.Clear();
            indices.Clear();

            if (mesh.vertexCount == 0)
            {
                return;
            }

            var topology = mesh.GetTopology(0);
            if (topology != MeshTopology.Triangles)
            {
                return;
            }

            mesh.GetIndices(indices, 0);
            mesh.GetVertices(vertices);

            PublishState();
        }

        void PublishState()
        {
            if (sender == null) return;

            var newFaces = MemoryMarshal.Cast<int, TriangleIndices>(indices.AsSpan()).ToArray();

            var newVertices = new Point[vertices.Count];
            BurstUtils.ToPoint(vertices.AsSpan(), newVertices);

            var msg = new MeshGeometryStamped
            {
                Header = new Header(seqNr++, time.Now(), "map"),
                Uuid = gameObject.name,
                MeshGeometry = new MeshGeometry
                {
                    Faces = newFaces,
                    Vertices = newVertices
                }
            };

            sender.Publish(msg);
        }

        void OnDestroy()
        {
            if (XRUtils.TryGetMeshManager(out var meshManager))
            {
                meshManager.meshesChanged -= OnMeshChanged;
            }

            numMeshes--;
            if (numMeshes <= 0)
            {
                sender?.Dispose();
                sender = null;
            }
        }

        [BurstCompile]
        static unsafe class BurstUtils
        {
            [BurstCompile(CompileSynchronously = true)]
            static void ToPoint4([NoAlias] float3x4* input, [NoAlias] double3x4* output, int inputLength)
            {
                for (int i = 0; i < inputLength; i++)
                {
                    float3x4 input3 = input[i];

                    double3x4 output3;
                    output3.c0.x = input3.c0.z;
                    output3.c0.y = -input3.c0.x;
                    output3.c0.z = input3.c0.y;

                    output3.c1.x = input3.c1.z;
                    output3.c1.y = -input3.c1.x;
                    output3.c1.z = input3.c1.y;

                    output3.c2.x = input3.c2.z;
                    output3.c2.y = -input3.c2.x;
                    output3.c2.z = input3.c2.y;

                    output3.c3.x = input3.c3.z;
                    output3.c3.y = -input3.c3.x;
                    output3.c3.z = input3.c3.y;

                    output[i] = output3;
                }
            }

            [BurstCompile(CompileSynchronously = true)]
            static void ToPoint([NoAlias] float3* input, [NoAlias] double3* output, int inputLength)
            {
                for (int i = 0; i < inputLength; i++)
                {
                    float3 input3 = input[i];

                    double3 output3;
                    output3.x = input3.z;
                    output3.y = -input3.x;
                    output3.z = input3.y;

                    output[i] = output3;
                }
            }

            public static void ToPoint(Span<Vector3> input, Point[] output)
            {
                fixed (Vector3* inputPtr = input)
                fixed (Point* outputPtr = output)
                {
                    //ToPoint((float3*) inputPtr, (double3*) outputPtr, input.Length);

                    int length = input.Length;
                    int length4 = length / 4;
                    ToPoint4((float3x4*)inputPtr, (double3x4*)outputPtr, length4);

                    int offset = length4 * 4;
                    ToPoint((float3*)(inputPtr + offset), (double3*)(outputPtr + offset), length - offset);
                }
            }
        }
    }
}