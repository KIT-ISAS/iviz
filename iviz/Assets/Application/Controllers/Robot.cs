using UnityEngine;
using RosSharp;
using System.Linq;
using System.Collections.Generic;
using RosSharp.Urdf;
using Iviz.App.Displays;
using System.Runtime.Serialization;
using Iviz.RoslibSharp;
using System;
using Iviz.App.Listeners;
using Iviz.Resources;
using System.Collections.ObjectModel;

namespace Iviz.App
{
    class RobotInfo : MonoBehaviour
    {
        public Robot owner;
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

    public sealed class Robot : MonoBehaviour, IController, IHasFrame
    {
        ObjectClickableNode node;
        RobotInfo robotInfo;

        public TFFrame Frame => node.Parent;

        public GameObject RobotObject { get; private set; }
        public GameObject BaseLink { get; private set; }

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

        public string Name
        {
            get => config.RobotName;
            set
            {
                if (value == "")
                {
                    name = "Robot: " + config.RobotResource;
                }
                else
                {
                    name = "Robot: " + value;
                }
                config.RobotName = value;
            }
        }

        public string LongName => (Name == "") ? config.RobotResource : Name;

        public string RobotResource
        {
            get => config.RobotResource;
            set
            {
                LoadRobotFromResource(value);
            }
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
                if (BaseLink != null)
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
                if (BaseLink != null)
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
                if (RobotObject != null)
                {
                    RobotObject.SetActive(value);
                }
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

        public string Decorate(string jointName)
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
                    RobotObject.transform.SetParentLocal(TFListener.BaseFrame.transform);
                    originalLinkParents.ForEach(x =>
                    {
                        TFFrame frame = TFListener.GetOrCreateFrame(Decorate(x.Key.name), node);
                        TFFrame parentFrame = TFListener.GetOrCreateFrame(Decorate(x.Value.name), node);

                        frame.Parent = null;
                        parentFrame.Parent = null;

                        x.Key.transform.SetParentLocal(frame.transform);
                        x.Key.transform.SetLocalPose(Pose.identity);
                    });
                    originalLinkParents.ForEach(x =>
                    {
                        TFFrame frame = TFListener.GetOrCreateFrame(Decorate(x.Key.name), node);
                        TFFrame parentFrame = TFListener.GetOrCreateFrame(Decorate(x.Value.name), node);
                        frame.Parent = parentFrame;
                    });
                }
                else
                {
                    originalLinkParents.ForEach(x =>
                    {
                        if (TFListener.TryGetFrame(Decorate(x.Key.name), out TFFrame frame))
                        {
                            frame.RemoveListener(node);
                        }
                        if (TFListener.TryGetFrame(Decorate(x.Value.name), out TFFrame parentFrame))
                        {
                            parentFrame.RemoveListener(node);
                        }

                        x.Key.transform.SetParentLocal(x.Value == null ? node.transform : x.Value.transform);
                        x.Key.transform.SetLocalPose(originalLinkPoses[x.Key]);
                    });
                    node.Parent = null;
                    RobotObject.transform.SetParentLocal(node.transform);
                    jointWriters.Values.ForEach(x => x.Reset());
                }
                if (BaseLink == null)
                {
                    node.Parent = TFListener.BaseFrame;
                }
                else
                {
                    node.AttachTo(Decorate(BaseLink.name));
                }
                config.AttachToTF = value;
            }
        }

        public ModuleData ModuleData
        {
            get => node.DisplayData;
            set => node.DisplayData = value;
        }

        void Awake()
        {
            node = ObjectClickableNode.Instantiate("RobotNode");
            //gameObject.layer = Resource.ClickableLayer;
            //boxCollider = GetComponent<BoxCollider>();

            Config = new RobotConfiguration();
        }

        void LoadRobotFromResource(string newResource)
        {
            if (newResource == config.RobotResource && RobotObject != null)
            {
                return;
            }

            bool oldAttachToTf = AttachToTF;

            DisposeRobot();

            config.RobotResource = newResource;

            RobotObject = ResourcePool.GetOrCreate(Resource.Robots.Objects[newResource], TFListener.BaseFrame.transform);
            RobotObject.name = newResource;
            //RobotObject.layer = Resource.ClickableLayer;

            Name = Name; // update name;

            node.Target = RobotObject;
            //robotCollider = RobotObject.GetComponent<BoxCollider>();
            //boxCollider.size = robotCollider.size;
            //boxCollider.center = robotCollider.center;

            if (node.Selected)
            {
                node.Selected = true; // update bounds
            }

            robotInfo = RobotObject.GetComponent<RobotInfo>(); // check if recycled
            if (robotInfo == null)
            {
                robotInfo = RobotObject.AddComponent<RobotInfo>();
                robotInfo.owner = this;
                UpdateMaterials();
            }

            originalLinkParents.Clear();
            originalLinkPoses.Clear();
            BaseLink = null;
            RobotObject.GetComponentsInChildren<UrdfLink>().ForEach(x =>
            {
                GameObject parentObject = x.transform.parent.gameObject;
                originalLinkParents.Add(x.gameObject, parentObject);
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

            BaseLink = RobotObject.
                GetComponentsInChildren<UrdfLink>().
                First(x => x.transform.parent.GetComponentInParent<UrdfLink>() == null).
                gameObject;

            if (BaseLink == null)
            {
                Debug.LogWarning("Robot " + newResource + " has no base link!");
            }
            else
            {
                node.AttachTo(Decorate(BaseLink.name));
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

        public void Stop()
        {
            node.Stop();
            DisposeRobot();
            Config = new RobotConfiguration();
            Stopped?.Invoke();
        }

        void DisposeRobot()
        {
            if (AttachToTF)
            {
                AttachToTF = false;
            }

            if (RobotObject != null)
            {
                ResourcePool.Dispose(Resource.Robots.Objects[config.RobotResource], RobotObject);
            }
            config.RobotResource = null;
            RobotObject = null;
            node.Target = null;
            //robotCollider = null;
            if (robotInfo != null)
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

        //public override void Recycle()
        //{
        //ResourcePool.Dispose(Resource.Markers.NamedBoundary, namedBoundary.gameObject);
        //namedBoundary = null;
        //}
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