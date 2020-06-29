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
        ARROW = Marker.ARROW,
        CUBE = Marker.CUBE,
        SPHERE = Marker.SPHERE,
        CYLINDER = Marker.CYLINDER,
        LINE_STRIP = Marker.LINE_STRIP,
        LINE_LIST = Marker.LINE_LIST,
        CUBE_LIST = Marker.CUBE_LIST,
        SPHERE_LIST = Marker.SPHERE_LIST,
        POINTS = Marker.POINTS,
        TEXT_VIEW_FACING = Marker.TEXT_VIEW_FACING,
        MESH_RESOURCE = Marker.MESH_RESOURCE,
        TRIANGLE_LIST = Marker.TRIANGLE_LIST,
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

        public bool ColliderEnabled
        {
            get => resource?.ColliderEnabled ?? false;
            set
            {
                if (!(resource is null))
                {
                    resource.ColliderEnabled = value;
                }
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
                    ResourcePool.Dispose(resourceType, resource.gameObject);
                    resource = null;
                }
                resourceType = newResourceType;
                if (resourceType == null)
                {
                    if (msg.Type() == MarkerType.MESH_RESOURCE)
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

            resource.gameObject.layer = Resource.ClickableLayer;

            switch (msg.Type())
            {
                case MarkerType.ARROW:
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
                case MarkerType.CUBE:
                case MarkerType.SPHERE:
                case MarkerType.CYLINDER:
                case MarkerType.MESH_RESOURCE:
                    MeshMarkerResource meshMarker = (MeshMarkerResource)resource;
                    meshMarker.Color = msg.Color.Sanitize().ToUnityColor();
                    transform.localScale = msg.Scale.Ros2Unity().Abs();
                    break;
                case MarkerType.TEXT_VIEW_FACING:
                    TextMarkerResource textResource = (TextMarkerResource)resource;
                    textResource.Text = msg.Text;
                    textResource.Color = msg.Color.Sanitize().ToUnityColor();
                    transform.localScale = (float)msg.Scale.Z * Vector3.one;
                    break;
                case MarkerType.CUBE_LIST:
                case MarkerType.SPHERE_LIST:
                    {
                        MeshListResource meshList = (MeshListResource)resource;
                        meshList.UseIntensityTexture = false;
                        meshList.UsePerVertexScale = false;
                        meshList.Mesh = (msg.Type() == MarkerType.CUBE_LIST) ? CachedCube : CachedSphere;
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
                case MarkerType.LINE_LIST:
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
                case MarkerType.LINE_STRIP:
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
                case MarkerType.POINTS:
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
                case MarkerType.TRIANGLE_LIST:
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
                case MarkerType.ARROW: return Resource.Displays.Arrow;
                case MarkerType.CYLINDER: return Resource.Displays.Cylinder;
                case MarkerType.CUBE: return Resource.Displays.Cube;
                case MarkerType.SPHERE: return Resource.Displays.Sphere;
                case MarkerType.TEXT_VIEW_FACING: return Resource.Displays.Text;
                case MarkerType.LINE_STRIP:
                case MarkerType.LINE_LIST:
                    return Resource.Displays.Line;
                case MarkerType.MESH_RESOURCE:
                    if (!Uri.TryCreate(msg.MeshResource, UriKind.Absolute, out Uri uri))
                    {
                        return null;
                    }
                    return Resource.Displays.Generic.TryGetValue(uri, out Resource.Info<GameObject> info) ? info : null;
                case MarkerType.CUBE_LIST:
                case MarkerType.SPHERE_LIST:
                    return Resource.Displays.MeshList;
                case MarkerType.POINTS:
                    return Resource.Displays.PointList;
                case MarkerType.TRIANGLE_LIST:
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
            resource.ColliderEnabled = false;
            ResourcePool.Dispose(resourceType, resource.gameObject);
            resource = null;
            resourceType = null;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (LastClickCount == 1)
            {
                Clicked?.Invoke(eventData.pointerCurrentRaycast.worldPosition, 0);
            }
            else if (LastClickCount == 2)
            {
                Clicked?.Invoke(eventData.pointerCurrentRaycast.worldPosition, 1);
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
