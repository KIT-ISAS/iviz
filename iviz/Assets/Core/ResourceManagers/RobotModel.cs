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

        readonly GameObject baseLinkObject;
        readonly Robot robot;
        readonly CancellationTokenSource runningTs = new();

        Color tint = Color.white;
        float smoothness = 0.5f;
        float metallic = 0.5f;
        bool occlusionOnly;
        bool enableShadows = true;
        bool visible = true;
        int numErrors;
        bool disposed;

        public string Name { get; }
        public string? BaseLink { get; private set; }
        public string Description { get; }
        public IReadOnlyDictionary<string, string> LinkParents => linkParents;

        public GameObject BaseLinkObject =>
            !disposed ? baseLinkObject : throw new ObjectDisposedException("this");

        public IReadOnlyDictionary<string, GameObject> LinkObjects =>
            !disposed ? linkObjects : throw new ObjectDisposedException("this");

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
            ThrowHelper.ThrowIfNullOrEmpty(robotDescription, nameof(robotDescription));

            robot = UrdfFile.CreateFromXml(robotDescription);

            Name = robot.Name;
            Description = robotDescription;
            baseLinkObject = new GameObject(Name);
        }

        public RobotModel Clone()
        {
            return new RobotModel(Description);
        }

        /// <summary>Constructs a robot asynchronously.</summary>
        /// <param name="provider">Object that can call CallService, usually a wrapped RosClient. Null if not available.</param>
        /// <param name="keepMeshMaterials">
        /// For external 3D models, whether to keep the materials instead
        /// of replacing them with the provided colors.
        /// </param>
        /// <param name="loadColliders">Whether collider models should be loaded.</param>
        public async ValueTask StartAsync(IServiceProvider? provider = null, bool keepMeshMaterials = false,
            bool loadColliders = true)
        {
            if (robot.Links.Count == 0 || robot.Joints.Count == 0)
            {
                RosLogger.Info($"Finished constructing empty robot '{Name}' with no links and no joints.");
                return;
            }

            runningTs.Token.ThrowIfCancellationRequested();

            var rootMaterials = new Dictionary<string, Material>();
            foreach (var material in robot.Materials)
            {
                rootMaterials[material.Name] = material;
            }

            foreach (var link in robot.Links)
            {
                var linkObject = new GameObject(link.Name);
                linkObject.transform.SetParentLocal(baseLinkObject.transform);
                linkObjects[link.Name ?? ""] = linkObject;
            }

            foreach (var joint in robot.Joints)
            {
                ProcessJoint(joint);
            }

            var keysWithoutParent = new HashSet<string>(linkObjects.Keys);
            keysWithoutParent.RemoveWhere(linkParents.Keys.Contains);

            if (keysWithoutParent.TryGetFirst(out string? baseLink))
            {
                BaseLink = baseLink;
                linkObjects[BaseLink].transform.SetParent(baseLinkObject.transform, false);
            }

            HideChildlessLinks();

            runningTs.Token.ThrowIfCancellationRequested();
            ApplyAnyValidConfiguration();

            var modelLoadingTasks = robot.Links.SelectMany(
                link => ProcessLinkAsync(keepMeshMaterials, link, rootMaterials, provider, loadColliders,
                    runningTs.Token));

            try
            {
                await modelLoadingTasks.WhenAll();
            }
            catch (OperationCanceledException)
            {
                RosLogger.Warn($"{this}: Robot building canceled.");
                runningTs.Cancel();
                throw;
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Failed to construct '{Name}'", e);
                runningTs.Cancel();
                throw;
            }

            Tint = tint;
            OcclusionOnly = occlusionOnly;
            Visible = visible;
            Smoothness = smoothness;
            Metallic = metallic;
            EnableShadows = enableShadows;

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

            var toHide = new HashSet<string>();

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

        void CancelTasks()
        {
            runningTs.Cancel();
        }

        IEnumerable<Task> ProcessLinkAsync(bool keepMeshMaterials,
            Link link,
            IReadOnlyDictionary<string, Material> rootMaterials,
            IServiceProvider? provider,
            bool loadColliders,
            CancellationToken token)
        {
            if (link.Visuals.Count == 0 && link.Collisions.Count == 0)
            {
                return Enumerable.Empty<Task>();
            }

            token.ThrowIfCancellationRequested();

            var linkObject = linkObjects[link.Name ?? ""];

            var tasks = new List<Task>(link.Collisions.Count + link.Visuals.Count);
            foreach (var visual in link.Visuals)
            {
                var task =
                    ProcessVisualAsync(keepMeshMaterials, visual, linkObject, rootMaterials, provider, token).AsTask();
                tasks.Add(task);
            }

            if (!loadColliders)
            {
                return tasks;
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
            IServiceProvider? provider,
            CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            (string? name, var origin, var geometry) = collision;

            var collisionObject = new GameObject(name != null ? $"[Collision:{name}]" : "[Collision]");
            collisionObject.transform.SetParent(linkObject.transform, false);
            collisionObject.transform.SetLocalPose(origin.ToPose());
            collisionObject.layer = LayerType.Collider;
            collisionObject.tag = ColliderTag;

            if (geometry.Mesh is var (meshUri, meshScale))
            {
                ResourceKey<GameObject>? info;
                try
                {
                    info = await Resource.GetGameObjectResourceAsync(meshUri, provider, token);
                }
                catch (Exception e) when (e is not OperationCanceledException)
                {
                    RosLogger.Error($"{this}: Failed to retrieve '{meshUri}'", e);
                    Object.Destroy(collisionObject);
                    numErrors++;
                    return;
                }

                if (info == null)
                {
                    RosLogger.Error($"{this}: Failed to retrieve collision '{meshUri}'.");
                    Object.Destroy(collisionObject);
                    numErrors++;
                    return;
                }

                var resourceObject = info.Object;
                //foreach (var meshFilter in resourceObject.GetComponentsInChildren<MeshFilter>())
                foreach (var meshFilter in resourceObject.GetAllChildren().WithComponent<MeshFilter>())
                {
                    var child = new GameObject("[Collider]");
                    child.transform.SetParentLocal(collisionObject.transform);
                    child.transform.SetLocalPose(meshFilter.transform.AsPose());
                    child.transform.localScale = meshFilter.transform.lossyScale;
                    child.layer = LayerType.Collider;

                    var collider = child.TryAddComponent<MeshCollider>();
                    collider.sharedMesh = meshFilter.sharedMesh; // note: may modify
                    collider.convex = true;
                }

                collisionObject.transform.localScale = meshScale.ToVector3().Abs();
            }
            else if (geometry.Cylinder is var (cylinderRadius, cylinderLength))
            {
                var resourceObject = Resource.Displays.Cylinder.Object;
                var collider = collisionObject.TryAddComponent<MeshCollider>();
                collider.sharedMesh = resourceObject.GetComponent<MeshFilter>().sharedMesh;
                collider.convex = true;

                collisionObject.transform.localScale = new Vector3(
                    cylinderRadius,
                    cylinderLength,
                    cylinderRadius);
            }
            else if (geometry.Box is { } box)
            {
                var collider = collisionObject.TryAddComponent<BoxCollider>();
                collider.size = box.Size.ToVector3().Abs();
            }
            else if (geometry.Sphere is { } sphere)
            {
                var collider = collisionObject.TryAddComponent<SphereCollider>();
                collider.radius = sphere.Radius;
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
            IServiceProvider? provider,
            CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var (name, origin, geometry, material) = visual;
            var (box, cylinder, sphere, mesh) = geometry;

            var visualObject = new GameObject(name != null ? $"[Visual:{name}]" : "[Visual]");
            visualObject.transform.SetParent(linkObject.transform, false);
            visualObject.transform.SetLocalPose(origin.ToPose());

            GameObject resourceObject;
            if (mesh is var (meshUri, meshScale))
            {
                ResourceKey<GameObject>? info;
                try
                {
                    info = await Resource.GetGameObjectResourceAsync(meshUri, provider, token);
                }
                catch (Exception e) when (e is not OperationCanceledException)
                {
                    RosLogger.Error($"{this}: Failed to retrieve '{meshUri}'", e);
                    numErrors++;
                    return;
                }

                if (info == null)
                {
                    RosLogger.Error($"{this}: Failed to retrieve visual '{meshUri}'.");
                    numErrors++;
                    return;
                }

                Rent(info, meshScale.ToVector3().Abs());
            }
            else if (cylinder is var (cylinderRadius, cylinderLength))
            {
                Rent(Resource.Displays.Cylinder,
                    new Vector3(cylinderRadius * 2, cylinderLength, cylinderRadius * 2).Abs());
            }
            else if (box != null)
            {
                Rent(Resource.Displays.Cube, box.Size.ToVector3().Abs());
            }
            else if (sphere != null)
            {
                Rent(Resource.Displays.Sphere, Mathf.Abs(sphere.Radius) * Vector3.one);
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

                //var renderers = resourceObject.GetComponentsInChildren<MeshRenderer>();
                var meshMarkerDisplays = resourceObject.GetAllChildren().WithComponent<MeshMarkerDisplay>();
                foreach (var display in meshMarkerDisplays)
                {
                    originalColors[display] = display.Color;
                    display.Color *= color;
                    //display.Color = (display.Color + color).Clamp(); // how are colors blended??
                    displays.Add(display);
                }
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
                    $"Node '{parent.name}' is descendant of '{child.name}' and cannot be set as its parent!");
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
                linkObject.transform.SetParentLocal(null);
            }

            foreach (var (linkObject, parentObject) in linkParentObjects)
            {
                var mTransform = linkObject.transform;
                mTransform.SetParentLocal(parentObject.transform);
                mTransform.localPosition = Vector3.zero;
                mTransform.localRotation = Quaternion.identity;
            }

            if (BaseLink != null)
            {
                linkObjects[BaseLink].transform.SetParentLocal(baseLinkObject.transform);
            }
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            CancelTasks();

            ResetLinkParents();

            Visible = true;
            Tint = Color.white;
            OcclusionOnly = false;

            foreach (var (display, color) in originalColors)
            {
                display.Color = color;
            }

            foreach (var (gameObject, resourceKey) in objectResources)
            {
                if (gameObject.TryGetComponent(out IDisplay display))
                {
                    display.Suspend();
                }

                ResourcePool.Return(resourceKey, gameObject);
            }

            Object.Destroy(baseLinkObject);
        }

        public void ApplyAnyValidConfiguration()
        {
            foreach (var (key, joint) in joints)
            {
                if (joint.Type is not (Joint.JointType.Revolute or Joint.JointType.Prismatic))
                {
                    continue;
                }

                var (lower, upper, _) = joint.Limit;
                if (lower > 0)
                {
                    TryWriteJoint(key, lower);
                }
                else if (upper < 0)
                {
                    TryWriteJoint(key, upper);
                }
            }
        }

        public bool TryWriteJoint(string jointName, float value)
        {
            ThrowHelper.ThrowIfNull(jointName, nameof(jointName));

            if (!joints.TryGetValue(jointName, out var joint))
            {
                return false;
            }

            var axis = joint.Axis.Xyz.ToVector3();
            var unityPose = joint.Type switch
            {
                Joint.JointType.Revolute or Joint.JointType.Continuous =>
                    Pose.identity.WithRotation(Quaternion.AngleAxis(-value * Mathf.Rad2Deg, axis)),
                Joint.JointType.Prismatic =>
                    Pose.identity.WithPosition(axis * value),
                _ => (Pose?)null
            };

            if (unityPose is not { } validatedPose)
            {
                return false;
            }

            var jointObject = jointObjects[jointName];
            jointObject.transform.SetLocalPose(validatedPose);
            return true;
        }

        public override string ToString() => $"[{nameof(RobotModel)} '{Name}']";
    }
}