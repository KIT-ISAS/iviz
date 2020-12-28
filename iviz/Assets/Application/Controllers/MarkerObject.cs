using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR.WSA;
using Logger = Iviz.Core.Logger;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    internal enum MarkerType
    {
        Arrow = Marker.ARROW,
        Cube = Marker.CUBE,
        Sphere = Marker.SPHERE,
        Cylinder = Marker.CYLINDER,
        LineStrip = Marker.LINE_STRIP,
        LineList = Marker.LINE_LIST,
        CubeList = Marker.CUBE_LIST,
        SphereList = Marker.SPHERE_LIST,
        Points = Marker.POINTS,
        TextViewFacing = Marker.TEXT_VIEW_FACING,
        MeshResource = Marker.MESH_RESOURCE,
        TriangleList = Marker.TRIANGLE_LIST,

        /// <summary>
        ///     New: Text marker that does not face the camera
        /// </summary>
        Text = 100,

        /// <summary>
        ///     New: Image marker. Pixels in Colors field, image width in Scale.z
        /// </summary>
        Image = 101
    }

    public enum MouseEventType
    {
        Click = InteractiveMarkerFeedback.BUTTON_CLICK,
        Down = InteractiveMarkerFeedback.MOUSE_DOWN,
        Up = InteractiveMarkerFeedback.MOUSE_UP
    }

    public sealed class MarkerObject : FrameNode
    {
        const string WarnStr = "<color=yellow>Warning:</color> ";
        const string ErrorStr = "<color=red>Error:</color> ";

        static Crc32Calculator Crc32 => Crc32Calculator.Instance;

        readonly StringBuilder description = new StringBuilder(250);

        class TaskInfo
        {
            [NotNull] readonly Task task;
            [NotNull] public CancellationTokenSource TokenSource { get; }
            [NotNull] public string Uri { get; }

            public TaskInfo([NotNull] Task task, [NotNull] CancellationTokenSource tokenSource, [NotNull] string uri)
            {
                this.task = task;
                TokenSource = tokenSource;
                Uri = uri;
            }
        }

        [CanBeNull] TaskInfo taskInfo;

        (string Ns, int Id) id;

        int numErrors;
        int numWarnings;

        [CanBeNull] IDisplay resource;
        [CanBeNull] Info<GameObject> resourceInfo;

        MarkerLineHelper lineHelper;
        [NotNull] MarkerLineHelper LineHelper => lineHelper ?? (lineHelper = new MarkerLineHelper());

        MarkerPointHelper pointHelper;
        [NotNull] MarkerPointHelper PointHelper => pointHelper ?? (pointHelper = new MarkerPointHelper());

        public Bounds? Bounds =>
            resource == null ? null : UnityUtils.TransformBound(resource.Bounds, resource.GetTransform());

        uint? previousHash;

        UnityEngine.Pose currentPose;
        Vector3 currentScale;


        public DateTime ExpirationTime { get; private set; }

        public bool OcclusionOnly
        {
            get => resource is ISupportsAROcclusion r && r.OcclusionOnly;
            set
            {
                if (resource is ISupportsAROcclusion arResource)
                {
                    arResource.OcclusionOnly = value;
                }
            }
        }

        public Color Tint
        {
            get => resource is ISupportsTint r ? r.Tint : Color.white;
            set
            {
                if (resource is ISupportsTint tintResource)
                {
                    tintResource.Tint = value;
                }
            }
        }

        public float Metallic
        {
            get => resource is ISupportsPbr r ? r.Metallic : 0;
            set
            {
                if (resource is ISupportsPbr pbrResource)
                {
                    pbrResource.Metallic = value;
                }
            }
        }

        public float Smoothness
        {
            get => resource is ISupportsPbr r ? r.Smoothness : 0;
            set
            {
                if (resource is ISupportsPbr pbrResource)
                {
                    pbrResource.Smoothness = value;
                }
            }
        }

        public bool Visible
        {
            get => resource == null || resource.Visible;
            set
            {
                if (resource != null)
                {
                    resource.Visible = value;
                }
            }
        }

        public int Layer
        {
            get => resource?.Layer ?? 0;
            set
            {
                if (resource != null)
                {
                    resource.Layer = value;
                }
            }
        }

        public bool TriangleListFlipWinding
        {
            get => resource is MeshTrianglesResource r && r.FlipWinding;
            set
            {
                if (resource is MeshTrianglesResource r && r.FlipWinding != value)
                {
                    r.FlipWinding = value;
                    previousHash = null;
                }
            }
        }

        public void Set([NotNull] Marker msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

            var newId = MarkerListener.IdFromMessage(msg);
            if (id != newId)
            {
                id = newId;
                name = $"{id.Ns}/{id.Id.ToString()}";
            }

            numWarnings = 0;
            numErrors = 0;

            description.Length = 0;
            description.Append("<color=maroon><b>* Marker: ").Append(id.Ns).Append("/").Append(id.Id)
                .Append("</b></color>").AppendLine();
            description.Append("Type: <b>");
            description.Append(DescriptionFromType(msg));
            if (msg.Type() == MarkerType.MeshResource)
            {
                description.Append(": ").Append(msg.MeshResource);
            }

            description.Append("</b>").AppendLine();

            ExpirationTime = msg.Lifetime == default ? DateTime.MaxValue : DateTime.Now + msg.Lifetime.ToTimeSpan();

            if (msg.Lifetime == default)
            {
                ExpirationTime = DateTime.MaxValue;
                description.Append("Expiration: None").AppendLine();
            }
            else
            {
                ExpirationTime = DateTime.Now + msg.Lifetime.ToTimeSpan();
                description.Append("Expiration: ").Append(msg.Lifetime.Secs).Append(" secs").AppendLine();
            }

            UpdateResource(msg);

            if (resource == null)
            {
                if (msg.Type() == MarkerType.MeshResource)
                {
                    description.Append(ErrorStr).Append("Mesh resource not found").AppendLine();
                    numErrors++;
                }

                return;
            }

            UpdateTransform(msg);

            switch (msg.Type())
            {
                case MarkerType.Arrow:
                    CreateArrow(msg);
                    break;
                case MarkerType.Cube:
                case MarkerType.Sphere:
                case MarkerType.Cylinder:
                case MarkerType.MeshResource:
                    CreateMeshResource(msg);
                    break;
                case MarkerType.TextViewFacing:
                case MarkerType.Text:
                {
                    CreateTextResource(msg);
                    break;
                }
                case MarkerType.CubeList:
                case MarkerType.SphereList:
                {
                    CreateMeshList(msg);
                    break;
                }
                case MarkerType.LineList:
                {
                    CreateLine(msg, false);
                    break;
                }
                case MarkerType.LineStrip:
                {
                    CreateLine(msg, true);
                    break;
                }
                case MarkerType.Points:
                {
                    CreatePoints(msg);
                    break;
                }
                case MarkerType.TriangleList:
                {
                    CreateTriangleList(msg);
                    break;
                }
                case MarkerType.Image:
                    CreateImage(msg);
                    break;
            }
        }

        [NotNull]
        T ValidateResource<T>() where T : MarkerResource
        {
            if (!(resource is T result))
            {
                throw new InvalidOperationException("Resource is not set!");
            }

            return result;
        }

        void CreateImage([NotNull] Marker msg)
        {
            ImageResource image = ValidateResource<ImageResource>();
            int count = msg.Colors.Length;
            int width = (int) msg.Scale.Z;
            int height = width == 0 ? 0 : count / width;
            if (width <= 0 || height <= 0 || width * height != count)
            {
                Debug.LogWarning("MarkerObject: Invalid image dimensions");
                return;
            }

            bool hasAlpha = msg.Colors.Any(color => color.A < 1);
            byte[] data;
            int bpp, j = 0;
            if (hasAlpha)
            {
                bpp = 3;
                data = new byte[count * 3];
                foreach (ColorRGBA color in msg.Colors)
                {
                    Color32 color32 = new Color(color.R, color.G, color.B);
                    data[j++] = color32.r;
                    data[j++] = color32.g;
                    data[j++] = color32.b;
                }
            }
            else
            {
                bpp = 4;
                data = new byte[count * 4];
                foreach (ColorRGBA color in msg.Colors)
                {
                    Color32 color32 = new Color(color.R, color.G, color.B);
                    data[j++] = color32.r;
                    data[j++] = color32.g;
                    data[j++] = color32.b;
                    data[j++] = color32.a;
                }
            }

            image.Set(width, height, bpp, data.AsSegment(), true);

            Vector3 imageScale = new Msgs.GeometryMsgs.Vector3(msg.Scale.X, msg.Scale.Y, 1).Ros2Unity().Abs();
            if (imageScale != currentScale)
            {
                currentScale = imageScale;
                transform.localScale = imageScale;
            }
        }

        void CreateTriangleList([NotNull] Marker msg)
        {
            description.Append("Scale: [")
                .Append(msg.Scale.X).Append(" | ")
                .Append(msg.Scale.Y).Append(" | ")
                .Append(msg.Scale.Z).Append("]").AppendLine();
            description.Append("Color: ")
                .Append(msg.Color.R).Append(" | ")
                .Append(msg.Color.G).Append(" | ")
                .Append(msg.Color.B).Append(" | ")
                .Append(msg.Color.A).AppendLine();

            if (msg.Points.Length == 0)
            {
                description.Append("Elements: Empty").AppendLine();
            }
            else
            {
                description.Append("Elements: ").Append(msg.Points.Length).AppendLine();
            }

            MeshTrianglesResource meshTriangles = ValidateResource<MeshTrianglesResource>();
            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
            {
                description.Append(ErrorStr).Append("Color array length ").Append(msg.Colors.Length)
                    .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                meshTriangles.Set(Array.Empty<Vector3>());
                numErrors++;
                return;
            }

            if (Mathf.Approximately(msg.Color.A, 0))
            {
                description.Append(WarnStr).Append("Color has alpha 0. Marker will not be visible").AppendLine();
                meshTriangles.Set(Array.Empty<Vector3>());
                numWarnings++;
                return;
            }

            if (msg.Color.HasNaN())
            {
                description.Append(ErrorStr).Append("Color has NaN. Marker will not be visible").AppendLine();
                meshTriangles.Set(Array.Empty<Vector3>());
                numWarnings++;
                return;
            }

            if (msg.Points.Length % 3 != 0)
            {
                description.Append(ErrorStr).Append("Point array length ").Append(msg.Colors.Length)
                    .Append(" needs to be a multiple of 3").AppendLine();
                meshTriangles.Set(Array.Empty<Vector3>());
                numErrors++;
                return;
            }

            Vector3 newScale = msg.Scale.Ros2Unity().Abs();
            if (newScale != currentScale)
            {
                currentScale = newScale;
                transform.localScale = newScale;
            }

            if (HasSameHash(msg))
            {
                //Debug.Log("Same message!");
                return;
            }

            meshTriangles.Color = msg.Color.Sanitize().ToUnityColor();
            var points = new Vector3[msg.Points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = msg.Points[i].Ros2Unity();
            }

            if (msg.Colors.Length != 0)
            {
                var colors = new Color[msg.Colors.Length];
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = msg.Colors[i].ToUnityColor();
                }

                meshTriangles.Set(points, colors);
            }
            else
            {
                meshTriangles.Set(points);
            }
        }

        void CreatePoints([NotNull] Marker msg)
        {
            PointListResource pointList = ValidateResource<PointListResource>();
            pointList.ElementScale = Mathf.Abs((float) msg.Scale.X);

            description.Append("Scale: ").Append(msg.Scale.X).AppendLine();
            description.Append("Color: ")
                .Append(msg.Color.R).Append(" | ")
                .Append(msg.Color.G).Append(" | ")
                .Append(msg.Color.B).Append(" | ")
                .Append(msg.Color.A).AppendLine();

            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
            {
                description.Append(ErrorStr).Append("Color array length ").Append(msg.Colors.Length)
                    .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                pointList.Reset();
                numErrors++;
                return;
            }

            if (Mathf.Approximately(msg.Color.A, 0) || msg.Color.HasNaN())
            {
                description.Append(WarnStr).Append("Color field has alpha 0 or NaN").AppendLine();
                pointList.Reset();
                numWarnings++;
                return;
            }

            if (msg.Points.Length == 0)
            {
                description.Append("Elements: Empty").AppendLine();
            }
            else
            {
                description.Append("Elements: ").Append(msg.Points.Length).AppendLine();
            }

            if (HasSameHash(msg))
            {
                //Debug.Log("Same message!");
                return;
            }

            PointListResource.DirectPointSetter setterCallback = PointHelper.GetPointSetter(msg);
            pointList.SetDirect(setterCallback, msg.Points.Length);
            pointList.UseColormap = false;
        }

        void CreateLine([NotNull] Marker msg, bool isStrip)
        {
            LineResource lineResource = ValidateResource<LineResource>();
            float elementScale = Mathf.Abs((float) msg.Scale.X);

            description.Append("Scale: ").Append(elementScale).AppendLine();
            description.Append("Color: ")
                .Append(msg.Color.R).Append(" | ")
                .Append(msg.Color.G).Append(" | ")
                .Append(msg.Color.B).Append(" | ")
                .Append(msg.Color.A).AppendLine();

            if (msg.Points.Length == 0)
            {
                description.Append("Elements: Empty").AppendLine();
            }
            else
            {
                description.Append("Elements: ").Append(msg.Points.Length).AppendLine();
            }

            if (Mathf.Approximately(elementScale, 0) || elementScale.IsInvalid())
            {
                description.Append(WarnStr).Append("Scale value of 0 or NaN").AppendLine();
                lineResource.Reset();
                numWarnings++;
                return;
            }

            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
            {
                description.Append(ErrorStr)
                    .Append("Color array length ").Append(msg.Colors.Length)
                    .Append(" does not match point array length ").Append(msg.Points.Length)
                    .AppendLine();
                lineResource.Reset();
                numErrors++;
                return;
            }

            if (Mathf.Approximately(msg.Color.A, 0) || msg.Color.HasNaN())
            {
                description.Append(WarnStr).Append("Color field has alpha 0 or NaN").AppendLine();
                lineResource.Reset();
                numWarnings++;
                return;
            }

            if (HasSameHash(msg))
            {
                //Debug.Log("Same message!");
                return;
            }

            lineResource.ElementScale = elementScale;
            LineResource.DirectLineSetter setterCallback =
                isStrip ? LineHelper.GetLineSetterForStrip(msg) : LineHelper.GetLineSetterForList(msg);
            lineResource.SetDirect(setterCallback, isStrip ? msg.Points.Length - 1 : msg.Points.Length / 2);
        }

        void CreateMeshList([NotNull] Marker msg)
        {
            MeshListResource meshList = ValidateResource<MeshListResource>();
            meshList.UseColormap = false;
            meshList.UseIntensityForScaleY = false;
            meshList.MeshResource = msg.Type() == MarkerType.CubeList
                ? Resource.Displays.Cube
                : Resource.Displays.Sphere;

            description.Append("Color: ")
                .Append(msg.Color.R).Append(" | ")
                .Append(msg.Color.G).Append(" | ")
                .Append(msg.Color.B).Append(" | ")
                .Append(msg.Color.A).AppendLine();

            description.Append("Scale: [")
                .Append(msg.Scale.X).Append(" | ")
                .Append(msg.Scale.Y).Append(" | ")
                .Append(msg.Scale.Z).Append("]").AppendLine();

            if (msg.Points.Length == 0)
            {
                description.Append("Elements: Empty").AppendLine();
            }
            else
            {
                description.Append("Elements: ").Append(msg.Points.Length).AppendLine();
            }

            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
            {
                description.Append(ErrorStr).Append("Color array length ").Append(msg.Colors.Length)
                    .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                meshList.Reset();
                numErrors++;
                return;
            }

            if (Mathf.Approximately(msg.Color.A, 0))
            {
                description.Append(WarnStr).Append("Color field has alpha 0").AppendLine();
                meshList.Reset();
                numWarnings++;
                return;
            }

            if (msg.Color.HasNaN())
            {
                description.Append(ErrorStr).Append("Color field has NaN. Marker will not be visible").AppendLine();
                meshList.Reset();
                numErrors++;
                return;
            }

            if (HasSameHash(msg))
            {
                return;
            }

            PointListResource.DirectPointSetter setterCallback = PointHelper.GetPointSetter(msg);
            meshList.ElementScale3 = msg.Scale.Ros2Unity().Abs();
            meshList.SetDirect(setterCallback, msg.Points.Length);
        }

        void CreateTextResource([NotNull] Marker msg)
        {
            TextMarkerResource textResource = ValidateResource<TextMarkerResource>();
            textResource.Text = msg.Text;
            textResource.Color = msg.Color.Sanitize().ToUnityColor();
            textResource.BillboardEnabled = msg.Type() != MarkerType.Text;
            textResource.ElementSize = (float) msg.Scale.Z;

            description.Append("Scale: ").Append(msg.Scale.Z).AppendLine();
            if (Mathf.Approximately((float) msg.Scale.Z, 0) || msg.Scale.Z.IsInvalid())
            {
                description.Append(WarnStr).Append("Scale value of 0 or NaN").AppendLine();
                numWarnings++;
            }

            description.Append("Color: ")
                .Append(msg.Color.R).Append(" | ")
                .Append(msg.Color.G).Append(" | ")
                .Append(msg.Color.B).Append(" | ")
                .Append(msg.Color.A).AppendLine();
        }

        void CreateMeshResource([NotNull] Marker msg)
        {
            if (resource is MeshMarkerResource meshMarker)
            {
                meshMarker.Color = msg.Color.Sanitize().ToUnityColor();
            }

            description.Append("Scale: [")
                .Append(msg.Scale.X).Append(" | ")
                .Append(msg.Scale.Y).Append(" | ")
                .Append(msg.Scale.Z).Append("]").AppendLine();

            if (Mathf.Approximately((float) msg.Scale.SquaredNorm, 0))
            {
                description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                numWarnings++;
            }
            else if (msg.Scale.HasNaN())
            {
                description.Append(WarnStr).Append("Scale value has NaN").AppendLine();
                numWarnings++;
            }

            description.Append("Color: ")
                .Append(msg.Color.R).Append(" | ")
                .Append(msg.Color.G).Append(" | ")
                .Append(msg.Color.B).Append(" | ")
                .Append(msg.Color.A).AppendLine();

            Vector3 newScale = msg.Scale.Ros2Unity().Abs();
            if (!Mathf.Approximately((newScale - currentScale).sqrMagnitude, 0))
            {
                transform.localScale = currentScale = newScale;
            }
        }


        void CreateArrow([NotNull] Marker msg)
        {
            ArrowResource arrowMarker = ValidateResource<ArrowResource>();
            arrowMarker.Color = msg.Color.Sanitize().ToUnityColor();
            switch (msg.Points.Length)
            {
                case 0:
                {
                    if (Mathf.Approximately((float) msg.Scale.SquaredNorm, 0))
                    {
                        description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                        arrowMarker.Visible = false;
                        numWarnings++;
                        return;
                    }

                    if (msg.Scale.HasNaN())
                    {
                        description.Append(WarnStr).Append("Scale value of NaN").AppendLine();
                        arrowMarker.Visible = false;
                        numWarnings++;
                        return;
                    }

                    arrowMarker.Visible = true;
                    arrowMarker.Set(msg.Scale.Ros2Unity().Abs());


                    description.Append("Scale: [")
                        .Append(msg.Scale.X).Append(" | ")
                        .Append(msg.Scale.Y).Append(" | ")
                        .Append(msg.Scale.Z).Append("]").AppendLine();
                    return;
                }
                case 2:
                {
                    float sx = Mathf.Abs((float) msg.Scale.X);
                    description.Append("Scale: ").Append(msg.Scale.X).AppendLine();
                    switch (sx)
                    {
                        case 0:
                            description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                            numWarnings++;
                            arrowMarker.Visible = false;
                            return;
                        case float.NaN:
                        case float.NegativeInfinity:
                        case float.PositiveInfinity:
                            description.Append(WarnStr).Append("Scale value of NaN or infinite").AppendLine();
                            numWarnings++;
                            arrowMarker.Visible = false;
                            return;
                        default:
                            arrowMarker.Visible = true;
                            arrowMarker.Set(msg.Points[0].Ros2Unity(), msg.Points[1].Ros2Unity(), sx);
                            return;
                    }
                }
                default:
                    description.Append(ErrorStr).Append("Point array should have a length of 0 or 2").AppendLine();
                    numErrors++;
                    break;
            }
        }

        void UpdateResource([NotNull] Marker msg)
        {
            var newResourceInfo = GetRequestedResource(msg);
            if (newResourceInfo == resourceInfo)
            {
                return;
            }

            if (resource != null && resourceInfo != null)
            {
                resource.DisposeResource(resourceInfo);
                resource = null;
            }

            resourceInfo = newResourceInfo;
            if (resourceInfo == null)
            {
                if (msg.Type() != MarkerType.MeshResource)
                {
                    Logger.Warn($"MarkerObject: Marker type '{msg.Type.ToString()}' has no resource assigned!");
                    description.Append(ErrorStr).AppendLine("Unknown marker type ").Append(msg.Type).AppendLine();
                    numErrors++;
                }
                else
                {
                    description.Append(WarnStr)
                        .Append("Unknown mesh resource '").Append(msg.MeshResource).Append("'")
                        .AppendLine();
                    numWarnings++;
                    //Logger.Warn($"MarkerObject: Unknown mesh resource '{msg.MeshResource}'");
                }

                return;
            }

            GameObject resourceGameObject = ResourcePool.GetOrCreate(resourceInfo, transform);

            resource = resourceGameObject.GetComponent<IDisplay>();
            if (resource != null)
            {
                Layer = LayerType.IgnoreRaycast;
                resource.Name = gameObject.name;
                return; // all OK
            }

            if (msg.Type() != MarkerType.MeshResource)
            {
                // shouldn't happen!
                Debug.LogWarning($"Resource '{resourceInfo}' has no MarkerResource!");
            }

            AssetWrapperResource wrapper = resourceGameObject.AddComponent<AssetWrapperResource>();
            wrapper.Layer = LayerType.IgnoreRaycast;
            resource = wrapper;
        }

        void UpdateTransform([NotNull] Marker msg)
        {
            /*
            if (msg.FrameLocked)
            {
                AttachTo(msg.Header.FrameId);
                description.Append("Frame Locked to: <i>").Append(msg.Header.FrameId).Append("</i>").AppendLine();
            }
            else
            {
                AttachTo(msg.Header.FrameId, msg.Header.Stamp);
            }
            */
            AttachTo(msg.Header.FrameId);
            description.Append("Frame Locked to: <i>").Append(msg.Header.FrameId).Append("</i>").AppendLine();

            if (msg.Pose.HasNaN())
            {
                description.Append(WarnStr).Append("Pose contains NaN values").AppendLine();
                numWarnings++;
                return;
            }

            UnityEngine.Pose newPose = msg.Pose.Ros2Unity();
            if (newPose != currentPose)
            {
                transform.SetLocalPose(currentPose = newPose);
            }
        }

        [CanBeNull]
        Info<GameObject> GetRequestedResource([NotNull] Marker msg)
        {
            if (msg.Type() == MarkerType.MeshResource)
            {
                if (taskInfo == null || taskInfo.Uri != msg.MeshResource)
                {
                    StopLoadResourceTask();
                    CancellationTokenSource tokenSource = new CancellationTokenSource();
                    taskInfo = new TaskInfo(LoadResourceAsync(msg.MeshResource, tokenSource), tokenSource,
                        msg.MeshResource);
                }

                return null;
            }

            StopLoadResourceTask();
            switch (msg.Type())
            {
                case MarkerType.Arrow:
                    return Resource.Displays.Arrow;
                case MarkerType.Cylinder:
                    return Resource.Displays.Cylinder;
                case MarkerType.Cube:
                    return Resource.Displays.Cube;
                case MarkerType.Sphere:
                    return Resource.Displays.Sphere;
                case MarkerType.TextViewFacing:
                case MarkerType.Text:
                    return Resource.Displays.Text;
                case MarkerType.LineStrip:
                case MarkerType.LineList:
                    return Resource.Displays.Line;
                case MarkerType.CubeList:
                case MarkerType.SphereList:
                    return Resource.Displays.MeshList;
                case MarkerType.Points:
                    return Resource.Displays.PointList;
                case MarkerType.TriangleList:
                    return Resource.Displays.MeshTriangles;
                case MarkerType.Image:
                    return Resource.Displays.Image;
                default:
                    return null;
            }
        }

        void StopLoadResourceTask()
        {
            taskInfo?.TokenSource.Cancel();
            taskInfo = null;
        }

        async Task LoadResourceAsync(string uriString, CancellationTokenSource tokenSource)
        {
            Info<GameObject> newResourceInfo;
            try
            {
                newResourceInfo = await Resource.GetGameObjectResourceAsync(uriString,
                    ConnectionManager.ServiceProvider,
                    tokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception e)
            {
                Logger.Error($"{this}: LoadResourceAsync failed for '{uriString}'", e);
                return;
            }

            if (newResourceInfo == null)
            {
                //Debug.LogWarning($"{this}: Resource {uriString} returned null");
                return;
            }

            GameObject resourceGameObject = ResourcePool.GetOrCreate(newResourceInfo, transform);
            var newResource = resourceGameObject.GetComponent<IDisplay>();
            if (newResource != null)
            {
                newResource.Layer = LayerType.IgnoreRaycast;
            }

            resourceInfo = newResourceInfo;
            resource = newResource;
        }

        [NotNull]
        static string DescriptionFromType([NotNull] Marker msg)
        {
            switch (msg.Type())
            {
                case MarkerType.Arrow:
                    return "Arrow";
                case MarkerType.Cylinder:
                    return "Cylinder";
                case MarkerType.Cube:
                    return "Cube";
                case MarkerType.Sphere:
                    return "Sphere";
                case MarkerType.TextViewFacing:
                    return "Text_View_Facing";
                case MarkerType.Text:
                    return "Text";
                case MarkerType.LineStrip:
                    return "LineStrip";
                case MarkerType.LineList:
                    return "LineList";
                case MarkerType.MeshResource:
                    return "MeshResource";
                case MarkerType.CubeList:
                    return "CubeList";
                case MarkerType.SphereList:
                    return "SphereList";
                case MarkerType.Points:
                    return "Points";
                case MarkerType.TriangleList:
                    return "TriangleList";
                case MarkerType.Image:
                    return "Image";
                default:
                    return "[Unknown]";
            }
        }

        public void GenerateLog([NotNull] StringBuilder baseDescription)
        {
            if (baseDescription == null)
            {
                throw new ArgumentNullException(nameof(baseDescription));
            }

            baseDescription.Append(description);
        }

        public void GetErrorCount(out int totalErrors, out int totalWarnings)
        {
            totalErrors = numErrors;
            totalWarnings = numWarnings;
        }


        public override void Stop()
        {
            base.Stop();

            StopLoadResourceTask();

            if (resource == null || resourceInfo == null)
            {
                return;
            }

            resource.DisposeResource(resourceInfo);
            resource = null;
            resourceInfo = null;
            previousHash = null;
        }

        static uint CalculateMarkerHash([NotNull] Marker msg)
        {
            uint hash = Crc32.Compute(msg.Type);
            hash = Crc32.Compute(msg.Color, hash);
            hash = Crc32.Compute(msg.Points, hash);
            hash = Crc32.Compute(msg.Colors, hash);
            return hash;
        }

        bool HasSameHash([NotNull] Marker msg)
        {
            uint currentHash = CalculateMarkerHash(msg);
            if (previousHash == currentHash)
            {
                return true;
            }

            previousHash = currentHash;
            return false;
        }

        public override string ToString()
        {
            return $"[MarkerObject {name}]";
        }

        void OnDestroy()
        {
            StopLoadResourceTask();
        }
    }


    internal static class MarkerTypeHelper
    {
        public static MarkerType Type([NotNull] this Marker marker)
        {
            return (MarkerType) marker.Type;
        }
    }
}