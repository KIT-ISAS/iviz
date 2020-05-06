using UnityEngine;
using RosSharp;
using System.Linq;
using System.Collections.Generic;
using RosSharp.Urdf;
using System;

namespace Iviz.App
{
    class RobotInfo : MonoBehaviour
    {
        public Robot owner;
    }

    public class Robot : ClickableDisplayNode
    {
        public GameObject RobotObject { get; private set; }
        public GameObject BaseLink { get; private set; }

        RobotInfo robotInfo;
        BoxCollider boxCollider;

        readonly Dictionary<GameObject, GameObject> originalLinkParents = new Dictionary<GameObject, GameObject>();
        readonly Dictionary<GameObject, Pose> originalLinkPoses = new Dictionary<GameObject, Pose>();
        readonly Dictionary<string, JointInfo> jointWriters = new Dictionary<string, JointInfo>();

        public IReadOnlyDictionary<string, JointInfo> JointWriters => jointWriters;

        [Serializable]
        public class Configuration
        {
            public Resource.Module module => Resource.Module.Robot;
            public string robotName = "";
            public string robotResource = Resource.Robots.Names[0];
            public string framePrefix = "";
            public string frameSuffix = "";
            public bool attachToTF = false;
        }

        readonly Configuration config = new Configuration();
        public Configuration Config
        {
            get => config;
            set
            {
                RobotResource = value.robotResource;
                AttachToTF = value.attachToTF;
                FramePrefix = value.framePrefix;
                FrameSuffix = value.frameSuffix;
            }
        }

        public string Name
        {
            get => config.robotName;
            set
            {
                if (value == "")
                {
                    name = "Robot: " + config.robotResource;
                }
                else
                {
                    name = "Robot: " + value;
                }
                config.robotName = value;
            }
        }

        public string LongName => (Name == "") ? config.robotResource : Name;

        public string RobotResource
        {
            get => config.robotResource;
            set
            {
                LoadRobotFromResource(value);
            }
        }

        public string FramePrefix
        {
            get => config.framePrefix;
            set
            {
                if (AttachToTF)
                {
                    AttachToTF = false;
                    config.framePrefix = value;
                    AttachToTF = true;
                }
                else
                {
                    config.framePrefix = value;
                }
                if (BaseLink != null)
                {
                    SetParent(Decorate(BaseLink.name));
                }
            }
        }

        public string FrameSuffix
        {
            get => config.frameSuffix;
            set
            {
                if (AttachToTF)
                {
                    AttachToTF = false;
                    config.frameSuffix = value;
                    AttachToTF = true;
                }
                else
                {
                    config.frameSuffix = value;
                }
                if (BaseLink != null)
                {
                    SetParent(Decorate(BaseLink.name));
                }
            }
        }

        public string Decorate(string jointName)
        {
            return $"{config.framePrefix}{jointName}{config.frameSuffix}";
        }

        public bool AttachToTF
        {
            get => config.attachToTF;
            set
            {
                if (value)
                {
                    RobotObject.transform.SetParentLocal(TFListener.BaseFrame.transform);
                    originalLinkParents.ForEach(x =>
                    {
                        TFFrame frame = TFListener.GetOrCreateFrame(Decorate(x.Key.name), this);
                        TFFrame parentFrame = TFListener.GetOrCreateFrame(Decorate(x.Value.name), this);

                        frame.Parent = null;
                        parentFrame.Parent = null;

                        x.Key.transform.SetParentLocal(frame.transform);
                        x.Key.transform.SetLocalPose(Pose.identity);
                    });
                    originalLinkParents.ForEach(x =>
                    {
                        TFFrame frame = TFListener.GetOrCreateFrame(Decorate(x.Key.name), this);
                        TFFrame parentFrame = TFListener.GetOrCreateFrame(Decorate(x.Value.name), this);
                        frame.Parent = parentFrame;
                    });
                }
                else
                {
                    originalLinkParents.ForEach(x =>
                    {
                        if (TFListener.TryGetFrame(Decorate(x.Key.name), out TFFrame frame))
                        {
                            frame.RemoveListener(this);
                        }
                        if (TFListener.TryGetFrame(Decorate(x.Value.name), out TFFrame parentFrame))
                        {
                            parentFrame.RemoveListener(this);
                        }

                        x.Key.transform.SetParentLocal(x.Value == null ? transform : x.Value.transform);
                        x.Key.transform.SetLocalPose(originalLinkPoses[x.Key]);
                    });
                    Parent = null;
                    RobotObject.transform.SetParentLocal(transform);
                    jointWriters.Values.ForEach(x => x.Reset());
                }
                if (BaseLink == null)
                {
                    Parent = TFListener.BaseFrame;
                }
                else
                {
                    SetParent(Decorate(BaseLink.name));
                }
                config.attachToTF = value;
            }
        }

