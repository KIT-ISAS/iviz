using System;
using System.Runtime.Serialization;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class SimpleRobotConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string SourceParameter { get; set; } = "";
        [DataMember] public string SavedRobotName { get; set; } = "";
        [DataMember] public string FramePrefix { get; set; } = "";
        [DataMember] public string FrameSuffix { get; set; } = "";
        [DataMember] public bool AttachedToTf { get; set; }
        [DataMember] public bool RenderAsOcclusionOnly { get; set; }
        [DataMember] public SerializableColor Tint { get; set; } = Color.white;
        [DataMember] public float Metallic { get; set; } = 0.5f;
        [DataMember] public float Smoothness { get; set; } = 0.5f;
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.Robot;
        [DataMember] public bool Visible { get; set; } = true;
    }

    /// <summary>
    /// Controller for robots.
    /// </summary>
    public sealed class SimpleRobotController : IController, IHasFrame, IJointProvider
    {
        readonly SimpleRobotConfiguration config = new SimpleRobotConfiguration();
        readonly FrameNode node;

        public SimpleRobotController([NotNull] IModuleData moduleData)
        {
            node = FrameNode.Instantiate("SimpleRobotNode");
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

            Config = new SimpleRobotConfiguration();
        }

        RobotModel robot;

        public RobotModel Robot
        {
            get => robot;
            private set
            {
                if (robot != null)
                {
                    robot.Cancel();
                    robot.Dispose();
                }

                robot = value;
            }
        }

        GameObject RobotObject => Robot.BaseLinkObject;

        public SimpleRobotConfiguration Config
        {
            get => config;
            set
            {
                AttachedToTf = value.AttachedToTf;
                FramePrefix = value.FramePrefix;
                FrameSuffix = value.FrameSuffix;
                Visible = value.Visible;
                RenderAsOcclusionOnly = value.RenderAsOcclusionOnly;
                Tint = value.Tint;
                Smoothness = value.Smoothness;
                Metallic = value.Metallic;
                
                ProcessRobotSource(value.SavedRobotName, value.SourceParameter);
            }
        }

        public string HelpText { get; private set; } = "<b>No Robot Loaded</b>";

        public string SourceParameter => config.SourceParameter;

        public string SavedRobotName => config.SavedRobotName;

        public string FramePrefix
        {
            get => config.FramePrefix;
            set
            {
                if (AttachedToTf)
                {
                    AttachedToTf = false;
                    config.FramePrefix = value;
                    AttachedToTf = true;
                }
                else
                {
                    config.FramePrefix = value;
                }

                if (Robot == null)
                {
                    return;
                }

                node.AttachTo(Decorate(Robot.BaseLink));
            }
        }

        public string FrameSuffix
        {
            get => config.FrameSuffix;
            set
            {
                if (AttachedToTf)
                {
                    AttachedToTf = false;
                    config.FrameSuffix = value;
                    AttachedToTf = true;
                }
                else
                {
                    config.FrameSuffix = value;
                }

                if (Robot == null)
                {
                    return;
                }

                node.AttachTo(Decorate(Robot.BaseLink));
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                if (Robot == null)
                {
                    return;
                }

                Robot.Visible = value;
            }
        }

        public bool RenderAsOcclusionOnly
        {
            get => config.RenderAsOcclusionOnly;
            set
            {
                config.RenderAsOcclusionOnly = value;
                if (Robot == null)
                {
                    return;
                }

                Robot.OcclusionOnly = value;
            }
        }

        public Color Tint
        {
            get => config.Tint;
            set
            {
                config.Tint = value;
                if (Robot is null)
                {
                    return;
                }

                Robot.Tint = value;
            }
        }
        
        public float Metallic
        {
            get => config.Metallic;
            set
            {
                config.Metallic = value;
                if (Robot is null)
                {
                    return;
                }

                Robot.Metallic = value;
            }
        }        
        
        public float Smoothness
        {
            get => config.Smoothness;
            set
            {
                config.Smoothness = value;
                if (Robot is null)
                {
                    return;
                }

                Robot.Smoothness = value;
            }
        }   

        public IModuleData ModuleData { get; }

        public void StopController()
        {
            node.Stop();

            if (AttachedToTf)
            {
                AttachedToTf = false;
            }

            Robot = null;
            Stopped?.Invoke();
            Object.Destroy(node.gameObject);
        }

        public void ResetController()
        {
            Robot = null;

            if (!string.IsNullOrEmpty(SavedRobotName))
            {
                if (!string.IsNullOrEmpty(SourceParameter))
                {
                    config.SourceParameter = "";
                }

                TryLoadSavedRobot(SavedRobotName);
            }

            if (!string.IsNullOrEmpty(SourceParameter))
            {
                TryLoadFromSourceParameter(SourceParameter);
            }

            if (AttachedToTf)
            {
                AttachedToTf = false;
                AttachedToTf = true;
            }
        }

        public TfFrame Frame => node.Parent;

        [NotNull] public string Name => Robot == null ? "[Empty]" : Robot.Name ?? "[No Name]";

        public event Action Stopped;

        public bool TryWriteJoint([NotNull] string joint, float value)
        {
            return Robot.TryWriteJoint(joint, value, out _);
        }

        public bool AttachedToTf
        {
            get => config.AttachedToTf;
            set
            {
                config.AttachedToTf = value;

                if (Robot is null)
                {
                    return;
                }

                if (value)
                {
                    AttachToTf();
                }
                else
                {
                    DetachFromTf();
                }
            }
        }

        public void ProcessRobotSource([CanBeNull] string savedRobotName, [CanBeNull] string sourceParameter)
        {
            if (!string.IsNullOrEmpty(savedRobotName))
            {
                if (!string.IsNullOrEmpty(sourceParameter))
                {
                    config.SourceParameter = "";
                }

                TryLoadSavedRobot(savedRobotName);
            }
            else if (!string.IsNullOrEmpty(sourceParameter))
            {
                TryLoadFromSourceParameter(sourceParameter);
            }
            else
            {
                TryLoadFromSourceParameter(null);
            }            
        }
        
        public async void TryLoadFromSourceParameter([CanBeNull] string value)
        {
            config.SourceParameter = "";
            Robot = null;

            if (string.IsNullOrEmpty(value))
            {
                config.SavedRobotName = "";
                HelpText = "[No Robot Selected]";
                return;
            }

            object parameterValue;
            try
            {
                const int timeoutInMs = 1000;
                parameterValue = await ConnectionManager.Connection.GetParameterAsync(value, timeoutInMs);
            }
            catch (Exception e)
            {
                Debug.LogError($"SimpleRobotController: Error while loading parameter '{value}': {e}");
                HelpText = "[Failed to Retrieve Parameter]";
                return;
            }

            if (!(parameterValue is string robotDescription))
            {
                Debug.Log($"SimpleRobotController: Parameter '{value}' was not string!");
                HelpText = "[Invalid Parameter Type]";
                return;
            }

            if (!LoadRobotFromDescription(robotDescription))
            {
                return;
            }

            config.SavedRobotName = "";
            config.SourceParameter = value;
        }

        public void TryLoadSavedRobot([CanBeNull] string robotName)
        {
            config.SavedRobotName = "";
            Robot = null;

            if (string.IsNullOrEmpty(robotName))
            {
                config.SourceParameter = "";
                HelpText = "[No Robot Selected]";
                return;
            }

            if (!Resource.TryGetRobot(robotName, out string robotDescription))
            {
                // shouldn't happen!
                Debug.Log("SimpleRobotController: Failed to load robot!");
                HelpText = "[Failed to Load Saved Robot]";
                return;
            }

            config.SourceParameter = "";
            config.SavedRobotName = robotName;
            LoadRobotFromDescription(robotDescription);
        }

        bool LoadRobotFromDescription([CanBeNull] string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                Debug.Log($"SimpleRobotController: Empty parameter '{description}'");
                HelpText = "[Robot Specification is Empty]";
                return false;
            }

            try
            {
                Robot = new RobotModel(description);
                Robot.StartAsync(ConnectionManager.ServiceProvider);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"SimpleRobotController: Error parsing description': {e}");
                HelpText = "[Failed to Parse Specification]";
                Robot = null;
                return false;
            }

            node.name = "SimpleRobotNode:" + Name;
            HelpText = string.IsNullOrEmpty(Robot.Name) ? "<b>[No Name]</b>" : $"<b>- {Name} -</b>";
            AttachedToTf = AttachedToTf;
            Visible = Visible;
            RenderAsOcclusionOnly = RenderAsOcclusionOnly;
            Tint = Tint;
            Smoothness = Smoothness;
            Metallic = Metallic;
            return true;
        }

        [NotNull]
        string Decorate(string jointName)
        {
            return $"{config.FramePrefix}{jointName}{config.FrameSuffix}";
        }

        void DetachFromTf()
        {
            foreach (var entry in Robot.LinkParents)
            {
                if (TfListener.TryGetFrame(Decorate(entry.Key), out TfFrame frame))
                {
                    frame.RemoveListener(node);
                }

                if (TfListener.TryGetFrame(Decorate(entry.Value), out TfFrame parentFrame))
                {
                    parentFrame.RemoveListener(node);
                }
            }

            node.Parent = null;
            Robot.ResetLinkParents();
            Robot.ApplyAnyValidConfiguration();

            node.AttachTo(Decorate(Robot.BaseLink));
            Robot.BaseLinkObject.transform.SetParentLocal(node.transform);
        }

        void AttachToTf()
        {
            RobotObject.transform.SetParentLocal(TfListener.MapFrame.transform);
            foreach (var entry in Robot.LinkObjects)
            {
                string link = entry.Key;
                GameObject linkObject = entry.Value;
                TfFrame frame = TfListener.GetOrCreateFrame(Decorate(link), node);
                linkObject.transform.SetParentLocal(frame.transform);
                linkObject.transform.SetLocalPose(Pose.identity);
            }

            // fill in missing frame parents, but only if it hasn't been provided already
            foreach (var entry in Robot.LinkParents)
            {
                TfFrame frame = TfListener.GetOrCreateFrame(Decorate(entry.Key), node);
                if (frame.Parent == TfListener.OriginFrame)
                {
                    TfFrame parentFrame = TfListener.GetOrCreateFrame(Decorate(entry.Value), node);
                    frame.Parent = parentFrame;
                }
            }

            node.AttachTo(Decorate(Robot.BaseLink));
            Robot.BaseLinkObject.transform.SetParentLocal(node.transform);
        }
    }
}