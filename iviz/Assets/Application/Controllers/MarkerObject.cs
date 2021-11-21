#nullable enable

using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Controllers
{
    public enum MarkerType
    {
        Invalid = -1,
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
    }

    public enum MouseEventType
    {
        Click = InteractiveMarkerFeedback.BUTTON_CLICK,
        Down = InteractiveMarkerFeedback.MOUSE_DOWN,
        Up = InteractiveMarkerFeedback.MOUSE_UP
    }

    public sealed class MarkerObject
    {
        const string WarnStr = "<color=yellow>Warning:</color> ";
        const string ErrorStr = "<color=red>Error:</color> ";
        const string FloatFormat = "#,0.###";

        readonly StringBuilder description = new(250);
        readonly FrameNode node;

        IDisplay? resource;
        Info<GameObject>? resourceInfo;
        CancellationTokenSource? runningTs;

        MarkerLineHelper? lineHelper;
        MarkerPointHelper? pointHelper;

        Pose currentPose;
        Vector3 currentScale;

        (string Ns, int Id) id;

        int numErrors;
        int numWarnings;

        uint? previousHash;

        bool occlusionOnly;
        bool triangleListFlipWinding;
        float metallic;
        float smoothness;
        Color tint;

        public event Action? BoundsChanged;

        MarkerLineHelper LineHelper => lineHelper ??= new MarkerLineHelper();
        MarkerPointHelper PointHelper => pointHelper ??= new MarkerPointHelper();
        public DateTime ExpirationTime { get; private set; }
        public MarkerType MarkerType { get; private set; }
        public Bounds? Bounds => resource?.Bounds.TransformBound(resource.GetTransform());
        public Transform Transform => node.transform;

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

        public MarkerObject(TfFrame parent)
        {
            node = FrameNode.Instantiate("[MarkerObject]");
            node.Parent = parent;
        }

        public async ValueTask SetAsync(Marker msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

            (string Ns, int Id) newId = MarkerListener.IdFromMessage(msg);
            if (id != newId)
            {
                id = newId;
                node.gameObject.name = $"{id.Ns}/{id.Id.ToString()}";
            }

            numWarnings = 0;
            numErrors = 0;

            description.Length = 0;
            description.Append("<color=#800000ff><b>").Append(id.Ns.Length != 0 ? id.Ns : "[]").Append("/")
                .Append(id.Id)
                .Append("</b></color>").AppendLine();

            description.Append("Type: <b>");
            description.Append(DescriptionFromType(msg));
            if (msg.Type() == MarkerType.MeshResource)
            {
                description.Append(": ").Append(msg.MeshResource);
            }

            description.Append("</b>").AppendLine();

            if (msg.Lifetime == default)
            {
                ExpirationTime = DateTime.MaxValue;
                description.Append("Expiration: None").AppendLine();
            }
            else
            {
                ExpirationTime = GameThread.Now + msg.Lifetime.ToTimeSpan();
                description.Append("Expiration: ").Append(msg.Lifetime.Secs).Append(" secs").AppendLine();
            }

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
                if (msg.Type() != MarkerType.MeshResource)
                {
                    return;
                }

                description.Append(ErrorStr).Append("Failed to load mesh resource. See Log.").AppendLine();
                numErrors++;

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
        }

        T ValidateResource<T>() where T : MarkerResource =>
            resource is T result
                ? result
                : throw new InvalidOperationException("Resource is not set!");

        void AppendColor(in ColorRGBA c)
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

        void AppendScale(in Msgs.GeometryMsgs.Vector3 c)
        {
            description.Append("Scale: [")
                .Append(c.X.ToString(FloatFormat)).Append(" | ")
                .Append(c.Y.ToString(FloatFormat)).Append(" | ")
                .Append(c.Z.ToString(FloatFormat)).Append("]").AppendLine();
        }

        void AppendScale(double c)
        {
            description.Append("Scale: ").Append(c.ToString(FloatFormat)).AppendLine();
        }

        void CreateTriangleList(Marker msg)
        {
            AppendScale(msg.Scale);
            AppendColor(msg.Color);

            if (msg.Points.Length == 0)
            {
                description.Append("Elements: Empty").AppendLine();
            }
            else
            {
                description.Append("Elements: ").Append(msg.Points.Length).AppendLine();
            }

            var meshTriangles = ValidateResource<MeshTrianglesResource>();
            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
            {
                description.Append(ErrorStr).Append("Color array length ").Append(msg.Colors.Length)
                    .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                meshTriangles.Clear();
                numErrors++;
                return;
            }

            if (Mathf.Approximately(msg.Color.A, 0))
            {
                description.Append(WarnStr).Append("Color has alpha 0. Marker will not be visible").AppendLine();
                meshTriangles.Clear();
                numWarnings++;
                return;
            }

            if (msg.Color.HasNaN())
            {
                description.Append(ErrorStr).Append("Color has NaN. Marker will not be visible").AppendLine();
                meshTriangles.Clear();
                numWarnings++;
                return;
            }

            if (msg.Points.Length % 3 != 0)
            {
                description.Append(ErrorStr).Append("Point array length ").Append(msg.Colors.Length)
                    .Append(" needs to be a multiple of 3").AppendLine();
                meshTriangles.Clear();
                numErrors++;
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
            for (int i = 0; i < srcPoints.Length; i++)
            {
                points.Array[i] = srcPoints[i].Ros2Unity();
            }

            if (msg.Colors.Length != 0)
            {
                var srcColors = msg.Colors;
                using var colors = new Rent<Color>(srcColors.Length);
                for (int i = 0; i < srcColors.Length; i++)
                {
                    ref var color = ref colors.Array[i]; 
                    (color.r, color.g, color.b, color.a) = srcColors[i];
                }

                meshTriangles.Set(points, colors);
            }
            else
            {
                meshTriangles.Set(points);
            }
        }

        void CreatePoints(Marker msg)
        {
            var pointList = ValidateResource<PointListResource>();
            pointList.ElementScale = Mathf.Abs((float)msg.Scale.X);

            AppendColor(msg.Color);
            AppendScale(msg.Scale.X);

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

            AppendColor(msg.Color);
            AppendScale(elementScale);

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

            AppendColor(msg.Color);
            AppendScale(msg.Scale);

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

            description.Append("Text: ").Append(msg.Text.Length).Append(" chars").AppendLine();
            AppendColor(msg.Color);
            AppendScale(msg.Scale.Z);

            if (Mathf.Approximately((float)msg.Scale.Z, 0) || msg.Scale.Z.IsInvalid())
            {
                description.Append(WarnStr).Append("Scale value of 0 or NaN").AppendLine();
                numWarnings++;
            }
        }

        void CreateMeshResource(Marker msg)
        {
            if (resource is MeshMarkerResource meshMarker)
            {
                meshMarker.Color = msg.Color.Sanitize().ToUnityColor();
            }

            AppendColor(msg.Color);
            AppendScale(msg.Scale);

            if (Mathf.Approximately((float)msg.Scale.SquaredNorm, 0))
            {
                description.Append(WarnStr).Append("Scale value of 0").AppendLine();
                numWarnings++;
            }
            else if (msg.Scale.HasNaN())
            {
                description.Append(WarnStr).Append("Scale value has NaN").AppendLine();
                numWarnings++;
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
                    if (Mathf.Approximately((float)msg.Scale.SquaredNorm, 0))
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

                    AppendColor(msg.Color);
                    AppendScale(msg.Scale);
                    return;
                }
                case 2:
                {
                    float sx = Mathf.Abs((float)msg.Scale.X);
                    AppendColor(msg.Color);
                    AppendScale(msg.Scale.X);
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
                    description.Append(ErrorStr).Append("Point array must have a length of 0 or 2").AppendLine();
                    numErrors++;
                    break;
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
                    description.Append(ErrorStr).AppendLine("Unknown marker type ").Append(msg.Type).AppendLine();
                    numErrors++;
                }
                else
                {
                    description.Append(WarnStr)
                        .Append("Unknown mesh resource '").Append(msg.MeshResource).Append("'")
                        .AppendLine();
                    numWarnings++;
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
                BoundsChanged?.Invoke();
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

        void UpdateTransform(Marker msg)
        {
            node.AttachTo(msg.Header);
            description.Append("Frame Locked to: <i>")
                .Append(string.IsNullOrEmpty(msg.Header.FrameId) ? "(none)" : msg.Header.FrameId)
                .Append("</i>").AppendLine();

            if (msg.Pose.HasNaN())
            {
                description.Append(WarnStr).Append("Pose contains NaN values").AppendLine();
                numWarnings++;
                return;
            }

            Pose newPose = msg.Pose.Ros2Unity();
            if (newPose == currentPose)
            {
                return;
            }

            if (!newPose.IsUsable())
            {
                numErrors++;
                description.Append(ErrorStr).Append(
                    $"Cannot use ({newPose.position.x}, {newPose.position.y}, {newPose.position.z}) as position. Values too large.");
                newPose = Pose.identity;
            }

            Transform.SetLocalPose(currentPose = newPose);
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
            description.Append("** Mesh is being downloaded...").AppendLine();

            try
            {
                var result = await Resource.GetGameObjectResourceAsync(meshResource,
                    ConnectionManager.ServiceProvider, runningTs.Token);
                runningTs.Token.ThrowIfCancellationRequested();
                description.Append(result != null ? "** Download finished." : "** Download failed.").AppendLine();
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

        public void GenerateLog(StringBuilder baseDescription)
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
            if (useScale)
            {
                hash = Crc32Calculator.Compute(msg.Scale, hash);
            }

            return hash;
        }

        public void Stop()
        {
            StopLoadResourceTask();
            DiscardResource();
            previousHash = null;
            BoundsChanged = null;
            node.DestroySelf();
        }

        void DiscardResource()
        {
            if (resource == null || resourceInfo == null)
            {
                return;
            }

            resource.ReturnToPool(resourceInfo);
            resource = null;
            resourceInfo = null;
        }

        public override string ToString() => $"[MarkerObject {node.name}]";
    }


    internal static class MarkerTypeHelper
    {
        public static MarkerType Type(this Marker marker) => (MarkerType)marker.Type;
    }
}