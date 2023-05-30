#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.MeshMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Ros;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.UI;
using Object = UnityEngine.Object;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers.XR
{
    public static class XRUtils
    {
        /// <summary>
        /// Makes hololens-specific adjustments
        /// </summary>
        public static void SetupForHololens()
        {
            // make thread pool huge because ROS makes dozens of connections
            // quickly and the hololens 2 cannot cope. this makes the hololens
            // freeze out of nowhere for 1 or 2 seconds
            ThreadPool.SetMinThreads(25, 20);

#if WINDOWS_UWP
            // this enables rendering from pv camera to align holograms when recording videos
            // see https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/mixed-reality-capture-unity
            var kind = global::Windows.Graphics.Holographic.HolographicViewConfigurationKind.PhotoVideoCamera;
            var viewConfiguration =
                global::Windows.Graphics.Holographic.HolographicDisplay.GetDefault()?.TryGetViewConfiguration(kind);
            if (viewConfiguration != null)
            {
                viewConfiguration.IsEnabled = true;
            }
#endif
        }

        public static void ProcessCanvasForXR(this GameObject root)
        {
            ProcessCanvasForXR(root.transform);
        }

        public static void ProcessCanvasForXR(this Component root)
        {
            ProcessCanvasForXR(root.transform);
        }

        static void ProcessCanvasForXR(this Transform root)
        {
            // add a TrackedDeviceGraphicRaycaster to ensure the canvas can interact with the XRController 
            foreach (var subCanvas in root.GetAllChildren().WithComponent<Canvas>())
            {
                subCanvas.gameObject.TryAddComponent<TrackedDeviceGraphicRaycaster>();
            }

            // remove the RectMask2D added for hololens compatibility
            // this ensures that masking works when using 16-bit depth buffers
            // when using 24-bit buffers this is not necessary
            foreach (var mask in root.GetAllChildren().WithComponent<RectMask2D>())
            {
                Object.Destroy(mask);
            }
        }

        /// <summary>
        /// Retrieves the mesh manager for the current platform if it exists
        /// </summary>
        public static bool TryGetMeshManager([NotNullWhen(true)] out ARMeshManager? meshManager)
        {
            if (ARController.Instance is { } ar)
            {
                // this is the mesh manager for ipads and iphones with depth cameras
                meshManager = ar.MeshManager;
            }
            else if (XRController.Instance is { } xr)
            {
                // this is the mesh manager for the hololens
                meshManager = xr.MeshManager;
            }
            else
            {
                meshManager = null;
                return false;
            }

            return meshManager != null;
        }

        static uint meshSeq;

        public static void ProcessMeshChange(List<int> indices, List<Vector3> vertices, GameObject source)
        {
            if (!ARController.EnableMeshingSubsystem) return;
            
            Sender<MeshGeometryStamped>? sender;
            if (ARController.Instance is { } ar)
            {
                sender = ar.MeshSender;
            }
            else if (XRController.Instance is { } xr)
            {
                sender = xr.MeshSender;
            }
            else
            {
                return;
            }

            if (sender is not { NumSubscribers: not 0 })
            {
                return;
            }

            string sourceName = source.name;

            Task.Run(PublishMeshChangeImpl);

            void PublishMeshChangeImpl()
            {
                if (sender == null) return; // shouldn't happen
                
                try
                {
                    var meshIndices = MemoryMarshal.Cast<int, TriangleIndices>(indices.AsReadOnlySpan()).ToArray();
                    var meshVertices = new Point[vertices.Count];

                    MeshBurstUtils.ToPoint(vertices.AsReadOnlySpan(), meshVertices);

                    var msg = new MeshGeometryStamped
                    {
                        Header = new Header(meshSeq++, time.Now(), "map"),
                        Uuid = sourceName,
                        MeshGeometry = new MeshGeometry
                        {
                            Faces = meshIndices,
                            Vertices = meshVertices
                        }
                    };

                    sender.Publish(msg);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{nameof(XRUtils)}: " +
                                    $"Error during {nameof(ProcessMeshChange)}", e);
                }
            }
        }
    }
}