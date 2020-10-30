using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using Pose = UnityEngine.Pose;
using Vector3 = UnityEngine.Vector3;

#if UNITY_WSA
using Microsoft.MixedReality.Toolkit.Input;
#endif

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
        Click,
        Down,
        Up
    }

    public sealed class MarkerObject : ClickableNode,
        IPointerDownHandler, IPointerUpHandler
#if UNITY_WSA
        , IMixedRealityPointerHandler
#endif
    {
        [CanBeNull] MarkerResource resource;
        [CanBeNull] Info<GameObject> resourceInfo;
        readonly StringBuilder description = new StringBuilder();
        bool clickable;
        string id;

        public delegate void MouseEventAction(in Vector3 point, MouseEventType type);

        public event MouseEventAction MouseEvent;

        public override Bounds Bounds => resource != null ? resource.Bounds : new Bounds();
        public override Bounds WorldBounds => resource != null ? resource.WorldBounds : new Bounds();
        public override string Name => name;
        public override Pose BoundsPose => resource != null ? resource.WorldPose : Pose.identity;
        public override Vector3 BoundsScale => resource != null ? resource.WorldScale : Vector3.one;
        public DateTime ExpirationTime { get; private set; }


        public bool Clickable
        {
            get => clickable;
            set
            {
                clickable = value;
                if (resource == null)
                {
                    return;
                }

                resource.Layer = value ? Resource.ClickableLayer : 0;
            }
        }

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

        public void Set([NotNull] Marker msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

            id = MarkerListener.IdFromMessage(msg);
            name = id;

            description.Length = 0;
            description.Append("<b>-- ").Append(id).Append(" --</b>").AppendLine();
            description.Append("Type: ").Append(DescriptionFromType(msg)).AppendLine();

            ExpirationTime = msg.Lifetime.IsZero ? DateTime.MaxValue : DateTime.Now + msg.Lifetime.ToTimeSpan();

            if (!msg.Lifetime.IsZero)
            {
                description.Append("Expiration: ").Append(msg.Lifetime.Secs).AppendLine();
            }

            UpdateResource(msg);

            if (resource == null)
            {
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
                    CrateMeshResource(msg);
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
                    CreateLineList(msg);
                    break;
                }
                case MarkerType.LineStrip:
                {
                    CreateLineStrip(msg);
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
                default:
                    throw new ArgumentOutOfRangeException();
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
            transform.localScale = imageScale;
        }

        void CreateTriangleList([NotNull] Marker msg)
        {
            MeshTrianglesResource meshTriangles = ValidateResource<MeshTrianglesResource>();
            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
            {
                description.Append("Error: Color array length ").Append(msg.Colors.Length)
                    .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                meshTriangles.Set(Array.Empty<Vector3>());
                return;
            }

            if (msg.Color.A == 0)
            {
                description.Append("Error: Color has alpha 0. Marker will not be visible").AppendLine();
                meshTriangles.Set(Array.Empty<Vector3>());
                return;
            }

            meshTriangles.Color = msg.Color.Sanitize().ToUnityColor();
            Vector3[] points = new Vector3[msg.Points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = msg.Points[i].Ros2Unity();
            }

            if (msg.Colors.Length != 0)
            {
                Color[] colors = new Color[msg.Colors.Length];
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

            transform.localScale = msg.Scale.Ros2Unity().Abs();
        }

        void CreatePoints([NotNull] Marker msg)
        {
            PointListResource pointList = ValidateResource<PointListResource>();
            pointList.ElementScale = Mathf.Abs((float) msg.Scale.X);

            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
            {
                description.Append("Error: Color array length ").Append(msg.Colors.Length)
                    .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                pointList.PointsWithColor = Array.Empty<PointWithColor>();
                return;
            }

            if (msg.Color.A == 0)
            {
                description.Append("Error: Color has alpha 0. Marker will not be visible").AppendLine();
                pointList.PointsWithColor = Array.Empty<PointWithColor>();
                return;
            }

            IEnumerable<PointWithColor> points;
            Color32 color32 = msg.Color.Sanitize().ToUnityColor32();
            if (msg.Colors.Length == 0)
            {
                IEnumerable<PointWithColor> PointEnumerator()
                {
                    foreach (Point position in msg.Points)
                    {
                        yield return new PointWithColor(position.Ros2Unity(), color32);
                    }
                }

                points = PointEnumerator();
            }
            else if (color32 == Color.white)
            {
                IEnumerable<PointWithColor> PointEnumerator()
                {
                    for (int i = 0; i < msg.Points.Length; i++)
                    {
                        yield return new PointWithColor(
                            msg.Points[i].Ros2Unity(),
                            msg.Colors[i].ToUnityColor32());
                    }
                }

                points = PointEnumerator();
            }
            else
            {
                Color color = color32;

                IEnumerable<PointWithColor> PointEnumerator()
                {
                    for (int i = 0; i < msg.Points.Length; i++)
                    {
                        yield return new PointWithColor(
                            msg.Points[i].Ros2Unity(),
                            color * msg.Colors[i].ToUnityColor());
                    }
                }

                points = PointEnumerator();
            }

            pointList.Set(points, msg.Points.Length);
            pointList.UseColormap = false;
        }

        void CreateLineStrip([NotNull] Marker msg)
        {
            LineResource lineResource = ValidateResource<LineResource>();
            lineResource.ElementScale = Mathf.Abs((float) msg.Scale.X);

            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
            {
                description.Append("Error: Color array length ").Append(msg.Colors.Length)
                    .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                lineResource.LinesWithColor = Array.Empty<LineWithColor>();
                return;
            }

            if (msg.Color.A == 0)
            {
                description.Append("Error: Color has alpha 0. Marker will not be visible").AppendLine();
                lineResource.LinesWithColor = Array.Empty<LineWithColor>();
                return;
            }

            Color32 color32 = msg.Color.ToUnityColor32();
            IEnumerable<LineWithColor> lines;
            if (msg.Colors.Length == 0)
            {
                float colorAsFloat = PointWithColor.FloatFromColorBits(color32);

                IEnumerable<LineWithColor> LineEnumerator()
                {
                    for (int i = 0; i < msg.Points.Length - 1; i++)
                    {
                        yield return new LineWithColor(
                            msg.Points[i].Ros2Unity(), colorAsFloat,
                            msg.Points[i + 1].Ros2Unity(), colorAsFloat
                        );
                    }
                }

                lines = LineEnumerator();
            }
            else if (color32 == Color.white)
            {
                IEnumerable<LineWithColor> LineEnumerator()
                {
                    for (int i = 0; i < msg.Points.Length - 1; i++)
                    {
                        yield return new LineWithColor(
                            msg.Points[i].Ros2Unity(), msg.Colors[i].ToUnityColor32(),
                            msg.Points[i + 1].Ros2Unity(), msg.Colors[i + 1].ToUnityColor32()
                        );
                    }
                }

                lines = LineEnumerator();
            }
            else
            {
                Color color = color32;

                IEnumerable<LineWithColor> LineEnumerator()
                {
                    for (int i = 0; i < msg.Points.Length - 1; i++)
                    {
                        yield return new LineWithColor(
                            msg.Points[i].Ros2Unity(), color * msg.Colors[i].ToUnityColor(),
                            msg.Points[i + 1].Ros2Unity(), color * msg.Colors[i + 1].ToUnityColor()
                        );
                    }
                }

                lines = LineEnumerator();
            }

            lineResource.Set(lines);
        }

        void CreateLineList([NotNull] Marker msg)
        {
            LineResource lineResource = ValidateResource<LineResource>();
            lineResource.ElementScale = Mathf.Abs((float) msg.Scale.X);

            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
            {
                description.Append("Error: Color array length ").Append(msg.Colors.Length)
                    .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                lineResource.LinesWithColor = Array.Empty<LineWithColor>();
                return;
            }

            if (msg.Color.A == 0)
            {
                description.Append("Error: Color has alpha 0. Marker will not be visible").AppendLine();
                lineResource.LinesWithColor = Array.Empty<LineWithColor>();
                return;
            }

            Color32 color32 = msg.Color.ToUnityColor32();
            IEnumerable<LineWithColor> lines;
            if (msg.Colors.Length == 0)
            {
                float colorAsFloat = PointWithColor.FloatFromColorBits(color32);

                IEnumerable<LineWithColor> LineEnumerator()
                {
                    for (int i = 0; i < msg.Points.Length / 2; i++)
                    {
                        yield return new LineWithColor(
                            msg.Points[2 * i + 0].Ros2Unity(), colorAsFloat,
                            msg.Points[2 * i + 1].Ros2Unity(), colorAsFloat
                        );
                    }
                }

                lines = LineEnumerator();
            }
            else if (color32 == Color.white)
            {
                IEnumerable<LineWithColor> LineEnumerator()
                {
                    for (int i = 0; i < msg.Points.Length / 2; i++)
                    {
                        yield return new LineWithColor(
                            msg.Points[2 * i + 0].Ros2Unity(), msg.Colors[2 * i + 0].ToUnityColor32(),
                            msg.Points[2 * i + 1].Ros2Unity(), msg.Colors[2 * i + 1].ToUnityColor32()
                        );
                    }
                }

                lines = LineEnumerator();
            }
            else
            {
                Color color = color32;

                IEnumerable<LineWithColor> LineEnumerator()
                {
                    for (int i = 0; i < msg.Points.Length / 2; i++)
                    {
                        yield return new LineWithColor(
                            msg.Points[2 * i + 0].Ros2Unity(), color * msg.Colors[2 * i + 0].ToUnityColor(),
                            msg.Points[2 * i + 1].Ros2Unity(), color * msg.Colors[2 * i + 1].ToUnityColor()
                        );
                    }
                }

                lines = LineEnumerator();
            }

            lineResource.Set(lines);
        }

        void CreateMeshList([NotNull] Marker msg)
        {
            MeshListResource meshList = ValidateResource<MeshListResource>();
            meshList.UseColormap = false;
            meshList.UseIntensityForScaleY = false;
            meshList.MeshResource = msg.Type() == MarkerType.CubeList
                ? Resource.Displays.Cube
                : Resource.Displays.Sphere;


            if (msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length)
            {
                description.Append("Error: Color array length ").Append(msg.Colors.Length)
                    .Append(" does not match point array length ").Append(msg.Points.Length).AppendLine();
                meshList.PointsWithColor = Array.Empty<PointWithColor>();
                return;
            }

            if (msg.Color.A == 0)
            {
                description.Append("Error: Color has alpha 0. Marker will not be visible").AppendLine();
                meshList.PointsWithColor = Array.Empty<PointWithColor>();
                return;
            }

            IEnumerable<PointWithColor> points;
            Color32 color32 = msg.Color.Sanitize().ToUnityColor32();
            if (msg.Colors.Length == 0)
            {
                IEnumerable<PointWithColor> PointEnumerator()
                {
                    foreach (Point position in msg.Points)
                    {
                        yield return new PointWithColor(position.Ros2Unity(), color32);
                    }
                }

                points = PointEnumerator();
            }
            else if (color32 == Color.white)
            {
                IEnumerable<PointWithColor> PointEnumerator()
                {
                    for (int i = 0; i < msg.Points.Length; i++)
                    {
                        yield return new PointWithColor(
                            msg.Points[i].Ros2Unity(),
                            msg.Colors[i].ToUnityColor32());
                    }
                }

                points = PointEnumerator();
            }
            else
            {
                Color color = color32;

                IEnumerable<PointWithColor> PointEnumerator()
                {
                    for (int i = 0; i < msg.Points.Length; i++)
                    {
                        yield return new PointWithColor(
                            msg.Points[i].Ros2Unity(),
                            color * msg.Colors[i].ToUnityColor());
                    }
                }

                points = PointEnumerator();
            }

            meshList.Set(points);
        }

        void CreateTextResource([NotNull] Marker msg)
        {
            TextMarkerResource textResource = ValidateResource<TextMarkerResource>();
            textResource.Text = msg.Text;
            textResource.Color = msg.Color.Sanitize().ToUnityColor();
            textResource.BillboardEnabled = msg.Type() != MarkerType.Text;
            transform.localScale = (float) msg.Scale.Z * Vector3.one;

            if (msg.Scale.Z == 0)
            {
                description.Append("Error: Text has scale.z value of 0. It will not be visible").AppendLine();
            }
        }

        void CrateMeshResource([NotNull] Marker msg)
        {
            if (resource is MeshMarkerResource meshMarker)
            {
                meshMarker.Color = msg.Color.Sanitize().ToUnityColor();
            }

            if (msg.Scale.SquaredNorm == 0)
            {
                description.AppendLine("Error: Marker has scale value of 0. It will not be visible");
            }
            else if (msg.Scale.HasNaN())
            {
                description.AppendLine("Error: Marker has a scale value of NaN. It will not be visible");
            }

            transform.localScale = msg.Scale.Ros2Unity().Abs();
        }

        
        void CreateArrow([NotNull] Marker msg)
        {
            ArrowResource arrowMarker = ValidateResource<ArrowResource>();
            arrowMarker.Color = msg.Color.Sanitize().ToUnityColor();
            switch (msg.Points.Length)
            {
                case 0:
                {
                    if (msg.Scale.SquaredNorm == 0)
                    {
                        description.AppendLine("Error: Arrow has scale value of 0. It will not be visible");
                        arrowMarker.Visible = false;
                        return;
                    }

                    if (msg.Scale.HasNaN())
                    {
                        description.AppendLine("Error: Arrow has a scale value of NaN. It will not be visible");
                        arrowMarker.Visible = false;
                        return;
                    }

                    arrowMarker.Visible = true;
                    transform.localScale = msg.Scale.Ros2Unity().Abs();
                    return;
                }                
                case 2:
                {
                    float sx = Mathf.Abs((float) msg.Scale.X);
                    switch (sx)
                    {
                        case 0:
                            description.AppendLine("Error: Arrow has scale.x value of 0. It will not be visible");
                            arrowMarker.Visible = false;
                            return;
                        case float.NaN:
                            description.AppendLine("Error: Arrow has scale.x value of NaN. It will not be visible");
                            arrowMarker.Visible = false;
                            return;
                        default:
                            arrowMarker.Visible = true;
                            arrowMarker.Set(msg.Points[0].Ros2Unity(), msg.Points[1].Ros2Unity(), sx);
                            return;
                    }
                }
                default:
                    description.Append(
                            "Error: Cannot understand arrow message. Point array should have a length of 0 or 2")
                        .AppendLine();
                    break;
            }
        }

        void UpdateResource([NotNull] Marker msg)
        {
            Info<GameObject> newResourceType = GetRequestedResource(msg);
            if (newResourceType == resourceInfo)
            {
                return;
            }

            if (resource != null && resourceInfo != null)
            {
                resource.Suspend();
                ResourcePool.Dispose(resourceInfo, resource.gameObject);
                resource = null;
            }

            resourceInfo = newResourceType;
            if (resourceInfo == null)
            {
                if (msg.Type() != MarkerType.MeshResource)
                {
                    Logger.Warn($"MarkerObject: Marker type '{msg.Type}' has no resource assigned!");
                }
                else
                {
                    description.Append("Error: Unknown mesh resource '").Append(msg.MeshResource).Append("'")
                        .AppendLine();
                    Logger.Warn($"MarkerObject: Unknown mesh resource '{msg.MeshResource}'");
                }

                return;
            }

            GameObject resourceGameObject = ResourcePool.GetOrCreate(resourceInfo, transform);

            resource = resourceGameObject.GetComponent<MarkerResource>();
            if (resource == null)
            {
                if (msg.Type() != MarkerType.MeshResource)
                {
                    // shouldn't happen!
                    Debug.LogWarning("Resource '" + resourceInfo + "' has no MarkerResource!");
                }

                resource = resourceGameObject.AddComponent<AssetWrapperResource>();
            }

            Clickable = Clickable; // reset value
        }

        void UpdateTransform([NotNull] Marker msg)
        {
            if (msg.FrameLocked)
            {
                AttachTo(msg.Header.FrameId);
            }
            else
            {
                AttachTo(msg.Header.FrameId, msg.Header.Stamp);
            }

            if (!msg.Pose.HasNaN())
            {
                transform.SetLocalPose(msg.Pose.Ros2Unity());
            }
        }

        [CanBeNull]
        static Info<GameObject> GetRequestedResource([NotNull] Marker msg)
        {
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
                case MarkerType.MeshResource:
                    if (!Uri.TryCreate(msg.MeshResource, UriKind.Absolute, out Uri uri))
                    {
                        Debug.Log($"MarkerObject: Could not convert '{msg.MeshResource}' into an uri!");
                        return null;
                    }

                    if (!Resource.TryGetResource(uri, out Info<GameObject> info, ConnectionManager.Connection))
                    {
                        Debug.Log($"MarkerObject: Failed to obtain resource '{uri}'!");
                        return null;
                    }

                    return info;
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

        [NotNull]
        static string DescriptionFromType([NotNull] Marker msg)
        {
            switch (msg.Type())
            {
                case MarkerType.Arrow:
                    return "arrow";
                case MarkerType.Cylinder:
                    return "cylinder";
                case MarkerType.Cube:
                    return "cube";
                case MarkerType.Sphere:
                    return "sphere";
                case MarkerType.TextViewFacing:
                    return "text_view_facing";
                case MarkerType.Text:
                    return "text";
                case MarkerType.LineStrip:
                    return "line_strip";
                case MarkerType.LineList:
                    return "line_list";
                case MarkerType.MeshResource:
                    return "mesh_resource:" + msg.MeshResource;
                case MarkerType.CubeList:
                    return "cube_list";
                case MarkerType.SphereList:
                    return "sphere_list";
                case MarkerType.Points:
                    return "points";
                case MarkerType.TriangleList:
                    return "triangle_list";
                case MarkerType.Image:
                    return "image";
                default:
                    return "[unknown]";
            }
        }

        public override void Stop()
        {
            base.Stop();
            MouseEvent = null;

            if (resource == null || resourceInfo == null)
            {
                return;
            }

            resource.Suspend();
            ResourcePool.Dispose(resourceInfo, resource.gameObject);
            resource = null;
            resourceInfo = null;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            MouseEvent?.Invoke(eventData.pointerCurrentRaycast.worldPosition, MouseEventType.Click);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            MouseEvent?.Invoke(eventData.pointerCurrentRaycast.worldPosition, MouseEventType.Down);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            MouseEvent?.Invoke(eventData.pointerCurrentRaycast.worldPosition, MouseEventType.Up);
        }

#if UNITY_WSA
        public override void OnPointerDown(MixedRealityPointerEventData eventData)
        {
            base.OnPointerClicked(eventData);
            Vector3 pointerPosition = ((GGVPointer)eventData.Pointer).Position;
            MouseEvent?.Invoke(pointerPosition, MouseEventType.Down);
        }

        public override void OnPointerDragged(MixedRealityPointerEventData eventData)
        {
            base.OnPointerClicked(eventData);
        }

        public override void OnPointerUp(MixedRealityPointerEventData eventData)
        {
            base.OnPointerClicked(eventData);
            Vector3 pointerPosition = ((GGVPointer)eventData.Pointer).Position;
            MouseEvent?.Invoke(pointerPosition, MouseEventType.Up);
        }

        public override void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            base.OnPointerClicked(eventData);
            Vector3 pointerPosition = ((GGVPointer)eventData.Pointer).Position;
            MouseEvent?.Invoke(pointerPosition, MouseEventType.Click);
        }
#endif
    }

    internal static class MarkerTypeHelper
    {
        public static MarkerType Type([NotNull] this Marker marker)
        {
            return (MarkerType) marker.Type;
        }
    }
}