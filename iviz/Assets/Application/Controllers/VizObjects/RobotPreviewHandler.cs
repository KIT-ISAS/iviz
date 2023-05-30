#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class RobotPreviewHandler : VizHandler, IHandles<RobotPreview>, IHandlesArray<RobotPreviewArray>
    {
        readonly Dictionary<string, RobotModel> cachedRobots = new();

        public override string Title => "Robot Previews";

        public override string BriefDescription
        {
            get
            {
                string vizObjectsStr = vizObjects.Count switch
                {
                    0 => "<b>No previews</b>",
                    1 => "<b>1 preview</b>",
                    _ => $"<b>{vizObjects.Count.ToString()} previews</b>"
                };

                const string errorStr = "No errors";

                return $"{vizObjectsStr}\n{errorStr}";
            }
        }

        public void Handle(RobotPreviewArray msg)
        {
            foreach (var widget in msg.Previews)
            {
                Handle(widget);
            }
        }
        
        public void Handle(RobotPreview msg)
        {
            switch ((ActionType)msg.Action)
            {
                case ActionType.Remove:
                    HandleRemove(msg.Id);
                    break;
                case ActionType.RemoveAll:
                    RemoveAll();
                    break;
                case ActionType.Add:
                    _ = HandleAddAsync(msg);
                    break;
                default:
                    RosLogger.Error(
                        $"{ToString()}: Object '{msg.Id}' requested unknown action {msg.Action.ToString()}");
                    break;
            }
        }

        async ValueTask HandleAddAsync(RobotPreview msg)
        {
            if (string.IsNullOrWhiteSpace(msg.Id))
            {
                RosLogger.Info($"{ToString()}: Cannot add preview with empty id");
                return;
            }

            if (vizObjects.TryGetValue(msg.Id, out var existingObject))
            {
                var previewObject = (PreviewObject)existingObject;
                previewObject.Update(msg);
                return;
            }

            RobotModel robot;
            if (msg.RobotDescription.Length != 0)
            {
                string robotDescription = msg.RobotDescription;
                robot = new RobotModel(robotDescription);
            }
            else if (msg.SourceParameter.Length != 0)
            {
                var (parameter, errorMsg) =
                    await RosManager.Connection.GetParameterAsync(msg.SourceParameter, nodeName: msg.SourceNode);
                if (errorMsg != null)
                {
                    RosLogger.Error(
                        $"{ToString()}: Preview '{msg.Id}' failed to load parameter '{msg.SourceParameter}'. " +
                        $"Reason: {errorMsg}");
                    return;
                }

                if (!parameter.TryGet(out string robotDescription))
                {
                    RosLogger.Error(
                        $"{ToString()}: Preview '{msg.Id}' failed to load parameter '{msg.SourceParameter}'. " +
                        $"Reason: Parameter not a string.");
                    return;
                }

                robot = new RobotModel(robotDescription);
            }
            else if (msg.SavedRobotName.Length != 0)
            {
                if (cachedRobots.TryGetValue(msg.SavedRobotName, out var existingRobot))
                {
                    robot = existingRobot.Clone();
                }
                else
                {
                    var (result, robotDescription) = await Resource.TryGetRobotAsync(msg.SavedRobotName);
                    if (!result)
                    {
                        RosLogger.Error(
                            $"{ToString()}: Preview '{msg.Id}' failed to load robot from saved name '{msg.SavedRobotName}'. " +
                            $"Reason: {robotDescription}");
                        return;
                    }

                    robot = new RobotModel(robotDescription);
                    cachedRobots[msg.SavedRobotName] = robot;
                }
            }
            else
            {
                RosLogger.Error($"{ToString()}: Preview '{msg.Id}' failed to load robot. All parameters were empty.");
                return;
            }

            var vizObject = new PreviewObject(msg, robot, $"{nameof(RobotPreview)} - {robot.Name}")
                { Interactable = Interactable, Visible = Visible };
            vizObjects[vizObject.id] = vizObject;

            try
            {
                // setting between vizObject creation and update.
                // reason before update: bug where joints not found in new PreviewObject
                // reason after creation: bug where robot was parentless 
                await robot.StartAsync(RosManager.Connection, true, false);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{ToString()}: Preview '{msg.Id}' failed to initialize robot.", e);
            }
            
            vizObject.Update(msg);
            //robot.Visible = true;
        }

        // ----------------------------------------------

        sealed class PreviewObject : VizObject
        {
            readonly RobotModel robot;

            public PreviewObject(RobotPreview msg, RobotModel robot, string typeDescription) : base(msg.Id, typeDescription)
            {
                this.robot = robot;
                robot.BaseLinkObject.transform.SetParentLocal(node.Transform);
            }

            public void Update(RobotPreview msg)
            {
                if (msg.JointNames.Length != msg.JointValues.Length)
                {
                    RosLogger.Error($"{ToString()}: Inconsistent lengths for joint names and values.");
                    return;
                }

                if (msg.RenderAsOcclusionOnly != robot.OcclusionOnly)
                {
                    robot.OcclusionOnly = msg.RenderAsOcclusionOnly;
                }

                bool newVisible = !msg.HideRobot;
                if (newVisible != robot.Visible)
                {
                    robot.Visible = newVisible;
                }
                
                if (msg.Tint != default && msg.Tint.ToUnity() != robot.Tint)
                {
                    robot.Tint = msg.Tint.ToUnity();
                }

                if (!Mathf.Approximately(msg.Metallic, robot.Metallic))
                {
                    robot.Metallic = msg.Metallic;
                }

                if (!Mathf.Approximately(msg.Smoothness, robot.Smoothness))
                {
                    robot.Smoothness = msg.Smoothness;
                }

                if (msg.JointValues.Length == 0)
                {
                    return;
                }

                foreach (var (name, value) in msg.JointNames.Zip(msg.JointValues))
                {
                    robot.TryWriteJoint(name, value);
                }
            }

            public override void Dispose()
            {
                robot.Dispose();
                base.Dispose();
            }
        }
    }
}