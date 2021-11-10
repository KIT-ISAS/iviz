#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.Urdf;
using Iviz.Tools;
using UnityEngine;
using Color = UnityEngine.Color;
using Joint = Iviz.Urdf.Joint;
using Logger = Iviz.Core.Logger;
using Material = Iviz.Urdf.Material;
using Object = UnityEngine.Object;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Displays
{
    /// <summary>
    /// Wrapper around the 3D models of a robot.
    /// </summary>
    public sealed class RobotModel : ISupportsTint, ISupportsPbr, ISupportsAROcclusion
    {
        readonly List<MeshMarkerResource> displays = new();
        readonly Dictionary<string, GameObject> jointObjects = new();
        readonly Dictionary<string, Joint> joints = new();
        readonly Dictionary<string, GameObject> linkObjects = new();
        readonly Dictionary<GameObject, GameObject> linkParentObjects = new();
        readonly Dictionary<string, string> linkParents = new();
        readonly List<(GameObject, Info<GameObject>)> objectResources = new();
        readonly Dictionary<MeshMarkerResource, Color> originalColors = new();

        readonly Robot robot;
        readonly CancellationTokenSource runningTs = new();

        bool occlusionOnly;
        bool visible = true;
        Color tint = Color.white;
        float smoothness = 0.5f;
        float metallic = 0.5f;
        int numErrors;

        public string Name { get; }
        public string? BaseLink { get; private set; }
        public GameObject BaseLinkObject { get; }

        public string Description { get; }
        public ReadOnlyDictionary<string, string> LinkParents { get; private set; }
        public ReadOnlyDictionary<string, GameObject> LinkObjects { get; private set; }
        public ReadOnlyDictionary<string, Joint> Joints { get; private set; }

        public string UnityName
        {
            get => BaseLinkObject.name;
            set => BaseLinkObject.name = value;
        }

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

        public float Smoothness
        {
            get => smoothness;
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
            get => metallic;
            set
            {
                metallic = value;
                foreach (var display in displays)
                {
                    display.Metallic = value;
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

            LinkParents = new Dictionary<string, string>().AsReadOnly();
            LinkObjects = new Dictionary<string, GameObject>().AsReadOnly();
            Joints = new Dictionary<string, Joint>().AsReadOnly();
            BaseLinkObject = new GameObject(Name);
        }

        /// <summary>Constructs a robot asynchronously.</summary>
        /// <param name="provider">Object that can call CallService, usually a wrapped RosClient. Null if not available.</param>
        /// <param name="keepMeshMaterials">
        /// For external 3D models, whether to keep the materials instead
        /// of replacing them with the provided colors.
        /// </param>
        /// <param name="token">An optional cancellation token.</param>
        public async ValueTask StartAsync(IExternalServiceProvider? provider = null,
            bool keepMeshMaterials = true,
            CancellationToken token = default)
        {
            using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token);
            try
            {
                var rootMaterials = new Dictionary<string, Material>();
                foreach (var material in robot.Materials)
                {
                    rootMaterials[material.Name] = material;
                }

                await robot.Links
                    .Select(link =>
                        ProcessLinkAsync(keepMeshMaterials, link, rootMaterials, provider, tokenSource.Token)
                            .AsTask())
                    .WhenAll();

                foreach (var joint in robot.Joints)
                {
                    ProcessJoint(joint);
                }

                if (linkObjects.Count == 0)
                {
                    Logger.Info($"Finished constructing empty robot '{Name}' with no links and no joints.");
                    return;
                }

                var keysWithoutParent = new HashSet<string>(linkObjects.Keys);
                keysWithoutParent.RemoveWhere(linkParents.Keys.Contains);

                BaseLink = keysWithoutParent.FirstOrDefault();
                if (BaseLink != null)
                {
                    linkObjects[BaseLink].transform.SetParent(BaseLinkObject.transform, false);
                }

                LinkParents = linkParents.AsReadOnly();
                LinkObjects = linkObjects.AsReadOnly();
                Joints = joints.AsReadOnly();

                Tint = tint;
                OcclusionOnly = occlusionOnly;
                Visible = visible;
                Smoothness = smoothness;
                Metallic = metallic;
                ApplyAnyValidConfiguration();

                string errorStr = numErrors == 0 ? "" : $"There were {numErrors.ToString()} errors.";
                Logger.Info($"Finished constructing robot '{Name}' with {LinkObjects.Count.ToString()} " +
                            $"links and {Joints.Count.ToString()} joints. {errorStr}");
            }
            catch (OperationCanceledException)
            {
                Logger.Error($"{this}: Robot building canceled.");
                throw;
            }
            catch (Exception e)
            {
                Logger.Error($"{this}: Failed to construct '{Name}'", e);
                throw;
            }
        }

        public void CancelTasks()
        {
            runningTs.Cancel();
        }


        async ValueTask ProcessLinkAsync(bool keepMeshMaterials,
            Link link,
            IReadOnlyDictionary<string, Material> rootMaterials,
            IExternalServiceProvider? provider,
            CancellationToken token)
        {
            GameObject linkObject = new("Link:" + link.Name);
            linkObject.transform.parent = BaseLinkObject.transform;

            linkObjects[link.Name ?? ""] = linkObject;

            foreach (var collision in link.Collisions)
            {
                await ProcessCollisionAsync(collision, linkObject, provider, token);
            }

            foreach (var visual in link.Visuals)
            {
                await ProcessVisualAsync(keepMeshMaterials, visual, linkObject, rootMaterials, provider, token);
            }
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

            if (geometry.Mesh != null)
            {
                string uri = geometry.Mesh.Filename;
                Info<GameObject>? info;
                try
                {
                    info = await Resource.GetGameObjectResourceAsync(geometry.Mesh.Filename, provider, token);
                }
                catch (Exception e) when (e is not OperationCanceledException)
                {
                    Logger.Error($"{this}: Failed to retrieve '{uri}'", e);
                    numErrors++;
                    return;
                }

                if (info == null)
                {
                    Logger.Error($"{this}: Failed to retrieve '{uri}'");
                    numErrors++;
                    return;
                }


                var resourceObject = info.Object;
                foreach (var meshFilter in resourceObject.GetComponentsInChildren<MeshFilter>())
                {
                    var child = new GameObject();
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

            var visualObject = new GameObject(name != null ? $"[Visual:{name}]" : "[Visual]");
            visualObject.transform.SetParent(linkObject.transform, false);
            visualObject.transform.SetLocalPose(origin.ToPose());


            GameObject resourceObject;
            if (mesh != null)
            {
                string uri = mesh.Filename;
                Info<GameObject>? info;
                try
                {
                    info = await Resource.GetGameObjectResourceAsync(uri, provider, token);
                }
                catch (Exception e) when (!(e is OperationCanceledException))
                {
                    Logger.Error($"{this}: Failed to retrieve '{uri}'", e);
                    numErrors++;
                    return;
                }

                if (info == null)
                {
                    Logger.Error($"{this}: Failed to retrieve '{uri}'");
                    numErrors++;
                    return;
                }

                InternalRent(info, mesh.Scale.ToVector3().Abs());
            }
            else if (cylinder != null)
            {
                (float radius, float length) = cylinder;
                InternalRent(Resource.Displays.Cylinder, new Vector3(radius * 2, length, radius * 2).Abs());
            }
            else if (box != null)
            {
                InternalRent(Resource.Displays.Cube, box.Size.ToVector3().Abs());
            }
            else if (sphere != null)
            {
                InternalRent(Resource.Displays.Sphere, Mathf.Abs(sphere.Radius) * Vector3.one);
            }
            else
            {
                return; //?
            }

            void InternalRent(Info<GameObject> info, in Vector3 scale)
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
                var resources = new List<MeshMarkerResource>();

                foreach (var renderer in renderers)
                {
                    var meshResource = renderer.gameObject.GetComponent<MeshMarkerResource>();
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
                var resource = resourceObject.AddComponent<MeshMarkerResource>();
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

            var originObject = new GameObject($"[JointOrigin:{joint.Name}]");

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

            originObject.transform.SetParent(parent.transform, false);
            jointObject.transform.SetParent(originObject.transform, false);
            child.transform.SetParent(jointObject.transform, false);

            linkParentObjects[child] = jointObject;

            originObject.transform.SetLocalPose(joint.Origin.ToPose());
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

            foreach (var entry in linkParentObjects)
            {
                var mTransform = entry.Key.transform;
                mTransform.parent = entry.Value.transform;
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

            foreach ((var key, Color color) in originalColors)
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

            Pose unityPose;
            switch (joint.Type)
            {
                case Joint.JointType.Revolute:
                case Joint.JointType.Continuous:
                    float angle = value * Mathf.Rad2Deg;
                    unityPose = Pose.identity.WithRotation(Quaternion.AngleAxis(-angle, joint.Axis.Xyz.ToVector3()));
                    break;
                case Joint.JointType.Prismatic:
                    unityPose = Pose.identity.WithPosition(joint.Axis.Xyz.ToVector3() * value);
                    break;
                default:
                    return false;
            }

            var jointObject = jointObjects[jointName];
            jointObject.transform.SetLocalPose(unityPose);

            return true;
        }

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

        public override string ToString()
        {
            return $"[Robot {Name}]";
        }
    }
}