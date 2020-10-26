using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Resources;
using Logger = Iviz.Logger;
using Pose = UnityEngine.Pose;
using Vector3 = UnityEngine.Vector3;

#if UNITY_WSA
using Microsoft.MixedReality.Toolkit.Input;
#endif

namespace Iviz.Controllers
{
    enum MarkerType
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
        /// New: Text marker that does not face the camera
        /// </summary>
        Text = 100,

        /// <summary>
        /// New: Image marker. Pixels in Colors field, image width in Scale.z
        /// </summary>
        Image = 101,
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
        MarkerResource resource;
        Info<GameObject> resourceType;

        public delegate void MouseEventAction(in Vector3 point, MouseEventType type);

        public event MouseEventAction MouseEvent;

        string Id { get; set; }

        public override Bounds Bounds => resource != null ? resource.Bounds : new Bounds();
        public override Bounds WorldBounds => resource != null ? resource.WorldBounds : new Bounds();
        public override string Name => name;
        public override Pose BoundsPose => resource != null ? resource.WorldPose : Pose.identity;
        public override Vector3 BoundsScale => resource != null ? resource.WorldScale : Vector3.one;

        DateTime expirationTime;

        bool clickable;

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

        public void Set(Marker msg)
        {
            Id = MarkerListener.IdFromMessage(msg);
            name = Id;

            expirationTime = msg.Lifetime.IsZero ? DateTime.MaxValue : DateTime.Now + msg.Lifetime.ToTimeSpan();

            Info<GameObject> newResourceType = GetRequestedResource(msg);
            if (newResourceType != resourceType)
            {
                if (resource != null)
                {
                    resource.Suspend();
                    ResourcePool.Dispose(resourceType, resource.gameObject);
                    resource = null;
                }

                resourceType = newResourceType;
                if (resourceType == null)
                {
                    Logger.Error(msg.Type() == MarkerType.MeshResource
                        ? $"MarkerObject: Unknown mesh resource '{msg.MeshResource}'"
                        : $"MarkerObject: Marker type '{msg.Type}' has no resource assigned!");

                    return;
                }

                GameObject newGameObject = ResourcePool.GetOrCreate(resourceType, transform);
                resource = newGameObject.GetComponent<MarkerResource>();
                if (resource == null)
                {
                    if (msg.Type() != MarkerType.MeshResource)
                    {
                        Debug.LogWarning("Resource '" + resourceType + "' has no MarkerResource!");
                    }

                    resource = newGameObject.AddComponent<AssetWrapperResource>();
                }

                Clickable = Clickable; // reset value
            }

            if (resource == null)
            {
                return;
            }

            UpdateTransform(msg);

            switch (msg.Type())
            {
                case MarkerType.Arrow:
                    ArrowResource arrowMarker = (ArrowResource) resource;
                    arrowMarker.Color = msg.Color.Sanitize().ToUnityColor();
                    if (msg.Points.Length == 2)
                    {
                        float sx = Mathf.Abs((float) msg.Scale.X);
                        arrowMarker.Set(msg.Points[0].Ros2Unity(), msg.Points[1].Ros2Unity(), sx);
                    }
                    else if (msg.Points.Length == 0)
                    {
                        transform.localScale = msg.Scale.Ros2Unity().Abs();
                    }
                    else
                    {
                        Logger.Debug("MarkerObject: Cannot understand marker message.");
                    }

                    break;
                case MarkerType.Cube:
                case MarkerType.Sphere:
                case MarkerType.Cylinder:
                case MarkerType.MeshResource:
                    if (resource is MeshMarkerResource meshMarker)
                    {
                        meshMarker.Color = msg.Color.Sanitize().ToUnityColor();
                    }

                    transform.localScale = msg.Scale.Ros2Unity().Abs();
                    break;
                case MarkerType.TextViewFacing:
                case MarkerType.Text:
                {
                    TextMarkerResource textResource = (TextMarkerResource) resource;
                    textResource.Text = msg.Text;
                    textResource.Color = msg.Color.Sanitize().ToUnityColor();
                    textResource.BillboardEnabled = (msg.Type() != MarkerType.Text);
                    transform.localScale = (float) msg.Scale.Z * Vector3.one;
                    break;
                }
                case MarkerType.CubeList:
                case MarkerType.SphereList:
                {
                    MeshListResource meshList = (MeshListResource) resource;
                    meshList.UseColormap = false;
                    meshList.UseIntensityForScaleY = false;
                    meshList.MeshResource = msg.Type() == MarkerType.CubeList
                        ? Resource.Displays.Cube
                        : Resource.Displays.Sphere;
                    if ((msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length) || msg.Color.A == 0)
                    {
                        Debug.Log("MarkerObject: Received list marker with unset or incorrect colors!");
                        meshList.PointsWithColor = Array.Empty<PointWithColor>();
                        break;
                    }

                    IEnumerable<PointWithColor> points;
                    Color32 color32 = msg.Color.Sanitize().ToUnityColor32();
                    if (msg.Colors.Length == 0)
                    {
                        IEnumerable<PointWithColor> PointEnumerator()
                        {
                            foreach (var position in msg.Points)
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
                    break;
                }
                case MarkerType.LineList:
                {
                    LineResource lineResource = (LineResource) resource;
                    lineResource.ElementScale = Mathf.Abs((float) msg.Scale.X);
                    if ((msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length) || msg.Color.A == 0)
                    {
                        Debug.Log("MarkerObject: Received linelist marker with unset or incorrect colors!");
                        lineResource.LinesWithColor = Array.Empty<LineWithColor>();
                        break;
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
                    break;
                }
                case MarkerType.LineStrip:
                {
                    LineResource lineResource = (LineResource) resource;
                    lineResource.ElementScale = Mathf.Abs((float) msg.Scale.X);
                    if ((msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length) || msg.Color.A == 0 ||
                        msg.Color == ColorRGBA.Black)
                    {
                        Debug.Log("MarkerObject: Received linelist marker with unset or incorrect colors!");
                        lineResource.LinesWithColor = Array.Empty<LineWithColor>();
                        break;
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
                    break;
                }
                case MarkerType.Points:
                {
                    PointListResource pointList = (PointListResource) resource;
                    pointList.ElementScale = Mathf.Abs((float) msg.Scale.X);
                    if ((msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length) || msg.Color.A == 0)
                    {
                        Debug.Log("MarkerObject: Received pointlist marker with unset or incorrect colors!");
                        pointList.PointsWithColor = Array.Empty<PointWithColor>();
                        break;
                    }

                    IEnumerable<PointWithColor> points;
                    Color32 color32 = msg.Color.Sanitize().ToUnityColor32();
                    if (msg.Colors.Length == 0)
                    {
                        IEnumerable<PointWithColor> PointEnumerator()
                        {
                            foreach (var position in msg.Points)
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
                    break;
                }
                case MarkerType.TriangleList:
                {
                    MeshTrianglesResource meshTriangles = (MeshTrianglesResource) resource;
                    if ((msg.Colors.Length != 0 && msg.Colors.Length != msg.Points.Length) || msg.Color.A == 0)
                    {
                        Debug.Log("MarkerObject: Received trianglelist marker with unset or incorrect colors!");
                        meshTriangles.Set(Array.Empty<Vector3>());
                        break;
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
                    break;
                }
                case MarkerType.Image:
                    ImageResource image = (ImageResource) resource;
                    int count = msg.Colors.Length;
                    int width = (int) msg.Scale.Z;
                    int height = width == 0 ? 0 : count / width;
                    if (width <= 0 || height <= 0 || width * height != count)
                    {
                        Debug.LogWarning("MarkerObject: Invalid image dimensions");
                        break;
                    }
                    else
                    {
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
                    }

                    Vector3 imageScale = new Msgs.GeometryMsgs.Vector3(msg.Scale.X, msg.Scale.Y, 1).Ros2Unity().Abs();
                    transform.localScale = imageScale;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void UpdateTransform(Marker msg)
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

        static Info<GameObject> GetRequestedResource(Marker msg)
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

                    if (Resource.TryGetResource(uri, out Info<GameObject> info, ConnectionManager.Connection))
                    {
                        return info;
                    }

                    Debug.Log($"MarkerObject: Failed to obtain resource '{uri}'!");
                    return null;
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

        public override void Stop()
        {
            base.Stop();
            MouseEvent = null;

            if (resource == null)
            {
                return;
            }

            resource.Suspend();
            ResourcePool.Dispose(resourceType, resource.gameObject);
            resource = null;
            resourceType = null;
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
        public static MarkerType Type(this Marker marker)
        {
            return (MarkerType) marker.Type;
        }
    }
}