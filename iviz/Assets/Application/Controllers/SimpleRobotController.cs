using UnityEngine;
using System.Runtime.Serialization;
using Iviz.Roslib;
using System;
using Iviz.Displays;
using Iviz.Resources;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class SimpleRobotConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public Resource.Module Module => Resource.Module.SimpleRobot;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string SourceParameter { get; set; } = "";
        [DataMember] public string FramePrefix { get; set; } = "";
        [DataMember] public string FrameSuffix { get; set; } = "";
        [DataMember] public bool AttachToTf { get; set; } = false;
        [DataMember] public bool RenderAsOcclusionOnly { get; set; } = false;
        [DataMember] public SerializableColor Tint { get; set; } = Color.white;
    }

    public sealed class SimpleRobotController : IController, IHasFrame, IJointProvider
    {
        readonly SimpleDisplayNode node;
        RobotModel robot;

        public TFFrame Frame => node.Parent;

        GameObject RobotObject => robot.BaseLinkObject;

        public string Name { get; } = "robot_description";

        public event Action Stopped;

        readonly SimpleRobotConfiguration config = new SimpleRobotConfiguration();

        public SimpleRobotConfiguration Config
        {
            get => config;
            set
            {
                AttachToTf = value.AttachToTf;
                FramePrefix = value.FramePrefix;
                FrameSuffix = value.FrameSuffix;
                Visible = value.Visible;
                RenderAsOcclusionOnly = value.RenderAsOcclusionOnly;
                Tint = value.Tint;
            }
        }

        public string SourceParameter
        {
            get => config.SourceParameter;
            set
            {
                if (value == config.SourceParameter)
                {
                    return;
                }

                config.SourceParameter = value;

                robot?.Dispose();
                if (value.Length == 0)
                {
                    return;
                }
                
                try
                {
                    string description = ConnectionManager.Connection.GetParameter(value);
                    robot = new RobotModel(description);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    robot = null;
                    config.SourceParameter = string.Empty;
                }
            }
        }

        public string FramePrefix
        {
            get => config.FramePrefix;
            set
            {
                if (AttachToTf)
                {
                    AttachToTf = false;
                    config.FramePrefix = value;
                    AttachToTf = true;
                }
                else
                {
                    config.FramePrefix = value;
                }

                if (robot is null)
                {
                    return;
                }

                node.AttachTo(Decorate(robot.BaseLink));
            }
        }

        public string FrameSuffix
        {
            get => config.FrameSuffix;
            set
            {
                if (AttachToTf)
                {
                    AttachToTf = false;
                    config.FrameSuffix = value;
                    AttachToTf = true;
                }
                else
                {
                    config.FrameSuffix = value;
                }

                if (robot is null)
                {
                    return;
                }

                node.AttachTo(Decorate(robot.BaseLink));
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                if (robot is null)
                {
                    return;
                }

                robot.Visible = value;
            }
        }

        public bool RenderAsOcclusionOnly
        {
            get => config.RenderAsOcclusionOnly;
            set
            {
                config.RenderAsOcclusionOnly = value;
                if (robot is null)
                {
                    return;
                }

                robot.OcclusionOnly = value;
            }
        }

        public Color Tint
        {
            get => config.Tint;
            set
            {
                config.Tint = value;
                if (robot is null)
                {
                    return;
                }

                robot.Tint = value;
            }
        }

        string Decorate(string jointName)
        {
            return $"{config.FramePrefix}{jointName}{config.FrameSuffix}";
        }

        public bool TryWriteJoint(string joint, float value)
        {
            return robot.TryWriteJoint(joint, value, out _);
        }

        public bool AttachToTf
        {
            get => config.AttachToTf;
            set
            {
                if (value == config.AttachToTf)
                {
                    return;
                }

                config.AttachToTf = value;

                if (robot is null)
                {
                    return;
                }

                if (value)
                {
                    RobotObject.transform.SetParentLocal(TFListener.MapFrame.transform);
                    foreach (var entry in robot.LinkObjects)
                    {
                        string link = entry.Key;
                        GameObject linkObject = entry.Value;
                        TFFrame frame = TFListener.GetOrCreateFrame(Decorate(link), node);
                        linkObject.transform.SetParentLocal(frame.transform);
                        linkObject.transform.SetLocalPose(Pose.identity);
                    }

                    // fill in missing frame parents, but only if it hasn't been provided already
                    foreach (var entry in robot.LinkParents)
                    {
                        TFFrame frame = TFListener.GetOrCreateFrame(Decorate(entry.Key), node);
                        if (frame.Parent == TFListener.RootFrame)
                        {
                            TFFrame parentFrame = TFListener.GetOrCreateFrame(Decorate(entry.Value), node);
                            frame.Parent = parentFrame;
                        }
                    }
                }
                else
                {
                    foreach (var entry in robot.LinkParents)
                    {
                        if (TFListener.TryGetFrame(Decorate(entry.Key), out TFFrame frame))
                        {
                            frame.RemoveListener(node);
                        }

                        if (TFListener.TryGetFrame(Decorate(entry.Value), out TFFrame parentFrame))
                        {
                            parentFrame.RemoveListener(node);
                        }
                    }

                    node.Parent = null;
                    robot.ResetLinkParents();
                    robot.ApplyAnyValidConfiguration();
                }

                node.AttachTo(Decorate(robot.BaseLink));
                robot.BaseLinkObject.transform.SetParentLocal(node.transform);
            }
        }

        public IModuleData ModuleData { get; private set; }

        public SimpleRobotController(IModuleData moduleData)
        {
            node = SimpleDisplayNode.Instantiate("SimpleRobotNode");
            ModuleData = moduleData;

            Config = new SimpleRobotConfiguration();
        }

        public void Stop()
        {
            node.Stop();

            if (AttachToTf)
            {
                AttachToTf = false;
            }

            robot?.Dispose();
            Stopped?.Invoke();
            UnityEngine.Object.Destroy(node.gameObject);
        }

        public void Reset()
        {
            string parameter = SourceParameter;
            SourceParameter = "";
            SourceParameter = parameter;
            
            if (AttachToTf)
            {
                AttachToTf = false;
                AttachToTf = true;
            }
        }
    }
}