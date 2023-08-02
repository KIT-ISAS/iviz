#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using Iviz.XmlRpc;
using UnityEngine;

namespace Iviz.Controllers
{
    /// <summary>
    /// Controller for robots.
    /// </summary>
    public sealed class SimpleRobotController : Controller, IHasFrame, IJointProvider
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
                Tint = value.Tint.ToUnity();
                Smoothness = value.Smoothness;
                Metallic = value.Metallic;
                KeepMeshMaterials = value.KeepMeshMaterials;
                Interactable = value.Interactable;

                _ = ProcessRobotSourceAsync(value.SavedRobotName, value.SourceParameter);
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
                if (Robot == null)
                {
                    config.FramePrefix = value;
                    return;
                }

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

                node.AttachTo(Decorate(Robot.BaseLink));
            }
        }

        public string FrameSuffix
        {
            get => config.FrameSuffix;
            set
            {
                if (Robot == null)
                {
                    config.FrameSuffix = value;
                    return;
                }

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

                node.AttachTo(Decorate(Robot.BaseLink));
            }
        }

        public override bool Visible
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
            get => config.Tint.ToUnity();
            set
            {
                config.Tint = value.ToRos();
                if (Robot == null)
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
                if (Robot == null)
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
                if (Robot == null)
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
                if (Robot == null)
                {
                    return;
                }

                var allColliders = Robot.LinkObjects.Values.SelectMany(
                    linkObject => linkObject.transform.GetAllChildren()
                        .WithComponent<Collider>()
                        .Where(collider => collider.gameObject.layer == LayerType.Collider));
                foreach (var collider in allColliders)
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

                if (!string.IsNullOrWhiteSpace(Robot.Name))
                {
                    return Robot.Name;
                }

                return Robot.LinkObjects.Count == 0 ? "[Empty Robot]" : "[Empty Name]";
            }
        }

        public bool AttachedToTf
        {
            get => config.AttachedToTf;
            set
            {
                config.AttachedToTf = value;

                if (Robot == null)
                {
                    return;
                }

                try
                {
                    if (value)
                    {
                        AttachToTf();
                    }
                    else
                    {
                        DetachFromTf();
                    }
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{ToString()}: Error while attaching to TF", e);
                }
            }
        }

        public event Action? Stopped;

        public SimpleRobotController(RobotConfiguration? config)
        {
            node = new FrameNode("SimpleRobotNode");
            Config = config ?? new RobotConfiguration();
        }


        public bool TryWriteJoint(string joint, float value)
        {
            if (Robot == null) ThrowHelper.ThrowInvalidOperation("There is no robot to set joints to!");
            return Robot.TryWriteJoint(joint, value);
        }

        public Task ProcessRobotSourceAsync(string? savedRobotName, string? sourceParameter)
        {
            if (!string.IsNullOrWhiteSpace(savedRobotName))
            {
                if (!string.IsNullOrWhiteSpace(sourceParameter))
                {
                    config.SourceParameter = "";
                }

                return TryLoadSavedRobotAsync(savedRobotName).AwaitNoThrow(this);
            }

            if (!string.IsNullOrWhiteSpace(sourceParameter))
            {
                return TryLoadFromSourceParameterAsync(sourceParameter).AwaitNoThrow(this);
            }

            return TryLoadFromSourceParameterAsync(null).AwaitNoThrow(this);
        }

        public async ValueTask TryLoadFromSourceParameterAsync(string? sourceParameter)
        {
            config.SourceParameter = sourceParameter ?? "";
            Robot = null;

            if (sourceParameter.IsNullOrEmpty())
            {
                config.SavedRobotName = "";
                HelpText = "[No Robot Loaded]";
                return;
            }

            RosValue parameterValue;
            string? errorMsg;
            try
            {
                HelpText = "- Requesting parameter -";
                (parameterValue, errorMsg) =
                    await RosManager.Connection.GetParameterAsync(sourceParameter, timeoutInMs: ParameterTimeoutInMs);
            }
            catch (OperationCanceledException)
            {
                HelpText = "Error: Task cancelled";
                RosLogger.Debug($"{ToString()}: Error while loading parameter '{sourceParameter}': " +
                                $"Task cancelled or timed out");
                return;
            }
            catch (Exception e)
            {
                RosLogger.Debug($"{ToString()}: Error while loading parameter '{sourceParameter}'", e);
                HelpText = "Error: Failed to retrieve parameter";
                return;
            }

            if (errorMsg != null)
            {
                HelpText = $"Error: {errorMsg}";
                RosLogger.Debug($"{ToString()}: Error while loading parameter '{sourceParameter}': {errorMsg}");
                return;
            }

            if (!parameterValue.TryGet(out string robotDescription))
            {
                RosLogger.Debug($"{ToString()}: Parameter '{sourceParameter}' was not string!");
                HelpText = "Error: Parameter contains no string";
                return;
            }

            if (!LoadRobotFromDescription(robotDescription))
            {
                return;
            }

            config.SavedRobotName = "";
        }

        public async ValueTask TryLoadSavedRobotAsync(string? robotName)
        {
            config.SavedRobotName = "";
            Robot = null;

            if (robotName.IsNullOrEmpty())
            {
                config.SourceParameter = "";
                HelpText = "[No Robot Loaded]";
                return;
            }

            (bool result, string robotDescription) = await Resource.TryGetRobotAsync(robotName);
            if (!result)
            {
                RosLogger.Debug($"{ToString()}: Failed to load robot! Error message: {robotDescription}");
                HelpText = $"[{robotDescription}]";
                return;
            }

            config.SourceParameter = "";
            config.SavedRobotName = robotName;
            LoadRobotFromDescription(robotDescription);
        }

        bool LoadRobotFromDescription(string? description)
        {
            if (description.IsNullOrEmpty())
            {
                RosLogger.Debug($"{ToString()}: Empty parameter '{description}'");
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
                RosLogger.Debug($"{ToString()}: Error parsing description", e);
                HelpText = "[Failed to Parse Description]";
                return false;
            }

            Robot = newRobot;
            Robot.BaseLinkObject.transform.SetParentLocal(node.Transform);

            HelpText = "[Loading Robot...]";

            async ValueTask LoadRobotAsync()
            {
                robotLoadingTask = newRobot.StartAsync(RosManager.Connection, KeepMeshMaterials).AsTask();
                await robotLoadingTask.AwaitNoThrow(this);
                UpdateStartTaskStatus();
            }

            _ = LoadRobotAsync().AwaitNoThrow(this);
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
                        RaiseRobotFinishedLoading();
                        return;
                    case TaskStatus.Canceled:
                        HelpText = "[Robot Task canceled.]";
                        robotLoadingTask = null;
                        RaiseRobotFinishedLoading();
                        return;
                    case TaskStatus.RanToCompletion:
                        node.Name = "SimpleRobotNode:" + Name;
                        if (Robot == null)
                        {
                            HelpText = "[Invalid Robot]";
                        }
                        else if (!string.IsNullOrWhiteSpace(Robot.Name))
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
                        Interactable = Interactable;
                        if (Robot != null)
                        {
                            RobotLinkHighlightable.ProcessRobot(Robot.Name, Robot.BaseLinkObject);
                        }

                        robotLoadingTask = null;
                        RaiseRobotFinishedLoading();
                        break;
                }
            }
            catch (Exception e)
            {
                RosLogger.Error($"{ToString()}: Error during {nameof(UpdateStartTaskStatus)}", e);
            }
        }

        void RaiseStopped()
        {
            Stopped.TryRaise(this);
        }

        void RaiseRobotFinishedLoading()
        {
            RobotFinishedLoading.TryRaise(this);
        }

        [return: NotNullIfNotNull("jointName")]
        string? Decorate(string? jointName)
        {
            if (jointName == null) return null;

            string framePrefix = config.FramePrefix;
            string frameSuffix = config.FrameSuffix;

            if (framePrefix.Length == 0 && frameSuffix.Length == 0)
            {
                return jointName;
            }

            return $"{framePrefix}{jointName}{frameSuffix}";
        }

        void DetachFromTf()
        {
            if (Robot == null)
            {
                return;
            }

            foreach (var (link, parentLink) in Robot.LinkParents)
            {
                if (TfModule.TryGetFrame(Decorate(link), out var frame))
                {
                    frame.RemoveListener(node);
                }

                if (TfModule.TryGetFrame(Decorate(parentLink), out var parentFrame))
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
            var mRobot = Robot;
            var mRobotObject = RobotObject;

            if (mRobot == null || mRobotObject == null)
            {
                return;
            }

            mRobotObject.transform.SetParentLocal(TfModule.RootFrame.Transform);
            foreach (var (link, linkObject) in mRobot.LinkObjects)
            {
                var frame = TfModule.GetOrCreateFrame(Decorate(link), node);
                linkObject.transform.SetParentLocal(frame.Transform);
                linkObject.transform.SetLocalPose(Pose.identity);
            }

            // fill in missing frame parents, but only they don't already have one
            foreach (var (link, parentLink) in mRobot.LinkParents)
            {
                var frame = TfModule.GetOrCreateFrame(Decorate(link), node);
                if (frame.Parent == TfModule.OriginFrame)
                {
                    frame.Parent = TfModule.GetOrCreateFrame(Decorate(parentLink), node);
                }
            }

            node.AttachTo(Decorate(mRobot.BaseLink));
            mRobot.BaseLinkObject.transform.SetParentLocal(node.Transform);
        }

        public void Dispose()
        {
            if (AttachedToTf)
            {
                AttachedToTf = false;
            }

            Robot = null;
            RobotFinishedLoading = null;
            RaiseStopped();
            node.Dispose();
        }

        public override void ResetController()
        {
            Robot = null;

            string savedRobotName = SavedRobotName;
            string sourceParameter = SourceParameter;

            if (!string.IsNullOrWhiteSpace(savedRobotName))
            {
                if (!string.IsNullOrWhiteSpace(sourceParameter))
                {
                    config.SourceParameter = "";
                }

                _ = TryLoadSavedRobotAsync(savedRobotName).AwaitNoThrow(this);
            }

            if (!string.IsNullOrWhiteSpace(sourceParameter))
            {
                _ = TryLoadFromSourceParameterAsync(sourceParameter).AwaitNoThrow(this);
            }


            if (AttachedToTf)
            {
                AttachedToTf = false;
                AttachedToTf = true;
            }
        }

        public override string ToString() => $"[{nameof(SimpleRobotController)} '{(robot?.Name ?? "(none)")}']";
    }
}