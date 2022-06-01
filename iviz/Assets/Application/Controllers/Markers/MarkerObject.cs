#nullable enable

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Controllers.Markers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.Highlighters;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using UnityEngine;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed class MarkerObject : IHasBounds
    {
        const string WarnStr = "<color=yellow>Warning:</color> ";
        const string ErrorStr = "<color=red>Error:</color> ";
        const string FloatFormat = "#,0.###";

        static MarkerLineHelper? lineHelper;
        static MarkerPointHelper? pointHelper;
        static uint globalIdCounter;

        static MarkerLineHelper LineHelper => lineHelper ??= new MarkerLineHelper();
        static MarkerPointHelper PointHelper => pointHelper ??= new MarkerPointHelper();

        readonly FrameNode node;
        readonly (string Ns, int Id) id;

        IDisplay? resource;
        ResourceKey<GameObject>? resourceKey;
        CancellationTokenSource? runningTs;
        BoundsHighlighter? highlighter;

        Marker? lastMessage;
        int? triangleMeshFailedIndex;

        Pose localPose;
        Vector3 localScale;
        uint? previousHash;
        float metallic = 0.5f;
        float smoothness = 0.5f;
        Color tint = Color.white;
        bool shadowsEnabled = true;
        bool occlusionOnly;
        bool triangleListFlipWinding;
        int numErrors, numWarnings;

        internal Quaternion LocalRotation => localPose.rotation;
        public DateTime ExpirationTime { get; private set; }
        public MarkerType MarkerType { get; private set; }
        public string UniqueNodeName { get; }

        public Transform Transform => node.Transform;
        Bounds? IHasBounds.Bounds => resource?.Bounds;
        Transform? IHasBounds.BoundsTransform => resource?.GetTransform();
        bool IHasBounds.AcceptsHighlighter => !ShowDescription;
        string IHasBounds.Caption => node.Name;

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
                    shadowResource.EnableShadows = value;
                }
            }
        }

        public bool Visible
        {
            set => node.Visible = value;
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
                if (resource is MeshTrianglesDisplay display)
                {
                    previousHash = null;
                    display.FlipWinding = value;
                }
            }
        }

        public MarkerObject(TfFrame parent, in (string Ns, int Id) id)
        {
            this.id = id;
            node = new FrameNode($"{id.Ns}/{id.Id.ToString()}", parent);
            UniqueNodeName = (++globalIdCounter).ToString();
        }

        public async void SetAsync(Marker msg)
        {
            ThrowHelper.ThrowIfNull(msg, nameof(msg));
            lastMessage = msg;
            ExpirationTime = msg.Lifetime == default
                ? DateTime.MaxValue
                : GameThread.Now + msg.Lifetime.ToTimeSpan();

            numErrors = 0;
            numWarnings = 0;

            if (msg.Type is < 0 or > (int)MarkerType.TriangleList)
            {
                // out!
                numErrors++;
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
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Failed to update resource", e);
                numErrors++;
                return;
            }

            if (resource == null)
            {
                numErrors++;
                return;
            }

            if (runningTs is { Token: { IsCancellationRequested: true } })
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

        T ValidateResource<T>() where T : MarkerDisplay
        {
            T? t = resource as T;
            if (t is null)
            {
                ThrowHelper.ThrowMissingAssetField("Resource does not have a marker component!");
            }

            return t;
        }

        void CreateTriangleList(Marker msg)
        {
            var meshResource = ValidateResource<MeshTrianglesDisplay>();
            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length
                || msg.Color.IsInvalid()
                || msg.Points.Length % 3 != 0
                || msg.Points.Length > NativeList.MaxElements)
            {
                numErrors++;
                meshResource.Clear();
                return;
            }
            
            if (msg.Color.A.ApproximatelyZero())
            {
                numWarnings++;
                meshResource.Clear();
                return;
            }

            var newScale = msg.Scale.Ros2Unity().Abs();
            if (newScale != localScale)
            {
                localScale = newScale;
                Transform.localScale = newScale;
            }

            if (HasSameHash(msg))
            {
                return;
            }

            meshResource.Color = msg.Color.Sanitize().ToUnity();
            meshResource.FlipWinding = TriangleListFlipWinding;

            int pointsLength = msg.Points.Length;
            if (pointsLength == 0)
            {
                meshResource.Clear();
                triangleMeshFailedIndex = null;
                return;
            }

            triangleMeshFailedIndex = CopyPoints(pointsLength, msg.Points, msg.Colors, meshResource);
        }

        static int? CopyPoints(int pointsLength, Point[] srcPoints, ColorRGBA[] srcColors, MeshTrianglesDisplay mesh)
        {
            using var points = new Rent<Vector3>(pointsLength);
            var dstPoints = points.Array;

            for (int i = 0; i < pointsLength; i++)
            {
                ref readonly Point srcPtr = ref srcPoints[i];
                ref Vector3 dstPtr = ref dstPoints[i];
                
                srcPtr.Ros2Unity(out dstPtr);
                if (dstPtr.IsInvalid()) // unlikely but needed!
                {
                    mesh.Clear();
                    return pointsLength - i;
                }
            }            
            
            var colors = MemoryMarshal.Cast<ColorRGBA, Color>(srcColors);
            mesh.Set(points, colors);
            return null;
        }

        void CreatePoints(Marker msg)
        {
            var pointList = ValidateResource<PointListDisplay>();
            pointList.ElementScale = Mathf.Abs((float)msg.Scale.X);

            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length
                || msg.Color.IsInvalid()
                || msg.Points.Length > NativeList.MaxElements)
            {
                numErrors++;
                pointList.Reset();
                return;
            }

            if (msg.Color.A.ApproximatelyZero())
            {
                numWarnings++;
                pointList.Reset();
                return;
            }

            if (HasSameHash(msg))
            {
                return;
            }

            var setterCallback = PointHelper.GetPointSetter(msg);
            pointList.Set(setterCallback, msg.Points.Length);
            pointList.UseColormap = false;
        }

        void CreateLine(Marker msg, bool isStrip)
        {
            var lineResource = ValidateResource<LineDisplay>();
            float elementScale = Mathf.Abs((float)msg.Scale.X);

            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length
                || elementScale.IsInvalid()
                || msg.Color.IsInvalid()
                || msg.Points.Length > NativeList.MaxElements)
            {
                numErrors++;
                lineResource.Reset();
                return;
            }

            if (elementScale.ApproximatelyZero()
                || msg.Color.A.ApproximatelyZero())
            {
                numWarnings++;
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
            var meshList = ValidateResource<MeshListDisplay>();
            meshList.UseColormap = false;
            meshList.UseIntensityForScaleY = false;
            meshList.MeshResource = msg.Type() == MarkerType.CubeList
                ? Resource.Displays.Cube
                : Resource.Displays.Sphere;

            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length
                || msg.Color.IsInvalid()
                || msg.Scale.IsInvalid()
                || msg.Points.Length > NativeList.MaxElements)
            {
                numErrors++;
                meshList.Reset();
                return;
            }

            if (msg.Color.A.ApproximatelyZero() || msg.Scale.ApproximatelyZero())
            {
                numWarnings++;
                meshList.Reset();
                return;
            }

            meshList.ElementScale3 = msg.Scale.Ros2Unity().Abs();

            if (HasSameHash(msg))
            {
                return;
            }

            var setterCallback = PointHelper.GetPointSetter(msg);
            meshList.Set(setterCallback, msg.Points.Length);
        }

        void CreateTextResource(Marker msg)
        {
            var textResource = ValidateResource<TextMarkerDisplay>();

            if (msg.Scale.Z.ApproximatelyZero())
            {
                numWarnings++;
            }
            else if (msg.Scale.Z.IsInvalid())
            {
                numErrors++;
                textResource.Text = "";
                return;
            }

            textResource.Text = msg.Text;
            textResource.Color = msg.Color.Sanitize().ToUnity();
            textResource.BillboardEnabled = true;
            textResource.ElementSize = (float)msg.Scale.Z;
        }

        void CreateMeshResource(Marker msg)
        {
            if (resource is ISupportsColor hasColor)
            {
                hasColor.Color = msg.Color.Sanitize().ToUnity();
            }

            Vector3 newScale;
            if (msg.Scale.MaxAbsCoeff().ApproximatelyZero())
            {
                numWarnings++;
                newScale = Vector3.zero;
            }
            else if (msg.Scale.IsInvalid())
            {
                numErrors++;
                newScale = Vector3.zero;
            }
            else
            {
                newScale = msg.Scale.Ros2Unity().Abs();
            }

            Transform.localScale = localScale = newScale;
        }

        void CreateArrow(Marker msg)
        {
            var arrowMarker = ValidateResource<ArrowDisplay>();
            arrowMarker.Color = msg.Color.Sanitize().ToUnity();
            switch (msg.Points.Length)
            {
                case 0:
                {
                    if (msg.Scale.IsInvalid())
                    {
                        arrowMarker.Visible = false;
                        numErrors++;
                        return;
                    }

                    if (msg.Scale.ApproximatelyZero())
                    {
                        arrowMarker.Visible = false;
                        numWarnings++;
                        return;
                    }

                    arrowMarker.Visible = true;
                    arrowMarker.Set(msg.Scale.Ros2Unity().Abs());
                    break;
                }
                case 2:
                {
                    float sx = Mathf.Abs((float)msg.Scale.X);
                    if (sx.IsInvalid()
                        || msg.Points[0].IsInvalid()
                        || msg.Points[1].IsInvalid())
                    {
                        numWarnings++;
                        arrowMarker.Visible = false;
                        return;
                    }

                    if (sx.ApproximatelyZero())
                    {
                        numWarnings++;
                        arrowMarker.Visible = false;
                        return;
                    }

                    arrowMarker.Visible = true;
                    arrowMarker.Set(msg.Points[0].Ros2Unity(), msg.Points[1].Ros2Unity(), sx);
                    break;
                }
                default:
                    numErrors++;
                    break;
            }
        }

        async ValueTask UpdateResourceAsync(Marker msg)
        {
            ProcessResource(msg, await GetRequestedResource(msg));
        }

        void ProcessResource(Marker msg, ResourceKey<GameObject>? newResourceKey)
        {
            if (newResourceKey == resourceKey)
            {
                return;
            }

            DiscardResource();
            resourceKey = newResourceKey;
            if (resourceKey == null)
            {
                if (msg.Type() != MarkerType.MeshResource)
                {
                    RosLogger.Warn($"{this}: Marker type '{msg.Type.ToString()}' has no resource assigned!");
                }

                BoundsChanged?.Invoke();
                return;
            }

            var resourceGameObject = ResourcePool.Rent(resourceKey, Transform);

            if (resourceGameObject.TryGetComponent(out resource))
            {
                Tint = Tint;
                Smoothness = Smoothness;
                Metallic = Metallic;
                OcclusionOnly = OcclusionOnly;
                ShadowsEnabled = ShadowsEnabled;

                if (resource is ISupportsDynamicBounds hasDynamicBounds)
                {
                    hasDynamicBounds.BoundsChanged += RaiseBoundsChanged;
                }
            }
            else
            {
                if (msg.Type() != MarkerType.MeshResource)
                {
                    // shouldn't happen!
                    RosLogger.Warn($"{this}: Mesh resource '{resourceKey}' has no {nameof(IDisplay)}!");
                }

                // add generic wrapper
                resource = resourceGameObject.AddComponent<AssetWrapperDisplay>();
            }

            if (resource is ISupportsLayer supportsLayer)
            {
                supportsLayer.Layer = LayerType.IgnoreRaycast;
            }

            BoundsChanged?.Invoke();
        }

        void RaiseBoundsChanged()
        {
            BoundsChanged?.Invoke();
        }

        void UpdateTransform(Marker msg)
        {
            if (msg.Pose.IsInvalid())
            {
                numErrors++;
                return;
            }

            node.AttachTo(msg.Header);

            var newPose = msg.Pose.Ros2Unity();
            if (newPose == localPose)
            {
                return;
            }

            localPose = !newPose.IsUsable()
                ? Pose.identity
                : newPose;
            Transform.SetLocalPose(localPose);
        }

        ValueTask<ResourceKey<GameObject>?> GetRequestedResource(Marker msg)
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

            static ValueTask<ResourceKey<GameObject>?> AsTask(ResourceKey<GameObject>? val) => new(val);
        }

        async ValueTask<ResourceKey<GameObject>?> RequestMeshResource(string meshResource)
        {
            StopLoadResourceTask();
            runningTs = new CancellationTokenSource();

            try
            {
                return await Resource.GetGameObjectResourceAsync(meshResource, RosManager.ServiceProvider,
                    runningTs.Token);
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
            totalErrors = numErrors;
            totalWarnings = numWarnings;
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
            uint hash = HashCalculator.Compute(msg.Type);
            hash = HashCalculator.Compute(msg.Color, hash);
            hash = HashCalculator.Compute(msg.Points, hash);
            hash = HashCalculator.Compute(msg.Colors, hash);
            return useScale
                ? HashCalculator.Compute(msg.Scale, hash)
                : hash;
        }

        public void Dispose()
        {
            StopLoadResourceTask();
            DiscardResource();
            ShowDescription = false;
            previousHash = null;
            BoundsChanged = null;
            node.Dispose();
        }

        void DiscardResource()
        {
            if (resource is ISupportsDynamicBounds hasDynamicBounds)
            {
                hasDynamicBounds.BoundsChanged -= RaiseBoundsChanged;
            }

            if (resource == null || resourceKey == null)
            {
                return;
            }

            resource.ReturnToPool(resourceKey);
            resource = null;
            resourceKey = null;
            BoundsChanged?.Invoke();
        }

        public override string ToString() => $"[{nameof(MarkerObject)} {node.Name}]";

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
                string timeInSecs = msg.Lifetime.ToTimeSpan().TotalSeconds.ToString("N01");
                description.Append("Expiration: ").Append(timeInSecs).Append(" secs").AppendLine();
            }

            if (msg.Type is < 0 or > (int)MarkerType.TriangleList)
            {
                description.Append(ErrorStr).Append("Unknown marker type ").AppendLine();
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
                if (string.IsNullOrWhiteSpace(msg.Header.FrameId))
                {
                    description.Append("Frame Locked to: <i>(none)</i>").AppendLine();
                    
                }
                else
                {
                    description.Append("Frame Locked to: <i>").Append(msg.Header.FrameId).Append("</i>").AppendLine();
                }

                if (msg.Pose.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Pose contains NaN values").AppendLine();
                    return;
                }

                var newPose = msg.Pose.Ros2Unity();
                if (!newPose.IsUsable())
                {
                    description.Append(ErrorStr).Append(
                        $"Cannot use ({newPose.position.x}, {newPose.position.y}, {newPose.position.z}) as position. " +
                        "Values too large.");
                }
            }

            void UpdateResourceAsyncLog()
            {
                if (resourceKey != null)
                {
                    return;
                }

                if (msg.Type() == MarkerType.MeshResource)
                {
                    description.Append(WarnStr).Append("Unknown mesh resource '").Append(msg.MeshResource).Append("'")
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

                        if (msg.Scale.MaxAbsCoeff().ApproximatelyZero())
                        {
                            description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                        }

                        if (msg.Scale.IsInvalid())
                        {
                            description.Append(ErrorStr).Append("Scale value invalid").AppendLine();
                        }

                        break;
                    case 2:
                        float sx = (float)msg.Scale.X;
                        AppendColorLog(msg.Color);
                        AppendScalarLog(msg.Scale.X);

                        if (sx.ApproximatelyZero())
                        {
                            description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                        }
                        else if (sx.IsInvalid())
                        {
                            description.Append(ErrorStr).Append("Scale value invalid").AppendLine();
                        }

                        if (msg.Points[0].IsInvalid() || msg.Points[1].IsInvalid())
                        {
                            description.Append(ErrorStr).Append("Start or end point is invalid").AppendLine();
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

                if (msg.Scale.MaxAbsCoeff().ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                }
                else if (msg.Scale.IsInvalid())
                {
                    description.Append(WarnStr).Append("Scale value has NaN").AppendLine();
                }
            }

            void CreateTextResourceLog()
            {
                description.Append("Text: ").Append(msg.Text.Length).Append(" chars").AppendLine();
                AppendColorLog(msg.Color);
                AppendScalarLog(msg.Scale.Z);

                if (msg.Scale.Z.ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                }

                if (msg.Scale.Z.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Scale value of NaN").AppendLine();
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

                if (msg.Points.Length > NativeList.MaxElements)
                {
                    description.Append(ErrorStr).Append("Number of points exceeds maximum of ")
                        .Append(NativeList.MaxElements);
                }

                if (msg.Color.A.ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Color field has alpha 0").AppendLine();
                    return;
                }

                if (msg.Color.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Color field is invalid").AppendLine();
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

                if (elementScale.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Scale value invalid").AppendLine();
                    return;
                }

                if (elementScale.ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Scale value of 0").AppendLine();
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

                if (msg.Points.Length > NativeList.MaxElements)
                {
                    description.Append(ErrorStr).Append("Number of points exceeds maximum of ")
                        .Append(NativeList.MaxElements);
                }

                if (msg.Color.A.ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Color field has alpha 0").AppendLine();
                }
                
                if (msg.Color.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Color field is invalid").AppendLine();
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

                if (msg.Color.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Color field is invalid").AppendLine();
                    return;
                }

                if (msg.Color.A.ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Color field has alpha 0").AppendLine();
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

                if (msg.Points.Length > NativeList.MaxElements)
                {
                    description.Append(ErrorStr).Append("Number of points exceeds maximum of ")
                        .Append(NativeList.MaxElements);
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

                if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
                {
                    description.Append(ErrorStr).Append("Color array length ").Append(msg.Colors.Length)
                        .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                    return;
                }

                if (msg.Points.Length > NativeList.MaxElements)
                {
                    description.Append(ErrorStr).Append("Number of points exceeds maximum of ")
                        .Append(NativeList.MaxElements);
                }

                if (msg.Color.A.ApproximatelyZero())
                {
                    description.Append(WarnStr).Append("Color has alpha 0. Marker will not be visible").AppendLine();
                    return;
                }

                if (msg.Color.IsInvalid())
                {
                    description.Append(ErrorStr).Append("Color has NaN. Marker will not be visible").AppendLine();
                    return;
                }

                if (msg.Points.Length % 3 != 0)
                {
                    description.Append(ErrorStr).Append("Point array length ").Append(msg.Colors.Length)
                        .Append(" needs to be a multiple of 3").AppendLine();
                }

                if (triangleMeshFailedIndex is { } failedIndex)
                {
                    description.Append(ErrorStr).Append("Index ").Append(failedIndex).Append(" has invalid values")
                        .AppendLine();
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