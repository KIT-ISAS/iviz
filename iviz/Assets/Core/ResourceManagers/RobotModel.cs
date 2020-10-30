using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Iviz.Resources;
using Iviz.Urdf;
using JetBrains.Annotations;
using UnityEngine;
using Color = UnityEngine.Color;
using Joint = Iviz.Urdf.Joint;
using Material = Iviz.Urdf.Material;
using Object = UnityEngine.Object;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Displays
{
    /// <summary>
    /// Wrapper around the 3D models of a robot.
    /// </summary>
    public sealed class RobotModel
    {
        readonly List<ISupportsTintAndAROcclusion> displays = new List<ISupportsTintAndAROcclusion>();
        readonly Dictionary<string, GameObject> jointObjects = new Dictionary<string, GameObject>();
        readonly Dictionary<string, Joint> joints = new Dictionary<string, Joint>();

        readonly Dictionary<string, GameObject> linkObjects = new Dictionary<string, GameObject>();
        readonly Dictionary<GameObject, GameObject> linkParentObjects = new Dictionary<GameObject, GameObject>();
        readonly Dictionary<string, string> linkParents = new Dictionary<string, string>();

        readonly List<(GameObject, Info<GameObject>)> objectResources =
            new List<(GameObject, Info<GameObject>)>();

        bool occlusionOnly;
        bool visible = true;
        Color tint = Color.white;

        /// <summary>
        /// Constructs a robot from the given URDF text.
        /// </summary>
        /// <param name="robotDescription">URDF text.</param>
        /// <param name="provider">Object that can call CallService, usually a wrapped RosClient. Null if not available.</param>
        /// <param name="keepMeshMaterials">
        /// For external 3D models, whether to keep the materials instead
        /// of replacing them with the provided colors.
        /// </param>
        public RobotModel([NotNull] string robotDescription, [CanBeNull] IExternalServiceProvider provider, bool keepMeshMaterials = true)
        {
            if (string.IsNullOrEmpty(robotDescription))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotDescription));
            }

            var robot = UrdfFile.CreateFromXml(robotDescription);

            Name = robot.Name;
            Description = robotDescription;

            var rootMaterials = new Dictionary<string, Material>();
            foreach (var material in robot.Materials)
            {
                if (material.Name == null)
                {
                    continue;
                }

                rootMaterials[material.Name] = material;
            }


            foreach (var link in robot.Links)
            {
                ProcessLink(keepMeshMaterials, link, rootMaterials, provider);
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
            BaseLinkObject = BaseLink == null ? null : linkObjects[BaseLink];

            LinkParents = new ReadOnlyDictionary<string, string>(linkParents);
            LinkObjects = new ReadOnlyDictionary<string, GameObject>(linkObjects);
            LinkParentObjects = new ReadOnlyDictionary<GameObject, GameObject>(linkParentObjects);
        }

        public string Name { get; }
        public string BaseLink { get; }
        public GameObject BaseLinkObject { get; }
        public string Description { get; }

        public ReadOnlyDictionary<string, string> LinkParents { get; }
        public ReadOnlyDictionary<string, GameObject> LinkObjects { get; }
        public ReadOnlyDictionary<GameObject, GameObject> LinkParentObjects { get; }

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

        void ProcessLink(bool keepMeshMaterials,
            Link link,
            IReadOnlyDictionary<string, Material> rootMaterials,
            IExternalServiceProvider provider)
        {
            var linkObject = new GameObject("Link:" + link.Name);
            linkObjects[link.Name] = linkObject;

            foreach (var visual in link.Visuals)
            {
                var geometry = visual.Geometry;

                var visualObject = new GameObject
                (
                    visual.Name != null ? $"[Visual:{visual.Name}]" : "[Visual]"
                );
                visualObject.transform.SetParent(linkObject.transform, false);
                visualObject.transform.SetLocalPose(visual.Origin.ToPose());


                GameObject resourceObject = null;
                var isSynthetic = false;
                if (geometry.Mesh != null)
                {
                    var uri = new Uri(geometry.Mesh.Filename);
                    if (!Resource.TryGetResource(uri, out Info<GameObject> info, provider))
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

                if (resourceObject == null)
                {
                    continue; //?
                }

                if (!isSynthetic)
                {
                    var resource = resourceObject.AddComponent<MeshMarkerResource>();

                    var material = GetMaterialForVisual(visual, rootMaterials);
                    if (material == null)
                    {
                        continue;
                    }

                    if (material.Color != null)
                    {
                        resource.Color = material.Color.ToColor();
                    }

                    if (material.Texture != null)
                    {
                        // TODO!!
                    }

                    displays.Add(resource);
                }
                else
                {
                    var material = GetMaterialForVisual(visual, keepMeshMaterials ? null : rootMaterials);
                    var color = material?.Color?.ToColor() ?? Color.white;

                    var renderers = resourceObject.GetComponentsInChildren<MeshRenderer>();
                    var resources = new List<ISupportsTintAndAROcclusion>();

                    foreach (var renderer in renderers)
                    {
                        var trianglesResource = renderer.gameObject.GetComponent<MeshTrianglesResource>();
                        if (trianglesResource == null)
                        {
                            continue;
                        }

                        trianglesResource.Color *= color;
                        resources.Add(trianglesResource);
                    }

                    displays.AddRange(resources);
                }
            }
        }

        void ProcessJoint(Joint joint)
        {
            var jointObject = new GameObject("{Joint:" + joint.Name + "}");
            jointObjects[joint.Name] = jointObject;

            var originObject = new GameObject($"[JointOrigin:{joint.Name}]");

            var parent = linkObjects[joint.Parent.Link];
            var child = linkObjects[joint.Child.Link];

            if (parent.transform.IsChildOf(child.transform))
            {
                Dispose();
                throw new MalformedUrdfException(
                    $"Node '{parent.name}' is child of '{child.name}' and cannot be set as its parent!");
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
            var material = visual.Material;

            if (material != null &&
                visual.Material.IsReference() &&
                rootMaterials != null &&
                rootMaterials.TryGetValue(visual.Material.Name, out var newMaterial))
            {
                material = newMaterial;
            }

            return material;
        }

        public void ResetLinkParents()
        {
            foreach (var linkObject in linkParentObjects.Keys)
            {
                linkObject.transform.parent = null;
            }

            foreach (var entry in linkParentObjects)
            {
                var mTransform = entry.Key.transform;
                mTransform.parent = entry.Value.transform;
                mTransform.localPosition = Vector3.zero;
                mTransform.localRotation = Quaternion.identity;
            }
        }

        public void Dispose()
        {
            ResetLinkParents();

            foreach (var (gameObject, info) in objectResources)
            {
                var display = gameObject.GetComponent<IDisplay>();
                display?.Suspend();
                ResourcePool.Dispose(info, gameObject);
            }

            Object.Destroy(BaseLinkObject);
        }

        public void ApplyAnyValidConfiguration()
        {
            foreach (var joint in joints)
            {
                if (joint.Value.Limit == null)
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

        public bool TryWriteJoint([NotNull] string jointName, float value, out Pose unityPose, bool onlyCalculatePose = false)
        {
            if (jointName == null)
            {
                throw new ArgumentNullException(nameof(jointName));
            }

            if (!joints.TryGetValue(jointName, out var joint))
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

            var jointObject = jointObjects[jointName];
            jointObject.transform.SetLocalPose(unityPose);

            return true;
        }
    }
}