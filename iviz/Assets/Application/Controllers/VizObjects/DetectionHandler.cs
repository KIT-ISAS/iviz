#nullable enable

using System;
using System.Text;
using Iviz.Core;
using Iviz.Displays.XR;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using UnityEngine;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;
using System.Collections.Generic;
using System.Linq;

namespace Iviz.Controllers
{
    public class DetectionHandler : VizHandler, IHandles<DetectionBox>, IHandlesArray<DetectionBoxArray>
    {
        float minValidScore;

        public override string Title => "Boundaries";

        public override string BriefDescription
        {
            get
            {
                string vizObjectsStr = vizObjects.Count switch
                {
                    0 => "<b>No detections</b>",
                    1 => "<b>1 detection</b>",
                    _ => $"<b>{vizObjects.Count.ToString()} detection</b>"
                };

                const string errorStr = "No errors";

                return $"{vizObjectsStr}\n{errorStr}";
            }
        }

        public float MinValidScore
        {
            get => minValidScore;
            set
            {
                minValidScore = value;
                foreach (var vizObject in vizObjects.Values.Cast<DetectionObject>())
                {
                    vizObject.MinValidScore = value;
                }
            }
        }

        public void Handle(DetectionBoxArray msg)
        {
            foreach (var detection in msg.Boxes)
            {
                Handle(detection);
            }
        }

        public void Handle(DetectionBox msg)
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
                    HandleAdd(msg);
                    break;
                default:
                    RosLogger.Error(
                        $"{ToString()}: Object '{msg.Id}' requested unknown action {msg.Action.ToString()}");
                    break;
            }
        }

        void HandleAdd(DetectionBox msg)
        {
            if (string.IsNullOrWhiteSpace(msg.Id))
            {
                RosLogger.Info($"{ToString()}: Cannot add detection with empty id");
                return;
            }

            DetectionObject detectionObject;
            if (vizObjects.TryGetValue(msg.Id, out var existingObject))
            {
                detectionObject = (DetectionObject)existingObject;
            }
            else
            {
                detectionObject = new DetectionObject(msg, nameof(DetectionObject))
                {
                    Interactable = Interactable,
                    Visible = Visible,
                    MinValidScore = MinValidScore
                };
                vizObjects[detectionObject.id] = detectionObject;
            }

            detectionObject.Update(msg);
        }

        sealed class DetectionObject : VizObject
        {
            readonly ContainerBoundary boundary;
            readonly List<(string Class, float Score)> lastClassifications = new();

            DetectionBox? lastMsg;
            float minValidScore;

            (string Class, float Score)? MaxClassification =>
                lastClassifications.Count != 0
                    ? lastClassifications[0]
                    : null;

            public float MinValidScore
            {
                get => minValidScore;
                set
                {
                    minValidScore = value;
                    UpdateScoreVisible();
                }
            }

            public DetectionObject(DetectionBox msg, string typeDescription) : base(msg.Id, typeDescription)
            {
                boundary = new ContainerBoundary();
                boundary.Transform.SetParentLocal(node.Transform);
            }

            public void Update(DetectionBox msg)
            {
                node.AttachTo(msg.Header.FrameId);

                boundary.HandlePointCloud(msg.PointCloud);
                boundary.Scale = msg.Bounds.Size.Ros2Unity().Abs();
                boundary.Pose = msg.Bounds.Center.Ros2Unity();
                boundary.Color = msg.Color.A > 0
                    ? msg.Color.ToUnity()
                    : Color.white;


                int numClasses = Mathf.Min(msg.Classes.Length, msg.Scores.Length);

                lastClassifications.Clear();
                for (int i = 0; i < numClasses; i++)
                {
                    lastClassifications.Add((msg.Classes[i], (float)msg.Scores[i]));
                }

                lastClassifications.Sort(
                    (pairA, pairB) => -pairA.Score.CompareTo(pairB.Score));

                boundary.Caption = MaxClassification?.Class ?? "";
                UpdateScoreVisible();

                lastMsg = msg;
            }

            void UpdateScoreVisible()
            {
                if (MaxClassification?.Score is { } score)
                {
                    node.Visible = score > MinValidScore;
                }
            }

            public override void GenerateLog(StringBuilder description)
            {
                description.Append(node.Visible ? "<color=#000080ff>" : "<color=#706060ff>");
                description.Append("<b>** Detection #").Append(id).Append(" **</b></color>")
                    .AppendLine();
                if (lastMsg == null)
                {
                    description.Append("(No information)").AppendLine();
                    return;
                }

                description.Append("<b>Position:</b> [");
                RosUtils.FormatPose(lastMsg.Bounds.Center, description, RosUtils.PoseFormat.OnlyPosition);
                description.Append("]").AppendLine();

                description.Append("<b>Orientation:</b> [");
                RosUtils.FormatPose(lastMsg.Bounds.Center, description, RosUtils.PoseFormat.OnlyRotation);
                description.Append("]").AppendLine();

                description.Append("<b>Size:</b> [");
                RosUtils.FormatPose(Pose.Identity.WithPosition(lastMsg.Bounds.Size), description,
                    RosUtils.PoseFormat.OnlyPosition);
                description.Append("]").AppendLine();

                description.Append("<b>Classification:</b> ").AppendLine();

                int numClasses = Mathf.Min(lastMsg.Classes.Length, lastMsg.Scores.Length);
                if (numClasses == 0)
                {
                    description.Append("    [empty]").AppendLine();
                }
                else
                {
                    foreach (var (klass, score) in lastClassifications)
                    {
                        bool invalid = score < MinValidScore; 
                        if (invalid) description.Append("<color=#706060ff>");

                        description
                            .Append("    <b>'").Append(klass).Append("'</b> --> ")
                            .Append(score.ToString("#,0.###"));

                        if (invalid) description.Append("</color>");

                        description.AppendLine();
                    }
                }
            }


            public override void Dispose()
            {
                boundary.Dispose();
                base.Dispose();
            }
        }
    }
}