        void Awake()
        {
            gameObject.layer = Resource.ClickableLayer;
            boxCollider = GetComponent<BoxCollider>();

            Config = new Configuration();
        }

        void LoadRobotFromResource(string newResource)
        {
            if (newResource == config.robotResource && RobotObject != null)
            {
                return;
            }

            bool oldAttachToTf = AttachToTF;

            DisposeRobot();

            config.robotResource = newResource;

            RobotObject = ResourcePool.GetOrCreate(Resource.Robots.GetObject(newResource), TFListener.BaseFrame.transform);
            RobotObject.name = newResource;

            Name = Name; // update name;

            BoxCollider robotCollider = RobotObject.GetComponent<BoxCollider>();
            boxCollider.size = robotCollider.size;
            boxCollider.center = robotCollider.center;

            Bounds = robotCollider.bounds;
            if (Selected)
            {
                Selected = true; // update bounds
            }

            robotInfo = RobotObject.GetComponent<RobotInfo>(); // check if recycled
            if (robotInfo == null)
            {
                robotInfo = RobotObject.AddComponent<RobotInfo>();
                robotInfo.owner = this;
                OptimizeMaterials();
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

            BaseLink = RobotObject.
                GetComponentsInChildren<UrdfLink>().
                First(x => x.transform.parent.GetComponentInParent<UrdfLink>() == null).
                gameObject;

            if (BaseLink == null)
            {
                Debug.LogWarning("Robot " + newResource + " has no base link!");
            }

            if (oldAttachToTf)
            {
                AttachToTF = true;
            }
        }

        void OptimizeMaterials()
        {
            MeshRenderer[] renderers = RobotObject.GetComponentsInChildren<MeshRenderer>();
            Dictionary<Texture, Material> materialsByTexture = new Dictionary<Texture, Material>();

            foreach (MeshRenderer meshRenderer in renderers)
            {
                Material[] materials = meshRenderer.sharedMaterials.ToArray();
                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i] == null)
                    {
                        materials[i] = Resource.Materials.Lit;
                    }
                    else if (materials[i].mainTexture == null)
                    {
                        Color c = materials[i].color;
                        materials[i] = Resource.Materials.Lit;
                        meshRenderer.SetPropertyColor(c, i);
                    }
                    else
                    {
                        Texture tex = materials[i].mainTexture;
                        if (!materialsByTexture.TryGetValue(tex, out Material material))
                        {
                            material = Instantiate(Resource.Materials.TexturedLit);
                            material.mainTexture = tex;
                            material.name = Resource.Materials.TexturedLit.name + " - " + materialsByTexture.Count;
                            materialsByTexture[tex] = material;
                        }
                        meshRenderer.SetPropertyColor(Color.white, i);
                        meshRenderer.SetPropertyMainTexST(materials[i].mainTextureOffset, materials[i].mainTextureScale, i);
                        materials[i] = material;
                    }
                }
                meshRenderer.sharedMaterials = materials;
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

        public override void Stop()
        {
            base.Stop();
            DisposeRobot();
            Config = new Configuration();
        }

        void DisposeRobot()
        {
            if (AttachToTF)
            {
                AttachToTF = false;
            }

            if (RobotObject != null)
            {
                ResourcePool.Dispose(Resource.Robots.GetObject(config.robotResource), RobotObject);
            }
            config.robotResource = null;
            RobotObject = null;
            if (robotInfo != null)
            {
                robotInfo.owner = null;
            }
            robotInfo = null;
            BaseLink = null;
            jointWriters.Clear();
            originalLinkParents.Clear();
            originalLinkPoses.Clear();
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