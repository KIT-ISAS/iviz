using UnityEngine;
using RosSharp;
using System.Linq;
using System.Collections.Generic;
using RosSharp.Urdf;
using System.Runtime.Serialization;
using Iviz.RoslibSharp;
using System;
using Iviz.Resources;
using System.Collections.ObjectModel;
using Iviz.Displays;

namespace Iviz.Controllers
{
    class RobotInfo : MonoBehaviour
    {
        public RobotController owner;
    }

    [DataContract]
    public sealed class RobotConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public Resource.Module Module => Resource.Module.Robot;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string RobotName { get; set; } = "";
        [DataMember] public string RobotResource { get; set; } = Resource.Robots.Names[0];
        [DataMember] public string FramePrefix { get; set; } = "";
        [DataMember] public string FrameSuffix { get; set; } = "";
        [DataMember] public bool AttachToTF { get; set; } = false;
        [DataMember] public bool RenderAsOcclusionOnly { get; set; } = false;
        [DataMember] public SerializableColor Tint { get; set; } = Color.white;
    }

    public sealed class RobotController : IController, IHasFrame
    {
        readonly ObjectClickableNode node;
        RobotInfo robotInfo;

        readonly GameObject carrier;
        readonly BoxCollider carrierCollider;

        public TFFrame Frame => node.Parent;

        GameObject RobotObject { get; set; }
        GameObject BaseLink { get; set; }

        public event Action Stopped;

        readonly Dictionary<GameObject, GameObject> originalLinkParents = new Dictionary<GameObject, GameObject>();
        readonly Dictionary<GameObject, Pose> originalLinkPoses = new Dictionary<GameObject, Pose>();
        readonly Dictionary<string, JointInfo> jointWriters = new Dictionary<string, JointInfo>();
        readonly List<MarkerWrapper> displays = new List<MarkerWrapper>();

        public IReadOnlyDictionary<string, JointInfo> JointWriters => new ReadOnlyDictionary<string, JointInfo>(jointWriters);

        readonly RobotConfiguration config = new RobotConfiguration();
        public RobotConfiguration Config
        {
            get => config;
            set
            {
                RobotResource = value.RobotResource;
                AttachToTF = value.AttachToTF;
                FramePrefix = value.FramePrefix;
                FrameSuffix = value.FrameSuffix;
                Visible = value.Visible;
                RenderAsOcclusionOnly = value.RenderAsOcclusionOnly;
                Tint = value.Tint;
            }
        }

        string Name
        {
            get => config.RobotName;
            set => config.RobotName = value;
        }

        public string LongName => (Name == "") ? config.RobotResource : Name;

        public string RobotResource
        {
            get => config.RobotResource;
            set => LoadRobotFromResource(value);
        }

        public string FramePrefix
        {
            get => config.FramePrefix;
            set
            {
                if (AttachToTF)
                {
                    AttachToTF = false;
                    config.FramePrefix = value;
                    AttachToTF = true;
                }
                else
                {
                    config.FramePrefix = value;
                }
                if (!(BaseLink is null))
                {
                    node.AttachTo(Decorate(BaseLink.name));
                }
            }
        }

        public string FrameSuffix
        {
            get => config.FrameSuffix;
            set
            {
                if (AttachToTF)
                {
                    AttachToTF = false;
                    config.FrameSuffix = value;
                    AttachToTF = true;
                }
                else
                {
                    config.FrameSuffix = value;
                }
                if (!(BaseLink is null))
                {
                    node.AttachTo(Decorate(BaseLink.name));
                }
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                displays.ForEach(x => x.Visible = value);
                RobotObject?.SetActive(value);
                node.Selected = false;
                
            }
        }

        public bool RenderAsOcclusionOnly
        {
            get => config.RenderAsOcclusionOnly;
            set
            {
                config.RenderAsOcclusionOnly = value;
                displays.ForEach(x => x.OcclusionOnly = value);
            }
        }

        public Color Tint
        {
            get => config.Tint;
            set
            {
                config.Tint = value;
                displays.ForEach(x => x.Tint = value);
            }
        }

        string Decorate(string jointName)
        {
            return $"{config.FramePrefix}{jointName}{config.FrameSuffix}";
        }

        public bool AttachToTF
        {
            get => config.AttachToTF;
            set
            {
                if (value)
                {
                    RobotObject.transform.SetParentLocal(TFListener.MapFrame.transform);
                    foreach (var link in originalLinkPoses.Keys)
                    {
                        TFFrame frame = TFListener.GetOrCreateFrame(Decorate(link.name), node);
                        link.transform.SetParentLocal(frame.transform);
                        link.transform.SetLocalPose(Pose.identity);
                    }

                    // fill in missing frame parents, but only if it hasn't been provided already
                    foreach (var entry in originalLinkParents)
                    {
                        TFFrame frame = TFListener.GetOrCreateFrame(Decorate(entry.Key.name), node);
                        if (frame.Parent == TFListener.RootFrame)
                        {
                            TFFrame parentFrame = TFListener.GetOrCreateFrame(Decorate(entry.Value.name), node);
                            frame.Parent = parentFrame;
                        }
                    }

                    /*
                    originalLinkParents.ForEach(x =>
                    {
                        TFFrame frame = TFListener.GetOrCreateFrame(Decorate(x.Key.name), node);
                        TFFrame parentFrame = TFListener.GetOrCreateFrame(Decorate(x.Value.name), node);

                        //Debug.Log(frame.Id + " " + Decorate(x.Key.name));
                        //frame.Parent = null;
                        //Debug.Log(parentFrame.Id);
                        //Debug.Log(parentFrame.Id + " " + Decorate(x.Value.name));
                        //parentFrame.Parent = null;

                        x.Key.transform.SetParentLocal(frame.transform);
                        x.Key.transform.SetLocalPose(Pose.identity);
                    });
                    **/
                    /*
                    originalLinkParents.ForEach(x =>
                    {
                        TFFrame frame = TFListener.GetOrCreateFrame(Decorate(x.Key.name), node);
                        TFFrame parentFrame = TFListener.GetOrCreateFrame(Decorate(x.Value.name), node);
                        frame.Parent = parentFrame;
                    });
                    */
                    //BaseLink.transform.SetParentLocal(node.transform);
                }
                else
                {
                    foreach (var entry in originalLinkParents)
                    {
                        if (TFListener.TryGetFrame(Decorate(entry.Key.name), out TFFrame frame))
                        {
                            frame.RemoveListener(node);
                        }
                        if (TFListener.TryGetFrame(Decorate(entry.Value.name), out TFFrame parentFrame))
                        {
                            parentFrame.RemoveListener(node);
                        }

                        entry.Key.transform.SetParentLocal(entry.Value.transform);
                        entry.Key.transform.SetLocalPose(originalLinkPoses[entry.Key]);
                    }

                    node.Parent = null;
                    //RobotObject.transform.SetParentLocal(node.transform);
                    jointWriters.Values.ForEach(x => x.Reset());
                }
                if (BaseLink is null)
                {
                    node.Parent = TFListener.MapFrame;
                }
                else
                {
                    node.AttachTo(Decorate(BaseLink.name));
                    BaseLink.transform.SetParentLocal(node.transform);
                }
                config.AttachToTF = value;
            }
        }

        public IModuleData ModuleData
        {
            get => node.ModuleData;
            private set => node.ModuleData = value;
        }

        public RobotController(IModuleData moduleData)
        {
            node = ObjectClickableNode.Instantiate("RobotNode");
            node.Selectable = false;
            ModuleData = moduleData;
            
            carrier = new GameObject();
            carrierCollider = carrier.AddComponent<BoxCollider>();
            node.Target = carrier;

            Config = new RobotConfiguration();
        }

        void LoadRobotFromResource(string newResource)
        {
            if (newResource == config.RobotResource && !(RobotObject is null))
            {
                return;
            }

            bool oldAttachToTf = AttachToTF;

            DisposeRobot();
            //node.Target = null;

            config.RobotResource = newResource;

            try
            {
                RobotObject =
                    ResourcePool.GetOrCreate(Resource.Robots.Objects[newResource], TFListener.MapFrame.transform);
            }
            catch (ResourceNotFoundException)
            {
                Debug.LogError("Robot: Resource '" + newResource + "' not found!");
                
                node.Target = null;
            }

            RobotObject.name = newResource + "!";

            Name = Name; // update name;


            if (node.Selected)
            {
                node.Selected = true; // update bounds
            }

            robotInfo = RobotObject.GetComponent<RobotInfo>(); // check if recycled
            if (robotInfo is null)
            {
                robotInfo = RobotObject.AddComponent<RobotInfo>();
                robotInfo.owner = this;
                UpdateMaterials();
            }

            originalLinkParents.Clear();
            originalLinkPoses.Clear();
            BaseLink = null;
            
            UrdfLink[] links = RobotObject.GetComponentsInChildren<UrdfLink>();
            
            links.ForEach(x =>
            {
                GameObject parentObject = x.transform.parent.gameObject;
                if (parentObject.GetComponent<UrdfLink>() != null)
                {
                    //Debug.Log("Original parent: " + x.gameObject + " -> " + parentObject);
                    originalLinkParents.Add(x.gameObject, parentObject);
                }

                originalLinkPoses.Add(x.gameObject, x.transform.AsLocalPose());
            });
            
            RobotObject.GetComponentsInChildren<UrdfJoint>().ForEach(x =>
            {
                if (x.JointType != UrdfJoint.JointTypes.Fixed)
                {
                    jointWriters[x.JointName] = x.gameObject.AddComponent<JointInfo>();
                }
            });

            RobotObject.SetActive(Visible);

            BaseLink = links.
                FirstOrDefault(x => x.transform.parent.GetComponentInParent<UrdfLink>() is null)?.
                gameObject;

            if (BaseLink is null)
            {
                Debug.LogWarning("Robot " + newResource + " has no base link!");
            }
            else
            {
                node.AttachTo(Decorate(BaseLink.name));
                BaseLink.transform.SetParentLocal(node.transform);

                BoxCollider robotCollider = RobotObject.GetComponent<BoxCollider>();
                carrierCollider.center = robotCollider.center;
                carrierCollider.size = robotCollider.size;
                robotCollider.enabled = false;

                node.name = "Node [" + newResource + "]";
                carrier.name = "Robot [" + newResource + "]";
            }

            if (oldAttachToTf)
            {
                AttachToTF = true;
            }
        }

        void UpdateMaterials()
        {
            MeshRenderer[] renderers = RobotObject.GetComponentsInChildren<MeshRenderer>();

            this.displays.Clear();
            foreach (MeshRenderer meshRenderer in renderers)
            {
                MarkerWrapper item = meshRenderer.gameObject.AddComponent<MarkerWrapper>();
                item.Tint = Tint;
                item.OcclusionOnly = RenderAsOcclusionOnly;
                displays.Add(item);
            }
        }

        /*
        public void UpdateJoints(IReadOnlyDictionary<string, float> newJointValues, List<string> unmatched)
        {
            unmatched?.Clear();
            newJointValues.ForEach(x =>
            {
                if (jointWriters.TryGetValue(x.Key, out JointInfo writer))
                {
                    writer.Write(x.Value);
                }
                else
                {
                    unmatched?.Add(x.Key);
                }
            });
        }
        */

        public void Stop()
        {
            node.Stop();
            DisposeRobot();
            Stopped?.Invoke();

            UnityEngine.Object.Destroy(carrier.gameObject);
            UnityEngine.Object.Destroy(node.gameObject);
        }

        public void Reset()
        {
            
        }

        void DisposeRobot()
        {
            if (AttachToTF)
            {
                AttachToTF = false;
            }

            if (!(RobotObject is null))
            {
                BaseLink?.transform.SetParentLocal(RobotObject.transform);
                ResourcePool.Dispose(Resource.Robots.Objects[config.RobotResource], RobotObject);
            }
            config.RobotResource = null;
            RobotObject = null;

            if (!(robotInfo is null))
            {
                robotInfo.owner = null;
            }
            robotInfo = null;
            BaseLink = null;
            jointWriters.Clear();
            originalLinkParents.Clear();
            originalLinkPoses.Clear();
            displays.Clear();
        }
    }

    public class JointInfo : MonoBehaviour
    {
        public UrdfJoint UrdfJoint { get; private set; }
        public float State { get; private set; } // rad or m
        public UrdfJoint.JointTypes Type => UrdfJoint.JointType;
        public bool HasRange { get; private set; }
        public float Min { get; private set; }
        public float Max { get; private set; }


        void Awake()
        {
            UrdfJoint = GetComponent<UrdfJoint>();
            if (Type == UrdfJoint.JointTypes.Revolute)
            {
                HingeJointLimitsManager manager = GetComponent<HingeJointLimitsManager>();
                if (manager != null)
                {
                    HasRange = true;
                    Min = manager.AngleLimitMin;
                    Max = manager.AngleLimitMax;
                }
            }
            else if (Type == UrdfJoint.JointTypes.Prismatic)
            {
                PrismaticJointLimitsManager manager = GetComponent<PrismaticJointLimitsManager>();
                if (manager != null)
                {
                    HasRange = true;
                    Min = manager.PositionLimitMin;
                    Max = manager.PositionLimitMax;
                }
            }
        }

        public void Write(float newState)
        {
            UrdfJoint.UpdateJointState(newState - State);
            State = newState;
        }

        public void Reset()
        {
            State = 0;
        }
    }

}