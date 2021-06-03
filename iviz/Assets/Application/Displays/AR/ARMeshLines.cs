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
            resource = ResourcePool.RentDisplay<LineResource>(transform);
            resource.ElementScale = 0.01f;

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
        }

        void Start()
        {
            WriteMeshLines();
        }

        void OnManagerChanged(ARMeshesChangedEventArgs args)
        {
            if (args.added.Contains(MeshFilter) || args.updated.Contains(MeshFilter))
            {
                WriteMeshLines();
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
            resource.SetDirect(DirectLineSetter);

            bool? DirectLineSetter(NativeList<float4x2> lineBuffer)
            {
                lineBuffer.Resize(indices.Count);
                switch (topology)
                {
                    case MeshTopology.Triangles:
                        for (int i = 0; i < indices.Count; i += 3)
                        {
                            Write(ref lineBuffer[i], vertices[i], vertices[i + 1]);
                            Write(ref lineBuffer[i + 1], vertices[i + 1], vertices[i + 2]);
                            Write(ref lineBuffer[i + 2], vertices[i + 2], vertices[i]);
                        }

                        break;
                    case MeshTopology.Quads:
                        for (int i = 0; i < indices.Count; i += 4)
                        {
                            Write(ref lineBuffer[i], vertices[i], vertices[i + 1]);
                            Write(ref lineBuffer[i + 1], vertices[i + 1], vertices[i + 2]);
                            Write(ref lineBuffer[i + 2], vertices[i + 2], vertices[i + 3]);
                            Write(ref lineBuffer[i + 3], vertices[i + 3], vertices[i]);
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
            if (Settings.ARCamera == null)
            {
                return;
            }

            var manager = Settings.ARCamera.GetComponent<ARMeshManager>();
            manager.meshesChanged -= OnManagerChanged;
            resource.ReturnToPool();
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