#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.Urdf;
using Iviz.Tools;
using Nito.AsyncEx;
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
    public sealed class RobotModel : ISupportsTint, ISupportsPbr, ISupportsAROcclusion, ISupportsShadows
    {
        public const string ColliderTag = "RobotCollider";

        readonly List<MeshMarkerDisplay> displays = new();
        readonly Dictionary<string, GameObject> jointObjects = new();
        readonly Dictionary<string, Joint> joints = new();
        readonly Dictionary<string, GameObject> linkObjects = new();
        readonly Dictionary<GameObject, GameObject> linkParentObjects = new();
        readonly Dictionary<string, string> linkParents = new();
        readonly List<(GameObject, ResourceKey<GameObject>)> objectResources = new();
        readonly Dictionary<MeshMarkerDisplay, Color> originalColors = new();

        readonly Robot robot;
        readonly CancellationTokenSource runningTs = new();

        Color tint = Color.white;
        float smoothness = 0.5f;
        float metallic = 0.5f;
        bool occlusionOnly;
        bool enableShadows = true;
        bool visible = true;
        int numErrors;

        public string Name { get; }
        public string? BaseLink { get; private set; }
        public GameObject BaseLinkObject { get; }
        public string Description { get; }
        public IReadOnlyDictionary<string, string> LinkParents => linkParents;
        public IReadOnlyDictionary<string, GameObject> LinkObjects => linkObjects;

        public bool OcclusionOnly
        {
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
            set
            {
                visible = value;
                foreach (var display in displays)
                {
                    display.Visible = value;
                }
            }
        }

        public float Smoothness
        {
            set
            {
                smoothness = value;
                foreach (var display in displays)
                {
                    display.Smoothness = value;
                }
            }
        }

        public float Metallic
        {
            set
            {
                metallic = value;
                foreach (var display in displays)
                {
                    display.Metallic = value;
                }
            }
        }
        
        public bool EnableShadows
        {
            set
            {
                enableShadows = value;
                foreach (var display in displays)
                {
                    display.EnableShadows = value;
                }
            }
        }


        /// <summary>
        /// Initializes a robot with the given URDF text. Call <see cref="StartAsync"/> to construct it.
        /// </summary>
        /// <param name="robotDescription">URDF text.</param>
        public RobotModel(string robotDescription)
        {
            if (string.IsNullOrEmpty(robotDescription))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotDescription));
            }

            robot = UrdfFile.CreateFromXml(robotDescription);

            Name = robot.Name;
            Description = robotDescription;
            BaseLinkObject = new GameObject(Name);
        }

        /// <summary>Constructs a robot asynchronously.</summary>
        /// <param name="provider">Object that can call CallService, usually a wrapped RosClient. Null if not available.</param>
        /// <param name="keepMeshMaterials">
        /// For external 3D models, whether to keep the materials instead
        /// of replacing them with the provided colors.
        /// </param>
        public async ValueTask StartAsync(IExternalServiceProvider? provider, bool keepMeshMaterials = false)
        {
            if (robot.Links.Count == 0 || robot.Joints.Count == 0)
            {
                RosLogger.Info($"Finished constructing empty robot '{Name}' with no links and no joints.");
                return;
            }

            var rootMaterials = new Dictionary<string, Material>();
            foreach (var material in robot.Materials)
            {
                rootMaterials[material.Name] = material;
            }

            foreach (var link in robot.Links)
            {
                var linkObject = new GameObject("Link:" + link.Name);
                linkObject.transform.parent = BaseLinkObject.transform;
                linkObjects[link.Name ?? ""] = linkObject;
            }

            foreach (var joint in robot.Joints)
            {
                ProcessJoint(joint);
            }

            try
            {
                var modelLoadingTasks = robot.Links.SelectMany(
                    link => ProcessLinkAsync(keepMeshMaterials, link, rootMaterials, provider, runningTs.Token));
                await modelLoadingTasks.WhenAll();
            }
            catch (OperationCanceledException)
            {
                RosLogger.Warn($"{this}: Robot building canceled.");
                throw;
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Failed to construct '{Name}'", e);
                throw;
            }

            var keysWithoutParent = new HashSet<string>(linkObjects.Keys);
            keysWithoutParent.RemoveWhere(linkParents.Keys.Contains);

            BaseLink = keysWithoutParent.FirstOrDefault();
            if (BaseLink != null)
            {
                linkObjects[BaseLink].transform.SetParent(BaseLinkObject.transform, false);
            }

            HideChildlessLinks();

            Tint = tint;
            OcclusionOnly = occlusionOnly;
            Visible = visible;
            Smoothness = smoothness;
            Metallic = metallic;
            EnableShadows = enableShadows; 
            ApplyAnyValidConfiguration();

            string errorStr = numErrors == 0 ? "" : $"There were {numErrors.ToString()} errors.";
            RosLogger.Info($"Finished constructing robot '{Name}' with {linkObjects.Count.ToString()} " +
                           $"links and {joints.Count.ToString()} joints. {errorStr}");
        }

        void HideChildlessLinks()
        {
            var links = new Dictionary<string, Link>();
            var linkChildren = new Dictionary<string, HashSet<string>>();

            foreach (var link in robot.Links)
            {
                links[link.Name ?? ""] = link;
            }

            foreach (string child in linkObjects.Keys)
            {
                linkChildren[child] = new HashSet<string>();
            }

            foreach (var (child, parent) in linkParents)
            {
                linkChildren[parent].Add(child);
            }

            HashSet<string> toHide = new HashSet<string>();

            bool hasChanges;
            do
            {
                hasChanges = false;

                foreach (var (linkName, children) in linkChildren)
                {
                    if (children.Count != 0)
                    {
                        continue;
                    }

                    var link = links[linkName];
                    if (link.Collisions.Count == 0 && link.Visuals.Count == 0)
                    {
                        toHide.Add(linkName);
                        hasChanges = true;
                    }
                }

                foreach (string child in toHide)
                {
                    linkChildren.Remove(child);
                    if (linkChildren.TryGetValue(linkParents[child], out var siblings))
                    {
                        siblings.Remove(child);
                    }

                    var gameObject = linkObjects[child];
                    var parent = gameObject.transform.parent.gameObject;
                    parent.SetActive(false);
                }
            } while (hasChanges);
        }

        public void CancelTasks()
        {
            runningTs.Cancel();
        }


        IEnumerable<Task> ProcessLinkAsync(bool keepMeshMaterials,
            Link link,
            IReadOnlyDictionary<string, Material> rootMaterials,
            IExternalServiceProvider? provider,
            CancellationToken token)
        {
            if (link.Visuals.Count == 0 && link.Collisions.Count == 0)
            {
                return Enumerable.Empty<Task>();
            }

            var linkObject = linkObjects[link.Name ?? ""];

            var tasks = new List<Task>(link.Collisions.Count + link.Visuals.Count);
            foreach (var visual in link.Visuals)
            {
                var task =
                    ProcessVisualAsync(keepMeshMaterials, visual, linkObject, rootMaterials, provider, token).AsTask();
                tasks.Add(task);
            }

            foreach (var collision in link.Collisions)
            {
                tasks.Add(ProcessCollisionAsync(collision, linkObject, provider, token).AsTask());
            }

            return tasks;
        }

        async ValueTask ProcessCollisionAsync(
            Urdf.Collision collision,
            GameObject linkObject,
            IExternalServiceProvider? provider,
            CancellationToken token)
        {
            (string? name, var origin, var geometry) = collision;

            var collisionObject = new GameObject(name != null ? $"[Collision:{name}]" : "[Collision]");
            collisionObject.transform.SetParent(linkObject.transform, false);
            collisionObject.transform.SetLocalPose(origin.ToPose());
            collisionObject.layer = LayerType.Collider;
            collisionObject.tag = ColliderTag;

            if (geometry.Mesh != null)
            {
                string uri = geometry.Mesh.Filename;
                ResourceKey<GameObject>? info;
                try
                {
                    info = await Resource.GetGameObjectResourceAsync(geometry.Mesh.Filename, provider, token);
                }
                catch (Exception e) when (e is not OperationCanceledException)
                {
                    RosLogger.Error($"{this}: Failed to retrieve '{uri}'", e);
                    Object.Destroy(collisionObject);
                    numErrors++;
                    return;
                }

                if (info == null)
                {
                    RosLogger.Error($"{this}: Failed to retrieve '{uri}'");
                    Object.Destroy(collisionObject);
                    numErrors++;
                    return;
                }

                var resourceObject = info.Object;
                foreach (var meshFilter in resourceObject.GetComponentsInChildren<MeshFilter>())
                {
                    var child = new GameObject("[Collider]");
                    child.transform.parent = collisionObject.transform;
                    child.transform.SetLocalPose(meshFilter.transform.AsPose());
                    child.transform.localScale = meshFilter.transform.lossyScale;
                    child.layer = LayerType.Collider;

                    var collider = child.EnsureComponent<MeshCollider>();
                    collider.sharedMesh = meshFilter.sharedMesh;
                    collider.convex = true;
                }

                collisionObject.transform.localScale = geometry.Mesh.Scale.ToVector3().Abs();
            }
            else if (geometry.Cylinder != null)
            {
                var resourceObject = Resource.Displays.Cylinder.Object;
                var collider = collisionObject.EnsureComponent<MeshCollider>();
                collider.sharedMesh = resourceObject.GetComponent<MeshFilter>().sharedMesh;
                collider.convex = true;

                collisionObject.transform.localScale = new Vector3(
                    geometry.Cylinder.Radius * 2,
                    geometry.Cylinder.Length,
                    geometry.Cylinder.Radius * 2);
            }
            else if (geometry.Box != null)
            {
                var collider = collisionObject.EnsureComponent<BoxCollider>();
                collider.size = geometry.Box.Size.ToVector3().Abs();
            }
            else if (geometry.Sphere != null)
            {
                var collider = collisionObject.EnsureComponent<SphereCollider>();
                collider.radius = geometry.Sphere.Radius;
            }
            else
            {
                Object.Destroy(collisionObject);
            }
        }

        async ValueTask ProcessVisualAsync(bool keepMeshMaterials,
            Visual visual,
            GameObject linkObject,
            IReadOnlyDictionary<string, Material> rootMaterials,
            IExternalServiceProvider? provider,
            CancellationToken token)
        {
            var (name, origin, geometry, material) = visual;
            var (box, cylinder, sphere, mesh) = geometry;

            GameObject visualObject;
            if (origin == Origin.Identity)
            {
                visualObject = linkObject;
            }
            else
            {
                visualObject = new GameObject(name != null ? $"[Visual:{name}]" : "[Visual]");
                visualObject.transform.SetParent(linkObject.transform, false);
                visualObject.transform.SetLocalPose(origin.ToPose());
            }

            GameObject resourceObject;
            if (mesh != null)
            {
                string uri = mesh.Filename;
                ResourceKey<GameObject>? info;
                try
                {
                    info = await Resource.GetGameObjectResourceAsync(uri, provider, token);
                }
                catch (Exception e) when (e is not OperationCanceledException)
                {
                    RosLogger.Error($"{this}: Failed to retrieve '{uri}'", e);
                    numErrors++;
                    return;
                }

                if (info == null)
                {
                    RosLogger.Error($"{this}: Failed to retrieve '{uri}'");
                    numErrors++;
                    return;
                }

                Rent(info, mesh.Scale.ToVector3().Abs());
            }
            else if (cylinder != null)
            {
                (float radius, float length) = cylinder;
                Rent(Resource.Displays.Cylinder, new Vector3(radius * 2, length, radius * 2).Abs());
            }
            else if (box != null)
            {
                Rent(Resource.Displays.Cube, box.Size.ToVector3().Abs());
            }
            else if (sphere != null)
            {
                Rent(Resource.Displays.Sphere, Math.Abs(sphere.Radius) * Vector3.one);
            }
            else
            {
                return; //?
            }

            void Rent(ResourceKey<GameObject> info, in Vector3 scale)
            {
                resourceObject = ResourcePool.Rent(info);
                resourceObject.layer = LayerType.IgnoreRaycast;
                objectResources.Add((resourceObject, info));
                resourceObject.transform.SetParent(visualObject.transform, false);
                visualObject.transform.localScale = scale;
            }

            if (mesh != null)
            {
                var resolvedMaterial = GetMaterialForVisual(material, keepMeshMaterials ? null : rootMaterials);
                var color = resolvedMaterial?.Color?.ToColor() ?? Color.white;

                var renderers = resourceObject.GetComponentsInChildren<MeshRenderer>();
                var resources = new List<MeshMarkerDisplay>();

                foreach (var renderer in renderers)
                {
                    var meshResource = renderer.gameObject.GetComponent<MeshMarkerDisplay>();
                    if (meshResource == null)
                    {
                        continue;
                    }

                    originalColors[meshResource] = meshResource.Color;
                    meshResource.Color *= color;
                    resources.Add(meshResource);
                }

                displays.AddRange(resources);
            }
            else
            {
                var resource = resourceObject.AddComponent<MeshMarkerDisplay>();
                displays.Add(resource);

                var resolvedMaterial = GetMaterialForVisual(material, rootMaterials);
                if (resolvedMaterial == null)
                {
                    return;
                }

                if (resolvedMaterial.Color != null)
                {
                    resource.Color = resolvedMaterial.Color.ToColor();
                }

                if (resolvedMaterial.Texture != null)
                {
                    // TODO!!
                }
            }
        }

        void ProcessJoint(Joint joint)
        {
            var jointObject = new GameObject("{Joint:" + joint.Name + "}");
            jointObjects[joint.Name] = jointObject;

            //var originObject = new GameObject($"[JointOrigin:{joint.Name}]");

            if (!linkObjects.TryGetValue(joint.Parent.Link, out var parent))
            {
                throw new MalformedUrdfException($"Cannot find link '{joint.Parent.Link}'");
            }

            if (!linkObjects.TryGetValue(joint.Child.Link, out var child))
            {
                throw new MalformedUrdfException($"Cannot find link '{joint.Child.Link}'");
            }

            if (parent.transform.IsChildOf(child.transform))
            {
                Dispose();
                throw new MalformedUrdfException(
                    $"Node '{parent.name}' is child of '{child.name}' and cannot be set as its parent!");
            }

            linkParents[joint.Child.Link] = joint.Parent.Link;
            joints[joint.Name] = joint;

            //originObject.transform.SetParent(parent.transform, false);
            //jointObject.transform.SetParent(originObject.transform, false);
            jointObject.transform.SetParent(parent.transform, false);
            child.transform.SetParent(jointObject.transform, false);

            linkParentObjects[child] = jointObject;

            //originObject.transform.SetLocalPose(joint.Origin.ToPose());
            jointObject.transform.SetLocalPose(joint.Origin.ToPose());
        }

        static Material? GetMaterialForVisual(Material? material, IReadOnlyDictionary<string, Material>? rootMaterials)
        {
            return material != null &&
                   material.IsReference() &&
                   rootMaterials != null &&
                   rootMaterials.TryGetValue(material.Name, out var newMaterial)
                ? newMaterial
                : material;
        }

        public void ResetLinkParents()
        {
            foreach (var linkObject in linkParentObjects.Keys)
            {
                linkObject.transform.parent = null;
            }

            foreach (var (linkObject, parentObject) in linkParentObjects)
            {
                var mTransform = linkObject.transform;
                mTransform.parent = parentObject.transform;
                mTransform.localPosition = Vector3.zero;
                mTransform.localRotation = Quaternion.identity;
            }

            if (BaseLink != null)
            {
                linkObjects[BaseLink].transform.SetParent(BaseLinkObject.transform, false);
            }
        }

        public void Dispose()
        {
            CancelTasks();

            ResetLinkParents();

            Visible = true;
            Tint = Color.white;
            OcclusionOnly = false;

            foreach (var (key, color) in originalColors)
            {
                key.Color = color;
            }

            foreach (var (gameObject, info) in objectResources)
            {
                var display = gameObject.GetComponent<IDisplay>();
                display?.Suspend();
                ResourcePool.Return(info, gameObject);
            }

            Object.Destroy(BaseLinkObject);
        }

        public void ApplyAnyValidConfiguration()
        {
            foreach (var (key, joint) in joints)
            {
                if (joint.Limit.Lower > 0)
                {
                    TryWriteJoint(key, joint.Limit.Lower);
                }
                else if (joint.Limit.Upper < 0)
                {
                    TryWriteJoint(key, joint.Limit.Upper);
                }
            }
        }

        public bool TryWriteJoint(string jointName, float value)
        {
            if (jointName == null)
            {
                throw new ArgumentNullException(nameof(jointName));
            }

            if (!joints.TryGetValue(jointName, out var joint))
            {
                return false;
            }

            var axis = joint.Axis.Xyz.ToVector3();
            Pose? unityPose = joint.Type switch
            {
                Joint.JointType.Revolute or Joint.JointType.Continuous => 
                    Pose.identity.WithRotation(Quaternion.AngleAxis(-value * Mathf.Rad2Deg, axis)),
                Joint.JointType.Prismatic => 
                    Pose.identity.WithPosition(axis * value),
                _ => null
            };

            if (unityPose is not { } validatedPose)
            {
                return false;
            }
            
            var jointObject = jointObjects[jointName];
            jointObject.transform.SetLocalPose(validatedPose);
            return true;
        }

        /*
        public void WriteJoints(JointState state)
        {
            WriteJoints(state.Name.Zip(state.Position));
        }

        public void WriteJoints(IReadOnlyList<string> names, double[] positions)
        {
            WriteJoints(names.Zip(positions));
        }

        public void WriteJoints(IEnumerable<(string name, double position)> jointPositions)
        {
            foreach ((string name, double position) in jointPositions)
            {
                TryWriteJoint(name, (float)position);
            }
        }
        */

        public override string ToString()
        {
            return $"[Robot {Name}]";
        }
    }
}