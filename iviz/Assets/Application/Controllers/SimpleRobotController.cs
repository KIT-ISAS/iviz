using System;
using System.Threading.Tasks;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Roslib.Utils;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Iviz.Controllers
{
    /// <summary>
    /// Controller for robots.
    /// </summary>
    public sealed class SimpleRobotController : IController, IHasFrame, IJointProvider
    {
        readonly RobotConfiguration config = new RobotConfiguration();
        readonly FrameNode node;
        Task robotLoadingTask;

        public SimpleRobotController([NotNull] IModuleData moduleData)
        {
            node = FrameNode.Instantiate("SimpleRobotNode");
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

            Config = new RobotConfiguration();
        }

        RobotModel robot;

        [CanBeNull]
        public RobotModel Robot
        {
            get => robot;
            private set
            {
                if (robot != null)
                {
                    robot.CancelTasks();
                    robot.Dispose();
                    robotLoadingTask = null;
                }

                robot = value;
            }
        }

        [CanBeNull] GameObject RobotObject => Robot?.BaseLinkObject;

        public RobotConfiguration Config
        {
            get => config;
            set
            {
                AttachedToTf = value.AttachedToTf;
                FramePrefix = value.FramePrefix;
                FrameSuffix = value.FrameSuffix;
                Visible = value.Visible;
                RenderAsOcclusionOnly = value.RenderAsOcclusionOnly;
                Tint = value.Tint.ToUnityColor();
                Smoothness = value.Smoothness;
                Metallic = value.Metallic;

                ProcessRobotSource(value.SavedRobotName, value.SourceParameter);
            }
        }

        public string HelpText { get; private set; } = "<b>No Robot Selected</b>";

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
            get => config.Tint.ToUnityColor();
            set
            {
                config.Tint = value.ToRos();
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

        [NotNull]
        public string Name
        {
            get
            {
                if (Robot == null)
                {
                    return "[No Robot Selected]";
                }

                if (!string.IsNullOrEmpty(Robot.Name))
                {
                    return Robot.Name;
                }

                if (Robot.LinkObjects.Count == 0)
                {
                    return "[Empty Robot]";
                }

                return Robot.Name == null ? "[No Name]" : "[Empty Name]";
            }
        }

        public event Action Stopped;

        public bool TryWriteJoint([NotNull] string joint, float value)
        {
            if (Robot == null)
            {
                throw new InvalidOperationException("There is no robot to set joints to!");
            }

            return Robot.TryWriteJoint(joint, value);
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

            XmlRpcValue parameterValue;
            string errorMsg;
            try
            {
                const int timeoutInMs = 800;
                (parameterValue, errorMsg) = await ConnectionManager.Connection.GetParameterAsync(value, timeoutInMs);
            }
            catch (OperationCanceledException)
            {
                HelpText = "<b>Error:</b> Task cancelled";
                return;
            }
            catch (Exception e)
            {
                Core.Logger.Debug($"{this}: Error while loading parameter '{value}'", e);
                HelpText = "<b>Error:</b> Failed to retrieve parameter";
                return;
            }

            if (errorMsg != null)
            {
                HelpText = $"<b>Error:</b> {errorMsg}";
                return;
            }

            if (!parameterValue.TryGetString(out string robotDescription))
            {
                Core.Logger.Debug($"{this}: Parameter '{value}' was not string!");
                HelpText = "<b>Error:</b> Expected string parameter";
                return;
            }

            if (!LoadRobotFromDescription(robotDescription))
            {
                return;
            }

            config.SavedRobotName = "";
            config.SourceParameter = value;
        }

        public async void TryLoadSavedRobot([CanBeNull] string robotName)
        {
            config.SavedRobotName = "";
            Robot = null;

            if (string.IsNullOrEmpty(robotName))
            {
                config.SourceParameter = "";
                HelpText = "[No Robot Selected]";
                return;
            }

            (bool result, string robotDescription) = await Resource.TryGetRobotAsync(robotName);
            if (!result)
            {
                Core.Logger.Debug($"{this}: Failed to load robot!");
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
                Core.Logger.Debug($"{this}: Empty parameter '{description}'");
                HelpText = "[Robot Specification is Empty]";
                return false;
            }

            try
            {
                Robot = new RobotModel(description);
            }
            catch (Exception e)
            {
                Core.Logger.Debug($"{this}: Error parsing description'", e);
                HelpText = "[Failed to Parse Specification]";
                Robot = null;
                return false;
            }

            robotLoadingTask = Robot.StartAsync(ConnectionManager.ServiceProvider);
            HelpText = "[Loading Robot...]";
            CheckRobotStartTask();
            return true;
        }

        public void CheckRobotStartTask()
        {
            if (robotLoadingTask == null)
            {
                return;
            }

            switch (robotLoadingTask.Status)
            {
                case TaskStatus.Faulted:
                    HelpText = "[Error Loading Robot. See Log.]";
                    Robot = null;
                    robotLoadingTask = null;
                    return;
                case TaskStatus.Canceled:
                    HelpText = "[Robot Task canceled.]";
                    robotLoadingTask = null;
                    return;
                case TaskStatus.RanToCompletion:
                    node.name = "SimpleRobotNode:" + Name;
                    if (Robot == null)
                    {
                        HelpText = "[Invalid Robot]";
                    }
                    else if (!string.IsNullOrEmpty(Robot.Name))
                    {
                        HelpText = $"<b>- {Name} -</b>";
                    }
                    else if (Robot.LinkObjects.Count == 0)
                    {
                        HelpText = "[Robot is Empty]";
                    }
                    else if (Robot.Name == null)
                    {
                        HelpText = "[No Name]";
                    }
                    else
                    {
                        HelpText = "[Empty Name]";
                    }

                    AttachedToTf = AttachedToTf;
                    Visible = Visible;
                    RenderAsOcclusionOnly = RenderAsOcclusionOnly;
                    Tint = Tint;
                    Smoothness = Smoothness;
                    Metallic = Metallic;
                    robotLoadingTask = null;
                    break;
            }
        }

        [NotNull]
        string Decorate(string jointName)
        {
            return $"{config.FramePrefix}{jointName}{config.FrameSuffix}";
        }

        void DetachFromTf()
        {
            if (robot == null)
            {
                return;
            }

            foreach (var entry in robot.LinkParents)
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
            robot.ResetLinkParents();
            robot.ApplyAnyValidConfiguration();

            node.AttachTo(Decorate(robot.BaseLink));
            robot.BaseLinkObject.transform.SetParentLocal(node.transform);
        }

        void AttachToTf()
        {
            if (robot == null || RobotObject == null)
            {
                return;
            }

            RobotObject.transform.SetParentLocal(TfListener.MapFrame.transform);
            foreach (var entry in robot.LinkObjects)
            {
                string link = entry.Key;
                GameObject linkObject = entry.Value;
                TfFrame frame = TfListener.GetOrCreateFrame(Decorate(link), node);
                linkObject.transform.SetParentLocal(frame.transform);
                linkObject.transform.SetLocalPose(Pose.identity);
            }

            // fill in missing frame parents, but only if it hasn't been provided already
            foreach (var entry in robot.LinkParents)
            {
                TfFrame frame = TfListener.GetOrCreateFrame(Decorate(entry.Key), node);
                if (frame.Parent == TfListener.OriginFrame)
                {
                    TfFrame parentFrame = TfListener.GetOrCreateFrame(Decorate(entry.Value), node);
                    frame.Parent = parentFrame;
                }
            }

            node.AttachTo(Decorate(robot.BaseLink));
            robot.BaseLinkObject.transform.SetParentLocal(node.transform);
        }
    }
}