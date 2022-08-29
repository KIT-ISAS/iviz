#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XR;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class RobotPreviewHandler : VizHandler
    {
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

        public void Handler(RobotPreview msg)
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
                    RosLogger.Error($"{this}: Object '{msg.Id}' requested unknown " +
                                    $"action {((int)msg.Action).ToString()}");
                    break;
            }
        }

        async ValueTask HandleAddAsync(RobotPreview msg)
        {
            if (string.IsNullOrWhiteSpace(msg.Id))
            {
                RosLogger.Info($"{this}: Cannot add preview with empty id");
                return;
            }
            
            if (vizObjects.TryGetValue(msg.Id, out var existingObject))
            {
                var previewObject = (PreviewObject)existingObject;
                previewObject.Update(msg);
                return;
            }

            string robotDescription;
            if (msg.RobotDescription.Length != 0)
            {
                robotDescription = msg.RobotDescription;
            }
            else if (msg.SourceParameter.Length != 0)
            {
                var (parameter, errorMsg) =
                    await RosManager.Connection.GetParameterAsync(msg.SourceParameter, nodeName: msg.SourceNode);
                if (errorMsg != null)
                {
                    RosLogger.Error($"{this}: Preview '{msg.Id}' failed to load parameter '{msg.SourceParameter}'. " +
                                    $"Reason: {errorMsg}");
                    return;
                }

                if (!parameter.TryGet(out robotDescription))
                {
                    RosLogger.Error($"{this}: Preview '{msg.Id}' failed to load parameter '{msg.SourceParameter}'. " +
                                    $"Reason: Parameter not a string.");
                    return;
                }
            }
            else if (msg.SavedRobotName.Length != 0)
            {
                bool result;
                (result, robotDescription) = await Resource.TryGetRobotAsync(msg.SavedRobotName);
                if (!result)
                {
                    RosLogger.Error(
                        $"{this}: Preview '{msg.Id}' failed to load robot from saved name '{msg.SavedRobotName}'. " +
                        $"Reason: {robotDescription}");
                    return;
                }
            }
            else
            {
                RosLogger.Error($"{this}: Preview '{msg.Id}' failed to load robot. All parameters were empty.");
                return;
            }

            var robot = new RobotModel(robotDescription);

            var vizObject = new PreviewObject(msg.Id, robot, $"{nameof(RobotPreview)} - {robot.Name}")
                { Interactable = Interactable, Visible = Visible };
            vizObjects[vizObject.id] = vizObject;
            
            await robot.StartAsync();
        }

        // ----------------------------------------------

        sealed class PreviewObject : VizObject
        {
            readonly RobotModel robot;

            public PreviewObject(string id, RobotModel robot, string typeDescription) : base(id, typeDescription)
            {
                this.robot = robot;
                robot.BaseLinkObject.transform.SetParentLocal(node.Transform);
            }

            public void Update(RobotPreview msg)
            {
                if (msg.RenderAsOcclusionOnly != robot.OcclusionOnly)
                {
                    robot.OcclusionOnly = msg.RenderAsOcclusionOnly;
                }

                if (msg.Tint != default && msg.Tint.ToUnity() != robot.Tint)
                {
                    robot.Tint = msg.Tint.ToUnity();
                }

                if (msg.Metallic != 0 && Mathf.Approximately(msg.Metallic, robot.Metallic))
                {
                    robot.Metallic = msg.Metallic;
                }

                if (msg.Smoothness != 0 && Mathf.Approximately(msg.Smoothness, robot.Smoothness))
                {
                    robot.Smoothness = msg.Smoothness;
                }

                if (msg.JointValues.Length == 0)
                {
                    return;
                }

                if (msg.JointNames.Length != msg.JointValues.Length)
                {
                    RosLogger.Error($"{this}: Inconsistent lengths for joint names and values.");
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