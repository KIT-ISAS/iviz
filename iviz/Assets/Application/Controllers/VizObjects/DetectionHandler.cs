#nullable enable

using Iviz.Core;
using Iviz.Displays.XR;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.VisionMsgs;
using UnityEngine;

namespace Iviz.Controllers
{
    public class DetectionHandler : VizHandler, IHandles<DetectionBox>, IHandlesArray<DetectionBoxArray>
    {
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
                    { Interactable = Interactable, Visible = Visible };
                vizObjects[detectionObject.id] = detectionObject;
            }

            detectionObject.Update(msg);
        }

        sealed class DetectionObject : VizObject
        {
            readonly ContainerBoundary boundary;

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
            }
            
            public override void Dispose()
            {
                boundary.Dispose();
                base.Dispose();
            }            
        }
    }
}