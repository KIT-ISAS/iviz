#nullable enable

using System;
using System.Runtime.CompilerServices;
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
    public sealed partial class MarkerObject : IHasBounds
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
        bool triangleMeshHasInvalidIndices;

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
        Transform? IHasBounds.BoundsTransform => ((Component?)resource)?.transform;
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

        public async ValueTask SetAsync(Marker msg)
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
                RosLogger.Error($"{ToString()}: Failed to update resource", e);
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

            RaiseBoundsChanged();
        }

        T ValidateResource<T>() where T : MarkerDisplay
        {
            if (resource is T t) return t;
            
            ThrowHelper.ThrowMissingAssetField("Resource does not have a marker component!");
            return null!; // unreachable
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
                triangleMeshHasInvalidIndices = false;
                return;
            }

            triangleMeshHasInvalidIndices = CopyPoints(msg.Points, msg.Colors, meshResource);
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
                    RosLogger.Warn($"{ToString()}: Marker type '{msg.Type.ToString()}' has no resource assigned!");
                }

                RaiseBoundsChanged();
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
                    RosLogger.Warn(
                        $"{ToString()}: Mesh resource '{resourceKey.ToString()}' has no {nameof(IDisplay)}!");
                }

                // add generic wrapper
                resource = resourceGameObject.AddComponent<AssetWrapperDisplay>();
            }

            if (resource is ISupportsLayer supportsLayer)
            {
                supportsLayer.Layer = LayerType.IgnoreRaycast;
            }

            RaiseBoundsChanged();
        }

        void RaiseBoundsChanged()
        {
            BoundsChanged.TryRaise(this);
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
                MarkerType.MeshResource => RequestMeshResourceAsync(msg.MeshResource),
                _ => default
            };

            static ValueTask<ResourceKey<GameObject>?> AsTask(ResourceKey<GameObject>? val) => new(val);
        }

        async ValueTask<ResourceKey<GameObject>?> RequestMeshResourceAsync(string meshResource)
        {
            StopLoadResourceTask();
            runningTs = new CancellationTokenSource();

            try
            {
                return await Resource.GetGameObjectResourceAsync(meshResource, RosManager.Connection,
                    runningTs.Token);
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                RosLogger.Error($"{ToString()}: " +
                                $"{nameof(Resource.GetGameObjectResourceAsync)} failed for '{meshResource}'", e);
                return null;
            }
            finally
            {
                runningTs.Cancel();
            }
        }

        void StopLoadResourceTask()
        {
            runningTs?.Cancel();
            runningTs = null;
        }

        bool HasSameHash(Marker msg)
        {
            uint currentHash = CalculateMarkerHash(msg);
            if (previousHash == currentHash)
            {
                return true;
            }

            previousHash = currentHash;
            return false;
        }

        public void GetErrorCount(out int totalErrors, out int totalWarnings)
        {
            totalErrors = numErrors;
            totalWarnings = numWarnings;
        }

        public void GenerateLog(StringBuilder description)
        {
            GenerateLog(description, this);
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
            RaiseBoundsChanged();
        }

        public override string ToString() => $"[{nameof(MarkerObject)} {node.Name}]";
    }
}