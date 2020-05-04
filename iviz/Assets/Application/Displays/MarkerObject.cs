using UnityEngine;
using System.Linq;
using System;
using UnityEngine.EventSystems;
using Iviz.Msgs.visualization_msgs;

namespace Iviz.App
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

    public class MarkerObject : ClickableDisplay
    {
        const string packagePrefix = "package://ibis/";

        public event Action<Vector3, int> Clicked;

        public string Id { get; private set; }

        MarkerResource resource;
        Resource.Info resourceType;
        Mesh cacheCube, cacheSphere;

        public DateTime ExpirationTime { get; private set; }

        void Awake()
        {
            cacheCube = Resource.Markers.Cube.GameObject.GetComponent<MeshFilter>().sharedMesh;
            cacheSphere = Resource.Markers.SphereSimple.GameObject.GetComponent<MeshFilter>().sharedMesh;
        }

        public void Set(Marker msg)
        {
            Id = MarkerListener.IdFromMessage(msg);
            name = Id;

            ExpirationTime = msg.lifetime.IsZero() ?
                DateTime.MaxValue :
                DateTime.Now + msg.lifetime.ToTimeSpan();

            Resource.Info newResourceType = GetRequestedResource(msg);
            if (newResourceType != resourceType)
            {
                if (resource != null)
                {
                    ResourcePool.Dispose(resourceType, resource.gameObject);
                    resource = null;
                }
                resourceType = newResourceType;
                if (resourceType == null)
                {
                    if (msg.Type() == MarkerType.MESH_RESOURCE)
                    {
                        Logger.Error($"MarkerObject: Unknown mesh resource '{msg.mesh_resource}'");
                    }
                    else
                    {
                        Logger.Error($"MarkerObject: Marker type '{msg.Type()}'");
                    }
                    return;
                }
                resource = ResourcePool.GetOrCreate(resourceType, transform).GetComponent<MarkerResource>();
                if (resource == null)
                {
                    Debug.LogError("Resource " + resourceType + " has no MarkerResource!");
                }
            }
            if (resource == null)
            {
                return;
            }

            UpdateTransform(msg);

            Bounds = resource.Bounds;
            resource.gameObject.layer = Resource.ClickableLayer;
            resource.Color = msg.color.ToUnityColor();


            switch(msg.Type()) {
                case MarkerType.CUBE:
                case MarkerType.SPHERE:
                case MarkerType.CYLINDER:
                    transform.localScale = msg.scale.Ros2Unity().Abs();
                    break;
                case MarkerType.TEXT_VIEW_FACING:
                    (resource as TextMarkerResource).Text = msg.text;
                    transform.localScale = (float)msg.scale.z * Vector3.one;
                    break;
                case MarkerType.CUBE_LIST:
                case MarkerType.SPHERE_LIST:
                    TriangleListResource meshList = resource as TriangleListResource;
                    meshList.Mesh = (msg.Type() == MarkerType.CUBE_LIST) ? cacheCube : cacheSphere;
                    meshList.SetSize(msg.points.Length);
                    meshList.Scale = msg.scale.Ros2Unity().Abs();
                    meshList.Color = msg.color.ToUnityColor();
                    meshList.Colors = (msg.colors.Length == 0) ? null : msg.colors.Select(x => x.ToUnityColor32());
                    meshList.Points = msg.points.Select(x => x.Ros2Unity());
                    break;
                case MarkerType.LINE_LIST:
                case MarkerType.LINE_STRIP:
                    resource.Width = (float)msg.scale.x;
                    break;
                case MarkerType.POINTS:
                    PointListResource pointList = resource as PointListResource;
                    pointList.Size = msg.points.Length;
                    pointList.Scale = msg.scale.Ros2Unity().Abs();
                    pointList.Color = msg.color.ToUnityColor();
                    pointList.Colors = (msg.colors.Length == 0) ? null : msg.colors.Select(x => x.ToUnityColor32());
                    pointList.Points = msg.points.Select(x => x.Ros2Unity());
                    break;
                case MarkerType.TRIANGLE_LIST:
                    MeshTrianglesResource meshMarker = resource as MeshTrianglesResource;
                    meshMarker.Color = Color.white ;// msg.color.ToUnityColor();
                    meshMarker.Set(msg.points.Select(x => x.Ros2Unity()).ToArray());
                    break;
            }
        }

        void UpdateTransform(Marker msg)
        {
            if (msg.frame_locked)
            {
                SetParent(msg.header.frame_id);
            }
            else if (msg.header.frame_id == "")
            {
                Parent = TFListener.DisplaysFrame;
            }
            else
            {
                Pose pose = TFListener.GetOrCreateFrame(msg.header.frame_id).transform.AsPose();
                Parent = TFListener.BaseFrame;
                transform.SetLocalPose(pose);
            }

            transform.SetLocalPose(msg.pose.Ros2Unity());
        }

        Resource.Info GetRequestedResource(Marker msg)
        {
            switch (msg.Type())
            {
                case MarkerType.ARROW: return Resource.Markers.Arrow;
                case MarkerType.CYLINDER: return Resource.Markers.Cylinder;
                case MarkerType.CUBE: return Resource.Markers.Cube;
                case MarkerType.SPHERE: return Resource.Markers.Sphere;
                case MarkerType.TEXT_VIEW_FACING: return Resource.Markers.Text;
                case MarkerType.LINE_STRIP: return Resource.Markers.LineStrip;
                case MarkerType.MESH_RESOURCE:
                    if (!Uri.IsWellFormedUriString(msg.mesh_resource, UriKind.Absolute))
                    {
                        return null;
                    }
                    if (msg.mesh_resource.StartsWith(packagePrefix))
                    {
                        string resourcePath = msg.mesh_resource.Substring(packagePrefix.Length);
                        return Resource.Markers.Generic.TryGetValue(resourcePath, out Resource.Info info) ? info : null;
                    }
                    return null;
                case MarkerType.CUBE_LIST:
                case MarkerType.SPHERE_LIST:
                    return Resource.Markers.MeshList;
                case MarkerType.POINTS:
                    return Resource.Markers.PointList;
                case MarkerType.TRIANGLE_LIST:
                    return Resource.Markers.MeshTriangles;
                default:
                    return null;
            }
        }

        public override void Stop()
        {
            base.Stop();
            if (resource == null)
            {
                return;
            }
            resource.Collider.enabled = false;
            ResourcePool.Dispose(resourceType, resource.gameObject);
            resource = null;
            resourceType = null;
            Clicked = null;
        }

        public void EnableColliders(bool b)
        {
            resource.Collider.enabled = b;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!eventData.IsPointerMoving())
            {
                if (eventData.clickCount == 1)
                {
                    Clicked?.Invoke(eventData.pointerCurrentRaycast.worldPosition, 0);
                }
                else if (eventData.clickCount == 2)
                {
                    Clicked?.Invoke(eventData.pointerCurrentRaycast.worldPosition, 1);
                }
            }
            base.OnPointerClick(eventData);
        }
    }

    static class MarkerTypeHelper
    {
        public static MarkerType Type(this Marker marker)
        {
            return (MarkerType)marker.type;
        }
    }
}
