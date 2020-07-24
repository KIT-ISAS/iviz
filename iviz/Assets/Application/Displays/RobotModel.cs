using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Iviz.Resources;
using Iviz.Urdf;
using UnityEngine;
using Color = UnityEngine.Color;
using Joint = Iviz.Urdf.Joint;
using Material = Iviz.Urdf.Material;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Displays
{
    public class RobotModel
    {
        public string Name { get; }
        public string BaseLink { get; }
        public GameObject BaseLinkObject { get; }

        readonly Dictionary<string, GameObject> linkObjects = new Dictionary<string, GameObject>();
        readonly Dictionary<string, GameObject> jointObjects = new Dictionary<string, GameObject>();
        readonly Dictionary<string, string> linkParents = new Dictionary<string, string>();
        readonly Dictionary<GameObject, GameObject> linkParentObjects = new Dictionary<GameObject, GameObject>();
        readonly Dictionary<string, Joint> joints = new Dictionary<string, Joint>();
        readonly List<(GameObject, Resource.Info<GameObject>)> objectResources = new List<(GameObject, Resource.Info<GameObject>)>() ;

        readonly List<ISupportsTintAndAROcclusion> displays = new List<ISupportsTintAndAROcclusion>();

        public ReadOnlyDictionary<string, string> LinkParents { get; }
        public ReadOnlyDictionary<string, GameObject> LinkObjects { get; }
        public ReadOnlyDictionary<GameObject, GameObject> LinkParentObjects { get; }

        
        bool occlusionOnly;
        public bool OcclusionOnly
        {
            get => occlusionOnly;
            set
            {
                occlusionOnly = value;
                foreach (var display in displays)
                {
                    display.OcclusionOnly = value;
                }
            }
        }

        Color tint = Color.white;
        public Color Tint
        {
            get => tint;
            set
            {
                tint = value;
                foreach (var display in displays)
                {
                    display.Tint = value;
                }
            }
        }

        bool visible = true;
        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                foreach (var display in displays)
                {
                    display.Visible = value;
                }
            }
        }

        public RobotModel(string robotDescription, bool keepMeshMaterials = true)
        {
            var robot = Robot.Create(robotDescription);

            Name = robot.Name;

            Dictionary<string, Material> rootMaterials = new Dictionary<string, Material>();
            foreach (var material in robot.Materials)
            {
                if (material.Name is null)
                {
                    continue;
                }

                rootMaterials[material.Name] = material;
            }


            foreach (var link in robot.Links)
            {
                ProcessLink(keepMeshMaterials, link, rootMaterials);
            }

            foreach (var joint in robot.Joints)
            {
                ProcessJoint(joint);
            }

            if (linkObjects.Count == 0)
            {
                return;
            }

            var unparentedKeys = new HashSet<string>(linkObjects.Keys);
            unparentedKeys.RemoveWhere(linkParents.Keys.Contains);

            BaseLink = unparentedKeys.FirstOrDefault();
            BaseLinkObject = BaseLink is null ? null : linkObjects[BaseLink];
            
            LinkParents = new ReadOnlyDictionary<string, string>(linkParents);
            LinkObjects = new ReadOnlyDictionary<string, GameObject>(linkObjects);
            LinkParentObjects = new ReadOnlyDictionary<GameObject, GameObject>(linkParentObjects);
        }

        void ProcessLink(bool keepMeshMaterials, Link link, IReadOnlyDictionary<string, Material> rootMaterials)
        {
            GameObject linkObject = new GameObject("Link:" + link.Name);
            linkObjects[link.Name] = linkObject;

            foreach (Visual visual in link.Visuals)
            {
                Geometry geometry = visual.Geometry;

                GameObject visualObject = new GameObject
                (
                    name: visual.Name != null ? $"[Visual:{visual.Name}]" : "[Visual]"
                );
                visualObject.transform.SetParent(linkObject.transform, false);
                visualObject.transform.SetLocalPose(visual.Origin.ToPose());


                GameObject resourceObject = null;
                bool isSynthetic = false;
                if (geometry.Mesh != null)
                {
                    Uri uri = new Uri(geometry.Mesh.Filename);
                    if (!Resource.TryGetResource(uri, out Resource.Info<GameObject> info))
                    {
                        Debug.Log("Robot: Failed to retrieve " + uri);
                        continue;
                    }

                    resourceObject = ResourcePool.GetOrCreate(info);
                    objectResources.Add((resourceObject, info));
                    resourceObject.transform.SetParent(visualObject.transform, false);
                    visualObject.transform.localScale = geometry.Mesh.Scale.ToVector3().Abs();
                    isSynthetic = true;
                }
                else if (geometry.Cylinder != null)
                {
                    resourceObject = ResourcePool.GetOrCreate(Resource.Displays.Cylinder);
                    objectResources.Add((resourceObject, Resource.Displays.Cylinder));
                    resourceObject.transform.SetParent(visualObject.transform, false);
                    visualObject.transform.localScale = new Vector3(
                        geometry.Cylinder.Radius * 2,
                        geometry.Cylinder.Length,
                        geometry.Cylinder.Radius * 2);
                }
                else if (geometry.Box != null)
                {
                    resourceObject = ResourcePool.GetOrCreate(Resource.Displays.Cube);
                    objectResources.Add((resourceObject, Resource.Displays.Cube));
                    resourceObject.transform.SetParent(visualObject.transform, false);
                    visualObject.transform.localScale = geometry.Box.Size.ToVector3().Abs();
                }
                else if (geometry.Sphere != null)
                {
                    resourceObject = ResourcePool.GetOrCreate(Resource.Displays.Sphere);
                    objectResources.Add((resourceObject, Resource.Displays.Sphere));
                    resourceObject.transform.SetParent(visualObject.transform, false);
                    visualObject.transform.localScale = geometry.Sphere.Radius * Vector3.one;
                }

                if (resourceObject is null)
                {
                    continue; //?
                }

                if (!isSynthetic)
                {
                    MeshMarkerResource resource = resourceObject.AddComponent<MeshMarkerResource>();

                    Material material = GetMaterialForVisual(visual, rootMaterials);
                    if (material is null)
                    {
                        continue;
                    }

                    if (material.Color != null)
                    {
                        resource.Color = material.Color.ToColor();
                    }
                    else
                    {
                        // TODO
                    }

                    displays.Add(resource);
                }
                else
                {
                    Material material = GetMaterialForVisual(visual, keepMeshMaterials ? null : rootMaterials);
                    Color color = material?.Color?.ToColor() ?? Color.white;
                    
                    MeshRenderer[] renderers = resourceObject.GetComponentsInChildren<MeshRenderer>();
                    
                    List<ISupportsTintAndAROcclusion> resources = new List<ISupportsTintAndAROcclusion>();

                    foreach (MeshRenderer renderer in renderers)
                    {
                        var trianglesResource = renderer.gameObject.GetComponent<MeshTrianglesResource>();
                        if (!(trianglesResource is null))
                        {
                            trianglesResource.Color = trianglesResource.Color * color;
                            resources.Add(trianglesResource);
                        }
                        else
                        {
                            var wrapperResource = renderer.gameObject.AddComponent<MarkerWrapperResource>();
                            wrapperResource.Color = wrapperResource.Color * color;
                            resources.Add(wrapperResource);
                        }
                    }
                    
                    displays.AddRange(resources);
                }
            }
        }

        void ProcessJoint(Joint joint)
        {
            GameObject jointObject = new GameObject("{Joint:" + joint.Name + "}");
            jointObjects[joint.Name] = jointObject;

            GameObject originObject = new GameObject($"[Origin:{joint.Name}]");

            GameObject parent = linkObjects[joint.Parent.Link];
            GameObject child = linkObjects[joint.Child.Link];

            if (parent.transform.IsChildOf(child.transform))
            {
                Dispose();
                throw new MalformedUrdfException();
            }

            linkParents[joint.Child.Link] = joint.Parent.Link;
            joints[joint.Name] = joint;

            originObject.transform.SetParent(parent.transform, false);
            jointObject.transform.SetParent(originObject.transform, false);
            child.transform.SetParent(jointObject.transform, false);

            linkParentObjects[child] = jointObject;

            originObject.transform.SetLocalPose(joint.Origin.ToPose());
        }

        static Material GetMaterialForVisual(Visual visual, IReadOnlyDictionary<string, Material> rootMaterials)
        {
            Material material = visual.Material;
            if (material != null &&
                visual.Material.IsReference() &&
                rootMaterials != null &&
                rootMaterials.TryGetValue(visual.Material.Name, out Material newMaterial))
            {
                material = newMaterial;
            }

            return material;
        }

        public void ResetLinkParents()
        {
            foreach (GameObject linkObject in linkParentObjects.Keys)
            {
                linkObject.transform.parent = null;
            }
            foreach (var entry in linkParentObjects)
            {
                Transform mTransform = entry.Key.transform;
                mTransform.parent = entry.Value.transform;
                mTransform.localPosition = Vector3.zero;
                mTransform.localRotation = Quaternion.identity;
            }
        }

        public void Dispose()
        {
            ResetLinkParents();

            foreach (var(gameObject, info) in objectResources)
            {
                IDisplay display = gameObject.GetComponent<IDisplay>();
                display?.Stop();
                ResourcePool.Dispose(info, gameObject);
            }
            UnityEngine.Object.Destroy(BaseLinkObject);
        }

        public void ApplyAnyValidConfiguration()
        {
            foreach (var joint in joints)
            {
                if (joint.Value.Limit is null)
                {
                    continue;
                }
                if (joint.Value.Limit.Lower > 0)
                {
                    TryWriteJoint(joint.Key, joint.Value.Limit.Lower, out _);
                }
                else if (joint.Value.Limit.Upper < 0)
                {
                    TryWriteJoint(joint.Key, joint.Value.Limit.Upper, out _);
                }
            }
        }
        
        public bool TryWriteJoint(string jointName, float value, out Pose unityPose, bool onlyCalculatePose = false)
        {
            if (!joints.TryGetValue(jointName, out Joint joint))
            {
                unityPose = default;
                return false;
            }

            switch (joint.Type)
            {
                case Joint.JointType.Revolute:
                case Joint.JointType.Continuous:
                    float angle = value * Mathf.Rad2Deg;
                    unityPose = new Pose(Vector3.zero, Quaternion.AngleAxis(-angle, joint.Axis.Xyz.ToVector3()));
                    break;
                case Joint.JointType.Prismatic:
                    unityPose = new Pose(joint.Axis.Xyz.ToVector3() * value, Quaternion.identity);
                    break;
                default:
                    unityPose = Pose.identity;
                    return false;
            }

            if (onlyCalculatePose)
            {
                return true;
            }
            
            GameObject jointObject = jointObjects[jointName];
            jointObject.transform.SetLocalPose(unityPose);

            return true;
        }
    }

    static class UrdfUtils
    {
        public static Vector3 ToVector3(this Urdf.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z).Ros2Unity();
        }

        static Quaternion ToQuaternion(this Urdf.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z).RosRpy2Unity();
        }

        public static Pose ToPose(this Origin v)
        {
            return new Pose(v.Xyz.ToVector3(), v.Rpy.ToQuaternion());
        }

        public static Color ToColor(this Urdf.Color v)
        {
            return new Color(v.Rgba.R, v.Rgba.G, v.Rgba.B, v.Rgba.A);
        }

        public static bool IsReference(this Material material)
        {
            return material.Color is null && material.Texture is null && !(material.Name is null);
        }
    }
}