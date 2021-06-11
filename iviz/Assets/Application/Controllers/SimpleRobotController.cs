using System;
using System.Threading.Tasks;
using Iviz.App;
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
        const int parameterTimeoutInMs = 3000;

        readonly RobotConfiguration config = new RobotConfiguration();
        readonly FrameNode node;
        Task robotLoadingTask;

        public SimpleRobotController([NotNull] IModuleData moduleData)
        {
            node = FrameNode.Instantiate("SimpleRobotNode");
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

            Config = new RobotConfiguration();
        }

        [CanBeNull] RobotModel robot;

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

        [NotNull] public string SourceParameter => config.SourceParameter;

        [NotNull] public string SavedRobotName => config.SavedRobotName;

        [NotNull]
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

        [NotNull]
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
                HelpText = "- Requesting parameter -";
                (parameterValue, errorMsg) = await ConnectionManager.Connection.GetParameterAsync(value, parameterTimeoutInMs);
            }
            catch (OperationCanceledException)
            {
                HelpText = "<b>Error:</b> Task cancelled";
                Core.Logger.Debug($"{this}: Error while loading parameter '{value}': Task cancelled or timed out");
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
                Core.Logger.Debug($"{this}: Error while loading parameter '{value}': {errorMsg}");
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
                HelpText = "[Robot Description is Empty]";
                return false;
            }

            Robot = null;
            RobotModel newRobot;
            try
            {
                newRobot = new RobotModel(description);
            }
            catch (Exception e)
            {
                Core.Logger.Debug($"{this}: Error parsing description'", e);
                HelpText = "[Failed to Parse Description]";
                return false;
            }

            Robot = newRobot;
            async Task LoadRobotAsync()
            {
                await newRobot.StartAsync(ConnectionManager.ServiceProvider);
                CheckRobotStartTask(true);
            }

            robotLoadingTask = LoadRobotAsync();
            HelpText = "[Loading Robot...]";
            CheckRobotStartTask();
            return true;
        }

        public void CheckRobotStartTask(bool forceCompletion = false)
        {
            if (robotLoadingTask == null)
            {
                return;
            }

            try
            {
                var status = forceCompletion 
                    ? TaskStatus.RanToCompletion 
                    : robotLoadingTask.Status;
                switch (status)
                {
                    case TaskStatus.Faulted:
                        HelpText = "[Error Loading Robot. See Log.]";
                        Robot = null;
                        robotLoadingTask = null;
                        ((SimpleRobotModuleData) ModuleData).OnRobotFinishedLoading();
                        return;
                    case TaskStatus.Canceled:
                        HelpText = "[Robot Task canceled.]";
                        robotLoadingTask = null;
                        ((SimpleRobotModuleData) ModuleData).OnRobotFinishedLoading();
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
                        ((SimpleRobotModuleData) ModuleData).OnRobotFinishedLoading();
                        break;
                }
            }
            catch (Exception e)
            {
                Core.Logger.Error($"{this}: Error in CheckRobotStartTask", e);
            }
        }

        [NotNull]
        string Decorate(string jointName)
        {
            return $"{config.FramePrefix}{jointName}{config.FrameSuffix}";
        }

        void DetachFromTf()
        {
            if (Robot == null)
            {
                return;
            }

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
            Robot.BaseLinkObject.transform.SetParentLocal(node.Transform);
        }

        void AttachToTf()
        {
            if (Robot == null || RobotObject == null)
            {
                return;
            }

            RobotObject.transform.SetParentLocal(TfListener.DefaultFrame.Transform);
            foreach (var pair in Robot.LinkObjects)
            {
                string link = pair.Key;
                GameObject linkObject = pair.Value;
                TfFrame frame = TfListener.GetOrCreateFrame(Decorate(link), node);
                linkObject.transform.SetParentLocal(frame.Transform);
                linkObject.transform.SetLocalPose(Pose.identity);
            }

            // fill in missing frame parents, but only they don't already have one
            foreach (var pair in Robot.LinkParents)
            {
                TfFrame frame = TfListener.GetOrCreateFrame(Decorate(pair.Key), node);
                if (frame.Parent == TfListener.OriginFrame)
                {
                    TfFrame parentFrame = TfListener.GetOrCreateFrame(Decorate(pair.Value), node);
                    frame.Parent = parentFrame;
                }
            }

            node.AttachTo(Decorate(Robot.BaseLink));
            Robot.BaseLinkObject.transform.SetParentLocal(node.Transform);
        }
        
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
    }
}