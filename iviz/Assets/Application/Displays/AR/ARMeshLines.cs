using System;
using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
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
        static float? pulseStart;
        static readonly int PulseCenter = Shader.PropertyToID("_PulseCenter");
        static readonly int PulseTime = Shader.PropertyToID("_PulseTime");
        static readonly int PulseDelta = Shader.PropertyToID("_PulseDelta");
        
        public static void TriggerPulse(in Vector3 start)
        {
            bool hasPulse = pulseStart != null;
            pulseStart = Time.time;
            if (!hasPulse)
            {
                GameThread.EveryFrame += UpdateStatic;
            }
            
            var material = Resource.Materials.LinePulse.Object;
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
            
            var material = Resource.Materials.LinePulse.Object;
            material.SetFloat(PulseTime,  (timeDiff - 0.5f));
        }
        

        bool pulseMaterialSet;
        readonly List<int> indices = new List<int>();
        readonly List<Vector3> vertices = new List<Vector3>();

        Transform mTransform;
        [NotNull] Transform Transform => mTransform != null ? mTransform : (mTransform = transform);


        [CanBeNull] MeshFilter meshFilter;
        [NotNull] MeshFilter MeshFilter => meshFilter != null ? meshFilter : (meshFilter = GetComponent<MeshFilter>());

        LineResource resource;
        static readonly int Tint = Shader.PropertyToID("_Tint");

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
            resource = ResourcePool.RentDisplay<LineResource>();
            resource.ElementScale = 0.001f;
            resource.Visible = ARController.IsVisible;

            resource.MaterialOverride = pulseStart != null
                ? Resource.Materials.LinePulse.Object
                : Resource.Materials.LineMesh.Object;
            pulseMaterialSet = pulseStart != null;

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
            resource.Visible = hasPulse || !value;
        }

        void OnManagerChanged(ARMeshesChangedEventArgs args)
        {
            if (args.added.Contains(MeshFilter) || args.updated.Contains(MeshFilter))
            {
                WriteMeshLines();
            }

            if (args.removed.Contains(MeshFilter))
            {
                resource.Reset();
            }
        }

        void Update()
        {
            if (resource.Visible)
            {
                var unityPose = ARController.ARPoseToUnity(Transform.AsPose());
                //var relativePose = TfListener.RelativePoseToOrigin(unityPose);    
                //resource.Transform.SetPose(unityPose);
                resource.Transform.SetPose(unityPose);
            }

            bool hasPulse = pulseStart != null;
            if (hasPulse && !pulseMaterialSet)
            {
                resource.MaterialOverride = Resource.Materials.LinePulse.Object;
                pulseMaterialSet = true;
                resource.Visible = true;
            }
            else if (!hasPulse && pulseMaterialSet)
            {
                resource.MaterialOverride = Resource.Materials.LineMesh.Object;
                pulseMaterialSet = false;
                resource.Visible = !ARController.IsVisible;
            }
        }

        void WriteMeshLines()
        {
            var mesh = MeshFilter.sharedMesh;

            indices.Clear();
            mesh.GetIndices(indices, 0);

            vertices.Clear();
            mesh.GetVertices(vertices);

            resource.SetDirect(DirectLineSetter);
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
                    Logger.Debug("MeshToLinesHelper: Unknown topology " + topology);
                    break;
            }

            return false;
        }

        public void Suspend()
        {
        }

        void OnDestroy()
        {
            resource.ReturnToPool();
            ARController.ARCameraViewChanged -= OnARCameraViewChanged;
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
            //f.c0.w = White;

            f.c1.x = b.x;
            f.c1.y = b.y;
            f.c1.z = b.z;
            //f.c1.w = White;
        }
    }
}