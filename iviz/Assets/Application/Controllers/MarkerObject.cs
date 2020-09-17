using UnityEngine;
using System.Linq;
using System;
using UnityEngine.EventSystems;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Displays;
using Iviz.Msgs.StdMsgs;
using Iviz.Resources;

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

        Text = 100,
        Image = 101,
    }

    public enum MouseEventType
    {
        Click, Down, Up
    }

    public sealed class MarkerObject : ClickableNode, 
        IPointerDownHandler, IPointerUpHandler
#if UNITY_WSA
        , IMixedRealityPointerHandler
#endif    
    {
        static Mesh CachedCube => Resource.Displays.Cube.Object.GetComponent<MeshFilter>().sharedMesh;
        static Mesh CachedSphere => Resource.Displays.SphereSimple.Object.GetComponent<MeshFilter>().sharedMesh;

        MarkerResource resource;
        Resource.Info<GameObject> resourceType;

        public delegate void MouseEventAction(in Vector3 point, MouseEventType type);
        public event MouseEventAction MouseEvent;

        public string Id { get; private set; }

        public override Bounds Bounds => resource?.Bounds ?? new Bounds();
        public override Bounds WorldBounds => resource?.WorldBounds ?? new Bounds();
        public override string Name => name;
        public override Pose BoundsPose => resource?.WorldPose ?? Pose.identity;
        public override Vector3 BoundsScale => resource?.WorldScale ?? Vector3.one;

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
                resource.ColliderEnabled = value;
            }
        }

        public bool OcclusionOnly
        {
            get => (resource as ISupportsAROcclusion)?.OcclusionOnlyActive ?? false;
            set
            {
                if (resource is ISupportsAROcclusion arResource)
                {
                    arResource.OcclusionOnlyActive = value;
                }
            }
        }

        public Color Tint
        {
            get => (resource as ISupportsTint)?.Tint ?? Color.white;
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
            get => resource?.Visible ?? true;
            set
            {
                if (!(resource is null))
                {
                    resource.Visible = value;
                }
            }
        }

        public void Set(Marker msg)
        {
            Id = MarkerListener.IdFromMessage(msg);
            name = Id;

            expirationTime = msg.Lifetime.IsZero ?
                DateTime.MaxValue :
                DateTime.Now + msg.Lifetime.ToTimeSpan();

            Resource.Info<GameObject> newResourceType = GetRequestedResource(msg);
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
                    if (msg.Type() == MarkerType.MeshResource)
                    {
                        Logger.Error($"MarkerObject: Unknown mesh resource '{msg.MeshResource}'");
                    }
                    else
                    {
                        Logger.Error($"MarkerObject: Marker type '{msg.Type}' has no resource assigned!");
                    }
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
                    ArrowResource arrowMarker = (ArrowResource)resource;
                    arrowMarker.Color = msg.Color.Sanitize().ToUnityColor();
                    if (msg.Points.Length == 2)
                    {
                        arrowMarker.Set(msg.Points[0].Ros2Unity(), msg.Points[1].Ros2Unity());
                        float sx = (float)msg.Scale.X;
                        Vector3 scale = new Vector3(1, sx, sx);
                        transform.localScale = scale.Ros2Unity().Abs();
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
                    textResource.EnableBillboard = (msg.Type() != MarkerType.Text);
                    transform.localScale = (float) msg.Scale.Z * Vector3.one;
                    break;
                }
                case MarkerType.CubeList:
                case MarkerType.SphereList:
                    {
                        MeshListResource meshList = (MeshListResource)resource;
                        meshList.UseIntensityTexture = false;
                        meshList.UsePerVertexScale = false;
                        meshList.Mesh = (msg.Type() == MarkerType.CubeList) ? CachedCube : CachedSphere;
                        PointWithColor[] points = new PointWithColor[msg.Points.Length];
                        Color color = msg.Color.Sanitize().ToUnityColor();
                        if (msg.Colors.Length == 0 || color == Color.black)
                        {
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i] = new PointWithColor(msg.Points[i].Ros2Unity(), color);
                            }
                        }
                        else if (color == Color.white)
                        {
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i] = new PointWithColor(
                                    msg.Points[i].Ros2Unity(),
                                    msg.Colors[i].ToUnityColor32());
                            }
                        }
                        else
                        {
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i] = new PointWithColor(
                                    msg.Points[i].Ros2Unity(),
                                    color * msg.Colors[i].ToUnityColor());
                            }
                        }
                        meshList.PointsWithColor = points;
                        break;
                    }
                case MarkerType.LineList:
                    {
                        LineResource lineResource = (LineResource)resource;
                        lineResource.ElementSize = (float)msg.Scale.X;
                        LineWithColor[] lines = new LineWithColor[msg.Points.Length / 2];
                        if (msg.Colors.Length == 0)
                        {
                            Color32 color = msg.Color.Sanitize().ToUnityColor32();
                            for (int i = 0; i < lines.Length; i++)
                            {
                                lines[i] = new LineWithColor(
                                    msg.Points[2 * i + 0].Ros2Unity(), color,
                                    msg.Points[2 * i + 1].Ros2Unity(), color
                                    );
                            }
                        }
                        else
                        {
                            Color color = msg.Color.Sanitize().ToUnityColor();
                            for (int i = 0; i < lines.Length; i++)
                            {
                                lines[i] = new LineWithColor(
                                    msg.Points[2 * i + 0].Ros2Unity(), color * msg.Colors[2 * i + 0].ToUnityColor(),
                                    msg.Points[2 * i + 1].Ros2Unity(), color * msg.Colors[2 * i + 1].ToUnityColor()
                                    );
                            }
                        }
                        lineResource.LinesWithColor = lines;
                        break;
                    }
                case MarkerType.LineStrip:
                    {
                        LineResource lineResource = (LineResource)resource;
                        lineResource.ElementSize = (float)msg.Scale.X;
                        LineWithColor[] lines = new LineWithColor[msg.Points.Length - 1];
                        if (msg.Colors.Length == 0)
                        {
                            Color32 color = msg.Color.Sanitize().ToUnityColor32();
                            for (int i = 0; i < lines.Length; i++)
                            {
                                lines[i] = new LineWithColor(
                                    msg.Points[i + 0].Ros2Unity(), color,
                                    msg.Points[i + 1].Ros2Unity(), color
                                    );
                            }
                        }
                        else
                        {
                            Color color = msg.Color.Sanitize().ToUnityColor();
                            for (int i = 0; i < lines.Length; i++)
                            {
                                lines[i] = new LineWithColor(
                                    msg.Points[i + 0].Ros2Unity(), color * msg.Colors[i + 0].ToUnityColor(),
                                    msg.Points[i + 1].Ros2Unity(), color * msg.Colors[i + 1].ToUnityColor()
                                    );
                            }
                        }
                        lineResource.LinesWithColor = lines;
                        break;
                    }
                case MarkerType.Points:
                    {
                        PointListResource pointList = (PointListResource)resource;
                        pointList.ElementSize = Mathf.Abs((float)msg.Scale.X);
                        PointWithColor[] points = new PointWithColor[msg.Points.Length];
                        Color color = msg.Color.Sanitize().ToUnityColor();
                        if (msg.Colors.Length == 0 || color == Color.black)
                        {
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i] = new PointWithColor(msg.Points[i].Ros2Unity(), color);
                            }
                        }
                        else if (color == Color.white)
                        {
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i] = new PointWithColor(
                                    msg.Points[i].Ros2Unity(),
                                    msg.Colors[i].ToUnityColor32());
                            }
                        }
                        else
                        {
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i] = new PointWithColor(
                                    msg.Points[i].Ros2Unity(),
                                    color * msg.Colors[i].ToUnityColor());
                            }
                        }
                        pointList.PointsWithColor = points;
                        pointList.UseIntensityTexture = false;
                        break;
                    }
                case MarkerType.TriangleList:
                    MeshTrianglesResource meshTriangles = (MeshTrianglesResource)resource;
                    meshTriangles.Color = msg.Color.Sanitize().ToUnityColor();
                    if (msg.Colors.Length != 0)
                    {
                        meshTriangles.Set(
                            msg.Points.Select(x => x.Ros2Unity()).ToArray(),
                            msg.Colors.Select(x => x.ToUnityColor()).ToArray()
                            );
                    }
                    else
                    {
                        meshTriangles.Set(msg.Points.Select(x => x.Ros2Unity()).ToArray());
                    }
                    transform.localScale = msg.Scale.Ros2Unity().Abs();
                    break;
                case MarkerType.Image:
                    ImageResource image = (ImageResource)resource;
                    int count = msg.Colors.Length;
                    int width = (int)msg.Scale.Z;
                    int height = count / width;
                    if (width <= 0 || height <= 0 ||  width * height != count)
                    {
                        Debug.LogWarning("MarkerObject: Invalid image dimensions");
                    }
                    else
                    {
                        bool hasAlpha = msg.Colors.Any(color => color.A < 1);
                        int j = 0;
                        if (hasAlpha)
                        {
                            byte[] data = new byte[count * 3];
                            foreach (ColorRGBA color in msg.Colors)
                            {
                                Color32 color32 = new Color(color.R, color.G, color.B);
                                data[j++] = color32.r;
                                data[j++] = color32.g;
                                data[j++] = color32.b;
                            }
                            image.Set(width, height, 3, data.AsSlice());
                        }
                        else
                        {
                            byte[] data = new byte[count * 4];
                            foreach (ColorRGBA color in msg.Colors)
                            {
                                Color32 color32 = new Color(color.R, color.G, color.B);
                                data[j++] = color32.r;
                                data[j++] = color32.g;
                                data[j++] = color32.b;                                
                                data[j++] = color32.a;                                
                            }
                            image.Set(width, height, 4, data.AsSlice());
                        }
                    }
                    transform.localScale = msg.Scale.Ros2Unity().Abs();
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

        static Resource.Info<GameObject> GetRequestedResource(Marker msg)
        {
            switch (msg.Type())
            {
                case MarkerType.Arrow: return Resource.Displays.Arrow;
                case MarkerType.Cylinder: return Resource.Displays.Cylinder;
                case MarkerType.Cube: return Resource.Displays.Cube;
                case MarkerType.Sphere: return Resource.Displays.Sphere;
                case MarkerType.TextViewFacing: 
                case MarkerType.Text:
                    return Resource.Displays.Text;
                case MarkerType.LineStrip:
                case MarkerType.LineList:
                    return Resource.Displays.Line;
                case MarkerType.MeshResource:
                    if (!Uri.TryCreate(msg.MeshResource, UriKind.Absolute, out Uri uri))
                    {
                        return null;
                    }
                    
                    return Resource.TryGetResource(uri, out Resource.Info<GameObject> info) ? info : null;
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
            return (MarkerType)marker.Type;
        }
    }
}
