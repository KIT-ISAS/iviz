using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.Roslib;
using Iviz.Urdf;
using Iviz.XmlRpc;
using JetBrains.Annotations;
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
        readonly List<MeshMarkerResource> displays = new List<MeshMarkerResource>();
        readonly Dictionary<string, GameObject> jointObjects = new Dictionary<string, GameObject>();
        readonly Dictionary<string, Joint> joints = new Dictionary<string, Joint>();

        readonly Dictionary<string, GameObject> linkObjects = new Dictionary<string, GameObject>();
        readonly Dictionary<GameObject, GameObject> linkParentObjects = new Dictionary<GameObject, GameObject>();
        readonly Dictionary<string, string> linkParents = new Dictionary<string, string>();

        readonly List<(GameObject, Info<GameObject>)> objectResources =
            new List<(GameObject, Info<GameObject>)>();

        readonly Robot robot;
        readonly CancellationTokenSource runningTs = new CancellationTokenSource();

        bool occlusionOnly;
        bool visible = true;
        Color tint = Color.white;
        float smoothness = 0.5f;
        float metallic = 0.5f;

        /// <summary>
        /// Initializes a robot with the given URDF text. Call <see cref="StartAsync"/> to construct it.
        /// </summary>
        /// <param name="robotDescription">URDF text.</param>
        public RobotModel([NotNull] string robotDescription)
        {
            if (string.IsNullOrEmpty(robotDescription))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(robotDescription));
            }

            robot = UrdfFile.CreateFromXml(robotDescription);

            Name = robot.Name;
            Description = robotDescription;

            LinkParents = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
            LinkObjects = new ReadOnlyDictionary<string, GameObject>(new Dictionary<string, GameObject>());
            Joints = new ReadOnlyDictionary<string, Joint>(new Dictionary<string, Joint>());
            BaseLinkObject = new GameObject(Name);
        }

        /// <summary>Constructs a robot asynchronously.</summary>
        /// <param name="provider">Object that can call CallService, usually a wrapped RosClient. Null if not available.</param>
        /// <param name="keepMeshMaterials">
        /// For external 3D models, whether to keep the materials instead
        /// of replacing them with the provided colors.
        /// </param>
        /// <param name="token">An optional cancellation token.</param>
        public async Task StartAsync([CanBeNull] IExternalServiceProvider provider = null, bool keepMeshMaterials = true,
            CancellationToken token = default)
        {
            IsStarting = true;
            CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token);

            try
            {
                var rootMaterials = new Dictionary<string, Material>();
                foreach (var material in robot.Materials)
                {
                    rootMaterials[material.Name] = material;
                }

                await robot.Links.Select(link =>
                    ProcessLinkAsync(keepMeshMaterials, link, rootMaterials, provider, tokenSource.Token)).WhenAll();

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

                LinkParents = new ReadOnlyDictionary<string, string>(linkParents);
                LinkObjects = new ReadOnlyDictionary<string, GameObject>(linkObjects);
                Joints = new ReadOnlyDictionary<string, Joint>(joints);

                Tint = tint;
                OcclusionOnly = occlusionOnly;
                Visible = visible;
                Smoothness = smoothness;
                Metallic = metallic;
                ApplyAnyValidConfiguration();

                string errorStr = NumErrors == 0 ? "" : $"There were {NumErrors.ToString()} errors.";
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
            finally
            {
                IsStarting = false;
                tokenSource.Dispose();
            }
        }

        public void Cancel()
        {
            runningTs.Cancel();
        }

        public string Name { get; }
        public string BaseLink { get; private set; }
        public GameObject BaseLinkObject { get; }

        [NotNull]
        public string UnityName
        {
            get => BaseLinkObject.name;
            set => BaseLinkObject.name = value;
        }
        public string Description { get; }

        public ReadOnlyDictionary<string, string> LinkParents { get; private set; }
        public ReadOnlyDictionary<string, GameObject> LinkObjects { get; private set; }
        public ReadOnlyDictionary<string, Joint> Joints { get; private set; }

        public bool IsStarting { get; private set; }
        public int NumErrors { get; private set; }

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

        async Task ProcessLinkAsync(bool keepMeshMaterials,
            [NotNull] Link link,
            [NotNull] IReadOnlyDictionary<string, Material> rootMaterials,
            [CanBeNull] IExternalServiceProvider provider,
            CancellationToken token)
        {
            var linkObject = new GameObject("Link:" + link.Name);
            linkObject.transform.parent = BaseLinkObject.transform;

            linkObjects[link.Name ?? ""] = linkObject;

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
                bool isSynthetic = false;
                if (geometry.Mesh != null)
                {
                    string uri = geometry.Mesh.Filename;
                    Info<GameObject> info;
                    try
                    {
                        info = await Resource.GetGameObjectResourceAsync(geometry.Mesh.Filename, provider, token);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"{this}: Failed to retrieve '{uri}'", e);
                        throw;
                    }

                    if (info == null)
                    {
                        Logger.Error($"{this}: Failed to retrieve '{uri}'");
                        NumErrors++;
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
                    displays.Add(resource);

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
                }
                else
                {
                    var material = GetMaterialForVisual(visual, keepMeshMaterials ? null : rootMaterials);
                    var color = material?.Color?.ToColor() ?? Color.white;

                    var renderers = resourceObject.GetComponentsInChildren<MeshRenderer>();
                    var resources = new List<MeshMarkerResource>();

                    foreach (var renderer in renderers)
                    {
                        var meshResource = renderer.gameObject.GetComponent<MeshMarkerResource>();
                        if (meshResource == null)
                        {
                            continue;
                        }

                        meshResource.Color *= color;
                        resources.Add(meshResource);
                    }

                    displays.AddRange(resources);
                }
            }
        }

        void ProcessJoint([NotNull] Joint joint)
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

        [CanBeNull]
        static Material GetMaterialForVisual([NotNull] Visual visual,
            [CanBeNull] IReadOnlyDictionary<string, Material> rootMaterials)
        {
            var material = visual.Material;

            if (material != null &&
                visual.Material != null &&
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

            if (BaseLink != null)
            {
                linkObjects[BaseLink].transform.SetParent(BaseLinkObject.transform, false);
            }
        }

        public void Dispose()
        {
            Cancel();

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
                if (joint.Value.Limit.Lower > 0)
                {
                    TryWriteJoint(joint.Key, joint.Value.Limit.Lower);
                }
                else if (joint.Value.Limit.Upper < 0)
                {
                    TryWriteJoint(joint.Key, joint.Value.Limit.Upper);
                }
            }
        }

        public bool TryWriteJoint([NotNull] string jointName, float value)
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

        public void WriteJoints([NotNull] JointState state)
        {
            WriteJoints(state.Name.Zip(state.Position));
        }

        public void WriteJoints([NotNull] IReadOnlyList<string> names, [NotNull] double[] positions)
        {
            WriteJoints(names.Zip(positions));
        }

        public void WriteJoints([NotNull] IEnumerable<(string name, double position)> jointPositions)
        {
            foreach ((string name, double position) in jointPositions)
            {
                TryWriteJoint(name, (float) position);
            }
        }

        [NotNull]
        public override string ToString()
        {
            return $"[Robot {Name}]";
        }
    }
}