#nullable enable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Iviz.Displays
{
    [RequireComponent(typeof(MeshFilter))]
    public sealed class ARMeshLines : MonoBehaviour, IDisplay
    {
        static readonly int PulseCenter = Shader.PropertyToID("_PulseCenter");
        static readonly int PulseTime = Shader.PropertyToID("_PulseTime");
        static readonly int PulseDelta = Shader.PropertyToID("_PulseDelta");

        static float? pulseStart;
        static GameObject? container;

        static bool TryGetMeshManager([NotNullWhen(true)] out ARMeshManager? meshManager)
        {
            if (ARController.Instance != null)
            {
                meshManager = ARController.Instance.MeshManager;
                return meshManager != null;
            }

            meshManager = null;
            return false;
        }

        public static void TriggerPulse(in Vector3 start)
        {
            bool hasPulse = pulseStart != null;
            pulseStart = Time.time;
            if (!hasPulse)
            {
                GameThread.EveryFrame += UpdateStatic;
                Debug.Log("adding to everyframe");
            }

            var material = Resources.Resource.Materials.LinePulse.Object;
            material.SetVector(PulseCenter, start);
            material.SetFloat(PulseDelta, 0.25f);
        }

        static void UpdateStatic()
        {
            if (pulseStart == null)
            {
                return;
            }

            float timeDiff = Time.time - pulseStart.Value;
            if (timeDiff > 10)
            {
                pulseStart = null;
                GameThread.EveryFrame -= UpdateStatic;
                return;
            }

            var material = Resources.Resource.Materials.LinePulse.Object;
            material.SetFloat(PulseTime, (timeDiff - 0.5f));
        }


        bool pulseMaterialSet;
        readonly List<int> indices = new List<int>();
        readonly List<Vector3> vertices = new List<Vector3>();

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
                    pulseStart = null;
                    container = new GameObject("AR Mesh Lines");
                }

                resource = ResourcePool.RentDisplay<LineResource>(container.transform);
                resource.ElementScale = 0.001f;
                resource.Visible = ARController.IsVisible;
                resource.MaterialOverride = pulseStart != null
                    ? Resources.Resource.Materials.LinePulse.Object
                    : Resources.Resource.Materials.LineMesh.Object;

                return resource;
            }
        } 


        static readonly int Tint = Shader.PropertyToID("_Tint");

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
            pulseMaterialSet = pulseStart != null;
            MeshFilter.sharedMesh = new Mesh { name = "AR Mesh" };
            Resource.Visible = Visible;

            if (TryGetMeshManager(out var meshManager))
            {
                meshManager.meshesChanged += OnManagerChanged;
            }
            else
            {
                RosLogger.Warn("ARMeshLines: No mesh manager found!");
            }

            ARController.ARCameraViewChanged += OnARCameraViewChanged;
            OnARCameraViewChanged(ARController.IsVisible);
        }

        void Start()
        {
            WriteMeshLines();
        }

        void OnARCameraViewChanged(bool value)
        {
            bool hasPulse = pulseStart != null;
            Resource.Visible = hasPulse || !value;
        }

        void OnManagerChanged(ARMeshesChangedEventArgs args)
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

            bool hasPulse = pulseStart != null;
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
                Resource.Visible = !ARController.IsVisible;
            }
        }

        void WriteMeshLines()
        {
            var mesh = MeshFilter.sharedMesh;

            indices.Clear();
            mesh.GetIndices(indices, 0);

            vertices.Clear();
            mesh.GetVertices(vertices);

            Resource.SetDirect(DirectLineSetter);
        }

        bool? DirectLineSetter([NotNull] NativeList<float4x2> lineBuffer)
        {
            var mesh = MeshFilter.sharedMesh;
            var topology = mesh.GetTopology(0);
            int[] mIndices = indices.ExtractArray();
            Vector3[] mVertices = vertices.ExtractArray();

            int count = indices.Count;
            lineBuffer.Resize(count);
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

                        Write(ref lineBuffer[i], a, b);
                        Write(ref lineBuffer[i + 1], b, c);
                        Write(ref lineBuffer[i + 2], c, a);
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

                        Write(ref lineBuffer[i], a, b);
                        Write(ref lineBuffer[i + 1], b, c);
                        Write(ref lineBuffer[i + 2], c, d);
                        Write(ref lineBuffer[i + 3], d, a);
                    }

                    break;
                default:
                    RosLogger.Debug("MeshToLinesHelper: Unknown topology " + topology);
                    break;
            }

            return false;
        }

        public void Suspend()
        {
        }

        void OnDestroy()
        {
            if (resource != null)
            {
                resource.ReturnToPool();
            }

            ARController.ARCameraViewChanged -= OnARCameraViewChanged;

            Destroy(MeshFilter.sharedMesh);

            if (TryGetMeshManager(out var meshManager))
            {
                meshManager.meshesChanged -= OnManagerChanged;
            }
        }

        static void Write(ref float4x2 f, in Vector3 a, in Vector3 b)
        {
            f.c0.x = a.x;
            f.c0.y = a.y;
            f.c0.z = a.z;

            f.c1.x = b.x;
            f.c1.y = b.y;
            f.c1.z = b.z;
        }
    }
}