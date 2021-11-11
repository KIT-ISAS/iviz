﻿#nullable enable

using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
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

    public sealed class MarkerObject : FrameNode
    {
        const string WarnStr = "<color=yellow>Warning:</color> ";
        const string ErrorStr = "<color=red>Error:</color> ";

        const string FloatFormat = "#,0.###";

        readonly StringBuilder description = new(250);

        IDisplay? resource;
        Info<GameObject>? resourceInfo;
        CancellationTokenSource? runningTs;

        MarkerLineHelper? lineHelper;
        MarkerPointHelper? pointHelper;

        MarkerLineHelper LineHelper => lineHelper ??= new MarkerLineHelper();
        MarkerPointHelper PointHelper => pointHelper ??= new MarkerPointHelper();

        Pose currentPose;
        Vector3 currentScale;

        (string Ns, int Id) id;

        int numErrors;
        int numWarnings;

        bool occlusionOnly;

        uint? previousHash;

        float metallic;
        float smoothness;
        Color tint;

        bool triangleListFlipWinding;

        public event Action? BoundsChanged;

        public DateTime ExpirationTime { get; private set; }
        public MarkerType MarkerType { get; private set; }

        public Bounds? Bounds =>
            resource == null ? null : resource.Bounds.TransformBound(resource.GetTransform());

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
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
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
                name = $"{id.Ns}/{id.Id.ToString()}";
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

            if (msg.Type < 0 || msg.Type > (int) MarkerType.TriangleList)
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
            description.Append("Color: ")
                .Append(c.R.ToString(FloatFormat)).Append(" | ")
                .Append(c.G.ToString(FloatFormat)).Append(" | ")
                .Append(c.B.ToString(FloatFormat)).Append(" | ")
                .Append(c.A.ToString(FloatFormat)).AppendLine();
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
                transform.localScale = newScale;
            }

            if (HasSameHash(msg))
            {
                //Debug.Log("Same message!");
                return;
            }

            meshTriangles.Color = msg.Color.Sanitize().ToUnityColor();

            using var points = new Rent<Vector3>(msg.Points.Length);
            for (int i = 0; i < points.Length; i++)
            {
                points.Array[i] = msg.Points[i].Ros2Unity();
            }

            meshTriangles.FlipWinding = TriangleListFlipWinding;

            if (msg.Colors.Length != 0)
            {
                using var colors = new Rent<Color>(msg.Colors.Length);
                for (int i = 0; i < colors.Length; i++)
                {
                    colors.Array[i] = msg.Colors[i].ToUnityColor();
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
            pointList.ElementScale = Mathf.Abs((float) msg.Scale.X);

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
            float elementScale = Mathf.Abs((float) msg.Scale.X);

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
            transform.localScale = Vector3.one;

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
            textResource.ElementSize = (float) msg.Scale.Z;

            description.Append("Text: ").Append(msg.Text.Length).Append(" chars").AppendLine();
            AppendColor(msg.Color);
            AppendScale(msg.Scale.Z);

            if (Mathf.Approximately((float) msg.Scale.Z, 0) || msg.Scale.Z.IsInvalid())
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

            Vector3 newScale = msg.Scale.Ros2Unity().Abs();
            transform.localScale = currentScale = newScale;
        }

        void CreateArrow(Marker msg)
        {
            var arrowMarker = ValidateResource<ArrowResource>();
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

                    AppendColor(msg.Color);
                    AppendScale(msg.Scale);
                    return;
                }
                case 2:
                {
                    float sx = Mathf.Abs((float) msg.Scale.X);
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
                    description.Append(ErrorStr).Append("Point array should have a length of 0 or 2").AppendLine();
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

                return;
            }

            GameObject resourceGameObject = ResourcePool.Rent(resourceInfo, transform);

            resource = resourceGameObject.GetComponent<IDisplay>();
            if (resource != null)
            {
                Layer = LayerType.IgnoreRaycast;
                resource.Name = gameObject.name;
                BoundsChanged?.Invoke();
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
            BoundsChanged?.Invoke();
        }

        void UpdateTransform(Marker msg)
        {
            AttachTo(msg.Header);
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

            transform.SetLocalPose(currentPose = newPose);
        }

        async ValueTask<Info<GameObject>?> GetRequestedResource(Marker msg)
        {
            if (msg.Type() != MarkerType.MeshResource)
            {
                StopLoadResourceTask();
            }

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
                case MarkerType.MeshResource:
                    if (Resource.TryGetResource(msg.MeshResource, out var newResourceInfo))
                    {
                        return newResourceInfo;
                    }

                    try
                    {
                        StopLoadResourceTask();
                        runningTs = new CancellationTokenSource();
                        description.Append("** Mesh is being downloaded...").AppendLine();

                        var result = await Resource.GetGameObjectResourceAsync(msg.MeshResource,
                            ConnectionManager.ServiceProvider, runningTs.Token);
                        description.Append(result != null ? "** Download finished." : "** Download failed.")
                            .AppendLine();
                        return result;
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception e)
                    {
                        RosLogger.Error($"{this}: LoadResourceAsync failed for '{msg.MeshResource}'", e);
                        return null;
                    }

                default:
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
                case MarkerType.Invalid:
                    return "Invalid";
                default:
                    return $"Unknown ({msg.Type.ToString()})";
            }
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

        protected override void Stop()
        {
            base.Stop();

            StopLoadResourceTask();

            DiscardResource();
            previousHash = null;
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

        public override string ToString() => $"[MarkerObject {name}]";

        void OnDestroy()
        {
            StopLoadResourceTask();
        }
    }


    internal static class MarkerTypeHelper
    {
        public static MarkerType Type(this Marker marker) => (MarkerType) marker.Type;
    }
}