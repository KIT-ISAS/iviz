#nullable enable

using System;
using System.Linq;
using System.Threading.Tasks;
using Iviz.App;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.XmlRpc;
using UnityEngine;

namespace Iviz.Controllers
{
    /// <summary>
    /// Controller for robots.
    /// </summary>
    public sealed class SimpleRobotController : IController, IHasFrame, IJointProvider
    {
        const int ParameterTimeoutInMs = 3000;

        readonly RobotConfiguration config = new();
        readonly FrameNode node;
        Task? robotLoadingTask;
        RobotModel? robot;

        GameObject? RobotObject => Robot?.BaseLinkObject;

        public event Action? RobotFinishedLoading;

        public RobotModel? Robot
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

        public RobotConfiguration Config
        {
            get => config;
            private set
            {
                AttachedToTf = value.AttachedToTf;
                FramePrefix = value.FramePrefix;
                FrameSuffix = value.FrameSuffix;
                Visible = value.Visible;
                RenderAsOcclusionOnly = value.RenderAsOcclusionOnly;
                Tint = value.Tint.ToUnityColor();
                Smoothness = value.Smoothness;
                Metallic = value.Metallic;
                KeepMeshMaterials = value.KeepMeshMaterials;
                Interactable = value.Interactable;

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

                node.AttachTo(Robot.BaseLink != null ? Decorate(Robot.BaseLink) : null);
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

                node.AttachTo(Robot.BaseLink != null ? Decorate(Robot.BaseLink) : null);
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

        public bool Interactable
        {
            get => config.Interactable;
            set
            {
                config.Interactable = value;
                if (Robot is null)
                {
                    return;
                }

                var colliders = Robot.BaseLinkObject
                    .GetComponentsInChildren<Collider>(true)
                    .Where(collider => collider.gameObject.layer == LayerType.Collider);
                foreach (var collider in colliders)
                {
                    collider.enabled = value;
                }
            }
        }

        public bool KeepMeshMaterials
        {
            get => config.KeepMeshMaterials;
            set => config.KeepMeshMaterials = value;
        }

        public TfFrame? Frame => node.Parent;

        public string Name
        {
            get
            {
                if (Robot == null)
                {
                    return "[No Robot Loaded]";
                }

                if (!string.IsNullOrEmpty(Robot.Name))
                {
                    return Robot.Name;
                }

                return Robot.LinkObjects.Count == 0 ? "[Empty Robot]" : "[Empty Name]";
            }
        }

        public event Action? Stopped;

        public SimpleRobotController(RobotConfiguration? config)
        {
            node = FrameNode.Instantiate("SimpleRobotNode");
            Config = config ?? new RobotConfiguration();
        }


        public bool TryWriteJoint(string joint, float value)
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

        public void ProcessRobotSource(string? savedRobotName, string? sourceParameter)
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

        public async void TryLoadFromSourceParameter(string? value)
        {
            config.SourceParameter = "";
            Robot = null;

            if (value is null or "")
            {
                config.SavedRobotName = "";
                HelpText = "[No Robot Loaded]";
                return;
            }

            XmlRpcValue parameterValue;
            string? errorMsg;
            try
            {
                HelpText = "- Requesting parameter -";
                (parameterValue, errorMsg) =
                    await ConnectionManager.Connection.GetParameterAsync(value, ParameterTimeoutInMs);
            }
            catch (OperationCanceledException)
            {
                HelpText = "<b>Error:</b> Task cancelled";
                RosLogger.Debug($"{this}: Error while loading parameter '{value}': Task cancelled or timed out");
                return;
            }
            catch (Exception e)
            {
                RosLogger.Debug($"{this}: Error while loading parameter '{value}'", e);
                HelpText = "<b>Error:</b> Failed to retrieve parameter";
                return;
            }

            if (errorMsg != null)
            {
                HelpText = $"<b>Error:</b> {errorMsg}";
                RosLogger.Debug($"{this}: Error while loading parameter '{value}': {errorMsg}");
                return;
            }

            if (!parameterValue.TryGetString(out string robotDescription))
            {
                RosLogger.Debug($"{this}: Parameter '{value}' was not string!");
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

        public async void TryLoadSavedRobot(string? robotName)
        {
            config.SavedRobotName = "";
            Robot = null;

            if (robotName is null or "")
            {
                config.SourceParameter = "";
                HelpText = "[No Robot Loaded]";
                return;
            }

            (bool result, string? robotDescription) = await Resource.TryGetRobotAsync(robotName);
            if (!result)
            {
                RosLogger.Debug($"{this}: Failed to load robot!");
                HelpText = "[Failed to Load Saved Robot]";
                return;
            }

            config.SourceParameter = "";
            config.SavedRobotName = robotName;
            LoadRobotFromDescription(robotDescription);
        }

        bool LoadRobotFromDescription(string? description)
        {
            if (description is null or "")
            {
                RosLogger.Debug($"{this}: Empty parameter '{description}'");
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
                RosLogger.Debug($"{this}: Error parsing description'", e);
                HelpText = "[Failed to Parse Description]";
                return false;
            }

            Robot = newRobot;
            HelpText = "[Loading Robot...]";

            async void LoadRobotAsync()
            {
                robotLoadingTask = newRobot.StartAsync(ConnectionManager.ServiceProvider, KeepMeshMaterials).AsTask();
                await robotLoadingTask;
                RobotLinkHighlightable.ProcessRobot(newRobot.Name, newRobot.BaseLinkObject);
                UpdateStartTaskStatus();
            }

            LoadRobotAsync();
            UpdateStartTaskStatus();
            return true;
        }

        public void UpdateStartTaskStatus()
        {
            if (robotLoadingTask == null)
            {
                return;
            }

            try
            {
                var status = robotLoadingTask.Status;
                switch (status)
                {
                    case TaskStatus.Faulted:
                        HelpText = "[Error Loading Robot. See Log.]";
                        Robot = null;
                        robotLoadingTask = null;
                        RobotFinishedLoading?.Invoke();
                        return;
                    case TaskStatus.Canceled:
                        HelpText = "[Robot Task canceled.]";
                        robotLoadingTask = null;
                        RobotFinishedLoading?.Invoke();
                        return;
                    case TaskStatus.RanToCompletion:
                        node.Name = "SimpleRobotNode:" + Name;
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
                        RobotFinishedLoading?.Invoke();
                        break;
                }
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error in CheckRobotStartTask", e);
            }
        }

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

            foreach (var (link, parentLink) in Robot.LinkParents)
            {
                if (TfListener.TryGetFrame(Decorate(link), out var frame))
                {
                    frame.RemoveListener(node);
                }

                if (TfListener.TryGetFrame(Decorate(parentLink), out var parentFrame))
                {
                    parentFrame.RemoveListener(node);
                }
            }

            node.Parent = null;
            Robot.ResetLinkParents();
            Robot.ApplyAnyValidConfiguration();

            node.AttachTo(Robot.BaseLink != null ? Decorate(Robot.BaseLink) : null);
            Robot.BaseLinkObject.transform.SetParentLocal(node.Transform);
        }

        void AttachToTf()
        {
            if (Robot == null || RobotObject == null)
            {
                return;
            }

            RobotObject.transform.SetParentLocal(TfListener.DefaultFrame.Transform);
            foreach (var (link, linkObject) in Robot.LinkObjects)
            {
                var frame = TfListener.GetOrCreateFrame(Decorate(link), node);
                linkObject.transform.SetParentLocal(frame.Transform);
                linkObject.transform.SetLocalPose(Pose.identity);
            }

            // fill in missing frame parents, but only they don't already have one
            foreach (var (link, parentLink) in Robot.LinkParents)
            {
                var frame = TfListener.GetOrCreateFrame(Decorate(link), node);
                if (frame.Parent == TfListener.OriginFrame)
                {
                    frame.Parent = TfListener.GetOrCreateFrame(Decorate(parentLink), node);
                }
            }

            node.AttachTo(Robot.BaseLink != null ? Decorate(Robot.BaseLink) : null);
            Robot.BaseLinkObject.transform.SetParentLocal(node.Transform);
        }

        public void Dispose()
        {
            if (AttachedToTf)
            {
                AttachedToTf = false;
            }

            Robot = null;
            RobotFinishedLoading = null;
            Stopped?.Invoke();
            node.Dispose();
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