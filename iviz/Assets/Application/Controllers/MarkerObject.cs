using UnityEngine;
using System.Linq;
using System;
using UnityEngine.EventSystems;
using Iviz.App.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Displays;
using Iviz.App.Listeners;
using Iviz.Resources;

namespace Iviz.App.Listeners
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
    }

    public sealed class MarkerObject : ClickableNode
    {
        static Mesh CachedCube => Resource.Displays.Cube.Object.GetComponent<MeshFilter>().sharedMesh;
        static Mesh CachedSphere => Resource.Displays.SphereSimple.Object.GetComponent<MeshFilter>().sharedMesh;

        MarkerResource resource;
        Resource.Info<GameObject> resourceType;

        public event Action<Vector3, int> Clicked;

        public string Id { get; private set; }

        public override Bounds Bounds => resource?.Bounds ?? new Bounds();
        public override Bounds WorldBounds => resource?.WorldBounds ?? new Bounds();
        public override string Name => name;
        public override Pose BoundsPose => resource?.WorldPose ?? Pose.identity;
        public override Vector3 BoundsScale => resource?.WorldScale ?? Vector3.one;

        public DateTime ExpirationTime { get; private set; }

        bool clickable;
        public bool Clickable
        {
            get => clickable;
            set
            {
                clickable = value;
                if (resource is null)
                {
                    return;
                }
                resource.Layer = value ? Resource.ClickableLayer : 0;
            }
        }

        public bool OcclusionOnly
        {
            get => (resource as ISupportsAROcclusion)?.OcclusionOnly ?? false;
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

            ExpirationTime = msg.Lifetime.IsZero ?
                DateTime.MaxValue :
                DateTime.Now + msg.Lifetime.ToTimeSpan();

            Resource.Info<GameObject> newResourceType = GetRequestedResource(msg);
            if (newResourceType != resourceType)
            {
                if (!(resource is null))
                {
                    resource.Stop();
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
                        Logger.Error($"MarkerObject: Marker type '{msg.Type()}'");
                    }
                    return;
                }
                resource = ResourcePool.GetOrCreate<MarkerResource>(resourceType, transform);
                Clickable = Clickable;
                if (resource is null)
                {
                    Debug.LogError("Resource " + resourceType + " has no MarkerResource!");
                }
            }
            if (resource is null)
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
                    MeshMarkerResource meshMarker = (MeshMarkerResource)resource;
                    meshMarker.Color = msg.Color.Sanitize().ToUnityColor();
                    transform.localScale = msg.Scale.Ros2Unity().Abs();
                    break;
                case MarkerType.TextViewFacing:
                    TextMarkerResource textResource = (TextMarkerResource)resource;
                    textResource.Text = msg.Text;
                    textResource.Color = msg.Color.Sanitize().ToUnityColor();
                    transform.localScale = (float)msg.Scale.Z * Vector3.one;
                    break;
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
                        lineResource.LineScale = (float)msg.Scale.X;
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
                        lineResource.LineScale = (float)msg.Scale.X;
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
                        pointList.Scale = msg.Scale.Ros2Unity().Abs();
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
                case MarkerType.TextViewFacing: return Resource.Displays.Text;
                case MarkerType.LineStrip:
                case MarkerType.LineList:
                    return Resource.Displays.Line;
                case MarkerType.MeshResource:
                    if (!Uri.TryCreate(msg.MeshResource, UriKind.Absolute, out Uri uri))
                    {
                        return null;
                    }
                    return Resource.Displays.Generic.TryGetValue(uri, out Resource.Info<GameObject> info) ? info : null;
                case MarkerType.CubeList:
                case MarkerType.SphereList:
                    return Resource.Displays.MeshList;
                case MarkerType.Points:
                    return Resource.Displays.PointList;
                case MarkerType.TriangleList:
                    return Resource.Displays.MeshTriangles;
                default:
                    return null;
            }
        }

        public override void Stop()
        {
            base.Stop();
            Clicked = null;

            if (resource is null)
            {
                return;
            }

            resource.Stop();
            ResourcePool.Dispose(resourceType, resource.gameObject);
            resource = null;
            resourceType = null;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            switch (LastClickCount)
            {
                case 1:
                    Clicked?.Invoke(eventData.pointerCurrentRaycast.worldPosition, 0);
                    break;
                case 2:
                    Clicked?.Invoke(eventData.pointerCurrentRaycast.worldPosition, 1);
                    break;
            }
        }

    }

    static class MarkerTypeHelper
    {
        public static MarkerType Type(this Marker marker)
        {
            return (MarkerType)marker.Type;
        }
    }
}
