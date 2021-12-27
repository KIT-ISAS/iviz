#nullable enable

using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Iviz.Common;
using Iviz.Controllers.Markers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.Highlighters;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class MarkerObject : IHasBounds, ISupportsDynamicBounds
    {
        const string WarnStr = "<color=yellow>Warning:</color> ";
        const string ErrorStr = "<color=red>Error:</color> ";
        const string FloatFormat = "#,0.###";

        static MarkerLineHelper? lineHelper;
        static MarkerPointHelper? pointHelper;
        static uint globalIdCounter;

        static MarkerLineHelper LineHelper => lineHelper ??= new MarkerLineHelper();
        static MarkerPointHelper PointHelper => pointHelper ??= new MarkerPointHelper();

        //readonly StringBuilder description = new(250);
        readonly FrameNode node;
        readonly (string Ns, int Id) id;

        IDisplay? resource;
        Info<GameObject>? resourceInfo;
        CancellationTokenSource? runningTs;
        BoundsHighlighter? highlighter;
        Marker? lastMessage;

        Vector3 currentScale;

        //int numErrors;
        //int numWarnings;

        uint? previousHash;
        float metallic = 0.5f;
        float smoothness = 0.5f;
        Color tint = Color.white;
        bool shadowsEnabled = true;
        bool occlusionOnly;
        bool triangleListFlipWinding;

        public DateTime ExpirationTime { get; private set; }
        public MarkerType MarkerType { get; private set; }
        public string UniqueNodeName { get; }
        public Pose Pose { get; private set; }

        public Transform Transform => node.Transform;
        Bounds? IHasBounds.Bounds => resource?.Bounds;
        Transform? IHasBounds.BoundsTransform => resource?.GetTransform();
        bool IHasBounds.AcceptsHighlighter => !ShowDescription;
        string IHasBounds.Caption => node.gameObject.name;

        public event Action? BoundsChanged;

        public bool OcclusionOnly
        {
            get => occlusionOnly;
            set
            {
                occlusionOnly = value;
                if (resource is ISupportsAROcclusion arResource)
                {
                    arResource.OcclusionOnly = value;
                }
            }
        }

        public bool ShowDescription
        {
            get => highlighter != null;
            set
            {
                if (value)
                {
                    highlighter = new BoundsHighlighter(this, true);
                }
                else
                {
                    highlighter?.Dispose();
                    highlighter = null;
                }
            }
        }

        public Color Tint
        {
            get => tint;
            set
            {
                tint = value;
                if (resource is ISupportsTint tintResource)
                {
                    tintResource.Tint = value;
                }
            }
        }

        public float Metallic
        {
            get => metallic;
            set
            {
                metallic = value;
                if (resource is ISupportsPbr pbrResource)
                {
                    pbrResource.Metallic = value;
                }
            }
        }

        public float Smoothness
        {
            get => smoothness;
            set
            {
                smoothness = value;
                if (resource is ISupportsPbr pbrResource)
                {
                    pbrResource.Smoothness = value;
                }
            }
        }

        public bool ShadowsEnabled
        {
            get => shadowsEnabled;
            set
            {
                shadowsEnabled = value;
                if (resource is ISupportsShadows shadowResource)
                {
                    shadowResource.ShadowsEnabled = value;
                }
            }
        }

        public bool Visible
        {
            get => node.gameObject.activeSelf;
            set => node.gameObject.SetActive(value);
        }

        public bool TriangleListFlipWinding
        {
            get => triangleListFlipWinding;
            set
            {
                if (triangleListFlipWinding == value)
                {
                    return;
                }

                triangleListFlipWinding = value;
                if (resource is MeshTrianglesResource r)
                {
                    previousHash = null;
                    r.FlipWinding = value;
                }
            }
        }

        public MarkerObject(TfFrame parent, (string Ns, int Id) id)
        {
            node = FrameNode.Instantiate("[MarkerObject]");
            node.Parent = parent;

            this.id = id;
            node.gameObject.name = $"{id.Ns}/{id.Id.ToString()}";

            UniqueNodeName = (++globalIdCounter).ToString();
        }

        public async ValueTask SetAsync(Marker msg)
        {
            lastMessage = msg ?? throw new ArgumentNullException(nameof(msg));
            ExpirationTime = msg.Lifetime == default
                ? DateTime.MaxValue
                : GameThread.Now + msg.Lifetime.ToTimeSpan();

            if (msg.Type is < 0 or > (int)MarkerType.TriangleList)
            {
                // out!
                StopLoadResourceTask();
                DiscardResource();
                return;
            }

            try
            {
                await UpdateResourceAsync(msg);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            if (resource == null)
            {
                return;
            }

            UpdateTransform(msg);

            MarkerType = msg.Type();
            switch (MarkerType)
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
                    CreateTextResource(msg);
                    break;
                case MarkerType.CubeList:
                case MarkerType.SphereList:
                    CreateMeshList(msg);
                    break;
                case MarkerType.LineList:
                    CreateLine(msg, false);
                    break;
                case MarkerType.LineStrip:
                    CreateLine(msg, true);
                    break;
                case MarkerType.Points:
                    CreatePoints(msg);
                    break;
                case MarkerType.TriangleList:
                    CreateTriangleList(msg);
                    break;
            }

            BoundsChanged?.Invoke();
        }

        T ValidateResource<T>() where T : MarkerResource =>
            resource is T result
                ? result
                : throw new InvalidOperationException("Resource is not set!");

        void CreateTriangleList(Marker msg)
        {
            var meshTriangles = ValidateResource<MeshTrianglesResource>();
            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length
                || Mathf.Approximately(msg.Color.A, 0)
                || msg.Color.HasNaN()
                || msg.Points.Length % 3 != 0)
            {
                meshTriangles.Clear();
                return;
            }

            var newScale = msg.Scale.Ros2Unity().Abs();
            if (newScale != currentScale)
            {
                currentScale = newScale;
                Transform.localScale = newScale;
            }

            if (HasSameHash(msg))
            {
                return;
            }

            meshTriangles.Color = msg.Color.Sanitize().ToUnityColor();
            meshTriangles.FlipWinding = TriangleListFlipWinding;

            var srcPoints = msg.Points;
            using var points = new Rent<Vector3>(srcPoints.Length);
            var pArray = points.Array;
            for (int i = 0; i < srcPoints.Length; i++)
            {
                srcPoints[i].Ros2Unity(out pArray[i]);
            }

            var colors = MemoryMarshal.Cast<ColorRGBA, Color>(msg.Colors);
            meshTriangles.Set(points, colors);
        }

        void CreatePoints(Marker msg)
        {
            var pointList = ValidateResource<PointListResource>();
            pointList.ElementScale = Mathf.Abs((float)msg.Scale.X);

            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length
                || Mathf.Approximately(msg.Color.A, 0)
                || msg.Color.HasNaN())
            {
                pointList.Reset();
                return;
            }

            if (HasSameHash(msg))
            {
                return;
            }

            var setterCallback = PointHelper.GetPointSetter(msg);
            pointList.SetDirect(setterCallback, msg.Points.Length);
            pointList.UseColormap = false;
        }

        void CreateLine(Marker msg, bool isStrip)
        {
            var lineResource = ValidateResource<LineResource>();
            float elementScale = Mathf.Abs((float)msg.Scale.X);

            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length
                || Mathf.Approximately(elementScale, 0)
                || elementScale.IsInvalid()
                || Mathf.Approximately(msg.Color.A, 0)
                || msg.Color.HasNaN())
            {
                lineResource.Reset();
                return;
            }

            lineResource.ElementScale = elementScale;
            Transform.localScale = Vector3.one;

            if (HasSameHash(msg))
            {
                return;
            }

            var setterCallback = isStrip
                ? LineHelper.GetLineSetterForStrip(msg)
                : LineHelper.GetLineSetterForList(msg);
            lineResource.SetDirect(setterCallback, isStrip ? msg.Points.Length - 1 : msg.Points.Length / 2);
        }

        void CreateMeshList(Marker msg)
        {
            var meshList = ValidateResource<MeshListResource>();
            meshList.UseColormap = false;
            meshList.UseIntensityForScaleY = false;
            meshList.MeshResource = msg.Type() == MarkerType.CubeList
                ? Resource.Displays.Cube
                : Resource.Displays.Sphere;

            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length
                || Mathf.Approximately(msg.Color.A, 0)
                || msg.Color.HasNaN())
            {
                meshList.Reset();
                return;
            }

            meshList.ElementScale3 = msg.Scale.Ros2Unity().Abs();

            if (HasSameHash(msg))
            {
                return;
            }

            var setterCallback = PointHelper.GetPointSetter(msg);
            meshList.SetDirect(setterCallback, msg.Points.Length);
        }

        void CreateTextResource(Marker msg)
        {
            var textResource = ValidateResource<TextMarkerResource>();
            textResource.Text = msg.Text;
            textResource.Color = msg.Color.Sanitize().ToUnityColor();
            textResource.BillboardEnabled = true;
            textResource.ElementSize = (float)msg.Scale.Z;
        }

        void CreateMeshResource(Marker msg)
        {
            if (resource is MeshMarkerResource meshMarker)
            {
                meshMarker.Color = msg.Color.Sanitize().ToUnityColor();
            }

            var newScale = msg.Scale.Ros2Unity().Abs();
            Transform.localScale = currentScale = newScale;
        }

        void CreateArrow(Marker msg)
        {
            var arrowMarker = ValidateResource<ArrowResource>();
            arrowMarker.Color = msg.Color.Sanitize().ToUnityColor();
            switch (msg.Points.Length)
            {
                case 0:
                {
                    if (Mathf.Approximately((float)msg.Scale.SquaredNorm, 0) || msg.Scale.HasNaN())
                    {
                        arrowMarker.Visible = false;
                        return;
                    }

                    arrowMarker.Visible = true;
                    arrowMarker.Set(msg.Scale.Ros2Unity().Abs());
                    break;
                }
                case 2:
                {
                    float sx = Mathf.Abs((float)msg.Scale.X);
                    if (Mathf.Approximately(sx, 0) || !float.IsFinite(sx))
                    {
                        arrowMarker.Visible = false;
                        return;
                    }

                    arrowMarker.Visible = true;
                    arrowMarker.Set(msg.Points[0].Ros2Unity(), msg.Points[1].Ros2Unity(), sx);
                    break;
                }
            }
        }

        async ValueTask UpdateResourceAsync(Marker msg)
        {
            var newResourceInfo = await GetRequestedResource(msg);
            if (newResourceInfo == resourceInfo)
            {
                return;
            }

            DiscardResource();
            resourceInfo = newResourceInfo;
            if (resourceInfo == null)
            {
                if (msg.Type() != MarkerType.MeshResource)
                {
                    RosLogger.Warn($"MarkerObject: Marker type '{msg.Type.ToString()}' has no resource assigned!");
                }

                BoundsChanged?.Invoke();
                return;
            }

            var resourceGameObject = ResourcePool.Rent(resourceInfo, Transform);

            resource = resourceGameObject.GetComponent<IDisplay>();
            if (resource != null)
            {
                resource.Layer = LayerType.IgnoreRaycast;
                Tint = Tint;
                Smoothness = Smoothness;
                Metallic = Metallic;
                OcclusionOnly = OcclusionOnly;
                ShadowsEnabled = ShadowsEnabled;

                if (resource is ISupportsDynamicBounds hasDynamicBounds)
                {
                    hasDynamicBounds.BoundsChanged += RaiseBoundsChanged;
                }

                return; // all OK
            }

            if (msg.Type() != MarkerType.MeshResource)
            {
                // shouldn't happen!
                Debug.LogWarning($"Mesh resource '{resourceInfo}' has no IDisplay!");
            }

            // add generic wrapper
            resource = resourceGameObject.AddComponent<AssetWrapperResource>();
            resource.Layer = LayerType.IgnoreRaycast;
            BoundsChanged?.Invoke();
        }

        void RaiseBoundsChanged()
        {
            BoundsChanged?.Invoke();
        }

        void UpdateTransform(Marker msg)
        {
            if (msg.Pose.HasNaN())
            {
                return;
            }

            var newPose = msg.Pose.Ros2Unity();
            if (newPose == Pose)
            {
                return;
            }

            node.AttachTo(msg.Header);
            Pose = !newPose.IsUsable()
                ? Pose.identity
                : newPose;
            Transform.SetLocalPose(Pose);
        }

        ValueTask<Info<GameObject>?> GetRequestedResource(Marker msg)
        {
            if (msg.Type() != MarkerType.MeshResource)
            {
                StopLoadResourceTask();
            }

            return msg.Type() switch
            {
                MarkerType.Arrow => AsTask(Resource.Displays.Arrow),
                MarkerType.Cylinder => AsTask(Resource.Displays.Cylinder),
                MarkerType.Cube => AsTask(Resource.Displays.Cube),
                MarkerType.Sphere => AsTask(Resource.Displays.Sphere),
                MarkerType.TextViewFacing => AsTask(Resource.Displays.Text),
                MarkerType.LineStrip => AsTask(Resource.Displays.Line),
                MarkerType.LineList => AsTask(Resource.Displays.Line),
                MarkerType.CubeList => AsTask(Resource.Displays.MeshList),
                MarkerType.SphereList => AsTask(Resource.Displays.MeshList),
                MarkerType.Points => AsTask(Resource.Displays.PointList),
                MarkerType.TriangleList => AsTask(Resource.Displays.MeshTriangles),
                MarkerType.MeshResource when Resource.TryGetResource(msg.MeshResource, out var info) => AsTask(info),
                MarkerType.MeshResource => RequestMeshResource(msg.MeshResource),
                _ => AsTask(null)
            };

            static ValueTask<Info<GameObject>?> AsTask(Info<GameObject>? val) => new(val);
        }

        async ValueTask<Info<GameObject>?> RequestMeshResource(string meshResource)
        {
            StopLoadResourceTask();
            runningTs = new CancellationTokenSource();

            try
            {
                var result = await Resource.GetGameObjectResourceAsync(meshResource,
                    ConnectionManager.ServiceProvider, runningTs.Token);
                runningTs.Token.ThrowIfCancellationRequested();
                return result;
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{this}: LoadResourceAsync failed for '{meshResource}'", e);
                return null;
            }
        }


        void StopLoadResourceTask()
        {
            runningTs?.Cancel();
            runningTs = null;
        }

        public void GetErrorCount(out int totalErrors, out int totalWarnings)
        {
            totalErrors = 0;
            totalWarnings = 0;
        }

        bool HasSameHash(Marker msg, bool useScale = false)
        {
            uint currentHash = CalculateMarkerHash(msg, useScale);
            if (previousHash == currentHash)
            {
                return true;
            }

            previousHash = currentHash;
            return false;
        }

        static uint CalculateMarkerHash(Marker msg, bool useScale)
        {
            uint hash = Crc32Calculator.Compute(msg.Type);
            hash = Crc32Calculator.Compute(msg.Color, hash);
            hash = Crc32Calculator.Compute(msg.Points, hash);
            hash = Crc32Calculator.Compute(msg.Colors, hash);
            return useScale
                ? Crc32Calculator.Compute(msg.Scale, hash)
                : hash;
        }

        public void Stop()
        {
            StopLoadResourceTask();
            DiscardResource();
            ShowDescription = false;
            previousHash = null;
            BoundsChanged = null;
            node.DestroySelf();
        }

        void DiscardResource()
        {
            if (resource is ISupportsDynamicBounds hasDynamicBounds)
            {
                hasDynamicBounds.BoundsChanged -= RaiseBoundsChanged;
            }

            if (resource == null || resourceInfo == null)
            {
                return;
            }

            resource.ReturnToPool(resourceInfo);
            resource = null;
            resourceInfo = null;
            BoundsChanged?.Invoke();
        }

        public override string ToString() => $"[MarkerObject {node.name}]";

        public void GenerateLog(StringBuilder description)
        {
            if (lastMessage is not { } msg)
            {
                return;
            }

            description.Append("<color=#800000ff>")
                .Append("<link=").Append(UniqueNodeName).Append(">")
                .Append("<b><u>").Append(id.Ns.Length != 0 ? id.Ns : "[]").Append("/").Append(id.Id)
                .Append("</u></b></link></color>")
                .AppendLine();

            description.Append("Type: <b>");
            description.Append(DescriptionFromType(msg));
            if (msg.Type() == MarkerType.MeshResource)
            {
                description.Append(": ").Append(msg.MeshResource);
            }

            description.Append("</b>").AppendLine();

            if (msg.Lifetime == default)
            {
                description.Append("Expiration: None").AppendLine();
            }
            else
            {
                description.Append("Expiration: ").Append(msg.Lifetime.Secs).Append(" secs").AppendLine();
            }

            if (msg.Type is < 0 or > (int)MarkerType.TriangleList)
            {
                return;
            }

            UpdateResourceAsyncLog();

            if (resource == null)
            {
                if (msg.Type() != MarkerType.MeshResource)
                {
                    return;
                }

                description.Append(ErrorStr).Append("Failed to load mesh resource. See Log.").AppendLine();
                return;
            }

            UpdateTransformLog();

            var markerType = msg.Type();
            switch (markerType)
            {
                case MarkerType.Arrow:
                    CreateArrowLog();
                    break;
                case MarkerType.Cube:
                case MarkerType.Sphere:
                case MarkerType.Cylinder:
                case MarkerType.MeshResource:
                    CreateMeshResourceLog();
                    break;
                case MarkerType.TextViewFacing:
                    CreateTextResourceLog();
                    break;
                case MarkerType.CubeList:
                case MarkerType.SphereList:
                    CreateMeshListLog();
                    break;
                case MarkerType.LineList:
                case MarkerType.LineStrip:
                    CreateLineLog();
                    break;
                case MarkerType.Points:
                    CreatePointsLog();
                    break;
                case MarkerType.TriangleList:
                    CreateTriangleListLog();
                    break;
            }

            void UpdateTransformLog()
            {
                description.Append("Frame Locked to: <i>")
                    .Append(string.IsNullOrEmpty(msg.Header.FrameId) ? "(none)" : msg.Header.FrameId)
                    .Append("</i>").AppendLine();

                if (msg.Pose.HasNaN())
                {
                    description.Append(WarnStr).Append("Pose contains NaN values").AppendLine();
                    return;
                }

                var newPose = msg.Pose.Ros2Unity();
                if (!newPose.IsUsable())
                {
                    description.Append(ErrorStr).Append(
                        $"Cannot use ({newPose.position.x}, {newPose.position.y}, {newPose.position.z}) as position. " +
                        $"Values too large.");
                }
            }

            void UpdateResourceAsyncLog()
            {
                if (resourceInfo != null)
                {
                    return;
                }

                if (msg.Type() != MarkerType.MeshResource)
                {
                    description.Append(ErrorStr).AppendLine("Unknown marker type ").Append(msg.Type).AppendLine();
                }
                else
                {
                    description.Append(WarnStr)
                        .Append("Unknown mesh resource '").Append(msg.MeshResource).Append("'")
                        .AppendLine();
                }
            }

            void CreateArrowLog()
            {
                switch (msg.Points.Length)
                {
                    case 0:
                        AppendColorLog(msg.Color);
                        AppendScaleLog(msg.Scale);

                        if (Mathf.Approximately((float)msg.Scale.SquaredNorm, 0))
                        {
                            description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                        }

                        if (msg.Scale.HasNaN())
                        {
                            description.Append(WarnStr).Append("Scale value of NaN").AppendLine();
                        }

                        break;
                    case 2:
                        float sx = Mathf.Abs((float)msg.Scale.X);
                        AppendColorLog(msg.Color);
                        AppendScalarLog(msg.Scale.X);

                        switch (sx)
                        {
                            case 0:
                                description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                                break;
                            case float.NaN:
                            case float.NegativeInfinity:
                            case float.PositiveInfinity:
                                description.Append(WarnStr).Append("Scale value of NaN or infinite").AppendLine();
                                break;
                        }

                        break;
                    default:
                        description.Append(ErrorStr).Append("Point array must have a length of 0 or 2").AppendLine();
                        break;
                }
            }

            void CreateMeshResourceLog()
            {
                AppendColorLog(msg.Color);
                AppendScaleLog(msg.Scale);

                if (Mathf.Approximately((float)msg.Scale.SquaredNorm, 0))
                {
                    description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                }
                else if (msg.Scale.HasNaN())
                {
                    description.Append(WarnStr).Append("Scale value has NaN").AppendLine();
                }
            }

            void CreateTextResourceLog()
            {
                description.Append("Text: ").Append(msg.Text.Length).Append(" chars").AppendLine();
                AppendColorLog(msg.Color);
                AppendScalarLog(msg.Scale.Z);

                if (Mathf.Approximately((float)msg.Scale.Z, 0) || msg.Scale.Z.IsInvalid())
                {
                    description.Append(WarnStr).Append("Scale value of 0 or NaN").AppendLine();
                }
            }

            void CreateMeshListLog()
            {
                AppendScaleLog(msg.Scale);
                AppendColorLog(msg.Color);

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
                    return;
                }

                if (Mathf.Approximately(msg.Color.A, 0))
                {
                    description.Append(WarnStr).Append("Color field has alpha 0").AppendLine();
                    return;
                }

                if (msg.Color.HasNaN())
                {
                    description.Append(ErrorStr).Append("Color field has NaN. Marker will not be visible").AppendLine();
                }
            }

            void CreateLineLog()
            {
                float elementScale = Mathf.Abs((float)msg.Scale.X);

                AppendColorLog(msg.Color);
                AppendScalarLog(elementScale);

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
                    return;
                }

                if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
                {
                    description.Append(ErrorStr)
                        .Append("Color array length ").Append(msg.Colors.Length)
                        .Append(" does not match point array length ").Append(msg.Points.Length)
                        .AppendLine();
                    return;
                }

                if (Mathf.Approximately(msg.Color.A, 0) || msg.Color.HasNaN())
                {
                    description.Append(WarnStr).Append("Color field has alpha 0 or NaN").AppendLine();
                }
            }

            void CreatePointsLog()
            {
                AppendColorLog(msg.Color);
                AppendScalarLog(msg.Scale.X);

                if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
                {
                    description.Append(ErrorStr).Append("Color array length ").Append(msg.Colors.Length)
                        .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                    return;
                }

                if (Mathf.Approximately(msg.Color.A, 0) || msg.Color.HasNaN())
                {
                    description.Append(WarnStr).Append("Color field has alpha 0 or NaN").AppendLine();
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
            }

            void CreateTriangleListLog()
            {
                AppendScaleLog(msg.Scale);
                AppendColorLog(msg.Color);

                if (msg.Points.Length == 0)
                {
                    description.Append("Elements: Empty").AppendLine();
                }
                else
                {
                    description.Append("Elements: ").Append(msg.Points.Length).AppendLine();
                }

                //var meshTriangles = ValidateResource<MeshTrianglesResource>();
                if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
                {
                    description.Append(ErrorStr).Append("Color array length ").Append(msg.Colors.Length)
                        .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                    //meshTriangles.Clear();
                    //numErrors++;
                    return;
                }

                if (Mathf.Approximately(msg.Color.A, 0))
                {
                    description.Append(WarnStr).Append("Color has alpha 0. Marker will not be visible").AppendLine();
                    //meshTriangles.Clear();
                    //numWarnings++;
                    return;
                }

                if (msg.Color.HasNaN())
                {
                    description.Append(ErrorStr).Append("Color has NaN. Marker will not be visible").AppendLine();
                    //meshTriangles.Clear();
                    //numWarnings++;
                    return;
                }

                if (msg.Points.Length % 3 != 0)
                {
                    description.Append(ErrorStr).Append("Point array length ").Append(msg.Colors.Length)
                        .Append(" needs to be a multiple of 3").AppendLine();
                    //meshTriangles.Clear();
                    //numErrors++;
                    //return;
                }
            }

            void AppendScaleLog(in Msgs.GeometryMsgs.Vector3 c)
            {
                description.Append("Scale: [")
                    .Append(c.X.ToString(FloatFormat)).Append(" | ")
                    .Append(c.Y.ToString(FloatFormat)).Append(" | ")
                    .Append(c.Z.ToString(FloatFormat)).Append("]").AppendLine();
            }

            void AppendColorLog(in ColorRGBA c)
            {
                description.Append("Color: ");

                string alpha = c.A.ToString(FloatFormat);

                switch (c.R, c.G, c.B)
                {
                    case (1, 1, 1):
                        description.Append("White | ").Append(alpha);
                        break;
                    case (0, 0, 0):
                        description.Append("Black | ").Append(alpha);
                        break;
                    case (1, 0, 0):
                        description.Append("Red | ").Append(alpha);
                        break;
                    case (0, 1, 0):
                        description.Append("Green | ").Append(alpha);
                        break;
                    case (0, 0, 1):
                        description.Append("Blue | ").Append(alpha);
                        break;
                    case (0.5f, 0.5f, 0.5f):
                        description.Append("Grey | ").Append(alpha);
                        break;
                    default:
                        description
                            .Append(c.R.ToString(FloatFormat)).Append(" | ")
                            .Append(c.G.ToString(FloatFormat)).Append(" | ")
                            .Append(c.B.ToString(FloatFormat)).Append(" | ")
                            .Append(alpha);
                        break;
                }

                description.AppendLine();
            }

            void AppendScalarLog(double c)
            {
                description.Append("Scale: ").Append(c.ToString(FloatFormat)).AppendLine();
            }
        }

        static string DescriptionFromType(Marker msg)
        {
            return msg.Type() switch
            {
                MarkerType.Arrow => "Arrow",
                MarkerType.Cylinder => "Cylinder",
                MarkerType.Cube => "Cube",
                MarkerType.Sphere => "Sphere",
                MarkerType.TextViewFacing => "Text_View_Facing",
                MarkerType.LineStrip => "LineStrip",
                MarkerType.LineList => "LineList",
                MarkerType.MeshResource => "MeshResource",
                MarkerType.CubeList => "CubeList",
                MarkerType.SphereList => "SphereList",
                MarkerType.Points => "Points",
                MarkerType.TriangleList => "TriangleList",
                MarkerType.Invalid => "Invalid",
                _ => $"Unknown ({msg.Type.ToString()})"
            };
        }
    }


    internal static class MarkerTypeHelper
    {
        public static MarkerType Type(this Marker marker) => (MarkerType)marker.Type;
    }
}